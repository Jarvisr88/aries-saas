namespace DevExpress.Office.Drawing
{
    using System;

    public class DrawingBlipTileInfoIndexChangeHistoryItem : DrawingBlipFillHistoryItem
    {
        public DrawingBlipTileInfoIndexChangeHistoryItem(DrawingBlipFill obj) : base(obj)
        {
        }

        protected override void RedoCore()
        {
            base.Object.SetIndexCore(DrawingBlipFill.TileInfoIndexAccessor, base.NewIndex, base.ChangeActions);
        }

        protected override void UndoCore()
        {
            base.Object.SetIndexCore(DrawingBlipFill.TileInfoIndexAccessor, base.OldIndex, base.ChangeActions);
        }
    }
}

