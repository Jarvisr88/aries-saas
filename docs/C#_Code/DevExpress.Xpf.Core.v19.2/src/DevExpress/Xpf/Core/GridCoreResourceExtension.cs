namespace DevExpress.Xpf.Core
{
    using System;

    public class GridCoreResourceExtension : ResourceExtensionBase
    {
        public GridCoreResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Grid";

        protected override string Postfix =>
            ".Core";
    }
}

