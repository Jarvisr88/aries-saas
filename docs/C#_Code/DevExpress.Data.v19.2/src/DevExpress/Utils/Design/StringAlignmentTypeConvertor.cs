namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;

    public class StringAlignmentTypeConvertor : EnumTypeConverter
    {
        public StringAlignmentTypeConvertor(Type type) : base(type)
        {
        }

        protected override void InitializeInternal(Type enumType)
        {
            Initialize(enumType, typeof(ResFinder));
        }
    }
}

