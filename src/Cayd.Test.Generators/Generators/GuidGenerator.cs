namespace Cayd.Test.Generators
{
    public static class GuidGenerator
    {
        /// <summary>
        /// Generates a Guid.
        /// </summary>
        /// <returns>Returns a Guid.</returns>
        public static Guid Generate()
            => Guid.NewGuid();

        /// <summary>
        /// Generates a basic time-based sequential Guid.
        /// </summary>
        /// <returns>Returns a sequential Guid.</returns>
        public static Guid GenerateSequential()
        {
            byte[] dateTimeBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
            byte[] randomBytes = new byte[8];
            Random.Shared.NextBytes(randomBytes);

            byte[] guidBytes = new byte[16];
            Array.Copy(dateTimeBytes, 0, guidBytes, 0, 8);
            Array.Copy(randomBytes, 0, guidBytes, 8, 8);

            return new Guid(guidBytes);
        }
    }
}
