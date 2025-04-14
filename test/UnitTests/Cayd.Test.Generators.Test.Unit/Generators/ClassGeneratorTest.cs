using System.Linq.Expressions;

namespace Cayd.Test.Generators.Test.Unit.Generators
{
    public class ClassGeneratorTest
    {
        [Fact]
        public void Generate_WhenTypeIsGiven_ShouldGenerateClassWithValuesExceptStructs()
        {
            // Act
            var result = ClassGenerator.Generate<TestClass>();

            // Assert
            Assert.NotNull(result);

            Assert.NotNull(result.TestBaseInt);             // Property from base class
            
            Assert.NotNull(result.TestBool);                // Bool

            Assert.NotNull(result.TestSByte);               // Numbers
            Assert.NotNull(result.TestByte);
            Assert.NotNull(result.TestShort);
            Assert.NotNull(result.TestUShort);
            Assert.NotNull(result.TestInt);
            Assert.NotNull(result.TestUInt);
            Assert.NotNull(result.TestLong);
            Assert.NotNull(result.TestULong);
            Assert.NotNull(result.TestFloat);
            Assert.NotNull(result.TestDouble);
            Assert.NotNull(result.TestDecimal);

            Assert.NotNull(result.TestString);              // String
            Assert.True(result.TestString.Length >= 5 && result.TestString.Length <= 10, $"String length is not between 5 and 10. Length: {result.TestString.Length}");

            Assert.NotNull(result.TestDateTime);            // Date time

            Assert.NotNull(result.TestGuid);                // Guid

            Assert.NotNull(result.TestTimeSpan);            // Time span

            Assert.NotNull(result.TestEnum);                // Enum

            Assert.Null(result.TestStruct1);                // Nullable struct

            Assert.Null(result.TestStruct2.TestValue1);     // Struct
            Assert.True(string.IsNullOrEmpty(result.TestStruct2.TestValue2), $"TestStruct1.TestValue2 is not null or empty: {result.TestStruct2.TestValue2}");
            Assert.True(string.IsNullOrEmpty(result.TestStruct2.testValue3), $"TestStruct1.testValue3 is not null or empty: {result.TestStruct2.testValue3}");
            Assert.True(result.TestStruct2.testValue4 == 0, $"TestStruct1.testValue3 is not 0: {result.TestStruct2.testValue4}");

            Assert.NotNull(result.TestMemberClass);         // Class

            Assert.NotNull(result.TestIntArray);            // Arrays
            Assert.True(result.TestIntArray.Length >= 3 && result.TestIntArray.Length <= 5, $"Int array length is not between 3 and 5. Length: {result.TestIntArray.Length}");
            Assert.NotNull(result.TestStringArray);
            Assert.True(result.TestStringArray.Length >= 3 && result.TestStringArray.Length <= 5, $"String array length is not between 3 and 5. Length: {result.TestStringArray.Length}");
            Assert.NotNull(result.TestClassArray);
            Assert.True(result.TestClassArray.Length >= 3 && result.TestClassArray.Length <= 5, $"Class array length is not between 3 and 5. Length: {result.TestClassArray.Length}");

            Assert.NotNull(result.TestIntIntDictionary);    // Dictionaries
            Assert.True(result.TestIntIntDictionary.Count >= 3 && result.TestIntIntDictionary.Count <= 5, $"int-int dictionary count is not between 3 and 5. Count: {result.TestIntIntDictionary.Count}");
            Assert.NotNull(result.TestStringStringDictionary);
            Assert.True(result.TestStringStringDictionary.Count >= 3 && result.TestStringStringDictionary.Count <= 5, $"string-string dictionary count is not between 3 and 5. Count: {result.TestStringStringDictionary.Count}");
            Assert.NotNull(result.TestClassClassDictionary);
            Assert.True(result.TestClassClassDictionary.Count >= 3 && result.TestClassClassDictionary.Count <= 5, $"class-class dictionary count is not between 3 and 5. Count: {result.TestClassClassDictionary.Count}");
            Assert.NotNull(result.TestIntClassIDictionary);
            Assert.True(result.TestIntClassIDictionary.Count >= 3 && result.TestIntClassIDictionary.Count <= 5, $"class-class dictionary count is not between 3 and 5. Count: {result.TestIntClassIDictionary.Count}");

            Assert.NotNull(result.TestIntList);             // Lists
            Assert.True(result.TestIntList.Count >= 3 && result.TestIntList.Count <= 5, $"Int list count is not between 3 and 5. Count: {result.TestIntList.Count}");
            Assert.NotNull(result.TestStringList);
            Assert.True(result.TestStringList.Count >= 3 && result.TestStringList.Count <= 5, $"Int list count is not between 3 and 5. Count: {result.TestStringList.Count}");
            Assert.NotNull(result.TestClassList);
            Assert.True(result.TestClassList.Count >= 3 && result.TestClassList.Count <= 5, $"Int list count is not between 3 and 5. Count: {result.TestClassList.Count}");
            Assert.NotNull(result.TestClassCollection);
            Assert.True(result.TestClassCollection.Count >= 3 && result.TestClassCollection.Count <= 5, $"Int list count is not between 3 and 5. Count: {result.TestClassCollection.Count}");
            Assert.NotNull(result.TestClassEnumerable);
            Assert.True(result.TestClassEnumerable.Count() >= 3 && result.TestClassEnumerable.Count() <= 5, $"Int list count is not between 3 and 5. Count: {result.TestClassEnumerable.Count()}");
            Assert.NotNull(result.TestQueue);
            Assert.True(result.TestQueue.Count() >= 3 && result.TestQueue.Count() <= 5, $"Int list count is not between 3 and 5. Count: {result.TestQueue.Count()}");
            Assert.NotNull(result.TestStack);
            Assert.True(result.TestStack.Count() >= 3 && result.TestStack.Count() <= 5, $"Int list count is not between 3 and 5. Count: {result.TestStack.Count()}");
            Assert.NotNull(result.TestHashSet);
            Assert.True(result.TestHashSet.Count() >= 3 && result.TestHashSet.Count() <= 5, $"Int list count is not between 3 and 5. Count: {result.TestHashSet.Count()}");
        }

        [Fact]
        public void Generate_WhenGeneratorsForPropertiesAreGiven_ShouldUseThoseGenerators()
        {
            // Arrange
            var dateTimeValue = DateTime.UtcNow;
            var guidValue = Guid.NewGuid();
            var timeSpanValue = TimeSpan.FromSeconds(5);

            // Act
            var result = ClassGenerator.Generate(new (Expression<Func<TestClass, object?>>, Func<object?>)[] {
                (x => x.TestBaseInt, () => 1),
                (x => x.TestSByte, () => (sbyte)1),
                (x => x.TestByte, () => (byte)1),
                (x => x.TestShort, () => (short)1),
                (x => x.TestUShort, () => (ushort)1),
                (x => x.TestInt, () => 1),
                (x => x.TestUInt, () => 1u),
                (x => x.TestLong, () => 1L),
                (x => x.TestULong, () => 1uL),
                (x => x.TestFloat, () => 1.0f),
                (x => x.TestDouble, () => 1.0),
                (x => x.TestDecimal, () => 1.0m),
                (x => x.TestString, ReturnOneString),
                (x => x.TestDateTime, () => dateTimeValue),
                (x => x.TestGuid, () => guidValue),
                (x => x.TestTimeSpan, () => timeSpanValue),
                (x => x.TestEnum, () => ETestEnum.State150),
                (x => x.TestStruct2, () => new TestStruct()
                { 
                    TestValue1 = ReturnOneString(), 
                    TestValue2 = "2", 
                    testValue3 = "3", 
                    testValue4 = 1 
                }),
                (x => x.TestMemberClass, () => new TestMemberClass()
                {
                    TestDateTime = dateTimeValue,
                    TestEnum = ETestEnum.State4,
                    TestInt = 1,
                    TestString = ReturnOneString(),
                    TestStruct = new TestStruct()
                    {
                        TestValue1 = "4",
                        TestValue2 = "5",
                        testValue3 = "6",
                        testValue4 = 2
                    }
                }),
            });

            // Assert
            Assert.NotNull(result);

            Assert.Equal(1, result.TestBaseInt);                    // Property from base class

            Assert.Equal((sbyte)1, result.TestSByte!.Value);        // Numbers
            Assert.Equal((byte)1, result.TestByte!.Value);
            Assert.Equal((short)1, result.TestShort!.Value);
            Assert.Equal((ushort)1, result.TestUShort!.Value);
            Assert.Equal(1, result.TestInt!.Value);
            Assert.Equal(1u, result.TestUInt!.Value);
            Assert.Equal(1L, result.TestLong!.Value);
            Assert.Equal(1uL, result.TestULong!.Value);
            Assert.Equal(1.0f, result.TestFloat!.Value);
            Assert.Equal(1.0, result.TestDouble!.Value);
            Assert.Equal(1.0m, result.TestDecimal!.Value);

            Assert.Equal("1", result.TestString);                   // String

            Assert.Equal(dateTimeValue, result.TestDateTime);       // Date time

            Assert.Equal(guidValue, result.TestGuid);               // Guid

            Assert.Equal(timeSpanValue.TotalSeconds, result.TestTimeSpan!.Value.TotalSeconds); // Time span

            Assert.Equal(ETestEnum.State150, result.TestEnum);      // Enum

            Assert.Equal("1", result.TestStruct2.TestValue1);       // Struct
            Assert.Equal("2", result.TestStruct2.TestValue2);
            Assert.Equal("3", result.TestStruct2.testValue3);
            Assert.Equal(1, result.TestStruct2.testValue4);

            Assert.Equal(dateTimeValue, result.TestMemberClass!.TestDateTime); // Class
            Assert.Equal(ETestEnum.State4, result.TestMemberClass.TestEnum);
            Assert.Equal(1, result.TestMemberClass.TestInt);
            Assert.Equal("1", result.TestMemberClass.TestString);
            Assert.Equal("4", result.TestMemberClass.TestStruct.TestValue1);
            Assert.Equal("5", result.TestMemberClass.TestStruct.TestValue2);
            Assert.Equal("6", result.TestMemberClass.TestStruct.testValue3);
            Assert.Equal(2, result.TestMemberClass.TestStruct.testValue4);
        }

        private string ReturnOneString() => "1";

        private class TestClassBase
        {
            public int? TestBaseInt { get; set; }
        }

        private class TestClass : TestClassBase
        {
            public bool? TestBool { get; set; } = null;
            public sbyte? TestSByte { get; set; } = null;
            public byte? TestByte { get; set; } = null;
            public short? TestShort { get; set; } = null;
            public ushort? TestUShort { get; set; } = null;
            public int? TestInt { get; set; } = null;
            public uint? TestUInt { get; set; } = null;
            public long? TestLong { get; set; } = null;
            public ulong? TestULong { get; set; } = null;
            public float? TestFloat { get; set; } = null;
            public double? TestDouble { get; set; } = null;
            public decimal? TestDecimal { get; set; } = null;
            public string? TestString { get; set; } = null;
            public DateTime? TestDateTime { get; set; } = null;
            public Guid? TestGuid { get; set; } = null;
            public TimeSpan? TestTimeSpan { get; set; } = null;
            public ETestEnum? TestEnum { get; set; } = null;
            public TestStruct? TestStruct1 { get; set; } = null;
            public TestStruct TestStruct2 { get; set; }
            public TestMemberClass? TestMemberClass { get; set; } = null;
            public int[]? TestIntArray { get; set; } = null;
            public string[]? TestStringArray { get; set; } = null;
            public TestMemberClass[]? TestClassArray { get; set; } = null;
            public Dictionary<int, int>? TestIntIntDictionary { get; set; } = null;
            public Dictionary<string, string>? TestStringStringDictionary { get; set; } = null;
            public Dictionary<TestMemberClass, TestMemberClass>? TestClassClassDictionary { get; set; } = null;
            public IDictionary<int, TestMemberClass>? TestIntClassIDictionary { get; set; } = null;
            public List<int>? TestIntList { get; set; } = null;
            public List<string>? TestStringList { get; set; } = null;
            public List<TestMemberClass>? TestClassList { get; set; } = null;
            public ICollection<TestMemberClass>? TestClassCollection { get; set; } = null;
            public IEnumerable<TestMemberClass>? TestClassEnumerable { get; set; } = null;
            public Queue<TestMemberClass>? TestQueue { get; set; } = null;
            public Stack<TestMemberClass>? TestStack { get; set; } = null;
            public HashSet<TestMemberClass>? TestHashSet { get; set; } = null;
        }

        private enum ETestEnum
        {
            Unknown         =   -1,
            State0          =   0,
            State1          =   1,
            State2          =   2,
            State3          =   3,
            State4          =   4,
            State5          =   5,
            State100        =   100,
            State150        =   150,
            State200        =   200,
            State1000       =   1000
        }

        private struct TestStruct
        {
            public string? TestValue1 { get; set; }
            public string TestValue2 { get; set; }
            public string testValue3;
            public int testValue4;
        }

        private class TestMemberClass
        {
            public int TestInt { get; set; }
            public string TestString { get; set; }
            public DateTime TestDateTime { get; set; }
            public ETestEnum TestEnum { get; set; }
            public TestStruct TestStruct { get; set; }
        }
    }
}
