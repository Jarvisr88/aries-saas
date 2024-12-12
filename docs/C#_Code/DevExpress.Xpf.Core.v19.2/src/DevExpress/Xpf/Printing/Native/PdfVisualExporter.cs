namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Native;
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public static class PdfVisualExporter
    {
        private const float dpi = 96f;

        internal static void DrawVisual(PdfGraphicsCommandConstructor constructor, Visual visual, bool applyOffset)
        {
            Matrix transform = visual.GetTransform();
            if (applyOffset)
            {
                Vector offset = VisualTreeHelper.GetOffset(visual);
                transform *= new Matrix(1.0, 0.0, 0.0, 1.0, offset.X, offset.Y);
            }
            constructor.DoWithState(delegate {
                if (VisualTreeHelper.GetOpacity(visual) != 0.0)
                {
                    DrawingBuilder builder = DrawingBuilder.Create(VisualTreeHelper.GetDrawing(visual));
                    if (builder != null)
                    {
                        builder.GenerateData(constructor);
                    }
                    int childrenCount = VisualTreeHelper.GetChildrenCount(visual);
                    for (int i = 0; i < childrenCount; i++)
                    {
                        Visual child = VisualTreeHelper.GetChild(visual, i) as Visual;
                        if (child != null)
                        {
                            DrawVisual(constructor, child, true);
                        }
                    }
                }
            }, new MatrixTransform(transform), VisualTreeHelper.GetClip(visual));
        }

        public static void ExportToStream(Visual visual, Stream stream)
        {
            PdfDocument document = new PdfDocument();
            Rect visualBounds = PdfExportHelper.GetVisualBounds(visual);
            PdfPage page = document.AddPage(new PdfRectangle(0.0, 0.0, visualBounds.Width.ScaleToDpi(96.0), visualBounds.Height.ScaleToDpi(96.0)));
            using (PdfGraphicsDocument document2 = new PdfGraphicsDocument(document.DocumentCatalog, 0))
            {
                PdfGraphicsCommandConstructor constructor = new PdfGraphicsCommandConstructor(page, document2, 96f, 96f);
                DrawVisual(constructor, visual, false);
                page.ReplaceCommands(constructor.Commands);
            }
            PdfSaveOptions options = new PdfSaveOptions();
            PdfDocumentWriter.Write(stream, document, options, null);
        }

        public static void ExportToStream(IEnumerable<Visual> visuals, Stream stream, PdfExportOptions options, Size? pageSize = new Size?())
        {
            PdfExportDocument exportDocument = new PdfExportDocument(stream, options, false);
            int[] allowedIndices = PageRangeParser.GetIndices(options.PageRange, visuals.Count<Visual>());
            visuals.ForEach<Visual>(delegate (Visual visual) {
                int index = visuals.IndexOf<Visual>(x => ReferenceEquals(x, visual));
                if (allowedIndices.Contains<int>(index))
                {
                    Size? nullable = pageSize;
                    Size size = (nullable != null) ? nullable.GetValueOrDefault() : PdfExportHelper.GetVisualBounds(visual).Size;
                    PdfGraphicsCommandConstructor constructor = exportDocument.CreateCommandConstructor(size.ToSizeF(), index, 96f);
                    DrawVisual(constructor, visual, false);
                    exportDocument.ApplyConstructor(constructor);
                }
            });
            exportDocument.FinalizeDocument();
        }
    }
}

