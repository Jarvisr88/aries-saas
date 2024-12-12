namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandIndex : XlsSourceCommandContentBase
    {
        private XlsContentIndex content = new XlsContentIndex();

        protected virtual XlsContentIndex CreateContent() => 
            new XlsContentIndex();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            dataReader.FirstRowIndex = this.content.FirstRowIndex;
            dataReader.LastRowIndex = this.content.LastRowIndex;
            dataReader.DefaultColumnWidthOffset = this.content.DefaultColumnWidthOffset;
            dataReader.DbCellPositions.Clear();
            dataReader.DbCellPositions.AddRange(this.content.DbCellsPositions);
        }

        protected override IXlsContent GetContent()
        {
            this.content = this.CreateContent();
            return this.content;
        }
    }
}

