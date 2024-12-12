namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandDbCell : XlsSourceCommandContentBase
    {
        private XlsContentDbCell content = new XlsContentDbCell();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Data)
            {
                dataReader.CellOffsets.Clear();
                dataReader.FirstRowOffset = this.content.FirstRowOffset;
                dataReader.CellOffsets.AddRange(this.content.StreamOffsets);
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

