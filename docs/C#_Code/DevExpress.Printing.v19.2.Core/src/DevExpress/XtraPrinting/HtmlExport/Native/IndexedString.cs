namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using DevExpress.Utils;
    using System;

    public sealed class IndexedString
    {
        private string value;

        public IndexedString(string value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.value = value;
        }

        public string Value =>
            this.value;
    }
}

