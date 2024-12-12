namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandMulBlank : XlsSourceCommandCellBase
    {
        private XlsContentMulBlank content = new XlsContentMulBlank();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Index)
            {
                dataReader.RegisterCell(this.RowIndex, this.content.FirstColumnIndex, this.content.LastColumnIndex);
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;

        public override int RowIndex =>
            this.content.RowIndex;
    }
}

