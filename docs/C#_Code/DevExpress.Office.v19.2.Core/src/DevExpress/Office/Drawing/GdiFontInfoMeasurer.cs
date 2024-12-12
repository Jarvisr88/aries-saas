namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Office.PInvoke;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class GdiFontInfoMeasurer : GdiPlusFontInfoMeasurer
    {
        private const int arraySize = 10;
        private float dpi;
        private System.Drawing.Graphics graphics;

        public GdiFontInfoMeasurer(DocumentLayoutUnitConverter unitConverter) : base(unitConverter)
        {
            this.dpi = GraphicsDpi.Pixel;
            this.graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
        }

        protected virtual float CharacterWidthToLayoutUnitsF(float width, float dpi) => 
            width;

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (this.graphics != null))
                {
                    this.graphics.Dispose();
                    this.graphics = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override float MeasureCharacterWidthF(char character, FontInfo fontInfo)
        {
            float width = 0f;
            System.Drawing.Graphics measureGraphics = base.MeasureGraphics;
            lock (measureGraphics)
            {
                IntPtr hdc = base.MeasureGraphics.GetHdc();
                try
                {
                    IntPtr hGdiObj = Win32.SelectObject(hdc, this.ObtainFontHandle(fontInfo));
                    try
                    {
                        Win32.ABCFLOAT[] result = new Win32.ABCFLOAT[1];
                        if (Win32.GetCharABCWidthsFloat(hdc, character, character, result))
                        {
                            width = result[0].GetWidth();
                        }
                    }
                    finally
                    {
                        IntPtr ptr2;
                        Win32.SelectObject(hdc, hGdiObj);
                        this.ReleaseFontHandle(ptr2);
                    }
                }
                finally
                {
                    base.MeasureGraphics.ReleaseHdc(hdc);
                }
            }
            return this.CharacterWidthToLayoutUnitsF(width, GraphicsDpi.Pixel);
        }

        public override float MeasureMaxDigitWidthF(FontInfo fontInfo)
        {
            float width = 0f;
            System.Drawing.Graphics graphics = this.graphics;
            lock (graphics)
            {
                IntPtr hdc = this.graphics.GetHdc();
                try
                {
                    IntPtr hGdiObj = Win32.SelectObject(hdc, this.ObtainFontHandle(fontInfo));
                    try
                    {
                        Win32.ABCFLOAT[] result = new Win32.ABCFLOAT[10];
                        if (Win32.GetCharABCWidthsFloat(hdc, '0', '9', result))
                        {
                            width = result[0].GetWidth();
                            for (int i = 1; i < 10; i++)
                            {
                                width = Math.Max(width, result[i].GetWidth());
                            }
                        }
                    }
                    finally
                    {
                        IntPtr ptr2;
                        Win32.SelectObject(hdc, hGdiObj);
                        this.ReleaseFontHandle(ptr2);
                    }
                }
                finally
                {
                    this.graphics.ReleaseHdc(hdc);
                }
            }
            return ((width <= 0f) ? base.MeasureMaxDigitWidthF(fontInfo) : this.CharacterWidthToLayoutUnitsF(width, this.dpi));
        }

        protected virtual IntPtr ObtainFontHandle(FontInfo fontInfo) => 
            ((GdiFontInfo) fontInfo).GdiFontHandle;

        protected virtual void ReleaseFontHandle(IntPtr hFont)
        {
        }

        protected System.Drawing.Graphics Graphics =>
            this.graphics;
    }
}

