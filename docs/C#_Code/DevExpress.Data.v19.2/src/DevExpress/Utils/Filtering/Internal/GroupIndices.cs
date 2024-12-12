namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class GroupIndices : HashTreeIndices
    {
        private readonly Action queryVisualUpdate;

        public GroupIndices(Func<bool> rootVisibility = null, Action queryVisualUpdate = null);
        public object Find(int index, Func<int, int, object> accessor);
        public object GetValue(int index, out int group, IDictionary<int, object[]> valuesCache, IDictionary<int, GroupValue> groupsCache, object root);
        public void Load(int key, object[] level, bool createChildren, Action<int, int, int> onEntryCreated, Action<int> onEntryRemoved);
        protected sealed override void OnExpanded();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupIndices.<>c <>9;
            public static Action<Action> <>9__2_0;

            static <>c();
            internal void <OnExpanded>b__2_0(Action x);
        }
    }
}

