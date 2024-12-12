namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfGraphicsStateParametersCache
    {
        private readonly IDictionary<double, PdfGraphicsStateParameters> cache = new Dictionary<double, PdfGraphicsStateParameters>();

        protected PdfGraphicsStateParametersCache()
        {
        }

        protected abstract PdfGraphicsStateParameters CreateParameters(double value);
        public PdfGraphicsStateParameters GetParameters(double key)
        {
            PdfGraphicsStateParameters parameters;
            double num = Math.Round(key, 4);
            if (!this.cache.TryGetValue(num, out parameters))
            {
                parameters = this.CreateParameters(num);
                this.cache.Add(num, parameters);
            }
            return parameters;
        }
    }
}

