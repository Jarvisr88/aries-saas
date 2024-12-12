namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class LogicalTreeLocker : IDisposable
    {
        private DockLayoutManager Container;
        private List<BaseLayoutItem> Items;

        public LogicalTreeLocker(DockLayoutManager container, params BaseLayoutItem[] items)
        {
            this.Container = container;
            if (this.Container != null)
            {
                this.Container.LockLogicalTreeChanging(this);
            }
            this.Items = this.CollectLayoutItems(items);
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__3_0;
                visit = <>c.<>9__3_0 = item => item.LockInLogicalTree();
            }
            this.Items.Accept<BaseLayoutItem>(visit);
        }

        private List<BaseLayoutItem> CollectLayoutItems(BaseLayoutItem[] items)
        {
            List<BaseLayoutItem> list = new List<BaseLayoutItem>();
            if (items == null)
            {
                return list;
            }
            items.Accept<BaseLayoutItem>(delegate (BaseLayoutItem item) {
                Action<BaseLayoutItem> <>9__1;
                Action<BaseLayoutItem> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<BaseLayoutItem> local1 = <>9__1;
                    action = <>9__1 = x => x.Accept(new VisitDelegate<BaseLayoutItem>(list.Add));
                }
                item.Do<BaseLayoutItem>(action);
            });
            return list.Distinct<BaseLayoutItem>().ToList<BaseLayoutItem>();
        }

        public void Dispose()
        {
            if (this.Container != null)
            {
                this.Container.UnlockLogicalTreeChanging(this);
            }
            VisitDelegate<BaseLayoutItem> visit = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                VisitDelegate<BaseLayoutItem> local1 = <>c.<>9__4_0;
                visit = <>c.<>9__4_0 = item => item.UnlockItemInLogicalTree();
            }
            this.Items.Accept<BaseLayoutItem>(visit);
            this.Container = null;
            this.Items = null;
        }

        internal bool IsLocked(DependencyObject element)
        {
            BaseLayoutItem item = element as BaseLayoutItem;
            return ((item != null) && this.Items.Contains(item));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LogicalTreeLocker.<>c <>9 = new LogicalTreeLocker.<>c();
            public static VisitDelegate<BaseLayoutItem> <>9__3_0;
            public static VisitDelegate<BaseLayoutItem> <>9__4_0;

            internal void <.ctor>b__3_0(BaseLayoutItem item)
            {
                item.LockInLogicalTree();
            }

            internal void <Dispose>b__4_0(BaseLayoutItem item)
            {
                item.UnlockItemInLogicalTree();
            }
        }
    }
}

