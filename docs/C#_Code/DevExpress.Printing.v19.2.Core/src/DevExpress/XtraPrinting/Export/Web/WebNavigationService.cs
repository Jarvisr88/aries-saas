namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class WebNavigationService : INavigationService
    {
        protected virtual string CreateNavigationScript()
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                return string.Empty;
            }
            if (this.Url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
            {
                return this.Url;
            }
            if (ReferenceEquals(this.Brick.NavigationPair, BrickPagePair.Empty))
            {
                return $"ASPx.xr_NavigateUrl('{DXHttpUtility.UrlEncodeToUnicodeCompatible(this.Url)}', '{this.Brick.Target}')";
            }
            if (!this.Context.CrossReferenceAvailable)
            {
                return string.Empty;
            }
            string htmlUrl = HtmlHelper.GetHtmlUrl(this.Url);
            return $"ASPx.xr_NavigateUrl('{DXHttpUtility.UrlEncodeToUnicodeCompatible(htmlUrl)}', '{this.Brick.Target}')";
        }

        string INavigationService.GetMouseDownScript(HtmlExportContext htmlExportContext, VisualBrick brick)
        {
            this.Url = brick.GetActualUrl();
            this.Brick = brick;
            this.Context = htmlExportContext;
            return this.CreateNavigationScript();
        }

        protected string Url { get; private set; }

        protected VisualBrick Brick { get; private set; }

        protected HtmlExportContext Context { get; private set; }
    }
}

