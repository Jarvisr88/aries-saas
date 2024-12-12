namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PdfShading : PdfDocumentDictionaryObject
    {
        public PdfShading(bool compressed, Color startColor, Color endColor, int index) : base(compressed)
        {
            this.StartColor = startColor;
            this.EndColor = endColor;
            this.Name = $"Sh{index}";
        }

        public override void AddToDictionary(PdfDictionary dictionary)
        {
            dictionary.Add(this.Name, base.Dictionary);
        }

        public override void FillUp()
        {
            base.Dictionary.Add("ShadingType", 2);
            base.Dictionary.Add("ColorSpace", "DeviceRGB");
            PdfArray array1 = new PdfArray();
            array1.Add(true);
            array1.Add(true);
            base.Dictionary.Add("Extend", array1);
            PdfArray array2 = new PdfArray();
            array2.Add(0);
            array2.Add(0);
            array2.Add(1);
            array2.Add(0);
            base.Dictionary.Add("Coords", array2);
            PdfDictionary dictionary = new PdfDictionary();
            dictionary.Add("FunctionType", 2);
            PdfArray array3 = new PdfArray();
            array3.Add(0);
            array3.Add(1);
            dictionary.Add("Domain", array3);
            dictionary.Add("C0", GetColorArray(this.StartColor));
            dictionary.Add("C1", GetColorArray(this.EndColor));
            dictionary.Add("N", 1);
            base.Dictionary.Add("Function", dictionary);
        }

        private static PdfArray GetColorArray(Color color) => 
            new PdfArray { 
                ToPdfColor(color.R),
                ToPdfColor(color.G),
                ToPdfColor(color.B)
            };

        private static float ToPdfColor(byte p) => 
            ((float) p) / 255f;

        public Color StartColor { get; private set; }

        public Color EndColor { get; private set; }

        public string Name { get; private set; }
    }
}

