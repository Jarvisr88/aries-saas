namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    internal class CustomObjectConverters : ObjectConverters
    {
        private ICustomObjectConverter customConverter;

        public CustomObjectConverters(ICustomObjectConverter customConverter)
        {
            this.customConverter = customConverter;
        }

        public override IOneTypeObjectConverter GetConverter(Type type)
        {
            IOneTypeObjectConverter customConverter = this.GetCustomConverter(type);
            return ((customConverter == null) ? (!base.IsConverterExists(type) ? null : base.Converters[type]) : customConverter);
        }

        public IOneTypeObjectConverter GetCustomConverter(Type type) => 
            !this.CustomConverter.CanConvert(type) ? null : new OneTypeCustomObjectConverter(type, this.CustomConverter);

        public override bool IsConverterExists(Type type) => 
            !this.CustomConverter.CanConvert(type) ? base.IsConverterExists(type) : true;

        protected ICustomObjectConverter CustomConverter =>
            this.customConverter;
    }
}

