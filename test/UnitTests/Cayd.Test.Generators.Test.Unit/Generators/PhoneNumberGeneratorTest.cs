using System.Reflection;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class PhoneNumberGeneratorTest
    {
        [Fact]
        public void GeneratePhoneNumber_WhenPhoneNumberLengthIsWithinRange_ShouldReturnPhoneNumber()
        {
            // Arrange
            var method = typeof(PhoneNumberGenerator).GetMethod("GeneratePhoneNumber", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> characters = new List<char>() { '+', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            // Act
            var result = (string)method!.Invoke(null, new object[] { "+1", 10 })!;

            // Assert
            Assert.Equal(12, result.Length);
            Assert.True(result.All(n => characters.Contains(n)));
        }

        [Fact]
        public void GeneratePhoneNumber_WhenPhoneNumberLengthIsNotWithinRange_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var method = typeof(PhoneNumberGenerator).GetMethod("GeneratePhoneNumber", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = Record.Exception(() =>
            {
                method!.Invoke(null, new object[] { "+1", 14 });
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result.InnerException);
        }

        [Fact]
        public void GenerateCustomCountryCode_WhenCountryCodeIsNotWithinRange_ShouldThrowArgumentOutOfRangeException()
        {
            // Act
            var result = Record.Exception(() =>
            {
                PhoneNumberGenerator.GenerateCustomCountryCode("+1111", 8);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }
    }
}
