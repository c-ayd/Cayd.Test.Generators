using System.Text;
using System.Text.RegularExpressions;

namespace Cayd.Test.Generators
{
    public static class StringGenerator
    {
        /// <summary>
        /// Generates a random string using Ascii characters.
        /// </summary>
        /// <param name="length">Length of the string.</param>
        /// <returns>Returns a random string containing only Ascii characters.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateUsingAsciiChars(int length)
            => GenerateString(AsciiChars, length);

        /// <summary>
        /// Generates a random string using Base64 URL characters.
        /// </summary>
        /// <param name="length">Length of the string.</param>
        /// <returns>Returns a random string containing only Base64 URL characters.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateUsingBase64UrlChars(int length)
            => GenerateString(Base64UrlChars, length);

        /// <summary>
        /// Generates a random string using custom characters.
        /// </summary>
        /// <param name="length">Length of the string.</param>
        /// <returns>Returns a random string containing only the given characters.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateUsingCustomChars(List<char> chars, int length)
            => GenerateString(chars, length);

        /// <summary>
        /// Generates a random string using Ascii characters with a format.
        /// </summary>
        /// <param name="format">Format to generate string. Example: "Hello, {0}. Welcome back!"</param>
        /// <param name="parameterLengths">Length of the parameters in the format. Pass only one length if all parameters need to be the same length. Otherwise, the number of lengths must match with the number of the parameters.</param>
        /// <returns>Returns a random string containing only Ascii characters.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateUsingAsciiCharsWithFormat(string format, params int[] parameterLengths)
            => GenerateStringWithFormat(format, AsciiChars, parameterLengths);

        /// <summary>
        /// Generates a random string using Base64 URL characters with a format.
        /// </summary>
        /// <param name="format">Format to generate string. Example: "Hello, {0}. Welcome back!"</param>
        /// <param name="parameterLengths">Length of the parameters in the format. Pass only one length if all parameters need to be the same length. Otherwise, the number of lengths must match with the number of the parameters.</param>
        /// <returns>Returns a random string containing only Base64 URL characters.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateUsingBase64UrlCharsWithFormat(string format, params int[] parameterLengths)
            => GenerateStringWithFormat(format, Base64UrlChars, parameterLengths);

        /// <summary>
        /// Generates a random string using custom characters with a format.
        /// </summary>
        /// <param name="format">Format to generate string. Example: "Hello, {0}. Welcome back!"</param>
        /// <param name="parameterLengths">Length of the parameters in the format. Pass only one length if all parameters need to be the same length. Otherwise, the number of lengths must match with the number of the parameters.</param>
        /// <returns>Returns a random string containing only the given characters.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateUsingCustomCharsWithFormat(string format, List<char> chars, params int[] parameterLengths)
            => GenerateStringWithFormat(format, chars, parameterLengths);

        private static string GenerateString(List<char> chars, int length)
        {
            if (chars == null || chars.Count == 0)
                throw new ArgumentException("The character set is empty.", nameof(chars));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[System.Random.Shared.Next(0, chars.Count)]);
            }

            return builder.ToString();
        }

        private static string GenerateStringWithFormat(string format, List<char> chars, params int[] parameterLengths)
        {
            Regex regex = new Regex("{.*?}");
            int numOfParameters = regex.Matches(format).Count;
            if (numOfParameters == 0)
                throw new ArgumentException($"There is no parameter in the format.", nameof(format));

            if (parameterLengths.Count() == 0)
                throw new ArgumentException("There must be at least one length element to generate strings.", nameof(parameterLengths));
            if (parameterLengths.Length != 1 && parameterLengths.Length != numOfParameters)
                throw new ArgumentException("There must be either one length element for all parameters in the format or the number of lengths must match with the number of the parameters.", nameof(parameterLengths));

            List<string> parameters = new List<string>();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < numOfParameters; i++)
            {
                int length = parameterLengths.Length == 1 ? parameterLengths[0] : parameterLengths[i];
                for (int j = 0; j < length; j++)
                {
                    builder.Append(chars[System.Random.Shared.Next(0, chars.Count)]);
                }

                parameters.Add(builder.ToString());
                builder.Clear();
            }

            return string.Format(format, parameters.ToArray());
        }

        private static List<char>? asciiChars = null;
        private static List<char> AsciiChars
        {
            get
            {
                if (asciiChars == null)
                {
                    asciiChars = new List<char>();
                    for (int i = 32; i <= 126; ++i)
                    {
                        asciiChars.Add((char)i);
                    }
                }

                return asciiChars;
            }
        }

        private static List<char>? base64UrlChars = null;
        private static List<char> Base64UrlChars
        {
            get
            {
                if (base64UrlChars == null)
                {
                    base64UrlChars = new List<char>();
                    for (char i = 'A'; i <= 'Z'; ++i)
                        base64UrlChars.Add(i);
                    for (char i = 'a'; i <= 'z'; ++i)
                        base64UrlChars.Add(i);
                    for (char i = '0'; i <= '9'; ++i)
                        base64UrlChars.Add(i);

                    base64UrlChars.Add('+');
                    base64UrlChars.Add('=');
                }

                return base64UrlChars;
            }
        }
    }
}
