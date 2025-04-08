namespace Cayd.Test.Generators
{
    public static class DateTimeGenerator
    {
        /// <summary>
        /// Generates the current time based on the time zone.
        /// </summary>
        /// <param name="timeZone">Time zone of the generated date.</param>
        /// <returns>Returns a date time.</returns>
        public static DateTime GenerateNow(ETimeZone timeZone)
        {
            switch (timeZone)
            {
                case ETimeZone.UTC:
                    return DateTime.UtcNow;
                default:
                    return DateTime.Now;
            }
        }

        /// <summary>
        /// Generates a date time earlier than the current time based on the time zone.
        /// </summary>
        /// <param name="timeZone">Time zone of the genertaed date.</param>
        /// <param name="timeShift">How much earlier the generated date is.</param>
        /// <returns>Returns a date time.</returns>
        public static DateTime GenerateBefore(ETimeZone timeZone, TimeSpan timeShift)
        {
            DateTime dateTime;
            switch (timeZone)
            {
                case ETimeZone.UTC:
                    dateTime = DateTime.UtcNow;
                    break;
                default:
                    dateTime = DateTime.Now;
                    break;
            }

            return dateTime.Add(-timeShift);
        }

        /// <summary>
        /// Generates a date time later than the current time based on the time zone.
        /// </summary>
        /// <param name="timeZone">Time zone of the generated date.</param>
        /// <param name="timeShift">How much late the generated date is.</param>
        /// <returns>Returns a date time.</returns>
        public static DateTime GenerateAfter(ETimeZone timeZone, TimeSpan timeShift)
        {
            DateTime dateTime;
            switch (timeZone)
            {
                case ETimeZone.UTC:
                    dateTime = DateTime.UtcNow;
                    break;
                default:
                    dateTime = DateTime.Now;
                    break;
            }

            return dateTime.Add(timeShift);
        }

        public enum ETimeZone
        {
            UTC         =   0,
            Local       =   1
        }
    }
}
