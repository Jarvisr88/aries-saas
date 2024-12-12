namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Runtime.CompilerServices;

    public class AttachedPropertyInfo : XmlXtraSerializer.XmlXtraPropertyInfo
    {
        public AttachedPropertyInfo(string name, Type propertyType, object value) : this(name, propertyType, null, null, value)
        {
        }

        public AttachedPropertyInfo(string name, Type propertyType, Type dPropType, Type ownerType, object value) : this(name, propertyType, dPropType, ownerType, value, false)
        {
        }

        public AttachedPropertyInfo(string name, Type propertyType, Type dPropType, Type ownerType, object value, bool isKey) : base(name, propertyType, value, isKey)
        {
            this.OwnerType = ownerType;
            char[] separator = new char[] { '.' };
            string[] strArray = name.Split(separator, StringSplitOptions.None);
            this.DependencyPropertyName = strArray[strArray.Length - 1];
            this.DependencyPropertyType = dPropType;
        }

        public Type OwnerType { get; private set; }

        public string DependencyPropertyName { get; private set; }

        public Type DependencyPropertyType { get; private set; }
    }
}

