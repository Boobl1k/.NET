using System;
using System.Globalization;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    internal class ExpressionNode
    {
        private decimal? Result { get; set; }
        private ExpressionNode V1 { get; init; }
        private ExpressionNode V2 { get; init; }
        private Operation Operation { get; init; }

        public static ExpressionNode FromString(string str) =>
            decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedResult)
                ? new ExpressionNode {Result = parsedResult}
                : StringParsingHelper.TryFindLastPlusOrMinus(ref str, out var beforePlus)
                    ? new ExpressionNode
                    {
                        V1 = FromString(beforePlus),
                        Operation = StringParsingHelper.ParseOperation(str[0]),
                        V2 = FromString(str[1..])
                    }
                    : StringParsingHelper.TryParseLastNumber(ref str, out var val1)
                        ? new ExpressionNode
                        {
                            V1 = FromString(str[..^1]),
                            Operation = StringParsingHelper.ParseOperation(str[^1]),
                            V2 = new ExpressionNode {Result = val1}
                        }
                        : str![0] is '('
                            ? StringParsingHelper.IsAllSingleBracketExpression(str)
                                ? FromString(str[1..^1])
                                : new ExpressionNode
                                {
                                    V1 = FromString(StringParsingHelper.TakeBrackets(ref str)),
                                    Operation = StringParsingHelper.ParseOperation(str[0]),
                                    V2 = FromString(str[1..])
                                }
                            : throw new Exception("qwe");

        public async Task<decimal> GetResultAsync()
        {
            if (Result is not null) return (decimal) Result;
            var v1Task = V1.GetResultAsync();
            var v2Task = V2.GetResultAsync();
            var v1 = await v1Task;
            var v2 = await v2Task;
            Console.WriteLine($"{v1} {OperationToString(Operation)} {v2}");
            await Task.Delay(1000);
            return (decimal) (Result = Operation switch
            {
                Operation.Plus => v1 + v2,
                Operation.Minus => v1 - v2,
                Operation.Mult => v1 * v2,
                Operation.Div => v1 / v2,
                _ => throw new ArgumentOutOfRangeException()
            });
        }

        private static char OperationToString(Operation operation) => operation switch
        {
            Operation.Plus => '+',
            Operation.Minus => '-',
            Operation.Mult => '*',
            Operation.Div => '/',
            _ => ' '
        };

        public override string ToString() =>
            Result is null ? $"({V1} {OperationToString(Operation)} {V2})" : Result.ToString();
    }
}
