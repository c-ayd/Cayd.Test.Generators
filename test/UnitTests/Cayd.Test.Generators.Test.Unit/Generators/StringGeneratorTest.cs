using System.Reflection;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class StringGeneratorTest
    {
        [Fact]
        public void GenerateString_WhenCharacterListIsGiven_ShouldGenerateStringWithOnlyThoseCharacters()
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateString", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e' };

            // Act
            var result = (string)method!.Invoke(null, new object[] { chars, 5 })!;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length == 5, $"Length: {result.Length}, Expected: 5");
            Assert.True(result.All(c => chars.Contains(c)), "Result contains characters that are not in the list.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new char[] { })]
        public void GenerateString_WhenCharacterListIsNullOrDoesNotContainAnyCharacter_ShouldThrowArgumentException(char[] charArray)
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateString", BindingFlags.NonPublic | BindingFlags.Static);
            List<char>? chars = null;
            if (charArray != null)
            {
                chars = new List<char>();
                foreach (var item in chars)
                {
                    chars.Add(item);
                }
            }

            // Act
            var result = Record.Exception(() =>
            {
                method!.Invoke(null, new object[] { chars!, 5 });
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result.InnerException);
        }

        [Fact]
        public void GenerateStringWithFormat_WhenCharacterListAndFormatAndSameNumberOfLengthsAreGiven_ShouldGenerateStringWithOnlyThoseCharactersWithRelatedLengthsInSameFormat()
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateStringWithFormat", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e' };
            string format = "Hello -{0}-{1}- World";

            // Act
            var result = (string)method!.Invoke(null, new object[] { format, chars, new int[] { 5, 10 } })!;
            var parts = result.Split('-');
            var string1 = parts[1];
            var string2 = parts[2];
            var expected = string.Format(format, string1, string2);

            // Assert
            Assert.NotNull(result);
            Assert.True(expected == result, $"Result: {result}, Expected: {expected}");
            Assert.True(string1.Length == 5, $"String1 Length: {string1.Length}, Expected: 5");
            Assert.True(string1.All(c => chars.Contains(c)), "String1 contains characters that are not in the list.");
            Assert.True(string2.Length == 10, $"String2 Length: {string2.Length}, Expected: 10");
            Assert.True(string2.All(c => chars.Contains(c)), "String2 contains characters that are not in the list.");
        }

        [Fact]
        public void GenerateStringWithFormat_WhenCharacterListAndFormatAndOneLengthAreGiven_ShouldGenerateStringWithOnlyThoseCharactersWithSameLengthInSameFormat()
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateStringWithFormat", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e' };
            string format = "Hello -{0}-{1}- World";

            // Act
            var result = (string)method!.Invoke(null, new object[] { format, chars, new int[] { 5 } })!;
            var parts = result.Split('-');
            var string1 = parts[1];
            var string2 = parts[2];
            var expected = string.Format(format, string1, string2);

            // Assert
            Assert.NotNull(result);
            Assert.True(expected == result, $"Result: {result}, Expected: {expected}");
            Assert.True(string1.Length == 5, $"String1 Length: {string1.Length}, Expected: 5");
            Assert.True(string1.All(c => chars.Contains(c)), "String1 contains characters that are not in the list.");
            Assert.True(string2.Length == 5, $"String2 Length: {string2.Length}, Expected: 5");
            Assert.True(string2.All(c => chars.Contains(c)), "String2 contains characters that are not in the list.");
        }

        [Fact]
        public void GenerateStringWithFormat_WhenThereIsNoParameterInFormat_ShouldThrowArgumentException()
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateStringWithFormat", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e' };
            string format = "Hello World";

            // Act
            var result = Record.Exception(() =>
            {
                method!.Invoke(null, new object[] { format, chars, new int[] { 5 } });
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result.InnerException);
        }

        [Fact]
        public void GenerateStringWithFormat_WhenNoLengthForParametersIsGiven_ShouldThrowArgumentException()
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateStringWithFormat", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e' };
            string format = "Hello -{0}-{1}- World";

            // Act
            var result = Record.Exception(() =>
            {
                method!.Invoke(null, new object[] { format, chars, new int[] { } });
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result.InnerException);
        }

        [Fact]
        public void GenerateStringWithFormat_WhenNumberOfLengthsForParametersIsNotOneAndDoesNotMatchWithLengthOfParameters_ShouldThrowArgumentException()
        {
            // Arrange
            var method = typeof(StringGenerator).GetMethod("GenerateStringWithFormat", BindingFlags.NonPublic | BindingFlags.Static);
            List<char> chars = new List<char>() { 'a', 'b', 'c', 'd', 'e' };
            string format = "Hello -{0}-{1}- World";

            // Act
            var result = Record.Exception(() =>
            {
                method!.Invoke(null, new object[] { format, chars, new int[] { 5, 10, 15 } });
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result.InnerException);
        }
    }
}
