namespace DevExpress.Utils.Design
{
    using System;

    public abstract class SmartTagPropertiesProviderAttribute : Attribute
    {
        public abstract object[] GetProperties(object component);
    }
}

