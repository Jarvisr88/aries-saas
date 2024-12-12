namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class WeakBarItemList : WeakList<BarItem>
    {
        public WeakBarItemList(BarManager manager);
        public override void Clear();
        public override void Insert(int index, BarItem item);
        public override void RemoveAt(int index);

        public BarManager Manager { get; private set; }
    }
}

