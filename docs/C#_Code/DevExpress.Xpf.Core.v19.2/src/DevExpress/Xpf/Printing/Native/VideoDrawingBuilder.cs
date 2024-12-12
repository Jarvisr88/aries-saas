namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows.Media;

    public class VideoDrawingBuilder : DrawingBuilder<VideoDrawing>
    {
        public VideoDrawingBuilder(VideoDrawing drawing) : base(drawing)
        {
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
        }
    }
}

