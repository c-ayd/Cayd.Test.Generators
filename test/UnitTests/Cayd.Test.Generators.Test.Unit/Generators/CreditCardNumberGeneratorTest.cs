namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class CreditCardNumberGeneratorTest
    {
        [Fact]
        public void Generate_ShouldReturnValidCreditCardNumber()
        {
            // Arrange
            var americanExpress = new CreditCardNumberGenerator.AmericanExpress();
            var masterCard = new CreditCardNumberGenerator.MasterCard();
            var visa = new CreditCardNumberGenerator.Visa();

            // Act
            var resultAE = CreditCardNumberGenerator.Generate(americanExpress);
            var resultMC = CreditCardNumberGenerator.Generate(masterCard);
            var resultV = CreditCardNumberGenerator.Generate(visa);

            // Assert
            Assert.True(ValidateCreditCardNumber(resultAE), $"American Express is not valid. Card number: {resultAE}");
            Assert.True(ValidateCreditCardNumber(resultMC), $"Master Card is not valid. Card number: {resultMC}");
            Assert.True(ValidateCreditCardNumber(resultV), $"Visa is not valid. Card number: {resultV}");
        }

        private bool ValidateCreditCardNumber(string number)
        {
            bool @double = false;
            int luhn = 0;
            for (int i = number.Length - 1; i >= 0 ; --i)
            {
                int product = int.Parse(number[i].ToString()) * (@double ? 2 : 1);
                if (product > 9)
                {
                    product -= 9;
                }

                luhn += product;
                @double = !@double;
            }

            return luhn % 10 == 0;
        }

        [Fact]
        public void Generate_WhenLengthIsLowerThanIINRange_ShouldThrowArgumentException()
        {
            // Arrange
            var network = new TestCardNetwork();

            // Act
            var result = Record.Exception(() =>
            {
                CreditCardNumberGenerator.Generate(network);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }

        private class TestCardNetwork : CreditCardNumberGenerator.CreditCardNetwork
        {
            public override List<string> IINRanges { get; } = new List<string>()
            {
                "1234"
            };

            public override int CardNumberLength { get; } = 3;
        }
    }
}
