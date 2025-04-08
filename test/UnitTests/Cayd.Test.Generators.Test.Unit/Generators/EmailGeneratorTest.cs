using System.Reflection;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class EmailGeneratorTest
    {
        [Fact]
        public void GenerateEmail_WhenLengthsAreGiven_ShouldReturnEmailWithExactLengths()
        {
            // Arrange
            var method = typeof(EmailGenerator).GetMethod("GenerateEmail", BindingFlags.NonPublic | BindingFlags.Static, new[] { typeof(int), typeof(int), typeof(int) });
            int usernameLength = 7, domainPartLength = 10, tldLength = 2;

            // Act
            var result = (string)method!.Invoke(null, new object[] { usernameLength, domainPartLength, tldLength })!;

            // Assert
            var length = usernameLength + domainPartLength + tldLength + 2;
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GenerateEmail_WhenDomainIsGiven_ShouldReturnEmailWithExactDomain()
        {
            // Arrange
            var method = typeof(EmailGenerator).GetMethod("GenerateEmail", BindingFlags.NonPublic | BindingFlags.Static, new[] { typeof(string), typeof(int) });
            int usernameLength = 7;
            string domain = "gmail.com";

            // Act
            var result = (string)method!.Invoke(null, new object[] { domain, usernameLength })!;

            // Assert
            var length = usernameLength + domain.Length + 1;
            var resultDomain = result.Split('@')[1];
            Assert.Equal(length, result.Length);
            Assert.Equal(domain, resultDomain);
        }
    }
}
