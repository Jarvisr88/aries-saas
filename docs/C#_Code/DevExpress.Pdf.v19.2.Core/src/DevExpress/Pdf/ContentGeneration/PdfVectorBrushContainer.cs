namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfVectorBrushContainer : PdfTileBrushContainer<PdfForm>
    {
        private readonly Action<PdfGraphicsCommandConstructor> tilePaint;

        public PdfVectorBrushContainer(double tileWidth, double tileHeight, DXWrapMode wrapMode, DXTransformationMatrix brushMatrix, Action<PdfGraphicsCommandConstructor> tilePaint) : base(tileWidth, tileHeight, wrapMode, brushMatrix)
        {
            this.tilePaint = tilePaint;
        }

        protected override void DrawTile(PdfCommandConstructor constructor, PdfForm tile, PdfRectangle bounds, PdfTransformationMatrix matrix)
        {
            PdfRectangle bBox = tile.BBox;
            PdfTransformationMatrix matrix2 = PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(bounds.Width / bBox.Width, 0.0, 0.0, bounds.Height / bBox.Height, bounds.Left, bounds.Bottom), matrix);
            matrix2 = PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, -1.0, 0.0, bBox.Height), matrix2);
            constructor.DrawForm(constructor.DocumentCatalog.Objects.AddResolvedObject(tile), matrix2);
        }

        protected override PdfForm GetTile(PdfGraphicsCommandConstructor constructor)
        {
            PdfForm form = new PdfForm(constructor.DocumentCatalog, new PdfRectangle(0.0, 0.0, base.TileWidth, base.TileHeight));
            using (PdfGraphicsCommandConstructor constructor2 = new PdfGraphicsCommandConstructor(form, constructor.GraphicsDocument, 72f, 72f))
            {
                this.tilePaint(constructor2);
                form.ReplaceCommands(constructor2.Commands);
            }
            return form;
        }
    }
}

