namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class DestinationInfo
    {
        private int destPageIndex;
        private float destTop;
        private RectangleF linkArea;
        private PdfPage linkPage;

        public DestinationInfo(int destPageIndex, float destTop)
        {
            this.destPageIndex = -1;
            this.linkArea = Rectangle.Empty;
            this.Initialize(destPageIndex, destTop, null, RectangleF.Empty);
        }

        public DestinationInfo(int destPageIndex, float destTop, PdfPage linkPage, RectangleF linkArea)
        {
            this.destPageIndex = -1;
            this.linkArea = Rectangle.Empty;
            this.Initialize(destPageIndex, destTop, linkPage, linkArea);
        }

        private void Initialize(int destPageIndex, float destTop, PdfPage linkPage, RectangleF linkArea)
        {
            this.destPageIndex = destPageIndex;
            this.destTop = destTop;
            this.linkPage = linkPage;
            this.linkArea = linkArea;
        }

        public int DestPageIndex =>
            this.destPageIndex;

        public float DestTop
        {
            get => 
                this.destTop;
            set => 
                this.destTop = value;
        }

        public PdfPage LinkPage =>
            this.linkPage;

        public RectangleF LinkArea
        {
            get => 
                this.linkArea;
            set => 
                this.linkArea = value;
        }
    }
}

