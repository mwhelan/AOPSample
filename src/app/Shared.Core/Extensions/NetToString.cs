using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Shared.Core.Extensions
{
    /// <summary>
    /// Takes variously formatted strings and turns them into words with spaces
    /// </summary>
    public static class NetToString
    {
        static readonly Func<string, string> FromUnderscoreSeparatedWords = methodName => string.Join(" ", methodName.Split(new[] { '_' }));
        static string FromPascalCase(string name)
        {
            var chars = name.Aggregate(
                new List<char>(),
                (list, currentChar) =>
                    {
                        if (currentChar == ' ')
                        {
                            list.Add(currentChar);
                            return list;
                        }

                        if (list.Count == 0)
                        {
                            list.Add(currentChar);
                            return list;
                        }

                        var lastCharacterInTheList = list[list.Count - 1];
                        if (lastCharacterInTheList != ' ')
                        {
                            if (char.IsDigit(lastCharacterInTheList))
                            {
                                if (char.IsLetter(currentChar))
                                    list.Add(' ');
                            }
                            else if (!char.IsLower(currentChar))
                                list.Add(' ');
                        }

                        list.Add(char.ToLower(currentChar, CultureInfo.CurrentCulture));

                        return list;
                    });

            var result = new string(chars.ToArray());
            return result.Replace(" i ", " I "); // I is an exception
        }

        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible",
            Justification = "Naaa.  It's fine as it is.")]
        public static Func<string, string> Convert = name => FromPascalCase(FromUnderscoreSeparatedWords(name));
    }
}