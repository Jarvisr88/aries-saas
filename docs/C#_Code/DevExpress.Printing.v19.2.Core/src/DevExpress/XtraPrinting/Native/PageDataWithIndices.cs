namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using System;

    public class PageDataWithIndices : PageData
    {
        private string pageIndices;

        public PageDataWithIndices();
        public PageDataWithIndices(ReadonlyPageData pageData, string indices);

        [XtraSerializableProperty]
        public string PageIndices { get; set; }
    }
}

