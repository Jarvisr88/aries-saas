namespace DevExpress.Utils.Design
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class UniversalTypeConverter : ExpandableObjectConverter
    {
        [CompilerGenerated, DebuggerHidden]
        private object <>n__0(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            base.ConvertFrom(context, culture, value);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            SafeBinaryFormatter.CanConvertFrom(context, sourceType, new Func<ITypeDescriptorContext, Type, bool>(this.CanConvertFrom), this.AllowBinaryType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(InstanceDescriptor)) ? SafeBinaryFormatter.CanConvertTo(context, destinationType, new Func<ITypeDescriptorContext, Type, bool>(this.CanConvertTo), this.AllowBinaryType) : true;

        protected virtual bool CheckParameter(Type propertyType, string propertyName, ParameterInfo[] pars)
        {
            if (propertyName.Length > 0)
            {
                propertyName = propertyName.Substring(0, 1).ToLower(CultureInfo.InvariantCulture) + propertyName.Substring(1);
            }
            foreach (ParameterInfo info in pars)
            {
                if (((info.Name == propertyName) || (info.Name == ("_" + propertyName))) && info.ParameterType.Equals(propertyType))
                {
                    return true;
                }
            }
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            SafeBinaryFormatter.ConvertFrom(value, x => this.<>n__0(context, culture, x), this.AllowBinaryType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (this.AllowBinaryType && destinationType.Equals(SafeBinaryFormatter.BinaryType))
            {
                return SafeBinaryFormatter.Serialize(value);
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value != null))
            {
                Type objectType = this.GetObjectType(value);
                ConstructorInfo constructor = objectType.GetConstructor(Type.EmptyTypes);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
                int num = 0;
                List<PropertyDescriptor> list = null;
                foreach (PropertyDescriptor descriptor in properties)
                {
                    if ((descriptor.SerializationVisibility != DesignerSerializationVisibility.Hidden) && descriptor.ShouldSerializeValue(value))
                    {
                        list = new List<PropertyDescriptor> {
                            descriptor
                        };
                        num++;
                    }
                }
                object[] arguments = null;
                if (num > 0)
                {
                    constructor = this.FindConstructor(properties, constructor, this.GetConstructors(objectType), list);
                    arguments = this.GenerateParameters(properties, constructor, value);
                }
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, arguments);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        protected string ExtractPropertyName(string valName)
        {
            string str = valName;
            if (str.StartsWith("_"))
            {
                str = str.Substring(1);
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) + str.Substring(1);
            }
            return str;
        }

        protected virtual ConstructorInfo[] FilterConstructors(ConstructorInfo[] ctors) => 
            ctors;

        protected virtual ConstructorInfo FindConstructor(PropertyDescriptorCollection properties, ConstructorInfo empty, ConstructorInfo[] ctors, List<PropertyDescriptor> list)
        {
            if ((ctors == null) || ((ctors.Length == 0) || ((list == null) || (list.Count == 0))))
            {
                return empty;
            }
            ConstructorInfo info = null;
            int length = -1;
            foreach (ConstructorInfo info2 in ctors)
            {
                ParameterInfo[] parameters = info2.GetParameters();
                bool flag = true;
                foreach (PropertyDescriptor descriptor in list)
                {
                    if (!this.CheckParameter(descriptor.PropertyType, descriptor.Name, parameters))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    if (parameters.Length == list.Count)
                    {
                        return info2;
                    }
                    if ((info == null) || (length > parameters.Length))
                    {
                        ParameterInfo[] infoArray3 = parameters;
                        int index = 0;
                        while (true)
                        {
                            if (index < infoArray3.Length)
                            {
                                ParameterInfo info3 = infoArray3[index];
                                string str = this.ExtractPropertyName(info3.Name);
                                PropertyDescriptor descriptor2 = properties[str];
                                if (descriptor2 != null)
                                {
                                    index++;
                                    continue;
                                }
                                flag = false;
                            }
                            if (flag)
                            {
                                info = info2;
                                length = parameters.Length;
                            }
                            break;
                        }
                    }
                }
            }
            if (info == null)
            {
                info = empty;
            }
            return info;
        }

        protected virtual object[] GenerateParameters(PropertyDescriptorCollection properties, ConstructorInfo ctor, object val)
        {
            if (ctor == null)
            {
                return null;
            }
            ParameterInfo[] parameters = ctor.GetParameters();
            if ((val == null) || (parameters.Length == 0))
            {
                return null;
            }
            object[] objArray = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo info = parameters[i];
                string str = this.ExtractPropertyName(info.Name);
                PropertyDescriptor descriptor = properties[str];
                if (descriptor == null)
                {
                    throw new WarningException("[UniConstructor]: Can't find property " + str);
                }
                objArray[i] = descriptor.GetValue(val);
            }
            return objArray;
        }

        protected ConstructorInfo[] GetConstructors(Type ctorType) => 
            this.FilterConstructors(ctorType.GetConstructors());

        protected virtual Type GetObjectType(object value) => 
            value.GetType();

        public static void ResetObject(object checkObject)
        {
            if (checkObject != null)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(checkObject);
                if ((properties != null) && (properties.Count != 0))
                {
                    foreach (PropertyDescriptor descriptor in properties)
                    {
                        if (descriptor.SerializationVisibility != DesignerSerializationVisibility.Hidden)
                        {
                            descriptor.ResetValue(checkObject);
                        }
                    }
                }
            }
        }

        public static bool ShouldSerializeObject(object checkObject)
        {
            bool flag;
            if (checkObject == null)
            {
                return false;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(checkObject);
            if ((properties == null) || (properties.Count == 0))
            {
                return false;
            }
            using (IEnumerator enumerator = properties.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PropertyDescriptor current = (PropertyDescriptor) enumerator.Current;
                        if (current.SerializationVisibility == DesignerSerializationVisibility.Content)
                        {
                            if (!ShouldSerializeObject(current.GetValue(checkObject)))
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            if ((current.SerializationVisibility == DesignerSerializationVisibility.Hidden) || !current.ShouldSerializeValue(checkObject))
                            {
                                continue;
                            }
                            flag = true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public static bool ShouldSerializeObject(object checkObject, IComponent owner)
        {
            IInheritanceService service = ((owner == null) || (owner.Site == null)) ? null : (owner.Site.GetService(typeof(IInheritanceService)) as IInheritanceService);
            if (service != null)
            {
                InheritanceAttribute inheritanceAttribute = service.GetInheritanceAttribute(owner);
                if ((inheritanceAttribute != null) && (inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited))
                {
                    return true;
                }
            }
            return ShouldSerializeObject(checkObject);
        }

        protected virtual bool AllowBinaryType =>
            true;
    }
}

