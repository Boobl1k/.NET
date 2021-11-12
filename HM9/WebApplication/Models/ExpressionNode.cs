using System;
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
        public decimal? Result { get; private set; }
        public ExpressionNode V1 { get; set; }
        public ExpressionNode V2 { get; set; }
        public Operation Operation { get; set; }

        #region parsing string

        public static ExpressionNode FromString(string str) =>
            decimal.TryParse(str, out var res)
                ? new ExpressionNode {Result = res}
                : TryParseFirstNumber(ref str, out var val1)
                    ? new ExpressionNode
                    {
                        V1 = new ExpressionNode {Result = val1},
                        Operation = ParseOperation(str[0]),
                        V2 = FromString(str[1..]),
                    }
                    : str[0] == '('
                        ? new ExpressionNode
                        {
                            V1 = FromString(TakeBrackets(ref str)),
                            Operation = ParseOperation(str[0]),
                            V2 = FromString(str[1..])
                        }
                        : throw new Exception("expected '(' not found");

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

        private static bool TryParseFirstNumber(ref string str, out decimal result)
        {
            var end = 0;
            for (var i = 0; i < str.Length; ++i)
                if (!IsNumber(str[i]))
                {
                    end = i;
                    break;
                }

            if (!decimal.TryParse(str[..end], out result)) return false;
            str = str[end..];
            return true;
        }

        private static string TakeBrackets(ref string str)
        {
            var opened = 0;
            int closingIndex;
            for (var i = 0;; ++i)
            {
                if (str[i] is '(')
                    ++opened;
                if (str[i] is ')')
                    --opened;
                if (opened is 0)
                {
                    closingIndex = i;
                    break;
                }
            }

            var res = str[1..closingIndex];
            str = (closingIndex + 1) < str.Length ? str[(closingIndex + 1)..] : string.Empty;
            return res;
        }

        #endregion

        public async Task<decimal> GetResultAsync()
        {
            if (Result is not null) return (decimal) Result;
            var v1Task = V1.GetResultAsync();
            var v2Task = V2.GetResultAsync();
            Result = Operation switch
            {
                Operation.Plus => await v1Task + await v2Task,
                Operation.Minus => await v1Task - await v2Task,
                Operation.Mult => await v1Task * await v2Task,
                Operation.Div => await v1Task / await v2Task,
                _ => throw new ArgumentOutOfRangeException()
            };
            return (decimal) Result;
        }
    }
}
