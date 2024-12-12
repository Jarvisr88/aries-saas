namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfGraphicsState : IDisposable, IGraphicsState
    {
        private readonly System.Drawing.Drawing2D.Matrix matrix;
        private readonly PdfGraphicsClip clip;
        private readonly GraphicsUnit pageUnit;

        public PdfGraphicsState(System.Drawing.Drawing2D.Matrix matrix, PdfGraphicsClip clip, GraphicsUnit pageUnit)
        {
            this.matrix = matrix.Clone();
            this.clip = clip;
            this.pageUnit = pageUnit;
        }

        public PdfGraphicsState Clone() => 
            new PdfGraphicsState(this.matrix, this.clip, this.pageUnit);

        public void Dispose()
        {
            this.matrix.Dispose();
        }

        public System.Drawing.Drawing2D.Matrix Matrix =>
            this.matrix.Clone();

        public PdfGraphicsClip Clip =>
            this.clip;

        public GraphicsUnit PageUnit =>
            this.pageUnit;
    }
}

