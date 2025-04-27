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

            /** Nullable Properties */
            Assert.NotNull(result.NullableTestBaseInt);             // Property from base class
            
            Assert.NotNull(result.NullableTestBool);                // Bool

            Assert.NotNull(result.NullableTestSByte);               // Numbers
            Assert.NotNull(result.NullableTestByte);
            Assert.NotNull(result.NullableTestShort);
            Assert.NotNull(result.NullableTestUShort);
            Assert.NotNull(result.NullableTestInt);
            Assert.NotNull(result.NullableTestUInt);
            Assert.NotNull(result.NullableTestLong);
            Assert.NotNull(result.NullableTestULong);
            Assert.NotNull(result.NullableTestFloat);
            Assert.NotNull(result.NullableTestDouble);
            Assert.NotNull(result.NullableTestDecimal);

            Assert.NotNull(result.NullableTestString);              // String
            Assert.True(result.NullableTestString.Length >= 5 && result.NullableTestString.Length <= 10, $"String length is not between 5 and 10. Length: {result.NullableTestString.Length}");

            Assert.NotNull(result.NullableTestDateTime);            // Date time

            Assert.NotNull(result.NullableTestGuid);                // Guid

            Assert.NotNull(result.NullableTestTimeSpan);            // Time span

            Assert.NotNull(result.NullableTestEnum);                // Enum

            Assert.Null(result.NullableTestStruct);                 // Struct

            Assert.NotNull(result.NullableTestMemberClass);         // Class

            Assert.NotNull(result.NullableTestIntArray);            // Arrays
            Assert.True(result.NullableTestIntArray.Length >= 3 && result.NullableTestIntArray.Length <= 5, $"Int array length is not between 3 and 5. Length: {result.NullableTestIntArray.Length}");
            Assert.NotNull(result.NullableTestStringArray);
            Assert.True(result.NullableTestStringArray.Length >= 3 && result.NullableTestStringArray.Length <= 5, $"String array length is not between 3 and 5. Length: {result.NullableTestStringArray.Length}");
            Assert.NotNull(result.NullableTestClassArray);
            Assert.True(result.NullableTestClassArray.Length >= 3 && result.NullableTestClassArray.Length <= 5, $"Class array length is not between 3 and 5. Length: {result.NullableTestClassArray.Length}");

            Assert.NotNull(result.NullableTestIntIntDictionary);    // Dictionaries
            Assert.True(result.NullableTestIntIntDictionary.Count >= 3 && result.NullableTestIntIntDictionary.Count <= 5, $"int-int dictionary count is not between 3 and 5. Count: {result.NullableTestIntIntDictionary.Count}");
            Assert.NotNull(result.NullableTestStringStringDictionary);
            Assert.True(result.NullableTestStringStringDictionary.Count >= 3 && result.NullableTestStringStringDictionary.Count <= 5, $"string-string dictionary count is not between 3 and 5. Count: {result.NullableTestStringStringDictionary.Count}");
            Assert.NotNull(result.NullableTestClassClassDictionary);
            Assert.True(result.NullableTestClassClassDictionary.Count >= 3 && result.NullableTestClassClassDictionary.Count <= 5, $"class-class dictionary count is not between 3 and 5. Count: {result.NullableTestClassClassDictionary.Count}");
            Assert.NotNull(result.NullableTestIntClassIDictionary);
            Assert.True(result.NullableTestIntClassIDictionary.Count >= 3 && result.NullableTestIntClassIDictionary.Count <= 5, $"class-class dictionary count is not between 3 and 5. Count: {result.NullableTestIntClassIDictionary.Count}");

            Assert.NotNull(result.NullableTestIntList);             // Enumerables
            Assert.True(result.NullableTestIntList.Count >= 3 && result.NullableTestIntList.Count <= 5, $"Int list count is not between 3 and 5. Count: {result.NullableTestIntList.Count}");
            Assert.NotNull(result.NullableTestStringList);
            Assert.True(result.NullableTestStringList.Count >= 3 && result.NullableTestStringList.Count <= 5, $"String list count is not between 3 and 5. Count: {result.NullableTestStringList.Count}");
            Assert.NotNull(result.NullableTestClassList);
            Assert.True(result.NullableTestClassList.Count >= 3 && result.NullableTestClassList.Count <= 5, $"Class list count is not between 3 and 5. Count: {result.NullableTestClassList.Count}");
            Assert.NotNull(result.NullableTestClassEnumerable);
            Assert.True(result.NullableTestClassEnumerable.Count() >= 3 && result.NullableTestClassEnumerable.Count() <= 5, $"Enumerable count is not between 3 and 5. Count: {result.NullableTestClassEnumerable.Count()}");
            Assert.NotNull(result.NullableTestClassCollection);
            Assert.True(result.NullableTestClassCollection.Count >= 3 && result.NullableTestClassCollection.Count <= 5, $"Collection count is not between 3 and 5. Count: {result.NullableTestClassCollection.Count}");
            Assert.NotNull(result.NullableTestClassIList);
            Assert.True(result.NullableTestClassIList.Count >= 3 && result.NullableTestClassIList.Count <= 5, $"IList count is not between 3 and 5. Count: {result.NullableTestClassIList.Count}");
            Assert.NotNull(result.NullableTestQueue);
            Assert.True(result.NullableTestQueue.Count() >= 3 && result.NullableTestQueue.Count() <= 5, $"Queue count is not between 3 and 5. Count: {result.NullableTestQueue.Count()}");
            Assert.NotNull(result.NullableTestStack);
            Assert.True(result.NullableTestStack.Count() >= 3 && result.NullableTestStack.Count() <= 5, $"Stack count is not between 3 and 5. Count: {result.NullableTestStack.Count()}");
            Assert.NotNull(result.NullableTestHashSet);
            Assert.True(result.NullableTestHashSet.Count() >= 3 && result.NullableTestHashSet.Count() <= 5, $"Hash set count is not between 3 and 5. Count: {result.NullableTestHashSet.Count()}");

            /** Non-nullable Properties */
            Assert.False(string.IsNullOrEmpty(result.TestString), "TestString is null or empty."); // String
            Assert.True(result.TestString.Length >= 5 && result.TestString.Length <= 10, $"String length is not between 5 and 10. Length: {result.TestString.Length}");

            Assert.Null(result.TestStruct.TestValue1);                // Struct
            Assert.True(string.IsNullOrEmpty(result.TestStruct.TestValue2), $"TestStruct1.TestValue2 is not null or empty: {result.TestStruct.TestValue2}");
            Assert.True(string.IsNullOrEmpty(result.TestStruct.testValue3), $"TestStruct1.testValue3 is not null or empty: {result.TestStruct.testValue3}");
            Assert.True(result.TestStruct.testValue4 == 0, $"TestStruct1.testValue3 is not 0: {result.TestStruct.testValue4}");

            Assert.NotNull(result.TestMemberClass);                   // Class

            Assert.NotNull(result.TestIntArray);                      // Arrays
            Assert.True(result.TestIntArray.Length >= 3 && result.TestIntArray.Length <= 5, $"Int array length is not between 3 and 5. Length: {result.TestIntArray.Length}");
            Assert.NotNull(result.TestStringArray);
            Assert.True(result.TestStringArray.Length >= 3 && result.TestStringArray.Length <= 5, $"String array length is not between 3 and 5. Length: {result.TestStringArray.Length}");
            Assert.NotNull(result.TestClassArray);
            Assert.True(result.TestClassArray.Length >= 3 && result.TestClassArray.Length <= 5, $"Class array length is not between 3 and 5. Length: {result.TestClassArray.Length}");

            Assert.NotNull(result.TestIntIntDictionary);              // Dictionaries
            Assert.True(result.TestIntIntDictionary.Count >= 3 && result.TestIntIntDictionary.Count <= 5, $"int-int dictionary count is not between 3 and 5. Count: {result.TestIntIntDictionary.Count}");
            Assert.NotNull(result.TestStringStringDictionary);
            Assert.True(result.TestStringStringDictionary.Count >= 3 && result.TestStringStringDictionary.Count <= 5, $"string-string dictionary count is not between 3 and 5. Count: {result.TestStringStringDictionary.Count}");
            Assert.NotNull(result.TestClassClassDictionary);
            Assert.True(result.TestClassClassDictionary.Count >= 3 && result.TestClassClassDictionary.Count <= 5, $"class-class dictionary count is not between 3 and 5. Count: {result.TestClassClassDictionary.Count}");
            Assert.NotNull(result.TestIntClassIDictionary);
            Assert.True(result.TestIntClassIDictionary.Count >= 3 && result.TestIntClassIDictionary.Count <= 5, $"class-class dictionary count is not between 3 and 5. Count: {result.TestIntClassIDictionary.Count}");

            Assert.NotNull(result.TestIntList);                       // Enumerables
            Assert.True(result.TestIntList.Count >= 3 && result.TestIntList.Count <= 5, $"Int list count is not between 3 and 5. Count: {result.TestIntList.Count}");
            Assert.NotNull(result.TestStringList);
            Assert.True(result.TestStringList.Count >= 3 && result.TestStringList.Count <= 5, $"String list count is not between 3 and 5. Count: {result.TestStringList.Count}");
            Assert.NotNull(result.TestClassList);
            Assert.True(result.TestClassList.Count >= 3 && result.TestClassList.Count <= 5, $"Class list count is not between 3 and 5. Count: {result.TestClassList.Count}");
            Assert.NotNull(result.TestClassEnumerable);
            Assert.True(result.TestClassEnumerable.Count() >= 3 && result.TestClassEnumerable.Count() <= 5, $"Enumerable count is not between 3 and 5. Count: {result.TestClassEnumerable.Count()}");
            Assert.NotNull(result.TestClassCollection);
            Assert.True(result.TestClassCollection.Count >= 3 && result.TestClassCollection.Count <= 5, $"Collection count is not between 3 and 5. Count: {result.TestClassCollection.Count}");
            Assert.NotNull(result.TestClassIList);
            Assert.True(result.TestClassIList.Count >= 3 && result.TestClassIList.Count <= 5, $"IList count is not between 3 and 5. Count: {result.TestClassIList.Count}");
            Assert.NotNull(result.TestQueue);
            Assert.True(result.TestQueue.Count() >= 3 && result.TestQueue.Count() <= 5, $"Queue count is not between 3 and 5. Count: {result.TestQueue.Count()}");
            Assert.NotNull(result.TestStack);
            Assert.True(result.TestStack.Count() >= 3 && result.TestStack.Count() <= 5, $"Stack count is not between 3 and 5. Count: {result.TestStack.Count()}");
            Assert.NotNull(result.TestHashSet);
            Assert.True(result.TestHashSet.Count() >= 3 && result.TestHashSet.Count() <= 5, $"Hash set count is not between 3 and 5. Count: {result.TestHashSet.Count()}");
        }

        [Fact]
        public void Generate_WhenGeneratorsForPropertiesAreGiven_ShouldUseThoseGenerators()
        {
            // Arrange
            var dateTimeValue = DateTime.UtcNow;
            var guidValue = Guid.NewGuid();
            var timeSpanValue = TimeSpan.FromSeconds(5);

            // Act
            var result = ClassGenerator.Generate(new (Expression<Func<TestClass, object?>>, Func<object?>?)[] {
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
                (x => x.TestStruct, () => new TestStruct()
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

            Assert.Equal((sbyte)1, result.TestSByte);               // Numbers
            Assert.Equal((byte)1, result.TestByte);
            Assert.Equal((short)1, result.TestShort);
            Assert.Equal((ushort)1, result.TestUShort);
            Assert.Equal(1, result.TestInt);
            Assert.Equal(1u, result.TestUInt);
            Assert.Equal(1L, result.TestLong);
            Assert.Equal(1uL, result.TestULong);
            Assert.Equal(1.0f, result.TestFloat);
            Assert.Equal(1.0, result.TestDouble);
            Assert.Equal(1.0m, result.TestDecimal);

            Assert.Equal("1", result.TestString);                   // String

            Assert.Equal(dateTimeValue, result.TestDateTime);       // Date time

            Assert.Equal(guidValue, result.TestGuid);               // Guid

            Assert.Equal(timeSpanValue.TotalSeconds, result.TestTimeSpan.TotalSeconds); // Time span

            Assert.Equal(ETestEnum.State150, result.TestEnum);      // Enum

            Assert.Equal("1", result.TestStruct.TestValue1);        // Struct
            Assert.Equal("2", result.TestStruct.TestValue2);
            Assert.Equal("3", result.TestStruct.testValue3);
            Assert.Equal(1, result.TestStruct.testValue4);

            Assert.Equal(dateTimeValue, result.TestMemberClass!.TestDateTime); // Class
            Assert.Equal(ETestEnum.State4, result.TestMemberClass.TestEnum);
            Assert.Equal(1, result.TestMemberClass.TestInt);
            Assert.Equal("1", result.TestMemberClass.TestString);
            Assert.Equal("4", result.TestMemberClass.TestStruct.TestValue1);
            Assert.Equal("5", result.TestMemberClass.TestStruct.TestValue2);
            Assert.Equal("6", result.TestMemberClass.TestStruct.testValue3);
            Assert.Equal(2, result.TestMemberClass.TestStruct.testValue4);
        }

        [Fact]
        public void Generate_WhenThereIsNestedReferencing_ShouldGenerateClassWithOneDepth()
        {
            // Act
            var result = ClassGenerator.Generate<RecursiveClass1>();

            // Assert
            Assert.NotNull(result);

            // Recursive class 1
            Assert.NotNull(result.NullableTestInt);

            Assert.NotNull(result.RecursiveClass2);
            Assert.NotNull(result.NullableRecursiveClass2);

            Assert.NotNull(result.RecursiveClass2Array);
            Assert.True(result.RecursiveClass2Array.Length >= 3 && result.RecursiveClass2Array.Length <= 5, $"Array length is not between 3 and 5. Length: {result.RecursiveClass2Array.Length}");

            Assert.NotNull(result.RecursiveClass2DictionaryKey);
            Assert.True(result.RecursiveClass2DictionaryKey.Count >= 3 && result.RecursiveClass2DictionaryKey.Count <= 5, $"Dictionary key count is not between 3 and 5. Length: {result.RecursiveClass2DictionaryKey.Count}");
            Assert.NotNull(result.RecursiveClass2DictionaryValue);
            Assert.True(result.RecursiveClass2DictionaryValue.Count >= 3 && result.RecursiveClass2DictionaryValue.Count <= 5, $"Dictionary value count is not between 3 and 5. Length: {result.RecursiveClass2DictionaryValue.Count}");
            Assert.NotNull(result.RecursiveClass2Dictionary);
            Assert.True(result.RecursiveClass2Dictionary.Count >= 3 && result.RecursiveClass2Dictionary.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2Dictionary.Count}");

            Assert.NotNull(result.RecursiveClass2List);
            Assert.True(result.RecursiveClass2List.Count >= 3 && result.RecursiveClass2List.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2List.Count}");
            Assert.NotNull(result.RecursiveClass2Enumerable);
            Assert.True(result.RecursiveClass2Enumerable.Count() >= 3 && result.RecursiveClass2Enumerable.Count() <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2Enumerable.Count()}");
            Assert.NotNull(result.RecursiveClass2Collection);
            Assert.True(result.RecursiveClass2Collection.Count >= 3 && result.RecursiveClass2Collection.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2Collection.Count}");
            Assert.NotNull(result.RecursiveClass2IList);
            Assert.True(result.RecursiveClass2IList.Count >= 3 && result.RecursiveClass2IList.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2IList.Count}");
            Assert.NotNull(result.RecursiveClass2Queue);
            Assert.True(result.RecursiveClass2Queue.Count >= 3 && result.RecursiveClass2Queue.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2Queue.Count}");
            Assert.NotNull(result.RecursiveClass2Stack);
            Assert.True(result.RecursiveClass2Stack.Count >= 3 && result.RecursiveClass2Stack.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2Stack.Count}");
            Assert.NotNull(result.RecursiveClass2HashSet);
            Assert.True(result.RecursiveClass2HashSet.Count >= 3 && result.RecursiveClass2HashSet.Count <= 5, $"Dictionary count is not between 3 and 5. Length: {result.RecursiveClass2HashSet.Count}");

            // Reference class 2
            Assert.NotNull(result.RecursiveClass2.NullableTestInt);

            Assert.Null(result.RecursiveClass2.RecursiveClass1);
            Assert.Null(result.RecursiveClass2.NullableRecursiveClass1);

            Assert.NotNull(result.RecursiveClass2.RecursiveClass1Array);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1Array);
            
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1DictionaryKey);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1DictionaryKey);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1DictionaryValue);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1DictionaryValue);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1Dictionary);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1Dictionary);

            Assert.NotNull(result.RecursiveClass2.RecursiveClass1List);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1List);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1Enumerable);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1Enumerable);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1Collection);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1Collection);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1IList);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1IList);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1Queue);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1Queue);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1Stack);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1Stack);
            Assert.NotNull(result.RecursiveClass2.RecursiveClass1HashSet);
            Assert.Empty(result.RecursiveClass2.RecursiveClass1HashSet);
        }

        [Fact]
        public void Generate_WhenClassIsAbstract_ShouldThrowException()
        {
            // Act
            var result = Record.Exception(() =>
            {
                ClassGenerator.Generate<TestAbstractClass>();
            });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MissingMethodException>(result);
        }

        private string ReturnOneString() => "1";

        private class TestClassBase
        {
            public int? NullableTestBaseInt { get; set; }
            public int TestBaseInt { get; set; }
        }

        private class TestClass : TestClassBase
        {
            public bool? NullableTestBool { get; set; } = null;
            public sbyte? NullableTestSByte { get; set; } = null;
            public byte? NullableTestByte { get; set; } = null;
            public short? NullableTestShort { get; set; } = null;
            public ushort? NullableTestUShort { get; set; } = null;
            public int? NullableTestInt { get; set; } = null;
            public uint? NullableTestUInt { get; set; } = null;
            public long? NullableTestLong { get; set; } = null;
            public ulong? NullableTestULong { get; set; } = null;
            public float? NullableTestFloat { get; set; } = null;
            public double? NullableTestDouble { get; set; } = null;
            public decimal? NullableTestDecimal { get; set; } = null;
            public string? NullableTestString { get; set; } = null;
            public DateTime? NullableTestDateTime { get; set; } = null;
            public Guid? NullableTestGuid { get; set; } = null;
            public TimeSpan? NullableTestTimeSpan { get; set; } = null;
            public ETestEnum? NullableTestEnum { get; set; } = null;
            public TestStruct? NullableTestStruct { get; set; } = null;
            public TestMemberClass? NullableTestMemberClass { get; set; } = null;
            public int[]? NullableTestIntArray { get; set; } = null;
            public string[]? NullableTestStringArray { get; set; } = null;
            public TestMemberClass[]? NullableTestClassArray { get; set; } = null;
            public Dictionary<int, int>? NullableTestIntIntDictionary { get; set; } = null;
            public Dictionary<string, string>? NullableTestStringStringDictionary { get; set; } = null;
            public Dictionary<TestMemberClass, TestMemberClass>? NullableTestClassClassDictionary { get; set; } = null;
            public IDictionary<int, TestMemberClass>? NullableTestIntClassIDictionary { get; set; } = null;
            public List<int>? NullableTestIntList { get; set; } = null;
            public List<string>? NullableTestStringList { get; set; } = null;
            public List<TestMemberClass>? NullableTestClassList { get; set; } = null;
            public IEnumerable<TestMemberClass>? NullableTestClassEnumerable { get; set; } = null;
            public ICollection<TestMemberClass>? NullableTestClassCollection { get; set; } = null;
            public IList<TestMemberClass>? NullableTestClassIList { get; set; } = null;
            public Queue<TestMemberClass>? NullableTestQueue { get; set; } = null;
            public Stack<TestMemberClass>? NullableTestStack { get; set; } = null;
            public HashSet<TestMemberClass>? NullableTestHashSet { get; set; } = null;

            public bool TestBool { get; set; }
            public sbyte TestSByte { get; set; }
            public byte TestByte { get; set; }
            public short TestShort { get; set; }
            public ushort TestUShort { get; set; }
            public int TestInt { get; set; }
            public uint TestUInt { get; set; }
            public long TestLong { get; set; }
            public ulong TestULong { get; set; }
            public float TestFloat { get; set; }
            public double TestDouble { get; set; }
            public decimal TestDecimal { get; set; }
            public string TestString { get; set; }
            public DateTime TestDateTime { get; set; }
            public Guid TestGuid { get; set; }
            public TimeSpan TestTimeSpan { get; set; }
            public ETestEnum TestEnum { get; set; }
            public TestStruct TestStruct { get; set; }
            public TestMemberClass TestMemberClass { get; set; }
            public int[] TestIntArray { get; set; }
            public string[] TestStringArray { get; set; }
            public TestMemberClass[] TestClassArray { get; set; }
            public Dictionary<int, int> TestIntIntDictionary { get; set; }
            public Dictionary<string, string> TestStringStringDictionary { get; set; }
            public Dictionary<TestMemberClass, TestMemberClass> TestClassClassDictionary { get; set; }
            public IDictionary<int, TestMemberClass> TestIntClassIDictionary { get; set; }
            public List<int> TestIntList { get; set; }
            public List<string> TestStringList { get; set; }
            public List<TestMemberClass> TestClassList { get; set; }
            public IEnumerable<TestMemberClass> TestClassEnumerable { get; set; }
            public ICollection<TestMemberClass> TestClassCollection { get; set; }
            public IList<TestMemberClass> TestClassIList { get; set; }
            public Queue<TestMemberClass> TestQueue { get; set; }
            public Stack<TestMemberClass> TestStack { get; set; }
            public HashSet<TestMemberClass> TestHashSet { get; set; }
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

        private class RecursiveClass1
        {
            public int? NullableTestInt { get; set; }
            public RecursiveClass2 RecursiveClass2 { get; set; }
            public RecursiveClass2? NullableRecursiveClass2 { get; set; }
            public RecursiveClass2[] RecursiveClass2Array { get; set; }
            public Dictionary<RecursiveClass2, int> RecursiveClass2DictionaryKey { get; set; }
            public Dictionary<int, RecursiveClass2> RecursiveClass2DictionaryValue { get; set; }
            public Dictionary<RecursiveClass2, RecursiveClass2> RecursiveClass2Dictionary { get; set; }
            public IDictionary<int, RecursiveClass2> RecursiveClass2IDictionary { get; set; }
            public List<RecursiveClass2> RecursiveClass2List { get; set; }
            public IEnumerable<RecursiveClass2> RecursiveClass2Enumerable { get; set; }
            public ICollection<RecursiveClass2> RecursiveClass2Collection { get; set; }
            public IList<RecursiveClass2> RecursiveClass2IList { get; set; }
            public Queue<RecursiveClass2> RecursiveClass2Queue { get; set; }
            public Stack<RecursiveClass2> RecursiveClass2Stack { get; set; }
            public HashSet<RecursiveClass2> RecursiveClass2HashSet { get; set; }
        }

        private class RecursiveClass2
        {
            public int? NullableTestInt { get; set; }
            public RecursiveClass1 RecursiveClass1 { get; set; }
            public RecursiveClass1? NullableRecursiveClass1 { get; set; }
            public RecursiveClass1[] RecursiveClass1Array { get; set; }
            public Dictionary<RecursiveClass1, int> RecursiveClass1DictionaryKey { get; set; }
            public Dictionary<int, RecursiveClass1> RecursiveClass1DictionaryValue { get; set; }
            public Dictionary<RecursiveClass1, RecursiveClass1> RecursiveClass1Dictionary { get; set; }
            public IDictionary<int, RecursiveClass1> RecursiveClass1IDictionary { get; set; }
            public List<RecursiveClass1> RecursiveClass1List { get; set; }
            public IEnumerable<RecursiveClass1> RecursiveClass1Enumerable { get; set; }
            public ICollection<RecursiveClass1> RecursiveClass1Collection { get; set; }
            public IList<RecursiveClass1> RecursiveClass1IList { get; set; }
            public Queue<RecursiveClass1> RecursiveClass1Queue { get; set; }
            public Stack<RecursiveClass1> RecursiveClass1Stack { get; set; }
            public HashSet<RecursiveClass1> RecursiveClass1HashSet { get; set; }
        }

        private abstract class TestAbstractClass
        {
            public int? NullableTestInt { get; set; }
        }
    }
}
