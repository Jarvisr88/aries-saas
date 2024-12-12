namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using System;
    using System.Xml;

    public class OutlineRoundLineJoinDestination : OutlinePropertiesDestinationBase
    {
        public OutlineRoundLineJoinDestination(DestinationAndXmlBasedImporter importer, Outline outline) : base(importer, outline)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.Outline.JoinStyle = LineJoinStyle.Round;
        }
    }
}

