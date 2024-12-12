namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfDrawContext : PdfMeasuringContext
    {
        private IPdfContentsOwner owner;
        private PdfHashtable pdfHashtable;
        private PdfStream stream;

        public PdfDrawContext(PdfStream stream, IPdfContentsOwner owner, PdfHashtable pdfHashtable)
        {
            this.stream = stream;
            this.owner = owner;
            this.pdfHashtable = pdfHashtable;
        }

        public void Arc(PointF point1, PointF point2, int startAngle, int endAngle)
        {
            List<float[]> list = this.CreateBezierArc(point1.X, point1.Y, point2.X, point2.Y, (float) startAngle, (float) endAngle);
            if (list.Count != 0)
            {
                float[] numArray = list[0];
                this.MoveTo(numArray[0], numArray[1]);
                for (int i = 0; i < list.Count; i++)
                {
                    numArray = list[i];
                    this.CurveTo(numArray[2], numArray[3], numArray[4], numArray[5], numArray[6], numArray[7]);
                }
            }
        }

        public void BeginText()
        {
            this.Stream.SetStringLine("BT");
        }

        public void Clip()
        {
            this.Stream.SetStringLine("W");
        }

        public void ClosePath()
        {
            this.Stream.SetStringLine("h");
        }

        public void ClosePathAndStroke()
        {
            this.Stream.SetStringLine("s");
        }

        public void ClosePathEoFillAndStroke()
        {
            this.Stream.SetStringLine("b*");
        }

        public void ClosePathFillAndStroke()
        {
            this.Stream.SetStringLine("b");
        }

        public void Concat(Matrix matrix)
        {
            float[] elements = matrix.Elements;
            this.Concat(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
        }

        public void Concat(float a, float b, float c, float d, float e, float f)
        {
            string[] textArray1 = new string[12];
            textArray1[0] = Utils.ToString((double) a);
            textArray1[1] = " ";
            textArray1[2] = Utils.ToString((double) b);
            textArray1[3] = " ";
            textArray1[4] = Utils.ToString((double) c);
            textArray1[5] = " ";
            textArray1[6] = Utils.ToString((double) d);
            textArray1[7] = " ";
            textArray1[8] = Utils.ToString((double) e);
            textArray1[9] = " ";
            textArray1[10] = Utils.ToString((double) f);
            textArray1[11] = " cm";
            this.Stream.SetStringLine(string.Concat(textArray1));
        }

        public static PdfDrawContext Create(PdfStream stream, IPdfContentsOwner owner, PdfHashtable pdfHashtable) => 
            (!SecurityHelper.IsUnmanagedCodeGrantedAndHasZeroHwnd || AzureCompatibility.Enable) ? new PartialTrustPdfDrawContext(stream, owner, pdfHashtable) : new PdfDrawContext(stream, owner, pdfHashtable);

        private List<float[]> CreateBezierArc(float x1, float y1, float x2, float y2, float startAngle, float endAngle)
        {
            float num;
            float num2;
            int num3;
            if (x1 > x2)
            {
                num = x1;
                x1 = x2;
                x2 = num;
            }
            if (y1 > y2)
            {
                num = y1;
                y1 = y2;
                y2 = num;
            }
            if (Math.Abs(endAngle) <= 90f)
            {
                num2 = endAngle;
                num3 = 1;
            }
            else
            {
                num3 = (int) Math.Ceiling((double) (Math.Abs(endAngle) / 90f));
                num2 = endAngle / ((float) num3);
            }
            float num4 = (x1 + x2) / 2f;
            float num5 = (y1 + y2) / 2f;
            float num6 = (x2 - x1) / 2f;
            float num7 = (y2 - y1) / 2f;
            float num8 = (float) ((num2 * 3.1415926535897931) / 360.0);
            float num9 = (float) Math.Abs((double) ((1.3333333333333333 * (1.0 - Math.Cos((double) num8))) / Math.Sin((double) num8)));
            List<float[]> list = new List<float[]>();
            for (int i = 0; i < num3; i++)
            {
                float num11 = (float) (((startAngle + (i * num2)) * 3.1415926535897931) / 180.0);
                float num12 = (float) (((startAngle + ((i + 1) * num2)) * 3.1415926535897931) / 180.0);
                float num13 = (float) Math.Cos((double) num11);
                float num14 = (float) Math.Cos((double) num12);
                float num15 = (float) Math.Sin((double) num11);
                float num16 = (float) Math.Sin((double) num12);
                if (num2 > 0f)
                {
                    float[] item = new float[] { num4 + (num6 * num13), num5 - (num7 * num15), num4 + (num6 * (num13 - (num9 * num15))), num5 - (num7 * (num15 + (num9 * num13))), num4 + (num6 * (num14 + (num9 * num16))), num5 - (num7 * (num16 - (num9 * num14))), num4 + (num6 * num14), num5 - (num7 * num16) };
                    list.Add(item);
                }
                else
                {
                    float[] item = new float[] { num4 + (num6 * num13), num5 - (num7 * num15), num4 + (num6 * (num13 + (num9 * num15))), num5 - (num7 * (num15 - (num9 * num13))), num4 + (num6 * (num14 - (num9 * num16))), num5 - (num7 * (num16 + (num9 * num14))), num4 + (num6 * num14), num5 - (num7 * num16) };
                    list.Add(item);
                }
            }
            return list;
        }

        public void CurveTo(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            string[] textArray1 = new string[12];
            textArray1[0] = Utils.ToString((double) x1);
            textArray1[1] = " ";
            textArray1[2] = Utils.ToString((double) y1);
            textArray1[3] = " ";
            textArray1[4] = Utils.ToString((double) x2);
            textArray1[5] = " ";
            textArray1[6] = Utils.ToString((double) y2);
            textArray1[7] = " ";
            textArray1[8] = Utils.ToString((double) x3);
            textArray1[9] = " ";
            textArray1[10] = Utils.ToString((double) y3);
            textArray1[11] = " c";
            this.Stream.SetStringLine(string.Concat(textArray1));
        }

        public void CurveToV(float x2, float y2, float x3, float y3)
        {
            string[] textArray1 = new string[] { Utils.ToString((double) x2), " ", Utils.ToString((double) y2), " ", Utils.ToString((double) x3), " ", Utils.ToString((double) y3), " v" };
            this.Stream.SetStringLine(string.Concat(textArray1));
        }

        public void CurveToY(float x1, float y1, float x3, float y3)
        {
            string[] textArray1 = new string[] { Utils.ToString((double) x1), " ", Utils.ToString((double) y1), " ", Utils.ToString((double) x3), " ", Utils.ToString((double) y3), " y" };
            this.Stream.SetStringLine(string.Concat(textArray1));
        }

        public void EndText()
        {
            this.Stream.SetStringLine("ET");
        }

        public void EoClip()
        {
            this.Stream.SetStringLine("W*");
        }

        public void EoFill()
        {
            this.Stream.SetStringLine("f*");
        }

        public void EoFillAndStroke()
        {
            this.Stream.SetStringLine("B*");
        }

        public void ExecuteGraphicsState(PdfTransparencyGS gs)
        {
            if (gs != null)
            {
                this.Stream.SetStringLine("/" + gs.Name + " gs");
            }
        }

        public void ExecuteXObject(PdfXObject xObject)
        {
            if (xObject != null)
            {
                this.Stream.SetStringLine("/" + xObject.Name + " Do");
            }
        }

        public void Fill()
        {
            this.Stream.SetStringLine("f");
        }

        public void FillAndStroke()
        {
            this.Stream.SetStringLine("B");
        }

        public void GRestore()
        {
            this.Stream.SetStringLine("Q");
        }

        public void GSave()
        {
            this.Stream.SetStringLine("q");
        }

        public void LineTo(float x, float y)
        {
            this.Stream.SetStringLine(Utils.ToString((double) x) + " " + Utils.ToString((double) y) + " l");
        }

        public void MoveTextPoint(float tx, float ty)
        {
            this.Stream.SetStringLine(this.pdfHashtable.GetSize(tx, ty));
        }

        public void MoveTextToNextLine()
        {
            this.Stream.SetStringLine("T*");
        }

        public void MoveTo(float x, float y)
        {
            this.Stream.SetStringLine(Utils.ToString((double) x) + " " + Utils.ToString((double) y) + " m");
        }

        public void NewPath()
        {
            this.Stream.SetStringLine("n");
        }

        public void Pattern(PdfPattern pattern)
        {
            this.Stream.SetStringLine("/Pattern cs /" + pattern.Name + " scn");
        }

        public void Rectangle(float x, float y, float width, float height)
        {
            this.Stream.SetStringLine(this.pdfHashtable.GetRectangle(x, y, width, height));
        }

        public override void SetCharSpacing(float charSpace)
        {
            base.SetCharSpacing(charSpace);
            this.Stream.SetStringLine(Utils.ToString((double) charSpace) + " Tc");
        }

        public void SetDash(float[] array, int phase)
        {
            this.Stream.SetString("[");
            for (int i = 0; i < array.Length; i++)
            {
                this.Stream.SetString(Utils.ToString((double) array[i]));
                if (i < (array.Length - 1))
                {
                    this.Stream.SetString(" ");
                }
            }
            this.Stream.SetStringLine("] " + Convert.ToString(phase) + " d");
        }

        public void SetFlat(int flatness)
        {
            if (flatness < 0)
            {
                flatness = 0;
            }
            if (flatness > 100)
            {
                flatness = 100;
            }
            this.Stream.SetStringLine(Convert.ToString(flatness) + " i");
        }

        internal void SetFontAndSize(PdfFont pdfFont, Font actualFont)
        {
            if (((pdfFont != null) && (pdfFont.Name != null)) && ((actualFont.Size >= 0f) && (actualFont.Size <= 300f)))
            {
                this.owner.Fonts.AddUnique(pdfFont);
                string[] textArray1 = new string[] { "/", pdfFont.Name, " ", Utils.ToString((double) actualFont.Size), "  Tf" };
                this.Stream.SetStringLine(string.Concat(textArray1));
                pdfFont.CreateInnerFont();
                base.SetFont(pdfFont, actualFont);
            }
        }

        public override void SetHorizontalScaling(int scale)
        {
            base.SetHorizontalScaling(scale);
            this.Stream.SetStringLine(Convert.ToString(scale) + " Tz");
        }

        public void SetLeading(float leading)
        {
            this.Stream.SetStringLine(Utils.ToString((double) leading) + " TL");
        }

        public void SetLineCap(PdfLineCapStyle lineCap)
        {
            this.Stream.SetStringLine(Convert.ToString(Convert.ToInt16(lineCap)) + " J");
        }

        public void SetLineJoin(PdfLineJoinStyle lineJoin)
        {
            this.Stream.SetStringLine(Convert.ToString(Convert.ToInt16(lineJoin)) + " j");
        }

        public void SetLineWidth(float lineWidth)
        {
            this.Stream.SetStringLine(Utils.ToString((double) lineWidth) + " w");
        }

        public void SetMiterLimit(int miterLimit)
        {
            this.Stream.SetStringLine(Convert.ToString(miterLimit) + " M");
        }

        public void SetRenderingMode(PdfTextRenderingMode render)
        {
            this.Stream.SetStringLine(Convert.ToString(Convert.ToInt16(render)) + " Tr");
        }

        public void SetRGBFillColor(Color color)
        {
            this.Stream.SetStringLine(this.pdfHashtable.GetRGBFillColor(color));
        }

        public void SetRGBStrokeColor(Color color)
        {
            this.Stream.SetStringLine(this.pdfHashtable.GetRGBStrokeColor(color));
        }

        public void SetTextMatrix(float x, float y)
        {
            this.SetTextMatrix(1f, 0f, 0f, 1f, x, y);
        }

        public void SetTextMatrix(float a, float b, float c, float d, float x, float y)
        {
            string[] textArray1 = new string[12];
            textArray1[0] = Utils.ToString((double) a);
            textArray1[1] = " ";
            textArray1[2] = Utils.ToString((double) b);
            textArray1[3] = " ";
            textArray1[4] = Utils.ToString((double) c);
            textArray1[5] = " ";
            textArray1[6] = Utils.ToString((double) d);
            textArray1[7] = " ";
            textArray1[8] = Utils.ToString((double) x);
            textArray1[9] = " ";
            textArray1[10] = Utils.ToString((double) y);
            textArray1[11] = " Tm";
            this.Stream.SetStringLine(string.Concat(textArray1));
        }

        public void SetTextRise(int rise)
        {
            this.Stream.SetStringLine(Convert.ToString(rise) + " Ts");
        }

        public override void SetWordSpacing(float wordSpace)
        {
            base.SetWordSpacing(wordSpace);
            this.Stream.SetStringLine(Utils.ToString((double) wordSpace) + " Tw");
        }

        public void Shading(PdfShading shading)
        {
            this.Stream.SetStringLine("/" + shading.Name + " sh");
        }

        public void ShowText(TextRun run)
        {
            this.ShowText(run, " Tj");
        }

        public void ShowText(string text)
        {
            TextRun run = new TextRun();
            run.Text = text;
            this.ShowText(run);
        }

        private void ShowText(TextRun textRun, string controlString)
        {
            base.CurrentFont.CharCache.RegisterTextRun(textRun);
            this.Stream.SetString(base.CurrentFont.ProcessText(textRun));
            this.Stream.SetStringLine(controlString);
        }

        public void ShowTextOnNextLine(string text)
        {
            TextRun textRun = new TextRun();
            textRun.Text = text;
            this.ShowText(textRun, " '");
        }

        public void Stroke()
        {
            this.Stream.SetStringLine("S");
        }

        private PdfStream Stream =>
            this.stream;
    }
}

