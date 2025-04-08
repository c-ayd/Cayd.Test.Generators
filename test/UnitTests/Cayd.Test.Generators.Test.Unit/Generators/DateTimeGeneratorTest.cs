namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class DateTimeGeneratorTest
    {
        [Fact]
        public void GenerateNow_WhenTimeZoneIsUtc_ShouldReturnUtcNow()
        {
            // Act
            var result = DateTimeGenerator.GenerateNow(DateTimeGenerator.ETimeZone.UTC);

            // Assert
            var difference = (result - DateTime.UtcNow);
            Assert.True(difference.TotalSeconds <= 5, "Result is later than UTC now by more than 5 seconds");
        }

        [Fact]
        public void GenerateNow_WhenTimeZoneIsLocal_ShouldReturnLocalNow()
        {
            // Act
            var result = DateTimeGenerator.GenerateNow(DateTimeGenerator.ETimeZone.Local);

            // Assert
            var difference = (result - DateTime.Now);
            Assert.True(difference.TotalSeconds <= 5, "Result is later than local now by more than 5 seconds");
        }

        [Fact]
        public void GenerateBefore_WhenTimeZoneIsUtcAndOneDayShiftIsGiven_ShouldReturnPastTimeAccordingToUtc()
        {
            // Act
            var result = DateTimeGenerator.GenerateBefore(DateTimeGenerator.ETimeZone.UTC, TimeSpan.FromDays(1));

            // Assert
            var difference = (DateTime.UtcNow - result);
            Assert.True(DateTime.UtcNow.CompareTo(result) > 0, "Result is not earlier than UTC now");
            Assert.True(difference.TotalSeconds > 86399 && difference.TotalSeconds < 86405, "There is not a day difference between the two dates.");
        }

        [Fact]
        public void GenerateBefore_WhenTimeZoneIsLocalAndOneDayShiftIsGiven_ShouldReturnPastTimeAccordingToUtc()
        {
            // Act
            var result = DateTimeGenerator.GenerateBefore(DateTimeGenerator.ETimeZone.Local, TimeSpan.FromDays(1));

            // Assert
            var difference = (DateTime.Now - result);
            Assert.True(DateTime.Now.CompareTo(result) > 0, "Result is not earlier than UTC now");
            Assert.True(difference.TotalSeconds > 86399 && difference.TotalSeconds < 86405, "There is not a day difference between the two dates.");
        }

        [Fact]
        public void GenerateAfter_WhenTimeZoneIsUtcAndOneDayShiftIsGiven_ShouldReturnPastTimeAccordingToUtc()
        {
            // Act
            var result = DateTimeGenerator.GenerateAfter(DateTimeGenerator.ETimeZone.UTC, TimeSpan.FromDays(1));

            // Assert
            var difference = (result - DateTime.UtcNow);
            Assert.True(DateTime.UtcNow.CompareTo(result) < 0, "Result is not later than UTC now");
            Assert.True(difference.TotalSeconds > 86399 && difference.TotalSeconds < 86405, "There is not a day difference between the two dates.");
        }

        [Fact]
        public void GenerateAfter_WhenTimeZoneIsLocalAndOneDayShiftIsGiven_ShouldReturnPastTimeAccordingToUtc()
        {
            // Act
            var result = DateTimeGenerator.GenerateAfter(DateTimeGenerator.ETimeZone.Local, TimeSpan.FromDays(1));

            // Assert
            var difference = (result - DateTime.Now);
            Assert.True(DateTime.Now.CompareTo(result) < 0, "Result is not later than UTC now");
            Assert.True(difference.TotalSeconds > 86399 && difference.TotalSeconds < 86405, "There is not a day difference between the two dates.");
        }
    }
}
