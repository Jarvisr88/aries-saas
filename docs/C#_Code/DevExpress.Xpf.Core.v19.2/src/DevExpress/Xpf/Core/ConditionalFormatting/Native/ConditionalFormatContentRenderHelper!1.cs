namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ConditionalFormatContentRenderHelper<T> where T: FrameworkElement, IChrome
    {
        private static readonly RenderTemplate conditionalFormatTemplate;
        public const int ImageLeftRightMargin = 1;
        private readonly T owner;
        private readonly ISupportHorizonalContentAlignment horizonalContentAlignmentProvider;
        private RenderPanelContext context;
        private Thickness margin;
        private DevExpress.Xpf.Core.ConditionalFormatting.Native.DataBarFormatInfo dataBarFormatInfo;

        static ConditionalFormatContentRenderHelper()
        {
            RenderPanel panel1 = new RenderPanel();
            panel1.LayoutProvider = LayoutProvider.GridInstance;
            panel1.ShouldCalcDpiAwareThickness = false;
            RenderPanel panel = panel1;
            RenderBorder item = new RenderBorder();
            item.ShouldCalcDpiAwareThickness = false;
            panel.Children.Add(item);
            RenderBorder border2 = new RenderBorder();
            border2.ShouldCalcDpiAwareThickness = false;
            panel.Children.Add(border2);
            RenderImage image1 = new RenderImage();
            image1.Stretch = Stretch.None;
            image1.HorizontalAlignment = HorizontalAlignment.Left;
            image1.VerticalAlignment = VerticalAlignment.Center;
            image1.Margin = new Thickness(1.0, 0.0, 1.0, 0.0);
            image1.ShouldCalcDpiAwareThickness = false;
            panel.Children.Add(image1);
            RenderTemplate template1 = new RenderTemplate();
            template1.RenderTree = panel;
            ConditionalFormatContentRenderHelper<T>.conditionalFormatTemplate = template1;
        }

        public ConditionalFormatContentRenderHelper(T owner)
        {
            this.owner = owner;
            this.horizonalContentAlignmentProvider = owner as ISupportHorizonalContentAlignment;
            this.context = null;
        }

        public void Arrange(Size finalSize)
        {
            if (this.context != null)
            {
                this.context.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
            }
        }

        private W GetRenderChild<W>(int index) where W: FrameworkRenderElementContext
        {
            if (this.context != null)
            {
                return (((IFrameworkRenderElementContext) this.context).GetRenderChild(index) as W);
            }
            return default(W);
        }

        public void InvalidateIconAlignment()
        {
            if (this.IconImage != null)
            {
                Visibility? nullable = this.IconImage.Visibility;
                Visibility collapsed = Visibility.Collapsed;
                if (((((Visibility) nullable.GetValueOrDefault()) == collapsed) ? (nullable == null) : true) && (this.horizonalContentAlignmentProvider != null))
                {
                    HorizontalAlignment? horizontalAlignment = this.IconImage.HorizontalAlignment;
                    HorizontalAlignment alignment = ConditionalFormatContentRenderHelper<T>.ToIconAlignment(this.horizonalContentAlignmentProvider.HorizonalContentAlignment);
                    if ((((HorizontalAlignment) horizontalAlignment.GetValueOrDefault()) == alignment) ? (horizontalAlignment == null) : true)
                    {
                        this.Redraw();
                    }
                }
            }
        }

        public void Measure(Size availableSize)
        {
            if (this.dataBarFormatInfo == null)
            {
                if (this.context != null)
                {
                    this.context.Visibility = 2;
                }
            }
            else
            {
                if (this.context == null)
                {
                    this.context = (RenderPanelContext) ChromeHelper.CreateContext(this.owner, ConditionalFormatContentRenderHelper<T>.conditionalFormatTemplate);
                    this.context.UseLayoutRounding = this.owner.UseLayoutRounding;
                    this.UpdateMargin();
                }
                this.context.Visibility = null;
                this.ValidataDataBar(availableSize);
                this.ValidateIcon();
                this.context.Measure(availableSize);
            }
        }

        private void Redraw()
        {
            this.owner.InvalidateMeasure();
            this.owner.InvalidateArrange();
            this.owner.InvalidateVisual();
        }

        public void Render(DrawingContext dc)
        {
            if (this.context != null)
            {
                this.context.Render(dc);
            }
        }

        public void SetMargin(Thickness margin)
        {
            if (this.margin != margin)
            {
                this.margin = margin;
                if (this.context != null)
                {
                    this.UpdateMargin();
                }
            }
        }

        private static HorizontalAlignment ToIconAlignment(HorizontalAlignment alignment) => 
            (alignment != HorizontalAlignment.Left) ? HorizontalAlignment.Left : HorizontalAlignment.Right;

        public void UpdateDataBarFormatInfo(DevExpress.Xpf.Core.ConditionalFormatting.Native.DataBarFormatInfo info)
        {
            if (!ReferenceEquals(this.dataBarFormatInfo, info))
            {
                this.dataBarFormatInfo = info;
                this.Redraw();
            }
        }

        private void UpdateMargin()
        {
            this.context.Margin = new Thickness?(this.margin);
        }

        private void ValidataDataBar(Size availableSize)
        {
            if ((this.dataBarFormatInfo.Format == null) || double.IsInfinity(availableSize.Width))
            {
                this.BarBorder.Visibility = 2;
                this.ZeroLineBorder.Visibility = 2;
            }
            else
            {
                double num5;
                double num6;
                Visibility? nullable = null;
                this.BarBorder.Visibility = nullable;
                nullable = null;
                this.ZeroLineBorder.Visibility = nullable;
                Thickness margin = this.dataBarFormatInfo.Format.Margin;
                double num = ((this.dataBarFormatInfo.ZeroPosition <= 0.0) || (this.dataBarFormatInfo.ZeroPosition >= 1.0)) ? 0.0 : this.dataBarFormatInfo.Format.ZeroLineThickness;
                double num2 = ((((availableSize.Width - margin.Left) - margin.Right) - this.margin.Left) - this.margin.Right) - num;
                double num3 = Math.Round((double) (this.dataBarFormatInfo.ZeroPosition * num2));
                double num4 = Math.Round((double) (num2 * Math.Abs((double) (this.dataBarFormatInfo.ZeroPosition - Math.Min(1.0, Math.Max(0.0, this.dataBarFormatInfo.ValuePosition))))));
                Brush fill = this.dataBarFormatInfo.Format.Fill;
                Brush borderBrush = this.dataBarFormatInfo.Format.BorderBrush;
                if (this.dataBarFormatInfo.ValuePosition > this.dataBarFormatInfo.ZeroPosition)
                {
                    num5 = num3 + num;
                    num6 = (num2 - num3) - num4;
                }
                else
                {
                    num6 = (num2 - num3) + num;
                    num5 = num3 - num4;
                    fill = this.dataBarFormatInfo.Format.FillNegative ?? fill;
                    borderBrush = this.dataBarFormatInfo.Format.BorderBrushNegative ?? borderBrush;
                }
                this.BarBorder.BorderThickness = new Thickness?(this.dataBarFormatInfo.Format.BorderThickness);
                this.BarBorder.Background = fill;
                this.BarBorder.BorderBrush = borderBrush;
                this.BarBorder.Margin = new Thickness(margin.Left + num5, margin.Top, margin.Right + num6, margin.Bottom);
                this.ZeroLineBorder.Margin = new Thickness(margin.Left + num3, 0.0, (margin.Right + num2) - num3, 0.0);
                this.ZeroLineBorder.Background = this.dataBarFormatInfo.Format.ZeroLineBrush;
            }
        }

        private void ValidateIcon()
        {
            if (this.dataBarFormatInfo.Icon == null)
            {
                this.IconImage.Visibility = 2;
            }
            else
            {
                this.IconImage.Visibility = null;
                this.IconImage.Source = this.dataBarFormatInfo.Icon;
                this.IconImage.VerticalAlignment = new VerticalAlignment?(this.dataBarFormatInfo.IconVerticalAlignment);
                if (this.horizonalContentAlignmentProvider != null)
                {
                    this.IconImage.HorizontalAlignment = new HorizontalAlignment?(ConditionalFormatContentRenderHelper<T>.ToIconAlignment(this.horizonalContentAlignmentProvider.HorizonalContentAlignment));
                }
            }
        }

        public RenderBorderContext BarBorder =>
            this.GetRenderChild<RenderBorderContext>(0);

        public RenderBorderContext ZeroLineBorder =>
            this.GetRenderChild<RenderBorderContext>(1);

        public RenderImageContext IconImage =>
            this.GetRenderChild<RenderImageContext>(2);

        public DevExpress.Xpf.Core.ConditionalFormatting.Native.DataBarFormatInfo DataBarFormatInfo =>
            this.dataBarFormatInfo;
    }
}

