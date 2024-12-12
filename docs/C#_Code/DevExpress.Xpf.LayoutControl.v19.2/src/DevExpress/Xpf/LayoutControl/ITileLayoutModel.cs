namespace DevExpress.Xpf.LayoutControl
{
    using System;

    public interface ITileLayoutModel : IFlowLayoutModel, ILayoutModelBase
    {
        bool ShowGroupHeaders { get; set; }
    }
}

