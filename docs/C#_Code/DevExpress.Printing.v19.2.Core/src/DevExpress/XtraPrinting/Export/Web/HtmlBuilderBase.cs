namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public abstract class HtmlBuilderBase : IHtmlExportProvider, ITableExportProvider
    {
        protected string emptyCellContent;
        protected HtmlExportContext fHtmlExportContext;
        protected BrickViewData fCurrentData;
        protected DXHtmlContainerControl fCurrentCell;
        protected Rectangle fCurrentCellBounds;
        private INavigationService navigationService;

        protected HtmlBuilderBase()
        {
        }

        private static PaddingInfo AdjustPadding(PaddingInfo padding, TextAlignment textAlignment)
        {
            Enum[] flags = new Enum[] { TextAlignment.TopLeft, TextAlignment.MiddleLeft, TextAlignment.BottomLeft };
            if (textAlignment.HasAnyFlag(flags))
            {
                padding.Right = 0;
            }
            Enum[] enumArray2 = new Enum[] { TextAlignment.TopCenter, TextAlignment.TopJustify, TextAlignment.TopLeft, TextAlignment.TopRight };
            if (textAlignment.HasAnyFlag(enumArray2))
            {
                padding.Bottom = 0;
            }
            padding.Dpi = 96f;
            return padding;
        }

        protected virtual DXHtmlTable Build(HtmlExportContext htmlExportContext)
        {
            this.fHtmlExportContext = htmlExportContext;
            string str = htmlExportContext.ScriptContainer.RegisterCssClass("height:0px;width:0px;overflow:hidden;font-size:0px;line-height:0px;");
            this.emptyCellContent = $"<!--[if lte IE 7]><div class="{str}"></div><![endif]-->";
            DXHtmlTable table = this.CreateHtmlTable();
            if (htmlExportContext.MainExportMode == HtmlExportMode.SingleFile)
            {
                htmlExportContext.ProgressReflector.Do(this.CountObjects, delegate {
                    this.SetupSpans(table);
                });
            }
            else
            {
                this.SetupSpans(table);
            }
            return (!this.fHtmlExportContext.CancelPending ? table : null);
        }

        public abstract DXHtmlTable BuildTable(LayoutControlCollection layoutControls, bool correctImportBrickBounds, HtmlExportContext htmlExportContext);
        protected static HtmlCellLayout CalculateCellLayout(HtmlCellLayout baselayout, RectangleF bounds, RectangleF originalBounds)
        {
            HtmlCellLayout areaLayout = new HtmlCellLayout();
            new LayoutCalculatorHoriz(areaLayout).Calculate((int) bounds.Left, (int) bounds.Right, (int) originalBounds.Left, (int) originalBounds.Right, baselayout);
            new LayoutCalculatorVert(areaLayout).Calculate((int) bounds.Top, (int) bounds.Bottom, (int) originalBounds.Top, (int) originalBounds.Bottom, baselayout);
            return areaLayout;
        }

        protected Rectangle CalculateCellRect(Rectangle bounds, HtmlCellLayout areaLayout)
        {
            Rectangle rect = bounds;
            if (areaLayout != null)
            {
                rect = RectHelper.DeflateRect(RectHelper.DeflateRect(rect, areaLayout.Padding.Left, areaLayout.Padding.Top, areaLayout.Padding.Right, areaLayout.Padding.Bottom), areaLayout.Borders.Left, areaLayout.Borders.Top, areaLayout.Borders.Right, areaLayout.Borders.Bottom);
            }
            return rect;
        }

        protected static int CalculateElement(int x0, int x1, int nearValue, int farValue) => 
            Math.Max(Math.Min(x1, farValue) - Math.Max(x0, nearValue), 0);

        protected void ClippingControl(BrickViewData data, Rectangle fCurrentCellBounds, DXHtmlContainerControl control, HtmlCellLayout baselayout)
        {
            Rectangle rectangle = this.CalculateCellRect(data.OriginalBounds, baselayout);
            Point offset = new Point(rectangle.X - fCurrentCellBounds.X, rectangle.Y - fCurrentCellBounds.Y);
            ClipControl control2 = HtmlHelper.SetClip(control, offset, fCurrentCellBounds.Size, rectangle.Size);
            if ((data.Style != null) && this.fHtmlExportContext.CopyStyleWhenClipping)
            {
                BrickStyle style = (BrickStyle) data.Style.Clone();
                style.BorderWidth = 0f;
                style.Sides = BorderSide.None;
                style.Padding = PaddingInfo.Empty;
                control2.InnerControlCSSClass = this.RegisterHtmlClassName(style, PSHtmlStyleRender.GetBorders(style), PaddingInfo.Empty);
            }
        }

        protected DXHtmlLiteralControl CreateEmptyCellControl() => 
            new DXHtmlLiteralControl(this.emptyCellContent);

        protected abstract DXHtmlTable CreateHtmlTable();
        void IHtmlExportProvider.RaiseHtmlItemCreated(VisualBrick brick)
        {
            brick.BrickOwner.RaiseHtmlItemCreated(brick, this.fHtmlExportContext.ScriptContainer, this.fCurrentCell);
        }

        void IHtmlExportProvider.SetAnchor(string anchorName)
        {
            VisualBrickExporter.SetHtmlAnchor(this.fCurrentCell, anchorName, this.fHtmlExportContext);
        }

        void IHtmlExportProvider.SetNavigationUrl(VisualBrick brick)
        {
            if (this.fHtmlExportContext.AllowURLsWithJSContent || (!string.IsNullOrEmpty(brick.Url) && (brick.Url.IndexOf("javascript:", StringComparison.OrdinalIgnoreCase) < 0)))
            {
                if (this.fHtmlExportContext.UseHRefHyperlinks)
                {
                    this.SetNavigationUrlUsingHRef(brick);
                }
                else
                {
                    this.SetNavigationUrlUsingScripts(brick);
                }
            }
        }

        void ITableExportProvider.SetCellImage(System.Drawing.Image image, TableCellImageInfo imageInfo, Rectangle bounds, PaddingInfo padding)
        {
            ImageBrick tableCell = this.fCurrentData.TableCell as ImageBrick;
            this.fHtmlExportContext.HtmlCellImageContentCreator.CreateContent(this.fCurrentCell, image, tableCell?.HtmlImageUrl, imageInfo.SizeMode, imageInfo.Alignment, bounds, imageInfo.ImageSize, padding);
        }

        void ITableExportProvider.SetCellShape(ShapeBase shape, TableCellLineInfo lineInfo, Color fillColor, int angle, PaddingInfo padding)
        {
        }

        void ITableExportProvider.SetCellText(object text)
        {
            string str = text as string;
            if (!string.IsNullOrEmpty(str))
            {
                new HtmlCellTextContentCreator(this.fCurrentCell).CreateContent(str, this.fCurrentData.Style, this.fCurrentData.OriginalBoundsF, this.fHtmlExportContext.Measurer);
            }
        }

        protected void FillCellContent(BrickViewData data, DXHtmlContainerControl control)
        {
            ITableCell tableCell = data.TableCell;
            HtmlCellLayout baselayout = new HtmlCellLayout(data.Style) {
                Padding = tableCell.ShouldApplyPadding ? AdjustPadding(data.Style.Padding, data.Style.TextAlignment) : PaddingInfo.Empty
            };
            HtmlCellLayout areaLayout = NeedClipping(data) ? CalculateCellLayout(baselayout, data.Bounds, data.OriginalBounds) : baselayout;
            if (data.Style != null)
            {
                if (!this.fHtmlExportContext.InlineCss)
                {
                    control.Attributes["class"] = this.RegisterHtmlClassName(data.Style, areaLayout.Borders, areaLayout.Padding);
                }
                else
                {
                    DXCssStyleCollection style = control.Style;
                    style.Value = style.Value + PSHtmlStyleRender.GetHtmlStyle(data.Style.Font, data.Style.ForeColor, data.Style.BackColor, data.Style.BorderColor, areaLayout.Borders, areaLayout.Padding, data.Style.BorderDashStyle, this.fHtmlExportContext.RightToLeftLayout);
                }
            }
            this.fCurrentCellBounds = this.CalculateCellRect(data.Bounds, areaLayout);
            HtmlHelper.SetStyleSize(control.Style, this.fCurrentCellBounds.Size);
            this.fCurrentCell = control;
            this.fCurrentData = data;
            ((BrickExporter) this.fHtmlExportContext.PrintingSystem.ExportersFactory.GetExporter(tableCell)).FillHtmlTableCell(this);
            if (NeedClipping(data))
            {
                this.ClippingControl(data, this.fCurrentCellBounds, control, baselayout);
            }
        }

        protected static bool NeedClipping(BrickViewData data) => 
            data.OriginalBounds != data.Bounds;

        protected string RegisterHtmlClassName(BrickStyle style, PaddingInfo borders, PaddingInfo padding)
        {
            if (style == null)
            {
                return string.Empty;
            }
            string str = PSHtmlStyleRender.GetHtmlStyle(style.Font, style.ForeColor, style.BackColor, style.BorderColor, borders, padding, style.BorderDashStyle, this.fHtmlExportContext.RightToLeftLayout);
            return this.fHtmlExportContext.ScriptContainer.RegisterCssClass(str);
        }

        private void SetNavigationUrlUsingHRef(VisualBrick brick)
        {
            string url = brick.Url;
            if (!string.IsNullOrEmpty(url))
            {
                DXHtmlAnchor child = new DXHtmlAnchor();
                if (brick.Target.Equals("_self"))
                {
                    url = "#" + url;
                }
                child.HRef = url;
                int num = this.fCurrentCell.Controls.Count - 1;
                while (true)
                {
                    if (num < 0)
                    {
                        child.Style.Add(DXHtmlTextWriterStyle.Color, HtmlConvert.ToHtml(brick.Style.ForeColor));
                        child.Style.Add(DXHtmlTextWriterStyle.TextDecoration, "none");
                        this.fCurrentCell.Controls.Add(child);
                        break;
                    }
                    child.Controls.AddAt(0, this.fCurrentCell.Controls[num]);
                    num--;
                }
            }
        }

        private void SetNavigationUrlUsingScripts(VisualBrick brick)
        {
            string mouseDownScript = this.NavigationService.GetMouseDownScript(this.fHtmlExportContext, brick);
            if (!string.IsNullOrEmpty(mouseDownScript))
            {
                this.fHtmlExportContext.RegisterNavigationScript();
                this.fCurrentCell.Style.Add("cursor", "pointer");
                this.fCurrentCell.Attributes.Add("onmousedown", mouseDownScript);
            }
        }

        protected abstract void SetupSpans(DXHtmlTable table);

        private INavigationService NavigationService
        {
            get
            {
                if (this.navigationService == null)
                {
                    this.navigationService = this.fHtmlExportContext.PrintingSystem.GetService<INavigationService>();
                    this.navigationService ??= new WebNavigationService();
                }
                return this.navigationService;
            }
        }

        ExportContext ITableExportProvider.ExportContext =>
            this.fHtmlExportContext;

        HtmlExportContext IHtmlExportProvider.HtmlExportContext =>
            this.fHtmlExportContext;

        DXHtmlContainerControl IHtmlExportProvider.CurrentCell =>
            this.fCurrentCell;

        Rectangle IHtmlExportProvider.CurrentCellBounds =>
            this.fCurrentCellBounds;

        BrickViewData ITableExportProvider.CurrentData =>
            this.fCurrentData;

        protected abstract int CountObjects { get; }

        public class HtmlCellLayout
        {
            public PaddingInfo Borders;
            public PaddingInfo Padding;

            public HtmlCellLayout()
            {
            }

            public HtmlCellLayout(BrickStyle style)
            {
                if (style != null)
                {
                    this.Borders.Left = GetBorderWidth(style, BorderSide.Left);
                    this.Borders.Right = GetBorderWidth(style, BorderSide.Right);
                    this.Borders.Top = GetBorderWidth(style, BorderSide.Top);
                    this.Borders.Bottom = GetBorderWidth(style, BorderSide.Bottom);
                }
            }

            private static int GetBorderWidth(BrickStyle style, BorderSide side) => 
                ((style.Sides & side) != BorderSide.None) ? ((int) style.BorderWidth) : ((int) 0f);
        }

        private abstract class LayoutCalculator
        {
            private HtmlBuilderBase.HtmlCellLayout calcLayout;

            public LayoutCalculator(HtmlBuilderBase.HtmlCellLayout calcLayout)
            {
                this.calcLayout = calcLayout;
            }

            public void Calculate(int boundsNear, int boundsFar, int baseBoundsNear, int baseBoundsFar, HtmlBuilderBase.HtmlCellLayout baselayout)
            {
                int num = baseBoundsNear + this.GetNearValue(baselayout.Borders);
                int num2 = baseBoundsFar - this.GetFarValue(baselayout.Borders);
                int nearValue = HtmlBuilderBase.CalculateElement(baseBoundsNear, num, boundsNear, boundsFar);
                this.SetBorders(this.calcLayout, nearValue, HtmlBuilderBase.CalculateElement(num2, baseBoundsFar, boundsNear, boundsFar));
                int num7 = HtmlBuilderBase.CalculateElement(num, num + this.GetNearValue(baselayout.Padding), boundsNear, boundsFar);
                this.SetPadding(this.calcLayout, num7, HtmlBuilderBase.CalculateElement(num2 - this.GetFarValue(baselayout.Padding), num2, boundsNear, boundsFar));
            }

            protected abstract int GetFarValue(PaddingInfo padding);
            protected abstract int GetNearValue(PaddingInfo padding);
            protected abstract void SetBorders(HtmlBuilderBase.HtmlCellLayout layout, int nearValue, int farValue);
            protected abstract void SetPadding(HtmlBuilderBase.HtmlCellLayout layout, int nearValue, int farValue);
        }

        private class LayoutCalculatorHoriz : HtmlBuilderBase.LayoutCalculator
        {
            public LayoutCalculatorHoriz(HtmlBuilderBase.HtmlCellLayout areaLayout) : base(areaLayout)
            {
            }

            protected override int GetFarValue(PaddingInfo padding) => 
                padding.Right;

            protected override int GetNearValue(PaddingInfo padding) => 
                padding.Left;

            protected override void SetBorders(HtmlBuilderBase.HtmlCellLayout layout, int nearValue, int farValue)
            {
                layout.Borders.Left = nearValue;
                layout.Borders.Right = farValue;
            }

            protected override void SetPadding(HtmlBuilderBase.HtmlCellLayout layout, int nearValue, int farValue)
            {
                layout.Padding.Left = nearValue;
                layout.Padding.Right = farValue;
            }
        }

        private class LayoutCalculatorVert : HtmlBuilderBase.LayoutCalculator
        {
            public LayoutCalculatorVert(HtmlBuilderBase.HtmlCellLayout areaLayout) : base(areaLayout)
            {
            }

            protected override int GetFarValue(PaddingInfo padding) => 
                padding.Bottom;

            protected override int GetNearValue(PaddingInfo padding) => 
                padding.Top;

            protected override void SetBorders(HtmlBuilderBase.HtmlCellLayout layout, int nearValue, int farValue)
            {
                layout.Borders.Top = nearValue;
                layout.Borders.Bottom = farValue;
            }

            protected override void SetPadding(HtmlBuilderBase.HtmlCellLayout layout, int nearValue, int farValue)
            {
                layout.Padding.Top = nearValue;
                layout.Padding.Bottom = farValue;
            }
        }

        protected class Table : ArrayList
        {
            public Table(int rowCount)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    this.Add(new ArrayList());
                }
            }

            public ArrayList this[int rowIndex] =>
                (ArrayList) base[rowIndex];

            public int RowCount =>
                this.Count;
        }
    }
}

