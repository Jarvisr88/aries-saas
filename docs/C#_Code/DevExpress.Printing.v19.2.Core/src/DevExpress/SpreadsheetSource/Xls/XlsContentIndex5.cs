namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsContentIndex5 : XlsContentIndex
    {
        private const int FixedPartSize5 = 12;

        public override void Read(XlReader reader, int size)
        {
            reader.ReadInt32();
            base.FirstRowIndex = reader.ReadInt16();
            base.LastRowIndex = reader.ReadInt16();
            base.DefaultColumnWidthOffset = reader.ReadUInt32();
            int num = (size - 12) / 4;
            for (int i = 0; i < num; i++)
            {
                base.DbCellsPositions.Add((long) reader.ReadUInt32());
            }
        }
    }
}

