namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class FormattedTextSingleFileOptionConverter : BooleanTypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? ((sourceType != typeof(string)) ? base.CanConvertFrom(context, sourceType) : true) : false) : (sourceType == typeof(string));

        protected bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            FormattedTextExportOptions instance = context.Instance as FormattedTextExportOptions;
            return ((instance != null) && (instance.ExportModeBase == ExportModeBase.SingleFilePageByPage));
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? base.GetStandardValuesSupported(context) : false) : base.GetStandardValuesSupported(context);
    }
}

