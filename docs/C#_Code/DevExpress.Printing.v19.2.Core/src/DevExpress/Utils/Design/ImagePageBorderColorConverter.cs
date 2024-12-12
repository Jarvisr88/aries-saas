namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class ImagePageBorderColorConverter : HtmlPageBorderColorConverter
    {
        protected override bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            ImageExportOptions instance = context.Instance as ImageExportOptions;
            return ((instance != null) && (instance.ExportMode == ImageExportMode.SingleFile));
        }
    }
}

