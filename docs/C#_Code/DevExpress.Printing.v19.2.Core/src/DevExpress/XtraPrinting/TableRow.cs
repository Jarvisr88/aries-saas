namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class TableRow : IXtraSupportDeserializeCollectionItem
    {
        private TableCellList bricks = new TableCellList();

        internal void Align(BrickAlignment alignment, float yOffset, float width)
        {
            float initialXOffset = width - this.CalcSize().Width;
            if (alignment == BrickAlignment.Center)
            {
                this.AlignFromPoint(yOffset, initialXOffset / 2f);
            }
            else if (alignment == BrickAlignment.Far)
            {
                this.AlignFromPoint(yOffset, initialXOffset);
            }
            else
            {
                this.AlignFromPoint(yOffset, 0f);
            }
        }

        private void AlignFromPoint(float yOffset, float initialXOffset)
        {
            float x = initialXOffset;
            foreach (Brick brick in this.bricks)
            {
                brick.Location = new PointF(x, yOffset);
                x += brick.Width;
            }
        }

        internal SizeF CalcSize()
        {
            this.bricks.InvalidateBounds();
            return this.bricks.Bounds.Size;
        }

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "Bricks") ? null : BrickFactory.CreateBrick(e);

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "Bricks")
            {
                this.Bricks.Add((Brick) e.Item.Value);
            }
        }

        [Description("Gets the array of bricks held by the TableRow."), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.Cached)]
        public BrickList Bricks =>
            this.bricks;
    }
}

