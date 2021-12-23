using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication.DB;

namespace WebApplication.Models;

public class CachedCalculator : ICachedCalculator
{
    private readonly ICalculator _calculator;

    public CachedCalculator(ICalculator calculator) => _calculator = calculator;

    public Expression FromString(string str)
    {
        return decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedResult)
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

        static BinaryExpression Compose(Expression e1, Expression e2, Operation operation) =>
            operation switch
            {
                Operation.Plus => Expression.MakeBinary(ExpressionType.Add, e1, e2),
                Operation.Minus => Expression.MakeBinary(ExpressionType.Subtract, e1, e2),
                Operation.Mult => Expression.MakeBinary(ExpressionType.Multiply, e1, e2),
                Operation.Div => Expression.MakeBinary(ExpressionType.Divide, e1, e2),
                _ => throw new Exception("композишь без операции")
            };
        
        static UnaryExpression Negotiate(Expression e) =>
            Expression.MakeUnary(ExpressionType.Negate, e, default);
    }
    
    public decimal CalculateWithCache(Expression expression, ExpressionsCache cache) => 
        (decimal) (new SlowExecutor(cache, _calculator).StartVisiting(expression) as ConstantExpression)!.Value!;

    private class SlowExecutor
    {
        private readonly ExpressionsCache _cache;
        private readonly ICalculator _calculator;

        public SlowExecutor(ExpressionsCache cache, ICalculator calculator)
        {
            _cache = cache;
            _calculator = calculator;
        }

        public Expression StartVisiting(Expression expression) => 
            Visit((dynamic) expression);

        private Expression Visit(BinaryExpression node)
        {
            var leftResult = Task.Run(
                () => (ConstantExpression) (
                    node.Left is BinaryExpression leftBinary
                        ? Visit(leftBinary)
                        : node.Left));
            var rightResult = Task.Run(
                () => (ConstantExpression) (
                    node.Right is BinaryExpression rightBinary
                        ? Visit(rightBinary)
                        : node.Right));

            Task.WaitAll(leftResult, rightResult);

            var expressionWithoutRes = new ComputedExpression
            {
                V1 = (decimal) leftResult.Result.Value!,
                V2 = (decimal) rightResult.Result.Value!,
                Op = ParseOperation(node.Method)
            };

            var computed = _cache.GetOrSet(expressionWithoutRes, () =>
            {
                var res = _calculator.Calculate(expressionWithoutRes.V1, expressionWithoutRes.V2,
                    expressionWithoutRes.Op);
                return res;
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

        private Expression Visit(UnaryExpression node)
        {
            var nodeResult = (node.Operand is BinaryExpression binary
                    ? Visit(binary)
                    : node.Operand)
                as ConstantExpression;
            return Expression.Constant(node.Method?.Invoke(default, new[] {nodeResult?.Value}));
        }
    }
}
