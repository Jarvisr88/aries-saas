namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfTileBrushContainer<TTile> : PdfBrushContainer
    {
        private readonly DXWrapMode wrapMode;
        private readonly double tileWidth;
        private readonly double tileHeight;
        private readonly PdfTransformationMatrix brushMatrix;

        protected PdfTileBrushContainer(double tileWidth, double tileHeight, DXWrapMode wrapMode, DXTransformationMatrix brushMatrix)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.wrapMode = wrapMode;
            this.brushMatrix = new PdfTransformationMatrix((double) brushMatrix.A, (double) brushMatrix.B, (double) brushMatrix.C, (double) brushMatrix.D, (double) brushMatrix.E, (double) brushMatrix.F);
        }

        protected abstract void DrawTile(PdfCommandConstructor constructor, TTile tile, PdfRectangle bounds, PdfTransformationMatrix matrix);
        public override PdfTransparentColor GetColor(PdfGraphicsCommandConstructor commandConstructor)
        {
            double width;
            double height;
            PdfRectangle boundingBox = new PdfRectangle(0.0, 0.0, this.tileWidth * 2.0, this.tileHeight * 2.0);
            PdfRectangle bBox = commandConstructor.BBox;
            PdfTransformationMatrix matrix = PdfTransformationMatrix.Multiply(this.brushMatrix, commandConstructor.TransformationMatrix);
            if (this.wrapMode != DXWrapMode.Clamp)
            {
                width = boundingBox.Width;
                height = boundingBox.Height;
            }
            else
            {
                PdfRectangle rectangle3 = PdfTransformationMatrix.Invert(matrix).TransformBoundingBox(bBox);
                width = rectangle3.Width;
                height = rectangle3.Height;
            }
            PdfTilingPattern pattern = new PdfTilingPattern(matrix, boundingBox, width, height, commandConstructor.DocumentCatalog);
            TTile tile = this.GetTile(commandConstructor);
            using (PdfCommandConstructor constructor = new PdfCommandConstructor(pattern.Resources))
            {
                if (this.wrapMode == DXWrapMode.Clamp)
                {
                    this.DrawTile(constructor, tile, new PdfRectangle(0.0, 0.0, this.tileWidth, this.tileHeight), PdfTransformationMatrix.Identity);
                }
                else
                {
                    PdfTransformationMatrix matrix2 = ((this.wrapMode == DXWrapMode.TileFlipX) || (this.wrapMode == DXWrapMode.TileFlipXY)) ? new PdfTransformationMatrix(-1.0, 0.0, 0.0, 1.0, this.tileWidth * 3.0, 0.0) : PdfTransformationMatrix.Identity;
                    PdfTransformationMatrix matrix3 = ((this.wrapMode == DXWrapMode.TileFlipY) || (this.wrapMode == DXWrapMode.TileFlipXY)) ? new PdfTransformationMatrix(1.0, 0.0, 0.0, -1.0, 0.0, this.tileHeight * 3.0) : PdfTransformationMatrix.Identity;
                    PdfTransformationMatrix matrix4 = PdfTransformationMatrix.Multiply(matrix2, matrix3);
                    this.DrawTile(constructor, tile, new PdfRectangle(0.0, 0.0, this.tileWidth, this.tileHeight), PdfTransformationMatrix.Identity);
                    this.DrawTile(constructor, tile, new PdfRectangle(this.tileWidth, 0.0, boundingBox.Width, this.tileHeight), matrix2);
                    this.DrawTile(constructor, tile, new PdfRectangle(0.0, this.tileHeight, this.tileWidth, boundingBox.Height), matrix3);
                    this.DrawTile(constructor, tile, new PdfRectangle(this.tileWidth, this.tileHeight, boundingBox.Width, boundingBox.Height), matrix4);
                }
                pattern.ReplaceCommands(constructor.Commands);
                return new PdfTransparentColor(0xff, pattern, new double[0]);
            }
        }

        protected abstract TTile GetTile(PdfGraphicsCommandConstructor constructor);

        protected double TileWidth =>
            this.tileWidth;

        protected double TileHeight =>
            this.tileHeight;
    }
}

