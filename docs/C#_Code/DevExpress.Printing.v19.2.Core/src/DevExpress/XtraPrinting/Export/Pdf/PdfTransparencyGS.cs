namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.CompilerServices;

    public class PdfTransparencyGS : PdfDocumentDictionaryObject
    {
        public PdfTransparencyGS(int transparency, bool compressed) : base(compressed)
        {
            this.Name = $"TransparencyGS{transparency}";
            this.Transparency = transparency;
        }

        public override void AddToDictionary(PdfDictionary dictionary)
        {
            dictionary.Add(this.Name, base.Dictionary);
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", "ExtGState");
            double num = ((double) this.Transparency) / 255.0;
            base.Dictionary.Add("CA", new PdfDouble(num));
            base.Dictionary.Add("ca", new PdfDouble(num));
        }

        public int Transparency { get; private set; }

        public string Name { get; private set; }
    }
}

