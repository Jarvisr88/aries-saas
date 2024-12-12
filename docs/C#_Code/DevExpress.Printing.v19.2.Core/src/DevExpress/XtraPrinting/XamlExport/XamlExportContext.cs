namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    public class XamlExportContext
    {
        private readonly DevExpress.XtraPrinting.Page page;
        private readonly int pageNumber;
        private readonly int pageCount;
        private readonly DevExpress.XtraPrinting.XamlExport.ResourceCache resourceCache;
        private readonly DevExpress.XtraPrinting.XamlExport.ResourceMap resourceMap;
        private readonly XamlCompatibility compatibility;
        private readonly bool isPartialTrustMode;
        private readonly bool embedImagesToXaml;
        private readonly DevExpress.XtraPrinting.XamlExport.TextMeasurementSystem textMeasurementSystem;

        public XamlExportContext(DevExpress.XtraPrinting.Page page, int pageNumber, int pageCount, DevExpress.XtraPrinting.XamlExport.ResourceCache resourceCache, DevExpress.XtraPrinting.XamlExport.ResourceMap resourceMap, XamlCompatibility compatibility, DevExpress.XtraPrinting.XamlExport.TextMeasurementSystem textMeasurementSystem, bool isPartialTrustMode, bool embedImagesToXaml)
        {
            this.page = page;
            this.pageNumber = pageNumber;
            this.pageCount = pageCount;
            this.resourceCache = resourceCache;
            this.resourceMap = resourceMap;
            this.compatibility = compatibility;
            this.textMeasurementSystem = textMeasurementSystem;
            this.isPartialTrustMode = isPartialTrustMode;
            this.embedImagesToXaml = embedImagesToXaml;
        }

        public DevExpress.XtraPrinting.Page Page =>
            this.page;

        public int PageNumber =>
            this.pageNumber;

        public int PageCount =>
            this.pageCount;

        public DevExpress.XtraPrinting.XamlExport.ResourceCache ResourceCache =>
            this.resourceCache;

        public DevExpress.XtraPrinting.XamlExport.ResourceMap ResourceMap =>
            this.resourceMap;

        public XamlCompatibility Compatibility =>
            this.compatibility;

        public bool IsPartialTrustMode =>
            this.isPartialTrustMode;

        public bool EmbedImagesToXaml =>
            this.embedImagesToXaml;

        public DevExpress.XtraPrinting.XamlExport.TextMeasurementSystem TextMeasurementSystem =>
            this.textMeasurementSystem;
    }
}

