using Cayd.Test.Generators.Generators;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class MacAddressGeneratorTest
    {
        [Fact]
        public void GenerateUnicast_ShouldGenerateUnicastMacAddress()
        {
            // Act
            var result = MacAddressGenerator.GenerateUnicast();

            // Arrange
            Assert.Equal(17, result.Length);

            var firstPart = result.Split(':')[0];
            var firstByte = Convert.ToByte(firstPart, fromBase: 16);
            Assert.True((firstByte & 0x00) == 0);
        }

        [Fact]
        public void GenerateMulticast_ShouldGenerateMulticastMacAddress()
        {
            // Act
            var result = MacAddressGenerator.GenerateMulticast();

            // Arrange
            Assert.Equal(17, result.Length);

            var firstPart = result.Split(':')[0];
            var firstByte = Convert.ToByte(firstPart, fromBase: 16);
            Assert.True((firstByte & 0x01) == 1);
        }
    }
}
