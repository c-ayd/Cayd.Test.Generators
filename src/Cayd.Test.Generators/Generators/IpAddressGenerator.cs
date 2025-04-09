using System.Text;

namespace Cayd.Test.Generators
{
    public static class IpAddressGenerator
    {
        /// <summary>
        /// Generates a random IPv4 adress with a random class.
        /// </summary>
        /// <returns>Returns a random IPv4 address.</returns>
        public static string GenerateIpv4()
        {
            var classes = typeof(EClass).GetEnumValues().OfType<EClass>().ToList();
            var @class = classes[Random.Shared.Next(0, classes.Count)];
            return _GenerateIpv4(@class);
        }

        /// <summary>
        /// Generates a random IPv4 address based on a given class.
        /// </summary>
        /// <param name="class">Class of the IPv4 address.</param>
        /// <returns>Returns a random IPv4 address based on the given class.</returns>
        public static string GenerateIpv4(EClass @class)
            => _GenerateIpv4(@class);

        /// <summary>
        /// Generates a random IPv4 address with its mask in CIDR notation.
        /// </summary>
        /// <returns>Returns a random IPv4 address in CIDR notation.</returns>
        public static string GenerateIpv4WithMask()
        {
            var classes = typeof(EClass).GetEnumValues().OfType<EClass>().ToList();
            var @class = classes[Random.Shared.Next(0, classes.Count)];
            var ip = _GenerateIpv4(@class);
            return AddMaskToIpv4(ip, (EMaskType)@class);
        }
        
        /// <summary>
        /// Generates a random IPv4 address with a given mask type in CIDR notation.
        /// </summary>
        /// <param name="class">Class of the IPv4 address.</param>
        /// <param name="maskType">Mask of the IPv4 address. It is used only for <see cref="EClass.D"/> and <see cref="EClass.E"/>. If any other class is chosen, the mask depends on the class.</param>
        /// <returns>Returns a random IPv4 address in CIDR notation.</returns>
        public static string GenerateIpv4WithMask(EClass @class, EMaskType maskType)
        {
            var ip = _GenerateIpv4(@class);

            if (@class == EClass.D || @class == EClass.E)
                return AddMaskToIpv4(ip, maskType);

            return AddMaskToIpv4(ip, (EMaskType)@class);
        }

        /// <summary>
        /// Generates a random private IPv4 address.
        /// </summary>
        /// <returns>Returns a random private IPv4 address.</returns>
        public static string GeneratePrivateIpv4()
        {
            var classes = typeof(EPrivateClass).GetEnumValues().OfType<EPrivateClass>().ToList();
            var @class = classes[Random.Shared.Next(0, classes.Count)];
            return _GeneratePrivateIpv4((EClass)@class);
        }

        /// <summary>
        /// Generates a random private IPv4 address based on a given class.
        /// </summary>
        /// <param name="class">Class of the IPv4 address.</param>
        /// <returns>Returns a random private IPv4 address based on the given class.</returns>
        public static string GeneratePrivateIpv4(EPrivateClass @class)
            => _GeneratePrivateIpv4((EClass)@class);

        /// <summary>
        /// Generates a random private IPv4 address with its mask in CIDR notation.
        /// </summary>
        /// <returns>Returns a random private IPv4 address in CIDR notation.</returns>
        public static string GeneratePrivateIpv4WithMask()
        {
            var classes = typeof(EPrivateClass).GetEnumValues().OfType<EPrivateClass>().ToList();
            var @class = classes[Random.Shared.Next(0, classes.Count)];
            var ip = _GeneratePrivateIpv4((EClass)@class);
            return AddMaskToIpv4(ip, (EMaskType)@class);
        }

        /// <summary>
        /// Generates a random private IPv4 address with a given class in CIDR notation.
        /// </summary>
        /// <param name="class">Class of the IPv4 address.</param>
        /// <returns>Returns a random private IPv4 address in CIDR notation.</returns>
        public static string GeneratePrivateIpv4WithMask(EPrivateClass @class)
        {
            var ip = _GeneratePrivateIpv4((EClass)@class);
            return AddMaskToIpv4(ip, (EMaskType)@class);
        }

        /// <summary>
        /// Generates a random IPv6 address with a random type.
        /// </summary>
        /// <returns>Returns a random IPv6 address.</returns>
        public static string GenerateIpv6()
        {
            var types = typeof(Ipv6Type).GetEnumValues().OfType<Ipv6Type>().ToList();
            var type = types[Random.Shared.Next(0, types.Count)];
            return _GenerateIpv6(type);
        }

        /// <summary>
        /// Generates a random IPv6 address based on a given type.
        /// </summary>
        /// <param name="type">Type of the IPv6 address.</param>
        /// <returns>Returns a random IPv6 address based on the given type.</returns>
        public static string GenerateIpv6WithType(Ipv6Type type)
            => _GenerateIpv6(type);
        
        /// <summary>
        /// Generates a random IPv6 address with a random prefix length.
        /// </summary>
        /// <returns>Returns a random IPv6 address with its prefix length.</returns>
        public static string GenerateIpv6WithPrefixLength()
        {
            var types = typeof(Ipv6Type).GetEnumValues().OfType<Ipv6Type>().ToList();
            var type = types[Random.Shared.Next(0, types.Count)];
            var ip = _GenerateIpv6(type);
            return AddPrefixLengthToIpv6(ip, Random.Shared.Next(1, 129));
        }

        /// <summary>
        /// Generates a random IPv6 address based on a given type and a prefix length.
        /// </summary>
        /// <param name="type">Type of the IPv6 address.</param>
        /// <param name="prefixLength">Prefix length of the IPv6 address. It must be between 1 and 128.</param>
        /// <returns>Returns a random IPv6 address based on the given type with its prefix length.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GenerateIpv6WithPrefixLength(Ipv6Type type, int prefixLength)
        {
            if (prefixLength < 1 || prefixLength > 128)
                throw new ArgumentOutOfRangeException(nameof(prefixLength), prefixLength, "The prefix length must be between 1 and 128");

            var ip = _GenerateIpv6(type);
            return AddPrefixLengthToIpv6(ip, Random.Shared.Next(1, 129));
        }

        private static string _GenerateIpv4(EClass @class)
        {
            int[] octets = new int[4];
            switch (@class)
            {
                case EClass.A:
                    octets[0] = PublicClassAOctet1Numbers[Random.Shared.Next(0, PublicClassAOctet1Numbers.Count)];
                    octets[1] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
                case EClass.B:
                    octets[0] = PublicClassBOctet1Numbers[Random.Shared.Next(0, PublicClassBOctet1Numbers.Count)];
                    octets[1] = octets[0] == 172 ? PublicClassBOctet2Numbers[Random.Shared.Next(0, PublicClassBOctet2Numbers.Count)] 
                        : OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
                case EClass.C:
                    octets[0] = PublicClassCOctet1Numbers[Random.Shared.Next(0, PublicClassCOctet1Numbers.Count)];
                    octets[1] = octets[1] == 192 ? PublicClassCOctet2Numbers[Random.Shared.Next(0, PublicClassCOctet2Numbers.Count)]
                        : OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
                case EClass.D:
                    octets[0] = PublicClassDOctet1Numbers[Random.Shared.Next(0, PublicClassDOctet1Numbers.Count)];
                    octets[1] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
                default:
                    octets[0] = PublicClassEOctet1Numbers[Random.Shared.Next(0, PublicClassEOctet1Numbers.Count)];
                    octets[1] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
            }

            StringBuilder builder = new StringBuilder()
                .Append(octets[0]).Append('.')
                .Append(octets[1]).Append('.')
                .Append(octets[2]).Append('.')
                .Append(octets[3]);

            return builder.ToString();
        }

        private static string _GeneratePrivateIpv4(EClass @class)
        {
            int[] octets = new int[4];
            switch (@class)
            {
                case EClass.A:
                    octets[0] = 10;
                    octets[1] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
                case EClass.B:
                    octets[0] = 172;
                    octets[1] = PrivateClassBOctet2Numbers[Random.Shared.Next(0, PrivateClassBOctet2Numbers.Count)];
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
                default:
                    octets[0] = 192;
                    octets[1] = 168;
                    octets[2] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    octets[3] = OctetNumbers[Random.Shared.Next(0, OctetNumbers.Count)];
                    break;
            }

            StringBuilder builder = new StringBuilder()
                .Append(octets[0]).Append('.')
                .Append(octets[1]).Append('.')
                .Append(octets[2]).Append('.')
                .Append(octets[3]);

            return builder.ToString();
        }

        private static string AddMaskToIpv4(string ip, EMaskType maskType)
        {
            switch (maskType)
            {
                case EMaskType.ClassA:
                    return ip + ClassMasks[0];
                case EMaskType.ClassB:
                    return ip + ClassMasks[1];
                default:
                    return ip + ClassMasks[2];
            }
        }

        private static string _GenerateIpv6(Ipv6Type type)
        {
            string firstHextet;
            switch (type)
            {
                case Ipv6Type.GlobalUnicast:
                    firstHextet = GuaHextet1Numbers[Random.Shared.Next(0, GuaHextet1Numbers.Count)];
                    break;
                case Ipv6Type.LinkLocal:
                    firstHextet = LinkLocalHextet1Numbers[Random.Shared.Next(0, LinkLocalHextet1Numbers.Count)];
                    break;
                default:
                    firstHextet = MulticastHextet1Numbers[Random.Shared.Next(0, MulticastHextet1Numbers.Count)];
                    break;
            }

            StringBuilder builder = new StringBuilder()
                .Append(firstHextet).Append(':');
            for (int i = 1; i < 8; ++i)
            {
                builder.Append(HextetNumbers[Random.Shared.Next(0, HextetNumbers.Count)])
                    .Append(':');
            }
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        private static string AddPrefixLengthToIpv6(string ip, int prefixLength)
            => $"{ip}/{prefixLength}";

        public enum EClass
        {
            A       =   0,
            B       =   1,
            C       =   2,
            D       =   3,
            E       =   4
        }

        public enum EPrivateClass
        {
            A       =   0,
            B       =   1,
            C       =   2
        }

        public enum EMaskType
        {
            ClassA      =   0,
            ClassB      =   1,
            ClassC      =   2
        }

        public enum Ipv6Type
        {
            GlobalUnicast   =   0,
            LinkLocal       =   1,
            Multicast       =   2
        }

        private static string[] ClassMasks = new string[] { "/8", "/16", "/32" };

        private static List<int>? octetNumbers = null;
        private static List<int> OctetNumbers
        {
            get
            {
                if (octetNumbers == null)
                {
                    octetNumbers = new List<int>();
                    for (int i = 0; i < 256; ++i)
                        octetNumbers.Add(i);
                }

                return octetNumbers;
            }
        }

        private static List<int>? publicClassAOctet1Numbers = null;
        private static List<int> PublicClassAOctet1Numbers
        {
            get
            {
                if (publicClassAOctet1Numbers == null)
                {
                    publicClassAOctet1Numbers = new List<int>();
                    for (int i = 0; i < 128; ++i)
                    {
                        if (i == 10)
                            continue;

                        publicClassAOctet1Numbers.Add(i);
                    }
                }

                return publicClassAOctet1Numbers;
            }
        }

        private static List<int>? publicClassBOctet1Numbers = null;
        private static List<int> PublicClassBOctet1Numbers
        {
            get
            {
                if (publicClassBOctet1Numbers == null)
                {
                    publicClassBOctet1Numbers = new List<int>();
                    for (int i = 128; i < 192; ++i)
                        publicClassBOctet1Numbers.Add(i);
                }

                return publicClassBOctet1Numbers;
            }
        }

        private static List<int>? publicClassBOctet2Numbers = null;
        private static List<int> PublicClassBOctet2Numbers
        {
            get
            {
                if (publicClassBOctet2Numbers == null)
                {
                    publicClassBOctet2Numbers = new List<int>();
                    for (int i = 0; i < 256; ++i)
                    {
                        if (i >= 16 && i <= 31)
                            continue;

                        publicClassBOctet2Numbers.Add(i);
                    }
                }

                return publicClassBOctet2Numbers;
            }
        }

        private static List<int>? privateClassBOctet2Numbers = null;
        private static List<int> PrivateClassBOctet2Numbers
        {
            get
            {
                if (privateClassBOctet2Numbers == null)
                {
                    privateClassBOctet2Numbers = new List<int>();
                    for (int i = 16; i < 32; ++i)
                        privateClassBOctet2Numbers.Add(i);
                }

                return privateClassBOctet2Numbers;
            }
        }

        private static List<int>? publicClassCOctet1Numbers = null;
        private static List<int> PublicClassCOctet1Numbers
        {
            get
            {
                if (publicClassCOctet1Numbers == null)
                {
                    publicClassCOctet1Numbers = new List<int>();
                    for (int i = 192; i < 224; ++i)
                        publicClassCOctet1Numbers.Add(i);
                }

                return publicClassCOctet1Numbers;
            }
        }

        private static List<int>? publicClassCOctet2Numbers = null;
        private static List<int> PublicClassCOctet2Numbers
        {
            get
            {
                if (publicClassCOctet2Numbers == null)
                {
                    publicClassCOctet2Numbers = new List<int>();
                    for (int i = 0; i < 256; ++i)
                    {
                        if (i == 168)
                            continue;

                        publicClassCOctet2Numbers.Add(i);
                    }
                }

                return publicClassCOctet2Numbers;
            }
        }

        private static List<int>? publicClassDOctet1Numbers = null;
        private static List<int> PublicClassDOctet1Numbers
        {
            get
            {
                if (publicClassDOctet1Numbers == null)
                {
                    publicClassDOctet1Numbers = new List<int>();
                    for (int i = 224; i < 240; ++i)
                        publicClassDOctet1Numbers.Add(i);
                }

                return publicClassDOctet1Numbers;
            }
        }

        private static List<int>? publicClassEOctet1Numbers = null;
        private static List<int> PublicClassEOctet1Numbers
        {
            get
            {
                if (publicClassEOctet1Numbers == null)
                {
                    publicClassEOctet1Numbers = new List<int>();
                    for (int i = 240; i < 256; ++i)
                        publicClassEOctet1Numbers.Add(i);
                }

                return publicClassEOctet1Numbers;
            }
        }

        private static List<string>? hextetNumbers = null;
        private static List<string> HextetNumbers
        {
            get
            {
                if (hextetNumbers == null)
                {
                    hextetNumbers = new List<string>();
                    for (int i = 0; i < 0xFFFF; ++i)
                        hextetNumbers.Add(i.ToString("x4"));
                }

                return hextetNumbers;
            }
        }

        private static List<string>? guaHextet1Numbers = null;
        private static List<string> GuaHextet1Numbers
        {
            get
            {
                if (guaHextet1Numbers == null)
                {
                    guaHextet1Numbers = new List<string>();
                    for (int i = 2000; i < 0x3FFF; ++i)
                        guaHextet1Numbers.Add(i.ToString("x4"));
                }

                return guaHextet1Numbers;
            }
        }

        private static List<string>? linkLocalHextet1Numbers = null;
        private static List<string> LinkLocalHextet1Numbers
        {
            get
            {
                if (linkLocalHextet1Numbers == null)
                {
                    linkLocalHextet1Numbers = new List<string>();
                    for (int i = 0xFE80; i < 0xFEBF; ++i)
                        linkLocalHextet1Numbers.Add(i.ToString("x4"));
                }

                return linkLocalHextet1Numbers;
            }
        }

        private static List<string>? multicastHextet1Numbers = null;
        private static List<string> MulticastHextet1Numbers
        {
            get
            {
                if (multicastHextet1Numbers == null)
                {
                    multicastHextet1Numbers = new List<string>();
                    for (int i = 0xFF00; i < 0xFFFF; ++i)
                        multicastHextet1Numbers.Add(i.ToString("x4"));
                }

                return multicastHextet1Numbers;
            }
        }
    }
}
