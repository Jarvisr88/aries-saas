namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class HtmlExportWatermarksConverter : ExportWatermarksConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            HtmlExportOptionsBase instance = context.Instance as HtmlExportOptionsBase;
            return ((instance != null) && (instance.ExportMode == HtmlExportMode.SingleFile));
        }
    }
}

