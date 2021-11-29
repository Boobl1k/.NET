using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication.DB;

// ReSharper disable CommentTypo

namespace WebApplication.Models;

internal static class ExpressionCalculator
{
    //сначала отсекаем случай, когда данная строка есть просто число
    //дальше идем от самых неприоритетных операторов к приоритетным
    //почему? потому что чем выше операция находится в дереве, тем позже она выполняется
    //так мы ищем плюсы и минусы, находящиеся вне скобок (так как они самые лохи в общем)
    //далее мы получили подзадачу, в которой есть скобки, которые мы потом разберем рекурсивно, и умножения с делениями
    //то есть сейчас самые неприоритетные операции - умножение и деление
    //если их выполнять слева направо, у нас будет всегда верный ответ
    //так что тут мы разбираем их СПРАВА НАЛЕВО (помним про очередность выполнения операций в дереве)
    //для этого пытаемся найти самый правый оператор (* или /)
    //осталось разобрать только скобки, символы скобок удаляем и тупо запускаем для этого рекурсию, 
    //не забыв проверить случай, когда все выражение - это одна большая скобка
    public static Expression FromString(string str) =>
        decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedResult)
            ? Expression.Constant(parsedResult)
            : StringParsingHelper.TryFindMiddlePlus(ref str, out var beforePlus)
                ? Compose(
                    FromString(beforePlus),
                    FromString(str[1..]),
                    StringParsingHelper.ParseOperation(str[0]))
                : StringParsingHelper.TryFindLastMultOrDiv(ref str, out var beforeOperation)
                    ? Compose(
                        FromString(beforeOperation),
                        FromString(str[1..]),
                        StringParsingHelper.ParseOperation(str[0]))
                    : str![0] is '('
                        ? StringParsingHelper.IsAllSingleBracketExpression(str)
                            ? FromString(str[1..^1])
                            : Compose(
                                FromString(StringParsingHelper.TakeBrackets(ref str)),
                                FromString(str[1..]),
                                StringParsingHelper.ParseOperation(str[0]))
                        : str[0] is '-' && StringParsingHelper.IsAllSingleBracketExpression(str[1..])
                            ? Negotiate(FromString(str[2..^1]))
                            : throw new Exception(str);

    private static BinaryExpression Compose(Expression e1, Expression e2, Operation operation) =>
        operation switch
        {
            Operation.Plus => Expression.MakeBinary(ExpressionType.Add, e1, e2),
            Operation.Minus => Expression.MakeBinary(ExpressionType.Subtract, e1, e2),
            Operation.Mult => Expression.MakeBinary(ExpressionType.Multiply, e1, e2),
            Operation.Div => Expression.MakeBinary(ExpressionType.Divide, e1, e2),
            _ => throw new Exception("композишь без операции")
        };

    private static UnaryExpression Negotiate(Expression e) =>
        Expression.MakeUnary(ExpressionType.Negate, e, default);

    public static decimal? ExecuteSlowly(Expression expression, ExpressionsCache cache)
    {
        var res = (new SlowExecutor(cache).Visit(expression) as ConstantExpression)?.Value as decimal?;
        cache.SaveChanges();
        return res;
    }

    private class SlowExecutor : ExpressionVisitor
    {
        private readonly ExpressionsCache _cache;
        public SlowExecutor(ExpressionsCache cache) => _cache = cache;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var leftResult = Task.Run(
                () => (ConstantExpression) (
                    node.Left is BinaryExpression leftBinary
                        ? VisitBinary(leftBinary)
                        : node.Left));
            var rightResult = Task.Run(
                () => (ConstantExpression) (
                    node.Right is BinaryExpression rightBinary
                        ? VisitBinary(rightBinary)
                        : node.Right));
            
            var delay = Task.Delay(1000); //глянь на это

            var operation = ParseOperation(node.Method);
            
            Task.WaitAll(leftResult, rightResult);

            var expressionWithoutRes = new ComputedExpression
            {
                V1 = (decimal) leftResult.Result.Value!,
                V2 = (decimal) rightResult.Result.Value!,
                Op = operation
            };

            Console.WriteLine($"{leftResult.Result} {node.Method} {rightResult.Result}");

            var computed = _cache.GetOrSet(expressionWithoutRes, () =>
            {
                var res = node.Method?.Invoke(default,
                    new[] {leftResult.Result.Value, rightResult.Result.Value});
                delay.Wait(); //нифига я умный да?
                return (decimal) res!;
            });

            return Expression.Constant(computed.Res);
        }

        private static Operation ParseOperation(MethodInfo methodInfo) =>
            (decimal) methodInfo.Invoke(default, new object[] {1m, 2m})! switch
            {
                3m => Operation.Plus,
                -1m => Operation.Minus,
                0.5m => Operation.Div,
                2m => Operation.Mult,
                _ => throw new Exception("метод не соответствует ни одной операции")
            };

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var nodeResult = (node.Operand is BinaryExpression binary
                    ? VisitBinary(binary)
                    : node.Operand)
                as ConstantExpression;
            return Expression.Constant(node.Method?.Invoke(default, new[] {nodeResult?.Value}));
        }
    }
}
