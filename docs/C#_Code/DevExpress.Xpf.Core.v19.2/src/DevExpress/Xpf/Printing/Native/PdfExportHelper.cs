namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class PdfExportHelper
    {
        public static bool CanSetBrush(Brush brush) => 
            (BrushBuilder.Create(brush) != null) || (TileBrushBuilder.Create(brush) != null);

        public static DXBrush ConvertToDxBrush(Brush brush, Rect bounds) => 
            BrushBuilder.Create(brush)?.CreateDxBrush(bounds);

        public static Rect GetVisualBounds(Visual visual)
        {
            Rect contentBounds = VisualTreeHelper.GetContentBounds(visual);
            return (contentBounds.IsEmpty ? VisualTreeHelper.GetDescendantBounds(visual) : contentBounds);
        }

        public static void SetBrush(Brush brush, Rect bounds, PdfGraphicsCommandConstructor constructor)
        {
            BrushBuilder builder = BrushBuilder.Create(brush);
            TileBrushBuilder builder2 = TileBrushBuilder.Create(brush);
            if (builder != null)
            {
                constructor.SetBrush(builder.CreateDxBrush(bounds));
            }
            if (builder2 != null)
            {
                builder2.SetDXBrush(bounds, constructor);
            }
        }
    }
}

