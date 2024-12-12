namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Drawing;

    public class CssHtmlCellImageContentCreator : HtmlCellImageContentCreator
    {
        private readonly bool inlineCss;

        public CssHtmlCellImageContentCreator(DevExpress.XtraPrinting.Export.Web.CssImageRepository imageRepository, bool inlineCss) : base(imageRepository)
        {
            this.inlineCss = inlineCss;
        }

        protected override DXWebControlBase CreateHtmlImage(Image image, Size htmlImageSize, string imageSrc)
        {
            if ((image != null) && ((image.Size == htmlImageSize) && !this.inlineCss))
            {
                DXHtmlDivision control = new DXHtmlDivision();
                base.SetControlSize(control, htmlImageSize);
                string classNameByImage = this.CssImageRepository.GetClassNameByImage(image);
                control.CssClass = !string.IsNullOrEmpty(control.CssClass) ? (control.CssClass + " " + classNameByImage) : classNameByImage;
                return control;
            }
            if ((image != null) && (base.imageRepository != null))
            {
                imageSrc = base.imageRepository.GetImageSource(image, false);
            }
            return base.CreateHtmlImage(image, htmlImageSize, imageSrc);
        }

        protected internal override string GetWatermarkImageSrc(ImageSource imageSource)
        {
            Image image = ImageSource.GetImage(imageSource);
            return this.CssImageRepository.GetWatermarkDataByImage(image);
        }

        protected override void ProcessImage(DXWebControlBase imgContainer, DXWebControlBase imageControl, Image image)
        {
            DXHtmlControl control = imageControl as DXHtmlControl;
            if ((control == null) || ((image == null) || (this.CssImageRepository.GetStreamLength(image) < 0x8000)))
            {
                imgContainer.Controls.Add(imageControl);
            }
            else
            {
                imgContainer.Controls.Add(new DXHtmlLiteralControl("<!--[if lt IE 9]><div style=\"display:none\"><![endif]-->"));
                imgContainer.Controls.Add(imageControl);
                imgContainer.Controls.Add(new DXHtmlLiteralControl("<!--[if lt IE 9]></div>"));
                DXHtmlImage child = new DXHtmlImage {
                    Style = { 
                        ["width"] = control.Style["width"],
                        ["height"] = control.Style["height"]
                    },
                    Src = "bad://"
                };
                child.ToolTip = child.Alt = "Internet Explorer can't display big images.";
                imgContainer.Controls.Add(child);
                imgContainer.Controls.Add(new DXHtmlLiteralControl("<![endif]-->"));
            }
        }

        protected override bool ValidateImageSrc(Image image, ref string imageSrc) => 
            true;

        private DevExpress.XtraPrinting.Export.Web.CssImageRepository CssImageRepository =>
            (DevExpress.XtraPrinting.Export.Web.CssImageRepository) base.imageRepository;
    }
}

