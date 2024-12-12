namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Printing;
    using System.Security;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DrawingConverter
    {
        public static BitmapSource CreateBitmapSource(FrameworkElement element)
        {
            Rect rectangle = new Rect(0.0, 0.0, Math.Round(element.ActualWidth), Math.Round(element.ActualHeight));
            if ((rectangle.Width == 0.0) || (rectangle.Height == 0.0))
            {
                return null;
            }
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(element), null, rectangle);
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int) rectangle.Width, (int) rectangle.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        internal static Font CreateGdiFont(System.Windows.Media.FontFamily family, System.Windows.FontStyle style, FontWeight weight, BrickTextDecorations textDecorations, double size)
        {
            if (family == null)
            {
                throw new ArgumentNullException("family");
            }
            Font font = new Font(family.FamilyNames.Values.First<string>(), GetFontSizeInPoints(size), ToGdiFontStyle(style, weight, textDecorations));
            if (GetFontFamilyName(font.FontFamily) != family.Source)
            {
                Font font2 = new Font(family.Source, GetFontSizeInPoints(size), ToGdiFontStyle(style, weight, textDecorations));
                if (GetFontFamilyName(font2.FontFamily) == family.Source)
                {
                    return font2;
                }
            }
            return font;
        }

        public static Image CreateGdiImage(FrameworkElement element)
        {
            BitmapSource bitmapSource = CreateBitmapSource(element);
            return ((bitmapSource != null) ? FromBitmapSource(bitmapSource) : null);
        }

        public static Image FromBitmapSource(BitmapSource bitmapSource)
        {
            Guard.ArgumentNotNull(bitmapSource, "bitmapSource");
            BitmapEncoder encoder = new PngBitmapEncoder {
                Frames = { BitmapFrame.Create(bitmapSource) }
            };
            MemoryStream stream = new MemoryStream();
            encoder.Save(stream);
            return (Bitmap) Image.FromStream(stream);
        }

        public static System.Windows.Media.Color FromGdiColor(System.Drawing.Color color) => 
            System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

        [SecuritySafeCritical]
        public static PageMediaSizeName FromPaperKind(PaperKind paperKind)
        {
            switch (paperKind)
            {
                case PaperKind.Custom:
                case PaperKind.LetterSmall:
                case PaperKind.Tabloid:
                case PaperKind.Ledger:
                case PaperKind.Statement:
                case PaperKind.Executive:
                case PaperKind.A4Small:
                    break;

                case PaperKind.Letter:
                    return PageMediaSizeName.NorthAmericaLetter;

                case PaperKind.Legal:
                    return PageMediaSizeName.NorthAmericaLegal;

                case PaperKind.A3:
                    return PageMediaSizeName.ISOA3;

                case PaperKind.A4:
                    return PageMediaSizeName.ISOA4;

                case PaperKind.A5:
                    return PageMediaSizeName.ISOA5;

                case PaperKind.B4:
                    return PageMediaSizeName.ISOB4;

                default:
                    if (paperKind != PaperKind.A4Extra)
                    {
                        break;
                    }
                    return PageMediaSizeName.ISOA4Extra;
            }
            return PageMediaSizeName.Unknown;
        }

        private static string GetFontFamilyName(System.Drawing.FontFamily fontFamily) => 
            fontFamily.Name;

        public static float GetFontSizeInPoints(double sizeInDeviceIndependentPixels) => 
            GraphicsUnitConverter.Convert((float) sizeInDeviceIndependentPixels, (float) 96f, (float) 72f);

        public static HorizontalAlignment GetHorizontalAlignment(DevExpress.XtraPrinting.TextAlignment textAlignment)
        {
            if (textAlignment > DevExpress.XtraPrinting.TextAlignment.BottomLeft)
            {
                if (textAlignment > DevExpress.XtraPrinting.TextAlignment.BottomRight)
                {
                    if ((textAlignment == DevExpress.XtraPrinting.TextAlignment.TopJustify) || ((textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleJustify) || (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomJustify)))
                    {
                        return HorizontalAlignment.Stretch;
                    }
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (textAlignment > DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
            {
                if (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (textAlignment)
                {
                    case DevExpress.XtraPrinting.TextAlignment.TopLeft:
                        goto TR_0001;

                    case DevExpress.XtraPrinting.TextAlignment.TopCenter:
                        goto TR_0003;

                    case (DevExpress.XtraPrinting.TextAlignment.TopCenter | DevExpress.XtraPrinting.TextAlignment.TopLeft):
                        break;

                    case DevExpress.XtraPrinting.TextAlignment.TopRight:
                        goto TR_0004;

                    default:
                        if (textAlignment != DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new InvalidOperationException();
        TR_0001:
            return HorizontalAlignment.Left;
        TR_0003:
            return HorizontalAlignment.Center;
        TR_0004:
            return HorizontalAlignment.Right;
        }

        public static VerticalAlignment GetVerticalAlignment(DevExpress.XtraPrinting.TextAlignment textAlignment)
        {
            if (textAlignment > DevExpress.XtraPrinting.TextAlignment.BottomLeft)
            {
                if (textAlignment > DevExpress.XtraPrinting.TextAlignment.BottomRight)
                {
                    if (textAlignment == DevExpress.XtraPrinting.TextAlignment.TopJustify)
                    {
                        goto TR_0003;
                    }
                    else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleJustify)
                    {
                        goto TR_0001;
                    }
                    else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomJustify)
                    {
                        goto TR_0005;
                    }
                }
                else if ((textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomCenter) || (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomRight))
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else if (textAlignment > DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
            {
                if ((textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleCenter) || (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleRight))
                {
                    goto TR_0001;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomLeft)
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else
            {
                switch (textAlignment)
                {
                    case DevExpress.XtraPrinting.TextAlignment.TopLeft:
                    case DevExpress.XtraPrinting.TextAlignment.TopCenter:
                    case DevExpress.XtraPrinting.TextAlignment.TopRight:
                        goto TR_0003;

                    case (DevExpress.XtraPrinting.TextAlignment.TopCenter | DevExpress.XtraPrinting.TextAlignment.TopLeft):
                        break;

                    default:
                        if (textAlignment != DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0005;
        TR_0000:
            throw new InvalidOperationException();
        TR_0001:
            return VerticalAlignment.Center;
        TR_0003:
            return VerticalAlignment.Top;
        TR_0005:
            return VerticalAlignment.Bottom;
        }

        public static System.Windows.TextAlignment GetWpfTextAlignment(DevExpress.XtraPrinting.TextAlignment textAlignment)
        {
            if (textAlignment > DevExpress.XtraPrinting.TextAlignment.BottomLeft)
            {
                if (textAlignment > DevExpress.XtraPrinting.TextAlignment.BottomRight)
                {
                    if ((textAlignment == DevExpress.XtraPrinting.TextAlignment.TopJustify) || ((textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleJustify) || (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomJustify)))
                    {
                        return System.Windows.TextAlignment.Justify;
                    }
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (textAlignment > DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
            {
                if (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (textAlignment == DevExpress.XtraPrinting.TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (textAlignment)
                {
                    case DevExpress.XtraPrinting.TextAlignment.TopLeft:
                        goto TR_0001;

                    case DevExpress.XtraPrinting.TextAlignment.TopCenter:
                        goto TR_0003;

                    case (DevExpress.XtraPrinting.TextAlignment.TopCenter | DevExpress.XtraPrinting.TextAlignment.TopLeft):
                        break;

                    case DevExpress.XtraPrinting.TextAlignment.TopRight:
                        goto TR_0004;

                    default:
                        if (textAlignment != DevExpress.XtraPrinting.TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new InvalidOperationException();
        TR_0001:
            return System.Windows.TextAlignment.Left;
        TR_0003:
            return System.Windows.TextAlignment.Center;
        TR_0004:
            return System.Windows.TextAlignment.Right;
        }

        public static System.Drawing.Color ToGdiColor(System.Windows.Media.Color color) => 
            System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        internal static System.Drawing.FontStyle ToGdiFontStyle(System.Windows.FontStyle style, FontWeight weight, BrickTextDecorations textDecorations)
        {
            System.Drawing.FontStyle regular = System.Drawing.FontStyle.Regular;
            if (style == FontStyles.Italic)
            {
                regular |= System.Drawing.FontStyle.Italic;
            }
            if (style == FontStyles.Oblique)
            {
                regular |= System.Drawing.FontStyle.Italic;
            }
            if (FontWeightHelper.Compare(weight, FontWeights.SemiBold) >= 0)
            {
                regular |= System.Drawing.FontStyle.Bold;
            }
            if ((textDecorations & BrickTextDecorations.Underline) != BrickTextDecorations.None)
            {
                regular |= System.Drawing.FontStyle.Underline;
            }
            if ((textDecorations & BrickTextDecorations.Strikethrough) != BrickTextDecorations.None)
            {
                regular |= System.Drawing.FontStyle.Strikeout;
            }
            return regular;
        }

        public static System.Windows.Point ToPoint(PointF value) => 
            new System.Windows.Point((double) value.X, (double) value.Y);

        public static Rect ToRect(RectangleF value) => 
            new Rect((double) value.X, (double) value.Y, (double) value.Width, (double) value.Height);

        public static System.Windows.Size ToSize(SizeF value) => 
            new System.Windows.Size((double) value.Width, (double) value.Height);

        public static StringTrimming ToStringTrimming(TextTrimming textTrimming)
        {
            switch (textTrimming)
            {
                case TextTrimming.None:
                    return StringTrimming.None;

                case TextTrimming.CharacterEllipsis:
                    return StringTrimming.EllipsisCharacter;

                case TextTrimming.WordEllipsis:
                    return StringTrimming.EllipsisWord;
            }
            throw new ArgumentException();
        }

        public static DevExpress.XtraPrinting.TextAlignment ToXtraPrintingTextAlignment(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            if (horizontalAlignment == HorizontalAlignment.Left)
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopLeft;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomLeft;
                }
            }
            else if (horizontalAlignment == HorizontalAlignment.Center)
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopCenter;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomCenter;
                }
            }
            else if (horizontalAlignment == HorizontalAlignment.Right)
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopRight;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomRight;
                }
            }
            else
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopJustify;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomJustify;
                }
            }
            throw new NotSupportedException();
        }

        public static DevExpress.XtraPrinting.TextAlignment ToXtraPrintingTextAlignment(System.Windows.TextAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            if (horizontalAlignment == System.Windows.TextAlignment.Left)
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopLeft;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomLeft;
                }
            }
            else if (horizontalAlignment == System.Windows.TextAlignment.Center)
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopCenter;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomCenter;
                }
            }
            else if (horizontalAlignment == System.Windows.TextAlignment.Right)
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopRight;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomRight;
                }
            }
            else
            {
                if (verticalAlignment == VerticalAlignment.Top)
                {
                    return DevExpress.XtraPrinting.TextAlignment.TopJustify;
                }
                if (verticalAlignment == VerticalAlignment.Center)
                {
                    return DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
                }
                if (verticalAlignment == VerticalAlignment.Bottom)
                {
                    return DevExpress.XtraPrinting.TextAlignment.BottomJustify;
                }
            }
            throw new NotSupportedException();
        }
    }
}

