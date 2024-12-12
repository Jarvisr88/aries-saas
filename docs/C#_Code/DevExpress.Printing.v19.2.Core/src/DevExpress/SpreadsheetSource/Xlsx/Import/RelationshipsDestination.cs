namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class RelationshipsDestination : ElementDestination<XlsxImporter>
    {
        private static readonly ElementHandlerTable<XlsxImporter> handlerTable = CreateElementHandlerTable();
        private readonly OpenXmlRelationCollection relations;

        public RelationshipsDestination(XlsxImporter importer, OpenXmlRelationCollection relations) : base(importer)
        {
            Guard.ArgumentNotNull(relations, "relations");
            this.relations = relations;
        }

        private static ElementHandlerTable<XlsxImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxImporter> table = new ElementHandlerTable<XlsxImporter>();
            table.Add("Relationship", new ElementHandler<XlsxImporter>(RelationshipsDestination.OnRelation));
            return table;
        }

        private static RelationshipsDestination GetThis(XlsxImporter importer) => 
            (RelationshipsDestination) importer.PeekDestination();

        private static Destination OnRelation(XlsxImporter importer, XmlReader reader) => 
            new RelationshipDestination(importer, GetThis(importer).relations);

        protected internal override ElementHandlerTable<XlsxImporter> ElementHandlerTable =>
            handlerTable;
    }
}

