namespace IChatYou.Common.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static bool HasLikeSpecialChars(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return text.IndexOfAny(new char[] { '%', '_', '[', ']', '^' }) != -1;
        }

        public static string Left(this string value, int maxLength)
        {
            if (maxLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ToLowerFirstChar(this string text)
        {
            if (text == null)
            {
                return null;
            }

            if (text.Length > 1)
            {
                return char.ToLowerInvariant(text[0]) + text.Substring(1);
            }

            return text.ToUpper();
        }

        public static string RemoveController(this string text)
        {
            if (!string.IsNullOrEmpty(text) && text.EndsWith("Controller"))
            {
                text = text.Replace("Controller", "");
            }

            return text;
        }
    }
}
