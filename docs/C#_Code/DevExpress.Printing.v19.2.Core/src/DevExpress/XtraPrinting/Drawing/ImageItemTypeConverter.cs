namespace DevExpress.XtraPrinting.Drawing
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    internal class ImageItemTypeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(string)) ? (!(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            ImageItem imageItem = value as ImageItem;
            return (((imageItem == null) || (destinationType != typeof(InstanceDescriptor))) ? base.ConvertTo(context, culture, value, destinationType) : this.CreateInstanceDescriptor(imageItem));
        }

        private InstanceDescriptor CreateInstanceDescriptor(ImageItem imageItem)
        {
            Type[] types = new Type[] { typeof(string), typeof(ImageSource) };
            object[] arguments = new object[] { imageItem.Id, imageItem.ImageSource };
            return new InstanceDescriptor(typeof(ImageItem).GetConstructor(types), arguments, true);
        }
    }
}

