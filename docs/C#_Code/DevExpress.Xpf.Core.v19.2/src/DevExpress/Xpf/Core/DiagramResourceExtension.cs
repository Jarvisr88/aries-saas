namespace DevExpress.Xpf.Core
{
    using System;

    public class DiagramResourceExtension : ResourceExtensionBase
    {
        public DiagramResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Diagram.Core";
    }
}

