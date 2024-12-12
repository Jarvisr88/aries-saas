namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XfPropColor : XfPropBase
    {
        public XfPropColor(short typeCode, XlColor color) : base(typeCode)
        {
            this.Color = color;
        }

        protected override int GetSizeCore() => 
            8;

        protected override void WriteCore(BinaryWriter writer)
        {
            byte num = 4;
            byte colorIndex = 0;
            if (this.Color.ColorType == XlColorType.Auto)
            {
                num = 0;
            }
            else if (this.Color.ColorType == XlColorType.Indexed)
            {
                num = 1;
                colorIndex = (byte) this.Color.ColorIndex;
            }
            else if (this.Color.ColorType == XlColorType.Rgb)
            {
                num = 2;
            }
            else if (this.Color.ColorType == XlColorType.Theme)
            {
                num = 3;
                colorIndex = (byte) this.Color.ThemeColor;
            }
            byte num3 = (byte) ((num << 1) | 1);
            writer.Write(num3);
            writer.Write(colorIndex);
            writer.Write((short) (this.Color.Tint * 32767.0));
            System.Drawing.Color rgb = this.Color.Rgb;
            writer.Write(rgb.R);
            writer.Write(rgb.G);
            writer.Write(rgb.B);
            writer.Write((byte) 0xff);
        }

        public XlColor Color { get; private set; }
    }
}

