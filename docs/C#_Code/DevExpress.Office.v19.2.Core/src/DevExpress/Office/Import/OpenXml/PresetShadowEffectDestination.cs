namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class PresetShadowEffectDestination : DrawingColorEffectDestinationBase
    {
        private PresetShadowType? type;
        private OffsetShadowInfo info;

        public PresetShadowEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new PresetShadowEffect(this.Color, this.type.Value, this.info);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.type = this.Importer.GetWpEnumOnOffNullValue<PresetShadowType>(reader, "prst", OpenXmlExporterBase.PresetShadowTypeTable);
            if (this.type == null)
            {
                this.Importer.ThrowInvalidFile();
            }
            this.info = ImportShadowEffectHelper.GetOffsetShadowInfo(this.Importer, reader);
        }
    }
}

