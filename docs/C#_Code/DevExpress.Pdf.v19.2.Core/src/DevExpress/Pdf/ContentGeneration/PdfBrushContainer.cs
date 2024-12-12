namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public abstract class PdfBrushContainer
    {
        protected PdfBrushContainer()
        {
        }

        public virtual bool FillShape(PdfGraphicsCommandConstructor commandConstructor, PdfShapeFillingStrategy shape) => 
            false;

        protected static PdfTransformationMatrix GetActualTransformationMatrix(PdfGraphicsCommandConstructor constructor, DXTilingBrush brush) => 
            PdfTransformationMatrix.Multiply(GetTransformationMatrix(brush), constructor.TransformationMatrix);

        public abstract PdfTransparentColor GetColor(PdfGraphicsCommandConstructor commandConstructor);
        public static PdfTransformationMatrix GetTransformationMatrix(DXTilingBrush brush)
        {
            DXTransformationMatrix transform = brush.Transform;
            return new PdfTransformationMatrix((double) transform.A, (double) transform.B, (double) transform.C, (double) transform.D, (double) transform.E, (double) transform.F);
        }
    }
}

