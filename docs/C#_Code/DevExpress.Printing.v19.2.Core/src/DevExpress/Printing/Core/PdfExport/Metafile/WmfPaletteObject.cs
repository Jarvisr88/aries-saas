namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class WmfPaletteObject
    {
        public void Read(MetaReader reader)
        {
            this.Start = reader.ReadUInt16();
            ushort num = reader.ReadUInt16();
            this.PaletteEntries = new WmfPaletteEntry[num];
            for (int i = 0; i < num; i++)
            {
                WmfPaletteEntry entry1 = new WmfPaletteEntry();
                entry1.Values = reader.ReadByte();
                entry1.Color = reader.ReadColorBGR(false);
                this.PaletteEntries[i] = entry1;
            }
        }

        public int Start { get; set; }

        public WmfPaletteEntry[] PaletteEntries { get; set; }
    }
}

