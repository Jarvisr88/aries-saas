namespace DevExpress.Xpf.Core
{
    using System;

    public class PrintingResourceExtension : ResourceExtensionBase
    {
        public PrintingResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Printing";
    }
}

