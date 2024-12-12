namespace DevExpress.Xpf.Core
{
    using System;

    public class PropertyGridResourceExtension : ResourceExtensionBase
    {
        public PropertyGridResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.PropertyGrid";
    }
}

