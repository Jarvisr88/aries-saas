namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsSourceTableFeature
    {
        private int tableSource;
        private uint tableFlags;
        private XLUnicodeString tableName = new XLUnicodeString();
        private readonly List<TableColumnInfo> columns = new List<TableColumnInfo>();
        private bool abortRead;

        public void Read(BinaryReader reader)
        {
            this.abortRead = false;
            this.tableSource = reader.ReadInt32();
            reader.ReadBytes(4);
            this.HasHeaderRow = reader.ReadUInt32() != 0;
            this.HasTotalRow = reader.ReadUInt32() != 0;
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            reader.ReadBytes(2);
            reader.ReadBytes(2);
            this.tableFlags = reader.ReadUInt32();
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            reader.ReadBytes(0x10);
            this.tableName = XLUnicodeString.FromStream(reader);
            int num = reader.ReadUInt16();
            if ((this.tableFlags & 0x4000) != 0)
            {
                XLUnicodeString.FromStream(reader);
            }
            if ((this.tableFlags & 0x100000) != 0)
            {
                XLUnicodeString.FromStream(reader);
            }
            for (int i = 0; (i < num) && !this.abortRead; i++)
            {
                TableColumnInfo column = new TableColumnInfo();
                this.ReadFieldDataItem(reader, column);
                this.columns.Add(column);
            }
        }

        private void ReadDXFN12List(BinaryReader reader, int size, Action<XlNumberFormat> action)
        {
            long position = reader.BaseStream.Position;
            reader.ReadUInt16();
            ushort num2 = reader.ReadUInt16();
            ushort num3 = reader.ReadUInt16();
            if (Convert.ToBoolean((int) (num2 & 0x200)))
            {
                if (!Convert.ToBoolean((int) (num3 & 1)))
                {
                    reader.ReadBytes(1);
                    action(XlNumberFormat.FromId(reader.ReadByte()));
                }
                else if (reader.ReadUInt16() > 2)
                {
                    action(XLUnicodeString.FromStream(reader).Value);
                }
            }
            if (Convert.ToBoolean((int) (num2 & 0x400)))
            {
                reader.ReadBytes(1);
                byte[] buffer = reader.ReadBytes(0x3f);
                reader.ReadBytes(0x10);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(4);
                reader.ReadBytes(2);
            }
            if (Convert.ToBoolean((int) (num2 & 0x800)))
            {
                reader.ReadBytes(8);
            }
            if (Convert.ToBoolean((int) (num2 & 0x1000)))
            {
                reader.ReadBytes(8);
            }
            if (Convert.ToBoolean((int) (num2 & 0x2000)))
            {
                reader.ReadBytes(4);
            }
            if (Convert.ToBoolean((int) (num2 & 0x4000)))
            {
                reader.ReadBytes(2);
            }
            long num4 = reader.BaseStream.Position;
            int count = size - ((int) (num4 - position));
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        private void ReadFieldDataItem(BinaryReader reader, TableColumnInfo column)
        {
            reader.ReadBytes(4);
            int dataProviderType = reader.ReadInt32();
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            uint num2 = reader.ReadUInt32();
            reader.ReadBytes(4);
            int num3 = reader.ReadInt32();
            uint num4 = reader.ReadUInt32();
            reader.ReadBytes(4);
            XLUnicodeString.FromStream(reader);
            if ((this.tableFlags & 0x200) == 0)
            {
                column.Name = XLUnicodeString.FromStream(reader).Value;
            }
            if (num2 > 0)
            {
                this.ReadDXFN12List(reader, (int) num2, format => column.TotalRowNumberFormat = format);
            }
            if (num4 > 0)
            {
                this.ReadDXFN12List(reader, (int) num4, format => column.NumberFormat = format);
            }
            if ((this.tableFlags & 2) != 0)
            {
                uint num5 = reader.ReadUInt32();
                reader.ReadBytes(2);
                if (num5 > 0)
                {
                    reader.ReadBytes((int) num5);
                }
            }
            if ((num3 & 4) != 0)
            {
                ushort num6 = reader.ReadUInt16();
                for (int i = 0; i < num6; i++)
                {
                    reader.ReadBytes(4);
                    reader.ReadBytes(4);
                    XLUnicodeString.FromStream(reader);
                }
            }
            if ((num3 & 8) != 0)
            {
                ushort count = reader.ReadUInt16();
                if (count > 0)
                {
                    reader.ReadBytes(count);
                }
            }
            if ((num3 & 0x80) != 0)
            {
                ushort count = reader.ReadUInt16();
                if (count > 0)
                {
                    byte[] totalFormula = reader.ReadBytes(count);
                    if ((num3 & 0x100) != 0)
                    {
                        this.ReadTotalFormulaExtra(reader, totalFormula);
                    }
                    if (this.abortRead)
                    {
                        return;
                    }
                }
            }
            if ((num3 & 0x400) != 0)
            {
                XLUnicodeString.FromStream(reader);
            }
            if (this.tableSource == 1)
            {
                this.ReadWSSInfo(reader, dataProviderType);
            }
            if (this.tableSource == 3)
            {
                reader.ReadBytes(4);
            }
            if (!this.HasHeaderRow && ((this.tableFlags & 0x200) == 0))
            {
                int count = reader.ReadInt32();
                if (count > 0)
                {
                    reader.ReadBytes(count);
                }
                if ((num3 & 0x200) != 0)
                {
                    XLUnicodeString.FromStream(reader);
                }
            }
        }

        private void ReadTotalFormulaExtra(BinaryReader reader, byte[] totalFormula)
        {
            this.abortRead = true;
        }

        private void ReadWSSInfo(BinaryReader reader, int dataProviderType)
        {
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            reader.ReadBytes(4);
            bool flag = (reader.ReadUInt32() & 0x40) != 0;
            switch (dataProviderType)
            {
                case 1:
                case 8:
                case 11:
                    XLUnicodeString.FromStream(reader);
                    break;

                case 2:
                case 4:
                case 6:
                    reader.ReadBytes(8);
                    break;

                case 3:
                    reader.ReadBytes(4);
                    break;

                default:
                    break;
            }
            if (flag)
            {
                XLUnicodeString.FromStream(reader);
            }
            reader.ReadBytes(4);
        }

        public bool HasHeaderRow { get; private set; }

        public bool HasTotalRow { get; private set; }

        public string TableName =>
            this.tableName.Value;

        public IList<TableColumnInfo> Columns =>
            this.columns;
    }
}

