namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class XlsPageRangeConverter : PageRangeConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            XlsExportOptions instance = context.Instance as XlsExportOptions;
            return ((instance != null) && (instance.ExportMode == XlsExportMode.SingleFile));
        }
    }
}

