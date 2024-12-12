namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    internal abstract class PageSetupControlController
    {
        private GraphicsUnit currentUnit;

        protected virtual SizeF GetActualPageSize() => 
            (this.PaperKind == System.Drawing.Printing.PaperKind.Custom) ? this.CustomPageSize : PageSizeInfo.GetPageSizeF(this.PaperKind, this.currentUnit.ToDpi());

        private static string GetFormatString(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.Pixel:
                    return ("{0:0} " + PreviewLocalizer.GetString(PreviewStringId.Margin_Pixel));

                case GraphicsUnit.Inch:
                    return "{0:0.##} ''";

                case GraphicsUnit.Millimeter:
                    return ("{0:0} " + PreviewLocalizer.GetString(PreviewStringId.Margin_Millimeter));
            }
            throw new InvalidOperationException();
        }

        private static string GetMask(GraphicsUnit unit)
        {
            switch (unit)
            {
                case GraphicsUnit.Pixel:
                case GraphicsUnit.Millimeter:
                    return "d";

                case GraphicsUnit.Inch:
                    return "f";
            }
            throw new InvalidOperationException();
        }

        protected void OnMarginChanged(MarginSide side)
        {
            this.Unsubscribe();
            switch (side)
            {
                case MarginSide.Left:
                {
                    float num = Math.Max(0f, this.LeftMargin);
                    this.LeftMargin = Math.Min(num, (this.PaperWidth - this.RightMargin) - this.MinPageSize.Width);
                    break;
                }
                case MarginSide.Top:
                {
                    float num3 = Math.Max(0f, this.TopMargin);
                    this.TopMargin = Math.Min(num3, (this.PaperHeight - this.BottomMargin) - this.MinPageSize.Height);
                    break;
                }
                case (MarginSide.Top | MarginSide.Left):
                    break;

                case MarginSide.Right:
                {
                    float num2 = Math.Max(0f, this.RightMargin);
                    this.RightMargin = Math.Min(num2, (this.PaperWidth - this.LeftMargin) - this.MinPageSize.Width);
                    break;
                }
                default:
                    if (side == MarginSide.Bottom)
                    {
                        float num4 = Math.Max(0f, this.BottomMargin);
                        this.BottomMargin = Math.Min(num4, (this.PaperHeight - this.TopMargin) - this.MinPageSize.Height);
                    }
                    break;
            }
            this.RefreshViewer();
            this.Subscribe();
        }

        protected void OnOrientationChanged()
        {
            this.Unsubscribe();
            float paperWidth = this.PaperWidth;
            this.PaperWidth = this.PaperHeight;
            this.PaperHeight = paperWidth;
            if (this.Landscape)
            {
                float leftMargin = this.LeftMargin;
                this.LeftMargin = this.BottomMargin;
                this.BottomMargin = this.RightMargin;
                this.RightMargin = this.TopMargin;
                this.TopMargin = leftMargin;
            }
            else
            {
                float topMargin = this.TopMargin;
                this.TopMargin = this.RightMargin;
                this.RightMargin = this.BottomMargin;
                this.BottomMargin = this.LeftMargin;
                this.LeftMargin = topMargin;
            }
            this.RefreshViewer();
            this.Subscribe();
        }

        protected void OnPaperKindChanged()
        {
            this.Unsubscribe();
            SizeF prevPageSize = new SizeF(this.PaperWidth, this.PaperHeight);
            SizeF actualPageSize = this.GetActualPageSize();
            if (this.Landscape)
            {
                this.PaperWidth = actualPageSize.Height;
                this.PaperHeight = actualPageSize.Width;
            }
            else
            {
                this.PaperWidth = actualPageSize.Width;
                this.PaperHeight = actualPageSize.Height;
            }
            this.ValidateMargins(prevPageSize);
            this.RefreshViewer();
            this.Subscribe();
        }

        protected void OnPaperSizeChanged()
        {
            this.Unsubscribe();
            if (this.PaperKind == System.Drawing.Printing.PaperKind.Custom)
            {
                this.ValidateMargins(this.CustomPageSize);
                this.CustomPageSize = new SizeF(this.PaperWidth, this.PaperHeight);
            }
            this.RefreshViewer();
            this.Subscribe();
        }

        protected void OnUnitChanged()
        {
            if (this.Unit != this.currentUnit)
            {
                this.Unsubscribe();
                this.CustomPageSize = GraphicsUnitConverter.Convert(this.CustomPageSize, this.currentUnit, this.Unit);
                this.PaperWidth = GraphicsUnitConverter.Convert(this.PaperWidth, this.currentUnit, this.Unit);
                this.PaperHeight = GraphicsUnitConverter.Convert(this.PaperHeight, this.currentUnit, this.Unit);
                this.LeftMargin = GraphicsUnitConverter.Convert(this.LeftMargin, this.currentUnit, this.Unit);
                this.RightMargin = GraphicsUnitConverter.Convert(this.RightMargin, this.currentUnit, this.Unit);
                this.TopMargin = GraphicsUnitConverter.Convert(this.TopMargin, this.currentUnit, this.Unit);
                this.BottomMargin = GraphicsUnitConverter.Convert(this.BottomMargin, this.currentUnit, this.Unit);
                this.currentUnit = this.Unit;
                this.SetDisplayFormat(GetMask(this.currentUnit), GetFormatString(this.currentUnit));
                this.RefreshViewer();
                this.Subscribe();
            }
        }

        protected abstract void RefreshViewer();
        protected abstract void SetDisplayFormat(string mask, string formatString);
        public void Start(System.Drawing.Printing.PaperKind paperKind, SizeF paperSize, bool landscape, GraphicsUnit unit, MarginsFloat margins)
        {
            this.Unsubscribe();
            this.CustomPageSize = paperSize;
            this.Unit = this.currentUnit = unit;
            this.PaperKind = paperKind;
            this.PaperWidth = paperSize.Width;
            this.PaperHeight = paperSize.Height;
            this.Landscape = landscape;
            this.LeftMargin = margins.Left;
            this.RightMargin = margins.Right;
            this.TopMargin = margins.Top;
            this.BottomMargin = margins.Bottom;
            this.SetDisplayFormat(GetMask(this.currentUnit), GetFormatString(this.currentUnit));
            this.RefreshViewer();
            this.Subscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
        private void ValidateMargins(SizeF prevPageSize)
        {
            float num2 = this.PaperHeight - (this.TopMargin + this.BottomMargin);
            if (((this.PaperWidth - (this.LeftMargin + this.RightMargin)) < this.MinPageSize.Width) || (num2 < this.MinPageSize.Height))
            {
                float num3 = Math.Min((float) (this.PaperWidth / prevPageSize.Width), (float) (this.PaperHeight / prevPageSize.Height));
                this.LeftMargin *= num3;
                this.RightMargin *= num3;
                this.TopMargin *= num3;
                this.BottomMargin *= num3;
            }
        }

        protected SizeF MinPageSize =>
            GraphicsUnitConverter.Convert(PrintingDocument.MinPageSize, GraphicsUnit.Document, this.currentUnit);

        protected SizeF CustomPageSize { get; set; }

        protected abstract System.Drawing.Printing.PaperKind PaperKind { get; set; }

        protected abstract float PaperWidth { get; set; }

        protected abstract float PaperHeight { get; set; }

        protected abstract GraphicsUnit Unit { get; set; }

        protected abstract bool Landscape { get; set; }

        protected abstract float LeftMargin { get; set; }

        protected abstract float RightMargin { get; set; }

        protected abstract float TopMargin { get; set; }

        protected abstract float BottomMargin { get; set; }
    }
}

