namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class EnumerableGeneratorTest
    {
        [Fact]
        public void Generate_WhenLimitsAreCorrect_ShouldReturnEnumerableWithElementCountWithinRange()
        {
            // Act
            var result = EnumerableGenerator.Generate<int>();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<int>>(result);
            Assert.True(result.Count() >= 3 && result.Count() <= 5, $"The count is not within 3 and 5. Count: {result.Count()}");
        }

        [Theory]
        [InlineData(-5, 5)]
        [InlineData(5, 1)]
        public void Generate_WhenLimitsAreNotCorrect_ShouldThrowArgumentOutOfRangeException(int min, int max)
        {
            // Act
            var result = Record.Exception(() =>
            {
                EnumerableGenerator.Generate<int>(min, max);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }
    }
}
