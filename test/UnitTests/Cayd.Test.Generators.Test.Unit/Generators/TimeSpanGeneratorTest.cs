namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class TimeSpanGeneratorTest
    {
        [Fact]
        public void Generate_WhenTimeDirectionIsNotGiven_ShouldGenerateTimeSpanRatherThanZero()
        {
            // Act
            var result = TimeSpanGenerator.Generate();

            // Assert
            Assert.NotEqual(TimeSpan.Zero, result);
        }

        [Theory]
        [InlineData(TimeSpanGenerator.ETimeDirection.Positive)]
        [InlineData(TimeSpanGenerator.ETimeDirection.Negative)]
        public void Generate_WhenTimeDirectionIsGiven_ShouldGenerateTimeSpanRatherThanZero(TimeSpanGenerator.ETimeDirection timeDirection)
        {
            // Act
            var result = TimeSpanGenerator.Generate(timeDirection);

            // Assert
            Assert.NotEqual(TimeSpan.Zero, result);
            Assert.True(timeDirection == TimeSpanGenerator.ETimeDirection.Positive ? 
                (result.TotalSeconds > 0) : (result.TotalSeconds < 0), $"Time Direction: {timeDirection.ToString()}");
        }

        [Fact]
        public void GenerateWithLimit_WhenTimeDirectionIsNotGiven_ShouldGenerateTimeSpanWithinRange()
        {
            // Assert
            var timeLimit = new TimeSpanGenerator.TimeLimit()
            {
                DayLimit = 5,
                HourLimit = 10,
                MinuteLimit = 0,
                SecondLimit = 20,
                MillisecondLimit = 30
            };

            // Act
            var result = TimeSpanGenerator.GenerateWithLimit(timeLimit);

            // Arrange
            Assert.True(result.Days >= -5 && result.Days <= 5, $"Days: {result.Days}, Expected: [-5, 5]");
            Assert.True(result.Hours >= -10 && result.Hours <= 10, $"Hours: {result.Hours}, Expected: [-10, 10]");
            Assert.True(result.Minutes == 0, $"Minutes: {result.Minutes}, Expected: 0");
            Assert.True(result.Seconds >= -20 && result.Seconds <= 20, $"Seconds: {result.Seconds}, Expected: [-20, 20]");
            Assert.True(result.Milliseconds >= -30 && result.Milliseconds <= 30, $"Milliseconds: {result.Milliseconds}, Expected: [-30, 30]");
        }

        [Theory]
        [InlineData(TimeSpanGenerator.ETimeDirection.Positive)]
        [InlineData(TimeSpanGenerator.ETimeDirection.Negative)]
        public void GenerateWithLimit_WhenTimeDirectionIsGiven_ShouldGenerateTimeSpanWithinRange(TimeSpanGenerator.ETimeDirection timeDirection)
        {
            // Assert
            var timeLimit = new TimeSpanGenerator.TimeLimit()
            {
                DayLimit = 5,
                HourLimit = 10,
                MinuteLimit = 0,
                SecondLimit = 20,
                MillisecondLimit = 30
            };

            // Act
            var result = TimeSpanGenerator.GenerateWithLimit(timeDirection, timeLimit);

            // Arrange
            Assert.True(timeDirection == TimeSpanGenerator.ETimeDirection.Negative ? 
                result.Days >= -5 : result.Days <= 5, $"Days: {result.Days}, Direction: {timeDirection.ToString()}");
            Assert.True(timeDirection == TimeSpanGenerator.ETimeDirection.Negative ? 
                result.Hours >= -10 : result.Hours <= 10, $"Hours: {result.Hours}, Direction: {timeDirection.ToString()}");
            Assert.True(result.Minutes == 0, $"Minutes: {result.Minutes}, Expected: 0");
            Assert.True(timeDirection == TimeSpanGenerator.ETimeDirection.Negative ? 
                result.Seconds >= -20 : result.Seconds <= 20, $"Seconds: {result.Seconds}, Direction: {timeDirection.ToString()}");
            Assert.True(timeDirection == TimeSpanGenerator.ETimeDirection.Negative ? 
                result.Milliseconds >= -30 : result.Milliseconds <= 30, $"Milliseconds: {result.Milliseconds}, Direction: {timeDirection.ToString()}");
        }
    }
}
