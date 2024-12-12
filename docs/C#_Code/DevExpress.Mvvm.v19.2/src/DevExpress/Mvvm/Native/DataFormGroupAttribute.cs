namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class DataFormGroupAttribute : Attribute
    {
        public DataFormGroupAttribute(string groupName, int order)
        {
            this.GroupName = groupName;
            this.Order = order;
        }

        public string GroupName { get; private set; }

        public int Order { get; private set; }
    }
}

