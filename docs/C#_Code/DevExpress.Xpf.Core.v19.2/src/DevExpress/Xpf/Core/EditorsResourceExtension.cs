namespace DevExpress.Xpf.Core
{
    using System;

    public class EditorsResourceExtension : ResourceExtensionBase
    {
        public EditorsResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string GetResourcePath() => 
            "Editors/" + base.ResourcePath;

        protected override string Namespace =>
            "DevExpress.Xpf.Core";
    }
}

