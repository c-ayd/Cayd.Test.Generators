namespace Cayd.Test.Generators.Generators
{
    public static class NumberGenerator
    {
        /// <summary>
        /// Generates a random numeric value within a desired range.
        /// </summary>
        /// <typeparam name="T">int, long, float or double</typeparam>
        /// <param name="min">Minimum value (inclusive).</param>
        /// <param name="max">Maximum value (inclusive). It must be greater than the minimum value.</param>
        /// <returns>Returns a numeric value value within the desired range.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static T GenerateBetween<T>(T min, T max)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            if (max.CompareTo(min) < 0)
                throw new ArgumentOutOfRangeException(nameof(max), max, $"Maximum value must be greater than the passed minimum value: {min}");
            
            var type = typeof(T);
            if (type == typeof(int))
            {
                return (T)(object)Random.Shared.Next((int)(object)min, (int)(object)max + 1);
            }
            else if (type == typeof(long))
            {
                return (T)(object)Random.Shared.NextInt64((long)(object)min, (long)(object)max + 1);
            }
            else if (type == typeof(float))
            {
                return (T)(object)(Random.Shared.NextSingle() * ((float)(object)max - (float)(object)min) + (float)(object)min);
            }
            else if (type == typeof(double))
            {
                return (T)(object)(Random.Shared.NextDouble() * ((double)(object)max - (double)(object)min) + (double)(object)min);
            }

            throw new ArgumentException($"{type.Name} is not supported. The type must be either int, long, float or double.");
        }
    }
}
