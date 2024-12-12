namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.IO;

    public class XfPropFullColor : XfPropColor
    {
        public XfPropFullColor(short typeCode, XlColor color) : base(typeCode, color)
        {
        }

        protected override int GetSizeCore() => 
            0x10;

        protected override void WriteCore(BinaryWriter writer)
        {
            ushort num = 4;
            if (base.Color.ColorType == XlColorType.Auto)
            {
                num = 0;
            }
            if (base.Color.ColorType == XlColorType.Indexed)
            {
                num = 1;
            }
            else if (base.Color.ColorType == XlColorType.Rgb)
            {
                num = 2;
            }
            else if (base.Color.ColorType == XlColorType.Theme)
            {
                num = 3;
            }
            writer.Write(num);
            writer.Write((short) (base.Color.Tint * 32767.0));
            switch (num)
            {
                case 1:
                    writer.Write(base.Color.ColorIndex);
                    break;

                case 2:
                {
                    Color rgb = base.Color.Rgb;
                    writer.Write(rgb.R);
                    writer.Write(rgb.G);
                    writer.Write(rgb.B);
                    writer.Write((byte) 0xff);
                    break;
                }
                case 3:
                    writer.Write((int) base.Color.ThemeColor);
                    break;

                default:
                    writer.Write(0);
                    break;
            }
            writer.Write((long) 0L);
        }
    }
}

