namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;

    public interface ITileLayoutControl : IFlowLayoutControl, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, IFlowLayoutModel, ITileLayoutModel
    {
        void OnTileSizeChanged(ITile tile);
        void StopGroupHeaderEditing();
        void TileClick(ITile tile);
    }
}

