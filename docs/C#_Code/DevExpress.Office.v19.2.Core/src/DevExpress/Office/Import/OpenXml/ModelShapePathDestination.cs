namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class ModelShapePathDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly Dictionary<PathFillMode, string> pathFillModeTable = GetPathFillModeTable();
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelShapePath modelShapePath;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public ModelShapePathDestination(DestinationAndXmlBasedImporter importer, ModelShapePath modelShapePath, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.modelShapePath = modelShapePath;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("close", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathDestination.OnClose));
            table.Add("moveTo", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathDestination.OnMoveTo));
            table.Add("lnTo", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathDestination.OnLineTo));
            table.Add("arcTo", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathDestination.OnArcTo));
            table.Add("quadBezTo", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathDestination.OnQuadraticBezierTo));
            table.Add("cubicBezTo", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapePathDestination.OnCubicBezierTo));
            return table;
        }

        private static Dictionary<PathFillMode, string> GetPathFillModeTable() => 
            new Dictionary<PathFillMode, string> { 
                { 
                    PathFillMode.None,
                    "none"
                },
                { 
                    PathFillMode.Norm,
                    "norm"
                },
                { 
                    PathFillMode.Lighten,
                    "lighten"
                },
                { 
                    PathFillMode.LightenLess,
                    "lightenLess"
                },
                { 
                    PathFillMode.Darken,
                    "darken"
                },
                { 
                    PathFillMode.DarkenLess,
                    "darkenLess"
                }
            };

        private static ModelShapePathDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ModelShapePathDestination) importer.PeekDestination();

        private static Destination OnArcTo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PathArc item = new PathArc();
            GetThis(importer).modelShapePath.Instructions.Add(item);
            return new PathArcDestination(importer, item, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);
        }

        private static Destination OnClose(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PathClose item = new PathClose();
            GetThis(importer).modelShapePath.Instructions.Add(item);
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        private static Destination OnCubicBezierTo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PathCubicBezier item = new PathCubicBezier(importer.DocumentModel.MainPart);
            GetThis(importer).modelShapePath.Instructions.Add(item);
            return new PathCubicBezierDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        private static Destination OnLineTo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PathLine item = new PathLine();
            GetThis(importer).modelShapePath.Instructions.Add(item);
            return new PathLineDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        private static Destination OnMoveTo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PathMove item = new PathMove();
            GetThis(importer).modelShapePath.Instructions.Add(item);
            return new PathMoveDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        private static Destination OnQuadraticBezierTo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PathQuadraticBezier item = new PathQuadraticBezier(importer.DocumentModel.MainPart);
            GetThis(importer).modelShapePath.Instructions.Add(item);
            return new PathQuadraticBezierDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.modelShapePath.Width = this.Importer.GetLongValue(reader, "w", 0L);
            if (this.modelShapePath.Width < 0L)
            {
                this.Importer.ThrowInvalidFile();
            }
            this.modelShapePath.Height = this.Importer.GetLongValue(reader, "h", 0L);
            if (this.modelShapePath.Height < 0L)
            {
                this.Importer.ThrowInvalidFile();
            }
            this.modelShapePath.FillMode = this.Importer.GetWpEnumValue<PathFillMode>(reader, "fill", pathFillModeTable, PathFillMode.Norm);
            this.modelShapePath.Stroke = this.Importer.GetWpSTOnOffValue(reader, "stroke", true);
            this.modelShapePath.ExtrusionOK = this.Importer.GetWpSTOnOffValue(reader, "extrusionOk", true);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

