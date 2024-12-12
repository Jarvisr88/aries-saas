namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    internal class PageXamlExporter : BrickXamlExporterBase
    {
        private static string GetBase64StringForImage(System.Drawing.Image image) => 
            (image != null) ? Convert.ToBase64String(PSConvert.ImageToArray(image, ImageFormat.Png)) : string.Empty;

        private static float GetBorderWidth(BorderSide borderSide, XamlBorderStyle borderStyle) => 
            ((borderStyle.Sides & borderSide) > BorderSide.None) ? borderStyle.BorderWidth : 0f;

        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.ChildElements;

        internal static Stream GetCheckBoxTemplatesResourceStream() => 
            ResourceStreamHelper.GetStream("Native.CheckBoxTemplates.xaml", typeof(ResFinder));

        private static string GetTextHorizontalAlignment(TextAlignment alignment, XamlCompatibility compatibility)
        {
            if (alignment > TextAlignment.BottomLeft)
            {
                if (alignment > TextAlignment.BottomRight)
                {
                    if ((alignment == TextAlignment.TopJustify) || ((alignment == TextAlignment.MiddleJustify) || (alignment == TextAlignment.BottomJustify)))
                    {
                        return ((compatibility == XamlCompatibility.WPF) ? XamlNames.AlignmentJustify : XamlNames.AlignmentLeft);
                    }
                }
                else if (alignment == TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (alignment == TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (alignment > TextAlignment.MiddleLeft)
            {
                if (alignment == TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (alignment == TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (alignment == TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (alignment)
                {
                    case TextAlignment.TopLeft:
                        goto TR_0001;

                    case TextAlignment.TopCenter:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        break;

                    case TextAlignment.TopRight:
                        goto TR_0004;

                    default:
                        if (alignment != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new ArgumentException("alignment");
        TR_0001:
            return XamlNames.AlignmentLeft;
        TR_0003:
            return XamlNames.AlignmentCenter;
        TR_0004:
            return XamlNames.AlignmentRight;
        }

        private static string GetTextTrimming(StringTrimming stringTrimming) => 
            (stringTrimming == StringTrimming.EllipsisCharacter) ? XamlNames.TextTrimmingCharacterEllipsis : ((stringTrimming == StringTrimming.EllipsisWord) ? XamlNames.TextTrimmingWordEllipsis : XamlNames.TextTrimmingNone);

        private static string GetVerticalAlignment(TextAlignment alignment)
        {
            if (alignment > TextAlignment.BottomLeft)
            {
                if (alignment > TextAlignment.BottomRight)
                {
                    if (alignment == TextAlignment.TopJustify)
                    {
                        goto TR_0003;
                    }
                    else if (alignment == TextAlignment.MiddleJustify)
                    {
                        goto TR_0001;
                    }
                    else if (alignment == TextAlignment.BottomJustify)
                    {
                        goto TR_0005;
                    }
                }
                else if ((alignment == TextAlignment.BottomCenter) || (alignment == TextAlignment.BottomRight))
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else if (alignment > TextAlignment.MiddleLeft)
            {
                if ((alignment == TextAlignment.MiddleCenter) || (alignment == TextAlignment.MiddleRight))
                {
                    goto TR_0001;
                }
                else if (alignment == TextAlignment.BottomLeft)
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else
            {
                switch (alignment)
                {
                    case TextAlignment.TopLeft:
                    case TextAlignment.TopCenter:
                    case TextAlignment.TopRight:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        break;

                    default:
                        if (alignment != TextAlignment.MiddleLeft)
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
        TR_0003:
            return XamlNames.AlignmentTop;
        TR_0005:
            return XamlNames.AlignmentBottom;
        }

        private static string ResolveFontFamilyName(Font font) => 
            FontResolver.ResolveEnglishFamilyName(font);

        private static void WriteBorderDashStyles(XamlWriter writer, IEnumerable<XamlLineStyle> lineStyles)
        {
            foreach (XamlLineStyle style in lineStyles)
            {
                WriteLineStart(writer, style);
                writer.WriteSetter(XamlAttribute.StrokeStartLineCap, "Square");
                writer.WriteSetter(XamlAttribute.StrokeEndLineCap, "Square");
                writer.WriteEndElement();
            }
        }

        private static void WriteBorderStyles(XamlWriter writer, IEnumerable<XamlBorderStyle> borderStyles, XamlExportContext exportContext)
        {
            foreach (XamlBorderStyle style in borderStyles)
            {
                writer.WriteStartElement(XamlTag.Style);
                writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, style.Name);
                writer.WriteAttribute(XamlAttribute.TargetType, XamlNames.TargetTypeBorder);
                float[] values = new float[] { GetBorderWidth(BorderSide.Left, style), GetBorderWidth(BorderSide.Top, style), GetBorderWidth(BorderSide.Right, style), GetBorderWidth(BorderSide.Bottom, style) };
                writer.WriteSetter(XamlAttribute.BorderThickness, values);
                writer.WriteSetter(XamlAttribute.BorderBrush, style.BorderBrush);
                writer.WriteSetter(XamlAttribute.Background, style.BackColor);
                PaddingInfo padding = style.Padding;
                PaddingInfo info3 = style.Padding;
                PaddingInfo info = padding.Scale(96f / info3.Dpi);
                float[] singleArray2 = new float[] { info.Left, info.Top, info.Right, info.Bottom };
                writer.WriteSetter(XamlAttribute.Padding, singleArray2);
                writer.WriteEndElement();
            }
        }

        public override void WriteBrickToXaml(XamlWriter writer, BrickBase brick, XamlExportContext exportContext, RectangleF clipRect, Action<XamlWriter> declareNamespaces, Action<XamlWriter, object> writeCustomProperties)
        {
            Page page = brick as Page;
            if (page == null)
            {
                throw new ArgumentException("brick");
            }
            writer.WriteStartElement(XamlTag.Canvas, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            writer.WriteNamespace(XamlNsPrefix.x, "http://schemas.microsoft.com/winfx/2006/xaml");
            writer.WriteNamespace(XamlNsPrefix.system, "clr-namespace:System;assembly=mscorlib");
            writer.WriteNamespace(XamlNsPrefix.dxpn, "http://schemas.devexpress.com/winfx/2008/xaml/printingcore/xtraprinting/native");
            if (!XamlExporter.InheritParentFlowDirection)
            {
                writer.WriteAttribute(XamlAttribute.FlowDirection, XamlNames.FlowDirectionLeftToRight);
            }
            declareNamespaces(writer);
            if (page.Document != null)
            {
                writer.WriteAttribute(XamlAttribute.Background, page.Document.PrintingSystem.Graph.PageBackColor);
            }
            float num = page.PageSize.Width.DocToDip();
            float num2 = page.PageSize.Height.DocToDip();
            writer.WriteAttribute(XamlAttribute.Width, num);
            writer.WriteAttribute(XamlAttribute.Height, num2);
            if (exportContext.Compatibility == XamlCompatibility.WPF)
            {
                writer.WriteAttribute(XamlAttribute.SnapsToDevicePixels, true.ToString());
            }
            writer.WriteAttribute(XamlAttribute.UseLayoutRounding, false.ToString());
            writer.WriteStartElement(XamlTag.CanvasClip);
            writer.WriteStartElement(XamlTag.RectangleGeometry);
            writer.WriteAttribute(XamlAttribute.Rect, new RectangleF(0f, 0f, num, num2));
            writer.WriteEndElement();
            writer.WriteEndElement();
            WriteResources(writer, exportContext);
            WatermarkXamlExporter.WriteToXaml(writer, page, exportContext);
        }

        private static void WriteCheckBoxTemplates(XamlWriter writer)
        {
            writer.WriteStartElement(XamlTag.ResourceDictionaryMergedDictionaries);
            using (Stream stream = GetCheckBoxTemplatesResourceStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.WriteRaw(reader.ReadToEnd());
                }
            }
            writer.WriteEndElement();
        }

        public override void WriteEndTags(XamlWriter writer)
        {
            writer.WriteEndElement();
        }

        private static void WriteFontSetters(XamlWriter writer, XamlTextBlockStyle textBlockStyle)
        {
            float num = textBlockStyle.Font.Size.ToDocFrom(textBlockStyle.Font.Unit);
            writer.WriteSetter(XamlAttribute.FontSize, num.DocToDip());
            if (textBlockStyle.Font.Bold)
            {
                writer.WriteSetter(XamlAttribute.FontWeight, XamlNames.FontBold);
            }
            else
            {
                writer.WriteSetter(XamlAttribute.FontWeight, XamlNames.FontNormal);
            }
            if (textBlockStyle.Font.Italic)
            {
                writer.WriteSetter(XamlAttribute.FontStyle, XamlNames.FontItalic);
            }
            else
            {
                writer.WriteSetter(XamlAttribute.FontStyle, XamlNames.FontNormal);
            }
            if (textBlockStyle.Font.Strikeout && textBlockStyle.Font.Underline)
            {
                writer.WriteSetter(XamlAttribute.TextDecorations, $"{XamlNames.FontStrikethrough},{XamlNames.FontUnderline}");
            }
            else
            {
                if (textBlockStyle.Font.Strikeout)
                {
                    writer.WriteSetter(XamlAttribute.TextDecorations, XamlNames.FontStrikethrough);
                }
                if (textBlockStyle.Font.Underline)
                {
                    writer.WriteSetter(XamlAttribute.TextDecorations, XamlNames.FontUnderline);
                }
            }
        }

        private static void WriteImageResources(XamlWriter writer, IEnumerable<ImageResource> imageResources, bool shouldWriteConverterResource, XamlCompatibility compatibility, bool embedImagesToXaml)
        {
            foreach (ImageResource resource in imageResources)
            {
                writer.WriteStartElement(XamlNsPrefix.system, XamlTag.String, "clr-namespace:System;assembly=mscorlib");
                writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, resource.Name);
                writer.WriteValue(GetBase64StringForImage(resource.Image));
                writer.WriteEndElement();
            }
            if (shouldWriteConverterResource)
            {
                if (embedImagesToXaml)
                {
                    writer.WriteStartElement(XamlNsPrefix.dxpn, XamlTag.Base64StringImageConverter, "http://schemas.devexpress.com/winfx/2008/xaml/printingcore/xtraprinting/native/presentation");
                    writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, "embeddedImageConverter");
                    writer.WriteEndElement();
                }
                if (compatibility == XamlCompatibility.WPF)
                {
                    writer.WriteStartElement(XamlNsPrefix.dxpn, XamlTag.RepositoryImageConverter, "http://schemas.devexpress.com/winfx/2008/xaml/printingcore/xtraprinting/native/presentation");
                    writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, "repositoryImageConverter");
                    writer.WriteEndElement();
                }
            }
        }

        private static void WriteLineStart(XamlWriter writer, XamlLineStyle lineStyle)
        {
            writer.WriteStartElement(XamlTag.Style);
            writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, lineStyle.Name);
            writer.WriteAttribute(XamlAttribute.TargetType, XamlNames.TargetTypeLine);
            writer.WriteSetter(XamlAttribute.Stroke, lineStyle.Stroke);
            writer.WriteSetter(XamlAttribute.StrokeThickness, lineStyle.StrokeThickness);
        }

        private static void WriteLineStyle(XamlWriter writer, XamlLineStyle lineStyle)
        {
            writer.WriteStartElement(XamlTag.Style);
            writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, lineStyle.Name);
            writer.WriteAttribute(XamlAttribute.TargetType, XamlNames.TargetTypeLine);
            writer.WriteSetter(XamlAttribute.Stroke, lineStyle.Stroke);
            writer.WriteSetter(XamlAttribute.StrokeThickness, lineStyle.StrokeThickness);
        }

        private static void WriteLineStyles(XamlWriter writer, IEnumerable<XamlLineStyle> lineStyles)
        {
            foreach (XamlLineStyle style in lineStyles)
            {
                WriteLineStyle(writer, style);
                writer.WriteEndElement();
            }
        }

        private static void WriteResources(XamlWriter writer, XamlExportContext exportContext)
        {
            writer.WriteStartElement(XamlTag.CanvasResources);
            writer.WriteStartElement(XamlTag.ResourceDictionary);
            WriteBorderStyles(writer, exportContext.ResourceCache.BorderStyles, exportContext);
            WriteBorderDashStyles(writer, exportContext.ResourceCache.BorderDashStyles);
            WriteTextBlockStyles(writer, exportContext.ResourceCache.TextBlockStyles, exportContext.Compatibility, exportContext.TextMeasurementSystem);
            WriteLineStyles(writer, exportContext.ResourceCache.LineStyles);
            WriteImageResources(writer, exportContext.ResourceCache.ImageResources, exportContext.ResourceMap.ImageResourcesDictionary.Count != 0, exportContext.Compatibility, exportContext.EmbedImagesToXaml);
            if (exportContext.ResourceMap.ShouldAddCheckBoxTemplates)
            {
                WriteCheckBoxTemplates(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private static void WriteTextBlockStyles(XamlWriter writer, IEnumerable<XamlTextBlockStyle> textBlockStyles, XamlCompatibility compatibility, TextMeasurementSystem textMeasurementSystem)
        {
            foreach (XamlTextBlockStyle style in textBlockStyles)
            {
                writer.WriteStartElement(XamlTag.Style);
                writer.WriteAttribute(XamlNsPrefix.x, XamlAttribute.Key, style.Name);
                writer.WriteAttribute(XamlAttribute.TargetType, XamlNames.TargetTypeTextBlock);
                writer.WriteSetter(XamlAttribute.FontFamily, ResolveFontFamilyName(style.Font));
                WriteFontSetters(writer, style);
                writer.WriteSetter(XamlAttribute.Foreground, style.Foreground);
                writer.WriteSetter(XamlAttribute.TextAlignment, GetTextHorizontalAlignment(style.TextAlignment, compatibility));
                writer.WriteSetter(XamlAttribute.VerticalAlignment, GetVerticalAlignment(style.TextAlignment));
                if (textMeasurementSystem == TextMeasurementSystem.NativeXpf)
                {
                    writer.WriteSetter(XamlAttribute.TextWrapping, style.WrapText ? XamlNames.TextWrappingWrap : XamlNames.TextWrappingNoWrap);
                }
                if (style.StringTrimming != StringTrimming.None)
                {
                    string textTrimming = GetTextTrimming(style.StringTrimming);
                    if (textTrimming != XamlNames.TextTrimmingNone)
                    {
                        writer.WriteSetter(XamlAttribute.TextTrimming, textTrimming);
                    }
                }
                writer.WriteEndElement();
            }
        }
    }
}

