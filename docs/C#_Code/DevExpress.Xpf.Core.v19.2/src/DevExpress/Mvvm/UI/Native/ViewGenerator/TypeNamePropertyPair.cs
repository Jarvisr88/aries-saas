namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;
    using System.Runtime.CompilerServices;

    public class TypeNamePropertyPair
    {
        public TypeNamePropertyPair(string typeFullName, string propertyName)
        {
            this.TypeFullName = typeFullName;
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }

        public string TypeFullName { get; private set; }
    }
}

