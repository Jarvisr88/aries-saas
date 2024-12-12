namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class XlsxPageRangeConverter : PageRangeConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            XlsxExportOptions instance = context.Instance as XlsxExportOptions;
            return ((instance != null) && (instance.ExportMode == XlsxExportMode.SingleFile));
        }
    }
}

