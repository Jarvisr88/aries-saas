namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class TextBrickXamlExporter : VisualBrickXamlExporter
    {
        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.Content;

        protected virtual string GetText(TextBrick brick, PrintingSystemBase ps, Page drawingPage, TextMeasurementSystem textMeasurementSystem)
        {
            string text = brick.Text;
            if (textMeasurementSystem != TextMeasurementSystem.GdiPlus)
            {
                return text;
            }
            if ((brick is LabelBrick) && (((LabelBrick) brick).Angle != 0f))
            {
                if (string.IsNullOrEmpty(text) || brick.StringFormat.FormatFlags.HasFlag(StringFormatFlags.MeasureTrailingSpaces))
                {
                    return text;
                }
                char[] trimChars = new char[] { ' ' };
                return text.TrimEnd(trimChars);
            }
            float width = Math.Max(0f, brick.Padding.DeflateWidth(brick.Width, 300f));
            using (GdiPlusMeasurer measurer = new GdiPlusMeasurer())
            {
                string[] strArray = TextFormatter.CreateInstance(GraphicsUnit.Document, measurer).FormatMultilineText(text, brick.Font, width, float.MaxValue, brick.StringFormat.Value);
                return string.Join(Environment.NewLine, strArray);
            }
        }

        private static string MapTextAlignmentToHorizontalAlignment(TextAlignment textAlignment)
        {
            if (textAlignment > TextAlignment.BottomLeft)
            {
                if (textAlignment > TextAlignment.BottomRight)
                {
                    if ((textAlignment == TextAlignment.TopJustify) || ((textAlignment == TextAlignment.MiddleJustify) || (textAlignment == TextAlignment.BottomJustify)))
                    {
                        goto TR_0001;
                    }
                }
                else if (textAlignment == TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
            }
            else if (textAlignment > TextAlignment.MiddleLeft)
            {
                if (textAlignment == TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (textAlignment != TextAlignment.BottomLeft)
                {
                    goto TR_0000;
                }
                goto TR_0001;
            }
            else
            {
                switch (textAlignment)
                {
                    case TextAlignment.TopLeft:
                        break;

                    case TextAlignment.TopCenter:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        goto TR_0000;

                    case TextAlignment.TopRight:
                        goto TR_0004;

                    default:
                        if (textAlignment == TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0000;
                }
                goto TR_0001;
            }
        TR_0000:
            throw new ArgumentException("textAlignment");
        TR_0001:
            return XamlNames.AlignmentLeft;
        TR_0003:
            return XamlNames.AlignmentCenter;
        TR_0004:
            return XamlNames.AlignmentRight;
        }

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            TextBrick brick2 = brick as TextBrick;
            if (brick2 == null)
            {
                throw new ArgumentException("brick");
            }
            writer.WriteStartElement(XamlTag.TextBlock);
            Page drawingPage = exportContext.Page;
            writer.WriteAttribute(XamlAttribute.Text, this.GetText(brick2, drawingPage?.Document.PrintingSystem, drawingPage, exportContext.TextMeasurementSystem));
            string str = exportContext.ResourceMap.TextBlockStylesDictionary[brick2];
            writer.WriteAttribute(XamlAttribute.Style, $"{{StaticResource {str}}}");
            LabelBrick brick3 = brick2 as LabelBrick;
            if (brick3 != null)
            {
                if (exportContext.TextMeasurementSystem == TextMeasurementSystem.GdiPlus)
                {
                    writer.WriteAttribute(XamlAttribute.TextWrapping, ((brick3.Angle == 0f) || !brick3.Style.StringFormat.WordWrap) ? XamlNames.TextWrappingNoWrap : XamlNames.TextWrappingWrap);
                }
                if ((exportContext.Compatibility == XamlCompatibility.WPF) && (brick3.Angle != 0f))
                {
                    writer.WriteAttribute(XamlAttribute.HorizontalAlignment, MapTextAlignmentToHorizontalAlignment(brick.Style.TextAlignment));
                    writer.WriteStartElement(XamlTag.TextBlockLayoutTransform);
                    writer.WriteStartElement(XamlTag.TransformGroup);
                    writer.WriteStartElement(XamlTag.RotateTransform);
                    writer.WriteAttribute(XamlAttribute.Angle, (float) (360f - brick3.Angle));
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
    }
}

