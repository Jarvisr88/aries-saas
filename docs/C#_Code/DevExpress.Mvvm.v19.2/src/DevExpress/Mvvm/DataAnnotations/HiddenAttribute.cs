namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class HiddenAttribute : Attribute
    {
        public HiddenAttribute() : this(true)
        {
        }

        public HiddenAttribute(bool hidden)
        {
            this.Hidden = hidden;
        }

        public bool Hidden { get; set; }
    }
}

