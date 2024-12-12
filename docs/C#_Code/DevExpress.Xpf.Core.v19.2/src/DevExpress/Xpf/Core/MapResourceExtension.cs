namespace DevExpress.Xpf.Core
{
    using System;

    public class MapResourceExtension : ResourceExtensionBase
    {
        public MapResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Map";
    }
}

