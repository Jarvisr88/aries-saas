namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class FillOverlayEffectDestination : FillEffectDestination
    {
        private BlendMode? blendMode;

        public FillOverlayEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new FillOverlayEffect(base.Fill, this.blendMode.Value);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.blendMode = this.Importer.GetWpEnumOnOffNullValue<BlendMode>(reader, "blend", OpenXmlExporterBase.BlendModeTable);
            if (this.blendMode == null)
            {
                this.Importer.ThrowInvalidFile();
            }
        }
    }
}

