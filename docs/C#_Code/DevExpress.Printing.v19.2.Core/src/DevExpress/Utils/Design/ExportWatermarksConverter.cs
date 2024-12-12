namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public abstract class ExportWatermarksConverter : BooleanTypeConverter
    {
        protected ExportWatermarksConverter()
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? ((sourceType != typeof(string)) ? base.CanConvertFrom(context, sourceType) : true) : false) : (sourceType == typeof(string));

        protected abstract bool GetOptionsDisableValue(ITypeDescriptorContext context);
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? base.GetStandardValuesSupported(context) : false) : base.GetStandardValuesSupported(context);
    }
}

