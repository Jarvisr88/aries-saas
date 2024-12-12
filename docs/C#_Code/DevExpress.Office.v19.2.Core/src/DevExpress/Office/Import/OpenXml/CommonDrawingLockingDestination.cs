namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class CommonDrawingLockingDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ICommonDrawingLocks commonDrawingLocks;

        public CommonDrawingLockingDestination(DestinationAndXmlBasedImporter importer, ICommonDrawingLocks commonDrawingLocks) : base(importer)
        {
            this.commonDrawingLocks = commonDrawingLocks;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable() => 
            new ElementHandlerTable<DestinationAndXmlBasedImporter>();

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.commonDrawingLocks.NoChangeAspect = this.Importer.GetWpSTOnOffValue(reader, "noChangeAspect", false);
            this.commonDrawingLocks.NoGroup = this.Importer.GetWpSTOnOffValue(reader, "noGrp", false);
            this.commonDrawingLocks.NoMove = this.Importer.GetWpSTOnOffValue(reader, "noMove", false);
            this.commonDrawingLocks.NoSelect = this.Importer.GetWpSTOnOffValue(reader, "noSelect", false);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

