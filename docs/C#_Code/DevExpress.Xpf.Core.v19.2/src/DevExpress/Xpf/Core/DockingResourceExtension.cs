namespace DevExpress.Xpf.Core
{
    using System;

    public class DockingResourceExtension : ResourceExtensionBase
    {
        public DockingResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Docking";
    }
}

