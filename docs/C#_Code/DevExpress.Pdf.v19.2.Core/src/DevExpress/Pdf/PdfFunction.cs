namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfFunction : PdfObject
    {
        protected const string FunctionTypeDictionaryKey = "FunctionType";

        protected PdfFunction() : base(-1)
        {
        }

        protected PdfFunction(int objectNumber) : base(objectNumber)
        {
        }

        protected internal abstract bool IsSame(PdfFunction function);
        internal static PdfFunction Parse(PdfObjectCollection objects, object value, bool expectDefault)
        {
            value = objects.TryResolve(value, null);
            PdfName name = value as PdfName;
            if (name == null)
            {
                return PdfCustomFunction.PerformParse(value);
            }
            string str = name.Name;
            if (str != "Default")
            {
                if (str == "Identity")
                {
                    return PdfPredefinedFunction.Identity;
                }
            }
            else if (expectDefault)
            {
                return PdfPredefinedFunction.Default;
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected internal abstract double[] Transform(double[] arguments);
        protected internal abstract object Write(PdfObjectCollection objects);
    }
}

