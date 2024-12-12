namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Implementation;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlsxPicture : XlDrawingObjectBase, IXlHyperlinkOwner
    {
        public string RelationId { get; set; }

        public int PictureId { get; set; }

        public XlPictureHyperlink HyperlinkClick { get; set; }

        public XlSourceRectangle SourceRectangle { get; set; }

        internal override XlDrawingObjectType DrawingObjectType =>
            XlDrawingObjectType.Picture;
    }
}

