namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTwoColorLinearGradientShadingBuilder : PdfLinearGradientShadingBuilder
    {
        private readonly ARGBColor[] brushColors;
        private readonly DXBlend blend;

        public PdfTwoColorLinearGradientShadingBuilder(ARGBColor[] brushColors, DXBlend blend) : base(blend.Positions)
        {
            this.brushColors = brushColors;
            this.blend = blend;
        }

        protected override PdfRange[] CreateEncodeArray()
        {
            double[] factors = this.blend.Factors;
            PdfRange[] rangeArray = new PdfRange[factors.Length];
            double min = 0.0;
            for (int i = 0; i < factors.Length; i++)
            {
                rangeArray[i] = new PdfRange(min, factors[i]);
                min = factors[i];
            }
            return rangeArray;
        }

        protected override PdfObjectList<PdfCustomFunction> CreateFunctions(PdfDocumentCatalog documentCatalog)
        {
            PdfCustomFunction item = new PdfExponentialInterpolationFunction(PdfGraphicsConverter.ConvertColorToColorComponents(this.brushColors[0]), PdfGraphicsConverter.ConvertColorToColorComponents(this.brushColors[1]), 1.0, PdfLinearGradientShadingBuilder.FunctionDomain, PdfLinearGradientShadingBuilder.FunctionsRange);
            double[] positions = this.blend.Positions;
            PdfObjectList<PdfCustomFunction> list = new PdfObjectList<PdfCustomFunction>(documentCatalog.Objects);
            for (int i = 0; i < positions.Length; i++)
            {
                list.Add(item);
            }
            return list;
        }
    }
}

