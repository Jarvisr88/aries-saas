namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class AdjustPoint2DDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly AdjustablePoint adjustablePoint;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public AdjustPoint2DDestination(DestinationAndXmlBasedImporter importer, AdjustablePoint adjustablePoint, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.adjustablePoint = adjustablePoint;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.adjustablePoint.X = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("x"));
            this.adjustablePoint.Y = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("y"));
            if ((this.adjustablePoint.X == null) || (this.adjustablePoint.Y == null))
            {
                this.Importer.ThrowInvalidFile();
            }
        }
    }
}

