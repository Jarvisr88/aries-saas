namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingLockingDestination : CommonDrawingLockingDestination
    {
        private readonly IDrawingLocks drawingLocks;

        public DrawingLockingDestination(DestinationAndXmlBasedImporter importer, IDrawingLocks drawingLocks) : base(importer, drawingLocks)
        {
            this.drawingLocks = drawingLocks;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.drawingLocks.NoAdjustHandles = this.Importer.GetWpSTOnOffValue(reader, "noAdjustHandles", false);
            this.drawingLocks.NoChangeArrowheads = this.Importer.GetWpSTOnOffValue(reader, "noChangeArrowheads", false);
            this.drawingLocks.NoChangeShapeType = this.Importer.GetWpSTOnOffValue(reader, "noChangeShapeType", false);
            this.drawingLocks.NoEditPoints = this.Importer.GetWpSTOnOffValue(reader, "noEditPoints", false);
            this.drawingLocks.NoResize = this.Importer.GetWpSTOnOffValue(reader, "noResize", false);
            this.drawingLocks.NoRotate = this.Importer.GetWpSTOnOffValue(reader, "noRot", false);
        }
    }
}

