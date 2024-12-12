namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    internal class CustomObjectConverterImplementation : ObjectConverterImplementation
    {
        private ICustomObjectConverter customConverter;
        private CustomObjectConverters customConverters;

        public CustomObjectConverterImplementation(ICustomObjectConverter customConverter)
        {
            this.customConverter = customConverter;
            this.customConverters = new CustomObjectConverters(customConverter);
            ObjectConverter.Instance.CopyConvertersTo(this);
        }

        protected override ObjectConverters Converters =>
            this.customConverters;
    }
}

