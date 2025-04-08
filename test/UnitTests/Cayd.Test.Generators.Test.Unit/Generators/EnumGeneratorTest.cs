using System.Reflection;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class EnumGeneratorTest
    {
        public enum ETestEnum
        {
            Undefined   =   -1,
            None        =   0,
            State1      =   1,
            State2      =   2,
            State3      =   3,
            State4      =   100,
            State5      =   200,
            State6      =   1000
        }

        [Fact]
        public void GetEnumValues_WithoutExclusion_ShouldReturnAllValuesOfEnum()
        {
            // Arrange
            var method = typeof(EnumGenerator).GetMethod("GetEnumValues", BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(ETestEnum));
            var values = Enum.GetValues(typeof(ETestEnum)).OfType<ETestEnum>().ToList();
            values.Sort();

            // Act
            var result = (List<ETestEnum>)method.Invoke(null, new object[] { Array.Empty<ETestEnum>() })!;

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(8, result.Count);
            Assert.True(values.SequenceEqual(result), $"The values or the order of the list is not the same");
        }

        [Fact]
        public void GetEnumValues_WithExclusion_ShouldReturnOnlyDesiredValuesOfEnum()
        {
            // Arrange
            var method = typeof(EnumGenerator).GetMethod("GetEnumValues", BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(ETestEnum));
            var values = new List<ETestEnum>() { ETestEnum.Undefined, ETestEnum.None, ETestEnum.State1, ETestEnum.State5, ETestEnum.State6 };

            // Act
            var result = (List<ETestEnum>)method.Invoke(null, new object[] { new ETestEnum[] { ETestEnum.State2, ETestEnum.State3, ETestEnum.State4 } })!;

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
            Assert.True(values.SequenceEqual(result), $"The values or the order of the list is not the same");
        }

        [Fact]
        public void Generate_ShouldReturnValueWithinRangeWithSameType()
        {
            // Arrange
            var values = Enum.GetValues(typeof(ETestEnum)).OfType<ETestEnum>().ToList();
            values.Sort();

            // Act
            var result = EnumGenerator.Generate<ETestEnum>();

            // Assert
            Assert.IsType<ETestEnum>(result);
            Assert.Contains(result, values);
        }

        [Fact]
        public void GenerateBetween_ShouldReturnValueWithinRangeWithSameType()
        {
            // Arrange
            var values = Enum.GetValues(typeof(ETestEnum)).OfType<ETestEnum>().ToList();
            values.Sort();
            var min = ETestEnum.State2;
            var max = ETestEnum.State5;

            // Act
            var result = EnumGenerator.GenerateBetween(min, max);

            // Assert
            Assert.IsType<ETestEnum>(result);
            Assert.Contains(result, values);
            Assert.True(result >= min, $"Returned Value: {result}, Min Value: {min}");
            Assert.True(result <= max, $"Returned Value: {result}, Max Value: {max}");
        }

        [Fact]
        public void GenerateBetween_WhenRangeIsNotCorrect_ShouldThrowArgumentOutOfRangeException()
        {
            // Act
            var result = Record.Exception(() =>
            {
                EnumGenerator.GenerateBetween(ETestEnum.State6, ETestEnum.State1);
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ArgumentOutOfRangeException>(result);
        }

        [Fact]
        public void GenerateStartingFrom_WhenDirectionIsUp_ShouldReturnValueWithinRangeWithSameType()
        {
            // Arrange
            var values = Enum.GetValues(typeof(ETestEnum)).OfType<ETestEnum>().ToList();
            values.Sort();
            var start = ETestEnum.State4;

            // Act
            var result = EnumGenerator.GenerateStartingFrom(start, EnumGenerator.EDirection.Up);

            // Assert
            Assert.IsType<ETestEnum>(result);
            Assert.Contains(result, values);
            Assert.True(result >= start, $"Returned Value: {result}, Start Value: {start}");
        }

        [Fact]
        public void GenerateStartingFrom_WhenDirectionIsDown_ShouldReturnValueWithinRangeWithSameType()
        {
            // Arrange
            var values = Enum.GetValues(typeof(ETestEnum)).OfType<ETestEnum>().ToList();
            values.Sort();
            var start = ETestEnum.State4;

            // Act
            var result = EnumGenerator.GenerateStartingFrom(start, EnumGenerator.EDirection.Down);

            // Assert
            Assert.IsType<ETestEnum>(result);
            Assert.Contains(result, values);
            Assert.True(result <= start, $"Returned Value: {result}, Start Value: {start}");
        }

        [Fact]
        public void GenerateStartingFrom_WhenDirectionIsDownAndStartingIsLastValue_ShouldReturnValueWithinRangeWithSameType()
        {
            // Arrange
            var values = Enum.GetValues(typeof(ETestEnum)).OfType<ETestEnum>().ToList();
            values.Sort();
            var start = ETestEnum.State6;

            // Act
            var result = EnumGenerator.GenerateStartingFrom(start, EnumGenerator.EDirection.Up);

            // Assert
            Assert.IsType<ETestEnum>(result);
            Assert.Contains(result, values);
            Assert.True(result <= start, $"Returned Value: {result}, Start Value: {start}");
        }
    }
}
