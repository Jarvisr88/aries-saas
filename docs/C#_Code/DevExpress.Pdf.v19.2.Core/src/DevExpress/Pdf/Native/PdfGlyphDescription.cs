namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfGlyphDescription
    {
        internal const int HeaderSize = 10;
        private const ushort ARG_1_AND_2_ARE_WORDS = 1;
        private const ushort WE_HAVE_A_SCALE = 8;
        private const ushort MORE_COMPONENTS = 0x20;
        private const ushort WE_HAVE_AN_X_AND_Y_SCALE = 0x40;
        private const ushort WE_HAVE_A_TWO_BY_TWO = 0x80;
        private readonly short numberOfContours;
        private readonly byte[] data;
        private readonly List<int> glyphIndexList = new List<int>();
        private readonly bool isInvalid;
        private bool isBoundingBoxParsed;
        private short? xMin;
        private short? yMin;
        private short? xMax;
        private short? yMax;

        public PdfGlyphDescription(PdfBinaryStream stream, int glyphDataSize, int glyphCount)
        {
            long position = stream.Position;
            this.numberOfContours = stream.ReadShort();
            this.data = stream.ReadArray(glyphDataSize - 2);
            if (this.numberOfContours < 0)
            {
                stream.Position = position + 10;
                ushort num2 = 0;
                do
                {
                    num2 = (ushort) stream.ReadUshort();
                    int item = stream.ReadUshort();
                    if (item >= glyphCount)
                    {
                        this.isInvalid = true;
                    }
                    this.glyphIndexList.Add(item);
                    stream.Position = ((num2 & 1) == 0) ? (stream.Position + 2L) : (stream.Position + 4L);
                    if ((num2 & 8) != 0)
                    {
                        stream.Position += 2L;
                    }
                    else if ((num2 & 0x40) != 0)
                    {
                        stream.Position += 4L;
                    }
                    else if ((num2 & 0x80) != 0)
                    {
                        stream.Position += 8L;
                    }
                }
                while ((num2 & 0x20) != 0);
            }
        }

        private void ReadBoundingBox()
        {
            if (this.data.Length >= 8)
            {
                using (PdfBinaryStream stream = new PdfBinaryStream(this.data))
                {
                    this.xMin = new short?(stream.ReadShort());
                    this.yMin = new short?(stream.ReadShort());
                    this.xMax = new short?(stream.ReadShort());
                    this.yMax = new short?(stream.ReadShort());
                }
            }
            this.isBoundingBoxParsed = true;
        }

        public void Write(PdfBinaryStream stream)
        {
            stream.WriteShort(this.numberOfContours);
            stream.WriteArray(this.data);
        }

        public bool IsEmpty =>
            (this.numberOfContours == 0) || this.isInvalid;

        public int Size =>
            this.data.Length + 2;

        public IEnumerable<int> AdditionalGlyphIndices =>
            this.glyphIndexList;

        public short? XMin
        {
            get
            {
                if (!this.isBoundingBoxParsed)
                {
                    this.ReadBoundingBox();
                }
                return this.xMin;
            }
        }

        public short? YMin
        {
            get
            {
                if (!this.isBoundingBoxParsed)
                {
                    this.ReadBoundingBox();
                }
                return this.yMin;
            }
        }

        public short? XMax
        {
            get
            {
                if (!this.isBoundingBoxParsed)
                {
                    this.ReadBoundingBox();
                }
                return this.xMax;
            }
        }

        public short? YMax
        {
            get
            {
                if (!this.isBoundingBoxParsed)
                {
                    this.ReadBoundingBox();
                }
                return this.yMax;
            }
        }
    }
}

