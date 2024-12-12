namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfStrokingAlphaCache : PdfGraphicsStateParametersCache
    {
        protected override PdfGraphicsStateParameters CreateParameters(double value)
        {
            PdfGraphicsStateParameters parameters1 = new PdfGraphicsStateParameters();
            parameters1.StrokingAlphaConstant = new double?(value);
            return parameters1;
        }
    }
}

