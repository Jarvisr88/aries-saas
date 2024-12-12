namespace DevExpress.Xpf.Bars.Automation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class WeakReferenceDictionary<TValue> where TValue: class
    {
        private List<WeakReferenceDictionary<TKey, TValue>.Record<WeakReference, WeakReference>> internalDict;

        public WeakReferenceDictionary();
        public void Add(TKey key, TValue value);
        public bool ContainsKey(TKey key);
        public bool ContainsValue(TValue value);
        private ReadOnlyCollection<WeakReferenceDictionary<TKey, TValue>.Record<TKey, TValue>> GetUnwrappedList();
        private void Set(TKey key, TValue value, bool add);

        public TValue this[TKey key] { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WeakReferenceDictionary<TKey, TValue>.<>c <>9;
            public static Func<WeakReferenceDictionary<TKey, TValue>.Record<TKey, TValue>, TValue> <>9__3_1;
            public static Func<WeakReference, object> <>9__7_1;
            public static Func<WeakReference, object> <>9__7_2;
            public static Func<WeakReference, bool> <>9__7_3;
            public static Func<bool> <>9__7_4;
            public static Func<WeakReference, bool> <>9__7_5;
            public static Func<bool> <>9__7_6;
            public static Func<WeakReferenceDictionary<TKey, TValue>.Record<WeakReference, WeakReference>, <>f__AnonymousType0<object, object, bool>> <>9__7_0;
            public static Func<<>f__AnonymousType0<object, object, bool>, bool> <>9__7_7;
            public static Func<<>f__AnonymousType0<object, object, bool>, WeakReferenceDictionary<TKey, TValue>.Record<TKey, TValue>> <>9__7_8;

            static <>c();
            internal TValue <get_Item>b__3_1(WeakReferenceDictionary<TKey, TValue>.Record<TKey, TValue> x);
            internal <>f__AnonymousType0<object, object, bool> <GetUnwrappedList>b__7_0(WeakReferenceDictionary<TKey, TValue>.Record<WeakReference, WeakReference> x);
            internal object <GetUnwrappedList>b__7_1(WeakReference k);
            internal object <GetUnwrappedList>b__7_2(WeakReference v);
            internal bool <GetUnwrappedList>b__7_3(WeakReference k);
            internal bool <GetUnwrappedList>b__7_4();
            internal bool <GetUnwrappedList>b__7_5(WeakReference v);
            internal bool <GetUnwrappedList>b__7_6();
            internal bool <GetUnwrappedList>b__7_7(<>f__AnonymousType0<object, object, bool> x);
            internal WeakReferenceDictionary<TKey, TValue>.Record<TKey, TValue> <GetUnwrappedList>b__7_8(<>f__AnonymousType0<object, object, bool> x);
        }

        private class Record<TRecordKey, TRecordValue>
        {
            public Record(TRecordKey key, TRecordValue value);

            public TRecordKey Key { get; set; }

            public TRecordValue Value { get; set; }
        }
    }
}

