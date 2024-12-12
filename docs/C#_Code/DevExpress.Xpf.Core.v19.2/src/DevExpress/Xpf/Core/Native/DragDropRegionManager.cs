namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public static class DragDropRegionManager
    {
        [ThreadStatic]
        private static List<DragDropRegionManager.Region> regions;

        public static IEnumerable<object> GetDragDropControls(string regionName, Dispatcher dispatcher);
        public static void RegisterDragDropRegion(object control, string regionName);
        public static void UnregisterDragDropRegion(object control, string regionName);

        private static List<DragDropRegionManager.Region> Regions { get; }

        private class Region
        {
            public readonly string RegionName;
            private readonly List<WeakReference> ControlReferences;

            public Region(string regionName);
            public void Add(object control);
            public IEnumerable<object> GetControls(Dispatcher dispatcher);
            private WeakReference GetReference(object control);
            public void Remove(object control);
            private void UpdateReferences();

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DragDropRegionManager.Region.<>c <>9;
                public static Func<WeakReference, object> <>9__4_0;

                static <>c();
                internal object <GetControls>b__4_0(WeakReference x);
            }
        }
    }
}

