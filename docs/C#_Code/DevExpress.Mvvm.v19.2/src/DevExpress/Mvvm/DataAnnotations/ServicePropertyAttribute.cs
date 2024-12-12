namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class ServicePropertyAttribute : Attribute
    {
        public ServicePropertyAttribute() : this(true)
        {
        }

        public ServicePropertyAttribute(ServiceSearchMode searchMode) : this(true)
        {
            this.SearchMode = searchMode;
        }

        public ServicePropertyAttribute(bool isServiceProperty)
        {
            this.IsServiceProperty = isServiceProperty;
        }

        public string Key { get; set; }

        public ServiceSearchMode SearchMode { get; set; }

        public bool IsServiceProperty { get; private set; }
    }
}

