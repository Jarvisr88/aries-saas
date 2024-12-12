namespace DevExpress.Utils.Design
{
    using System;

    public class NullableSizeTypeConverter : NullableTypeConverter
    {
        public NullableSizeTypeConverter(Type type) : base(type)
        {
        }

        protected override string GetDefaultPopupText() => 
            "Custom";

        protected override string GetNullText() => 
            "Auto";
    }
}

