namespace DevExpress.XtraPrinting.Design
{
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.ComponentModel;

    public class WatermarkImageTransparencyConverter : Int32Converter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            this.Enabled(context) ? base.CanConvertFrom(context, sourceType) : false;

        private bool Enabled(ITypeDescriptorContext context)
        {
            PageWatermark watermark = ((context != null) ? ((PageWatermark) context.Instance) : null) as PageWatermark;
            return ((watermark == null) || (ImageSource.IsNullOrEmpty(watermark.ImageSource) || !watermark.ImageSource.HasSvgImage));
        }
    }
}

