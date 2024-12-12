namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class GroupShapeLocksDestination : CommonDrawingLockingDestination
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly GroupShapeLocks locks;

        public GroupShapeLocksDestination(DestinationAndXmlBasedImporter importer, GroupShapeLocks locks) : base(importer, locks)
        {
            this.locks = locks;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable() => 
            new ElementHandlerTable<DestinationAndXmlBasedImporter>();

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ProcessElementOpen(reader);
            this.locks.NoResize = this.Importer.GetWpSTOnOffValue(reader, "noResize", false);
            this.locks.NoRotate = this.Importer.GetWpSTOnOffValue(reader, "noRot", false);
            this.locks.NoUngroup = this.Importer.GetWpSTOnOffValue(reader, "noUngrp", false);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

