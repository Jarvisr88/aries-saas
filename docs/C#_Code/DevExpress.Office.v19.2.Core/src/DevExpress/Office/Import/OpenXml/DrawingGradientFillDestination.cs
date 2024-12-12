namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class DrawingGradientFillDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static Dictionary<string, TileFlipType> tileFlipTable;
        private static object syncTileFlipTable = new object();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingGradientFill fill;

        public DrawingGradientFillDestination(DestinationAndXmlBasedImporter importer, DrawingGradientFill fill) : base(importer)
        {
            this.fill = fill;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("gsLst", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingGradientFillDestination.OnGradientStopList));
            table.Add("lin", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingGradientFillDestination.OnLinearGradient));
            table.Add("path", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingGradientFillDestination.OnPathGradient));
            table.Add("tileRect", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingGradientFillDestination.OnTileRectangle));
            return table;
        }

        private static DrawingGradientFillDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingGradientFillDestination) importer.PeekDestination();

        private static Destination OnGradientStopList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingGradientStopListDestination(importer, GetThis(importer).fill);

        private static Destination OnLinearGradient(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingGradientLinearDestination(importer, GetThis(importer).fill);

        private static Destination OnPathGradient(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingGradientPathDestination(importer, GetThis(importer).fill);

        private static Destination OnTileRectangle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingGradientFill fill = GetThis(importer).fill;
            return new RelativeRectangleDestination(importer, delegate (RectangleOffset value) {
                fill.TileRect = value;
            });
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            this.fill.EndUpdate();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.fill.BeginUpdate();
            string str = this.Importer.ReadAttribute(reader, "flip");
            if (!string.IsNullOrEmpty(str))
            {
                this.fill.Flip = this.Importer.GetWpEnumValueCore<TileFlipType>(str, TileFlipTable, TileFlipType.None);
            }
            str = this.Importer.ReadAttribute(reader, "rotWithShape");
            if (!string.IsNullOrEmpty(str))
            {
                this.fill.RotateWithShape = this.Importer.GetOnOffValue(str, true);
            }
        }

        private static Dictionary<string, TileFlipType> TileFlipTable
        {
            get
            {
                if (tileFlipTable == null)
                {
                    object syncTileFlipTable = DrawingGradientFillDestination.syncTileFlipTable;
                    lock (syncTileFlipTable)
                    {
                        tileFlipTable ??= DictionaryUtils.CreateBackTranslationTable<TileFlipType>(OpenXmlExporterBase.TileFlipTypeTable);
                    }
                }
                return tileFlipTable;
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

