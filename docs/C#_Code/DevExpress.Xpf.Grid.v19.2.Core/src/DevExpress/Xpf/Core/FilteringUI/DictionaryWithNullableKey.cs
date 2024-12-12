namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class DictionaryWithNullableKey
    {
        public static DictionaryWithNullableKey<TKey, TElement> ToDictionaryWithNullableKey<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            TElement nullKeyValue = default(TElement);
            Func<KeyValuePair<TKey, TElement>, TKey> func1 = <>c__0<TSource, TKey, TElement>.<>9__0_2;
            if (<>c__0<TSource, TKey, TElement>.<>9__0_2 == null)
            {
                Func<KeyValuePair<TKey, TElement>, TKey> local1 = <>c__0<TSource, TKey, TElement>.<>9__0_2;
                func1 = <>c__0<TSource, TKey, TElement>.<>9__0_2 = x => x.Key;
            }
            return new DictionaryWithNullableKey<TKey, TElement>((from x in source select new KeyValuePair<TKey, TElement>(keySelector(x), elementSelector(x))).Where<KeyValuePair<TKey, TElement>>(delegate (KeyValuePair<TKey, TElement> x) {
                if (x.Key == null)
                {
                    nullKeyValue = x.Value;
                }
                return (x.Key != null);
            }).ToDictionary<KeyValuePair<TKey, TElement>, TKey, TElement>(func1, <>c__0<TSource, TKey, TElement>.<>9__0_3 ??= x => x.Value), nullKeyValue);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TSource, TKey, TElement>
        {
            public static readonly DictionaryWithNullableKey.<>c__0<TSource, TKey, TElement> <>9;
            public static Func<KeyValuePair<TKey, TElement>, TKey> <>9__0_2;
            public static Func<KeyValuePair<TKey, TElement>, TElement> <>9__0_3;

            static <>c__0()
            {
                DictionaryWithNullableKey.<>c__0<TSource, TKey, TElement>.<>9 = new DictionaryWithNullableKey.<>c__0<TSource, TKey, TElement>();
            }

            internal TKey <ToDictionaryWithNullableKey>b__0_2(KeyValuePair<TKey, TElement> x) => 
                x.Key;

            internal TElement <ToDictionaryWithNullableKey>b__0_3(KeyValuePair<TKey, TElement> x) => 
                x.Value;
        }
    }
}

