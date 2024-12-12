namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class ToolBarItemAttribute : OrderAttribute
    {
        public string Page { get; set; }

        public string PageGroup { get; set; }
    }
}

