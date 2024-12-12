namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils.StructuredStorage.Reader;
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class RangeHelper : IDisposable
    {
        private Stream stream;
        private const int BeginOfSubstreamCode = 0x809;
        private const int OleObjectRangeCode = 0xde;
        private const string Link = "Link";
        private const string LinkSource = "Link Source";

        public RangeHelper(Stream stream)
        {
            this.stream = this.CopyStream(stream);
        }

        private Stream CopyStream(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            long position = this.SaveAndResetStreamPosition(stream);
            MemoryStream destination = new MemoryStream();
            stream.CopyTo(destination);
            destination.Position = 0L;
            this.RestoreStreamPosition(stream, position);
            return destination;
        }

        public void Dispose()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
            this.stream = null;
        }

        private bool FindBeginOfSubstream(BinaryReader reader)
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                long num = reader.BaseStream.Length - reader.BaseStream.Position;
                if (num < 4L)
                {
                    return false;
                }
                if (reader.ReadUInt16() == 0x809)
                {
                    ushort num3 = reader.ReadUInt16();
                    reader.BaseStream.Position += num3;
                    return true;
                }
            }
            return false;
        }

        private bool FindOleObjectRange(BinaryReader reader)
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                long num = reader.BaseStream.Length - reader.BaseStream.Position;
                if (num < 4L)
                {
                    return false;
                }
                if (reader.ReadInt16() == 0xde)
                {
                    return true;
                }
                ushort num3 = reader.ReadUInt16();
                reader.BaseStream.Position += num3;
            }
            return false;
        }

        private XlCellRange GetBiff8Range()
        {
            XlCellRange range = null;
            StructuredStorageReader reader = null;
            try
            {
                using (Stream stream = new StructuredStorageReader(this.stream).GetStream("Workbook"))
                {
                    using (BinaryReader reader2 = new BinaryReader(stream))
                    {
                        if (this.FindBeginOfSubstream(reader2) && this.FindOleObjectRange(reader2))
                        {
                            range = this.ReadRangeData(reader2);
                        }
                    }
                }
            }
            catch
            {
                range = null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            return range;
        }

        private XlCellRange GetColumnRange(string start, string end)
        {
            XlCellPosition topLeft = new XlCellPosition(int.Parse(start.Remove(0, 1)) - 1, -1);
            return new XlCellRange(topLeft, new XlCellPosition(int.Parse(end.Remove(0, 1)) - 1, -1));
        }

        private XlCellRange GetFullRange(string start, string end)
        {
            Match match = new Regex(@"\d+").Match(start);
            Match match2 = new Regex(@"\d+").Match(end);
            XlCellPosition topLeft = new XlCellPosition(int.Parse(match.NextMatch().Value) - 1, int.Parse(match.Value) - 1);
            return new XlCellRange(topLeft, new XlCellPosition(int.Parse(match2.NextMatch().Value) - 1, int.Parse(match2.Value) - 1));
        }

        private XlCellRange GetLinkRange()
        {
            XlCellRange columnRange = null;
            MemoryStream data = null;
            XlCellRange range2;
            if (Clipboard.ContainsData("Link"))
            {
                data = Clipboard.GetData("Link") as MemoryStream;
            }
            else if (Clipboard.ContainsData("Link Source"))
            {
                data = Clipboard.GetData("Link Source") as MemoryStream;
            }
            if (data == null)
            {
                return columnRange;
            }
            using (StreamReader reader = new StreamReader(data))
            {
                string[] separator = new string[] { "\0" };
                string[] strArray = reader.ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if ((strArray.Length != 3) || (strArray[0] != "Excel"))
                {
                    range2 = null;
                }
                else
                {
                    char[] chArray1 = new char[] { ':' };
                    string[] strArray2 = strArray[2].Split(chArray1);
                    if (strArray2.Length == 1)
                    {
                        string start = strArray2[0];
                        if ((start[0] == 'C') || (start[0] == 'S'))
                        {
                            columnRange = this.GetColumnRange(start, start);
                        }
                    }
                    else if (strArray2.Length == 2)
                    {
                        string input = strArray2[0];
                        string end = strArray2[1];
                        if (new Regex(@"\D\d+\D\d+").Match(input).Success)
                        {
                            columnRange = this.GetFullRange(input, end);
                        }
                        else if ((input[0] == 'C') || (input[0] == 'S'))
                        {
                            columnRange = this.GetColumnRange(input, end);
                        }
                    }
                    return columnRange;
                }
            }
            return range2;
        }

        public XlCellRange GetRange()
        {
            XlCellRange linkRange = null;
            linkRange = this.GetLinkRange();
            if ((linkRange == null) && (this.stream != null))
            {
                linkRange = this.GetBiff8Range();
            }
            return linkRange;
        }

        private XlCellRange ReadRangeData(BinaryReader reader)
        {
            ushort num = reader.ReadUInt16();
            reader.ReadUInt16();
            byte column = reader.ReadByte();
            return new XlCellRange(new XlCellPosition(reader.ReadByte(), reader.ReadUInt16()), new XlCellPosition(column, reader.ReadUInt16()));
        }

        private void RestoreStreamPosition(Stream stream, long position)
        {
            stream.Position = position;
        }

        private long SaveAndResetStreamPosition(Stream stream)
        {
            long position = stream.Position;
            stream.Position = 0L;
            return position;
        }
    }
}

