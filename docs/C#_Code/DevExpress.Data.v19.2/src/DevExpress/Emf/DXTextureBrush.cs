namespace DevExpress.Emf
{
    using System;
    using System.Drawing;

    public class DXTextureBrush : DXTilingBrush
    {
        private readonly System.Drawing.Image image;

        public DXTextureBrush(System.Drawing.Image image, DXWrapMode wrapMode) : base(wrapMode)
        {
            this.image = image;
        }

        public override void Accept(IDXBrushVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Dispose()
        {
            this.image.Dispose();
        }

        public override void Write(EmfContentWriter writer)
        {
        }

        public System.Drawing.Image Image =>
            this.image;

        public override int DataSize =>
            0;
    }
}

