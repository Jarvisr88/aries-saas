namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class CustomUIFiltersSettings : ICustomUIFiltersSettings, IEnumerable<ICustomUIFiltersBox>, IEnumerable
    {
        private readonly IStorage<ICustomUIFiltersBox> storageCore;

        public CustomUIFiltersSettings(IEnumerable<ICustomUIFiltersBox> children)
        {
            Func<ICustomUIFiltersBox, int> getOrder = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<ICustomUIFiltersBox, int> local1 = <>c.<>9__1_0;
                getOrder = <>c.<>9__1_0 = x => x.Metric.Order;
            }
            this.storageCore = new Storage<ICustomUIFiltersBox>(children, getOrder);
        }

        void ICustomUIFiltersSettings.EnsureFiltersType(string path)
        {
            Func<ICustomUIFiltersBox, string> func1 = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<ICustomUIFiltersBox, string> local1 = <>c.<>9__5_0;
                func1 = <>c.<>9__5_0 = x => x.Metric.Path;
            }
            Action<ICustomUIFiltersBox> @do = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Action<ICustomUIFiltersBox> local2 = <>c.<>9__5_1;
                @do = <>c.<>9__5_1 = box => box.EnsureFiltersType();
            }
            this.storageCore[path, func1].Do<ICustomUIFiltersBox>(@do);
        }

        public bool HasFilters(string path)
        {
            Func<ICustomUIFiltersBox, string> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ICustomUIFiltersBox, string> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = x => x.Metric.Path;
            }
            return (this.storageCore[path, func1] != null);
        }

        IEnumerator<ICustomUIFiltersBox> IEnumerable<ICustomUIFiltersBox>.GetEnumerator() => 
            this.storageCore.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.storageCore.GetEnumerator();

        public ICustomUIFilters this[string path]
        {
            get
            {
                Func<ICustomUIFiltersBox, string> func1 = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<ICustomUIFiltersBox, string> local1 = <>c.<>9__4_0;
                    func1 = <>c.<>9__4_0 = x => x.Metric.Path;
                }
                Func<ICustomUIFiltersBox, ICustomUIFilters> get = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<ICustomUIFiltersBox, ICustomUIFilters> local2 = <>c.<>9__4_1;
                    get = <>c.<>9__4_1 = box => box.Filters;
                }
                return this.storageCore[path, func1].Get<ICustomUIFiltersBox, ICustomUIFilters>(get, null);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFiltersSettings.<>c <>9 = new CustomUIFiltersSettings.<>c();
            public static Func<ICustomUIFiltersBox, int> <>9__1_0;
            public static Func<ICustomUIFiltersBox, string> <>9__2_0;
            public static Func<ICustomUIFiltersBox, string> <>9__4_0;
            public static Func<ICustomUIFiltersBox, ICustomUIFilters> <>9__4_1;
            public static Func<ICustomUIFiltersBox, string> <>9__5_0;
            public static Action<ICustomUIFiltersBox> <>9__5_1;

            internal int <.ctor>b__1_0(ICustomUIFiltersBox x) => 
                x.Metric.Order;

            internal string <DevExpress.Utils.Filtering.Internal.ICustomUIFiltersSettings.EnsureFiltersType>b__5_0(ICustomUIFiltersBox x) => 
                x.Metric.Path;

            internal void <DevExpress.Utils.Filtering.Internal.ICustomUIFiltersSettings.EnsureFiltersType>b__5_1(ICustomUIFiltersBox box)
            {
                box.EnsureFiltersType();
            }

            internal string <get_Item>b__4_0(ICustomUIFiltersBox x) => 
                x.Metric.Path;

            internal ICustomUIFilters <get_Item>b__4_1(ICustomUIFiltersBox box) => 
                box.Filters;

            internal string <HasFilters>b__2_0(ICustomUIFiltersBox x) => 
                x.Metric.Path;
        }
    }
}

