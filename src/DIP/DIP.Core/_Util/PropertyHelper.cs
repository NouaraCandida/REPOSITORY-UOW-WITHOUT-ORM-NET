using System;
using System.Linq.Expressions;

namespace DIP.Core._Util
{
    public static class PropertyHelper
    {
        public static string ClassAndPropertyName<T>(Expression<Func<T>> property)
        {
            var expression = GetMemberInfo(property);
            string path = string.Concat(expression.Member.DeclaringType.Name,
                ".", expression.Member.Name);
            return path;
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException(nameof(method));

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentNullException(nameof(method));

            return memberExpr;
        }

    }
}
