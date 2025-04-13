namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class DateTimeGeneratorTest
    {
        [Theory]
        [InlineData(DateTimeGenerator.ETimeZone.UTC)]
        [InlineData(DateTimeGenerator.ETimeZone.Local)]
        public void GenerateNow_WhenTimeZoneIsGiven_ShouldReturnDateTimeAccordingly(DateTimeGenerator.ETimeZone timeZone)
        {
            // Act
            var result = DateTimeGenerator.GenerateNow(timeZone);

            // Assert
            var difference = (result - (timeZone == DateTimeGenerator.ETimeZone.UTC ? DateTime.UtcNow : DateTime.Now));
            Assert.True(difference.TotalSeconds <= 5, $"Time Zone: {timeZone}. Result is later than the time zone now by more than 5 seconds");
        }

        [Theory]
        [InlineData(DateTimeGenerator.ETimeZone.UTC)]
        [InlineData(DateTimeGenerator.ETimeZone.Local)]
        public void GenerateBefore_WhenTimeZoneIsGiven_ShouldReturnPastTimeAccordingly(DateTimeGenerator.ETimeZone timeZone)
        {
            // Act
            var result = DateTimeGenerator.GenerateBefore(timeZone);

            // Assert
            var dateTime = timeZone == DateTimeGenerator.ETimeZone.UTC ? DateTime.UtcNow : DateTime.Now;
            var difference = (dateTime - result);
            Assert.True(dateTime.CompareTo(result) > 0, $"Time Zone: {timeZone}. Result is not earlier than the time zone now");
        }

        [Theory]
        [InlineData(DateTimeGenerator.ETimeZone.UTC)]
        [InlineData(DateTimeGenerator.ETimeZone.Local)]
        public void GenerateBefore_WhenTimeZoneIsGivenAndOneDayShiftIsGiven_ShouldReturnPastTimeAccordingly(DateTimeGenerator.ETimeZone timeZone)
        {
            // Act
            var result = DateTimeGenerator.GenerateBefore(timeZone, TimeSpan.FromDays(1));

            // Assert
            var dateTime = timeZone == DateTimeGenerator.ETimeZone.UTC ? DateTime.UtcNow : DateTime.Now;
            var difference = (dateTime - result);
            Assert.True(dateTime.CompareTo(result) > 0, $"Time Zone: {timeZone}. Result is not earlier than the time zone now");
            Assert.True(difference.TotalSeconds > 86399 && difference.TotalSeconds < 86405, $"Time Zone: {timeZone}. There is not a day difference between the two dates.");
        }

        [Theory]
        [InlineData(DateTimeGenerator.ETimeZone.UTC)]
        [InlineData(DateTimeGenerator.ETimeZone.Local)]
        public void GenerateAfter_WhenTimeZoneIsGiven_ShouldReturnPastTimeAccordingly(DateTimeGenerator.ETimeZone timeZone)
        {
            // Act
            var result = DateTimeGenerator.GenerateBefore(timeZone);

            // Assert
            var dateTime = timeZone == DateTimeGenerator.ETimeZone.UTC ? DateTime.UtcNow : DateTime.Now;
            var difference = (dateTime - result);
            Assert.True(dateTime.CompareTo(result) > 0, $"Time Zone: {timeZone}. Result is not later than the time zone now");
        }

        [Theory]
        [InlineData(DateTimeGenerator.ETimeZone.UTC)]
        [InlineData(DateTimeGenerator.ETimeZone.Local)]
        public void GenerateAfter_WhenTimeZoneIsGivenAndOneDayShiftIsGiven_ShouldReturnPastTimeAccordingly(DateTimeGenerator.ETimeZone timeZone)
        {
            // Act
            var result = DateTimeGenerator.GenerateAfter(timeZone, TimeSpan.FromDays(1));

            // Assert
            var dateTime = timeZone == DateTimeGenerator.ETimeZone.UTC ? DateTime.UtcNow : DateTime.Now;
            var difference = (result - dateTime);
            Assert.True(dateTime.CompareTo(result) < 0, $"Time Zone: {timeZone}. Result is not later than the time zone now");
            Assert.True(difference.TotalSeconds > 86399 && difference.TotalSeconds < 86405, $"Time Zone: {timeZone}. There is not a day difference between the two dates.");
        }
    }
}
