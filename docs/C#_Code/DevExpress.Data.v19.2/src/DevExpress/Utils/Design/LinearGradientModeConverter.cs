namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;

    public class LinearGradientModeConverter : EnumTypeConverter
    {
        public LinearGradientModeConverter() : base(typeof(LinearGradientMode))
        {
        }

        protected override void InitializeInternal(Type enumType)
        {
            Initialize(enumType, typeof(ResFinder));
        }
    }
}

