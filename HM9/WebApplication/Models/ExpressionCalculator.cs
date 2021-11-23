using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;

// ReSharper disable CommentTypo

namespace WebApplication.Models
{
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
        //для этого пытаемся пропарсить самое правое число, и относительно него делаем Expression
        //осталось разобрать только скобки, тупо делаем для этого рекурсию, 
        //не забыв проверить случай, когда все выражение - это одна большая скобка
        public static Expression FromString(string str) =>
            decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedResult)
                ? Expression.Constant(parsedResult)
                : StringParsingHelper.TryFindMiddlePlus(ref str, out var beforePlus)
                    ? Compose(
                        FromString(beforePlus),
                        FromString(str[1..]),
                        StringParsingHelper.ParseOperation(str[0]))
                    : StringParsingHelper.TryParseLastNumber(ref str, out var val1)
                        ? Compose(
                            FromString(str[..^1]),
                            Expression.Constant(val1),
                            StringParsingHelper.ParseOperation(str[^1]))
                        : str![0] is '('
                            ? StringParsingHelper.IsAllSingleBracketExpression(str)
                                ? FromString(str[1..^1])
                                : Compose(
                                    FromString(StringParsingHelper.TakeBrackets(ref str)),
                                    FromString(str[1..]),
                                    StringParsingHelper.ParseOperation(str[0]))
                            : throw new Exception("qwe");

        private static BinaryExpression Compose(Expression e1, Expression e2, Operation operation) =>
            operation switch
            {
                Operation.Plus => Expression.MakeBinary(ExpressionType.Add, e1, e2),
                Operation.Minus => Expression.MakeBinary(ExpressionType.Subtract, e1, e2),
                Operation.Mult => Expression.MakeBinary(ExpressionType.Multiply, e1, e2),
                Operation.Div => Expression.MakeBinary(ExpressionType.Divide, e1, e2),
                _ => throw new Exception("композишь без операции")
            };

        public static decimal? ExecuteSlowly(BinaryExpression expression) =>
            (new SlowExecutor().Visit(expression) as ConstantExpression)?.Value as decimal?;

        private class SlowExecutor : ExpressionVisitor
        {
            protected override Expression VisitBinary(BinaryExpression node)
            {
                Task.Delay(1000).Wait();
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
                Task.WaitAll(leftResult, rightResult);
                Console.WriteLine($"{leftResult.Result} {node.Method} {rightResult.Result}");
                var res = node.Method?.Invoke(default,
                    new[] {leftResult.Result.Value, rightResult.Result.Value});
                return Expression.Constant(res);
            }
        }
    }
}
