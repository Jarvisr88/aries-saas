namespace DevExpress.Xpf.Docking
{
    using System;

    public interface IActiveItemOwner
    {
        void Activate(BaseLayoutItem item);
        void Activate(BaseLayoutItem item, bool setFocus);

        DockLayoutManager Container { get; }

        BaseLayoutItem ActiveItem { get; set; }
    }
}

