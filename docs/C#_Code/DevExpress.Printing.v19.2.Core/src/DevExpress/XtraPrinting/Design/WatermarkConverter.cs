namespace DevExpress.XtraPrinting.Design
{
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class WatermarkConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(destinationType == typeof(string)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Watermark watermark = value as Watermark;
            if (watermark != null)
            {
                if (!string.IsNullOrEmpty(watermark.Text))
                {
                    return PreviewLocalizer.GetString(PreviewStringId.WatermarkTypeText);
                }
                if (!ImageSource.IsNullOrEmpty(watermark.ImageSource))
                {
                    return PreviewLocalizer.GetString(PreviewStringId.WatermarkTypePicture);
                }
            }
            return PreviewLocalizer.GetString(PreviewStringId.NoneString);
        }
    }
}

