namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class PdfBitmap : PdfImageBase
    {
        private System.Drawing.Size size;

        public PdfBitmap(string name, System.Drawing.Size size, bool compressed) : base(name, compressed)
        {
            this.size = size;
        }

        public override void FillUp()
        {
            base.FillUp();
            base.Attributes.Add("Subtype", "Image");
            base.Attributes.Add("Width", this.size.Width);
            base.Attributes.Add("Height", this.size.Height);
        }

        protected System.Drawing.Size Size =>
            this.size;
    }
}

