namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Import.OpenXml;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;

    public static class OpenXmlDrawingExportHelper
    {
        public const string DrawingMLNamespace = "http://schemas.openxmlformats.org/drawingml/2006/main";
        public const string RelsNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        private static Dictionary<PathFillMode, string> pathFillModeTable;
        private static object syncPathFillModeTable = new object();

        private static string ConvertBoolToString(bool value) => 
            value ? "1" : "0";

        private static Dictionary<PathFillMode, string> CreatePathFillModeTable() => 
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

        public static void ExportAdjustHandles(XmlWriter documentContentWriter, ModelAdjustHandlesList adjustHandles)
        {
            WriteStartElement(documentContentWriter, "ahLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                foreach (AdjustablePoint point in adjustHandles)
                {
                    XYAdjustHandle xyAdjustHandle = point as XYAdjustHandle;
                    if (xyAdjustHandle != null)
                    {
                        ExportXYAdjustHandle(documentContentWriter, xyAdjustHandle);
                        continue;
                    }
                    ExportPolarAdjustHandle(documentContentWriter, (PolarAdjustHandle) point);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportAdjustValues(XmlWriter documentContentWriter, ModelShapeGuideList adjustValues)
        {
            WriteStartElement(documentContentWriter, "avLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportGuideList(documentContentWriter, adjustValues);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportChildExtents(XmlWriter documentContentWriter, Transform2D childTransform)
        {
            WriteStartElement(documentContentWriter, "chExt", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportXfrmExtentsCore(documentContentWriter, childTransform);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportChildOffset(XmlWriter documentContentWriter, Transform2D childTransform)
        {
            WriteStartElement(documentContentWriter, "chOff", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportXfrmOffsetCore(documentContentWriter, childTransform);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportCommonDrawingLocks(XmlWriter writer, ICommonDrawingLocks commonDrawingLocks)
        {
            WriteBoolValue(writer, "noGrp", commonDrawingLocks.NoGroup, false);
            WriteBoolValue(writer, "noSelect", commonDrawingLocks.NoSelect, false);
            WriteBoolValue(writer, "noChangeAspect", commonDrawingLocks.NoChangeAspect, false);
            WriteBoolValue(writer, "noMove", commonDrawingLocks.NoMove, false);
        }

        public static void ExportConnectionEnd(XmlWriter writer, int endConnectionId, int endConnectionIdx)
        {
            WriteStartElement(writer, "endCxn", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportConnectionShapeConnectionCore(writer, endConnectionId, endConnectionIdx);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        private static void ExportConnectionShapeConnectionCore(XmlWriter writer, int id, int idx)
        {
            WriteIntValue(writer, "id", id);
            WriteIntValue(writer, "idx", idx);
        }

        public static void ExportConnectionShapeLocks(XmlWriter writer, ConnectionShapeLocks locks)
        {
            WriteStartElement(writer, "cxnSpLocks", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportDrawingLocks(writer, locks);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportConnectionStart(XmlWriter writer, int startConnectionId, int startConnectionIdx)
        {
            WriteStartElement(writer, "stCxn", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportConnectionShapeConnectionCore(writer, startConnectionId, startConnectionIdx);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportCustomGeometry(XmlWriter documentContentWriter, ModelShapeCustomGeometry customGeometry)
        {
            WriteStartElement(documentContentWriter, "custGeom", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (customGeometry.AdjustValues.Count > 0)
                {
                    ExportAdjustValues(documentContentWriter, customGeometry.AdjustValues);
                }
                if (customGeometry.Guides.Count > 0)
                {
                    ExportGuides(documentContentWriter, customGeometry.Guides);
                }
                if (customGeometry.AdjustHandles.Count > 0)
                {
                    ExportAdjustHandles(documentContentWriter, customGeometry.AdjustHandles);
                }
                if (customGeometry.ConnectionSites.Count > 0)
                {
                    ExportShapeConnectionSites(documentContentWriter, customGeometry.ConnectionSites);
                }
                if (!customGeometry.ShapeTextRectangle.IsEmpty())
                {
                    ExportShapeTextRectangle(documentContentWriter, customGeometry.ShapeTextRectangle);
                }
                ExportShapePaths(documentContentWriter, customGeometry.Paths);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportDrawingEffectStyle(XmlWriter documentContentWriter, DrawingEffectStyle style, IOpenXmlOfficeImageExporter imageExporter)
        {
            ContainerEffect containerEffect = style.ContainerEffect;
            if (containerEffect.ApplyEffectList)
            {
                GenerateContainerEffectContent(documentContentWriter, containerEffect, imageExporter);
            }
            GenerateScene3DContent(documentContentWriter, style.Scene3DProperties);
            GenerateShape3DContent(documentContentWriter, style.Shape3DProperties);
        }

        private static void ExportDrawingLocks(XmlWriter writer, IDrawingLocks drawingLocks)
        {
            ExportCommonDrawingLocks(writer, drawingLocks);
            WriteBoolValue(writer, "noRot", drawingLocks.NoRotate, false);
            WriteBoolValue(writer, "noResize", drawingLocks.NoResize, false);
            WriteBoolValue(writer, "noEditPoints", drawingLocks.NoEditPoints, false);
            WriteBoolValue(writer, "noAdjustHandles", drawingLocks.NoAdjustHandles, false);
            WriteBoolValue(writer, "noChangeShapeType", drawingLocks.NoChangeShapeType, false);
            WriteBoolValue(writer, "noChangeArrowheads", drawingLocks.NoChangeArrowheads, false);
        }

        private static void ExportEffectReference(XmlWriter writer, ShapeStyle shapeStyle)
        {
            WriteStartElement(writer, "effectRef", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportStyleMatrixReference(writer, shapeStyle.EffectReferenceIdx, shapeStyle.EffectColor);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        private static void ExportFillReference(XmlWriter writer, ShapeStyle shapeStyle)
        {
            WriteStartElement(writer, "fillRef", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportStyleMatrixReference(writer, shapeStyle.FillReferenceIdx, shapeStyle.FillColor);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        private static void ExportFontReference(XmlWriter writer, ShapeStyle shapeStyle)
        {
            WriteStartElement(writer, "fontRef", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteEnumValue<XlFontSchemeStyles>(writer, "idx", shapeStyle.FontReferenceIdx, OpenXmlExporterBase.FontCollectionIndexTable);
                if (shapeStyle.FontColor != null)
                {
                    new OpemXmlDrawingColorExporter(writer).GenerateDrawingColorContent(shapeStyle.FontColor);
                }
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportGroupShapeLocks(XmlWriter writer, GroupShapeLocks groupShapeLocks)
        {
            WriteStartElement(writer, "grpSpLocks", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportCommonDrawingLocks(writer, groupShapeLocks);
                WriteBoolValue(writer, "noResize", groupShapeLocks.NoResize, false);
                WriteBoolValue(writer, "noRot", groupShapeLocks.NoRotate, false);
                WriteBoolValue(writer, "noUngrp", groupShapeLocks.NoUngroup, false);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportGroupShapeProperties(XmlWriter documentContentWriter, GroupShapeProperties groupShapeProperties, string productNamespace, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, "grpSpPr", productNamespace);
            try
            {
                WriteEnumValue<OpenXmlBlackWhiteMode>(documentContentWriter, "bwMode", groupShapeProperties.BlackAndWhiteMode, ShapePropertiesDestination.BlackWhiteModeTable, OpenXmlBlackWhiteMode.Empty);
                if (!groupShapeProperties.Transform2D.IsEmpty || !groupShapeProperties.ChildTransform2D.IsEmpty)
                {
                    ExportGroupShapeXfrm(documentContentWriter, groupShapeProperties.Transform2D, groupShapeProperties.ChildTransform2D);
                }
                GenerateDrawingFillContent(documentContentWriter, groupShapeProperties.Fill, imageExporter);
                ContainerEffect containerEffect = groupShapeProperties.EffectStyle.ContainerEffect;
                if (containerEffect.ApplyEffectList)
                {
                    GenerateContainerEffectContent(documentContentWriter, containerEffect, imageExporter);
                }
                GenerateScene3DContent(documentContentWriter, groupShapeProperties.EffectStyle.Scene3DProperties);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportGroupShapeXfrm(XmlWriter documentContentWriter, Transform2D mainTransform, Transform2D childTransform)
        {
            WriteStartElement(documentContentWriter, "xfrm", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportXfrmCore(documentContentWriter, mainTransform);
                if (!childTransform.IsEmpty)
                {
                    ExportChildOffset(documentContentWriter, childTransform);
                    ExportChildExtents(documentContentWriter, childTransform);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportGuideList(XmlWriter documentContentWriter, ModelShapeGuideList guideList)
        {
            foreach (ModelShapeGuide guide in guideList)
            {
                ExportShapeGuide(documentContentWriter, guide);
            }
        }

        public static void ExportGuides(XmlWriter documentContentWriter, ModelShapeGuideList guides)
        {
            WriteStartElement(documentContentWriter, "gdLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportGuideList(documentContentWriter, guides);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportLineReference(XmlWriter writer, ShapeStyle shapeStyle)
        {
            WriteStartElement(writer, "lnRef", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportStyleMatrixReference(writer, shapeStyle.LineReferenceIdx, shapeStyle.LineColor);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportPath(XmlWriter documentContentWriter, ModelShapePath path)
        {
            WriteStartElement(documentContentWriter, "path", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteLongValue(documentContentWriter, "w", path.Width, 0);
                WriteLongValue(documentContentWriter, "h", path.Height, 0);
                WriteEnumValue<PathFillMode>(documentContentWriter, "fill", path.FillMode, PathFillModeTable, PathFillMode.Norm);
                WriteBoolValue(documentContentWriter, "stroke", path.Stroke, true);
                WriteBoolValue(documentContentWriter, "extrusionOk", path.ExtrusionOK, true);
                new PathInstructionExportWalker(documentContentWriter, path).Walk();
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportPictureLocks(XmlWriter writer, IPictureLocks pictureLocks)
        {
            WriteStartElement(writer, "picLocks", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportDrawingLocks(writer, pictureLocks);
                WriteBoolValue(writer, "noCrop", pictureLocks.NoCrop, false);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportPolarAdjustHandle(XmlWriter documentContentWriter, PolarAdjustHandle polarAdjustHandle)
        {
            WriteStartElement(documentContentWriter, "ahPolar", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (!string.IsNullOrEmpty(polarAdjustHandle.RadialGuide))
                {
                    WriteStringValue(documentContentWriter, "gdRefR", polarAdjustHandle.RadialGuide);
                }
                if (polarAdjustHandle.MinimumRadial != null)
                {
                    WriteStringValue(documentContentWriter, "minR", polarAdjustHandle.MinimumRadial.ToString());
                }
                if (polarAdjustHandle.MaximumRadial != null)
                {
                    WriteStringValue(documentContentWriter, "maxR", polarAdjustHandle.MaximumRadial.ToString());
                }
                if (!string.IsNullOrEmpty(polarAdjustHandle.AngleGuide))
                {
                    WriteStringValue(documentContentWriter, "gdRefAng", polarAdjustHandle.AngleGuide);
                }
                if (polarAdjustHandle.MinimumAngle != null)
                {
                    WriteStringValue(documentContentWriter, "minAng", polarAdjustHandle.MinimumAngle.ToString());
                }
                if (polarAdjustHandle.MaximumAngle != null)
                {
                    WriteStringValue(documentContentWriter, "maxAng", polarAdjustHandle.MaximumAngle.ToString());
                }
                ExportShapePositionCoordinate(documentContentWriter, polarAdjustHandle);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportPresetGeometry(XmlWriter documentContentWriter, ShapeProperties shapeProperties)
        {
            WriteStartElement(documentContentWriter, "prstGeom", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                string str;
                if (PresetGeometryDestination.shapeTypeTable.TryGetValue(shapeProperties.ShapeType, out str))
                {
                    WriteStringValue(documentContentWriter, "prst", str);
                }
                if (shapeProperties.PresetAdjustList.Count != 0)
                {
                    ExportAdjustValues(documentContentWriter, shapeProperties.PresetAdjustList);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapeConnectionSite(XmlWriter documentContentWriter, ModelShapeConnection connectionSite)
        {
            WriteStartElement(documentContentWriter, "cxn", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "ang", connectionSite.Angle.ToString());
                ExportShapePositionCoordinate(documentContentWriter, connectionSite);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapeConnectionSites(XmlWriter documentContentWriter, ModelShapeConnectionList connectionSites)
        {
            WriteStartElement(documentContentWriter, "cxnLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                foreach (ModelShapeConnection connection in connectionSites)
                {
                    ExportShapeConnectionSite(documentContentWriter, connection);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapeGuide(XmlWriter documentContentWriter, ModelShapeGuide modelShapeGuide)
        {
            WriteStartElement(documentContentWriter, "gd", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "name", modelShapeGuide.Name);
                WriteStringValue(documentContentWriter, "fmla", (modelShapeGuide.Formula != null) ? modelShapeGuide.Formula.ToString() : string.Empty);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapeLocks(XmlWriter writer, ShapeLocks shapeLocks)
        {
            WriteStartElement(writer, "spLocks", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportDrawingLocks(writer, shapeLocks);
                WriteBoolValue(writer, "noTextEdit", shapeLocks.NoTextEdit, false);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportShapePathArc(XmlWriter documentContentWriter, PathArc value)
        {
            WriteStartElement(documentContentWriter, "arcTo", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "wR", value.WidthRadius.ToString());
                WriteStringValue(documentContentWriter, "hR", value.HeightRadius.ToString());
                WriteStringValue(documentContentWriter, "stAng", value.StartAngle.ToString());
                WriteStringValue(documentContentWriter, "swAng", value.SwingAngle.ToString());
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapePathClose(XmlWriter documentContentWriter, PathClose value)
        {
            WriteStartElement(documentContentWriter, "close", "http://schemas.openxmlformats.org/drawingml/2006/main");
            WriteEndElement(documentContentWriter);
        }

        public static void ExportShapePathCubicBezier(XmlWriter documentContentWriter, PathCubicBezier value)
        {
            WriteStartElement(documentContentWriter, "cubicBezTo", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                foreach (AdjustablePoint point in value.Points)
                {
                    ExportShapePathPosition(documentContentWriter, point);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapePathLine(XmlWriter documentContentWriter, PathLine value)
        {
            WriteStartElement(documentContentWriter, "lnTo", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportShapePathPosition(documentContentWriter, value.Point);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapePathMove(XmlWriter documentContentWriter, PathMove value)
        {
            WriteStartElement(documentContentWriter, "moveTo", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportShapePathPosition(documentContentWriter, value.Point);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportShapePathPosition(XmlWriter documentContentWriter, AdjustablePoint adjustablePoint)
        {
            WriteStartElement(documentContentWriter, "pt", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportShapePositionCore(documentContentWriter, adjustablePoint);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapePathQuadraticBezier(XmlWriter documentContentWriter, PathQuadraticBezier value)
        {
            WriteStartElement(documentContentWriter, "quadBezTo", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                foreach (AdjustablePoint point in value.Points)
                {
                    ExportShapePathPosition(documentContentWriter, point);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapePaths(XmlWriter documentContentWriter, ModelShapePathsList paths)
        {
            WriteStartElement(documentContentWriter, "pathLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                foreach (ModelShapePath path in paths)
                {
                    ExportPath(documentContentWriter, path);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapePositionCoordinate(XmlWriter documentContentWriter, AdjustablePoint adjustablePoint)
        {
            WriteStartElement(documentContentWriter, "pos", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportShapePositionCore(documentContentWriter, adjustablePoint);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportShapePositionCore(XmlWriter documentContentWriter, AdjustablePoint adjustablePoint)
        {
            WriteStringValue(documentContentWriter, "x", adjustablePoint.X.ToString());
            WriteStringValue(documentContentWriter, "y", adjustablePoint.Y.ToString());
        }

        public static void ExportShapeProperties(XmlWriter documentContentWriter, ShapeProperties shapeProperties, string productNamespace, IOpenXmlOfficeImageExporter imageExporter)
        {
            Action exportXfrm = delegate {
                if (!shapeProperties.Transform2D.IsEmpty)
                {
                    ExportXfrm(documentContentWriter, shapeProperties.Transform2D);
                }
            };
            ExportShapeProperties(documentContentWriter, shapeProperties, productNamespace, imageExporter, exportXfrm);
        }

        public static void ExportShapeProperties(XmlWriter documentContentWriter, ShapeProperties shapeProperties, string productNamespace, IOpenXmlOfficeImageExporter imageExporter, Action exportXfrm)
        {
            WriteStartElement(documentContentWriter, "spPr", productNamespace);
            try
            {
                string str;
                if ((shapeProperties.BlackAndWhiteMode != OpenXmlBlackWhiteMode.Empty) && ShapePropertiesDestination.BlackWhiteModeTable.TryGetValue(shapeProperties.BlackAndWhiteMode, out str))
                {
                    WriteStringValue(documentContentWriter, "bwMode", str);
                }
                if (exportXfrm != null)
                {
                    exportXfrm();
                }
                if (shapeProperties.ShapeType != ShapePreset.None)
                {
                    ExportPresetGeometry(documentContentWriter, shapeProperties);
                }
                else
                {
                    ExportCustomGeometry(documentContentWriter, shapeProperties.CustomGeometry);
                }
                GenerateDrawingFillContent(documentContentWriter, shapeProperties.Fill, imageExporter);
                GenerateOutlineContent(documentContentWriter, shapeProperties.Outline, imageExporter);
                ExportDrawingEffectStyle(documentContentWriter, shapeProperties.EffectStyle, imageExporter);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportShapeStyle(XmlWriter writer, ShapeStyle shapeStyle, string nameSpace)
        {
            WriteStartElement(writer, "style", nameSpace);
            try
            {
                ExportLineReference(writer, shapeStyle);
                ExportFillReference(writer, shapeStyle);
                ExportEffectReference(writer, shapeStyle);
                ExportFontReference(writer, shapeStyle);
            }
            finally
            {
                WriteEndElement(writer);
            }
        }

        public static void ExportShapeTextRectangle(XmlWriter documentContentWriter, AdjustableRect shapeTextRectangle)
        {
            WriteStartElement(documentContentWriter, "rect", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "l", shapeTextRectangle.Left.ToString());
                WriteStringValue(documentContentWriter, "t", shapeTextRectangle.Top.ToString());
                WriteStringValue(documentContentWriter, "r", shapeTextRectangle.Right.ToString());
                WriteStringValue(documentContentWriter, "b", shapeTextRectangle.Bottom.ToString());
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void ExportStyleMatrixReference(XmlWriter writer, int idx, DrawingColor effectColor)
        {
            WriteIntValue(writer, "idx", idx);
            if (effectColor != null)
            {
                new OpemXmlDrawingColorExporter(writer).GenerateDrawingColorContent(effectColor);
            }
        }

        public static void ExportXfrm(XmlWriter documentContentWriter, Transform2D xfrm)
        {
            WriteStartElement(documentContentWriter, "xfrm", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportXfrmCore(documentContentWriter, xfrm);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportXfrmCore(XmlWriter documentContentWriter, Transform2D xfrm)
        {
            if (xfrm.FlipV)
            {
                WriteBoolValue(documentContentWriter, "flipV", xfrm.FlipV);
            }
            if (xfrm.FlipH)
            {
                WriteBoolValue(documentContentWriter, "flipH", xfrm.FlipH);
            }
            if (xfrm.Rotation != 0)
            {
                WriteIntValue(documentContentWriter, "rot", xfrm.DocumentModel.UnitConverter.ModelUnitsToAdjAngle(xfrm.Rotation));
            }
            ExportXfrmOffset(documentContentWriter, xfrm);
            ExportXfrmExtents(documentContentWriter, xfrm);
        }

        public static void ExportXfrmExtents(XmlWriter documentContentWriter, Transform2D xfrm)
        {
            WriteStartElement(documentContentWriter, "ext", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportXfrmExtentsCore(documentContentWriter, xfrm);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportXfrmExtentsCore(XmlWriter documentContentWriter, Transform2D xfrm)
        {
            long num = xfrm.DocumentModel.UnitConverter.CeilingModelUnitsToEmuL(xfrm.Cx);
            long num2 = xfrm.DocumentModel.UnitConverter.CeilingModelUnitsToEmuL(xfrm.Cy);
            WriteLongValue(documentContentWriter, "cx", Math.Min(Math.Max(0L, num), 0x18cdffffce64L));
            WriteLongValue(documentContentWriter, "cy", Math.Min(Math.Max(0L, num2), 0x18cdffffce64L));
        }

        public static void ExportXfrmOffset(XmlWriter documentContentWriter, Transform2D xfrm)
        {
            WriteStartElement(documentContentWriter, "off", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                ExportXfrmOffsetCore(documentContentWriter, xfrm);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void ExportXfrmOffsetCore(XmlWriter documentContentWriter, Transform2D xfrm)
        {
            long num = xfrm.DocumentModel.UnitConverter.CeilingModelUnitsToEmuL(xfrm.OffsetX);
            long num2 = xfrm.DocumentModel.UnitConverter.CeilingModelUnitsToEmuL(xfrm.OffsetY);
            WriteLongValue(documentContentWriter, "x", Math.Min(Math.Max(-27273042329600L, num), 0x18cdffffce64L));
            WriteLongValue(documentContentWriter, "y", Math.Min(Math.Max(-27273042329600L, num2), 0x18cdffffce64L));
        }

        public static void ExportXYAdjustHandle(XmlWriter documentContentWriter, XYAdjustHandle xyAdjustHandle)
        {
            WriteStartElement(documentContentWriter, "ahXY", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (!string.IsNullOrEmpty(xyAdjustHandle.HorizontalGuide))
                {
                    WriteStringValue(documentContentWriter, "gdRefX", xyAdjustHandle.HorizontalGuide);
                }
                if (xyAdjustHandle.MinX != null)
                {
                    WriteStringValue(documentContentWriter, "minX", xyAdjustHandle.MinX.ToString());
                }
                if (xyAdjustHandle.MaxX != null)
                {
                    WriteStringValue(documentContentWriter, "maxX", xyAdjustHandle.MaxX.ToString());
                }
                if (!string.IsNullOrEmpty(xyAdjustHandle.VerticalGuide))
                {
                    WriteStringValue(documentContentWriter, "gdRefY", xyAdjustHandle.VerticalGuide);
                }
                if (xyAdjustHandle.MinY != null)
                {
                    WriteStringValue(documentContentWriter, "minY", xyAdjustHandle.MinY.ToString());
                }
                if (xyAdjustHandle.MaxY != null)
                {
                    WriteStringValue(documentContentWriter, "maxY", xyAdjustHandle.MaxY.ToString());
                }
                ExportShapePositionCoordinate(documentContentWriter, xyAdjustHandle);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateAutoFitContent(IDrawingTextAutoFit autoFit, IDrawingTextAutoFitVisitor drawingTextAutoFitVisitor)
        {
            autoFit.Visit(drawingTextAutoFitVisitor);
        }

        private static void GenerateBackdropContent(XmlWriter documentContentWriter, BackdropPlane backdropPlane)
        {
            if (!backdropPlane.IsDefault)
            {
                WriteStartElement(documentContentWriter, "backdrop", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    string[] attrNames = new string[] { "x", "y", "z" };
                    GenerateVectorContent(documentContentWriter, "anchor", attrNames, backdropPlane.AnchorPoint);
                    string[] strArray = new string[] { "dx", "dy", "dz" };
                    GenerateVectorContent(documentContentWriter, "norm", strArray, backdropPlane.NormalVector);
                    GenerateVectorContent(documentContentWriter, "up", strArray, backdropPlane.UpVector);
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        public static void GenerateBodyPropertiesContent(XmlWriter documentContentWriter, DrawingTextBodyProperties properties, string ns, IDrawingTextAutoFitVisitor drawingTextAutoFitVisitor, IDrawingText3DVisitor drawingText3DVisitor)
        {
            DrawingTextInset inset = properties.Inset;
            WriteStartElement(documentContentWriter, "bodyPr", ns);
            try
            {
                ITextBodyOptions options = properties.Options;
                WriteIntValue(documentContentWriter, "rot", properties.Rotation, options.HasRotation);
                WriteOptionalBoolValue(documentContentWriter, "spcFirstLastPara", properties.ParagraphSpacing, options.HasParagraphSpacing);
                WriteEnumValue<DrawingTextVerticalOverflowType>(documentContentWriter, "vertOverflow", properties.VerticalOverflow, OpenXmlExporterBase.VerticalOverflowTypeTable, options.HasVerticalOverflow);
                WriteEnumValue<DrawingTextHorizontalOverflowType>(documentContentWriter, "horzOverflow", properties.HorizontalOverflow, OpenXmlExporterBase.HorizontalOverflowTypeTable, options.HasHorizontalOverflow);
                WriteEnumValue<DrawingTextVerticalTextType>(documentContentWriter, "vert", properties.VerticalText, OpenXmlExporterBase.VerticalTextTypeTable, options.HasVerticalText);
                WriteEnumValue<DrawingTextWrappingType>(documentContentWriter, "wrap", properties.TextWrapping, OpenXmlExporterBase.TextWrappingTypeTable, options.HasTextWrapping);
                DocumentModelUnitConverter unitConverter = properties.DocumentModel.UnitConverter;
                WriteFloatEmuValue(documentContentWriter, unitConverter, "lIns", inset.Left, inset.Options.HasLeft);
                WriteFloatEmuValue(documentContentWriter, unitConverter, "tIns", inset.Top, inset.Options.HasTop);
                WriteFloatEmuValue(documentContentWriter, unitConverter, "rIns", inset.Right, inset.Options.HasRight);
                WriteFloatEmuValue(documentContentWriter, unitConverter, "bIns", inset.Bottom, inset.Options.HasBottom);
                WriteIntValue(documentContentWriter, "numCol", properties.NumberOfColumns, options.HasNumberOfColumns);
                WriteFloatEmuValue(documentContentWriter, unitConverter, "spcCol", properties.SpaceBetweenColumns, options.HasSpaceBetweenColumns);
                WriteOptionalBoolValue(documentContentWriter, "rtlCol", properties.RightToLeftColumns, options.HasRightToLeftColumns);
                WriteOptionalBoolValue(documentContentWriter, "fromWordArt", properties.FromWordArt, options.HasFromWordArt);
                WriteEnumValue<DrawingTextAnchoringType>(documentContentWriter, "anchor", properties.Anchor, OpenXmlExporterBase.AnchoringTypeTable, options.HasAnchor);
                WriteOptionalBoolValue(documentContentWriter, "anchorCtr", properties.AnchorCenter, options.HasAnchorCenter);
                WriteOptionalBoolValue(documentContentWriter, "forceAA", properties.ForceAntiAlias, options.HasForceAntiAlias);
                WriteBoolValue(documentContentWriter, "upright", properties.UprightText, DrawingTextBodyInfo.DefaultInfo.UprightText);
                WriteOptionalBoolValue(documentContentWriter, "compatLnSpc", properties.CompatibleLineSpacing, options.HasCompatibleLineSpacing);
                GeneratePresetTextWarpContent(documentContentWriter, properties);
                GenerateAutoFitContent(properties.AutoFit, drawingTextAutoFitVisitor);
                GenerateScene3DContent(documentContentWriter, properties.Scene3D);
                GenerateText3DContent(properties.Text3D, drawingText3DVisitor);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateCameraContent(XmlWriter documentContentWriter, IScene3DCamera camera)
        {
            WriteStartElement(documentContentWriter, "camera", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteEnumValue<PresetCameraType>(documentContentWriter, "prst", camera.Preset, OpenXmlExporterBase.PresetCameraTypeTable);
                WriteIntValue(documentContentWriter, "fov", camera.Fov, Scene3DPropertiesInfo.DefaultInfo.Fov);
                WriteIntValue(documentContentWriter, "zoom", camera.Zoom, Scene3DPropertiesInfo.DefaultInfo.Zoom);
                if (camera.HasRotation)
                {
                    GenerateRotationContent(documentContentWriter, camera.Lat, camera.Lon, camera.Rev);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateContainerEffectContent(XmlWriter documentContentWriter, ContainerEffect effect, IOpenXmlOfficeImageExporter imageExporter)
        {
            string tagName = effect.HasEffectsList ? "effectLst" : "effectDag";
            GenerateContainerEffectContent(documentContentWriter, tagName, effect, imageExporter);
        }

        public static void GenerateContainerEffectContent(XmlWriter documentContentWriter, string tagName, ContainerEffect container, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (!container.HasEffectsList)
                {
                    WriteEnumValue<DrawingEffectContainerType>(documentContentWriter, "type", container.Type, OpenXmlExporterBase.DrawingEffectContainerTypeTable, DrawingEffectContainerType.Sibling);
                    WriteStringValue(documentContentWriter, "name", container.Name, !string.IsNullOrEmpty(container.Name));
                }
                GenerateDrawingEffectCollectionContent(documentContentWriter, container.Effects, imageExporter);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateDrawingBlipContent(XmlWriter documentContentWriter, DrawingBlip blip, IOpenXmlOfficeImageExporter imageExporter)
        {
            if (!IsDefaultDrawingBlip(blip))
            {
                GenerateDrawingBlipContentCore(documentContentWriter, blip, blip.Image, imageExporter);
            }
        }

        public static void GenerateDrawingBlipContentCore(XmlWriter documentContentWriter, DrawingBlip blip, OfficeImage currentImage, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, "blip", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                string str3;
                WriteStringAttr(documentContentWriter, "xmlns", "r", null, "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                if (currentImage != null)
                {
                    if (blip.Embedded)
                    {
                        string str = imageExporter.ExportImageData(blip.DocumentModel, currentImage);
                        WriteStringAttr(documentContentWriter, "r", "embed", "http://schemas.openxmlformats.org/officeDocument/2006/relationships", str);
                    }
                    else
                    {
                        string str2 = imageExporter.ExportExternalImageData(blip.Link, currentImage);
                        WriteStringAttr(documentContentWriter, "r", "link", "http://schemas.openxmlformats.org/officeDocument/2006/relationships", str2);
                    }
                }
                if ((blip.CompressionState != CompressionState.None) && OpenXmlExporterBase.CompressionStateTable.TryGetValue(blip.CompressionState, out str3))
                {
                    WriteStringValue(documentContentWriter, "cstate", str3);
                }
                GenerateDrawingEffectCollectionContent(documentContentWriter, blip.Effects, imageExporter);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateDrawingBlipFillContent(XmlWriter documentContentWriter, DrawingBlipFill fill, IOpenXmlOfficeImageExporter imageExporter)
        {
            GenerateDrawingBlipFillContent(documentContentWriter, fill, imageExporter, "http://schemas.openxmlformats.org/drawingml/2006/main");
        }

        public static void GenerateDrawingBlipFillContent(XmlWriter documentContentWriter, DrawingBlipFill fill, IOpenXmlOfficeImageExporter imageExporter, string ns)
        {
            if (imageExporter != null)
            {
                WriteStartElement(documentContentWriter, "blipFill", ns);
                try
                {
                    WriteIntValue(documentContentWriter, "dpi", fill.Dpi);
                    if (!fill.RotateWithShape)
                    {
                        WriteBoolValue(documentContentWriter, "rotWithShape", fill.RotateWithShape);
                    }
                    GenerateDrawingBlipContent(documentContentWriter, fill.Blip, imageExporter);
                    GenerateRelativeRectContent(documentContentWriter, "srcRect", fill.SourceRectangle);
                    if (fill.Stretch)
                    {
                        GenerateDrawingBlipFillStretch(documentContentWriter, fill);
                    }
                    else
                    {
                        GenerateDrawingBlipFillTile(documentContentWriter, fill);
                    }
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GenerateDrawingBlipFillStretch(XmlWriter documentContentWriter, DrawingBlipFill fill)
        {
            WriteStartElement(documentContentWriter, "stretch", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                GenerateRelativeRectContent(documentContentWriter, "fillRect", fill.FillRectangle);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateDrawingBlipFillTile(XmlWriter documentContentWriter, DrawingBlipFill fill)
        {
            WriteStartElement(documentContentWriter, "tile", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "algn", OpenXmlExporterBase.RectangleAlignTypeTable[fill.TileAlign]);
                WriteStringValue(documentContentWriter, "flip", OpenXmlExporterBase.TileFlipTypeTable[fill.TileFlip]);
                WriteIntValue(documentContentWriter, "sx", fill.ScaleX);
                WriteIntValue(documentContentWriter, "sy", fill.ScaleY);
                WriteLongValue(documentContentWriter, "tx", fill.DocumentModel.UnitConverter.ModelUnitsToEmuL(fill.OffsetX));
                WriteLongValue(documentContentWriter, "ty", fill.DocumentModel.UnitConverter.ModelUnitsToEmuL(fill.OffsetY));
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateDrawingColorTag(XmlWriter documentContentWriter, string tag, DrawingColor color)
        {
            WriteStartElement(documentContentWriter, tag, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                new OpemXmlDrawingColorExporter(documentContentWriter).GenerateDrawingColorContent(color);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateDrawingEffectCollectionContent(XmlWriter documentContentWriter, DrawingEffectCollection effects, IOpenXmlOfficeImageExporter imageExporter)
        {
            new DrawingEffectExportWalker(documentContentWriter, effects, imageExporter).Walk();
        }

        private static void GenerateDrawingEffectStyleContent(XmlWriter documentContentWriter, DrawingEffectStyle effectStyle, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, "effectStyle", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                GenerateContainerEffectContent(documentContentWriter, effectStyle.ContainerEffect, imageExporter);
                GenerateScene3DContent(documentContentWriter, effectStyle.Scene3DProperties);
                GenerateShape3DContent(documentContentWriter, effectStyle.Shape3DProperties);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateDrawingFillContent(XmlWriter documentContentWriter, IDrawingFill fill, IOpenXmlOfficeImageExporter imageExporter)
        {
            if (fill.FillType != DrawingFillType.Automatic)
            {
                DrawingFillExportWalker visitor = new DrawingFillExportWalker(documentContentWriter, imageExporter);
                fill.Visit(visitor);
            }
        }

        public static void GenerateDrawingFillTag(XmlWriter documentContentWriter, string tag)
        {
            WriteStartElement(documentContentWriter, tag, "http://schemas.openxmlformats.org/drawingml/2006/main");
            WriteEndElement(documentContentWriter);
        }

        public static void GenerateDrawingGradientFillContent(XmlWriter documentContentWriter, DrawingGradientFill fill)
        {
            WriteStartElement(documentContentWriter, "gradFill", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (fill.UseFlip)
                {
                    WriteEnumValue<TileFlipType>(documentContentWriter, "flip", fill.Flip, OpenXmlExporterBase.TileFlipTypeTable, TileFlipType.None);
                }
                if (fill.UseRotateWithShape)
                {
                    WriteBoolValue(documentContentWriter, "rotWithShape", fill.RotateWithShape);
                }
                GenerateDrawingGradientStopList(documentContentWriter, fill);
                if (fill.UseGradientType)
                {
                    if (fill.GradientType == GradientType.Linear)
                    {
                        GenerateDrawingGradientLinearContent(documentContentWriter, fill);
                    }
                    else
                    {
                        GenerateDrawingGradientPathContent(documentContentWriter, fill);
                    }
                }
                GenerateRelativeRectContent(documentContentWriter, "tileRect", fill.TileRect);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateDrawingGradientLinearContent(XmlWriter documentContentWriter, DrawingGradientFill fill)
        {
            WriteStartElement(documentContentWriter, "lin", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (fill.UseAngle)
                {
                    WriteIntValue(documentContentWriter, "ang", fill.DocumentModel.UnitConverter.ModelUnitsToAdjAngle(fill.Angle));
                }
                if (fill.UseScaled)
                {
                    WriteBoolValue(documentContentWriter, "scaled", fill.Scaled);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateDrawingGradientPathContent(XmlWriter documentContentWriter, DrawingGradientFill fill)
        {
            WriteStartElement(documentContentWriter, "path", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "path", OpenXmlExporterBase.GradientTypeTable[fill.GradientType]);
                GenerateRelativeRectContent(documentContentWriter, "fillToRect", fill.FillRect);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateDrawingGradientStop(XmlWriter documentContentWriter, DrawingGradientStop gradientStop)
        {
            WriteStartElement(documentContentWriter, "gs", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteIntValue(documentContentWriter, "pos", gradientStop.Position);
                new OpemXmlDrawingColorExporter(documentContentWriter).GenerateDrawingColorContent(gradientStop.Color);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateDrawingGradientStopList(XmlWriter documentContentWriter, DrawingGradientFill fill)
        {
            WriteStartElement(documentContentWriter, "gsLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                foreach (DrawingGradientStop stop in fill.GradientStops)
                {
                    GenerateDrawingGradientStop(documentContentWriter, stop);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateDrawingListContent<T>(XmlWriter documentContentWriter, string tagName, List<T> list, Action<XmlWriter, T, IOpenXmlOfficeImageExporter> action, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    action(documentContentWriter, list[i], imageExporter);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateDrawingPatternFillContent(XmlWriter documentContentWriter, DrawingPatternFill fill)
        {
            WriteStartElement(documentContentWriter, "pattFill", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "prst", OpenXmlExporterBase.DrawingPatternTypeTable[fill.PatternType]);
                GenerateDrawingColorTag(documentContentWriter, "fgClr", fill.ForegroundColor);
                GenerateDrawingColorTag(documentContentWriter, "bgClr", fill.BackgroundColor);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateHeadEndStyle(XmlWriter documentContentWriter, Outline outline)
        {
            if (!outline.Info.IsDefaultHeadEndStyle)
            {
                GenerateHeadEndStyleCore(documentContentWriter, outline);
            }
        }

        private static void GenerateHeadEndStyleCore(XmlWriter documentContentWriter, Outline outline)
        {
            WriteStartElement(documentContentWriter, "headEnd", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (outline.HasHeadType)
                {
                    WriteEnumValue<OutlineHeadTailType>(documentContentWriter, "type", outline.HeadType, OpenXmlExporterBase.HeadTailTypeTable, OutlineInfo.DefaultHeadTailType);
                }
                if (outline.HasHeadWidth)
                {
                    WriteEnumValue<OutlineHeadTailSize>(documentContentWriter, "w", outline.HeadWidth, OpenXmlExporterBase.HeadTailSizeTable, OutlineInfo.DefaultHeadTailSize);
                }
                if (outline.HasHeadLength)
                {
                    WriteEnumValue<OutlineHeadTailSize>(documentContentWriter, "len", outline.HeadLength, OpenXmlExporterBase.HeadTailSizeTable, OutlineInfo.DefaultHeadTailSize);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateLightRigContent(XmlWriter documentContentWriter, IScene3DLightRig lightRig)
        {
            WriteStartElement(documentContentWriter, "lightRig", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteEnumValue<LightRigPreset>(documentContentWriter, "rig", lightRig.Preset, OpenXmlExporterBase.LightRigPresetTable);
                WriteEnumValue<LightRigDirection>(documentContentWriter, "dir", lightRig.Direction, OpenXmlExporterBase.LightRigDirectionTable);
                if (lightRig.HasRotation)
                {
                    GenerateRotationContent(documentContentWriter, lightRig.Lat, lightRig.Lon, lightRig.Rev);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateLineJoinStyle(XmlWriter documentContentWriter, Outline outline)
        {
            if (outline.HasLineJoinStyle)
            {
                GenerateOutlineLineJoinTag(documentContentWriter, "bevel", outline, LineJoinStyle.Bevel);
                GenerateOutlineLineJoinTag(documentContentWriter, "round", outline, LineJoinStyle.Round);
                GenerateOutlineLineJoinTag(documentContentWriter, "miter", outline, LineJoinStyle.Miter);
            }
        }

        public static void GenerateOutlineContent(XmlWriter documentContentWriter, Outline outline, IOpenXmlOfficeImageExporter imageExporter)
        {
            GenerateOutlineContent(documentContentWriter, outline, "ln", imageExporter);
        }

        public static void GenerateOutlineContent(XmlWriter documentContentWriter, Outline outline, string tagName, IOpenXmlOfficeImageExporter imageExporter)
        {
            if (!outline.IsDefault)
            {
                WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    GenerateOutlineContentCore(documentContentWriter, outline, imageExporter);
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GenerateOutlineContentCore(XmlWriter documentContentWriter, Outline outline, IOpenXmlOfficeImageExporter imageExporter)
        {
            OutlineInfo defaultInfo = OutlineInfo.DefaultInfo;
            WriteIntValue(documentContentWriter, "w", outline.DocumentModel.UnitConverter.ModelUnitsToEmu(outline.Width), outline.HasWidth);
            if (outline.HasEndCapStyle)
            {
                WriteEnumValue<OutlineEndCapStyle>(documentContentWriter, "cap", outline.EndCapStyle, OpenXmlExporterBase.EndCapStyleTable, defaultInfo.EndCapStyle);
            }
            WriteStringValue(documentContentWriter, "cmpd", OpenXmlExporterBase.CompoundTypeTable[outline.CompoundType], outline.HasCompoundType);
            if (outline.HasStrokeAlignment)
            {
                WriteEnumValue<OutlineStrokeAlignment>(documentContentWriter, "algn", outline.StrokeAlignment, OpenXmlExporterBase.StrokeAlignmentTable, defaultInfo.StrokeAlignment);
            }
            GenerateDrawingFillContent(documentContentWriter, outline.Fill, imageExporter);
            GeneratePresetDash(documentContentWriter, outline);
            GenerateLineJoinStyle(documentContentWriter, outline);
            GenerateHeadEndStyle(documentContentWriter, outline);
            GenerateTailEndStyle(documentContentWriter, outline);
        }

        private static void GenerateOutlineLineJoinTag(XmlWriter documentContentWriter, string tagName, Outline outline, LineJoinStyle defaultLineJoin)
        {
            if (outline.JoinStyle == defaultLineJoin)
            {
                WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    if (tagName == "miter")
                    {
                        WriteIntValue(documentContentWriter, "lim", outline.MiterLimit, outline.HasMiterLimit);
                    }
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GeneratePresetDash(XmlWriter documentContentWriter, Outline outline)
        {
            if (outline.HasDashing)
            {
                WriteStartElement(documentContentWriter, "prstDash", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    WriteEnumValue<OutlineDashing>(documentContentWriter, "val", outline.Dashing, OpenXmlExporterBase.PresetDashTable);
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GeneratePresetTextWarpContent(XmlWriter documentContentWriter, DrawingTextBodyProperties properties)
        {
            if (properties.PresetTextWarp != DrawingPresetTextWarp.NoShape)
            {
                WriteStartElement(documentContentWriter, "prstTxWarp", "http://schemas.openxmlformats.org/drawingml/2006/main");
                WriteEnumValue<DrawingPresetTextWarp>(documentContentWriter, "prst", properties.PresetTextWarp, OpenXmlExporterBase.PresetTextWarpTable, DrawingPresetTextWarp.NoShape);
                ExportAdjustValues(documentContentWriter, properties.PresetAdjustValues);
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateRelativeRectContent(XmlWriter documentContentWriter, string tag, RectangleOffset rect)
        {
            WriteStartElement(documentContentWriter, tag, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteIntValue(documentContentWriter, "l", rect.LeftOffset, 0);
                WriteIntValue(documentContentWriter, "t", rect.TopOffset, 0);
                WriteIntValue(documentContentWriter, "r", rect.RightOffset, 0);
                WriteIntValue(documentContentWriter, "b", rect.BottomOffset, 0);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateRotationContent(XmlWriter documentContentWriter, int lat, int lon, int rev)
        {
            WriteStartElement(documentContentWriter, "rot", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteIntValue(documentContentWriter, "lat", lat);
                WriteIntValue(documentContentWriter, "lon", lon);
                WriteIntValue(documentContentWriter, "rev", rev);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateScene3DContent(XmlWriter documentContentWriter, Scene3DProperties scene3d)
        {
            if (!scene3d.IsDefault)
            {
                WriteStartElement(documentContentWriter, "scene3d", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    GenerateCameraContent(documentContentWriter, scene3d.Camera);
                    GenerateLightRigContent(documentContentWriter, scene3d.LightRig);
                    GenerateBackdropContent(documentContentWriter, scene3d.BackdropPlane);
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        public static void GenerateShape3DContent(XmlWriter documentContentWriter, Shape3DProperties shape3d)
        {
            if (!shape3d.IsDefault)
            {
                WriteStartElement(documentContentWriter, "sp3d", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    DocumentModelUnitConverter unitConverter = shape3d.DocumentModel.UnitConverter;
                    if (shape3d.ShapeDepth != 0)
                    {
                        WriteDrawingCoordinate(documentContentWriter, "z", unitConverter.ModelUnitsToEmuL(shape3d.ShapeDepth));
                    }
                    if (shape3d.ExtrusionHeight != 0)
                    {
                        WriteDrawingCoordinate(documentContentWriter, "extrusionH", unitConverter.ModelUnitsToEmuL(shape3d.ExtrusionHeight));
                    }
                    if (shape3d.ContourWidth != 0)
                    {
                        WriteDrawingCoordinate(documentContentWriter, "contourW", unitConverter.ModelUnitsToEmuL(shape3d.ContourWidth));
                    }
                    WriteEnumValue<PresetMaterialType>(documentContentWriter, "prstMaterial", shape3d.PresetMaterial, OpenXmlExporterBase.PresetMaterialTypeTable, PresetMaterialType.WarmMatte);
                    GenerateShapeBevel3DPropertiesContent(documentContentWriter, shape3d.TopBevel, "bevelT");
                    GenerateShapeBevel3DPropertiesContent(documentContentWriter, shape3d.BottomBevel, "bevelB");
                    GenerateShapeColor3DPropertiesContent(documentContentWriter, shape3d.ExtrusionColor, "extrusionClr");
                    GenerateShapeColor3DPropertiesContent(documentContentWriter, shape3d.ContourColor, "contourClr");
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GenerateShapeBevel3DPropertiesContent(XmlWriter documentContentWriter, ShapeBevel3DProperties bevel, string tagName)
        {
            if (!bevel.IsDefault)
            {
                WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    DocumentModelUnitConverter unitConverter = bevel.DocumentModel.UnitConverter;
                    if (bevel.Width != 0x129a8L)
                    {
                        WriteDrawingCoordinate(documentContentWriter, "w", unitConverter.ModelUnitsToEmuL(bevel.Width));
                    }
                    if (bevel.Height != 0x129a8L)
                    {
                        WriteDrawingCoordinate(documentContentWriter, "h", unitConverter.ModelUnitsToEmuL(bevel.Height));
                    }
                    WriteEnumValue<PresetBevelType>(documentContentWriter, "prst", bevel.PresetType, OpenXmlExporterBase.PresetBevelTypeTable, PresetBevelType.Circle);
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GenerateShapeColor3DPropertiesContent(XmlWriter documentContentWriter, DrawingColor color, string tagName)
        {
            if (!color.IsEmpty)
            {
                WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    new OpemXmlDrawingColorExporter(documentContentWriter).GenerateDrawingColorContent(color);
                }
                finally
                {
                    WriteEndElement(documentContentWriter);
                }
            }
        }

        private static void GenerateTailEndStyle(XmlWriter documentContentWriter, Outline outline)
        {
            if (!outline.Info.IsDefaultTailEndStyle)
            {
                GenerateTailEndStyleCore(documentContentWriter, outline);
            }
        }

        private static void GenerateTailEndStyleCore(XmlWriter documentContentWriter, Outline outline)
        {
            WriteStartElement(documentContentWriter, "tailEnd", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (outline.HasTailType)
                {
                    WriteEnumValue<OutlineHeadTailType>(documentContentWriter, "type", outline.TailType, OpenXmlExporterBase.HeadTailTypeTable, OutlineInfo.DefaultHeadTailType);
                }
                if (outline.HasTailWidth)
                {
                    WriteEnumValue<OutlineHeadTailSize>(documentContentWriter, "w", outline.TailWidth, OpenXmlExporterBase.HeadTailSizeTable, OutlineInfo.DefaultHeadTailSize);
                }
                if (outline.HasTailLength)
                {
                    WriteEnumValue<OutlineHeadTailSize>(documentContentWriter, "len", outline.TailLength, OpenXmlExporterBase.HeadTailSizeTable, OutlineInfo.DefaultHeadTailSize);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateText3DContent(IDrawingText3D text3d, IDrawingText3DVisitor drawingText3DVisitor)
        {
            text3d.Visit(drawingText3DVisitor);
        }

        public static void GenerateThemeFormatSchemesContent(XmlWriter documentContentWriter, ThemeFormatScheme scheme, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, "fmtScheme", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                WriteStringValue(documentContentWriter, "name", scheme.Name, !string.IsNullOrEmpty(scheme.Name));
                GenerateDrawingListContent<IDrawingFill>(documentContentWriter, "fillStyleLst", scheme.FillStyleList, new Action<XmlWriter, IDrawingFill, IOpenXmlOfficeImageExporter>(OpenXmlDrawingExportHelper.GenerateDrawingFillContent), imageExporter);
                GenerateDrawingListContent<Outline>(documentContentWriter, "lnStyleLst", scheme.LineStyleList, new Action<XmlWriter, Outline, IOpenXmlOfficeImageExporter>(OpenXmlDrawingExportHelper.GenerateThemeOutlineContent), imageExporter);
                GenerateDrawingListContent<DrawingEffectStyle>(documentContentWriter, "effectStyleLst", scheme.EffectStyleList, new Action<XmlWriter, DrawingEffectStyle, IOpenXmlOfficeImageExporter>(OpenXmlDrawingExportHelper.GenerateDrawingEffectStyleContent), imageExporter);
                GenerateDrawingListContent<IDrawingFill>(documentContentWriter, "bgFillStyleLst", scheme.BackgroundFillStyleList, new Action<XmlWriter, IDrawingFill, IOpenXmlOfficeImageExporter>(OpenXmlDrawingExportHelper.GenerateDrawingFillContent), imageExporter);
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static void GenerateThemeOutlineContent(XmlWriter documentContentWriter, Outline outline, IOpenXmlOfficeImageExporter imageExporter)
        {
            WriteStartElement(documentContentWriter, "ln", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                if (!outline.IsDefault)
                {
                    GenerateOutlineContentCore(documentContentWriter, outline, imageExporter);
                }
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        private static void GenerateVectorContent(XmlWriter documentContentWriter, string tagName, string[] attrNames, Scene3DVector vector)
        {
            WriteStartElement(documentContentWriter, tagName, "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                DocumentModelUnitConverter unitConverter = vector.DocumentModel.UnitConverter;
                WriteDrawingCoordinate(documentContentWriter, attrNames[0], unitConverter.ModelUnitsToEmuL(vector.X));
                WriteDrawingCoordinate(documentContentWriter, attrNames[1], unitConverter.ModelUnitsToEmuL(vector.Y));
                WriteDrawingCoordinate(documentContentWriter, attrNames[2], unitConverter.ModelUnitsToEmuL(vector.Z));
            }
            finally
            {
                WriteEndElement(documentContentWriter);
            }
        }

        public static bool IsDefaultDrawingBlip(DrawingBlip blip) => 
            blip.IsEmpty && ReferenceEquals(blip.Image, null);

        internal static void WriteBoolValue(XmlWriter documentContentWriter, string tag, bool value)
        {
            WriteStringAttr(documentContentWriter, null, tag, null, ConvertBoolToString(value));
        }

        private static void WriteBoolValue(XmlWriter documentContentWriter, string attr, bool value, bool defaultValue)
        {
            if (value != defaultValue)
            {
                WriteBoolValue(documentContentWriter, attr, value);
            }
        }

        private static void WriteDrawingCoordinate(XmlWriter documentContentWriter, string attr, long coordinate)
        {
            WriteLongValue(documentContentWriter, attr, coordinate);
        }

        internal static void WriteEndElement(XmlWriter documentContentWriter)
        {
            documentContentWriter.WriteEndElement();
        }

        internal static void WriteEnumValue<T>(XmlWriter documentContentWriter, string attr, T value, Dictionary<T, string> table)
        {
            WriteStringValue(documentContentWriter, attr, table[value]);
        }

        internal static void WriteEnumValue<T>(XmlWriter documentContentWriter, string attr, T value, Dictionary<T, string> table, T defaultValue)
        {
            if (!value.Equals(defaultValue))
            {
                WriteEnumValue<T>(documentContentWriter, attr, value, table);
            }
        }

        private static void WriteEnumValue<T>(XmlWriter documentContentWriter, string attr, T value, Dictionary<T, string> table, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteEnumValue<T>(documentContentWriter, attr, value, table);
            }
        }

        private static void WriteFloatEmuValue(XmlWriter documentContentWriter, DocumentModelUnitConverter unitConverter, string tag, float value)
        {
            WriteStringAttr(documentContentWriter, null, tag, null, unitConverter.ModelUnitsToEmuF(value).ToString(CultureInfo.InvariantCulture));
        }

        private static void WriteFloatEmuValue(XmlWriter documentContentWriter, DocumentModelUnitConverter unitConverter, string attr, float value, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteFloatEmuValue(documentContentWriter, unitConverter, attr, value);
            }
        }

        private static void WriteIntEmuValue(XmlWriter documentContentWriter, DocumentModelUnitConverter unitConverter, string tag, int value)
        {
            WriteStringAttr(documentContentWriter, null, tag, null, unitConverter.ModelUnitsToEmu(value).ToString(CultureInfo.InvariantCulture));
        }

        private static void WriteIntEmuValue(XmlWriter documentContentWriter, DocumentModelUnitConverter unitConverter, string attr, int value, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteIntEmuValue(documentContentWriter, unitConverter, attr, value);
            }
        }

        private static void WriteIntEmuValue(XmlWriter documentContentWriter, DocumentModelUnitConverter unitConverter, string attr, int value, int defaultValue)
        {
            WriteIntEmuValue(documentContentWriter, unitConverter, attr, value, value != defaultValue);
        }

        internal static void WriteIntValue(XmlWriter documentContentWriter, string tag, int value)
        {
            WriteStringAttr(documentContentWriter, null, tag, null, value.ToString(CultureInfo.InvariantCulture));
        }

        internal static void WriteIntValue(XmlWriter documentContentWriter, string attr, int value, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteIntValue(documentContentWriter, attr, value);
            }
        }

        public static void WriteIntValue(XmlWriter documentContentWriter, string attr, int value, int defaultValue)
        {
            WriteIntValue(documentContentWriter, attr, value, value != defaultValue);
        }

        internal static void WriteLongValue(XmlWriter documentContentWriter, string tag, long value)
        {
            WriteStringAttr(documentContentWriter, null, tag, null, value.ToString(CultureInfo.InvariantCulture));
        }

        private static void WriteLongValue(XmlWriter documentContentWriter, string tag, long value, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteLongValue(documentContentWriter, tag, value);
            }
        }

        internal static void WriteLongValue(XmlWriter documentContentWriter, string tag, long value, int defaultValue)
        {
            WriteLongValue(documentContentWriter, tag, value, value != defaultValue);
        }

        private static void WriteOptionalBoolValue(XmlWriter documentContentWriter, string attr, bool value, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteBoolValue(documentContentWriter, attr, value);
            }
        }

        internal static void WriteStartElement(XmlWriter documentContentWriter, string tag, string ns)
        {
            documentContentWriter.WriteStartElement(tag, ns);
        }

        private static void WriteStringAttr(XmlWriter documentContentWriter, string prefix, string attr, string ns, string value)
        {
            documentContentWriter.WriteAttributeString(prefix, attr, ns, value);
        }

        internal static void WriteStringValue(XmlWriter documentContentWriter, string tag, string value)
        {
            WriteStringAttr(documentContentWriter, null, tag, null, value);
        }

        private static void WriteStringValue(XmlWriter documentContentWriter, string attr, string value, bool shouldExport)
        {
            if (shouldExport)
            {
                WriteStringValue(documentContentWriter, attr, value);
            }
        }

        public static Dictionary<PathFillMode, string> PathFillModeTable
        {
            get
            {
                if (pathFillModeTable == null)
                {
                    object syncPathFillModeTable = OpenXmlDrawingExportHelper.syncPathFillModeTable;
                    lock (syncPathFillModeTable)
                    {
                        pathFillModeTable ??= CreatePathFillModeTable();
                    }
                }
                return pathFillModeTable;
            }
        }
    }
}

