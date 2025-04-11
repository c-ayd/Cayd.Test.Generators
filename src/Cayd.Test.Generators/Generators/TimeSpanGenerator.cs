namespace Cayd.Test.Generators
{
    public static class TimeSpanGenerator
    {
        /// <summary>
        /// Generates a random time span between -7 and 7 days, including hours, minutes, seconds and milliseconds.
        /// </summary>
        /// <returns>Returns a random time span.</returns>
        public static TimeSpan Generate()
        {
            var timeSpan = new TimeSpan(
                Random.Shared.Next(0, 7),
                Random.Shared.Next(0, 24),
                Random.Shared.Next(0, 60),
                Random.Shared.Next(0, 60),
                Random.Shared.Next(1, 1000));

            return BooleanGenerator.Generate() ? timeSpan : -timeSpan;
        }

        /// <summary>
        /// Generates a random time span between -7 and 7 days, including hours, minutes, seconds and milliseconds, based on a time direction.
        /// </summary>
        /// <param name="timeDirection">Direction of the time span. <see cref="ETimeDirection.Positive"/> indicates future, whereas <see cref="ETimeDirection.Negative"/> indicates past.</param>
        /// <returns>Returns a random time span.</returns>
        public static TimeSpan Generate(ETimeDirection timeDirection)
        {
            var timeSpan = new TimeSpan(
                Random.Shared.Next(0, 7),
                Random.Shared.Next(0, 24),
                Random.Shared.Next(0, 60),
                Random.Shared.Next(0, 60),
                Random.Shared.Next(1, 1000));

            return timeDirection == ETimeDirection.Positive ? timeSpan : -timeSpan;
        }

        /// <summary>
        /// Generates a random time span based on a time limit including days, hours, minutes, seconds and milliseconds.
        /// </summary>
        /// <param name="timeLimit">Limit of the time span. If a limit is not specified for a specific time unit, the time unit is always 0.</param>
        /// <returns>Returns a random time span.</returns>
        public static TimeSpan GenerateWithLimit(TimeLimit timeLimit)
        {
            var timeSpan = new TimeSpan(
                Random.Shared.Next(0, timeLimit.DayLimit + 1),
                Random.Shared.Next(0, timeLimit.HourLimit + 1),
                Random.Shared.Next(0, timeLimit.MinuteLimit + 1),
                Random.Shared.Next(0, timeLimit.SecondLimit + 1),
                Random.Shared.Next(0, timeLimit.MillisecondLimit + 1));

            return BooleanGenerator.Generate() ? timeSpan : -timeSpan;
        }

        /// <summary>
        /// Generates a random time span based on a time direction and a time limit including days, hours, minutes, seconds and milliseconds
        /// </summary>
        /// <param name="timeDirection">Direction of the time span. <see cref="ETimeDirection.Positive"/> indicates future, whereas <see cref="ETimeDirection.Negative"/> indicates past.</param>
        /// <param name="timeLimit">Limit of the time span. If a limit is not specified for a specific time unit, the time unit is always 0.</param>
        /// <returns>Returns a random time span.</returns>
        public static TimeSpan GenerateWithLimit(ETimeDirection timeDirection, TimeLimit timeLimit)
        {
            var timeSpan = new TimeSpan(
               Random.Shared.Next(0, timeLimit.DayLimit + 1),
               Random.Shared.Next(0, timeLimit.HourLimit + 1),
               Random.Shared.Next(0, timeLimit.MinuteLimit + 1),
               Random.Shared.Next(0, timeLimit.SecondLimit + 1),
               Random.Shared.Next(0, timeLimit.MillisecondLimit + 1));

            return timeDirection == ETimeDirection.Positive ? timeSpan : -timeSpan;
        }

        public enum ETimeDirection
        {
            Positive        =   0,
            Negative        =   1
        }

        public struct TimeLimit
        {
            public int DayLimit { get; set; }
            public int HourLimit { get; set; }
            public int MinuteLimit { get; set; }
            public int SecondLimit { get; set; }
            public int MillisecondLimit { get; set; }
        }
    }
}
