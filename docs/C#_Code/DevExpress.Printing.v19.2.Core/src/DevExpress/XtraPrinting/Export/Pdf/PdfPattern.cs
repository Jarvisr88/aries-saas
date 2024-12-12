namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PdfPattern : PdfDocumentStreamObject
    {
        public PdfPattern(bool compressed, Color foreColor, Color backColor, int index) : base(compressed)
        {
            this.ForegroundColor = foreColor;
            this.BackgroundColor = backColor;
            this.Name = $"P{index}";
        }

        public override void AddToDictionary(PdfDictionary dictionary)
        {
            dictionary.Add(this.Name, base.Stream);
        }

        private void DrawPattrnLine(ref PdfDictionary gst, string name, Color color, int s, int e)
        {
            if (color.A != 0)
            {
                base.Stream.SetString("q ");
                if (color.A != 0xff)
                {
                    gst ??= new PdfDictionary();
                    PdfDictionary dictionary = new PdfDictionary();
                    dictionary.Add("Type", "ExtGState");
                    dictionary.Add("ca", new PdfDouble(ToPdfColorFloat(color.A)));
                    gst.Add(name, dictionary);
                    base.Stream.SetStringLine("/" + name + " gs");
                }
                base.Stream.SetStringLine(GetColorString(color) + " rg");
                base.Stream.SetStringLine(string.Format("{0} 0 m {1} 0 l {1} 1200 l {0} 1200 l h f Q", s, e));
            }
        }

        public override void FillUp()
        {
            base.Attributes.Add("PatternType", 1);
            base.Attributes.Add("PaintType", 1);
            base.Attributes.Add("TilingType", 2);
            PdfArray array1 = new PdfArray();
            array1.Add(0);
            array1.Add(0);
            array1.Add(8);
            array1.Add(0x4b0);
            base.Attributes.Add("BBox", array1);
            PdfArray array2 = new PdfArray();
            array2.Add((float) 0.6f);
            array2.Add((float) -0.6f);
            array2.Add((float) 0.6f);
            array2.Add((float) 0.6f);
            array2.Add(0);
            array2.Add(0x48);
            base.Attributes.Add("Matrix", array2);
            base.Attributes.Add("XStep", 8);
            base.Attributes.Add("YStep", 0x4b0);
            PdfDictionary dictionary = new PdfDictionary();
            PdfDictionary gst = null;
            base.Stream.SetStringLine("1 0 0 1 0 0 cm");
            this.DrawPattrnLine(ref gst, "GSF", this.ForegroundColor, 0, 3);
            this.DrawPattrnLine(ref gst, "GSB", this.BackgroundColor, 3, 8);
            dictionary.AddIfNotNull("ExtGState", gst);
            base.Attributes.Add("Resources", dictionary);
        }

        private static string GetColorString(Color color)
        {
            string[] textArray1 = new string[] { ToPdfColor(color.R), " ", ToPdfColor(color.G), " ", ToPdfColor(color.B) };
            return string.Concat(textArray1);
        }

        private static string ToPdfColor(byte p) => 
            Utils.ToString(ToPdfColorFloat(p));

        private static double ToPdfColorFloat(byte p) => 
            Math.Round((double) (((float) p) / 255f), 3);

        public Color ForegroundColor { get; private set; }

        public Color BackgroundColor { get; private set; }

        public string Name { get; private set; }
    }
}

