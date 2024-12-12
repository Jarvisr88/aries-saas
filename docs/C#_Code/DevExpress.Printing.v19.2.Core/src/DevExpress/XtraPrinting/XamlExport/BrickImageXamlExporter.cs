namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    internal class BrickImageXamlExporter : VisualBrickXamlExporter
    {
        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.Content;

        public override bool RequiresImageResource() => 
            true;

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            VisualBrick brick2 = brick;
            if (brick2 == null)
            {
                throw new ArgumentException("brick");
            }
            writer.WriteStartElement(XamlTag.Image);
            writer.WriteAttribute(XamlAttribute.Source, exportContext.ResourceMap.ImageResourcesDictionary[brick2]);
            if (exportContext.Compatibility == XamlCompatibility.WPF)
            {
                writer.WriteAttribute(XamlAttribute.RenderOptionsBitmapScalingMode, XamlNames.Fant);
            }
            writer.WriteEndElement();
        }
    }
}

