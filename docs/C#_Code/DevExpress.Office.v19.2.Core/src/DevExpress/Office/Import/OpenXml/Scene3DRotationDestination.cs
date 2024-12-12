namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using System;
    using System.Xml;

    public class Scene3DRotationDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private Scene3DRotationInfo info;

        public Scene3DRotationDestination(DestinationAndXmlBasedImporter importer, Scene3DRotationInfo info) : base(importer)
        {
            this.info = info;
        }

        private int GetAngle(XmlReader reader, string coordinateName)
        {
            int defaultValue = -2147483648;
            int num2 = this.Importer.GetIntegerValue(reader, coordinateName, defaultValue);
            if (num2 == defaultValue)
            {
                this.Importer.ThrowInvalidFile();
            }
            DrawingValueChecker.CheckPositiveFixedAngle(num2, coordinateName);
            return num2;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.info.Latitude = this.GetAngle(reader, "lat");
            this.info.Longitude = this.GetAngle(reader, "lon");
            this.info.Revolution = this.GetAngle(reader, "rev");
        }
    }
}

