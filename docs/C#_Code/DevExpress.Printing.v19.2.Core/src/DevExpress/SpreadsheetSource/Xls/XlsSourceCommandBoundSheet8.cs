namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsSourceCommandBoundSheet8 : XlsSourceCommandBase
    {
        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (this.IsRegularSheet)
            {
                contentBuilder.InnerWorksheets.Add(new XlsWorksheet(this.Name, this.VisibleState, this.StartPosition));
            }
            contentBuilder.SheetNames.Add(this.Name);
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.StartPosition = reader.ReadNotCryptedInt32();
            this.VisibleState = ((XlSheetVisibleState) reader.ReadByte()) & (XlSheetVisibleState.VeryHidden | XlSheetVisibleState.Hidden);
            this.IsRegularSheet = reader.ReadByte() == 0;
            this.Name = ShortXLUnicodeString.FromStream(reader).Value;
        }

        public int StartPosition { get; protected set; }

        public XlSheetVisibleState VisibleState { get; protected set; }

        public string Name { get; protected set; }

        protected bool IsRegularSheet { get; set; }
    }
}

