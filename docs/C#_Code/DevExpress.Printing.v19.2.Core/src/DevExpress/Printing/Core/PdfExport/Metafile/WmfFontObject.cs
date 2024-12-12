namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class WmfFontObject
    {
        private static string[] fontNames;
        internal const int MarkerBold = 1;
        internal const int MarkerItalic = 2;
        internal const int MarkerCourier = 0;
        internal const int MarkerHelvetica = 4;
        internal const int MarkerTimes = 8;
        internal const int MarkerSymbol = 12;
        internal const int FontDontCare = 0;
        internal const int FontRoman = 1;
        internal const int FontSwiss = 2;
        internal const int FontModern = 3;
        internal const int FontScript = 4;
        internal const int FontDecorative = 5;
        internal const int FixedPitch = 1;
        private PdfFont font;

        static WmfFontObject()
        {
            string[] textArray1 = new string[14];
            textArray1[0] = "Courier";
            textArray1[1] = "Courier-Bold";
            textArray1[2] = "Courier-Oblique";
            textArray1[3] = "Courier-BoldOblique";
            textArray1[4] = "Helvetica";
            textArray1[5] = "Helvetica-Bold";
            textArray1[6] = "Helvetica-Oblique";
            textArray1[7] = "Helvetica-BoldOblique";
            textArray1[8] = "Times-Roman";
            textArray1[9] = "Times-Bold";
            textArray1[10] = "Times-Italic";
            textArray1[11] = "Times-BoldItalic";
            textArray1[12] = "Symbol";
            textArray1[13] = "ZapfDingbats";
            fontNames = textArray1;
        }

        public PdfFont GetFont(float fontSize, IPdfContentsOwner owner)
        {
            if (this.font == null)
            {
                string str;
                if ((this.Facename.IndexOf("courier") != -1) || ((this.Facename.IndexOf("terminal") != -1) || (this.Facename.IndexOf("fixedsys") != -1)))
                {
                    str = fontNames[this.Italic + this.Bold];
                }
                else if ((this.Facename.IndexOf("ms sans serif") != -1) || ((this.Facename.IndexOf("arial") != -1) || (this.Facename.IndexOf("system") != -1)))
                {
                    str = fontNames[(4 + this.Italic) + this.Bold];
                }
                else if (this.Facename.IndexOf("arial black") != -1)
                {
                    str = fontNames[(4 + this.Italic) + 1];
                }
                else if ((this.Facename.IndexOf("times") != -1) || ((this.Facename.IndexOf("ms serif") != -1) || (this.Facename.IndexOf("roman") != -1)))
                {
                    str = fontNames[(8 + this.Italic) + this.Bold];
                }
                else if (this.Facename.IndexOf("symbol") != -1)
                {
                    str = fontNames[12];
                }
                else
                {
                    int num = this.PitchAndFamily & 3;
                    switch (((this.PitchAndFamily >> 4) & 7))
                    {
                        case 1:
                            str = fontNames[(8 + this.Italic) + this.Bold];
                            break;

                        case 2:
                        case 4:
                        case 5:
                            str = fontNames[(4 + this.Italic) + this.Bold];
                            break;

                        case 3:
                            str = fontNames[this.Italic + this.Bold];
                            break;

                        default:
                            str = (num != 1) ? fontNames[(4 + this.Italic) + this.Bold] : fontNames[this.Italic + this.Bold];
                            break;
                    }
                }
                FontStyle regular = FontStyle.Regular;
                if (this.Bold == 1)
                {
                    regular |= FontStyle.Bold;
                }
                if (this.IsItalic)
                {
                    regular |= FontStyle.Italic;
                }
                if (this.StrikeOut)
                {
                    regular |= FontStyle.Strikeout;
                }
                if (this.Underline)
                {
                    regular |= FontStyle.Underline;
                }
                this.font = PdfFonts.CreatePdfFont(new Font(str, fontSize, regular), false);
                owner.Fonts.AddUnique(this.font);
            }
            return this.font;
        }

        public void Read(MetaReader reader)
        {
            this.Height = reader.ReadInt16();
            this.Width = reader.ReadInt16();
            this.Angle = (float) ((((double) reader.ReadInt16()) / 1800.0) * 3.1415926535897931);
            this.Orientation = reader.ReadInt16();
            this.Weight = reader.ReadInt16();
            this.IsItalic = reader.ReadByte() != 0;
            this.Underline = reader.ReadByte() != 0;
            this.StrikeOut = reader.ReadByte() != 0;
            this.CharSet = reader.ReadByte();
            reader.ReadByte();
            this.ClipPrecision = reader.ReadByte();
            this.Quality = reader.ReadByte();
            this.PitchAndFamily = reader.ReadByte();
            int num = 0x20;
            byte[] bytes = new byte[num];
            int index = 0;
            while (true)
            {
                if (index < num)
                {
                    int num3 = reader.ReadByte();
                    if (num3 != 0)
                    {
                        bytes[index] = (byte) num3;
                        index++;
                        continue;
                    }
                }
                try
                {
                    this.Facename = Encoding.GetEncoding(0x4e4).GetString(bytes, 0, index);
                }
                catch
                {
                    this.Facename = Encoding.ASCII.GetString(bytes, 0, index);
                }
                this.Facename = this.Facename.ToLower(CultureInfo.InvariantCulture);
                return;
            }
        }

        public short Height { get; set; }

        public short Width { get; set; }

        public float Angle { get; set; }

        public short Orientation { get; set; }

        public short Weight { get; set; }

        public bool IsItalic { get; set; }

        public bool Underline { get; set; }

        public bool StrikeOut { get; set; }

        public byte CharSet { get; set; }

        public byte OutPrecision { get; set; }

        public byte ClipPrecision { get; set; }

        public byte Quality { get; set; }

        public int PitchAndFamily { get; set; }

        public string Facename { get; set; }

        public int Italic =>
            this.IsItalic ? 2 : 0;

        public int Bold =>
            (this.Width > 600) ? 1 : 0;
    }
}

