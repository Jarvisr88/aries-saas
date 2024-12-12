namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class RasterizationResolutionConverter : Int32Converter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (context.Instance as PageByPageExportOptionsBase).RasterizeImages ? base.CanConvertFrom(context, sourceType) : false;
    }
}

