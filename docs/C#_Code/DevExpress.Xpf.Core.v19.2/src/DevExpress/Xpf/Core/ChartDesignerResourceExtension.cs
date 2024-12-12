namespace DevExpress.Xpf.Core
{
    using System;

    public class ChartDesignerResourceExtension : ResourceExtensionBase
    {
        public ChartDesignerResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Charts.Designer";
    }
}

