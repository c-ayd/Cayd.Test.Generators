using System.Security.Cryptography;

namespace Cayd.Test.Generators.Generators
{
    public static class MacAddressGenerator
    {
        /// <summary>
        /// Read only MAC address whose value is 'FF:FF:FF:FF:FF:FF'
        /// </summary>
        public static readonly string Broadcast = "FF:FF:FF:FF:FF:FF";

        /// <summary>
        /// Generates a unicast MAC address.
        /// </summary>
        /// <returns>Returns a unicast MAC address.</returns>
        public static string GenerateUnicast()
            => GenerateMacAddress(0x00);

        /// <summary>
        /// Generates a multicast MAC address.
        /// </summary>
        /// <returns>Returns a multicast MAC address.</returns>
        public static string GenerateMulticast()
            => GenerateMacAddress(0x01);

        private static string GenerateMacAddress(byte type)
        {
            var bytes = new byte[6];
            RandomNumberGenerator.Fill(bytes);

            bytes[0] = (byte)(type | (bytes[0] & 0xFE));

            return BitConverter.ToString(bytes).Replace('-', ':');
        }
    }
}
