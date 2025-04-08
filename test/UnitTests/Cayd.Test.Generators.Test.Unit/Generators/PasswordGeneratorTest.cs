using System.Reflection;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class PasswordGeneratorTest
    {
        [Fact]
        public void Generate_WhenNoParameterIsGiven_ShouldReturnRandomPasswordWithLengthOfSix()
        {
            // Act
            var result = PasswordGenerator.Generate();

            // Assert
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public void Generate_WhenLengthIsGiven_ShouldReturnRandomPasswordWithSameLength()
        {
            // Arrange
            int length = 10;

            // Act
            var result = PasswordGenerator.Generate(length);

            // Assert
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GenerateWithCustomRules_WhenLengthIsEqualOrGreaterThanMinimumRequiredLength_ShouldReturnRandomPasswordWithSameLength()
        {
            // Arrange
            var digits = (List<char>)(typeof(PasswordGenerator).GetProperty("Digits", BindingFlags.NonPublic | BindingFlags.Static))!.GetValue(null)!;
            var lowercaseCharacters = (List<char>)(typeof(PasswordGenerator).GetProperty("LowercaseCharacters", BindingFlags.NonPublic | BindingFlags.Static))!.GetValue(null)!;
            var uppercaseCharacters = (List<char>)(typeof(PasswordGenerator).GetProperty("UppercaseCharacters", BindingFlags.NonPublic | BindingFlags.Static))!.GetValue(null)!;
            var nonAlphanumericCharacters = (List<char>)(typeof(PasswordGenerator).GetProperty("NonAlphanumericCharacters", BindingFlags.NonPublic | BindingFlags.Static))!.GetValue(null)!;
            int length = 10;

            // Act
            var result = PasswordGenerator.GenerateWithCustomRules(length, true, true, true, true);

            // Assert
            Assert.Equal(length, result.Length);
            Assert.True(digits.Any(d => result.Contains(d)));
            Assert.True(lowercaseCharacters.Any(d => result.Contains(d)));
            Assert.True(uppercaseCharacters.Any(d => result.Contains(d)));
            Assert.True(nonAlphanumericCharacters.Any(d => result.Contains(d)));
        }

        [Fact]
        public void GenerateWithCustomRules_WhenLengthIsLowerThanMinimumRequiredLength_ShouldThrowArgumentException()
        {
            // Arrange
            int length = 3;

            // Act
            var result = Record.Exception(() =>
            {
                PasswordGenerator.GenerateWithCustomRules(length, true, true, true, true);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }
    }
}
