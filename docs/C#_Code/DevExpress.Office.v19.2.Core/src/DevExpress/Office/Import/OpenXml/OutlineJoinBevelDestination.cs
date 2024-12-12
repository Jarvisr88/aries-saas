namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using System;
    using System.Xml;

    public class OutlineJoinBevelDestination : OutlinePropertiesDestinationBase
    {
        public OutlineJoinBevelDestination(DestinationAndXmlBasedImporter importer, Outline outline) : base(importer, outline)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.Outline.JoinStyle = LineJoinStyle.Bevel;
        }
    }
}

