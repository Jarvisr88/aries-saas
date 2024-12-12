namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class FormattedTextPageRangeConverter : PageRangeConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            FormattedTextExportOptions instance = context.Instance as FormattedTextExportOptions;
            return ((instance != null) && (instance.ExportModeBase == ExportModeBase.SingleFile));
        }
    }
}

