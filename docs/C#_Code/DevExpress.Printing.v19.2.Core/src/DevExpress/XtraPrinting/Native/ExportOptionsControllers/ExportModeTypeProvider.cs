namespace DevExpress.XtraPrinting.Native.ExportOptionsControllers
{
    using System;
    using System.ComponentModel;

    public class ExportModeTypeProvider : TypeDescriptionProvider
    {
        private object[] modes;
        private Type exportModeType;

        public ExportModeTypeProvider(object[] modes, Type exportModeType);
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance);
        public object Validate(object value);
    }
}

