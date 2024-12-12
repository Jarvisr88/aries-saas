namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ExtentsDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly Transform2D xfrm;

        public ExtentsDestination(DestinationAndXmlBasedImporter importer, Transform2D xfrm) : base(importer)
        {
            this.xfrm = xfrm;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            long num = this.Importer.GetLongValue(reader, "cx", -1L);
            long num2 = this.Importer.GetLongValue(reader, "cy", -1L);
            if ((num < 0L) || (num2 < 0L))
            {
                this.Importer.ThrowInvalidFile();
            }
            this.xfrm.Cx = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsD((double) num);
            this.xfrm.Cy = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsD((double) num2);
        }
    }
}

