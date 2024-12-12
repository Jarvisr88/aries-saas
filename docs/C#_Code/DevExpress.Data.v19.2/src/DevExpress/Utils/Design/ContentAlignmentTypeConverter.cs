namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;

    public class ContentAlignmentTypeConverter : EnumTypeConverter
    {
        public ContentAlignmentTypeConverter(Type type) : base(type)
        {
        }

        protected override void InitializeInternal(Type enumType)
        {
            Initialize(enumType, typeof(ResFinder));
        }
    }
}

