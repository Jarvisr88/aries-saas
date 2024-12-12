namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct JPXSize
    {
        private readonly int gridWidth;
        private readonly int gridHeight;
        private readonly int tileWidth;
        private readonly int tileHeight;
        private readonly int gridHorizontalOffset;
        private readonly int gridVerticalOffset;
        private readonly int tileHorizontalOffset;
        private readonly int tileVerticalOffset;
        private readonly JPXComponent[] components;
        public int GridWidth =>
            this.gridWidth;
        public int GridHeight =>
            this.gridHeight;
        public int TileWidth =>
            this.tileWidth;
        public int TileHeight =>
            this.tileHeight;
        public int GridHorizontalOffset =>
            this.gridHorizontalOffset;
        public int GridVerticalOffset =>
            this.gridVerticalOffset;
        public int TileHorizontalOffset =>
            this.tileHorizontalOffset;
        public int TileVerticalOffset =>
            this.tileVerticalOffset;
        public JPXComponent[] Components =>
            this.components;
        public JPXSize(PdfBigEndianStreamReader reader)
        {
            this.gridWidth = reader.ReadInt32();
            this.gridHeight = reader.ReadInt32();
            this.gridHorizontalOffset = reader.ReadInt32();
            this.gridVerticalOffset = reader.ReadInt32();
            this.tileWidth = reader.ReadInt32();
            this.tileHeight = reader.ReadInt32();
            this.tileHorizontalOffset = reader.ReadInt32();
            this.tileVerticalOffset = reader.ReadInt32();
            int num = reader.ReadInt16();
            this.components = new JPXComponent[num];
            for (int i = 0; i < num; i++)
            {
                this.components[i] = new JPXComponent(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            }
            if ((this.gridWidth < 1) || ((this.gridHeight < 1) || ((this.tileWidth < 1) || ((this.tileHeight < 1) || (num < 1)))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

