namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ImageTypeConverter : ImageConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
            return new AttributeHelper(typeof(ResFinder)).LocalizeProperties(properties);
        }
    }
}

