using System.Text;

namespace Cayd.Test.Generators
{
    public static class CreditCardNumberGenerator
    {
        /// <summary>
        /// Generates a random credit card number that can pass Luhn algorithm.
        /// </summary>
        /// <param name="network">Credit card network. This can be user defined or pre-ready ones coming with the library: <see cref="AmericanExpress"/> <see cref="MasterCard"/> <see cref="Visa"/></param>
        /// <returns>Returns a random credit card number.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Generate(CreditCardNetwork network)
        {
            StringBuilder builder = new StringBuilder()
                .Append(network.IINRanges[System.Random.Shared.Next(0, network.IINRanges.Count)]);

            int length = network.Length - builder.Length;
            if (length < 1)
                throw new ArgumentException("The card's length is lower than the card's IIN range.");

            --length;
            for (int i = 0; i < length; ++i)
            {
                builder.Append(Digits[System.Random.Shared.Next(0, Digits.Count)]);
            }

            bool @double = true;
            int luhn = 0;
            for (int i = builder.Length - 1; i >= 0; --i)
            {
                int product = int.Parse(builder[i].ToString()) * (@double ? 2 : 1);
                if (product > 9)
                {
                    product -= 9;
                }

                luhn += product;
                @double = !@double;
            }

            int remaining = 10 - (luhn % 10);
            builder.Append(remaining);

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

        public abstract class CreditCardNetwork
        {
            public abstract List<string> IINRanges { get; }
            public abstract int Length { get; }
        }

        public sealed class AmericanExpress : CreditCardNetwork
        {
            public override List<string> IINRanges { get; } = new List<string>()
            {
                "34", "37"
            };

            public override int Length { get; } = 15;
        }

        public sealed class MasterCard : CreditCardNetwork
        {
            public override List<string> IINRanges { get; } = new List<string>()
            {
                "2221", "2720", "51", "55"
            };

            public override int Length { get; } = 16;
        }

        public sealed class Visa : CreditCardNetwork
        {
            public override List<string> IINRanges { get; } = new List<string>()
            {
                "4"
            };

            public override int Length { get; } = 16;
        }
    }
}
