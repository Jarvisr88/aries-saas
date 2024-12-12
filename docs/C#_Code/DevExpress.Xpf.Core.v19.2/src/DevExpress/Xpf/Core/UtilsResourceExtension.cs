namespace DevExpress.Xpf.Core
{
    using System;

    public class UtilsResourceExtension : ResourceExtensionBase
    {
        public UtilsResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

