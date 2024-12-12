namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfFontCmapTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "cmap";
        private readonly short version;
        private readonly List<PdfFontCmapFormatEntry> cMapTables;
        private bool shouldWrite;

        public PdfFontCmapTableDirectoryEntry(IDictionary<short, short> charset) : base("cmap")
        {
            this.cMapTables = new List<PdfFontCmapFormatEntry>();
            this.cMapTables.Add(new PdfFontCmapSegmentMappingFormatEntry(charset));
            this.shouldWrite = true;
        }

        public PdfFontCmapTableDirectoryEntry(byte[] tableData) : base("cmap", tableData)
        {
            this.cMapTables = new List<PdfFontCmapFormatEntry>();
            PdfBinaryStream tableStream = base.TableStream;
            this.version = tableStream.ReadShort();
            short capacity = tableStream.ReadShort();
            this.cMapTables = new List<PdfFontCmapFormatEntry>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                PdfFontPlatformID platformId = (PdfFontPlatformID) tableStream.ReadShort();
                PdfFontEncodingID encodingId = (PdfFontEncodingID) tableStream.ReadShort();
                int num3 = tableStream.ReadInt();
                long position = tableStream.Position;
                tableStream.Position = num3;
                this.cMapTables.Add(PdfFontCmapFormatEntry.CreateEntry(platformId, encodingId, (PdfFontCmapFormatID) tableStream.ReadShort(), tableStream));
                tableStream.Position = position;
            }
        }

        public PdfFontCmapTableDirectoryEntry(PdfFontCmapSegmentMappingFormatEntry segmentMappingFormatEntry) : base("cmap")
        {
            this.cMapTables = new List<PdfFontCmapFormatEntry>();
            this.cMapTables.Add(segmentMappingFormatEntry);
            this.shouldWrite = true;
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream tableStream = base.CreateNewStream();
                tableStream.WriteShort(this.version);
                short count = (short) this.cMapTables.Count;
                tableStream.WriteShort(count);
                int num2 = 4 + (count * 8);
                foreach (PdfFontCmapFormatEntry entry in this.cMapTables)
                {
                    tableStream.WriteShort((short) entry.PlatformId);
                    tableStream.WriteShort((short) entry.EncodingId);
                    tableStream.WriteInt(num2);
                    num2 += entry.Length;
                }
                foreach (PdfFontCmapFormatEntry entry2 in this.cMapTables)
                {
                    entry2.Write(tableStream);
                }
            }
        }

        public PdfFontCmapSegmentMappingFormatEntry Validate(bool skipEncodingValidation, bool isSymbolic)
        {
            PdfFontCmapSegmentMappingFormatEntry entry8;
            PdfFontEncodingID encodingID = isSymbolic ? PdfFontEncodingID.Symbol : PdfFontEncodingID.UGL;
            PdfFontCmapTrimmedMappingFormatEntry formatEntry = null;
            PdfFontCmapByteEncodingFormatEntry entry3 = null;
            PdfFontCmapSegmentMappingFormatEntry entry4 = null;
            using (List<PdfFontCmapFormatEntry>.Enumerator enumerator = this.cMapTables.GetEnumerator())
            {
                while (true)
                {
                    PdfFontCmapSegmentMappingFormatEntry entry;
                    if (enumerator.MoveNext())
                    {
                        PdfFontCmapFormatEntry current = enumerator.Current;
                        entry = current as PdfFontCmapSegmentMappingFormatEntry;
                        if (entry != null)
                        {
                            if ((entry.PlatformId == PdfFontPlatformID.Microsoft) && (skipEncodingValidation || (entry.EncodingId == encodingID)))
                            {
                                this.shouldWrite = entry.Validate();
                                entry8 = entry;
                                break;
                            }
                            entry4 = entry;
                        }
                        PdfFontCmapTrimmedMappingFormatEntry entry6 = current as PdfFontCmapTrimmedMappingFormatEntry;
                        if (entry6 != null)
                        {
                            formatEntry = entry6;
                        }
                        PdfFontCmapByteEncodingFormatEntry entry7 = current as PdfFontCmapByteEncodingFormatEntry;
                        if (entry7 != null)
                        {
                            entry3 = entry7;
                        }
                        continue;
                    }
                    if (formatEntry == null)
                    {
                        entry = (entry3 == null) ? ((entry4 == null) ? new PdfFontCmapSegmentMappingFormatEntry(encodingID) : new PdfFontCmapSegmentMappingFormatEntry(encodingID, entry4)) : ((!isSymbolic || (entry4 == null)) ? new PdfFontCmapSegmentMappingFormatEntry(encodingID, entry3) : new PdfFontCmapSegmentMappingFormatEntry(encodingID, entry4));
                    }
                    else
                    {
                        if (!isSymbolic && (formatEntry.EncodingId == PdfFontEncodingID.Symbol))
                        {
                            if ((entry4 != null) && (entry4.PlatformId == PdfFontPlatformID.Microsoft))
                            {
                                return entry4;
                            }
                            encodingID = PdfFontEncodingID.Symbol;
                        }
                        entry = new PdfFontCmapSegmentMappingFormatEntry(encodingID, formatEntry);
                    }
                    this.cMapTables.Clear();
                    this.cMapTables.Add(entry);
                    this.shouldWrite = true;
                    return entry;
                }
            }
            return entry8;
        }

        public List<PdfFontCmapFormatEntry> CMapTables =>
            this.cMapTables;

        public bool ShouldWrite =>
            this.shouldWrite;
    }
}

