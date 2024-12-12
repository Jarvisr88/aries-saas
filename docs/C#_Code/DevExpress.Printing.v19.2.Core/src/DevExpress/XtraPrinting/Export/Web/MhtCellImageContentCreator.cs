namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Drawing;

    internal class MhtCellImageContentCreator : HtmlCellImageContentCreator
    {
        private IScriptContainer scriptContainer;

        public MhtCellImageContentCreator(IImageRepository imageRepository, IScriptContainer scriptContainer) : base(imageRepository)
        {
            this.scriptContainer = scriptContainer;
        }

        protected override void ProcessImage(DXWebControlBase imgContainer, DXWebControlBase imageControl, Image image)
        {
            imgContainer.Controls.Add(imageControl);
            DXHtmlImage image2 = imageControl as DXHtmlImage;
            if ((image2 != null) && IsPng(image))
            {
                image2.Style.Add("filter", "expression(fixPng(this))");
                string script = @"function fixPng(element) {if(/MSIE (5\.5|6).+Win/.test(navigator.userAgent)) {if(element.tagName=='IMG' && /\.png$/.test(element.src)) {var src = partlyEscape(element.src);element.src = '" + base.GetBlankGifSrc() + "';element.style.filter = \"progid:DXImageTransform.Microsoft.AlphaImageLoader(src='\" + src + \"',sizingMethod='scale')\";}}}function partlyEscape(s) {var parts = s.split('!');var arr = parts[0].split(/[\\\\\\/]/);for(var i = 3; i < arr.length; i++)arr[i] = escape(arr[i]);return arr.join('/') + '!' + parts[1];}";
                if (!this.scriptContainer.IsClientScriptBlockRegistered("fixPng"))
                {
                    this.scriptContainer.RegisterClientScriptBlock("fixPng", script);
                }
            }
        }

        protected override bool ValidateImageSrc(Image image, ref string imageSrc)
        {
            imageSrc = base.imageRepository.GetImageSource(image, false);
            return !string.IsNullOrEmpty(imageSrc);
        }
    }
}

