namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;

    public class XlsSourceCommandFeature11 : XlsSourceCommandBase
    {
        private static short[] typeCodes = new short[] { 0x875, 0x812 };
        private SharedFeatureType featureType;
        private XlsRef8 tableRef;
        private XlsSourceTableFeature tableFeature;

        protected override void CheckPosition(XlReader reader, long initialPosition, long expectedPosition)
        {
        }

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if ((this.featureType == SharedFeatureType.Table) && ((contentBuilder.CurrentSheet != null) && (this.tableFeature != null)))
            {
                XlCellRange range = XlCellRange.FromLTRB(this.tableRef.FirstColumnIndex, this.tableRef.FirstRowIndex, this.tableRef.LastColumnIndex, this.tableRef.LastRowIndex);
                range.SheetName = contentBuilder.CurrentSheet.Name;
                Table item = new Table(this.tableFeature.TableName, range, this.tableFeature.HasHeaderRow, this.tableFeature.HasTotalRow);
                item.Columns.AddRange(this.tableFeature.Columns);
                contentBuilder.InnerTables.Add(item);
                this.tableFeature = null;
            }
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            reader.Seek((long) 12, SeekOrigin.Current);
            this.featureType = (SharedFeatureType) reader.ReadUInt16();
            if (this.featureType != SharedFeatureType.Table)
            {
                this.SkipRestOfData(reader, 14);
            }
            else
            {
                reader.Seek(5L, SeekOrigin.Current);
                if (reader.ReadUInt16() != 1)
                {
                    this.featureType = SharedFeatureType.Unknown;
                    this.SkipRestOfData(reader, 0x15);
                }
                else
                {
                    reader.Seek(6L, SeekOrigin.Current);
                    this.tableRef = XlsRef8.FromStream(reader);
                    using (XlsCommandStream stream = new XlsCommandStream(reader, typeCodes, base.Size - 0x23))
                    {
                        using (BinaryReader reader2 = new BinaryReader(stream))
                        {
                            this.tableFeature = new XlsSourceTableFeature();
                            this.tableFeature.Read(reader2);
                        }
                    }
                }
            }
        }

        private void SkipRestOfData(XlReader reader, int count)
        {
            int num = base.Size - count;
            if (num > 0)
            {
                reader.Seek((long) num, SeekOrigin.Current);
            }
        }

        private enum SharedFeatureType
        {
            Unknown = 0,
            Protection = 2,
            IgnoredErrors = 3,
            SmartTag = 4,
            Table = 5
        }
    }
}

