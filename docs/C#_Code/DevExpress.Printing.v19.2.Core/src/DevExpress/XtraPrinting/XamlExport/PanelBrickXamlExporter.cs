namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    internal class PanelBrickXamlExporter : VisualBrickXamlExporter
    {
        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.ChildElements;

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            writer.WriteAttribute(XamlAttribute.Padding, (float) 0f);
            if (!brick.NoClip)
            {
                writer.WriteAttribute(XamlNsPrefix.dxpn, XamlAttribute.VisualHelperClipToBounds, true.ToString());
            }
        }

        public override void WriteEndTags(XamlWriter writer)
        {
            base.WriteEndTags(writer);
            writer.WriteEndElement();
        }
    }
}

