using static Cayd.Test.Generators.Utility.Utility;

namespace Cayd.Test.Generators.Test.Unit.Utility
{
    public class LerpTest
    {
        private const double TOLERANCE = 0.001;

        [Theory]
        [InlineData(10.0, 20.0, 15.0, 0.5)]
        [InlineData(10.0, 20.0, 12.0, 0.2)]
        [InlineData(10.0, 20.0, 18.0, 0.8)]
        [InlineData(10.0, 20.0, 10.0, 0.0)]
        [InlineData(10.0, 20.0, 20.0, 1.0)]
        public void Lerp_WhenValueWithinRange_ShouldReturnBetweenZeroAndOne(double y1, double y2, double x, double value)
        {
            // Act
            double result = Lerp(y1, y2, x);

            // Assert
            Assert.Equal(value, result, TOLERANCE);
        }

        [Theory]
        [InlineData(10.0, 20.0, 5.0, 0.0)]
        [InlineData(10.0, 20.0, 25.0, 1.0)]
        public void Lerp_WhenValueExceedsRange_ShouldReturnZeroOrOne(double y1, double y2, double x, double value)
        {
            // Act
            double result = Lerp(y1, y2, x);

            // Assert
            Assert.Equal(value, result, TOLERANCE);
        }

        [Theory]
        [InlineData(10.0, 10.0)]
        [InlineData(20.0, 10.0)]
        public void Lerp_WhenUpperLimitIsEqualOrGreaterThanLowerLimit_ShouldReturnZero(double y1, double y2)
        {
            // Act
            double result = Lerp(y1, y2, 1.0);

            // Assert
            Assert.Equal(0.0, result, TOLERANCE);
        }
    }
}
