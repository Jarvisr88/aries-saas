namespace DevExpress.Xpf.Core
{
    using System;

    public class DemoBaseImageSourceExtension : RelativeImageSourceExtensionBase
    {
        public DemoBaseImageSourceExtension(string relativePath) : base(relativePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.DemoBase";
    }
}

