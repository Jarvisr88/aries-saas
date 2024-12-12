namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    public abstract class XlDrawingObject : XlDrawingObjectBase
    {
        protected XlDrawingObject()
        {
        }

        public void SetAbsoluteAnchor(int x, int y, int width, int height)
        {
            base.AnchorType = XlAnchorType.Absolute;
            base.TopLeft = new XlAnchorPoint(0, 0, x, y);
            base.BottomRight = new XlAnchorPoint(0, 0, x + width, y + height);
        }

        public void SetOneCellAnchor(XlAnchorPoint topLeft, int width, int height)
        {
            base.AnchorType = XlAnchorType.OneCell;
            base.TopLeft = topLeft;
            base.BottomRight = new XlAnchorPoint(topLeft.Column, topLeft.Row, topLeft.ColumnOffsetInPixels + width, topLeft.RowOffsetInPixels + height);
        }

        public void SetTwoCellAnchor(XlAnchorPoint topLeft, XlAnchorPoint bottomRight, XlAnchorType anchorBehavior)
        {
            base.AnchorType = XlAnchorType.TwoCell;
            base.AnchorBehavior = anchorBehavior;
            base.TopLeft = topLeft;
            base.BottomRight = bottomRight;
        }
    }
}

