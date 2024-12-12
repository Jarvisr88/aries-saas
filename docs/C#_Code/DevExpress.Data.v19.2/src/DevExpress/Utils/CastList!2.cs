namespace DevExpress.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CastList<T, TKey> : SimpleBridgeList<T, TKey> where T: class where TKey: class, T
    {
        public CastList(IList<TKey> keys) : this(keys, null, func1)
        {
            Func<T, TKey> func1 = <>c<T, TKey>.<>9__0_0;
            if (<>c<T, TKey>.<>9__0_0 == null)
            {
                Func<T, TKey> local1 = <>c<T, TKey>.<>9__0_0;
                func1 = <>c<T, TKey>.<>9__0_0 = x => (TKey) x;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CastList<T, TKey>.<>c <>9;
            public static Func<T, TKey> <>9__0_0;

            static <>c()
            {
                CastList<T, TKey>.<>c.<>9 = new CastList<T, TKey>.<>c();
            }

            internal TKey <.ctor>b__0_0(T x) => 
                (TKey) x;
        }
    }
}

