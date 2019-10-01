using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkContractor.BLL
{
    public static class ShortCodeTransform
    {
        private static readonly IReadOnlyList<char> Chars;

        static ShortCodeTransform()
        {
            Chars = ConfigureList().ToArray();
#warning operations without overflow check
        }

        public static int? ConvertToInt(this string code)
        {
            var lth = Chars.Count;
            var stack = new Stack<char>(code);

            var result = 0;
            while (stack.Count > 0)
            {
                result *= lth;
                var top = stack.Pop();
                var i = Chars.GetIndex(top);
                if (i is null) return null;
                result += i.Value + 1;
            }

            return result;
        }

        public static string Convert(this int code)
        {
            var lth = Chars.Count;
            var list = new List<char>(16);
            while (code > 0)
            {
                var mod = code % lth;
                code /= lth;

                list.Add(Chars[mod - 1]);
            }

            return new string(list.ToArray()); //without reverse
        }

        public static string Convert(this Guid code)
        {
            return code.ToString();
        }

        public static Guid? ConvertToGuid(this string code)
        {
            var successful = Guid.TryParse(code, out var result);
            return successful ? (Guid?) result : null;
        }

        private static IEnumerable<char> ConfigureList()
        {
            var result = CharRange('a', 'z')
                .Concat(CharRange('A', 'Z'))
                .Concat(CharRange('1', '9'));

            result = result.ExcludeChar('l')
                .ExcludeChar('I')
                .ExcludeChar('O');

            return result;
        }

        private static IEnumerable<char> CharRange(char first, char last)
        {
            return Enumerable.Range(first, last - first + 1).Select(i => (char) i);
        }

        private static IEnumerable<char> ExcludeChar(this IEnumerable<char> en, char ch)
        {
            return en.Where(i => i != ch);
        }

        private static int? GetIndex(this IReadOnlyList<char> array, char of)
        {
            for (var i = 0; i < array.Count; i++)
                if (array[i] == of)
                    return i;
            return null;
        }

        public static bool IsShortCode(this string code)
        {
            return code.Length < 7;
        }
    }
}