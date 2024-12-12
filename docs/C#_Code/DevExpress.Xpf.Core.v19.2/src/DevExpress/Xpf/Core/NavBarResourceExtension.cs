namespace DevExpress.Xpf.Core
{
    using System;

    public class NavBarResourceExtension : ResourceExtensionBase
    {
        public NavBarResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.NavBar";
    }
}

