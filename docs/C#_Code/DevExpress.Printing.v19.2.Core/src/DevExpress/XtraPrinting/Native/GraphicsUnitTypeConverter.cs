namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;

    public class GraphicsUnitTypeConverter : EnumTypeConverter
    {
        public GraphicsUnitTypeConverter() : base(typeof(GraphicsUnit))
        {
        }

        protected override void InitializeInternal(Type enumType)
        {
            Initialize(enumType, typeof(ResFinder));
        }
    }
}

