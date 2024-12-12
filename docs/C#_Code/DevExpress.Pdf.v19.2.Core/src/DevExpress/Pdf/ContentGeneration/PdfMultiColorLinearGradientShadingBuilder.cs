namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfMultiColorLinearGradientShadingBuilder : PdfLinearGradientShadingBuilder
    {
        private readonly DXColorBlend colorBlend;

        public PdfMultiColorLinearGradientShadingBuilder(DXColorBlend colorBlend) : base(colorBlend.Positions)
        {
            this.colorBlend = colorBlend;
        }

        protected override PdfRange[] CreateEncodeArray()
        {
            double[] positions = this.colorBlend.Positions;
            PdfRange[] rangeArray = new PdfRange[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                rangeArray[i] = new PdfRange(0.0, 1.0);
            }
            return rangeArray;
        }

        protected override PdfObjectList<PdfCustomFunction> CreateFunctions(PdfDocumentCatalog documentCatalog)
        {
            ARGBColor[] colors = this.colorBlend.Colors;
            double[] numArray = PdfGraphicsConverter.ConvertColorToColorComponents(colors[0]);
            PdfObjectList<PdfCustomFunction> list = new PdfObjectList<PdfCustomFunction>(documentCatalog.Objects);
            PdfRange[] functionDomain = PdfLinearGradientShadingBuilder.FunctionDomain;
            PdfRange[] functionsRange = PdfLinearGradientShadingBuilder.FunctionsRange;
            for (int i = 0; i < colors.Length; i++)
            {
                double[] numArray2 = PdfGraphicsConverter.ConvertColorToColorComponents(colors[i]);
                list.Add(new PdfExponentialInterpolationFunction(numArray, numArray2, 1.0, functionDomain, functionsRange));
                numArray = numArray2;
            }
            return list;
        }
    }
}

