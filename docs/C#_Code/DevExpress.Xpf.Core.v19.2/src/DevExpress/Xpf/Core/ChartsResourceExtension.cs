namespace DevExpress.Xpf.Core
{
    using System;

    public class ChartsResourceExtension : ResourceExtensionBase
    {
        public ChartsResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Charts";
    }
}

