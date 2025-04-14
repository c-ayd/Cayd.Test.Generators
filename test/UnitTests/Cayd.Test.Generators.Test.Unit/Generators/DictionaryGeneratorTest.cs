namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class DictionaryGeneratorTest
    {
        [Fact]
        public void Generate_WhenLimitsAreCorrect_ShouldReturnDictionaryWithElementCountWithinRange()
        {
            // Act
            var result = DictionaryGenerator.Generate<int, int>();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IDictionary<int, int>>(result);
            Assert.True(result.Count() >= 3 && result.Count() <= 5, $"The count is not within 5 and 9. Count: {result.Count()}");
        }

        [Theory]
        [InlineData(-5, 5)]
        [InlineData(5, 1)]
        public void Generate_WhenLimitsAreNotCorrect_ShouldThrowArgumentOutOfRangeException(int min, int max)
        {
            // Act
            var result = Record.Exception(() =>
            {
                DictionaryGenerator.Generate<int, int>(min, max);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }
    }
}
