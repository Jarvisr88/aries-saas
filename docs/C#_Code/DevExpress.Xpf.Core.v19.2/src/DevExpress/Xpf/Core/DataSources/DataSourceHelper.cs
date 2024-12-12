namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class DataSourceHelper
    {
        public static Type ExtractEnumerableType(IEnumerable obj)
        {
            if (obj == null)
            {
                return null;
            }
            Func<Type, bool> predicate = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__0_0;
                predicate = <>c.<>9__0_0 = t => t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            }
            IEnumerable<Type> source = obj.GetType().GetInterfaces().Where<Type>(predicate);
            return ((source.Count<Type>() != 0) ? source.ElementAt<Type>(0).GetGenericArguments()[0] : null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataSourceHelper.<>c <>9 = new DataSourceHelper.<>c();
            public static Func<Type, bool> <>9__0_0;

            internal bool <ExtractEnumerableType>b__0_0(Type t) => 
                t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }
    }
}

