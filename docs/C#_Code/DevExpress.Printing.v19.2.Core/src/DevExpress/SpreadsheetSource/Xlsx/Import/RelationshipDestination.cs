namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Xml;

    public class RelationshipDestination : LeafElementDestination<XlsxImporter>
    {
        private readonly OpenXmlRelationCollection relations;

        public RelationshipDestination(XlsxImporter importer, OpenXmlRelationCollection relations) : base(importer)
        {
            Guard.ArgumentNotNull(relations, "relations");
            this.relations = relations;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            OpenXmlRelation item = new OpenXmlRelation {
                Id = this.Importer.ReadAttribute(reader, "Id")
            };
            if (!string.IsNullOrEmpty(item.Id))
            {
                item.Type = this.Importer.ReadAttribute(reader, "Type");
                if (!string.IsNullOrEmpty(item.Type))
                {
                    string str = this.Importer.ReadAttribute(reader, "Target");
                    if (!string.IsNullOrEmpty(str))
                    {
                        item.Target = DXHttpUtility.UrlDecode(str);
                        item.TargetMode = this.Importer.ReadAttribute(reader, "TargetMode");
                        this.relations.Add(item);
                    }
                }
            }
        }
    }
}

