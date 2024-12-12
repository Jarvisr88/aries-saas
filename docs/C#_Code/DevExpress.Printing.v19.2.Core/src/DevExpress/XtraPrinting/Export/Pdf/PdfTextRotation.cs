namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfTextRotation
    {
        private const float angle = 90f;
        private PdfGraphicsImpl gr;
        private StringAlignment lineAlignment;
        private StringAlignment alignment;

        private PdfTextRotation(PdfGraphicsImpl gr, StringAlignment lineAlignment, StringAlignment alignment)
        {
            this.gr = gr;
            this.lineAlignment = lineAlignment;
            this.alignment = alignment;
        }

        private void ApplyTransform(float x, float y)
        {
            this.gr.TranslateTransform(-x, -y, MatrixOrder.Append);
            this.gr.RotateTransform(90f, MatrixOrder.Append);
            this.gr.TranslateTransform(x, y, MatrixOrder.Append);
        }

        private void BeginRotation(RectangleF bounds)
        {
            this.gr.SaveTransformState();
            float x = this.CalculateCenterX(bounds);
            this.ApplyTransform(x, this.CalculateCenterY(bounds));
        }

        public static void BeginRotation(PdfTextRotation rotation, RectangleF bounds)
        {
            if (rotation != null)
            {
                rotation.BeginRotation(bounds);
            }
        }

        private float CalculateCenterX(RectangleF bounds)
        {
            switch (this.alignment)
            {
                case StringAlignment.Near:
                    return bounds.Left;

                case StringAlignment.Center:
                    return (bounds.Left + (bounds.Width / 2f));

                case StringAlignment.Far:
                    return bounds.Right;
            }
            throw new ArgumentException();
        }

        private float CalculateCenterY(RectangleF bounds)
        {
            switch (this.lineAlignment)
            {
                case StringAlignment.Near:
                    return bounds.Top;

                case StringAlignment.Center:
                    return (bounds.Top + (bounds.Height / 2f));

                case StringAlignment.Far:
                    return bounds.Bottom;
            }
            throw new ArgumentException();
        }

        public static PdfTextRotation CreateInstance(PdfGraphicsImpl gr, StringFormat format) => 
            ((format.FormatFlags & StringFormatFlags.DirectionVertical) == 0) ? null : new PdfTextRotation(gr, format.LineAlignment, format.Alignment);

        private void EndRotation()
        {
            this.gr.ResetTransform();
            this.gr.ApplyTransformState(MatrixOrder.Append, true);
        }

        public static void EndRotation(PdfTextRotation rotation)
        {
            if (rotation != null)
            {
                rotation.EndRotation();
            }
        }
    }
}

