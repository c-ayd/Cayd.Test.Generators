using Cayd.Random.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Cayd.Test.Generators
{
    public static class DictionaryGenerator
    {
        /// <summary>
        /// Generates an <see cref="IDictionary{TKey, TValue}"/> with an element count between 3 and 5.
        /// </summary>
        /// <typeparam name="TKey">Key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary.</typeparam>
        /// <returns>Returns an <see cref="IDictionary{TKey, TValue}"/>.</returns>
        public static IDictionary<TKey, TValue> Generate<TKey, TValue>()
            => GenerateDictionary<TKey, TValue>(null, 3, 6);

        /// <summary>
        /// Generates an <see cref="IDictionary{TKey, TValue}"/> with an element count between 0 and a specified max count.
        /// </summary>
        /// <typeparam name="TKey">Key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary.</typeparam>
        /// <param name="maxCount">Exclusive upper bound of the element count. It must be equal to or greater than 0.</param>
        /// <returns>Returns an <see cref="IDictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IDictionary<TKey, TValue> Generate<TKey, TValue>(int maxCount)
            => GenerateDictionary<TKey, TValue>(null, 0, maxCount);

        /// <summary>
        /// Generates an <see cref="IDictionary{TKey, TValue}"/> with an element count between specified min and max counts.
        /// </summary>
        /// <typeparam name="TKey">Key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">Value type of the dictionary.</typeparam>
        /// <param name="minCount">Inclusive lower bound of the element count. It must be equal to or greater than 0.</param>
        /// <param name="maxCount">Exclusive upper bound of the element count. It must be equal to or greater than <paramref name="minCount"/></param>
        /// <returns>Returns an <see cref="IDictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IDictionary<TKey, TValue> Generate<TKey, TValue>(int minCount, int maxCount)
            => GenerateDictionary<TKey, TValue>(null, minCount, maxCount);

        internal static object Generate(Type? skipType, Type keyType, Type valueType, int minCount, int maxCount)
        {
            var generateMethod = typeof(DictionaryGenerator).GetMethod(nameof(GenerateDictionary), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(keyType, valueType);
            return generateMethod.Invoke(null, new object?[] { skipType, minCount, maxCount })!;
        }

        private static IDictionary<TKey, TValue> GenerateDictionary<TKey, TValue>(Type? skipType, int minCount, int maxCount)
        {
            if (minCount < 0)
                throw new ArgumentOutOfRangeException(nameof(minCount), minCount, "The minimum count must be equal to or greater than 0.");
            if (maxCount < minCount)
                throw new ArgumentOutOfRangeException(nameof(maxCount), maxCount, $"The maximum count must equal to or greater than the given minimum count: {minCount}");

            var keyType = typeof(TKey);
            var valueType = typeof(TValue);
            var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            var dictionary = Activator.CreateInstance(dictionaryType);

            if (skipType != null && keyType == skipType)
                return (IDictionary<TKey, TValue>)dictionary!;

            int count = System.Random.Shared.NextInt(minCount, maxCount);
            var addMethod = dictionaryType.GetMethod("Add")!;
            for (int i = 0; i < count; ++i)
            {
                object generatedKey;
                if (keyType == typeof(string) || keyType.IsValueType)
                {
                    generatedKey = ClassGenerator.GeneratePrimitiveType(keyType);
                }
                else
                {
                    var generateMethod = typeof(ClassGenerator).GetMethod(nameof(ClassGenerator.Generate), BindingFlags.Static | BindingFlags.Public)!.MakeGenericMethod(keyType);
                    var funcType = typeof(Func<,>).MakeGenericType(keyType, typeof(object));
                    var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                    var methodParameter = Array.CreateInstance(typeof(ValueTuple<,>).MakeGenericType(expressionType, typeof(Func<object>)), 0);

                    generatedKey = generateMethod.Invoke(null, new object[] { methodParameter })!;
                }

                object? generatedValue;
                if (valueType == typeof(string) || valueType.IsValueType)
                {
                    generatedValue = ClassGenerator.GeneratePrimitiveType(valueType);
                }
                else
                {
                    if (skipType != null && valueType == skipType)
                    {
                        generatedValue = null;
                    }
                    else
                    {
                        var generateMethod = typeof(ClassGenerator).GetMethod(nameof(ClassGenerator.Generate), BindingFlags.Static | BindingFlags.Public)!.MakeGenericMethod(valueType);
                        var funcType = typeof(Func<,>).MakeGenericType(valueType, typeof(object));
                        var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                        var methodParameter = Array.CreateInstance(typeof(ValueTuple<,>).MakeGenericType(expressionType, typeof(Func<object>)), 0);

                        generatedValue = generateMethod.Invoke(null, new object[] { methodParameter })!;
                    }
                }

                addMethod.Invoke(dictionary, new object?[] { generatedKey, generatedValue });
            }

            return (IDictionary<TKey, TValue>)dictionary!;
        }
    }
}
