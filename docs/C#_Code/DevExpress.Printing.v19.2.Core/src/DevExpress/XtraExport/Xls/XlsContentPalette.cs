namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    internal class XlsContentPalette : XlsContentBase
    {
        private readonly XlsPalette palette;

        public XlsContentPalette(XlsPalette palette)
        {
            Guard.ArgumentNotNull(palette, "palette");
            this.palette = palette;
        }

        private int ArgbToValue(byte red, byte green, byte blue) => 
            (red + (green << 8)) + (blue << 0x10);

        public override int GetSize() => 
            0xe2;

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            int num = 0x38;
            writer.Write((short) num);
            for (int i = 0; i < num; i++)
            {
                Color color = this.palette[i + 8];
                int num3 = this.ArgbToValue(color.R, color.G, color.B);
                writer.Write(num3);
            }
        }
    }
}

