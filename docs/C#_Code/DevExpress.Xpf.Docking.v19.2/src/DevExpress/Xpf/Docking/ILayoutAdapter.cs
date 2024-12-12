namespace DevExpress.Xpf.Docking
{
    using System;

    public interface ILayoutAdapter
    {
        string Resolve(DockLayoutManager owner, object item);
    }
}

