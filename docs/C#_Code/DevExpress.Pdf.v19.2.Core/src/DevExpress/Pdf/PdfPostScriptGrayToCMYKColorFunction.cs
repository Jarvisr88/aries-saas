namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPostScriptGrayToCMYKColorFunction : PdfPostScriptCalculatorFunction
    {
        internal static string StringData = "{dup 0 mul exch dup 0 mul exch dup 0 mul exch 1 mul }";
        internal static string AlternateStringData = "{1.000000 2 1 roll 1.000000 2 1 roll 1.000000 2 1 roll 0 index 1.000000 \ncvr exch sub 2 1 roll 5 -1 roll 1.000000 cvr exch sub 5 1 \nroll 4 -1 roll 1.000000 cvr exch sub 4 1 roll 3 -1 roll 1.000000 \ncvr exch sub 3 1 roll 2 -1 roll 1.000000 cvr exch sub 2 1 \nroll pop }";

        internal PdfPostScriptGrayToCMYKColorFunction(PdfReaderDictionary dictionary, byte[] data) : base(dictionary, data)
        {
        }

        protected override double[] PerformTransformation(double[] arguments)
        {
            if (arguments.Length != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            double[] numArray = new double[4];
            numArray[3] = arguments[0];
            return numArray;
        }
    }
}

