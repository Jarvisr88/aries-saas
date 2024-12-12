namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public abstract class PageRangeConverter : StringConverter
    {
        protected PageRangeConverter()
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (context != null) ? (!this.GetOptionsDisableValue(context) ? ((sourceType != typeof(string)) ? base.CanConvertFrom(context, sourceType) : true) : false) : (sourceType == typeof(string));

        protected abstract bool GetOptionsDisableValue(ITypeDescriptorContext context);
    }
}

