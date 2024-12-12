namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class OutlinePropertiesDestinationBase : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DevExpress.Office.Drawing.Outline outline;

        public OutlinePropertiesDestinationBase(DestinationAndXmlBasedImporter importer, DevExpress.Office.Drawing.Outline outline) : base(importer)
        {
            this.outline = outline;
        }

        protected DevExpress.Office.Drawing.Outline Outline =>
            this.outline;
    }
}

