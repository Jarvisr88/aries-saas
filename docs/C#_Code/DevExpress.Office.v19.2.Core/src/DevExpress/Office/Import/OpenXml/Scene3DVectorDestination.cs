namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using System;
    using System.Xml;

    public class Scene3DVectorDestination : Scene3DAnchorPointDestination
    {
        public Scene3DVectorDestination(DestinationAndXmlBasedImporter importer, Scene3DVector vector) : base(importer, vector)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.CreateVector(reader, "dx", "dy", "dz");
        }
    }
}

