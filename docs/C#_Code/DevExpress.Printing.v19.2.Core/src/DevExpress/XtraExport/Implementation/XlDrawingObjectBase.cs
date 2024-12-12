namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class XlDrawingObjectBase
    {
        protected XlDrawingObjectBase()
        {
        }

        internal bool IsSingleCellAnchor() => 
            (this.BottomRight.ColumnOffsetInPixels == 0) && ((this.BottomRight.RowOffsetInPixels == 0) && this.BottomRight.Equals(this.TopLeft));

        public string Name { get; set; }

        public XlAnchorType AnchorType { get; set; }

        public XlAnchorType AnchorBehavior { get; set; }

        public XlAnchorPoint TopLeft { get; set; }

        public XlAnchorPoint BottomRight { get; set; }

        internal abstract XlDrawingObjectType DrawingObjectType { get; }
    }
}

