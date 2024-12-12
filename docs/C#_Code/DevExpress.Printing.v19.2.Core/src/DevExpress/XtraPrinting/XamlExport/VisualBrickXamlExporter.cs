namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Globalization;

    public abstract class VisualBrickXamlExporter : BrickXamlExporterBase
    {
        private int borderStartsCount;
        protected const string staticResourceFormat = "{{StaticResource {0}}}";

        protected VisualBrickXamlExporter()
        {
        }

        protected virtual unsafe RectangleF GetBorderBoundsInPixels(VisualBrick brick)
        {
            RectangleF ef = new RectangleF(brick.Location, brick.Size).DocToDip();
            float num = (brick.BorderStyle == BrickBorderStyle.Outset) ? 1f : ((brick.BorderStyle == BrickBorderStyle.Center) ? 0.5f : 0f);
            RectangleF* efPtr1 = &ef;
            efPtr1.X -= brick.Style.BorderWidth * num;
            RectangleF* efPtr2 = &ef;
            efPtr2.Y -= brick.Style.BorderWidth * num;
            RectangleF* efPtr3 = &ef;
            efPtr3.Width += (brick.Style.BorderWidth * 2f) * num;
            RectangleF* efPtr4 = &ef;
            efPtr4.Height += (brick.Style.BorderWidth * 2f) * num;
            return ((ef.Width > 0f) ? ((ef.Height > 0f) ? ef : RectangleF.Empty) : RectangleF.Empty);
        }

        protected static float GetBorderWidth(BorderSide borderSide, VisualBrick brick) => 
            ((brick.Style.Sides & borderSide) > BorderSide.None) ? brick.Style.BorderWidth : 0f;

        private static float GetThicknessValue(BorderSide borderSide, BorderSide brickSides, float value) => 
            ((brickSides & borderSide) > BorderSide.None) ? value : 0f;

        protected static float[] GetThicknessValues(BorderSide brickSides, float value) => 
            new float[] { GetThicknessValue(BorderSide.Left, brickSides, value), GetThicknessValue(BorderSide.Top, brickSides, value), GetThicknessValue(BorderSide.Right, brickSides, value), GetThicknessValue(BorderSide.Bottom, brickSides, value) };

        public override bool RequiresBorderStyle() => 
            true;

        private static void WriteAttachedValues(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick)
        {
            writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.VisualHelperIsVisualBrickBorder, true.ToString());
            WriteTagAttribute(writer, exportContext, visualBrick);
            if ((visualBrick.Value != null) && !visualBrick.Value.Equals(string.Empty))
            {
                object[] args = new object[] { visualBrick.Value };
                writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.PreviewClickHelperTag, string.Format(CultureInfo.InvariantCulture, "{0}", args));
            }
            if (!ReferenceEquals(visualBrick.NavigationPair, BrickPagePair.Empty))
            {
                string tagByIndices = DocumentMapTreeViewNodeHelper.GetTagByIndices(visualBrick.NavigationBrickIndices, visualBrick.NavigationPageIndex);
                writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.PreviewClickHelperNavigationPair, tagByIndices);
            }
            else if (!string.IsNullOrEmpty(visualBrick.GetActualUrl()))
            {
                writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.PreviewClickHelperUrl, visualBrick.GetActualUrl());
            }
        }

        private void WriteBorderStart(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick, RectangleF borderBounds)
        {
            writer.WriteStartElement(XamlTag.Border);
            this.borderStartsCount++;
            if (exportContext.ResourceMap.BorderStylesDictionary.ContainsKey(visualBrick))
            {
                string str = exportContext.ResourceMap.BorderStylesDictionary[visualBrick];
                writer.WriteAttribute(XamlAttribute.Style, $"{{StaticResource {str}}}");
            }
            writer.WriteAttribute(XamlAttribute.Width, borderBounds.Width);
            writer.WriteAttribute(XamlAttribute.Height, borderBounds.Height);
            writer.WriteAttribute(XamlAttribute.CanvasLeft, borderBounds.X);
            writer.WriteAttribute(XamlAttribute.CanvasTop, borderBounds.Y);
            if (visualBrick.BrickType == "Image")
            {
                writer.WriteAttribute(XamlAttribute.UseLayoutRounding, true.ToString());
            }
        }

        public override void WriteBrickToXaml(XamlWriter writer, BrickBase brick, XamlExportContext exportContext, RectangleF clipRect, Action<XamlWriter> declareNamespaces, Action<XamlWriter, object> writeCustomProperties)
        {
            VisualBrick visualBrick = brick as VisualBrick;
            if (visualBrick == null)
            {
                throw new ArgumentException("brick");
            }
            this.borderStartsCount = 0;
            if (visualBrick.Style.BorderDashStyle == BorderDashStyle.Solid)
            {
                this.WriteSolidBorder(writer, exportContext, visualBrick);
            }
            else if (visualBrick.Style.BorderDashStyle == BorderDashStyle.Double)
            {
                this.WriteDoubleBorder(writer, exportContext, visualBrick);
            }
            else
            {
                this.WriteDashedBorder(writer, exportContext, visualBrick);
            }
            this.WriteBrickToXamlCore(writer, visualBrick, exportContext);
            if (base.BrickExportMode == XamlBrickExportMode.ChildElements)
            {
                writer.WriteStartElement(XamlTag.Canvas);
                float num = (visualBrick.BorderStyle == BrickBorderStyle.Outset) ? 0f : ((visualBrick.BorderStyle == BrickBorderStyle.Center) ? 0.5f : 1f);
                float num2 = ((visualBrick.Sides & BorderSide.Left) > BorderSide.None) ? (-visualBrick.Style.BorderWidth * num) : 0f;
                float num3 = ((visualBrick.Sides & BorderSide.Top) > BorderSide.None) ? (-visualBrick.Style.BorderWidth * num) : 0f;
                float[] values = new float[] { num2, num3 };
                writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.VisualHelperOffset, values);
            }
        }

        protected virtual void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
        }

        private void WriteDashedBorder(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick)
        {
            RectangleF borderBoundsInPixels = this.GetBorderBoundsInPixels(visualBrick);
            float x = -visualBrick.Style.BorderWidth / 2f;
            RectangleF rect = RectangleF.Inflate(borderBoundsInPixels, x, x);
            if ((visualBrick.Style.Sides & BorderSide.Left) > BorderSide.None)
            {
                WriteLine(writer, exportContext, visualBrick, RectHelper.TopLeft(rect), RectHelper.BottomLeft(rect));
            }
            if ((visualBrick.Style.Sides & BorderSide.Right) > BorderSide.None)
            {
                WriteLine(writer, exportContext, visualBrick, RectHelper.BottomRight(rect), RectHelper.TopRight(rect));
            }
            if ((visualBrick.Style.Sides & BorderSide.Top) > BorderSide.None)
            {
                WriteLine(writer, exportContext, visualBrick, RectHelper.TopRight(rect), RectHelper.TopLeft(rect));
            }
            if ((visualBrick.Style.Sides & BorderSide.Bottom) > BorderSide.None)
            {
                WriteLine(writer, exportContext, visualBrick, RectHelper.BottomLeft(rect), RectHelper.BottomRight(rect));
            }
            this.WriteBorderStart(writer, exportContext, visualBrick, borderBoundsInPixels);
            WriteAttachedValues(writer, exportContext, visualBrick);
        }

        private void WriteDoubleBorder(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick)
        {
            float num = visualBrick.Style.BorderWidth / 3f;
            RectangleF borderBoundsInPixels = this.GetBorderBoundsInPixels(visualBrick);
            this.WriteBorderStart(writer, exportContext, visualBrick, borderBoundsInPixels);
            WriteAttachedValues(writer, exportContext, visualBrick);
            writer.WriteAttribute(XamlAttribute.BorderThickness, GetThicknessValues(visualBrick.Style.Sides, num));
            writer.WriteAttribute(XamlAttribute.Padding, GetThicknessValues(visualBrick.Style.Sides, num));
            borderBoundsInPixels = RectHelper.InflateRect(borderBoundsInPixels, -num * 2f, visualBrick.Style.Sides);
            this.WriteBorderStart(writer, exportContext, visualBrick, borderBoundsInPixels);
            writer.WriteAttribute(XamlAttribute.BorderThickness, GetThicknessValues(visualBrick.Style.Sides, num));
        }

        public override void WriteEndTags(XamlWriter writer)
        {
            for (int i = 0; i < this.borderStartsCount; i++)
            {
                writer.WriteEndElement();
            }
        }

        private static void WriteLine(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick, PointF startPoint, PointF endPoint)
        {
            writer.WriteStartElement(XamlTag.Line);
            if (exportContext.ResourceMap.BorderDashStylesDictionary.ContainsKey(visualBrick))
            {
                string str = exportContext.ResourceMap.BorderDashStylesDictionary[visualBrick];
                writer.WriteAttribute(XamlAttribute.Style, $"{{StaticResource {str}}}");
            }
            writer.WriteAttribute(XamlAttribute.X1, startPoint.X);
            writer.WriteAttribute(XamlAttribute.X2, endPoint.X);
            writer.WriteAttribute(XamlAttribute.Y1, startPoint.Y);
            writer.WriteAttribute(XamlAttribute.Y2, endPoint.Y);
            writer.WriteAttribute(XamlAttribute.StrokeDashArray, VisualBrick.GetDashPattern(visualBrick.Style.BorderDashStyle));
            writer.WriteAttribute(XamlAttribute.CanvasZIndex, (float) 1f);
            writer.WriteEndElement();
        }

        private void WriteSolidBorder(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick)
        {
            this.WriteBorderStart(writer, exportContext, visualBrick, this.GetBorderBoundsInPixels(visualBrick));
            WriteAttachedValues(writer, exportContext, visualBrick);
        }

        private static void WriteTagAttribute(XamlWriter writer, XamlExportContext exportContext, VisualBrick visualBrick)
        {
            writer.WriteAttribute(XamlAttribute.Tag, DocumentMapTreeViewNodeHelper.GetTagByIndices(exportContext.Page.GetIndicesByBrick(visualBrick), exportContext.PageNumber - 1));
        }
    }
}

