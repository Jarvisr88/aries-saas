namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfLinearGradientShadingBuilder
    {
        private static readonly PdfRange[] functionsRange = new PdfRange[] { new PdfRange(0.0, 1.0), new PdfRange(0.0, 1.0), new PdfRange(0.0, 1.0) };
        private static readonly PdfRange[] functionDomain = new PdfRange[] { new PdfRange(0.0, 1.0) };
        private readonly double[] positions;

        protected PdfLinearGradientShadingBuilder(double[] positions)
        {
            this.positions = positions;
        }

        protected abstract PdfRange[] CreateEncodeArray();
        private PdfCustomFunction CreateFunction(PdfDocumentCatalog documentCatalog)
        {
            PdfObjectList<PdfCustomFunction> functions = this.CreateFunctions(documentCatalog);
            if (functions.Count == 1)
            {
                return functions[0];
            }
            double[] bounds = new double[this.Positions.Length - 1];
            for (int i = 0; i < (this.Positions.Length - 1); i++)
            {
                bounds[i] = this.Positions[i];
            }
            return new PdfStitchingFunction(bounds, this.CreateEncodeArray(), functions, functionDomain, functionsRange);
        }

        protected abstract PdfObjectList<PdfCustomFunction> CreateFunctions(PdfDocumentCatalog documentCatalog);
        public PdfAxialShading CreateShading(PdfDocumentCatalog documentCatalog, PdfRectangle gradientRect)
        {
            PdfCustomFunction item = this.CreateFunction(documentCatalog);
            double y = gradientRect.Bottom + (gradientRect.Height / 2.0);
            PdfPoint axisStart = new PdfPoint(gradientRect.Left, y);
            PdfPoint axisEnd = new PdfPoint(gradientRect.Right, y);
            PdfObjectList<PdfCustomFunction> blendFunctions = new PdfObjectList<PdfCustomFunction>(documentCatalog.Objects);
            blendFunctions.Add(item);
            return new PdfAxialShading(axisStart, axisEnd, blendFunctions);
        }

        protected static PdfRange[] FunctionsRange =>
            functionsRange;

        protected static PdfRange[] FunctionDomain =>
            functionDomain;

        protected double[] Positions =>
            this.positions;
    }
}

