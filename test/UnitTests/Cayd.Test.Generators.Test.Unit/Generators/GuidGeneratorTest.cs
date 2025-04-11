namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class GuidGeneratorTest
    {
        [Fact]
        public void GenerateSequential_ShouldGenerateSequentialGuids()
        {
            // Act
            var result1 = GuidGenerator.GenerateSequential();
            var result2 = GuidGenerator.GenerateSequential();
            var result3 = GuidGenerator.GenerateSequential();

            // Assert
            Assert.True(result1.CompareTo(result2) < 0, "Guid1 and 2 are not sequential");
            Assert.True(result2.CompareTo(result3) < 0, "Guid2 and 3 are not sequential");
        }
    }
}
