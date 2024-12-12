namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal static class WatermarkXamlExporter
    {
        private const int showOnTopZIndex = 1;

        private static SizeF CalculateWatermarkImageSizeInZoomMode(Page page, XamlExportContext exportContext)
        {
            SizeF watermarkAreaSize = GetWatermarkAreaSize(page, exportContext);
            SizeF imageSize = page.ActualWatermark.ImageSource.GetImageSize(false);
            float num = watermarkAreaSize.Width / imageSize.Width;
            float num2 = watermarkAreaSize.Height / imageSize.Height;
            return ((num <= num2) ? new SizeF(watermarkAreaSize.Width, imageSize.Height * num) : new SizeF(imageSize.Width * num2, watermarkAreaSize.Height));
        }

        private static bool ContainsImage(PageWatermark wm) => 
            !ImageSource.IsNullOrEmpty(wm.ImageSource);

        private static bool ContainsText(PageWatermark wm) => 
            !string.IsNullOrEmpty(wm.Text);

        private static string GetPageRange(Document document) => 
            ((document == null) || (document.PrintingSystem == null)) ? null : GetPageRange(document.PrintingSystem.Watermark);

        private static string GetPageRange(PageWatermark pageWatermark)
        {
            Watermark watermark = pageWatermark as Watermark;
            return watermark?.PageRange;
        }

        private static string GetPageRange(Page page) => 
            GetPageRange(page.ActualWatermark) ?? GetPageRange(page.Document);

        private static int GetRotationAngle(DirectionMode directionMode)
        {
            switch (directionMode)
            {
                case DirectionMode.Horizontal:
                    return 0;

                case DirectionMode.ForwardDiagonal:
                    return 310;

                case DirectionMode.BackwardDiagonal:
                    return 50;

                case DirectionMode.Vertical:
                    return 270;
            }
            throw new ArgumentException("directionMode");
        }

        private static unsafe SizeF GetWatermarkAreaSize(Page page, XamlExportContext exportContext)
        {
            SizeF ef = page.PageSize.DocToDip();
            SizeF* efPtr1 = &ef;
            efPtr1.Width -= page.PageData.MinMarginsF.Left.DocToDip() + page.PageData.MinMarginsF.Right.DocToDip();
            SizeF* efPtr2 = &ef;
            efPtr2.Height -= page.PageData.MinMarginsF.Top.DocToDip() + page.PageData.MinMarginsF.Bottom.DocToDip();
            return ef;
        }

        private static SizeF GetWatermarkImageSize(Page page, XamlExportContext exportContext)
        {
            PageWatermark actualWatermark = page.ActualWatermark;
            switch (actualWatermark.ImageViewMode)
            {
                case ImageViewMode.Clip:
                    return actualWatermark.ImageSource.GetImageSize(false);

                case ImageViewMode.Stretch:
                    return GetWatermarkAreaSize(page, exportContext);

                case ImageViewMode.Zoom:
                    return CalculateWatermarkImageSizeInZoomMode(page, exportContext);
            }
            throw new ArgumentException("page");
        }

        private static string MapContentAlignmentToHorizontalAlignment(ContentAlignment alignment)
        {
            if (alignment > ContentAlignment.MiddleCenter)
            {
                if (alignment > ContentAlignment.BottomLeft)
                {
                    if (alignment == ContentAlignment.BottomCenter)
                    {
                        goto TR_0001;
                    }
                    else if (alignment == ContentAlignment.BottomRight)
                    {
                        goto TR_0005;
                    }
                }
                else if (alignment == ContentAlignment.MiddleRight)
                {
                    goto TR_0005;
                }
                else if (alignment == ContentAlignment.BottomLeft)
                {
                    goto TR_0003;
                }
                goto TR_0000;
            }
            else
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        goto TR_0003;

                    case ContentAlignment.TopCenter:
                        goto TR_0001;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        goto TR_0000;

                    case ContentAlignment.TopRight:
                        break;

                    default:
                        if (alignment != ContentAlignment.MiddleLeft)
                        {
                            if (alignment != ContentAlignment.MiddleCenter)
                            {
                                goto TR_0000;
                            }
                            goto TR_0001;
                        }
                        goto TR_0003;
                }
            }
            goto TR_0005;
        TR_0000:
            throw new ArgumentException("alignment");
        TR_0001:
            return XamlNames.AlignmentCenter;
        TR_0003:
            return XamlNames.AlignmentLeft;
        TR_0005:
            return XamlNames.AlignmentRight;
        }

        private static string MapContentAlignmentToVerticalAlignment(ContentAlignment alignment)
        {
            if (alignment > ContentAlignment.MiddleCenter)
            {
                if (alignment > ContentAlignment.BottomLeft)
                {
                    if ((alignment == ContentAlignment.BottomCenter) || (alignment == ContentAlignment.BottomRight))
                    {
                        goto TR_0005;
                    }
                }
                else if (alignment == ContentAlignment.MiddleRight)
                {
                    goto TR_0001;
                }
                else if (alignment == ContentAlignment.BottomLeft)
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        return XamlNames.AlignmentTop;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        break;

                    default:
                        if ((alignment != ContentAlignment.MiddleLeft) && (alignment != ContentAlignment.MiddleCenter))
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0005;
        TR_0000:
            throw new ArgumentException("alignment");
        TR_0001:
            return XamlNames.AlignmentCenter;
        TR_0005:
            return XamlNames.AlignmentBottom;
        }

        private static string MapImageViewModeToStretch(ImageViewMode imageWatermarkMode)
        {
            switch (imageWatermarkMode)
            {
                case ImageViewMode.Clip:
                    return XamlNames.StretchFill;

                case ImageViewMode.Stretch:
                    return XamlNames.StretchFill;

                case ImageViewMode.Zoom:
                    return XamlNames.StretchUniform;
            }
            throw new ArgumentException("imageWatermarkMode");
        }

        private static float MapTransparencyToOpacity(int transparency) => 
            1f - (((float) transparency) / 255f);

        private static void WriteFontAttributes(XamlWriter writer, PageWatermark wm)
        {
            float num = wm.Font.Size.ToDocFrom(wm.Font.Unit);
            writer.WriteAttribute(XamlAttribute.FontSize, num.DocToDip());
            if (wm.Font.Bold)
            {
                writer.WriteAttribute(XamlAttribute.FontWeight, XamlNames.FontBold);
            }
            if (wm.Font.Italic)
            {
                writer.WriteAttribute(XamlAttribute.FontStyle, XamlNames.FontItalic);
            }
            if (wm.Font.Strikeout && wm.Font.Underline)
            {
                writer.WriteAttribute(XamlAttribute.TextDecorations, XamlNames.FontStrikethrough + "," + XamlNames.FontUnderline);
            }
            else
            {
                if (wm.Font.Strikeout)
                {
                    writer.WriteAttribute(XamlAttribute.TextDecorations, XamlNames.FontStrikethrough);
                }
                if (wm.Font.Underline)
                {
                    writer.WriteAttribute(XamlAttribute.TextDecorations, XamlNames.FontUnderline);
                }
            }
        }

        private static void WriteImageWatermark(XamlWriter writer, Page page, XamlExportContext exportContext, PageWatermark wm)
        {
            if (exportContext.ResourceMap.ImageResourcesDictionary.ContainsKey(page) && (!wm.ImageTiling || !exportContext.IsPartialTrustMode))
            {
                writer.WriteStartElement(XamlTag.Border);
                if (!wm.ShowBehind)
                {
                    writer.WriteAttribute(XamlAttribute.CanvasZIndex, (float) 1f);
                }
                SizeF watermarkAreaSize = GetWatermarkAreaSize(page, exportContext);
                writer.WriteAttribute(XamlAttribute.Width, watermarkAreaSize.Width);
                writer.WriteAttribute(XamlAttribute.Height, watermarkAreaSize.Height);
                writer.WriteStartElement(XamlTag.Image);
                SizeF ef2 = ((wm.ImageViewMode != ImageViewMode.Clip) || wm.ImageTiling) ? watermarkAreaSize : wm.ImageSource.GetImageSize(false);
                writer.WriteAttribute(XamlAttribute.Width, ef2.Width);
                writer.WriteAttribute(XamlAttribute.Height, ef2.Height);
                if (exportContext.Compatibility == XamlCompatibility.WPF)
                {
                    writer.WriteAttribute(XamlAttribute.RenderOptionsBitmapScalingMode, XamlNames.Fant);
                }
                string str = exportContext.ResourceMap.ImageResourcesDictionary[page];
                writer.WriteAttribute(XamlAttribute.Source, str);
                float[] values = new float[] { page.PageData.MinMarginsF.Left.DocToDip(), page.PageData.MinMarginsF.Top.DocToDip(), page.PageData.MinMarginsF.Right.DocToDip(), page.PageData.MinMarginsF.Bottom.DocToDip() };
                writer.WriteAttribute(XamlAttribute.Margin, values);
                if ((wm.ImageTransparency != 0) && !wm.ImageTiling)
                {
                    writer.WriteAttribute(XamlAttribute.Opacity, MapTransparencyToOpacity(wm.ImageTransparency));
                }
                if (wm.ImageViewMode != ImageViewMode.Zoom)
                {
                    writer.WriteAttribute(XamlAttribute.VerticalAlignment, MapContentAlignmentToVerticalAlignment(wm.ImageAlign));
                    writer.WriteAttribute(XamlAttribute.HorizontalAlignment, MapContentAlignmentToHorizontalAlignment(wm.ImageAlign));
                }
                if (!wm.ImageTiling)
                {
                    writer.WriteAttribute(XamlAttribute.Stretch, MapImageViewModeToStretch(wm.ImageViewMode));
                }
                else
                {
                    writer.WriteAttribute(XamlAttribute.Stretch, MapImageViewModeToStretch(ImageViewMode.Stretch));
                    writer.WriteStartElement(XamlTag.ImageEffect);
                    writer.WriteStartElement(XamlNsPrefix.dxpn, XamlTag.TileEffect, "http://schemas.devexpress.com/winfx/2008/xaml/printingcore/xtraprinting/native/presentation");
                    SizeF watermarkImageSize = GetWatermarkImageSize(page, exportContext);
                    float[] singleArray2 = new float[] { watermarkAreaSize.Width / watermarkImageSize.Width, watermarkAreaSize.Height / watermarkImageSize.Height };
                    writer.WriteAttribute(XamlAttribute.TileCount, singleArray2);
                    writer.WriteAttribute(XamlAttribute.Opacity, MapTransparencyToOpacity(wm.ImageTransparency));
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private static void WriteRotateTransform(XamlWriter writer, PageWatermark wm)
        {
            if (wm.TextDirection != DirectionMode.Horizontal)
            {
                writer.WriteStartElement(XamlTag.TextBlockRenderTransform);
                writer.WriteStartElement(XamlTag.TransformGroup);
                writer.WriteStartElement(XamlTag.RotateTransform);
                writer.WriteAttribute(XamlAttribute.Angle, (float) GetRotationAngle(wm.TextDirection));
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private static void WriteTextWatermark(XamlWriter writer, Page page, PageWatermark wm, XamlExportContext exportContext)
        {
            writer.WriteStartElement(XamlTag.Border);
            SizeF watermarkAreaSize = GetWatermarkAreaSize(page, exportContext);
            writer.WriteAttribute(XamlAttribute.MinWidth, watermarkAreaSize.Width);
            writer.WriteAttribute(XamlAttribute.MinHeight, watermarkAreaSize.Height);
            writer.WriteAttribute(XamlAttribute.MaxWidth, watermarkAreaSize.Width);
            writer.WriteAttribute(XamlAttribute.MaxHeight, watermarkAreaSize.Height);
            float[] values = new float[] { page.PageData.MinMarginsF.Left.DocToDip(), page.PageData.MinMarginsF.Top.DocToDip(), page.PageData.MinMarginsF.Right.DocToDip(), page.PageData.MinMarginsF.Bottom.DocToDip() };
            writer.WriteAttribute(XamlAttribute.Margin, values);
            if (!wm.ShowBehind)
            {
                writer.WriteAttribute(XamlAttribute.CanvasZIndex, (float) 1f);
            }
            writer.WriteStartElement(XamlTag.TextBlock);
            if (wm.TextTransparency != 0)
            {
                writer.WriteAttribute(XamlAttribute.Opacity, MapTransparencyToOpacity(wm.TextTransparency));
            }
            writer.WriteAttribute(XamlAttribute.Text, wm.Text);
            WriteFontAttributes(writer, wm);
            writer.WriteAttribute(XamlAttribute.FontFamily, FontResolver.ResolveFamilyName(wm.Font));
            writer.WriteAttribute(XamlAttribute.Foreground, wm.ForeColor);
            writer.WriteAttribute(XamlAttribute.VerticalAlignment, MapContentAlignmentToVerticalAlignment(ContentAlignment.MiddleCenter));
            writer.WriteAttribute(XamlAttribute.HorizontalAlignment, MapContentAlignmentToHorizontalAlignment(ContentAlignment.MiddleCenter));
            float[] singleArray2 = new float[] { 0.5f, 0.5f };
            writer.WriteAttribute(XamlAttribute.RenderTransformOrigin, singleArray2);
            WriteRotateTransform(writer, wm);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public static void WriteToXaml(XamlWriter writer, Page page, XamlExportContext exportContext)
        {
            WriteWatermarkToXaml(writer, page, exportContext, page.ActualWatermark, GetPageRange(page));
        }

        private static void WriteWatermarkToXaml(XamlWriter writer, Page page, XamlExportContext exportContext, PageWatermark wm, string pageRange)
        {
            if ((wm != null) && (string.IsNullOrEmpty(pageRange) || (Array.IndexOf<int>(PageRangeParser.GetIndices(pageRange, page.Document.PageCount), page.Index) >= 0)))
            {
                if (ContainsImage(wm))
                {
                    WriteImageWatermark(writer, page, exportContext, wm);
                }
                if (ContainsText(wm))
                {
                    WriteTextWatermark(writer, page, wm, exportContext);
                }
            }
        }
    }
}

