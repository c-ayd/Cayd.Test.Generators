using System.Text;

namespace Cayd.Test.Generators
{
    public static class PasswordGenerator
    {
        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <param name="length">Length of the password. The default is 6.</param>
        /// <returns>Returns a random password.</returns>
        public static string Generate(int length = 6)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; ++i)
            {
                builder.Append(AllCharacters[Random.Shared.Next(0, AllCharacters.Count)]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates a random password with respect to the given rules.
        /// </summary>
        /// <param name="length">Length of the password.</param>
        /// <param name="requireDigit">Whether the password requires a digit.</param>
        /// <param name="requireLowercase">Whether the password requires a lowercase character.</param>
        /// <param name="requireUppercase">Whether the password requires an uppercase character.</param>
        /// <param name="requireNonAlphaNumeric">Whether the password requires a non-alphanumeric character</param>
        /// <returns>Returns a random password.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateWithCustomRules(int length, bool requireDigit, bool requireLowercase, bool requireUppercase, bool requireNonAlphaNumeric)
        {
            int leastNumberOfCharacters = 0;
            if (requireDigit) ++leastNumberOfCharacters;
            if (requireLowercase) ++leastNumberOfCharacters;
            if (requireUppercase) ++leastNumberOfCharacters;
            if (requireNonAlphaNumeric) ++leastNumberOfCharacters;
            if (length < leastNumberOfCharacters)
                throw new ArgumentException($"According to the rules, the least number of length should be {leastNumberOfCharacters}", nameof(length));

            StringBuilder builder = new StringBuilder();
            if (requireDigit) builder.Append(Digits[Random.Shared.Next(0, Digits.Count)]);
            if (requireLowercase) builder.Append(LowercaseCharacters[Random.Shared.Next(0, LowercaseCharacters.Count)]);
            if (requireUppercase) builder.Append(UppercaseCharacters[Random.Shared.Next(0, UppercaseCharacters.Count)]);
            if (requireNonAlphaNumeric) builder.Append(NonAlphanumericCharacters[Random.Shared.Next(0, NonAlphanumericCharacters.Count)]);

            length -= builder.Length;
            for (int i = 0; i < length; ++i)
            {
                builder.Append(AllCharacters[Random.Shared.Next(0, AllCharacters.Count)]);
            }

            return builder.ToString();
        }

        private static List<char>? digits = null;
        private static List<char> Digits
        {
            get
            {
                if (digits == null)
                {
                    digits = new List<char>();
                    for (char i = '0'; i < '9'; ++i)
                        digits.Add(i);
                }

                return digits;
            }
        }

        private static List<char>? lowercaseCharacters = null;
        private static List<char> LowercaseCharacters
        {
            get
            {
                if (lowercaseCharacters == null)
                {
                    lowercaseCharacters = new List<char>();
                    for (char i = 'a'; i < 'z'; ++i)
                        lowercaseCharacters.Add(i);
                }

                return lowercaseCharacters;
            }
        }

        private static List<char>? uppercaseCharacters = null;
        private static List<char> UppercaseCharacters
        {
            get
            {
                if (uppercaseCharacters == null)
                {
                    uppercaseCharacters = new List<char>();
                    for (char i = 'A'; i < 'Z'; ++i)
                        uppercaseCharacters.Add(i);
                }

                return uppercaseCharacters;
            }
        }

        private static List<char>? nonAlphanumericCharacters = null;
        private static List<char> NonAlphanumericCharacters
        {
            get
            {
                if (nonAlphanumericCharacters == null)
                {
                    nonAlphanumericCharacters = new List<char>()
                    {
                        ' ', '!', '"', '#', '$', '%', '&', '\'', '(',')', '*', '+', ',', '-', '.', '/', ':',
                        ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~'
                    };
                }

                return nonAlphanumericCharacters;
            }
        }

        private static List<char>? allCharacters = null;
        private static List<char> AllCharacters
        {
            get
            {
                if (allCharacters == null)
                {
                    allCharacters = new List<char>();
                    allCharacters.AddRange(Digits);
                    allCharacters.AddRange(LowercaseCharacters);
                    allCharacters.AddRange(UppercaseCharacters);
                    allCharacters.AddRange(NonAlphanumericCharacters);
                }

                return allCharacters;
            }
        }
    }
}
