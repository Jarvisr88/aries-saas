namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    internal class MetafileInfo
    {
        private System.Drawing.Imaging.Metafile metafile;

        public MetafileInfo(System.Drawing.Imaging.Metafile metafile)
        {
            this.metafile = metafile;
            this.HorizontalResolution = 72f;
            this.VerticalResolution = 72f;
            if (metafile != null)
            {
                this.ImageSize = metafile.Size;
                this.HorizontalResolution = metafile.HorizontalResolution;
                this.VerticalResolution = metafile.VerticalResolution;
            }
        }

        internal RectangleF GetBounds(ref GraphicsUnit pageUnit)
        {
            if (this.metafile != null)
            {
                return this.metafile.GetBounds(ref pageUnit);
            }
            pageUnit = this.TestPageUnit;
            return this.TestBounds;
        }

        public System.Drawing.Imaging.Metafile Metafile =>
            this.metafile;

        public bool IsNull =>
            ReferenceEquals(this.metafile, null);

        public float HorizontalResolution { get; set; }

        public float VerticalResolution { get; set; }

        public Size ImageSize { get; set; }

        internal RectangleF TestBounds { get; set; }

        internal GraphicsUnit TestPageUnit { get; set; }
    }
}

