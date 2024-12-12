namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class RotatedShapeTypeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(InstanceDescriptor)) ? (!(destinationType == typeof(string)) ? base.CanConvertTo(context, destinationType) : true) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            (!(destinationType == typeof(string)) || ((context == null) || (context.Instance == null))) ? (!(destinationType == typeof(InstanceDescriptor)) ? base.ConvertTo(context, culture, value, destinationType) : DoConvertToInstanceDescriptor(context, culture, value, destinationType)) : DoConvertToString(context, culture, value, destinationType);

        private static InstanceDescriptor DoConvertToInstanceDescriptor(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            new InstanceDescriptor(value.GetType().GetConstructor(new Type[0]), new object[0], false);

        private static string DoConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            RotatedShape shape = value as RotatedShape;
            return (((shape == null) || (shape.Shape == null)) ? string.Empty : GetDisplayName(context, culture, shape.Shape.ShapeId));
        }

        private static string GetDisplayName(ITypeDescriptorContext context, CultureInfo culture, ShapeId shapeId) => 
            (string) TypeDescriptor.GetConverter(shapeId.GetType()).ConvertTo(context, culture, shapeId, typeof(string));

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            RotatedShape shape = value as RotatedShape;
            if (shape == null)
            {
                return base.GetProperties(context, value, attributes);
            }
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(shape.Shape, attributes))
            {
                list.Add(new RotatedShapePropertyDescriptor(descriptor));
            }
            return new PropertyDescriptorCollection(list.ToArray());
        }
    }
}

