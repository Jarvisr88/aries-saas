namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class FontTypeConverter : FontConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Font), attributes);
            return new AttributeHelper(typeof(ResFinder)).LocalizeProperties(properties);
        }
    }
}

