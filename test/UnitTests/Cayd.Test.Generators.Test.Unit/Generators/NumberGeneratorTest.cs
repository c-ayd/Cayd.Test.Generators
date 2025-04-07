namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class NumberGeneratorTest
    {
        [Theory]
        [InlineData(5, 10, typeof(int))]
        [InlineData(5U, 10U, typeof(long))]
        [InlineData(5.0f, 10.0f, typeof(float))]
        [InlineData(5.0, 10.0, typeof(double))]
        public void GenerateBetween_WhenRangeIsCorrect_ShouldReturnValueWithinRangeWithSameType(object min, object max, Type type)
        {
            // Arrange
            var method = typeof(NumberGenerator).GetMethod("GenerateBetween")!.MakeGenericMethod(type);
            var minParam = Convert.ChangeType(min, type);
            var maxParam = Convert.ChangeType(max, type);

            // Act
            var result = method.Invoke(null, new object[] { minParam, maxParam });

            // Assert
            Assert.True(result!.GetType() == type, $"Returned Type: {result.GetType().Name}. Expected: {type.Name}");
            Assert.True(((IComparable)result).CompareTo(minParam) >= 0, $"Returned Value: {result}. Min Value: {min}");
            Assert.True(((IComparable)result).CompareTo(maxParam) <= 0, $"Returned Value: {result}. Max Value: {min}");
        }

        [Fact]
        public void GenerateBetween_WhenRangeIsNotCorrect_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int min = 10, max = 5;

            // Act
            var result = Record.Exception(() =>
            {
                NumberGenerator.GenerateBetween(min, max);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }

        [Fact]
        public void GenerateBetween_WhenMinMaxAreSame_ShouldReturnLimit()
        {
            // Arrange
            int min = 10, max = 10;

            // Act
            var result = NumberGenerator.GenerateBetween(min, max);

            // Assert
            Assert.Equal(max, result);
        }

        [Fact]
        public void GenerateBetween_WhenTypeIsNotCorrect_ShouldThrowArgumentException()
        {
            // Arrange
            byte min = 5, max = 10;

            // Act
            var result = Record.Exception(() =>
            {
                NumberGenerator.GenerateBetween(min, max);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }
    }
}
