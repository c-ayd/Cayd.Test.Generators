using Cayd.Random.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Cayd.Test.Generators
{
    public static class EnumerableGenerator
    {
        /// <summary>
        /// Generates an <see cref="IEnumerable{T}"/> with an element count between 3 and 5.
        /// </summary>
        /// <typeparam name="T">Element type of the enumerable.</typeparam>
        /// <returns>Returns a <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> Generate<T>()
            => GenerateEnumerable<T>(null, 3, 6);

        /// <summary>
        /// Generates an <see cref="IEnumerable{T}"/> with an element count between 0 and a specified max count.
        /// </summary>
        /// <typeparam name="T">Element type of the enumerable.</typeparam>
        /// <param name="maxCount">Exclusive upper bound of the element count. It must be equal to or greater than 0.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<T> Generate<T>(int maxCount)
            => GenerateEnumerable<T>(null, 0, maxCount);

        /// <summary>
        /// Generates an <see cref="IEnumerable{T}"/> with an element count between specified min and max counts.
        /// </summary>
        /// <typeparam name="T">Element type of the enumerable.</typeparam>
        /// <param name="minCount">Inclusive lower bound of the element count. It must be equal to or greater than 0.</param>
        /// <param name="maxCount">Exclusive upper bound of the element count. It must be equal to or greater than <paramref name="minCount"/></param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<T> Generate<T>(int minCount, int maxCount)
            => GenerateEnumerable<T>(null, minCount, maxCount);

        internal static object Generate(Type? skipType, Type elementType, int minCount, int maxCount)
        {
            var generateMethod = typeof(EnumerableGenerator).GetMethod(nameof(GenerateEnumerable), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(elementType);
            return generateMethod.Invoke(null, new object?[] { skipType, minCount, maxCount })!;
        }

        private static IEnumerable<T> GenerateEnumerable<T>(Type? skipType, int minCount, int maxCount)
        {
            if (minCount < 0)
                throw new ArgumentOutOfRangeException(nameof(minCount), minCount, "The minimum count must be equal to or greater than 0.");
            if (maxCount < minCount)
                throw new ArgumentOutOfRangeException(nameof(maxCount), maxCount, $"The maximum count must equal to or greater than the given minimum count: {minCount}");

            var elementType = typeof(T);
            var listType = typeof(List<>).MakeGenericType(elementType);
            var list = Activator.CreateInstance(listType);

            if ((skipType != null && elementType == skipType) || elementType.IsAbstract)
                return (IEnumerable<T>)list!;

            int count = System.Random.Shared.NextInt(minCount, maxCount);
            var addMethod = listType.GetMethod("Add")!;
            for (int i = 0; i < count; ++i)
            {
                object generatedElement;
                if (elementType == typeof(string) || elementType.IsValueType)
                {
                    generatedElement = ClassGenerator.GeneratePrimitiveType(elementType);
                }
                else
                {
                    var generateMethod = typeof(ClassGenerator).GetMethod(nameof(ClassGenerator.Generate), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(elementType);
                    var funcType = typeof(Func<,>).MakeGenericType(elementType, typeof(object));
                    var expressionType = typeof(Expression<>).MakeGenericType(funcType);
                    var methodParameter = Array.CreateInstance(typeof(ValueTuple<,>).MakeGenericType(expressionType, typeof(Func<object>)), 0);

                    generatedElement = generateMethod.Invoke(null, new object[] 
                    {
                        skipType ?? elementType,
                        methodParameter 
                    })!;
                }

                addMethod.Invoke(list, new object[] { generatedElement });
            }

            return (IEnumerable<T>)list!;
        }
    }
}
