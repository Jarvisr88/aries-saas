namespace DevExpress.Xpf.Core
{
    using System;

    public class DXTabControlResourceExtension : ResourceExtensionBase
    {
        public DXTabControlResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath() => 
            "TabControl/" + base.ResourcePath;

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

