namespace Cayd.Test.Generators
{
    public static class EnumGenerator
    {
        /// <summary>
        /// Generates a random enum value from the desired type.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="exclude">Enum values to exclude.</param>
        /// <returns>Returns a random enum value from the desired type.</returns>
        public static T Generate<T>(params T[] exclude)
            where T : Enum
        {
            var values = GetEnumValues(exclude);
            return values[Random.Shared.Next(values.Count())];
        }

        /// <summary>
        /// Generates a random enum value within a desired range.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="min">Minimum numerical value of the enum (inclusive).</param>
        /// <param name="max">Maximum numerical value of the enum (inclusive). It must be greater than the minimum value.</param>
        /// <param name="exclude">Enum values to exclude.</param>
        /// <returns>Returns a random enum value within the desired range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T GenerateBetween<T>(T min, T max, params T[] exclude)
            where T : Enum
        {
            var values = GetEnumValues(exclude);

            if (!values.Contains(min))
                throw new ArgumentOutOfRangeException(nameof(min), min.ToString(), $"{min.ToString()} does not exist in enum {typeof(T).Name}");
            if (!values.Contains(max))
                throw new ArgumentOutOfRangeException(nameof(max), max.ToString(), $"{max.ToString()} does not exist in enum {typeof(T).Name}");
            if (max.CompareTo(min) < 0)
                throw new ArgumentOutOfRangeException(nameof(max), max, $"Maximum value must be greater than the passed minimum value: {min}");

            int minIndex = values.FindIndex(0, values.Count, e => e.Equals(min));
            int maxIndex = values.FindIndex(0, values.Count, e => e.Equals(max));
            return values[Random.Shared.Next(minIndex, maxIndex)];
        }

        /// <summary>
        /// Generates a random enum value within a desired range starting from a specific value.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="start">Numerical starting value of the enum (inclusive).</param>
        /// <param name="direction">Direction of the range (numerical).</param>
        /// <param name="exclude">Enum values to exclude.</param>
        /// <returns>Returns a random enum value within the desired range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static T GenerateStartingFrom<T>(T start, EDirection direction, params T[] exclude)
            where T : Enum
        {
            var values = GetEnumValues(exclude);

            if (!values.Contains(start))
                throw new ArgumentOutOfRangeException(nameof(start), start.ToString(), $"{start.ToString()} does not exist in enum {typeof(T).Name}");

            int startIndex = values.FindIndex(0, values.Count, e => e.Equals(start));
            switch (direction)
            {
                case EDirection.Up:
                    return values[Random.Shared.Next(startIndex, values.Count)];
                default:
                    if (startIndex == values.Count - 1)
                        return values[Random.Shared.Next(0, values.Count)];

                    return values[Random.Shared.Next(0, startIndex + 1)];
            }
        }

        private static List<T> GetEnumValues<T>(params T[] exclude)
            where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            if (exclude.Length > 0)
            {
                values.RemoveAll(e => exclude.Contains(e));
            }

            values.Sort();
            return values;
        }

        public enum EDirection
        {
            Up,
            Down
        }
    }
}
