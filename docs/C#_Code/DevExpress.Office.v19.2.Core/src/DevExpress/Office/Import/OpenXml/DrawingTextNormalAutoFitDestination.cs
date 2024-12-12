namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using System;
    using System.Xml;

    public class DrawingTextNormalAutoFitDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DrawingTextNormalAutoFit autoFit;

        public DrawingTextNormalAutoFitDestination(DestinationAndXmlBasedImporter importer, DrawingTextNormalAutoFit autoFit) : base(importer)
        {
            this.autoFit = autoFit;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.autoFit.FontScale = this.Importer.GetIntegerValue(reader, "fontScale", 0x186a0);
            this.autoFit.LineSpaceReduction = this.Importer.GetIntegerValue(reader, "lnSpcReduction", 0);
        }
    }
}

