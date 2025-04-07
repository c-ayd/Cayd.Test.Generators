namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class BooleanGeneratorTest
    {
        [Fact]
        public void Generate_ShouldReturnBoolean()
        {
            // Act
            var result = BooleanGenerator.Generate();

            // Assert
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(0.1)]
        [InlineData(0.0)]
        [InlineData(1.0)]
        public void GenerateWithProbability_WhenProbabilityIsWithinRange_ShouldReturnBoolean(double probability)
        {
            // Act
            var result = BooleanGenerator.GenerateWithProbability(probability);

            // Assert
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        public void GenerateWithProbability_WhenProbabilityIsNotWithinRange_ShouldThrowArgumentOutOfRangeException(double probability)
        {
            // Act
            var result = Record.Exception(() =>
            {
                BooleanGenerator.GenerateWithProbability(probability);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }
    }
}
