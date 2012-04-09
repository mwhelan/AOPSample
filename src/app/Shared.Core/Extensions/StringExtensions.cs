using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shared.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToProperCase(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        public static string ToWords(this string text)
        {
            return NetToString.Convert(text);
        }

        public static string ToProperCaseWords(this string text)
        {
            var words = ToWords(text);
            return words.ToProperCase();
        }

        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return String.Format(stringFormat, args);
        }

        public static string TestableId(this string value)
        {
            return Regex.Replace(value, @"[^-_:A-Za-z0-9]", "_");
        }
    }
}
