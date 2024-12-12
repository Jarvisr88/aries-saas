namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class DrawingGradientPathDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static Dictionary<string, GradientType> gradientTypeTable;
        private static object syncGradientTypeTable = new object();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingGradientFill fill;

        public DrawingGradientPathDestination(DestinationAndXmlBasedImporter importer, DrawingGradientFill fill) : base(importer)
        {
            this.fill = fill;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("fillToRect", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingGradientPathDestination.OnFillRectangle));
            return table;
        }

        private static DrawingGradientPathDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingGradientPathDestination) importer.PeekDestination();

        private static Destination OnFillRectangle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingGradientFill fill = GetThis(importer).fill;
            return new RelativeRectangleDestination(importer, delegate (RectangleOffset value) {
                fill.FillRect = value;
            });
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.fill.GradientType = this.Importer.GetWpEnumValue<GradientType>(reader, "path", GradientTypeTable, GradientType.Circle);
        }

        private static Dictionary<string, GradientType> GradientTypeTable
        {
            get
            {
                if (gradientTypeTable == null)
                {
                    object syncGradientTypeTable = DrawingGradientPathDestination.syncGradientTypeTable;
                    lock (syncGradientTypeTable)
                    {
                        gradientTypeTable ??= DictionaryUtils.CreateBackTranslationTable<GradientType>(OpenXmlExporterBase.GradientTypeTable);
                    }
                }
                return gradientTypeTable;
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

