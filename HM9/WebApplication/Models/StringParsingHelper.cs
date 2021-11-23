using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WebApplication.Models
{
    internal static class StringParsingHelper
    {
        public static Operation ParseOperation(char c) => c switch
        {
            '+' => Operation.Plus,
            '-' => Operation.Minus,
            '*' => Operation.Mult,
            '/' => Operation.Div,
            _ => default
        };

        public static bool IsAllSingleBracketExpression(string str) =>
            str[0] is '(' && str[^1] is ')' &&
            str.Sum(c => c switch
            {
                '(' => 1,
                ')' => -1,
                _ => 0
            }) is 0;

        public static bool TryParseLastNumber(ref string str, out decimal result)
        {
            bool IsNumber(char c) => c is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' or '.';

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

        public static string TakeBrackets(ref string str)
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

        public static bool TryFindMiddlePlus(ref string str, out string beforePlus)
        {
            var openedBrackets = 0;
            var indexes = new List<int>();
            for (var i = 0; i < str.Length; ++i)
                switch (str[i])
                {
                    case '(':
                        ++openedBrackets;
                        break;
                    case ')':
                        --openedBrackets;
                        break;
                    case '+':
                    {
                        if (openedBrackets is 0)
                            indexes.Add(i);
                        break;
                    }
                }

            beforePlus = default;
            if (indexes.Count is 0) return false;
            var middleIndex = indexes[indexes.Count / 2];
            beforePlus = str[..middleIndex];
            str = str[middleIndex..];
            return true;
        }
    }
}
