using System;
using System.Globalization;
using System.Linq.Expressions;

namespace WebApplication.Models
{
    internal static class ExpressionCalculator
    {
        public static Expression<Func<decimal>> FromString(string str) =>
            decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedResult)
                ? () => parsedResult
                : StringParsingHelper.TryFindPlusOrMinus(ref str, out var beforePlus)
                    ? Compose(
                        FromString(beforePlus),
                        FromString(str[1..]),
                        StringParsingHelper.ParseOperation(str[0]))
                    : StringParsingHelper.TryParseLastNumber(ref str, out var val1)
                        ? Compose(FromString(str[..^1]), () => val1, StringParsingHelper.ParseOperation(str[^1]))
                        : str![0] is '('
                            ? StringParsingHelper.IsAllSingleBracketExpression(str)
                                ? FromString(str[1..^1])
                                : Compose(
                                    FromString(StringParsingHelper.TakeBrackets(ref str)),
                                    FromString(str[1..]),
                                    StringParsingHelper.ParseOperation(str[0]))
                            : throw new Exception("qwe");

        private static Expression<Func<decimal>> Compose(
            Expression<Func<decimal>> e1,
            Expression<Func<decimal>> e2,
            Operation operation) => operation switch
        {
            Operation.Plus => () => e1.AsFunc()() + e2.AsFunc()(),
            Operation.Minus => () => e1.AsFunc()() - e2.AsFunc()(),
            Operation.Mult => () => e1.AsFunc()() * e2.AsFunc()(),
            Operation.Div => () => e1.AsFunc()() / e2.AsFunc()(),
            _ => throw new Exception("композишь без операции")
        };
    }
}
