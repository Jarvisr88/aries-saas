namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class GroupValuesCache : IGroupValuesCache, IEnumerable<object>, IEnumerable, IDisposable, IGroupValuesSource, IVisibleItemsSource
    {
        private readonly IGrouping<int, object> Root;
        private readonly Func<int, bool> isExpanded;
        private readonly Func<string> rootText;
        internal readonly GroupIndices indices;
        private int? depthCore;
        private readonly IDictionary<int, object[]> values;
        private readonly IDictionary<int, string[]> displayTexts;
        private readonly IDictionary<int, GroupValue> groups;

        internal GroupValuesCache(int? depth, Func<int, bool> isExpanded, Func<string> rootText, Func<bool> rootVisibility, Action queryUpdate);
        private GroupValue CreateGroupValue(int level, int key, int parent, int index);
        int IGroupValuesCache.GetIndex(int group);
        int? IGroupValuesCache.GetParent(int group);
        object[] IGroupValuesCache.GetPath(int group);
        bool IGroupValuesCache.IsLoaded(int key);
        bool IGroupValuesCache.Reload(int key, object[] level, int? depth, string[] texts);
        bool IGroupValuesCache.TryGetValue(int key, out object[] level);
        string IGroupValuesSource.GetText(int key, int index);
        object IGroupValuesSource.GetValue(int index, out int group);
        object IGroupValuesSource.GetValue(int key, int index);
        [IteratorStateMachine(typeof(GroupValuesCache.<DevExpress-Utils-Filtering-Internal-IGroupValuesSource-Groups>d__32))]
        IEnumerator<IGrouping<int, object>> IGroupValuesSource.Groups();
        ICheckedValuesEnumerator IGroupValuesSource.Values(Func<int, bool> isChecked);
        [IteratorStateMachine(typeof(GroupValuesCache.<DevExpress-Utils-Filtering-Internal-IGroupValuesSource-Values>d__33))]
        IEnumerator<object> IGroupValuesSource.Values(int key, int level);
        void IVisibleItemsSource.Expand(int group);
        [DebuggerStepThrough, DebuggerHidden]
        int IVisibleItemsSource.GetIndex(int index, bool returnSourceIndex);
        [IteratorStateMachine(typeof(GroupValuesCache.<DevExpress-Utils-Filtering-Internal-IVisibleItemsSource-Indices>d__48)), DebuggerStepThrough, DebuggerHidden]
        IEnumerable<int> IVisibleItemsSource.Indices(HashSet<int> groups);
        [DebuggerStepThrough, DebuggerHidden]
        IEnumerable<int> IVisibleItemsSource.Indices(IEnumerable<int> indexes);
        [DebuggerStepThrough, DebuggerHidden]
        IEnumerator<int> IVisibleItemsSource.Indices(int index, bool forwardDirection);
        private IEnumerator<object> GetEnumeratorCore();
        private int GetGroupKey(int key, int parentKey, int childIndex);
        private object GetGroupValueByIndex(int group, int index);
        private int GetGroupValueKey(int key, int index, int childIndex);
        private IEnumerator<int> GetGroupValuesEnumerator(HashSet<int> groups);
        private void InitializeLevel(int key, int level, object[] children);
        private void ResetCaches();
        private bool ResetLevels(int key);
        private bool ResetLevels(int key, object[] level);
        IEnumerator<object> IEnumerable<object>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
        void IDisposable.Dispose();

        public int Depth { get; }

        object[] IGroupValuesCache.this[int key] { get; }

        int IGroupValuesSource.Count { get; }

        object IGroupValuesSource.this[int index] { get; }

        int IVisibleItemsSource.Count { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupValuesCache.<>c <>9;
            public static Func<int, bool> <>9__7_0;
            public static Func<string> <>9__7_1;

            static <>c();
            internal bool <.ctor>b__7_0(int group);
            internal string <.ctor>b__7_1();
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Utils-Filtering-Internal-IGroupValuesSource-Groups>d__32 : IEnumerator<IGrouping<int, object>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IGrouping<int, object> <>2__current;
            public GroupValuesCache <>4__this;
            private IEnumerator<object> <rootEnumerator>5__1;

            [DebuggerHidden]
            public <DevExpress-Utils-Filtering-Internal-IGroupValuesSource-Groups>d__32(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            IGrouping<int, object> IEnumerator<IGrouping<int, object>>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }


        [CompilerGenerated]
        private sealed class <DevExpress-Utils-Filtering-Internal-IVisibleItemsSource-Indices>d__48 : IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private int <>2__current;
            private int <>l__initialThreadId;
            private HashSet<int> groups;
            public HashSet<int> <>3__groups;
            public GroupValuesCache <>4__this;
            private IEnumerator<int> <enumerator>5__1;

            [DebuggerHidden]
            public <DevExpress-Utils-Filtering-Internal-IVisibleItemsSource-Indices>d__48(int <>1__state);
            private void <>m__Finally1();
            [DebuggerStepThrough, DebuggerHidden]
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<int> IEnumerable<int>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            int IEnumerator<int>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private sealed class CheckedValuesEnumerator : GroupValuesCache.ValuesEnumeratorBase, ICheckedValuesEnumerator, IEnumerator<object>, IDisposable, IEnumerator
        {
            private readonly Func<int, bool> isChecked;

            public CheckedValuesEnumerator(IGroupValue root, Func<int, bool> isChecked);
            protected sealed override bool NextValue(object value);

            public int Group { get; private set; }
        }

        private sealed class ValuesEnumerator : GroupValuesCache.ValuesEnumeratorBase
        {
            public ValuesEnumerator(IGroupValue root);
            protected sealed override bool NextValue(object value);
        }

        private abstract class ValuesEnumeratorBase : IEnumerator<object>, IDisposable, IEnumerator
        {
            protected readonly Stack<IEnumerator<object>> enumeratorsStack;
            protected IEnumerator<object> currentEnumerator;

            protected ValuesEnumeratorBase(IGroupValue root);
            protected int GetCurrentGroup();
            protected abstract bool NextValue(object value);
            private void ResetCore();
            bool IEnumerator.MoveNext();
            void IEnumerator.Reset();
            void IDisposable.Dispose();

            object IEnumerator.Current { get; }

            object IEnumerator<object>.Current { get; }
        }
    }
}

