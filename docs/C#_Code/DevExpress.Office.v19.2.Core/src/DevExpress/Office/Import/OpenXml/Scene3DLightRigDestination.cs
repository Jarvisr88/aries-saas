namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class Scene3DLightRigDestination : Scene3DInfoDestinationBase
    {
        public Scene3DLightRigDestination(DestinationAndXmlBasedImporter importer, IDrawingCache cache, Scene3DProperties properties, Scene3DPropertiesInfo info) : base(importer, cache, properties, info)
        {
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (base.HasRotation)
            {
                base.Info.HasLightRigRotation = true;
            }
            base.Properties.AssignLightRigRotationInfoIndex(base.Cache.Scene3DRotationInfoCache.AddItem(base.RotationInfo));
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            LightRigDirection? nullable = this.Importer.GetWpEnumOnOffNullValue<LightRigDirection>(reader, "dir", OpenXmlExporterBase.LightRigDirectionTable);
            if (nullable == null)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.Info.LightRigDirection = nullable.Value;
            LightRigPreset? nullable2 = this.Importer.GetWpEnumOnOffNullValue<LightRigPreset>(reader, "rig", OpenXmlExporterBase.LightRigPresetTable);
            if (nullable2 == null)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.Info.LightRigPreset = nullable2.Value;
        }
    }
}

