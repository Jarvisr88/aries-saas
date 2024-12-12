namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal interface INavigationService
    {
        string GetMouseDownScript(HtmlExportContext htmlExportContext, VisualBrick brick);
    }
}

