namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PathArcDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly PathArc pathArc;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public PathArcDestination(DestinationAndXmlBasedImporter importer, PathArc pathArc, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.pathArc = pathArc;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.pathArc.HeightRadius = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("hR"));
            this.pathArc.WidthRadius = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("wR"));
            this.pathArc.StartAngle = this.adjustableAngleCache.GetCachedAdjustableAngle(reader.GetAttribute("stAng"));
            this.pathArc.SwingAngle = this.adjustableAngleCache.GetCachedAdjustableAngle(reader.GetAttribute("swAng"));
            if ((ReferenceEquals(this.pathArc.HeightRadius, null) | ReferenceEquals(this.pathArc.WidthRadius, null)) || ((this.pathArc.StartAngle == null) || (this.pathArc.SwingAngle == null)))
            {
                this.Importer.ThrowInvalidFile();
            }
        }
    }
}

