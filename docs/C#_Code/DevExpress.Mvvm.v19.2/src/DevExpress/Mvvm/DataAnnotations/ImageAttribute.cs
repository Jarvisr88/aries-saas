namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class ImageAttribute : Attribute
    {
        public ImageAttribute()
        {
        }

        public ImageAttribute(string imageUri)
        {
            this.ImageUri = imageUri;
        }

        public string ImageUri { get; internal set; }
    }
}

