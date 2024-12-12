namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class ShapePropertiesDestination : ShapePropertiesDestinationBase
    {
        public static Dictionary<OpenXmlBlackWhiteMode, string> BlackWhiteModeTable = CreateBlackWhiteModeTable();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public ShapePropertiesDestination(DestinationAndXmlBasedImporter importer, ShapeProperties shapeProperties) : base(importer, shapeProperties)
        {
        }

        private static Dictionary<OpenXmlBlackWhiteMode, string> CreateBlackWhiteModeTable() => 
            new Dictionary<OpenXmlBlackWhiteMode, string> { 
                { 
                    OpenXmlBlackWhiteMode.Auto,
                    "auto"
                },
                { 
                    OpenXmlBlackWhiteMode.Black,
                    "black"
                },
                { 
                    OpenXmlBlackWhiteMode.BlackGray,
                    "blackGray"
                },
                { 
                    OpenXmlBlackWhiteMode.BlackWhite,
                    "blackWhite"
                },
                { 
                    OpenXmlBlackWhiteMode.Clr,
                    "clr"
                },
                { 
                    OpenXmlBlackWhiteMode.Gray,
                    "gray"
                },
                { 
                    OpenXmlBlackWhiteMode.GrayWhite,
                    "grayWhite"
                },
                { 
                    OpenXmlBlackWhiteMode.Hidden,
                    "hidden"
                },
                { 
                    OpenXmlBlackWhiteMode.InvGray,
                    "invGray"
                },
                { 
                    OpenXmlBlackWhiteMode.LtGray,
                    "ltGray"
                },
                { 
                    OpenXmlBlackWhiteMode.White,
                    "white"
                }
            };

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("xfrm", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestination.OnXfrm));
            table.Add("prstGeom", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestination.OnPresetGeometry));
            table.Add("custGeom", new ElementHandler<DestinationAndXmlBasedImporter>(ShapePropertiesDestination.OnCustomGeometry));
            AddCommonHandlers(table);
            return table;
        }

        private static ShapePropertiesDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ShapePropertiesDestination) importer.PeekDestination();

        private static Destination OnCustomGeometry(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ShapeProperties shapeProperties = GetThis(importer).ShapeProperties;
            shapeProperties.ShapeType = ShapePreset.None;
            return new CustomGeometryDestination(importer, shapeProperties.CustomGeometry);
        }

        private static Destination OnPresetGeometry(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new PresetGeometryDestination(importer, GetThis(importer).ShapeProperties);

        private static Destination OnXfrm(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new XfrmDestination(importer, GetThis(importer).ShapeProperties.Transform2D);

        public override void ProcessElementOpen(XmlReader reader)
        {
            base.ShapeProperties.BlackAndWhiteMode = this.Importer.GetWpEnumValue<OpenXmlBlackWhiteMode>(reader, "bwMode", BlackWhiteModeTable, OpenXmlBlackWhiteMode.Empty);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

