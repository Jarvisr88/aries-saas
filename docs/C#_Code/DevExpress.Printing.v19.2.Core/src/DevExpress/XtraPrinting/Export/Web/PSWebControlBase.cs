namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public abstract class PSWebControlBase : DXHtmlGenericControl
    {
        private const string cssClass = "pagebreak";
        protected Document document;
        protected WebStyleControl styleControl;
        protected ScriptBlockControl scriptControl;
        protected HtmlExportContext htmlExportContext;
        protected HtmlExportOptionsBase options;

        protected PSWebControlBase(Document document, IImageRepository imageRepository, HtmlExportOptionsBase options) : base(DXHtmlTextWriterTag.Unknown)
        {
            Guard.ArgumentNotNull(document, "document");
            Guard.ArgumentNotNull(imageRepository, "imageRepository");
            this.document = document;
            this.styleControl = new WebStyleControl();
            this.scriptControl = this.CreateScriptControl(this.styleControl);
            if (imageRepository is CssImageRepository)
            {
                ((CssImageRepository) imageRepository).ScriptContainer = this.scriptControl;
            }
            this.htmlExportContext = this.CreateExportContext(document.PrintingSystem, this.scriptControl, imageRepository, options);
            this.options = options;
        }

        protected void AddChildrenControl(DXWebControlBase control)
        {
            if (control != null)
            {
                this.Controls.Add(control);
            }
        }

        protected virtual void AddControlsAfterCreatePages()
        {
        }

        protected virtual void AddControlsBeforeCreatePages()
        {
            this.Controls.Add(this.scriptControl);
        }

        protected static void AddPageControls(PSWebControlBase dest, HtmlExportOptionsBase options)
        {
            Document document = dest.document;
            int[] pageIndices = ExportOptionsHelper.GetPageIndices(options, document.PageCount);
            for (int i = 0; i < pageIndices.Length; i++)
            {
                DXHtmlDivision control = new DXHtmlDivision();
                Page page = document.Pages[pageIndices[i]];
                float num2 = (i == 0) ? 0f : document.Pages[pageIndices[i - 1]].PageSize.Width;
                float num3 = (i == (pageIndices.Length - 1)) ? 0f : document.Pages[pageIndices[i + 1]].PageSize.Width;
                BorderSide sides = BorderSide.Right | BorderSide.Left;
                if (num2 <= page.PageSize.Width)
                {
                    sides |= BorderSide.Top;
                }
                if (num3 < page.PageSize.Width)
                {
                    sides |= BorderSide.Bottom;
                }
                string str = PSHtmlStyleRender.GetBorderHtml(options.PageBorderColor, Color.Transparent, sides, options.PageBorderWidth, dest.RightToLeftLayout) + "background-color: " + HtmlConvert.ToHtml(page.Document.PrintingSystem.Graph.PageBackColor);
                control.Attributes["style"] = str;
                Size size = GraphicsUnitConverter.Convert(Size.Round(page.PageSize), (float) 300f, (float) 96f);
                HtmlHelper.SetStyleSize(control.Style, size);
                if (options.ExportWatermarks && PageHasWatermark(document, pageIndices[i]))
                {
                    control.Controls.Add(CreateWatermark(page, dest.htmlExportContext, 0, size.Height, false));
                }
                using (HtmlPageLayoutBuilder builder = new HtmlPageLayoutBuilder(page, dest.htmlExportContext))
                {
                    DXHtmlTable child = dest.CreateHtmlLayoutTable(builder, options.TableLayout);
                    if (child != null)
                    {
                        control.Controls.Add(child);
                    }
                }
                dest.AddChildrenControl(control);
            }
        }

        protected internal override void CreateChildControls()
        {
            if (!base.ChildControlsCreated)
            {
                base.ChildControlsCreated = true;
                this.CreateChildControlsCore();
            }
        }

        protected virtual void CreateChildControlsCore()
        {
            this.Controls.Clear();
            if (this.document != null)
            {
                this.styleControl.ClearContent();
                this.scriptControl.ClearContent();
                this.AddControlsBeforeCreatePages();
                this.CreatePages();
                this.AddControlsAfterCreatePages();
            }
        }

        protected DXHtmlTable CreateContentTable(LayoutControlCollection layoutControls, bool tableLayout) => 
            this.GetHtmlBuilder(tableLayout).BuildTable(layoutControls, this.document.CorrectImportBrickBounds, this.htmlExportContext);

        protected virtual HtmlExportContext CreateExportContext(PrintingSystemBase printingSystem, IScriptContainer scriptContainer, IImageRepository imageRepository, HtmlExportOptionsBase options) => 
            new HtmlExportContext(printingSystem, this.scriptControl, imageRepository, options);

        protected DXHtmlTable CreateHtmlLayoutTable(ILayoutBuilder layoutBuilder, bool tableLayout)
        {
            LayoutControlCollection layoutControls = layoutBuilder.BuildLayoutControls();
            DevExpress.XtraPrinting.ProgressReflector progressReflector = this.ProgressReflector;
            progressReflector.RangeValue++;
            DXHtmlTable table = this.CreateContentTable(layoutControls, tableLayout);
            if (this.htmlExportContext.MainExportMode != HtmlExportMode.SingleFile)
            {
                DevExpress.XtraPrinting.ProgressReflector reflector2 = this.ProgressReflector;
                reflector2.RangeValue++;
            }
            return table;
        }

        protected static DXHtmlDivision CreateImageWatermark(Page page, Size pageSize, HtmlExportContext htmlExportContext, Point offset, bool needClipMargins)
        {
            PageWatermark actualWatermark = page.ActualWatermark;
            string watermarkImageSrc = htmlExportContext.HtmlCellImageContentCreator.GetWatermarkImageSrc(actualWatermark.ImageSource);
            string style = PSHtmlStyleRender.GetHtmlWatermarkImageStyle(pageSize, offset, needClipMargins, watermarkImageSrc, actualWatermark);
            string str3 = htmlExportContext.ScriptContainer.RegisterCssClass(style);
            return new DXHtmlDivision { Attributes = { ["class"] = str3 } };
        }

        public static DXHtmlControl CreatePageBreaker(WebStyleControl styles)
        {
            DXHtmlDivision division = new DXHtmlDivision();
            styles.AddStyle("page-break-after:always;height:0px;width:0px;overflow:hidden;font-size:0px;line-height:0px;", "pagebreak");
            division.CssClass = "pagebreak";
            return division;
        }

        protected abstract void CreatePages();
        protected virtual ScriptBlockControl CreateScriptControl(WebStyleControl styleControl) => 
            new ScriptBlockControl(styleControl);

        protected static DXHtmlDivision CreateTextWatermark(Page page, Size pageSize, HtmlExportContext htmlExportContext, Point offset, bool needClipMargins)
        {
            PageWatermark actualWatermark = page.ActualWatermark;
            SizeF ef = GraphicsUnitConverter.Convert(htmlExportContext.Measurer.MeasureString(actualWatermark.Text, actualWatermark.Font, GraphicsUnit.Pixel), GraphicsDpi.Pixel, (float) 96f);
            string style = PSHtmlStyleRender.GetHtmlWatermarkTextStyle(pageSize, offset, needClipMargins, Size.Round(ef), actualWatermark);
            string str2 = htmlExportContext.ScriptContainer.RegisterCssClass(style);
            return new DXHtmlDivision { 
                Attributes = { ["class"] = str2 },
                Controls = { new DXHtmlLiteralControl(actualWatermark.Text) }
            };
        }

        protected static DXHtmlDivision CreateWatermark(Page page, HtmlExportContext context, int topOffset, int pageHeight, bool needClipMargins)
        {
            DXHtmlDivision division = new DXHtmlDivision();
            Size pageSize = GraphicsUnitConverter.Convert(Size.Round(page.PageSize), (float) 300f, (float) 96f);
            int num = needClipMargins ? (pageSize.Width - GraphicsUnitConverter.Convert((int) (page.Margins.Left + page.Margins.Right), (float) 100f, (float) 96f)) : pageSize.Width;
            division.Attributes["style"] = $"width:{num}px;height:{pageHeight}px;position:absolute;overflow:hidden;z-index:{page.ActualWatermark.ShowBehind ? "0" : "1"};";
            if (!page.ActualWatermark.ShowBehind)
            {
                division.Attributes.Add("onmousedown", "if(!event || !event.target) { return; } event.target.style.display = 'none'; var lowerDiv = document.elementFromPoint(event.clientX, event.clientY); event.target.style.display = 'block'; if(!lowerDiv) { return; } var newEvent = document.createEvent('MouseEvent'); newEvent.initMouseEvent('mousedown', true, true, window, 0, event.screenX, event.screenY, event.clientX, event.clientY, false, false, false, false, 0, null); lowerDiv.dispatchEvent(newEvent);");
            }
            Point offset = new Point(GraphicsUnitConverter.Convert(page.Margins.Left, 100f, 96f), topOffset);
            if (!ImageSource.IsNullOrEmpty(page.ActualWatermark.ImageSource))
            {
                division.Controls.Add(CreateImageWatermark(page, pageSize, context, offset, needClipMargins));
            }
            if (!string.IsNullOrEmpty(page.ActualWatermark.Text))
            {
                division.Controls.Add(CreateTextWatermark(page, pageSize, context, offset, needClipMargins));
            }
            return division;
        }

        protected virtual HtmlBuilderBase GetHtmlBuilder(bool tableLayout) => 
            tableLayout ? ((HtmlBuilderBase) new HtmlTableBuilder()) : ((HtmlBuilderBase) new HtmlDivBuilder());

        protected static bool PageHasWatermark(Document document, int pageIndex) => 
            !document.Pages[pageIndex].ActualWatermark.IsEmpty && document.PrintingSystem.Watermark.NeedDraw(pageIndex, document.PageCount);

        protected internal override void Render(DXHtmlTextWriter writer)
        {
            this.CreateChildControls();
            base.RenderContents(writer);
        }

        protected DevExpress.XtraPrinting.ProgressReflector ProgressReflector =>
            this.document.ProgressReflector;

        public WebStyleControl Styles =>
            this.styleControl;

        public bool RightToLeftLayout =>
            this.htmlExportContext.RightToLeftLayout;
    }
}

