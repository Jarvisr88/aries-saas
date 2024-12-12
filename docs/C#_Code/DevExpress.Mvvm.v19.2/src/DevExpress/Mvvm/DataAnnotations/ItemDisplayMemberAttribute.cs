namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class ItemDisplayMemberAttribute : Attribute
    {
        public ItemDisplayMemberAttribute(string displayMember)
        {
            this.DisplayMember = displayMember;
        }

        public string DisplayMember { get; private set; }
    }
}

