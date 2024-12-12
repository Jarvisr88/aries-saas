namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class Accessor
    {
        private static readonly IDictionary<string, Func<object, object>> accessorsCache = new Dictionary<string, Func<object, object>>(StringComparer.Ordinal);

        private static Func<object, object> GetAccessor(Type type, string member, Func<object, object> defaultAccessor = null)
        {
            Func<object, object> func;
            string key = type.FullName + "." + member;
            if (!accessorsCache.TryGetValue(key, out func))
            {
                MemberInfo m = type.GetMember(member).FirstOrDefault<MemberInfo>();
                Func<Expression<Func<object, object>>, Func<object, object>> get = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<Expression<Func<object, object>>, Func<object, object>> local1 = <>c.<>9__2_0;
                    get = <>c.<>9__2_0 = e => e.Compile();
                }
                func = MemberReader.GetAccessor(type, m).Get<Expression<Func<object, object>>, Func<object, object>>(get, defaultAccessor ?? GetDefaultAccessor(type, member));
                accessorsCache.Add(key, func);
            }
            return func;
        }

        private static Func<object, object> GetDefaultAccessor(Type type, string member) => 
            (type == typeof(DataRow)) ? x => ((DataRow) x)[member] : MemberReader.defaultAccessor;

        internal static object GetMemberValue(this object @this, string member, Func<object, object> defaultAccessor = null) => 
            !string.IsNullOrEmpty(member) ? @this.Get<object, object>(x => GetAccessor(x.GetType(), member, defaultAccessor)(x), null) : null;

        internal static void Reset()
        {
            accessorsCache.Clear();
        }

        internal static void Reset(Type type, string member = null)
        {
            string text1 = member;
            if (member == null)
            {
                string local1 = member;
                text1 = string.Empty;
            }
            string keyPrefix = type.FullName + "." + text1;
            string[] strArray = (from k in accessorsCache.Keys
                where k.StartsWith(keyPrefix, StringComparison.Ordinal)
                select k).ToArray<string>();
            for (int i = 0; i < strArray.Length; i++)
            {
                accessorsCache.Remove(strArray[i]);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Accessor.<>c <>9 = new Accessor.<>c();
            public static Func<Expression<Func<object, object>>, Func<object, object>> <>9__2_0;

            internal Func<object, object> <GetAccessor>b__2_0(Expression<Func<object, object>> e) => 
                e.Compile();
        }
    }
}

