namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfNonStrokingAlphaCache : PdfGraphicsStateParametersCache
    {
        protected override PdfGraphicsStateParameters CreateParameters(double value)
        {
            PdfGraphicsStateParameters parameters1 = new PdfGraphicsStateParameters();
            parameters1.NonStrokingAlphaConstant = new double?(value);
            return parameters1;
        }
    }
}

