using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public enum Operation : byte
    {
        Plus,
        Minus,
        Mult,
        Div
    }

    public class ExpressionNode
    {
        private decimal? Result { get; set; }
        private ExpressionNode V1 { get; init; }
        private ExpressionNode V2 { get; init; }
        private Operation Operation { get; init; }

        #region parsing string

        public static ExpressionNode FromString(string str) =>
            decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedResult)
                ? new ExpressionNode {Result = parsedResult}
                : TryFindPlusOrMinus(ref str, out var beforePlus)
                    ? new ExpressionNode
                    {
                        V1 = FromString(beforePlus),
                        Operation = ParseOperation(str[0]),
                        V2 = FromString(str[1..])
                    }
                    : TryParseLastNumber(ref str, out var val1)
                        ? new ExpressionNode
                        {
                            V1 = FromString(str[..^1]),
                            Operation = ParseOperation(str[^1]),
                            V2 = new ExpressionNode {Result = val1}
                        }
                        : str![0] is '('
                            ? IsAllSingleBracketExpression(str)
                                ? FromString(str[1..^1])
                                : new ExpressionNode
                                {
                                    V1 = FromString(TakeBrackets(ref str)),
                                    Operation = ParseOperation(str[0]),
                                    V2 = FromString(str[1..])
                                }
                            : throw new Exception("qwe");

        private static bool IsAllSingleBracketExpression(string str)
        {
            if (str[0] is not '(' || str[^1] is not ')') return false;
            var opened = str.Sum(c => c switch
            {
                '(' => 1,
                ')' => -1,
                _ => 0
            });
            return opened is 0;
        }

        private static Operation ParseOperation(char c) => c switch
        {
            '+' => Operation.Plus,
            '-' => Operation.Minus,
            '*' => Operation.Mult,
            '/' => Operation.Div,
            _ => default
        };

        private static bool IsNumber(char c) =>
            c is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' or '.';

        private static bool TryParseLastNumber(ref string str, out decimal result)
        {
            var start = 0;
            for (var i = str.Length - 1; i >= 0; --i)
                if (!IsNumber(str[i]))
                {
                    start = i + 1;
                    break;
                }

            if (!decimal.TryParse(str[start..], NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return false;
            str = str[..start];
            return true;
        }

        private static string TakeBrackets(ref string str)
        {
            if (str[0] is not '(') throw new Exception("метод только для случаев, когда на первом символе '('");

            var opened = 0;
            int closingIndex;
            for (var i = 0;; ++i)
                if (str[i] is '(')
                    ++opened;
                else if (str[i] is ')' && --opened is 0)
                {
                    closingIndex = i;
                    break;
                }

            if (closingIndex is default(int)) throw new Exception("несостыковочка по скобкам");

            var res = str[1..closingIndex];
            str = (closingIndex + 1) < str.Length ? str[(closingIndex + 1)..] : string.Empty;
            return res;
        }

        private static bool TryFindPlusOrMinus(ref string str, out string result)
        {
            var openedBrackets = 0;
            var index = -1;
            for (var i = 0; i < str.Length; ++i)
                if (index is not -1) break;
                else
                    switch (str[i])
                    {
                        case '(':
                            ++openedBrackets;
                            continue;
                        case ')':
                            --openedBrackets;
                            continue;
                        case '-':
                        case '+':
                            if (openedBrackets is 0)
                                index = i;
                            break;
                    }

            result = string.Empty;
            if (index is -1) return false;
            result = str[..index];
            str = str[index..];
            return true;
        }

        #endregion

        public async Task<decimal> GetResultAsync()
        {
            if (Result is not null) return (decimal) Result;
            var v1Task = V1.GetResultAsync();
            var v2Task = V2.GetResultAsync();
            var v1 = await v1Task;
            var v2 = await v2Task;
            Console.WriteLine($"{v1} {OperationToString(Operation)} {v2}");
            await Task.Delay(1000);
            Result = Operation switch
            {
                Operation.Plus => v1 + v2,
                Operation.Minus => v1 - v2,
                Operation.Mult => v1 * v2,
                Operation.Div => v1 / v2,
                _ => throw new ArgumentOutOfRangeException()
            };

            return (decimal) Result;
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
