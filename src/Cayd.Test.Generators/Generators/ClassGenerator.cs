using Cayd.Random.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Cayd.Test.Generators
{
    public static class ClassGenerator
    {
#if NET8_0_OR_GREATER
        /// <summary>
        /// Generates an instance of a specified class and populates it with random values.
        /// This method generates values only for properties, except structs. Members of structs will always have their default values.
        /// However, Guid, DateTime and TimeSpan values are generated.
        /// <para>Example usage 1:</para>
        /// <code>
        /// Generate&lt;MyClass&gt;();
        /// </code>
        /// Example usage 2:
        /// <code>
        /// Generate&lt;MyClass&gt;([
        ///     (x => x.Property1, GeneratorMethod),
        ///     (x => x.Property2, () => GeneratorMethod(param1, param2, ...)),
        ///     (x => x.Property3, () => "Exact value instead of random"),
        ///     (x => x.Property4, () => new OtherClass() { ... }),
        ///     (x => x.Property5, () => { 
        ///         // ... lambda method
        ///     })
        /// ]);
        /// </code>
        /// </summary>
        /// <param name="propertiesAndGenerators">Optional custom property generators</param>
        /// <returns>Returns an instance of the specified class.</returns>
#else
        /// <summary>
        /// Generates an instance of a specified class and populates it with random values.
        /// This method generates values only for properties, except structs. Members of structs will always have their default values.
        /// However, Guid, DateTime and TimeSpan values are generated.
        /// <para>Example usage 1:</para>
        /// <code>
        /// Generate&lt;MyClass&gt;();
        /// </code>
        /// Example usage 2:
        /// <code>
        /// Generate(new (Expression&lt;Func&lt;MyClass, object?&gt;&gt;, Func&lt;object?&gt;?)[] {
        ///     (x => x.Property1, GeneratorMethod),
        ///     (x => x.Property2, () => GeneratorMethod(param1, param2, ...)),
        ///     (x => x.Property3, () => "Exact value instead of random"),
        ///     (x => x.Property4, () => new OtherClass() { ... }),
        ///     (x => x.Property5, () => { 
        ///         // ... lambda method
        ///     })
        /// });
        /// </code>
        /// </summary>
        /// <param name="propertiesAndGenerators">Optional custom property generators</param>
        /// <returns>Returns an instance of the specified class.</returns>
#endif
        public static T Generate<T>(params (Expression<Func<T, object?>> property, Func<object?>? valueGenerator)[] propertiesAndGenerators)
            where T : class
            => Generate(typeof(T), propertiesAndGenerators);

        private static T Generate<T>(Type mainType, params (Expression<Func<T, object?>> property, Func<object?>? valueGenerator)[] propertiesAndGenerators)
            where T : class
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance(type);

            foreach (var propertyInfo in type.GetProperties())
            {
                // Manual assignment from user
                var propertyAndGenerator = propertiesAndGenerators.FirstOrDefault(_ => Utility.Utility.GetPropertyNameFromExpression(_.property) == propertyInfo.Name);
                if (propertyAndGenerator.property != null)
                {
                    if (propertyAndGenerator.valueGenerator == null)
                        continue;

                    propertyInfo.SetValue(instance, propertyAndGenerator.valueGenerator());
                    continue;
                }

                var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                if (propertyType.IsValueType)
                {
                    propertyInfo.SetValue(instance, GeneratePrimitiveType(propertyType));
                }
                else
                {
                    // String is a class type. It needs to be checked extra.
                    if (propertyType == typeof(string))
                    {
                        propertyInfo.SetValue(instance, GeneratePrimitiveType(propertyType));
                        continue;
                    }

                    // Array is a collection type and its creation is different than the other collection types. It needs to be checked before other collection types.
                    if (propertyType.IsArray)
                    {
                        var arrayType = propertyType.GetElementType()!;
                        if (mainType == arrayType)
                        {
                            var array = Array.CreateInstance(arrayType, 0);
                            propertyInfo.SetValue(instance, array);
                        }
                        else
                        {
                            var array = Array.CreateInstance(arrayType, System.Random.Shared.NextInt(minCollectionCount, maxCollectionCount));

                            for (int i = 0; i < array.Length; ++i)
                            {
                                object generatedValue;
                                if (arrayType == typeof(string) || arrayType.IsValueType)
                                {
                                    generatedValue = GeneratePrimitiveType(arrayType);
                                }
                                else
                                {
                                    var generateMethod = typeof(ClassGenerator).GetMethod(nameof(Generate), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(arrayType);
                                    var funcType = typeof(Func<,>).MakeGenericType(arrayType, typeof(object));
                                    var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                                    var methodParameter = Array.CreateInstance(typeof(ValueTuple<,>).MakeGenericType(expressionType, typeof(Func<object>)), 0);

                                    generatedValue = generateMethod.Invoke(null, new object[] 
                                    {
                                        mainType,
                                        methodParameter 
                                    })!;
                                }

                                array.SetValue(generatedValue, i);
                            }

                            propertyInfo.SetValue(instance, array);
                        }

                        continue;
                    }

                    // Collection types
                    var interfaces = propertyType.GetInterfaces();
                    if ((propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>)) ||
                        (interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
                    {
                        var keyType = propertyType.GenericTypeArguments[0];
                        var valueType = propertyType.GenericTypeArguments[1];
                        var dictionary = DictionaryGenerator.Generate(mainType, keyType, valueType, minCollectionCount, maxCollectionCount);
                        propertyInfo.SetValue(instance, dictionary);
                        continue;
                    }
                    if ((propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                        (interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))))
                    {
                        var elementType = propertyType.GenericTypeArguments[0];
                        var enumerable = EnumerableGenerator.Generate(mainType, elementType, minCollectionCount, maxCollectionCount);

                        var propertyGenericTypeDef = propertyType.GetGenericTypeDefinition();
                        if (propertyGenericTypeDef == typeof(Queue<>) ||
                            propertyGenericTypeDef == typeof(Stack<>) ||
                            propertyGenericTypeDef == typeof(HashSet<>))
                        {
                            var propertyGenericType = propertyGenericTypeDef.MakeGenericType(elementType);
                            propertyInfo.SetValue(instance, Activator.CreateInstance(propertyGenericType, new object[] { enumerable }));
                        }
                        else
                        {
                            propertyInfo.SetValue(instance, enumerable);
                        }

                        continue;
                    }

                    // A class type
                    if (propertyType.IsClass)
                    {
                        if (propertyType == mainType)
                            continue;

                        var generateMethod = typeof(ClassGenerator).GetMethod(nameof(Generate), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(propertyType);
                        var funcType = typeof(Func<,>).MakeGenericType(propertyType, typeof(object));
                        var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                        var methodParameter = Array.CreateInstance(typeof(ValueTuple<,>).MakeGenericType(expressionType, typeof(Func<object>)), 0);

                        propertyInfo.SetValue(instance, generateMethod.Invoke(null, new object[] 
                        { 
                            mainType,
                            methodParameter
                        }));
                        continue;
                    }

                    // If none them matches, the property will have its default values
                }
            }

            return (T)instance!;
        }

        internal static object GeneratePrimitiveType(Type type)
        {
            if (type == typeof(bool)) return GenerateBool();
            else if (type == typeof(sbyte)) return GenerateSByte();
            else if (type == typeof(byte)) return GenerateByte();
            else if (type == typeof(short)) return GenerateShort();
            else if (type == typeof(ushort)) return GenerateUShort();
            else if (type == typeof(int)) return GenerateInt();
            else if (type == typeof(uint)) return GenerateUInt();
            else if (type == typeof(long)) return GenerateLong();
            else if (type == typeof(ulong)) return GenerateULong();
            else if (type == typeof(float)) return GenerateFloat();
            else if (type == typeof(double)) return GenerateDouble();
            else if (type == typeof(decimal)) return GenerateDecimal();
            else if (type == typeof(string)) return GenerateString();
            else if (type == typeof(DateTime)) return GenerateDateTime();
            else if (type == typeof(Guid)) return GenerateGuid();
            else if (type == typeof(TimeSpan)) return GenerateTimeSpan();
            else if (type.IsEnum)
            {
                var enumGeneratorMethod = typeof(EnumGenerator).GetMethod(nameof(EnumGenerator.Generate), BindingFlags.Static | BindingFlags.Public)!.MakeGenericMethod(type);
                return enumGeneratorMethod.Invoke(null, new object[] { Array.CreateInstance(type, 0) })!;
            }

            // If none them matches, the property will have its default values
            // For instance, structs are not handled and their members will have their own default values
            return default!;
        }

        private static int minStringLength = 5;
        private static int maxStringLength = 11;
        /// <summary>
        /// Sets the default length range for the strings that are generated via the default <see cref="GenerateString"/> method. Setting the range affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        /// <param name="minLength">Inclusive minimum length of strings. The value must be equal to or greater than 0.</param>
        /// <param name="maxLength">Exclusive maximum length of strings. The value must be equal to or greater than <paramref name="minLength"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void SetDefaultStringLength(int minLength, int maxLength)
        {
            if (minLength < 0)
                throw new ArgumentOutOfRangeException(nameof(minLength), minLength, "The minimum length must be equal to or greater than 0.");
            if (maxLength < minLength)
                throw new ArgumentOutOfRangeException(nameof(maxLength), maxLength, $"The maximum length must be equal to or greater than the given minimum length: {minLength}.");

            minStringLength = minLength;
            maxStringLength = maxLength;
        }

        private static int minCollectionCount = 3;
        private static int maxCollectionCount = 6;
        /// <summary>
        /// Sets the default count range for the collections that are generated. Setting the range affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        /// <param name="minCount">Inclusive minimum count of collections. The value must be equal to or greater than 0.</param>
        /// <param name="maxCount">Exclusive maximum count of collections. The value must be equal to or greater than <paramref name="minCount"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void SetDefaultCollectionCount(int minCount, int maxCount)
        {
            if (minCount < 0)
                throw new ArgumentOutOfRangeException(nameof(minCount), minCount, "The minimum count must be equal to or greater than 0.");
            if (maxCount < minCount)
                throw new ArgumentOutOfRangeException(nameof(maxCount), maxCount, $"The maximum count must be equal to or greater than the given minimum count: {minCount}.");

            minCollectionCount = minCount;
            maxCollectionCount = maxCount;
        }

        /// <summary>
        /// Defines how to generate boolean values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<bool> GenerateBool { private get; set; } = System.Random.Shared.NextBool;
        /// <summary>
        /// Defines how to generate sbyte values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<sbyte> GenerateSByte { private get; set; } = System.Random.Shared.NextSByte;
        /// <summary>
        /// Defines how to generate byte values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<byte> GenerateByte { private get; set; } = System.Random.Shared.NextByte;
        /// <summary>
        /// Defines how to generate short values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<short> GenerateShort { private get; set; } = System.Random.Shared.NextShort;
        /// <summary>
        /// Defines how to generate ushort values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<ushort> GenerateUShort { private get; set; } = System.Random.Shared.NextUShort;
        /// <summary>
        /// Defines how to generate int values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<int> GenerateInt { private get; set; } = System.Random.Shared.NextInt;
        /// <summary>
        /// Defines how to generate uint values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<uint> GenerateUInt { private get; set; } = System.Random.Shared.NextUInt;
        /// <summary>
        /// Defines how to generate long values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<long> GenerateLong { private get; set; } = System.Random.Shared.NextLong;
        /// <summary>
        /// Defines how to generate ulong values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<ulong> GenerateULong { private get; set; } = System.Random.Shared.NextULong;
        /// <summary>
        /// Defines how to generate float values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<float> GenerateFloat { private get; set; } = System.Random.Shared.NextFloat;
        /// <summary>
        /// Defines how to generate double values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<double> GenerateDouble { private get; set; } = System.Random.Shared.NextDoubleLimits;
        /// <summary>
        /// Defines how to generate decimal values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<decimal> GenerateDecimal { private get; set; } = System.Random.Shared.NextDecimal;
        /// <summary>
        /// Defines how to generate string values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<string> GenerateString { private get; set; } = () => StringGenerator.GenerateUsingAsciiChars(System.Random.Shared.NextInt(minStringLength, maxStringLength));
        /// <summary>
        /// Defines how to generate date time values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<DateTime> GenerateDateTime { private get; set; } = () => DateTimeGenerator.GenerateNow(DateTimeGenerator.ETimeZone.UTC);
        /// <summary>
        /// Defines how to generate guid values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<Guid> GenerateGuid { private get; set; } = GuidGenerator.Generate;
        /// <summary>
        /// Defines how to generate time span values for classes. Setting this property affects <see cref="ClassGenerator"/> globally.
        /// </summary>
        public static Func<TimeSpan> GenerateTimeSpan { private get; set; } = () => TimeSpanGenerator.Generate(TimeSpanGenerator.ETimeDirection.Positive);
    }
}
