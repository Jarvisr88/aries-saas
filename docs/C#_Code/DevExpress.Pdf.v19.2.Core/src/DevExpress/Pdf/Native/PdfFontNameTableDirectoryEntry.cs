namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfFontNameTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "name";
        private const string nameFontSubfamily = "Regular";
        private const string nameVersion = "0.0";
        private readonly List<PdfFontNameRecord> namingTable;
        private string familyName;
        private string subFamilyName;
        private string macFamilyName;
        private string postScriptName;
        private bool shouldWrite;

        public PdfFontNameTableDirectoryEntry(byte[] tableData) : base("name", tableData)
        {
            this.namingTable = new List<PdfFontNameRecord>();
            PdfBinaryStream tableStream = base.TableStream;
            if (tableStream.Length > 6L)
            {
                short num = tableStream.ReadShort();
                short num2 = tableStream.ReadShort();
                short dataOffset = tableStream.ReadShort();
                for (int i = 0; i < num2; i++)
                {
                    this.namingTable.Add(new PdfFontNameRecord(tableStream, dataOffset));
                }
            }
        }

        public PdfFontNameTableDirectoryEntry(PdfFontCmapTableDirectoryEntry cmapEntry, string fontName) : base("name")
        {
            this.namingTable = new List<PdfFontNameRecord>();
            this.Create(cmapEntry, fontName);
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream stream = base.CreateNewStream();
                stream.WriteShort(0);
                short count = (short) this.namingTable.Count;
                stream.WriteShort(count);
                stream.WriteShort((short) (6 + (count * 12)));
                short num2 = 0;
                foreach (PdfFontNameRecord record in this.namingTable)
                {
                    stream.WriteShort((short) record.PlatformID);
                    stream.WriteShort((short) record.EncodingID);
                    stream.WriteShort((short) record.LanguageID);
                    stream.WriteShort((short) record.NameID);
                    byte[] nameBytes = record.NameBytes;
                    short num3 = (nameBytes == null) ? ((short) 0) : ((short) nameBytes.Length);
                    stream.WriteShort(num3);
                    stream.WriteShort(num2);
                    num2 = (short) (num2 + num3);
                }
                foreach (PdfFontNameRecord record2 in this.namingTable)
                {
                    if (record2.NameBytes != null)
                    {
                        stream.WriteArray(record2.NameBytes);
                    }
                }
            }
        }

        public bool ContainsFontFamilyName(string name)
        {
            bool flag;
            using (List<PdfFontNameRecord>.Enumerator enumerator = this.namingTable.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfFontNameRecord current = enumerator.Current;
                        if ((current.PlatformID != PdfFontPlatformID.Microsoft) || ((current.NameID != PdfFontNameID.FontFamily) || (current.Name != name)))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void Create(PdfFontCmapTableDirectoryEntry cmapEntry, string fontName)
        {
            Dictionary<PdfFontNameID, byte[]> dictionary = new Dictionary<PdfFontNameID, byte[]>();
            Encoding bigEndianUnicode = Encoding.BigEndianUnicode;
            byte[] bytes = bigEndianUnicode.GetBytes(fontName);
            dictionary.Add(PdfFontNameID.FontFamily, bytes);
            dictionary.Add(PdfFontNameID.FontSubfamily, bigEndianUnicode.GetBytes("Regular"));
            dictionary.Add(PdfFontNameID.FullFontName, bytes);
            dictionary.Add(PdfFontNameID.UniqueFontId, bytes);
            dictionary.Add(PdfFontNameID.Version, bigEndianUnicode.GetBytes("0.0"));
            dictionary.Add(PdfFontNameID.PostscriptName, bytes);
            this.namingTable.Clear();
            foreach (PdfFontCmapFormatEntry entry in cmapEntry.CMapTables)
            {
                PdfFontPlatformID platformId = entry.PlatformId;
                PdfFontEncodingID encodingId = entry.EncodingId;
                PdfFontLanguageID languageID = (platformId == PdfFontPlatformID.Microsoft) ? PdfFontLanguageID.EnglishUnitedStates : PdfFontLanguageID.English;
                foreach (KeyValuePair<PdfFontNameID, byte[]> pair in dictionary)
                {
                    this.namingTable.Add(new PdfFontNameRecord(platformId, languageID, pair.Key, encodingId, pair.Value));
                }
            }
            this.shouldWrite = true;
        }

        private string FindName(PdfFontPlatformID platform, PdfFontEncodingID encoding, PdfFontLanguageID language, PdfFontNameID id)
        {
            string name;
            using (List<PdfFontNameRecord>.Enumerator enumerator = this.namingTable.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfFontNameRecord current = enumerator.Current;
                        if ((current.PlatformID != platform) || ((current.EncodingID != encoding) || ((current.LanguageID != language) || (current.NameID != id))))
                        {
                            continue;
                        }
                        name = current.Name;
                    }
                    else
                    {
                        return string.Empty;
                    }
                    break;
                }
            }
            return name;
        }

        public string FamilyName
        {
            get
            {
                this.familyName ??= this.FindName(PdfFontPlatformID.Microsoft, PdfFontEncodingID.UGL, PdfFontLanguageID.EnglishUnitedStates, PdfFontNameID.FontFamily);
                return this.familyName;
            }
        }

        public string SubFamilyName
        {
            get
            {
                this.subFamilyName ??= this.FindName(PdfFontPlatformID.Microsoft, PdfFontEncodingID.UGL, PdfFontLanguageID.EnglishUnitedStates, PdfFontNameID.FontSubfamily);
                return this.subFamilyName;
            }
        }

        public string MacFamilyName
        {
            get
            {
                this.macFamilyName ??= this.FindName(PdfFontPlatformID.Macintosh, PdfFontEncodingID.Symbol, PdfFontLanguageID.English, PdfFontNameID.FontFamily);
                return this.macFamilyName;
            }
        }

        public string PostScriptName
        {
            get
            {
                this.postScriptName ??= this.FindName(PdfFontPlatformID.Macintosh, PdfFontEncodingID.Symbol, PdfFontLanguageID.English, PdfFontNameID.PostscriptName);
                return this.postScriptName;
            }
        }
    }
}

