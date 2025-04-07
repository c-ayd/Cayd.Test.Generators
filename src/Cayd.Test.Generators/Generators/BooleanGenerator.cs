namespace Cayd.Test.Generators
{
    public static class BooleanGenerator
    {
        /// <summary>
        /// Generates a random boolean with a proability of 0.5.
        /// </summary>
        /// <returns>Returns TRUE or FALSE.</returns>
        public static bool Generate()
        {
            return Random.Shared.Next(0, 2) == 1;
        }

        /// <summary>
        /// Generates a random boolean with a desired probability.
        /// </summary>
        /// <param name="probability">Probability of the return type to be TRUE. It must be between 0.0 and 1.0</param>
        /// <returns>Returns TRUE or FALSE.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool GenerateWithProbability(double probability)
        {
            if (probability < 0.0 || probability > 1.0)
                throw new ArgumentOutOfRangeException(nameof(probability), probability, "Probability must be between 0.0 and 1.0");

            return Random.Shared.NextDouble() >= probability;
        }
    }
}
