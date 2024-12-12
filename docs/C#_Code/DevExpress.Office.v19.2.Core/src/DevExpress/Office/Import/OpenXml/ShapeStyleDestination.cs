namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class ShapeStyleDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly Dictionary<string, XlFontSchemeStyles> fontCollectionIndexTable = GetFontCollectionIndexTable();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ShapeStyle shapeStyle;

        public ShapeStyleDestination(DestinationAndXmlBasedImporter importer, ShapeStyle shapeStyle) : base(importer)
        {
            this.shapeStyle = shapeStyle;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("lnRef", new ElementHandler<DestinationAndXmlBasedImporter>(ShapeStyleDestination.OnLineReference));
            table.Add("fillRef", new ElementHandler<DestinationAndXmlBasedImporter>(ShapeStyleDestination.OnFillReference));
            table.Add("effectRef", new ElementHandler<DestinationAndXmlBasedImporter>(ShapeStyleDestination.OnEffectReference));
            table.Add("fontRef", new ElementHandler<DestinationAndXmlBasedImporter>(ShapeStyleDestination.OnFontReference));
            return table;
        }

        private static Dictionary<string, XlFontSchemeStyles> GetFontCollectionIndexTable() => 
            new Dictionary<string, XlFontSchemeStyles> { 
                { 
                    "major",
                    XlFontSchemeStyles.Major
                },
                { 
                    "minor",
                    XlFontSchemeStyles.Minor
                },
                { 
                    "none",
                    XlFontSchemeStyles.None
                }
            };

        private static int GetIntegerIdx(string value)
        {
            int num;
            if (!int.TryParse(value, out num))
            {
                num = 1;
            }
            return num;
        }

        private static ShapeStyleDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ShapeStyleDestination) importer.PeekDestination();

        private static Destination OnEffectReference(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ShapeStyle style = GetThis(importer).shapeStyle;
            return new StyleReferenceDestination(importer, delegate (string value) {
                int integerIdx = GetIntegerIdx(value);
                style.EffectReferenceIdx = integerIdx;
            }, style.EffectColor);
        }

        private static Destination OnFillReference(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ShapeStyle style = GetThis(importer).shapeStyle;
            return new StyleReferenceDestination(importer, delegate (string value) {
                int integerIdx = GetIntegerIdx(value);
                style.FillReferenceIdx = integerIdx;
            }, style.FillColor);
        }

        private static Destination OnFontReference(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ShapeStyle style = GetThis(importer).shapeStyle;
            return new StyleReferenceDestination(importer, delegate (string value) {
                XlFontSchemeStyles none;
                if (!fontCollectionIndexTable.TryGetValue(value, out none))
                {
                    none = XlFontSchemeStyles.None;
                }
                style.FontReferenceIdx = none;
            }, style.FontColor);
        }

        private static Destination OnLineReference(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ShapeStyle style = GetThis(importer).shapeStyle;
            return new StyleReferenceDestination(importer, delegate (string value) {
                int integerIdx = GetIntegerIdx(value);
                style.LineReferenceIdx = integerIdx;
            }, style.LineColor);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

