namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class QuoteStringsWithSeparatorsConverter : ExportWatermarksConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            TextExportOptionsBase instance = context.Instance as TextExportOptionsBase;
            return ((instance != null) && string.IsNullOrEmpty(instance.GetActualSeparator()));
        }
    }
}

