namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using System;
    using System.Xml;

    public class Scene3DAnchorPointDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly Scene3DVector vector;

        public Scene3DAnchorPointDestination(DestinationAndXmlBasedImporter importer, Scene3DVector vector) : base(importer)
        {
            this.vector = vector;
        }

        protected void CreateVector(XmlReader reader, string xName, string yName, string zName)
        {
            this.vector.DocumentModel.BeginUpdate();
            try
            {
                this.vector.X = this.GetCoordinate(reader, xName);
                this.vector.Y = this.GetCoordinate(reader, yName);
                this.vector.Z = this.GetCoordinate(reader, zName);
            }
            finally
            {
                this.vector.DocumentModel.EndUpdate();
            }
        }

        private long GetCoordinate(XmlReader reader, string coordinateName)
        {
            long defaultValue = -27273042329601L;
            long num2 = this.Importer.GetLongValue(reader, coordinateName, defaultValue);
            if (num2 == defaultValue)
            {
                this.Importer.ThrowInvalidFile();
            }
            DrawingValueChecker.CheckCoordinate(num2, coordinateName);
            return this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(num2);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.CreateVector(reader, "x", "y", "z");
        }
    }
}

