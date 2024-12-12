namespace DevExpress.Utils
{
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    public class PointFloatConverterForDisplay : PointFloatConverter
    {
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => 
            true;

        protected override TypeConverter GetSingleConverter() => 
            new SingleTypeConverter();
    }
}

