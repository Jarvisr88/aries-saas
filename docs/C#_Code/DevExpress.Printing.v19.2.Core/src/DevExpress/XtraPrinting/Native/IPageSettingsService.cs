namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.DocumentView;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    public interface IPageSettingsService
    {
        void Assign(IPage page, MarginsF margins, MarginsF minMargins, PaperKind paperKind, Size pageSize, bool landscape, string paperName);
        void SetBottomMargin(IPage page, float value);
        void SetLeftMargin(IPage page, float value);
        void SetRightMargin(IPage page, float value);
        void SetTopMargin(IPage page, float value);
    }
}

