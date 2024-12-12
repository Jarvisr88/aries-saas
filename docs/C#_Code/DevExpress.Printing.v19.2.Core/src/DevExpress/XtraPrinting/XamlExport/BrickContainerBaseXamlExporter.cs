namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class BrickContainerBaseXamlExporter : BrickXamlExporterBase
    {
        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.ChildElements;

        public override void WriteBrickToXaml(XamlWriter writer, BrickBase brick, XamlExportContext exportContext, RectangleF clipRect, Action<XamlWriter> declareNamespaces, Action<XamlWriter, object> writeCustomProperties)
        {
            writer.WriteStartElement(XamlTag.Canvas);
            BrickContainerBase base2 = (BrickContainerBase) brick;
            if (base2.Brick != null)
            {
                float num = base2.Brick.Width.DocToDip();
                float num2 = base2.Brick.Height.DocToDip();
                writer.WriteAttribute(XamlAttribute.Width, num);
                writer.WriteAttribute(XamlAttribute.Height, num2);
            }
            writer.WriteAttribute(XamlAttribute.CanvasLeft, ((float) (brick.X + brick.InnerBrickListOffset.X)).DocToDip());
            writer.WriteAttribute(XamlAttribute.CanvasTop, ((float) (brick.Y + brick.InnerBrickListOffset.Y)).DocToDip());
            if (base2.Brick != null)
            {
                writer.WriteAttribute(XamlAttribute.Tag, DocumentMapTreeViewNodeHelper.GetTagByIndices(exportContext.Page.GetIndicesByBrick(base2.Brick), exportContext.PageNumber - 1));
            }
        }

        public override void WriteEndTags(XamlWriter writer)
        {
            writer.WriteEndElement();
        }
    }
}

