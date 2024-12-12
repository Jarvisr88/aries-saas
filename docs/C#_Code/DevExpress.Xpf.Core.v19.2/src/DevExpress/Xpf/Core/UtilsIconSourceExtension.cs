namespace DevExpress.Xpf.Core
{
    using System;

    public class UtilsIconSourceExtension : RelativeIconSourceExtensionBase
    {
        public UtilsIconSourceExtension(string relativePath) : base(relativePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

