namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class UniversalCollectionTypeConverter : TypeConverter
    {
        protected string GetObjectCaption(object obj)
        {
            if (obj == null)
            {
                return "<null>";
            }
            ICaptionSupport support = obj as ICaptionSupport;
            if (support != null)
            {
                return support.Caption;
            }
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj)["Caption"];
            return ((descriptor == null) ? obj.ToString() : descriptor.GetValue(obj).ToString());
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            IList list = value as IList;
            if ((list == null) || (list.Count <= 0))
            {
                return null;
            }
            PropertyDescriptor[] properties = new PropertyDescriptor[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                object obj2 = list[i];
                string objectCaption = this.GetObjectCaption(obj2);
                string caption = string.Format((list.Count > 9) ? "{0:d2}" : "{0}", i);
                if ((objectCaption != null) && (objectCaption.Length > 0))
                {
                    caption = caption + " - " + objectCaption;
                }
                properties[i] = new UniversalCollectionPropertyDescriptor(obj2, caption);
            }
            return new PropertyDescriptorCollection(properties);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
            true;
    }
}

