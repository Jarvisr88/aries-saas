namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class DocxTableLayoutConverter : BooleanTypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? ((sourceType != typeof(string)) ? base.CanConvertFrom(context, sourceType) : true) : false) : (sourceType == typeof(string));

        private bool GetOptionsDisableValue(ITypeDescriptorContext context)
        {
            DocxExportOptions instance = context.Instance as DocxExportOptions;
            return ((instance != null) && (instance.ExportModeBase == ExportModeBase.SingleFile));
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? base.GetStandardValuesSupported(context) : false) : base.GetStandardValuesSupported(context);
    }
}

