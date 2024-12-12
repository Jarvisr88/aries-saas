namespace DevExpress.Xpf.Core
{
    using System;

    public class RibbonResourceExtension : ResourceExtensionBase
    {
        public RibbonResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath() => 
            base.ResourcePath;

        protected override string Namespace =>
            "DevExpress.Xpf.Ribbon";
    }
}

