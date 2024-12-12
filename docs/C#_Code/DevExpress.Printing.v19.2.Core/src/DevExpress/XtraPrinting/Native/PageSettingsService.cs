namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    internal class PageSettingsService : IPageSettingsService
    {
        private PrintingSystemBase ps;

        public PageSettingsService(PrintingSystemBase ps);
        public virtual void Assign(IPage page, MarginsF margins, MarginsF minMargins, PaperKind paperKind, Size pageSize, bool landscape, string paperName);
        private CustomPageData GetPageData(Page page);
        private void OnChangePageSettings();
        public virtual void SetBottomMargin(IPage page, float value);
        public virtual void SetLeftMargin(IPage page, float value);
        public virtual void SetRightMargin(IPage page, float value);
        public virtual void SetTopMargin(IPage page, float value);
        private static int ToHundredths(float value);

        private XtraPageSettingsBase PageSettings { get; }
    }
}

