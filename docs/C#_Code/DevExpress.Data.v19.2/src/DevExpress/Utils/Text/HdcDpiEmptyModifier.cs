namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;

    public class HdcDpiEmptyModifier : HdcDpiModifier
    {
        public HdcDpiEmptyModifier(Graphics gr, Size viewPort) : base(gr, viewPort, (int) GraphicsDpi.Pixel)
        {
        }

        protected override void ApplyHDCDpi()
        {
        }

        protected override void RestoreHDCDpi()
        {
        }
    }
}

