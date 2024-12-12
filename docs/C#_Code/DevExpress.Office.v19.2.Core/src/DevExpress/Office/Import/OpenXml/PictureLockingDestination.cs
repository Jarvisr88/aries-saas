namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PictureLockingDestination : DrawingLockingDestination
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private PictureLocks locks;

        public PictureLockingDestination(DestinationAndXmlBasedImporter importer, PictureLocks locks) : base(importer, locks)
        {
            this.locks = locks;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("extLst", new ElementHandler<DestinationAndXmlBasedImporter>(PictureLockingDestination.OnExtensionList));
            return table;
        }

        private static Destination OnExtensionList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new EmptyDestination<DestinationAndXmlBasedImporter>(importer);

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.locks.NoCrop = this.Importer.GetWpSTOnOffValue(reader, "noCrop", false);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

