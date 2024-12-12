namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsPictureObject : XlDrawingObjectBase, IXlHyperlinkOwner
    {
        public int PictureId { get; set; }

        public int BlipId { get; set; }

        public XlPictureHyperlink HyperlinkClick { get; set; }

        public XlSourceRectangle SourceRectangle { get; set; }

        internal override XlDrawingObjectType DrawingObjectType =>
            XlDrawingObjectType.Picture;
    }
}

