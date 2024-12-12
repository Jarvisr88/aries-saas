namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfHashtable
    {
        private Dictionary<Color, string> strokeColors = new Dictionary<Color, string>();
        private Dictionary<Color, string> fillColors = new Dictionary<Color, string>();
        private Dictionary<RectangleF, string> rectangles = new Dictionary<RectangleF, string>();
        private Dictionary<SizeF, string> sizes = new Dictionary<SizeF, string>();

        public void Clear()
        {
            this.strokeColors.Clear();
            this.fillColors.Clear();
            this.rectangles.Clear();
            this.sizes.Clear();
        }

        private string ColorToString(Color color)
        {
            Color color2 = DXColor.Blend(color, DXColor.White);
            string[] textArray1 = new string[] { Utils.ToString(Math.Round((double) (((float) color2.R) / 255f), 3)), " ", Utils.ToString(Math.Round((double) (((float) color2.G) / 255f), 3)), " ", Utils.ToString(Math.Round((double) (((float) color2.B) / 255f), 3)) };
            return string.Concat(textArray1);
        }

        public string GetRectangle(float x, float y, float width, float height)
        {
            string str;
            RectangleF key = new RectangleF(x, y, width, height);
            if (!this.rectangles.TryGetValue(key, out str))
            {
                string[] textArray1 = new string[] { Utils.ToString((double) x), " ", Utils.ToString((double) y), " ", Utils.ToString((double) width), " ", Utils.ToString((double) height), " re" };
                str = string.Concat(textArray1);
                this.rectangles.Add(key, str);
            }
            return str;
        }

        public string GetRGBFillColor(Color color)
        {
            string str;
            if (!this.fillColors.TryGetValue(color, out str))
            {
                str = this.ColorToString(color) + " rg";
                this.fillColors.Add(color, str);
            }
            return str;
        }

        public string GetRGBStrokeColor(Color color)
        {
            string str;
            if (!this.strokeColors.TryGetValue(color, out str))
            {
                str = this.ColorToString(color) + " RG";
                this.strokeColors.Add(color, str);
            }
            return str;
        }

        public string GetSize(float tx, float ty)
        {
            string str;
            SizeF key = new SizeF(tx, ty);
            if (!this.sizes.TryGetValue(key, out str))
            {
                str = Utils.ToString((double) tx) + " " + Utils.ToString((double) ty) + " Td";
                this.sizes.Add(key, str);
            }
            return str;
        }
    }
}

