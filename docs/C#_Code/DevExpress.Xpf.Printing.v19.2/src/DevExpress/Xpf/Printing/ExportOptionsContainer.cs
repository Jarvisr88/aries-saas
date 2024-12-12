namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class ExportOptionsContainer : ExportOptions
    {
        public ExportOptionsContainer()
        {
            base.options.Add(typeof(XpsExportOptions), new XpsExportOptions());
        }

        [Description("Gets the settings used to specify export parameters when a document is exported to XPS format.")]
        public XpsExportOptions Xps =>
            (XpsExportOptions) base.options[typeof(XpsExportOptions)];
    }
}

