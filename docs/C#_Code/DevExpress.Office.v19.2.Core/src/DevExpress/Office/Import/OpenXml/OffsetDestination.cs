namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class OffsetDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly Transform2D xfrm;

        public OffsetDestination(DestinationAndXmlBasedImporter importer, Transform2D xfrm) : base(importer)
        {
            this.xfrm = xfrm;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            long num;
            long num2;
            base.ProcessElementOpen(reader);
            string s = this.Importer.ReadAttribute(reader, "y");
            if (!long.TryParse(this.Importer.ReadAttribute(reader, "x"), out num))
            {
                this.Importer.ThrowInvalidFile();
            }
            if (!long.TryParse(s, out num2))
            {
                this.Importer.ThrowInvalidFile();
            }
            this.xfrm.OffsetX = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsD((double) num);
            this.xfrm.OffsetY = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsD((double) num2);
        }
    }
}

