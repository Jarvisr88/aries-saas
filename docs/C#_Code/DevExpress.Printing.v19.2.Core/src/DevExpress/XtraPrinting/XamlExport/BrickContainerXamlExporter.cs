namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class BrickContainerXamlExporter : BrickXamlExporterBase
    {
        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.ChildElements;

        public override void WriteBrickToXaml(XamlWriter writer, BrickBase brick, XamlExportContext exportContext, RectangleF clipRect, Action<XamlWriter> declareNamespaces, Action<XamlWriter, object> writeCustomProperties)
        {
            writer.WriteStartElement(XamlTag.Canvas);
            float num = ((float) (brick.X + brick.InnerBrickListOffset.X)).DocToDip();
            writer.WriteAttribute(XamlAttribute.CanvasLeft, num);
            float num2 = ((float) (brick.Y + brick.InnerBrickListOffset.Y)).DocToDip();
            writer.WriteAttribute(XamlAttribute.CanvasTop, num2);
            if (!brick.NoClip)
            {
                writer.WriteStartElement(XamlTag.CanvasClip);
                writer.WriteStartElement(XamlTag.RectangleGeometry);
                BrickContainer container = (BrickContainer) brick;
                RectangleF ef = new RectangleF(container.Brick.X, -container.InnerBrickListOffset.Y, container.Width, container.Height).DocToDip();
                writer.WriteAttribute(XamlAttribute.Rect, ef);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        public override void WriteEndTags(XamlWriter writer)
        {
            writer.WriteEndElement();
        }
    }
}

