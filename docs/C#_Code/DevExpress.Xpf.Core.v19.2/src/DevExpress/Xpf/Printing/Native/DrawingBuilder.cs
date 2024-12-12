namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows.Media;

    public abstract class DrawingBuilder
    {
        protected DrawingBuilder()
        {
        }

        public static DrawingBuilder Create(Drawing drawing)
        {
            switch (drawing)
            {
                case (null):
                    return null;
                    break;
            }
            if (drawing is DrawingGroup)
            {
                return new DrawingGroupBuilder((DrawingGroup) drawing);
            }
            if (drawing is GeometryDrawing)
            {
                return new GeometryDrawingBuilder((GeometryDrawing) drawing);
            }
            if (drawing is ImageDrawing)
            {
                return new ImageDrawingBuilder((ImageDrawing) drawing);
            }
            if (drawing is GlyphRunDrawing)
            {
                return new GlyphRunDrawingBuilder((GlyphRunDrawing) drawing);
            }
            if (!(drawing is VideoDrawing))
            {
                throw new Exception("Unknow source");
            }
            return new VideoDrawingBuilder((VideoDrawing) drawing);
        }

        public abstract void GenerateData(PdfGraphicsCommandConstructor constructor);
    }
}

