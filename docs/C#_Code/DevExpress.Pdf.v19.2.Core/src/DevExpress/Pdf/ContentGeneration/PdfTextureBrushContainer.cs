namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTextureBrushContainer : PdfTileBrushContainer<PdfXObjectCachedResource>
    {
        private readonly DXTextureBrush brush;

        public PdfTextureBrushContainer(DXTextureBrush brush) : base((double) brush.Image.Width, (double) brush.Image.Height, brush.WrapMode, brush.Transform)
        {
            this.brush = brush;
        }

        protected override void DrawTile(PdfCommandConstructor constructor, PdfXObjectCachedResource tile, PdfRectangle bounds, PdfTransformationMatrix matrix)
        {
            PdfTransformationMatrix transform = PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, -1.0, 0.0, 1.0), new PdfTransformationMatrix(bounds.Width, 0.0, 0.0, bounds.Height, bounds.Left, bounds.Bottom)), matrix);
            tile.Draw(constructor, new PdfRectangle(0.0, 0.0, 1.0, 1.0), transform);
        }

        protected override PdfXObjectCachedResource GetTile(PdfGraphicsCommandConstructor constructor) => 
            constructor.ImageCache.AddXObject(this.brush.Image);
    }
}

