namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class LineBrickXamlExporter : VisualBrickXamlExporter
    {
        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.Content;

        private static RectangleF GetLineClientRectInPixels(LineBrick lineBrick)
        {
            RectangleF ef = new RectangleF(PointF.Empty, lineBrick.Size).DocToDip();
            ef.Width = (ef.Width - GetBorderWidth(BorderSide.Left, lineBrick)) - GetBorderWidth(BorderSide.Right, lineBrick);
            ef.Height = (ef.Height - GetBorderWidth(BorderSide.Top, lineBrick)) - GetBorderWidth(BorderSide.Bottom, lineBrick);
            return ef;
        }

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            LineBrick lineBrick = brick as LineBrick;
            if (lineBrick == null)
            {
                throw new ArgumentException("brick");
            }
            writer.WriteStartElement(XamlTag.Line);
            writer.WriteAttribute(XamlAttribute.Style, $"{{StaticResource {exportContext.ResourceMap.LineStylesDictionary[lineBrick]}}}");
            RectangleF lineClientRectInPixels = GetLineClientRectInPixels(lineBrick);
            writer.WriteAttribute(XamlAttribute.X1, lineBrick.GetPoint1(lineClientRectInPixels).X);
            writer.WriteAttribute(XamlAttribute.Y1, lineBrick.GetPoint1(lineClientRectInPixels).Y);
            writer.WriteAttribute(XamlAttribute.X2, lineBrick.GetPoint2(lineClientRectInPixels).X);
            writer.WriteAttribute(XamlAttribute.Y2, lineBrick.GetPoint2(lineClientRectInPixels).Y);
            writer.WriteAttribute(XamlAttribute.StrokeDashArray, VisualBrick.GetDashPattern(lineBrick.LineStyle));
            writer.WriteEndElement();
        }
    }
}

