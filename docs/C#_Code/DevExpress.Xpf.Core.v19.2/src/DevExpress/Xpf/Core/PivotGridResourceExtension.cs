namespace DevExpress.Xpf.Core
{
    using System;

    public class PivotGridResourceExtension : ResourceExtensionBase
    {
        public PivotGridResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.PivotGrid";
    }
}

