namespace DevExpress.XtraExport.Xls
{
    public class XlsContentCF : XlsContentCFBase
    {
        private XlsDxfN format = new XlsDxfN();

        public XlsDxfN Format =>
            this.format;

        protected override XlsDxfN DifferentialFormat =>
            this.format;
    }
}

