namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;

    public class LineJoinEnumTypeConverter : EnumTypeConverter
    {
        public LineJoinEnumTypeConverter(Type type) : base(type)
        {
        }

        protected override void InitializeInternal(Type enumType)
        {
            Initialize(enumType, typeof(ResFinder));
        }
    }
}

