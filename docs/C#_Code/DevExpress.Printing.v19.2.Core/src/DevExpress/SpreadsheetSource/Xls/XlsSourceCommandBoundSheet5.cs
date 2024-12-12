namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;

    public class XlsSourceCommandBoundSheet5 : XlsSourceCommandBoundSheet8
    {
        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            base.StartPosition = reader.ReadNotCryptedInt32();
            base.VisibleState = ((XlSheetVisibleState) reader.ReadByte()) & (XlSheetVisibleState.VeryHidden | XlSheetVisibleState.Hidden);
            base.IsRegularSheet = reader.ReadByte() == 0;
            base.Name = contentBuilder.ReadString(reader);
        }
    }
}

