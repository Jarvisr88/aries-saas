namespace DevExpress.Xpf.Core
{
    using System;

    public class GridResourceExtension : ResourceExtensionBase
    {
        public GridResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Grid";
    }
}

