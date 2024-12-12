namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Reflection;

    public class DXInheritedPropertyDescriptor : System.ComponentModel.PropertyDescriptor
    {
        private object defaultValue;
        private static object noDefault = new object();
        private object originalValue;
        private System.ComponentModel.PropertyDescriptor propertyDescriptor;

        public DXInheritedPropertyDescriptor(System.ComponentModel.PropertyDescriptor propertyDescriptor, object component, bool rootComponent) : base(propertyDescriptor, new Attribute[0])
        {
            MethodInfo[] methods;
            int num;
            this.propertyDescriptor = propertyDescriptor;
            this.InitInheritedDefaultValue(component, rootComponent);
            bool flag = false;
            if (!typeof(ICollection).IsAssignableFrom(propertyDescriptor.PropertyType) || (!propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content) || (propertyDescriptor.Attributes[typeof(InheritableCollectionAttribute)] != null)))
            {
                goto TR_0002;
            }
            else
            {
                ICollection instance = propertyDescriptor.GetValue(component) as ICollection;
                if ((instance == null) || (instance.Count <= 0))
                {
                    goto TR_0002;
                }
                else
                {
                    methods = TypeDescriptor.GetReflectionType(instance).GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    num = 0;
                }
            }
            goto TR_000E;
        TR_0002:
            if (!flag && (this.defaultValue != noDefault))
            {
                List<Attribute> list2 = new List<Attribute>(this.AttributeArray) {
                    new DefaultValueAttribute(this.defaultValue)
                };
                Attribute[] array = new Attribute[list2.Count];
                list2.CopyTo(array, 0);
                this.AttributeArray = array;
            }
            return;
        TR_0004:
            num++;
        TR_000E:
            while (true)
            {
                if (num >= methods.Length)
                {
                    break;
                }
                MethodInfo info = methods[num];
                ParameterInfo[] parameters = info.GetParameters();
                if (parameters.Length == 1)
                {
                    string name = info.Name;
                    Type c = null;
                    if (name.Equals("AddRange") && parameters[0].ParameterType.IsArray)
                    {
                        c = parameters[0].ParameterType.GetElementType();
                    }
                    else if (name.Equals("Add"))
                    {
                        c = parameters[0].ParameterType;
                    }
                    if ((c == null) || typeof(IComponent).IsAssignableFrom(c))
                    {
                        goto TR_0004;
                    }
                    else
                    {
                        Attribute[] attributeArray = new List<Attribute>(this.AttributeArray) { 
                            DesignerSerializationVisibilityAttribute.Hidden,
                            ReadOnlyAttribute.Yes,
                            new EditorAttribute(typeof(UITypeEditor), typeof(UITypeEditor)),
                            new TypeConverterAttribute(typeof(ReadOnlyCollectionConverter))
                        }.ToArray();
                        this.AttributeArray = attributeArray;
                        flag = true;
                    }
                    break;
                }
                goto TR_0004;
            }
            goto TR_0002;
        }

        public override bool CanResetValue(object component) => 
            (this.defaultValue != noDefault) ? !Equals(this.GetValue(component), this.defaultValue) : this.propertyDescriptor.CanResetValue(component);

        private object ClonedDefaultValue(object value)
        {
            DesignerSerializationVisibilityAttribute attribute = (DesignerSerializationVisibilityAttribute) this.propertyDescriptor.Attributes[typeof(DesignerSerializationVisibilityAttribute)];
            DesignerSerializationVisibility visibility = (attribute != null) ? attribute.Visibility : DesignerSerializationVisibility.Visible;
            if ((value != null) && (visibility == DesignerSerializationVisibility.Content))
            {
                if (value is ICloneable)
                {
                    value = ((ICloneable) value).Clone();
                    return value;
                }
                value = noDefault;
            }
            return value;
        }

        protected override void FillAttributes(IList attributeList)
        {
            base.FillAttributes(attributeList);
            foreach (Attribute attribute in this.propertyDescriptor.Attributes)
            {
                attributeList.Add(attribute);
            }
        }

        public override object GetValue(object component) => 
            this.propertyDescriptor.GetValue(component);

        private void InitInheritedDefaultValue(object component, bool rootComponent)
        {
            try
            {
                object defaultValue;
                if (this.propertyDescriptor.ShouldSerializeValue(component))
                {
                    this.defaultValue = this.propertyDescriptor.GetValue(component);
                    defaultValue = this.defaultValue;
                    this.defaultValue = this.ClonedDefaultValue(this.defaultValue);
                }
                else
                {
                    DefaultValueAttribute attribute = (DefaultValueAttribute) this.propertyDescriptor.Attributes[typeof(DefaultValueAttribute)];
                    if (attribute != null)
                    {
                        this.defaultValue = attribute.Value;
                        defaultValue = this.defaultValue;
                    }
                    else
                    {
                        this.defaultValue = noDefault;
                        defaultValue = this.propertyDescriptor.GetValue(component);
                    }
                }
                this.SaveOriginalValue(defaultValue);
            }
            catch
            {
                this.defaultValue = noDefault;
            }
        }

        public override void ResetValue(object component)
        {
            if (this.defaultValue == noDefault)
            {
                this.propertyDescriptor.ResetValue(component);
            }
            else
            {
                this.SetValue(component, this.defaultValue);
            }
        }

        private void SaveOriginalValue(object value)
        {
            if (!(value is ICollection))
            {
                this.originalValue = value;
            }
            else
            {
                this.originalValue = new object[((ICollection) value).Count];
                ((ICollection) value).CopyTo((Array) this.originalValue, 0);
            }
        }

        public override void SetValue(object component, object value)
        {
            this.propertyDescriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component) => 
            !this.IsReadOnly ? ((this.defaultValue != noDefault) ? !Equals(this.GetValue(component), this.defaultValue) : this.propertyDescriptor.ShouldSerializeValue(component)) : (this.propertyDescriptor.ShouldSerializeValue(component) && this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content));

        public override Type ComponentType =>
            this.propertyDescriptor.ComponentType;

        public override bool IsReadOnly =>
            this.propertyDescriptor.IsReadOnly || this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);

        public object OriginalValue =>
            this.originalValue;

        public ICollection OriginalCollection =>
            this.OriginalValue as ICollection;

        public bool IsCollection =>
            this.OriginalCollection != null;

        public bool IsEmptyOriginalCollection =>
            this.IsCollection && (this.OriginalCollection.Count == 0);

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor
        {
            get => 
                this.propertyDescriptor;
            set => 
                this.propertyDescriptor = value;
        }

        public override Type PropertyType =>
            this.propertyDescriptor.PropertyType;

        private class ReadOnlyCollectionConverter : TypeConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
                !(destinationType == typeof(string)) ? base.ConvertTo(context, culture, value, destinationType) : "Read-only";
        }
    }
}

