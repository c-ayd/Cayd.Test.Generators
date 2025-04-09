using System.Reflection;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class IpAddressGeneratorTest
    {
        [Fact]
        public void _GenerateIpv4_ShouldReturnCorrectIpFormat()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.EClass.A })!;

            // Assert
            var octets = result.Split('.');
            Assert.Equal(4, octets.Length);
        }

        [Fact]
        public void _GenerateIpv4_WhenClassIsA_ShouldReturnPublicClassAIp()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.EClass.A })!;

            // Assert
            var octets = result.Split('.');
            Assert.True(int.Parse(octets[0]) >= 0 && int.Parse(octets[0]) <= 127, $"IP is not class A. First octet: {octets[0]}");
            Assert.False(int.Parse(octets[0]) == 10, "IP is private class A");
        }

        [Fact]
        public void _GenerateIpv4_WhenClassIsB_ShouldReturnPublicClassBIp()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.EClass.B })!;

            // Assert
            var octets = result.Split('.');
            Assert.True(int.Parse(octets[0]) >= 128 && int.Parse(octets[0]) <= 191, $"IP is not class B. First octet: {octets[0]}");
            Assert.False(int.Parse(octets[1]) == 172 && int.Parse(octets[1]) >= 16 && int.Parse(octets[1]) <= 31, "IP is private class B");
        }

        [Fact]
        public void _GenerateIpv4_WhenClassIsC_ShouldReturnPublicClassCIp()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.EClass.C })!;

            // Assert
            var octets = result.Split('.');
            Assert.True(int.Parse(octets[0]) >= 192 && int.Parse(octets[0]) <= 223, $"IP is not class C. First octet: {octets[0]}");
            Assert.False(int.Parse(octets[0]) == 192 && int.Parse(octets[1]) == 168, "IP is private class C");
        }

        [Fact]
        public void _GenerateIpv4_WhenClassIsD_ShouldReturnPublicClassDIp()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.EClass.D })!;

            // Assert
            var octets = result.Split('.');
            Assert.True(int.Parse(octets[0]) >= 224 && int.Parse(octets[0]) <= 239, $"IP is not class D. First octet: {octets[0]}");
        }

        [Fact]
        public void _GenerateIpv4_WhenClassIsE_ShouldReturnPublicClassEIp()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.EClass.E })!;

            // Assert
            var octets = result.Split('.');
            Assert.True(int.Parse(octets[0]) >= 240 && int.Parse(octets[0]) <= 255, $"IP is not class E. First octet: {octets[0]}");
        }

        [Theory]
        [InlineData(IpAddressGenerator.EMaskType.ClassA, "8")]
        [InlineData(IpAddressGenerator.EMaskType.ClassB, "16")]
        [InlineData(IpAddressGenerator.EMaskType.ClassC, "32")]
        public void AddMaskToIpv4_WhenClassIsA_ShouldReturnIpAddressWithRelatedMask(IpAddressGenerator.EMaskType maskType, string expectedMask)
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("AddMaskToIpv4", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { "ip", maskType })!;

            // Assert
            var mask = result.Split('/')[1];
            Assert.Equal(expectedMask, mask);
        }

        [Theory]
        [InlineData(IpAddressGenerator.EClass.A, IpAddressGenerator.EMaskType.ClassB, "8")]
        [InlineData(IpAddressGenerator.EClass.B, IpAddressGenerator.EMaskType.ClassA, "16")]
        [InlineData(IpAddressGenerator.EClass.C, IpAddressGenerator.EMaskType.ClassA, "32")]
        [InlineData(IpAddressGenerator.EClass.D, IpAddressGenerator.EMaskType.ClassA, "8")]
        [InlineData(IpAddressGenerator.EClass.E, IpAddressGenerator.EMaskType.ClassC, "32")]
        public void GenerateIpv4WithMask_WhenClassIsAOrBOrC_ShouldReturnIpAddressWithRelatedMask(IpAddressGenerator.EClass @class, IpAddressGenerator.EMaskType maskType, string expectedMask)
        {
            // Act
            var result = IpAddressGenerator.GenerateIpv4WithMask(@class, maskType);

            // Assert
            var mask = result.Split('/')[1];
            Assert.Equal(expectedMask, mask);
        }

        [Fact]
        public void _GenerateIpv6_ShouldReturnCorrectIpFormat()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv6", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { IpAddressGenerator.Ipv6Type.GlobalUnicast })!;

            // Assert
            var hextets = result.Split(':');
            Assert.Equal(8, hextets.Length);
        }

        [Theory]
        [InlineData(IpAddressGenerator.Ipv6Type.GlobalUnicast, 2000, 0x3FFF)]
        [InlineData(IpAddressGenerator.Ipv6Type.LinkLocal, 0xFE80, 0xFEBF)]
        [InlineData(IpAddressGenerator.Ipv6Type.Multicast, 0xFF00, 0xFFFF)]
        public void _GenerateIpv6_WhenTypeIsGiven_ShouldReturnIpAddressWithRelatedType(IpAddressGenerator.Ipv6Type type, int rangeMin, int rangeMax)
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("_GenerateIpv6", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            var result = (string)method!.Invoke(null, new object[] { type })!;

            // Assert
            var firstHextet = int.Parse(result.Split(':')[0], System.Globalization.NumberStyles.HexNumber);
            Assert.True(firstHextet >= rangeMin && firstHextet <= rangeMax, $"IP is not within the range. First hextet: {firstHextet}");
        }

        [Fact]
        public void AddPrefixLengthToIpv6_ShouldReturnIpAddressWithPrefixLength()
        {
            // Arrange
            var method = typeof(IpAddressGenerator).GetMethod("AddPrefixLengthToIpv6", BindingFlags.NonPublic | BindingFlags.Static);
            var prefixLength = 100;

            // Act
            var result = (string)method!.Invoke(null, new object[] { "ip", prefixLength })!;

            // Assert
            var resultPrefixLength = int.Parse(result.Split('/')[1]);
            Assert.Equal(prefixLength, resultPrefixLength);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(130)]
        public void GenerateIpv6WithPrefixLength_WhenPrefixLengthIsNotInRange_ShouldThrowArgumentOutOfRangeException(int prefixLength)
        {
            // Act
            var result = Record.Exception(() =>
            {
                IpAddressGenerator.GenerateIpv6WithPrefixLength(IpAddressGenerator.Ipv6Type.GlobalUnicast, prefixLength);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }
    }
}
