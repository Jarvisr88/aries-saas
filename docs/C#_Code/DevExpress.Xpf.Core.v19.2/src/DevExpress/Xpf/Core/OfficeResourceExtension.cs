namespace DevExpress.Xpf.Core
{
    using System;

    public class OfficeResourceExtension : ResourceExtensionBase
    {
        public OfficeResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath() => 
            "Office/" + base.ResourcePath;

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

