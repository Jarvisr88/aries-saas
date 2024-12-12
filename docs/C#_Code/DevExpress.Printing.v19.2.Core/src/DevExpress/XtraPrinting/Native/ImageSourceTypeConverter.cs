namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Data;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;

    public class ImageSourceTypeConverter : ExpandableObjectConverter
    {
        [ThreadStatic]
        private static bool? serializeSharedResources = true;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(string)) ? (!(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            ImageSource imageSource = value as ImageSource;
            if (!ImageSource.IsNullOrEmpty(imageSource))
            {
                if (destinationType == typeof(InstanceDescriptor))
                {
                    return this.CreateInstanceDescriptor(imageSource);
                }
                if (destinationType == typeof(string))
                {
                    string fullName = imageSource.ImageInstance.GetType().FullName;
                    return new DXDisplayNameAttribute(typeof(ResFinder), "PropertyNamesRes", fullName).DisplayName;
                }
            }
            return (!(destinationType == typeof(string)) ? base.ConvertTo(context, culture, value, destinationType) : PreviewStringId.NoneString.GetString());
        }

        private static PropertyDescriptor CreateImageSourcePropertyDescriptor(PropertyDescriptor property)
        {
            string resourceName = $"{property.ComponentType}.{property.Name}";
            Attribute attribute = new DXDisplayNameAttribute(typeof(ResFinder), resourceName);
            Attribute[] attrs = new Attribute[] { attribute };
            return new ImageSourcePropertyDescriptor(property, attrs);
        }

        private InstanceDescriptor CreateInstanceDescriptor(ImageSource imageSource)
        {
            bool valueOrDefault = SerializeSharedResources.GetValueOrDefault(true);
            if (valueOrDefault && (imageSource.IsSharedResource && imageSource.HasSvgImage))
            {
                Type[] typeArray1 = new Type[] { typeof(SvgImage), typeof(bool) };
                object[] objArray1 = new object[] { imageSource.SvgImage, imageSource.IsSharedResource };
                return new InstanceDescriptor(typeof(ImageSource).GetConstructor(typeArray1), objArray1, true);
            }
            if (!valueOrDefault || !imageSource.IsSharedResource)
            {
                Type[] typeArray3 = new Type[] { typeof(string), typeof(string) };
                return new InstanceDescriptor(typeof(ImageSource).GetConstructor(typeArray3), imageSource.GetMetadata(), true);
            }
            Type[] types = new Type[] { typeof(System.Drawing.Image), typeof(bool) };
            object[] arguments = new object[] { imageSource.Image, imageSource.IsSharedResource };
            return new InstanceDescriptor(typeof(ImageSource).GetConstructor(types), arguments, true);
        }

        private static PropertyDescriptorCollection GetImageProperties(ImageSource imageSource, Attribute[] attributes)
        {
            if (imageSource.Image == null)
            {
                return null;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(imageSource.Image, attributes);
            PropertyDescriptor[] descriptorArray = new PropertyDescriptor[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                descriptorArray[i] = CreateImageSourcePropertyDescriptor(properties[i]);
            }
            string[] names = new string[] { "RawFormat", "PixelFormat", "PhysicalDimension", "Size", "Tag", "HorizontalResolution", "VerticalResolution" };
            return new PropertyDescriptorCollection(descriptorArray).Sort(names);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            ImageSource imageSource = value as ImageSource;
            return (ImageSource.IsNullOrEmpty(imageSource) ? base.GetProperties(context, value, attributes) : (imageSource.HasSvgImage ? GetSvgImageProperties(imageSource, attributes) : GetImageProperties(imageSource, attributes)));
        }

        private static PropertyDescriptorCollection GetSvgImageProperties(ImageSource imageSource, Attribute[] attributes)
        {
            if (imageSource.SvgImage == null)
            {
                return null;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(imageSource.SvgImage, attributes);
            string[] names = new string[] { "Width", "Height", "OffsetX", "OffsetY" };
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            for (int i = 0; i < names.Length; i++)
            {
                PropertyDescriptor property = properties[names[i]];
                if (property != null)
                {
                    list.Add(CreateImageSourcePropertyDescriptor(property));
                }
            }
            return new PropertyDescriptorCollection(list.ToArray()).Sort(names);
        }

        public static bool? SerializeSharedResources
        {
            get => 
                serializeSharedResources;
            set => 
                serializeSharedResources = value;
        }

        private class ImageSourcePropertyDescriptor : PropertyDescriptorWrapper
        {
            public ImageSourcePropertyDescriptor(PropertyDescriptor oldPropertyDescriptor) : base(oldPropertyDescriptor)
            {
            }

            public ImageSourcePropertyDescriptor(PropertyDescriptor oldPropertyDescriptor, Attribute[] attrs) : base(oldPropertyDescriptor, attrs)
            {
            }

            public override bool CanResetValue(object component) => 
                base.CanResetValue(this.GetActualComponent(component));

            private object GetActualComponent(object component)
            {
                ImageSource source = component as ImageSource;
                return ((source == null) ? component : source.ImageInstance);
            }

            public override object GetValue(object component) => 
                base.GetValue(this.GetActualComponent(component));

            public override void ResetValue(object component)
            {
                base.ResetValue(this.GetActualComponent(component));
            }

            public override void SetValue(object component, object value)
            {
                base.SetValue(this.GetActualComponent(component), value);
            }

            public override bool ShouldSerializeValue(object component) => 
                base.ShouldSerializeValue(this.GetActualComponent(component));
        }
    }
}

