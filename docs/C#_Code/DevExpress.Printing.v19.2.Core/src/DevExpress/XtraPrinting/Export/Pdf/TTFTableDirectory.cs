namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class TTFTableDirectory
    {
        private TTFFile owner;
        private byte[] sfntVersion;
        private ushort numTables;
        private ushort searchRange;
        private ushort entrySelector;
        private ushort rangeShift;
        private List<TTFTableDirectoryEntry> entries = new List<TTFTableDirectoryEntry>();

        public TTFTableDirectory(TTFFile owner)
        {
            this.owner = owner;
        }

        public void Initialize(TTFTableDirectory pattern)
        {
            this.sfntVersion = new byte[pattern.sfntVersion.Length];
            pattern.sfntVersion.CopyTo(this.sfntVersion, 0);
            this.numTables = (ushort) this.Count;
            double y = Math.Floor(Math.Log((double) this.numTables, 2.0));
            double num2 = Math.Pow(2.0, y);
            this.searchRange = (ushort) (num2 * 16.0);
            this.entrySelector = (ushort) y;
            this.rangeShift = (ushort) ((this.numTables * 0x10) - this.searchRange);
        }

        public void Read(TTFStream ttfStream)
        {
            ttfStream.Seek((int) this.owner.Offset);
            this.sfntVersion = ttfStream.ReadBytes(4);
            this.numTables = ttfStream.ReadUShort();
            this.searchRange = ttfStream.ReadUShort();
            this.entrySelector = ttfStream.ReadUShort();
            this.rangeShift = ttfStream.ReadUShort();
            this.ReadEntries(ttfStream);
        }

        private void ReadEntries(TTFStream ttfStream)
        {
            this.entries.Clear();
            for (int i = 0; i < this.NumTables; i++)
            {
                TTFTableDirectoryEntry item = new TTFTableDirectoryEntry();
                item.Read(ttfStream);
                this.entries.Add(item);
            }
        }

        public void Register(TTFTable table)
        {
            TTFTableDirectoryEntry item = new TTFTableDirectoryEntry();
            item.Initialize(table);
            this.entries.Add(item);
        }

        public void Write(TTFStream ttfStream)
        {
            ttfStream.Seek(0);
            ttfStream.WriteBytes(this.sfntVersion);
            ttfStream.WriteUShort(this.numTables);
            ttfStream.WriteUShort(this.searchRange);
            ttfStream.WriteUShort(this.entrySelector);
            ttfStream.WriteUShort(this.rangeShift);
            this.WriteEntries(ttfStream);
        }

        public void WriteCheckSum(TTFStream ttfStream)
        {
            ttfStream.Seek(0);
            ttfStream.Move(SizeOf);
            for (int i = 0; i < this.Count; i++)
            {
                this[i].WriteCheckSum(ttfStream);
            }
        }

        private void WriteEntries(TTFStream ttfStream)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Write(ttfStream);
            }
        }

        public void WriteOffsets(TTFStream ttfStream)
        {
            ttfStream.Seek(0);
            ttfStream.Move(SizeOf);
            for (int i = 0; i < this.Count; i++)
            {
                this[i].WriteOffset(ttfStream);
            }
        }

        public static int SizeOf =>
            12;

        private int NumTables =>
            Convert.ToInt32(this.numTables);

        public TTFFile Owner =>
            this.owner;

        public int Count =>
            this.entries.Count;

        public TTFTableDirectoryEntry this[int index] =>
            this.entries[index];

        public TTFTableDirectoryEntry this[string tag]
        {
            get
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Tag == tag)
                    {
                        return this[i];
                    }
                }
                return null;
            }
        }
    }
}

