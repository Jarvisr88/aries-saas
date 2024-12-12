namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using System;
    using System.ComponentModel;

    public class CustomEnumDescriptor : CustomTypeDescriptor
    {
        private TypeConverter typeConverter;

        public CustomEnumDescriptor(ICustomTypeDescriptor customTypeDescriptor, TypeConverter typeConverter);
        public override TypeConverter GetConverter();
    }
}

