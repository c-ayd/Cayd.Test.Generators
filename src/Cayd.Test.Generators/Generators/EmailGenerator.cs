using System.Text;

namespace Cayd.Test.Generators
{
    public static class EmailGenerator
    {
        /// <summary>
        /// Generates a random email whose local part and domain part lengths are between 5 and 11, and whose top-level domain length is 3.
        /// </summary>
        /// <returns>Returns a random email.</returns>
        public static string Generate()
            => GenerateEmail(Random.Shared.Next(5, 11), Random.Shared.Next(5, 11), 3);

        /// <summary>
        /// Generates a random email based on provided lengths. If any parameter is skipped, the skipped ones' lengths are the defaults, which is as same as <see cref="Generate"/>.
        /// </summary>
        /// <param name="localPartLength">Length of the local part of the email.</param>
        /// <param name="domainPartLength">Length of the domain part of the email.</param>
        /// <param name="tldLength">Length of the top-level domain of the email.</param>
        /// <returns>Returns a random email based on the provided lengths.</returns>
        public static string GenerateCustomLength(int? localPartLength, int? domainPartLength, int? tldLength)
            => GenerateEmail(localPartLength ?? Random.Shared.Next(5, 11), domainPartLength ?? Random.Shared.Next(5, 11), tldLength ?? 3);

        /// <summary>
        /// Generates a random email based on a provided domain.
        /// </summary>
        /// <param name="domain">Domain of the email. (E.g. "gmail.com", "outlook.com").</param>
        /// <param name="localPartLength">Length of the local part of the email. This parameter can be skipped if the default length is desired.</param>
        /// <returns>Returns a random email based on the provided domain.</returns>
        public static string GenerateCustomDomain(string domain, int? localPartLength)
            => GenerateEmail(domain, localPartLength ?? Random.Shared.Next(5, 11));

        private static string GenerateEmail(int localPartLength, int domainPartLength, int tldLength)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(AlphanumericCharacters[Random.Shared.Next(0, AlphanumericCharacters.Count)]);
            for (int i = 0; i < localPartLength - 1; ++i)
            {
                builder.Append(LocalPartCharacters[Random.Shared.Next(0, LocalPartCharacters.Count)]);
            }

            builder.Append('@');

            for (int i = 0; i < domainPartLength; ++i)
            {
                builder.Append(DomainCharacters[Random.Shared.Next(0, DomainCharacters.Count)]);
            }

            builder.Append('.');

            for (int i = 0; i < tldLength; ++i)
            {
                builder.Append(DomainCharacters[Random.Shared.Next(0, DomainCharacters.Count)]);
            }

            return builder.ToString();
        }

        private static string GenerateEmail(string domain, int localPartLength)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(AlphanumericCharacters[Random.Shared.Next(0, AlphanumericCharacters.Count)]);
            for (int i = 0; i < localPartLength - 1; ++i)
            {
                builder.Append(LocalPartCharacters[Random.Shared.Next(0, LocalPartCharacters.Count)]);
            }

            builder.Append('@')
                .Append(domain);

            return builder.ToString();
        }

        private static List<char>? alphanumericCharacters = null;
        private static List<char> AlphanumericCharacters
        {
            get
            {
                if (alphanumericCharacters == null)
                {
                    alphanumericCharacters = new List<char>();
                    for (char i = 'A'; i <= 'Z'; ++i)
                        alphanumericCharacters.Add(i);
                    for (char i = 'a'; i <= 'z'; ++i)
                        alphanumericCharacters.Add(i);
                }

                return alphanumericCharacters;
            }
        }

        private static List<char>? localPartCharacters = null;
        private static List<char> LocalPartCharacters
        {
            get
            {
                if (localPartCharacters == null)
                {
                    localPartCharacters = new List<char>()
                    {
                        '!', '#', '$', '%', '&', '*', '+', '/', '=', '?', '^', '_', '{', '|', '}', '~', '-', '`'
                    };

                    for (char i = 'A'; i <= 'Z'; ++i)
                        localPartCharacters.Add(i);
                    for (char i = 'a'; i <= 'z'; ++i)
                        localPartCharacters.Add(i);
                    for (char i = '0'; i <= '9'; ++i)
                        localPartCharacters.Add(i);
                }

                return localPartCharacters;
            }
        }

        private static List<char>? domainCharacters = null;
        private static List<char> DomainCharacters
        {
            get
            {
                if (domainCharacters == null)
                {
                    domainCharacters = new List<char>();
                    for (char i = 'A'; i <= 'Z'; ++i)
                        domainCharacters.Add(i);
                    for (char i = 'a'; i <= 'z'; ++i)
                        domainCharacters.Add(i);
                    for (char i = '0'; i <= '9'; ++i)
                        domainCharacters.Add(i);

                    domainCharacters.Add('-');
                    domainCharacters.Add('.');
                }

                return domainCharacters;
            }
        }
    }
}
