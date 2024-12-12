namespace DevExpress.Xpf.Printing.Native
{
    using System;

    public abstract class DrawingBuilder<TDrawing> : DrawingBuilder where TDrawing: System.Windows.Media.Drawing
    {
        private readonly TDrawing drawing;

        protected DrawingBuilder(TDrawing drawing)
        {
            this.drawing = drawing;
        }

        protected TDrawing Drawing =>
            this.drawing;
    }
}

