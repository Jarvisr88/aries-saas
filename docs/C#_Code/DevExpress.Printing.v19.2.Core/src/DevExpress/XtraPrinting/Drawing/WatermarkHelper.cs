namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.TextRotation;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class WatermarkHelper
    {
        public static unsafe void DrawTileImage(IGraphics gr, ImageSource imageSource, float scale, RectangleF dstRect)
        {
            if (!ImageSource.IsNullOrEmpty(imageSource))
            {
                RectangleF ef2 = dstRect;
                ef2.Intersect(gr.ClipBounds);
                gr.ClipBounds = ef2;
                gr.SaveTransformState();
                float sx = (scale * 300f) / (imageSource.HasSvgImage ? 96f : imageSource.Image.HorizontalResolution);
                float sy = (scale * 300f) / (imageSource.HasSvgImage ? 96f : imageSource.Image.VerticalResolution);
                SizeF imageSize = imageSource.GetImageSize(false);
                if (gr is IPdfGraphics)
                {
                    imageSize.Width = Convert.ToInt32((float) (imageSize.Width * sx));
                    imageSize.Height = Convert.ToInt32((float) (imageSize.Height * sy));
                }
                else
                {
                    gr.ScaleTransform(sx, sy);
                    RectangleF* efPtr1 = &dstRect;
                    efPtr1.X /= sx;
                    RectangleF* efPtr2 = &dstRect;
                    efPtr2.Y /= sy;
                    RectangleF* efPtr3 = &dstRect;
                    efPtr3.Width /= sx;
                    RectangleF* efPtr4 = &dstRect;
                    efPtr4.Height /= sy;
                }
                try
                {
                    float left = dstRect.Left;
                    while (left < dstRect.Right)
                    {
                        float top = dstRect.Top;
                        while (true)
                        {
                            if (top >= dstRect.Bottom)
                            {
                                left += imageSize.Width - 1f;
                                break;
                            }
                            ImagePainter.Draw(imageSource, gr, new RectangleF(left, top, imageSize.Width, imageSize.Height));
                            top += imageSize.Height - 1f;
                        }
                    }
                }
                finally
                {
                    RectangleF ef;
                    gr.ResetTransform();
                    gr.ApplyTransformState(MatrixOrder.Append, true);
                    gr.ClipBounds = ef;
                }
            }
        }

        public static void DrawWatermark(DirectionMode textDirection, IGraphics gr, string text, Font font, Brush brush, Rectangle rect, StringFormat sf)
        {
            switch (textDirection)
            {
                case DirectionMode.Horizontal:
                    RotatedTextPainter.DrawRotatedString(gr, text, font, brush, Rectangle.Round(rect), sf, 0f, -1f, TextAlignment.MiddleCenter);
                    return;

                case DirectionMode.ForwardDiagonal:
                    RotatedTextPainter.DrawRotatedString(gr, text, font, brush, Rectangle.Round(rect), sf, 50f, -1f, TextAlignment.MiddleCenter);
                    return;

                case DirectionMode.BackwardDiagonal:
                    RotatedTextPainter.DrawRotatedString(gr, text, font, brush, Rectangle.Round(rect), sf, -50f, -1f, TextAlignment.MiddleCenter);
                    return;

                case (DirectionMode.BackwardDiagonal | DirectionMode.ForwardDiagonal):
                    break;

                case DirectionMode.Vertical:
                    RotatedTextPainter.DrawRotatedString(gr, text, font, brush, Rectangle.Round(rect), sf, 90f, -1f, TextAlignment.MiddleCenter);
                    break;

                default:
                    return;
            }
        }

        private static bool FitToWidth(SizeF baseSize, SizeF size) => 
            (baseSize.Width / size.Width) < (baseSize.Height / size.Height);

        public static ContentAlignment GetAdjustedAlignment(SizeF baseSize, SizeF size, ContentAlignment originalAlignment)
        {
            if (!FitToWidth(baseSize, size))
            {
                if (originalAlignment > ContentAlignment.MiddleCenter)
                {
                    if (originalAlignment > ContentAlignment.BottomLeft)
                    {
                        if (originalAlignment == ContentAlignment.BottomCenter)
                        {
                            goto TR_000B;
                        }
                        else if (originalAlignment == ContentAlignment.BottomRight)
                        {
                            goto TR_000F;
                        }
                    }
                    else if (originalAlignment == ContentAlignment.MiddleRight)
                    {
                        goto TR_000F;
                    }
                    else if (originalAlignment == ContentAlignment.BottomLeft)
                    {
                        goto TR_000D;
                    }
                }
                else
                {
                    switch (originalAlignment)
                    {
                        case ContentAlignment.TopLeft:
                            goto TR_000D;

                        case ContentAlignment.TopCenter:
                            goto TR_000B;

                        case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                            break;

                        case ContentAlignment.TopRight:
                            goto TR_000F;

                        default:
                            if (originalAlignment == ContentAlignment.MiddleLeft)
                            {
                                goto TR_000D;
                            }
                            else if (originalAlignment == ContentAlignment.MiddleCenter)
                            {
                                goto TR_000B;
                            }
                            break;
                    }
                }
                goto TR_0000;
            }
            else
            {
                if (originalAlignment > ContentAlignment.MiddleCenter)
                {
                    if (originalAlignment > ContentAlignment.BottomLeft)
                    {
                        if ((originalAlignment == ContentAlignment.BottomCenter) || (originalAlignment == ContentAlignment.BottomRight))
                        {
                            goto TR_0005;
                        }
                    }
                    else if (originalAlignment == ContentAlignment.MiddleRight)
                    {
                        goto TR_0001;
                    }
                    else if (originalAlignment == ContentAlignment.BottomLeft)
                    {
                        goto TR_0005;
                    }
                }
                else
                {
                    switch (originalAlignment)
                    {
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.TopCenter:
                        case ContentAlignment.TopRight:
                            return ContentAlignment.TopCenter;

                        case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                            break;

                        default:
                            if ((originalAlignment != ContentAlignment.MiddleLeft) && (originalAlignment != ContentAlignment.MiddleCenter))
                            {
                                break;
                            }
                            goto TR_0001;
                    }
                }
                goto TR_0000;
            }
            goto TR_000F;
        TR_0000:
            return ContentAlignment.MiddleCenter;
        TR_0001:
            return ContentAlignment.MiddleCenter;
        TR_0005:
            return ContentAlignment.BottomCenter;
        TR_000B:
            return ContentAlignment.MiddleCenter;
        TR_000D:
            return ContentAlignment.MiddleLeft;
        TR_000F:
            return ContentAlignment.MiddleRight;
        }

        public static float GetAdjustedScale(SizeF baseSize, SizeF size) => 
            Math.Min((float) (baseSize.Width / size.Width), (float) (baseSize.Height / size.Height));

        public static Size GetAdjustedSize(SizeF baseSize, SizeF size)
        {
            float adjustedScale = GetAdjustedScale(baseSize, size);
            return Size.Round(new SizeF(adjustedScale * size.Width, adjustedScale * size.Height));
        }
    }
}

