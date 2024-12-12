namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.OpenXml.Export;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class DrawingBlipFillTileDestination : LeafElementDestination<DestinationAndXmlBasedImporter>
    {
        private static Dictionary<string, RectangleAlignType> tileAlignTable;
        private static object syncTileAlignTable = new object();
        private static readonly Dictionary<string, TileFlipType> tileFlipTable = DictionaryUtils.CreateBackTranslationTable<TileFlipType>(OpenXmlExporterBase.TileFlipTypeTable);
        private readonly DrawingBlipFill fill;

        public DrawingBlipFillTileDestination(DestinationAndXmlBasedImporter importer, DrawingBlipFill fill) : base(importer)
        {
            this.fill = fill;
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.fill.Stretch = false;
            this.fill.TileAlign = this.Importer.GetWpEnumValue<RectangleAlignType>(reader, "algn", TileAlignTable, RectangleAlignType.TopLeft);
            this.fill.TileFlip = this.Importer.GetWpEnumValue<TileFlipType>(reader, "flip", tileFlipTable, TileFlipType.None);
            this.fill.ScaleX = this.Importer.GetIntegerValue(reader, "sx", 0);
            this.fill.ScaleY = this.Importer.GetIntegerValue(reader, "sy", 0);
            this.fill.OffsetX = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(this.Importer.GetLongValue(reader, "tx", 0L));
            this.fill.OffsetY = this.Importer.DocumentModel.UnitConverter.EmuToModelUnitsL(this.Importer.GetLongValue(reader, "ty", 0L));
        }

        private static Dictionary<string, RectangleAlignType> TileAlignTable
        {
            get
            {
                if (tileAlignTable == null)
                {
                    object syncTileAlignTable = DrawingBlipFillTileDestination.syncTileAlignTable;
                    lock (syncTileAlignTable)
                    {
                        tileAlignTable ??= DictionaryUtils.CreateBackTranslationTable<RectangleAlignType>(OpenXmlExporterBase.RectangleAlignTypeTable);
                    }
                }
                return tileAlignTable;
            }
        }
    }
}

