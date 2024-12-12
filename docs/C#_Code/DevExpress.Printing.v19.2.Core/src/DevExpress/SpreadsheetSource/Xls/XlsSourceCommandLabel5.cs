namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public class XlsSourceCommandLabel5 : XlsSourceCommandLabel
    {
        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            base.InnerContent.RowIndex = reader.ReadUInt16();
            base.InnerContent.ColumnIndex = reader.ReadUInt16();
            base.InnerContent.FormatIndex = reader.ReadUInt16();
            base.InnerContent.Value = contentBuilder.ReadString2(reader);
            int count = (base.Size - base.InnerContent.Value.Length) - 8;
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }
    }
}

