namespace DevExpress.Xpf.Core
{
    using System;

    public class GaugesResourceExtension : ResourceExtensionBase
    {
        public GaugesResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Gauges";
    }
}

