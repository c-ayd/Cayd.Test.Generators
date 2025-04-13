using System.Text;

namespace Cayd.Test.Generators
{
    public static class PhoneNumberGenerator
    {
        /// <summary>
        /// Generates a random global phone number whose length is between 8 and 15.
        /// </summary>
        /// <returns>Returns a random global phone number.</returns>
        public static string Generate()
            => GeneratePhoneNumber("+", System.Random.Shared.Next(8, 16));

        /// <summary>
        /// Generates a random local phone number whose length is between 8 and 15.
        /// </summary>
        /// <returns>Returns a random local phone number.</returns>
        public static string GenerateLocal()
            => GeneratePhoneNumber("0", System.Random.Shared.Next(8, 16));

        /// <summary>
        /// Generates a random global phone number based on a given length.
        /// </summary>
        /// <param name="numberLength">Length of the phone number. It must be between 8 and 15.</param>
        /// <returns>Returns a random global phone number based on the given length.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GenerateCustomLength(int numberLength)
            => GeneratePhoneNumber("+", numberLength);

        /// <summary>
        /// Generates a random local phone number based on a given length.
        /// </summary>
        /// <param name="numberLength">Length of the phone number. It must be between 8 and 15.</param>
        /// <returns>Returns a random local phone number based on the given length.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GenerateLocalCustomLength(int numberLength)
            => GeneratePhoneNumber("0", numberLength);

        /// <summary>
        /// Generates a random phone number based on a given country code and length.
        /// </summary>
        /// <param name="countryCode">Country code of the phone number. It must be at least 1 character or up to 3 characters.</param>
        /// <param name="numberLength">Length of the phone number. This parameter and the length of countryCode in total must be between 8 and 15.</param>
        /// <returns>Returns a random global phone number based on the given country code and the length.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GenerateCustomCountryCode(string countryCode, int numberLength)
        {
            if (countryCode.Length == 0 || countryCode.Length > 3)
                throw new ArgumentOutOfRangeException(nameof(countryCode), countryCode, "Country code must be at least 1 character or up to 3 characters.");

            return GeneratePhoneNumber("+" + countryCode, numberLength);
        }

        private static string GeneratePhoneNumber(string prefix, int numberLength)
        {
            int totalLength = prefix.Length + numberLength;
            if (totalLength < 8 || totalLength > 15)
                throw new ArgumentOutOfRangeException("Phone number's lengt must be between 8 and 15.");

            StringBuilder builder = new StringBuilder()
                .Append(prefix);

            for (int i = 0; i < numberLength; ++i)
            {
                builder.Append(Digits[System.Random.Shared.Next(0, Digits.Count)]);
            }

            return builder.ToString();
        }

        private static List<string>? digits = null;
        private static List<string> Digits
        {
            get
            {
                if (digits == null)
                {
                    digits = new List<string>();
                    for (int i = 0; i < 10; ++i)
                        digits.Add(i.ToString());
                }

                return digits;
            }
        }
    }
}
