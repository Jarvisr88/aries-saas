namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class TransformActivator
    {
        private Matrix transform;
        private GraphicsUnit pageUnit;
        private SizeF pageSize;

        public TransformActivator(Matrix transform, GraphicsUnit pageUnit, SizeF pageSize)
        {
            this.transform = transform;
            this.pageUnit = pageUnit;
            this.pageSize = pageSize;
        }

        public void ApplyToDrawContext(PdfDrawContext context)
        {
            if (!this.transform.IsIdentity)
            {
                using (Matrix matrix = this.transform.Clone())
                {
                    using (Matrix matrix2 = new Matrix())
                    {
                        float scaleX = this.TransformValue(1f);
                        matrix2.Translate(0f, this.pageSize.Height);
                        matrix2.Scale(scaleX, -scaleX);
                        matrix.Multiply(matrix2, MatrixOrder.Append);
                        matrix2.Invert();
                        matrix.Multiply(matrix2, MatrixOrder.Prepend);
                        context.Concat(matrix);
                    }
                }
            }
        }

        private float TransformValue(float value) => 
            GraphicsUnitConverter.Convert(value, GraphicsDpi.UnitToDpiI(this.pageUnit), (float) 72f);

        public Matrix Transform =>
            this.transform;
    }
}

