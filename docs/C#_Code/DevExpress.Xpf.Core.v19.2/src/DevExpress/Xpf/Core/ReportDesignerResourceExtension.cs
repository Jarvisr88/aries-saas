namespace DevExpress.Xpf.Core
{
    using System;

    public class ReportDesignerResourceExtension : ResourceExtensionBase
    {
        public ReportDesignerResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.ReportDesigner";
    }
}

