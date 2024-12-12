namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class DXImageAttribute : Attribute
    {
        public DXImageAttribute()
        {
        }

        public DXImageAttribute(string imageName)
        {
            this.ImageName = imageName;
        }

        public string ImageName { get; internal set; }

        public string LargeImageUri { get; set; }

        public string SmallImageUri { get; set; }
    }
}

