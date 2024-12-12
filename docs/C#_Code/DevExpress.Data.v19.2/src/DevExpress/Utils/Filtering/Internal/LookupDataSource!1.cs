namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal sealed class LookupDataSource<T> : IEnumerable<KeyValuePair<object, string>>, IEnumerable
    {
        private readonly IEnumerable dataSource;
        private readonly string valueMember;
        private readonly string displayMember;
        private readonly HashSet<int> nullAndBlanks;
        private readonly HashSet<object> valuesLookup;
        private readonly IDictionary<int, string> displayTextLookup;
        private readonly IDisplayMetricAttributes displayMetricAttributes;
        private readonly string nullName;
        private readonly bool forceFilterByDisplayText;
        private readonly bool useBlanks;
        private readonly Action delayedInitialization;
        private readonly SynchronizationContext context;
        private int maxDisplayIndexCore;
        private static readonly bool allowNull;
        private IEnumerable<KeyValuePair<object, string>> lookupDataSource;

        static LookupDataSource();
        public LookupDataSource(IEnumerable dataSource, IMetricAttributes attributes, HashSet<object> valuesLookup, IDictionary<int, string> displayTextLookup, Action delayedInitialization = null);
        [IteratorStateMachine(typeof(LookupDataSource<>.<CreateLookup>d__34))]
        private IEnumerator<KeyValuePair<object, string>> CreateLookup(IEnumerable dataSource, string nullName, bool useBlanks);
        private IEnumerable<KeyValuePair<object, string>> CreateTextLookup(IEnumerable dataSource, string nullName, bool useBlanks);
        private IEnumerable<KeyValuePair<object, string>> CreateValuesLookup(IEnumerable dataSource, string nullName, bool useBlanks);
        private static string GetDisplayText(IDisplayMetricAttributes displayMetricAttributes, int index, object item, string displayValue);
        private static object GetLookupDataItem(object item);
        private static string GetNullDisplayText(IDisplayMetricAttributes displayMetricAttributes, int index, string nullName);
        private static bool IsBlank(object value);
        public bool IsNullOrBlank(int index);
        private void OnEndLookup(int visibleIndex);
        private bool SkipBlank(bool useBlanks, object value, int nextIndex);
        private bool SkipNull(object value, int nextIndex);
        IEnumerator<KeyValuePair<object, string>> IEnumerable<KeyValuePair<object, string>>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();

        public bool Loaded { get; }

        public int NullAndBlanksCount { get; }

        public int MaxDisplayIndex { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookupDataSource<T>.<>c <>9;
            public static Func<ILookupMetricAttributes, string> <>9__12_0;
            public static Func<ILookupMetricAttributes, string> <>9__12_1;
            public static Func<ICollectionMetricAttributes, string> <>9__12_2;
            public static Func<ILookupMetricAttributes, bool> <>9__12_3;

            static <>c();
            internal string <.ctor>b__12_0(ILookupMetricAttributes x);
            internal string <.ctor>b__12_1(ILookupMetricAttributes x);
            internal string <.ctor>b__12_2(ICollectionMetricAttributes x);
            internal bool <.ctor>b__12_3(ILookupMetricAttributes x);
        }

        [CompilerGenerated]
        private sealed class <CreateLookup>d__34 : IEnumerator<KeyValuePair<object, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<object, string> <>2__current;
            public IEnumerable dataSource;
            public LookupDataSource<T> <>4__this;
            public string nullName;
            public bool useBlanks;
            private IEnumerable<KeyValuePair<object, string>> <iterable>5__1;
            private IEnumerator<KeyValuePair<object, string>> <>7__wrap1;

            [DebuggerHidden]
            public <CreateLookup>d__34(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            KeyValuePair<object, string> IEnumerator<KeyValuePair<object, string>>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

