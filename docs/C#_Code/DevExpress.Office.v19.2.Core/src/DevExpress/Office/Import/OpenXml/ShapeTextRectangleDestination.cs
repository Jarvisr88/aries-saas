namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ShapeTextRectangleDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly AdjustableRect textBoxRectangle;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public ShapeTextRectangleDestination(DestinationAndXmlBasedImporter importer, AdjustableRect textBoxRectangle, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.textBoxRectangle = textBoxRectangle;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            AdjustableCoordinate cachedAdjustableCoordinate = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.Importer.ReadAttribute(reader, "l"));
            AdjustableCoordinate coordinate2 = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.Importer.ReadAttribute(reader, "r"));
            AdjustableCoordinate coordinate3 = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.Importer.ReadAttribute(reader, "t"));
            AdjustableCoordinate coordinate4 = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.Importer.ReadAttribute(reader, "b"));
            if ((cachedAdjustableCoordinate == null) || ((coordinate2 == null) || ((coordinate3 == null) || (coordinate4 == null))))
            {
                this.Importer.ThrowInvalidFile();
            }
            this.textBoxRectangle.Bottom = coordinate4;
            this.textBoxRectangle.Top = coordinate3;
            this.textBoxRectangle.Left = cachedAdjustableCoordinate;
            this.textBoxRectangle.Right = coordinate2;
        }
    }
}

