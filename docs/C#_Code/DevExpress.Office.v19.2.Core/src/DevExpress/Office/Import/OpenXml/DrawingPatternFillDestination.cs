namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class DrawingPatternFillDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static Dictionary<string, DrawingPatternType> patternTypeTable;
        private static object syncPatternTypeTable = new object();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingPatternFill patternFill;

        public DrawingPatternFillDestination(DestinationAndXmlBasedImporter importer, DrawingPatternFill patternFill) : base(importer)
        {
            this.patternFill = patternFill;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("bgClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingPatternFillDestination.OnBackgroundColor));
            table.Add("fgClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingPatternFillDestination.OnForegroundColor));
            return table;
        }

        private static DrawingPatternFillDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingPatternFillDestination) importer.PeekDestination();

        private static Destination OnBackgroundColor(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingColorDestination(importer, GetThis(importer).patternFill.BackgroundColor);

        private static Destination OnForegroundColor(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingColorDestination(importer, GetThis(importer).patternFill.ForegroundColor);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.patternFill.PatternType = this.Importer.GetWpEnumValue<DrawingPatternType>(reader, "prst", PatternTypeTable, DrawingPatternType.Percent5);
        }

        private static Dictionary<string, DrawingPatternType> PatternTypeTable
        {
            get
            {
                if (patternTypeTable == null)
                {
                    object syncPatternTypeTable = DrawingPatternFillDestination.syncPatternTypeTable;
                    lock (syncPatternTypeTable)
                    {
                        patternTypeTable ??= DictionaryUtils.CreateBackTranslationTable<DrawingPatternType>(OpenXmlExporterBase.DrawingPatternTypeTable);
                    }
                }
                return patternTypeTable;
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

