using System.Linq.Expressions;

namespace Cayd.Test.Generators.Utility
{
    internal static partial class Utility
    {
        internal static string? GetPropertyNameFromExpression<T>(Expression<Func<T, object?>>? propertyExpression)
        {
            if (propertyExpression == null)
                return null;

            if (propertyExpression.Body is MemberExpression memberExpression)
                return memberExpression.Member.Name;
            if (propertyExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                return operand.Member.Name;

            throw new ArgumentException("Property expression is wrong", propertyExpression.ToString());
        }
    }
}
