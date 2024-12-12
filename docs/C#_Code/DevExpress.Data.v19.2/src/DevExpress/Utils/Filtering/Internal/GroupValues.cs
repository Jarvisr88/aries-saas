namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class GroupValues : IGroupValues, IEnumerable<object>, IEnumerable, IEnumerable<IGrouping<int, object>>, ICheckedGroupValues, IExpandableGroups, IDisposable
    {
        private readonly EventHandlerList Events;
        private readonly IGroupValuesCache Cache;
        private readonly IHashTreeChecks groupChecks;
        private readonly IHashTreeChecks groupRadios;
        private readonly HashSet<int> checks;
        private readonly IDictionary<int, ICheckedGroup> delayedChecks;
        private readonly HashSet<int> delayedCheckedGroups;
        private Func<bool> isRadioMode;
        private bool isDisposing;
        private static readonly object query;
        private static readonly object loaded;
        private static readonly object @checked;
        private static readonly object visualUpdateRequired;
        private readonly GroupValuesCheckedEventArgs CheckedArgs;
        private readonly HashSet<int> expandedGroups;

        event EventHandler<GroupValuesCheckedEventArgs> ICheckedGroupValues.Checked;

        event EventHandler<GroupValuesLoadedEventArgs> IGroupValues.Loaded;

        event EventHandler IGroupValues.VisualUpdateRequired;

        static GroupValues();
        public GroupValues(int? depth = new int?(), Action<object[]> groupValuesQuery = null, Func<bool> radioModeQuery = null, Func<string> rootTextQuery = null, Func<bool> rootVisibilityQuery = null);
        private void ClearChecks();
        [IteratorStateMachine(typeof(GroupValues.<DevExpress-Utils-Filtering-Internal-ICheckedGroupValues-GetDelayedFilters>d__28<>))]
        IEnumerable<CriteriaOperator> ICheckedGroupValues.GetDelayedFilters<T>(string[] grouping, Type[] groupingTypes);
        ICheckedValuesEnumerator ICheckedGroupValues.GetEnumerator();
        IEnumerable<int> ICheckedGroupValues.GetIndices();
        void ICheckedGroupValues.Initialize(ICheckedGroup value);
        void ICheckedGroupValues.Prepare();
        void IExpandableGroups.ChangeState(int group);
        int IExpandableGroups.GetIndex(int visibleIndex);
        int IExpandableGroups.GetVisibleIndex(int index);
        IEnumerable<int> IExpandableGroups.GetVisibleIndices(IEnumerable<int> indexes);
        IEnumerator<int> IExpandableGroups.GetVisibleIndices(int index);
        IEnumerator<int> IExpandableGroups.GetVisibleIndicesInverted(int index);
        bool IExpandableGroups.IsExpanded(int group);
        int IGroupValues.GetIndex(int group);
        int? IGroupValues.GetParent(int group);
        object IGroupValues.GetValue(int index, out int group);
        void IGroupValues.Invert();
        bool? IGroupValues.IsChecked(object value, int? group);
        bool IGroupValues.IsLoaded(object[] path);
        bool IGroupValues.Load(object[] level, int? group, int? depth, string[] texts);
        void IGroupValues.QueryLoad(int group);
        void IGroupValues.Reset();
        void IGroupValues.Toggle(object value, int? group);
        void IGroupValues.ToggleAll();
        private object[] GetInvertedValues(IList<object> values, object[] level);
        private GroupValuesLoadedEventArgs GetLoadedArgs(int group, object[] level);
        internal object[] GetPath(int group);
        internal object[] GetValue(int group, IList<object> values, out bool useInversion);
        private int GetValueKey(object value, int key);
        internal bool IsValueChecked(object value, int group);
        private void OnQueryComplete(int group, object[] level);
        private void QueryLoadCore(int group);
        private void RaiseCheckedChanged();
        private void RaiseLoaded(int group, object[] level);
        private void RaiseQuery(int group);
        private void RaiseVisualUpdateRequired();
        private void ResetChecksAndExpandedState(int key, object[] values);
        IEnumerator<IGrouping<int, object>> IEnumerable<IGrouping<int, object>>.GetEnumerator();
        IEnumerator<object> IEnumerable<object>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
        void IDisposable.Dispose();

        private IGroupValuesSource GroupValuesSource { get; }

        int IGroupValues.Count { get; }

        object IGroupValues.this[int index] { get; }

        int IGroupValues.Depth { get; }

        bool IGroupValues.HasValue { get; }

        bool ICheckedGroupValues.HasDelayedFilters { get; }

        private IVisibleItemsSource VisibleItems { get; }

        int IExpandableGroups.VisibleItemsCount { get; }

        private IHashTreeChecks HashTreeChecks { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupValues.<>c <>9;
            public static Action<object[]> <>9__8_0;
            public static Func<bool> <>9__8_1;
            public static Func<bool> <>9__10_0;

            static <>c();
            internal void <.ctor>b__8_0(object[] _);
            internal bool <.ctor>b__8_1();
            internal bool <System.IDisposable.Dispose>b__10_0();
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Utils-Filtering-Internal-ICheckedGroupValues-GetDelayedFilters>d__28<T> : IEnumerable<CriteriaOperator>, IEnumerable, IEnumerator<CriteriaOperator>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private CriteriaOperator <>2__current;
            private int <>l__initialThreadId;
            public GroupValues <>4__this;
            private string[] grouping;
            public string[] <>3__grouping;
            private Type[] groupingTypes;
            public Type[] <>3__groupingTypes;
            private IEnumerator<KeyValuePair<int, ICheckedGroup>> <>7__wrap1;

            [DebuggerHidden]
            public <DevExpress-Utils-Filtering-Internal-ICheckedGroupValues-GetDelayedFilters>d__28(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<CriteriaOperator> IEnumerable<CriteriaOperator>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            CriteriaOperator IEnumerator<CriteriaOperator>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private sealed class GroupChecks : HashTreeChecks
        {
            private readonly IGroupValuesCache cache;

            public GroupChecks(GroupValues values, IGroupValuesCache cache);
            protected sealed override int GetDepth();
            protected sealed override bool TryGetChildren(int group, out object[] children);
            protected sealed override bool TryGetParent(int key, out int parentKey);
            protected sealed override bool TryGetPath(int group, out object[] path);
        }

        private sealed class GroupRadios : HashTreeRadios
        {
            private readonly IGroupValuesCache cache;

            public GroupRadios(GroupValues values, IGroupValuesCache cache);
            protected sealed override bool TryGetChildren(int group, out object[] children);
        }
    }
}

