namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConvertToAttribute : Attribute
    {
        public ConvertToAttribute(System.Type type)
        {
            this.Type = type;
        }

        public System.Type Type { get; private set; }
    }
}

