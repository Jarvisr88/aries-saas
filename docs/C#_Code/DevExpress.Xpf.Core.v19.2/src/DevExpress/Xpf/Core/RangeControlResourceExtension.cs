namespace DevExpress.Xpf.Core
{
    using System;

    public class RangeControlResourceExtension : ResourceExtensionBase
    {
        public RangeControlResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath() => 
            "RangeControl/" + base.ResourcePath;

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

