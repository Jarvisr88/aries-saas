namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class ContextMenuItemAttribute : OrderAttribute
    {
        public string Group { get; set; }
    }
}

