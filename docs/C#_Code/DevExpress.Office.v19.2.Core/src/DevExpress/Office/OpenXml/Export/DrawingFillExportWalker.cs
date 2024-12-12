namespace DevExpress.Office.OpenXml.Export
{
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingFillExportWalker : IDrawingFillVisitor
    {
        private readonly XmlWriter documentContentWriter;
        private readonly IOpenXmlOfficeImageExporter imageExporter;

        public DrawingFillExportWalker(XmlWriter documentContentWriter, IOpenXmlOfficeImageExporter imageExporter)
        {
            this.documentContentWriter = documentContentWriter;
            this.imageExporter = imageExporter;
        }

        public void Visit(DrawingBlipFill fill)
        {
            OpenXmlDrawingExportHelper.GenerateDrawingBlipFillContent(this.documentContentWriter, fill, this.imageExporter);
        }

        public void Visit(DrawingFill fill)
        {
            if (fill.FillType == DrawingFillType.None)
            {
                OpenXmlDrawingExportHelper.GenerateDrawingFillTag(this.documentContentWriter, "noFill");
            }
            else if (fill.FillType == DrawingFillType.Group)
            {
                OpenXmlDrawingExportHelper.GenerateDrawingFillTag(this.documentContentWriter, "grpFill");
            }
        }

        public void Visit(DrawingGradientFill fill)
        {
            OpenXmlDrawingExportHelper.GenerateDrawingGradientFillContent(this.documentContentWriter, fill);
        }

        public void Visit(DrawingPatternFill fill)
        {
            OpenXmlDrawingExportHelper.GenerateDrawingPatternFillContent(this.documentContentWriter, fill);
        }

        public void Visit(DrawingSolidFill fill)
        {
            OpenXmlDrawingExportHelper.GenerateDrawingColorTag(this.documentContentWriter, "solidFill", fill.Color);
        }
    }
}

