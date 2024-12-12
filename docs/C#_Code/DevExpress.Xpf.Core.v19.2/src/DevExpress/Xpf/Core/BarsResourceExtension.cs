namespace DevExpress.Xpf.Core
{
    using System;

    public class BarsResourceExtension : ResourceExtensionBase
    {
        public BarsResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath() => 
            "Bars/" + base.ResourcePath;

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

