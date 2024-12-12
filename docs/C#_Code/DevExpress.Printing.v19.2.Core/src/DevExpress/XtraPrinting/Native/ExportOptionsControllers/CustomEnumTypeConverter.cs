namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    public class CustomEnumTypeConverter : EnumTypeConverter
    {
        private object[] modes;

        public CustomEnumTypeConverter(object[] modes, Type exportModeType);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
    }
}

