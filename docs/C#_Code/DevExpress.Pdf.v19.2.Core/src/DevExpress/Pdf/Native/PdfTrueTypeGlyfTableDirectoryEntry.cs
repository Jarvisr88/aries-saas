namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfTrueTypeGlyfTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "glyf";
        private readonly IDictionary<int, PdfGlyphDescription> glyphs;
        private IDictionary<int, PdfGlyphDescription> subsetGlyphs;
        private int[] glyphOffsets;
        private bool shouldWrite;

        public PdfTrueTypeGlyfTableDirectoryEntry(byte[] tableData) : base("glyf", tableData)
        {
            this.glyphs = new Dictionary<int, PdfGlyphDescription>();
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream stream = base.CreateNewStream();
                foreach (KeyValuePair<int, PdfGlyphDescription> pair in this.subsetGlyphs)
                {
                    stream.Position = this.glyphOffsets[pair.Key];
                    pair.Value.Write(stream);
                    int position = (int) stream.Position;
                    int num2 = Pad4(position);
                    for (int i = position; i < num2; i++)
                    {
                        stream.WriteByte(0);
                    }
                }
            }
        }

        private int[] CalculateOffsets(int glyphCount)
        {
            // Invalid method body.
        }

        public void CreateSubset(PdfFontFile fontFile, ICollection<int> glyphIndices)
        {
            this.subsetGlyphs = new SortedDictionary<int, PdfGlyphDescription>();
            Queue<int> queue = new Queue<int>(glyphIndices);
            while (queue.Count > 0)
            {
                PdfGlyphDescription description;
                int key = queue.Dequeue();
                if (this.glyphs.TryGetValue(key, out description))
                {
                    foreach (int num2 in description.AdditionalGlyphIndices)
                    {
                        if (!this.subsetGlyphs.ContainsKey(num2))
                        {
                            queue.Enqueue(num2);
                        }
                    }
                    this.subsetGlyphs[key] = description;
                }
            }
            this.glyphOffsets = this.CalculateOffsets(this.glyphOffsets.Length - 1);
            PdfTrueTypeLocaTableDirectoryEntry loca = fontFile.Loca;
            if (loca != null)
            {
                loca.GlyphOffsets = this.glyphOffsets;
            }
            this.shouldWrite = true;
        }

        private static int Pad4(int val)
        {
            int num = val % 4;
            return ((num == 0) ? val : ((val + 4) - num));
        }

        public void ReadGlyphs(PdfFontFile fontFile)
        {
            this.glyphs.Clear();
            PdfTrueTypeLocaTableDirectoryEntry loca = fontFile.Loca;
            if (loca != null)
            {
                PdfBinaryStream tableStream = base.TableStream;
                tableStream.Position = 0L;
                int length = (int) tableStream.Length;
                this.glyphOffsets = loca.GlyphOffsets;
                int glyphCount = this.glyphOffsets.Length - 1;
                List<int> list = new List<int>(this.glyphOffsets);
                list.Sort();
                Dictionary<int, PdfGlyphDescription> dictionary = new Dictionary<int, PdfGlyphDescription>();
                int num3 = list[0];
                if (list[0] != this.glyphOffsets[0])
                {
                    this.shouldWrite = true;
                }
                int num5 = 0;
                int index = 1;
                while (true)
                {
                    if (num5 >= glyphCount)
                    {
                        int num4 = this.glyphOffsets[0];
                        int num10 = 0;
                        int num11 = 1;
                        while (true)
                        {
                            PdfGlyphDescription description2;
                            if (num11 > glyphCount)
                            {
                                this.subsetGlyphs = this.glyphs;
                                this.glyphOffsets = this.CalculateOffsets(glyphCount);
                                if (this.shouldWrite)
                                {
                                    loca.GlyphOffsets = this.glyphOffsets;
                                    PdfFontMaxpTableDirectoryEntry maxp = fontFile.Maxp;
                                    if (maxp != null)
                                    {
                                        maxp.NumGlyphs = (short) glyphCount;
                                    }
                                }
                                break;
                            }
                            int num12 = this.glyphOffsets[num11];
                            if (((num12 - num4) != 0) && dictionary.TryGetValue(this.glyphOffsets[num10], out description2))
                            {
                                this.glyphs.Add(num10, description2);
                            }
                            num4 = num12;
                            num10++;
                            num11++;
                        }
                        break;
                    }
                    int num7 = num3;
                    int num8 = this.glyphOffsets[index];
                    num3 = Math.Min(list[index++], length);
                    if (num3 != num8)
                    {
                        this.shouldWrite = true;
                    }
                    int glyphDataSize = num3 - num7;
                    if (glyphDataSize < 10)
                    {
                        if (glyphDataSize != 0)
                        {
                            num3 = num7;
                            this.shouldWrite = true;
                        }
                    }
                    else
                    {
                        tableStream.Position = num7;
                        PdfGlyphDescription description = new PdfGlyphDescription(tableStream, glyphDataSize, glyphCount);
                        if (description.IsEmpty)
                        {
                            this.shouldWrite = true;
                        }
                        else
                        {
                            dictionary[num7] = description;
                        }
                    }
                    num5++;
                }
            }
        }

        public IDictionary<int, PdfGlyphDescription> Glyphs =>
            this.glyphs;
    }
}

