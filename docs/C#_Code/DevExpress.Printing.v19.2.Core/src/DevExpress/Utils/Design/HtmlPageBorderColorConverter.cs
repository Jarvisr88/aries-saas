namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class HtmlPageBorderColorConverter : ColorConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? ((sourceType != typeof(string)) ? base.CanConvertFrom(context, sourceType) : true) : false) : (sourceType == typeof(string));

        protected virtual bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            HtmlExportOptionsBase instance = context.Instance as HtmlExportOptionsBase;
            return ((instance != null) && (instance.ExportMode == HtmlExportMode.SingleFile));
        }
    }
}

