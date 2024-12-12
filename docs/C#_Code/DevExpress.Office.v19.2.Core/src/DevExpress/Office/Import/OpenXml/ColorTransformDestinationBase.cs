namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class ColorTransformDestinationBase : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DrawingColor color;

        protected ColorTransformDestinationBase(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer)
        {
            this.color = color;
        }

        protected int GetIntegerValue(XmlReader reader) => 
            this.Importer.GetIntegerValue(reader, "val");

        protected DrawingColor Color =>
            this.color;
    }
}

