namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Windows.Media;

    public class DrawingImageBuilder : ImagesSourceBuilder<DrawingImage>
    {
        internal DrawingImageBuilder(DrawingImage source) : base(source)
        {
        }

        public override Bitmap CreateImage()
        {
            throw new NotImplementedException();
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            DrawingBuilder builder = DrawingBuilder.Create(base.Source.Drawing);
            if (builder != null)
            {
                builder.GenerateData(constructor);
            }
        }
    }
}

