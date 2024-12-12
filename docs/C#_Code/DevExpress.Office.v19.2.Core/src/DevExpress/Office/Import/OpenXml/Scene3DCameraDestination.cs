namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.OpenXml.Export;
    using System;
    using System.Xml;

    public class Scene3DCameraDestination : Scene3DInfoDestinationBase
    {
        public Scene3DCameraDestination(DestinationAndXmlBasedImporter importer, IDrawingCache cache, Scene3DProperties properties, Scene3DPropertiesInfo info) : base(importer, cache, properties, info)
        {
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (base.HasRotation)
            {
                base.Info.HasCameraRotation = true;
            }
            base.Properties.AssignCameraRotationInfoIndex(base.Cache.Scene3DRotationInfoCache.AddItem(base.RotationInfo));
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            int num = this.Importer.GetIntegerValue(reader, "fov", Scene3DPropertiesInfo.DefaultInfo.Fov);
            DrawingValueChecker.CheckFOVAngle(num, "fovAngle");
            base.Info.Fov = num;
            int num2 = this.Importer.GetIntegerValue(reader, "zoom", Scene3DPropertiesInfo.DefaultInfo.Zoom);
            if (num2 < 0)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.Info.Zoom = num2;
            PresetCameraType? nullable = this.Importer.GetWpEnumOnOffNullValue<PresetCameraType>(reader, "prst", OpenXmlExporterBase.PresetCameraTypeTable);
            if (nullable == null)
            {
                this.Importer.ThrowInvalidFile();
            }
            base.Info.CameraType = nullable.Value;
        }
    }
}

