namespace DevExpress.Xpf.Docking
{
    using System;

    internal interface IDockLayoutManagerListener
    {
        void Subscribe(DockLayoutManager manager);
        void Unsubscribe(DockLayoutManager manager);
    }
}

