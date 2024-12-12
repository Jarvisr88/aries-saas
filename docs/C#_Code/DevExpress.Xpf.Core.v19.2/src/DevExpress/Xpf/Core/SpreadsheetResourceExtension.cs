namespace DevExpress.Xpf.Core
{
    using System;

    public class SpreadsheetResourceExtension : ResourceExtensionBase
    {
        public SpreadsheetResourceExtension(string resourcePath) : base(resourcePath)
        {
        }

        protected override string Namespace =>
            "DevExpress.Xpf.Spreadsheet";
    }
}

