namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTrueTypeLocaTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "loca";
        private bool isShortFormat;
        private int[] glyphOffsets;
        private bool shouldWrite;

        public PdfTrueTypeLocaTableDirectoryEntry(byte[] tableData) : base("loca", tableData)
        {
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream stream = base.CreateNewStream();
                int length = this.glyphOffsets.Length;
                if (this.isShortFormat)
                {
                    for (int i = 0; i < length; i++)
                    {
                        stream.WriteShort((short) (this.glyphOffsets[i] / 2));
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        stream.WriteInt(this.glyphOffsets[i]);
                    }
                }
            }
        }

        public void ReadOffsets(PdfFontFile fontFile)
        {
            PdfFontHeadTableDirectoryEntry head = fontFile.Head;
            this.isShortFormat = (head != null) && (head.IndexToLocFormat == PdfIndexToLocFormat.Short);
            int num = this.isShortFormat ? (base.Length / 2) : (base.Length / 4);
            if (num <= 1)
            {
                this.glyphOffsets = new int[0];
            }
            else
            {
                PdfFontMaxpTableDirectoryEntry maxp = fontFile.Maxp;
                if (maxp != null)
                {
                    int num2 = maxp.NumGlyphs + 1;
                    if (num > num2)
                    {
                        num = num2;
                    }
                    else if (num < num2)
                    {
                        maxp.NumGlyphs = num - 1;
                    }
                }
                PdfBinaryStream tableStream = base.TableStream;
                tableStream.Position = 0L;
                this.glyphOffsets = new int[num];
                if (this.isShortFormat)
                {
                    for (int i = 0; i < num; i++)
                    {
                        this.glyphOffsets[i] = tableStream.ReadUshort() * 2;
                    }
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        this.glyphOffsets[i] = tableStream.ReadInt();
                    }
                }
            }
        }

        public int[] GlyphOffsets
        {
            get => 
                this.glyphOffsets;
            set
            {
                this.glyphOffsets = value;
                this.shouldWrite = true;
            }
        }
    }
}

