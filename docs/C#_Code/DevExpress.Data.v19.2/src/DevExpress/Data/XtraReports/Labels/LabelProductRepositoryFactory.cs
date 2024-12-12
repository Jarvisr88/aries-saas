namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.ComponentModel;
    using System.Xml.Linq;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Labels.LabelProductRepositoryFactory class from the DevExpress.XtraReports assembly instead.")]
    public class LabelProductRepositoryFactory
    {
        public ILabelProductRepository Create();
        private XDocument CreateCustomDocument();
        private XDocument CreateDefaultDocument();
    }
}

