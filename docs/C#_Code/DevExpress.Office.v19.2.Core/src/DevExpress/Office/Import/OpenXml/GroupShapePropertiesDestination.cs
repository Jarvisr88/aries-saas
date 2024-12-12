namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class GroupShapePropertiesDestination : DrawingFillDestinationBase
    {
        public static Dictionary<OpenXmlBlackWhiteMode, string> BlackWhiteModeTable = CreateBlackWhiteModeTable();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly GroupShapeProperties groupShapeProperties;

        public GroupShapePropertiesDestination(DestinationAndXmlBasedImporter importer, GroupShapeProperties groupShapeProperties) : base(importer)
        {
            this.groupShapeProperties = groupShapeProperties;
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
            table.Add("xfrm", new ElementHandler<DestinationAndXmlBasedImporter>(GroupShapePropertiesDestination.OnXfrm));
            table.Add("effectDag", new ElementHandler<DestinationAndXmlBasedImporter>(GroupShapePropertiesDestination.OnEffectGraph));
            table.Add("effectLst", new ElementHandler<DestinationAndXmlBasedImporter>(GroupShapePropertiesDestination.OnEffectList));
            table.Add("scene3d", new ElementHandler<DestinationAndXmlBasedImporter>(GroupShapePropertiesDestination.OnScene3DProperties));
            AddFillHandlers(table);
            return table;
        }

        private static GroupShapePropertiesDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (GroupShapePropertiesDestination) importer.PeekDestination();

        private static Destination OnEffectGraph(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingEffectsDAGDestination(importer, GetThis(importer).groupShapeProperties.EffectStyle.ContainerEffect);

        private static Destination OnEffectList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingEffectsListDestination(importer, GetThis(importer).groupShapeProperties.EffectStyle.ContainerEffect);

        private static Destination OnScene3DProperties(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new Scene3DPropertiesDestination(importer, GetThis(importer).groupShapeProperties.EffectStyle.Scene3DProperties);

        private static Destination OnXfrm(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GroupShapeProperties groupShapeProperties = GetThis(importer).groupShapeProperties;
            return new GroupXfrmDestination(importer, groupShapeProperties.Transform2D, groupShapeProperties.ChildTransform2D);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (base.Fill != null)
            {
                this.groupShapeProperties.Fill = base.Fill;
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.groupShapeProperties.BlackAndWhiteMode = this.Importer.GetWpEnumValue<OpenXmlBlackWhiteMode>(reader, "bwMode", BlackWhiteModeTable, OpenXmlBlackWhiteMode.Empty);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

