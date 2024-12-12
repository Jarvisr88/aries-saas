namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PathInstructionExportWalker : IPathInstructionWalker
    {
        private readonly ModelShapePath path;
        private readonly XmlWriter documentContentWriter;

        public PathInstructionExportWalker(XmlWriter documentContentWriter, ModelShapePath path)
        {
            this.documentContentWriter = documentContentWriter;
            this.path = path;
        }

        public void Visit(PathArc pathArc)
        {
            OpenXmlDrawingExportHelper.ExportShapePathArc(this.documentContentWriter, pathArc);
        }

        public void Visit(PathClose value)
        {
            OpenXmlDrawingExportHelper.ExportShapePathClose(this.documentContentWriter, value);
        }

        public void Visit(PathCubicBezier value)
        {
            OpenXmlDrawingExportHelper.ExportShapePathCubicBezier(this.documentContentWriter, value);
        }

        public void Visit(PathLine pathLine)
        {
            OpenXmlDrawingExportHelper.ExportShapePathLine(this.documentContentWriter, pathLine);
        }

        public void Visit(PathMove pathMove)
        {
            OpenXmlDrawingExportHelper.ExportShapePathMove(this.documentContentWriter, pathMove);
        }

        public void Visit(PathQuadraticBezier value)
        {
            OpenXmlDrawingExportHelper.ExportShapePathQuadraticBezier(this.documentContentWriter, value);
        }

        public void Walk()
        {
            foreach (IPathInstruction instruction in this.path.Instructions)
            {
                instruction.Visit(this);
            }
        }
    }
}

