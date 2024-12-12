namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    public abstract class PSCommandsTypeConverter : EnumTypeConverter
    {
        public PSCommandsTypeConverter() : base(typeof(PrintingSystemCommand))
        {
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => 
            new TypeConverter.StandardValuesCollection(this.Commands);

        protected abstract PrintingSystemCommand[] Commands { get; }
    }
}

