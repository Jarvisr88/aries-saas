namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows;

    internal abstract class BrickCreator : IBrickCreator
    {
        protected readonly PrintingSystemBase ps;
        protected readonly Dictionary<BrickStyleKey, BrickStyle> brickStyles;
        private readonly Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters;

        public BrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters)
        {
            Guard.ArgumentNotNull(ps, "ps");
            Guard.ArgumentNotNull(brickStyles, "brickStyles");
            Guard.ArgumentNotNull(onPageUpdaters, "onPageUpdaters");
            this.ps = ps;
            this.brickStyles = brickStyles;
            this.onPageUpdaters = onPageUpdaters;
        }

        public abstract VisualBrick Create(UIElement source, UIElement parent);
        private static BrickStyle CreateBrickStyle(BrickStyleKey styleKey)
        {
            BrickStyle style = BrickStyle.CreateDefault();
            style.BackColor = DrawingConverter.ToGdiColor(styleKey.BackColor);
            style.ForeColor = DrawingConverter.ToGdiColor(styleKey.ForeColor);
            style.BorderColor = DrawingConverter.ToGdiColor(styleKey.BorderColor);
            style.BorderStyle = BrickBorderStyle.Inset;
            style.BorderDashStyle = styleKey.BorderDashStyle;
            InitializeBorder(style, styleKey.BorderThickness);
            if (styleKey.FontFamily != null)
            {
                style.Font = DrawingConverter.CreateGdiFont(styleKey.FontFamily, styleKey.FontStyle, styleKey.FontWeight, styleKey.TextDecorations, (double) ((float) styleKey.FontSize));
                style.Padding = new PaddingInfo((int) styleKey.Padding.Left, (int) styleKey.Padding.Right, (int) styleKey.Padding.Top, (int) styleKey.Padding.Bottom, GraphicsUnit.Pixel);
                style.TextAlignment = DrawingConverter.ToXtraPrintingTextAlignment(styleKey.HorizontalAlignment, styleKey.VerticalAlignment);
                StringFormatFlags formatFlags = StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox;
                if (styleKey.TextWrapping == TextWrapping.NoWrap)
                {
                    formatFlags |= StringFormatFlags.NoWrap;
                }
                if (styleKey.FlowDirection == FlowDirection.RightToLeft)
                {
                    formatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                style.StringFormat = BrickStringFormat.Create(style.TextAlignment, formatFlags, DrawingConverter.ToStringTrimming(styleKey.TextTrimming));
            }
            return style;
        }

        private static BrickStyleKey CreateBrickStyleKey(IExportSettings exportSettings)
        {
            BrickStyleKey key = new BrickStyleKey {
                BackColor = exportSettings.Background,
                ForeColor = exportSettings.Foreground,
                BorderColor = exportSettings.BorderColor,
                BorderThickness = exportSettings.BorderThickness,
                BorderDashStyle = exportSettings.BorderDashStyle
            };
            ITextExportSettings settings = exportSettings as ITextExportSettings;
            if (settings != null)
            {
                key.FontFamily = settings.FontFamily;
                key.FontStyle = settings.FontStyle;
                key.FontWeight = settings.FontWeight;
                key.FontSize = settings.FontSize;
                key.Padding = settings.Padding;
                key.HorizontalAlignment = settings.HorizontalAlignment;
                key.VerticalAlignment = settings.VerticalAlignment;
                key.TextWrapping = settings.TextWrapping;
                key.TextDecorations = GetBrickTextDecorations(settings.TextDecorations);
                key.TextTrimming = settings.TextTrimming;
                key.FlowDirection = settings.FlowDirection;
            }
            return key;
        }

        internal static BrickTextDecorations GetBrickTextDecorations(TextDecorationCollection decorations)
        {
            BrickTextDecorations none = BrickTextDecorations.None;
            if (decorations.Contains(TextDecorations.Underline.First<TextDecoration>()))
            {
                none |= BrickTextDecorations.Underline;
            }
            if (decorations.Contains(TextDecorations.Strikethrough.First<TextDecoration>()))
            {
                none |= BrickTextDecorations.Strikethrough;
            }
            return none;
        }

        protected virtual Rect GetElementRect(UIElement element, UIElement parent) => 
            GetRelativeElementRect(element, parent);

        private static Rect GetRelativeElementRect(UIElement element, UIElement parent) => 
            element.TransformToVisual(parent).TransformBounds(new Rect(new System.Windows.Point(0.0, 0.0), element.RenderSize));

        private static void InitializeBorder(BrickStyle style, Thickness thickness)
        {
            double[] numArray = new double[] { thickness.Left, thickness.Top, thickness.Right, thickness.Bottom };
            double num = 0.0;
            double[] numArray2 = numArray;
            int index = 0;
            while (true)
            {
                if (index < numArray2.Length)
                {
                    double num3 = numArray2[index];
                    if (num3 <= 0.0)
                    {
                        index++;
                        continue;
                    }
                    num = num3;
                }
                if (num > 0.0)
                {
                    foreach (double num5 in numArray)
                    {
                        if ((num5 > 0.0) && (num5 != num))
                        {
                            throw new NotSupportedException("All rectangle borders must have the same thickness. In addition, any border can have a zero thickness, which means that this border is invisible.");
                        }
                    }
                }
                style.BorderWidth = (float) num;
                BorderSide[] sideArray = new BorderSide[] { BorderSide.Left, BorderSide.Top, BorderSide.Right, BorderSide.Bottom };
                style.Sides = BorderSide.None;
                for (int i = 0; i < sideArray.Length; i++)
                {
                    if (numArray[i] > 0.0)
                    {
                        style.Sides |= sideArray[i];
                    }
                }
                return;
            }
        }

        protected void InitializeBrickCore(UIElement source, UIElement parent, VisualBrick brick, IExportSettings exportSettings)
        {
            Rect elementRect = this.GetElementRect(source, parent);
            brick.Initialize(this.ps, GraphicsUnitConverter2.PixelToDoc(new RectangleF((float) elementRect.X, (float) elementRect.Y, (float) elementRect.Width, (float) elementRect.Height)));
            brick.RightToLeftLayout = this.ps.Document.RightToLeftLayout;
            brick.Url = exportSettings.Url;
            string elementTag = ExportSettings.GetElementTag(source);
            if (elementTag != null)
            {
                brick.Value = elementTag;
            }
            BrickStyleKey key = CreateBrickStyleKey(exportSettings);
            BrickStyle style = null;
            if (!this.brickStyles.TryGetValue(key, out style))
            {
                style = CreateBrickStyle(key);
                this.brickStyles.Add(key, style);
            }
            brick.Style = style;
            if (exportSettings.MergeValue != null)
            {
                brick.SetAttachedValue<object>(BrickAttachedProperties.MergeValue, exportSettings.MergeValue);
            }
            IOnPageUpdater onPageUpdater = exportSettings.OnPageUpdater;
            if (onPageUpdater != null)
            {
                this.onPageUpdaters[brick] = onPageUpdater;
            }
        }
    }
}

