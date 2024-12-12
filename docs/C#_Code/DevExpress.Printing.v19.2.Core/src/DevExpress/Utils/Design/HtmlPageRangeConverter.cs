namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class HtmlPageRangeConverter : PageRangeConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            HtmlExportOptionsBase instance = context.Instance as HtmlExportOptionsBase;
            return ((instance != null) && (instance.ExportMode == HtmlExportMode.SingleFile));
        }
    }
}

