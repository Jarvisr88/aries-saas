namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class MemberReader
    {
        private static IDictionary<string, Func<object, object>> accessors;
        internal static Func<object, object> defaultAccessor;

        static MemberReader();
        private static Expression<Func<object, object>> Accessor(Type sourceType, MemberInfo sourceProperty);
        private static Expression<Func<object, object>> Call(Type sourceType, MethodInfo sourceMethod);
        private static Expression CheckMemberType(MemberExpression accessor);
        private static Expression CheckReturnType(MethodCallExpression call);
        internal static Expression<Func<object, object>> GetAccessor(Type viewModelType, MemberInfo m);
        internal static object Read(object viewModel, string memberName);
        internal static object Read(object viewModel, string memberName, IDictionary<string, object> valuesHash);
        internal static void ResetAccessors(object viewModel);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MemberReader.<>c <>9;
            public static Func<Expression<Func<object, object>>, Func<object, object>> <>9__3_2;

            static <>c();
            internal object <.cctor>b__10_0(object s);
            internal Func<object, object> <Read>b__3_2(Expression<Func<object, object>> e);
        }
    }
}

