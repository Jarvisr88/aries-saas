namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class GeometryDrawingBuilder : DrawingBuilder<GeometryDrawing>
    {
        public GeometryDrawingBuilder(GeometryDrawing drawing) : base(drawing)
        {
        }

        public static void DrawGeometry(PdfGraphicsCommandConstructor constructor, Geometry geometry, bool fill)
        {
            PdfGeometry geometry2 = new PdfGeometry(geometry, fill, !fill, false);
            if (fill)
            {
                constructor.FillPath(geometry2.Points, geometry2.PointTypes, geometry2.NonZero);
            }
            else
            {
                constructor.DrawPath(geometry2.Points, geometry2.PointTypes);
            }
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            Geometry geometry = base.Drawing.Geometry;
            if ((geometry != null) && (base.Drawing.Bounds != Rect.Empty))
            {
                constructor.DoWithState(delegate {
                    Rect bounds = this.Drawing.Bounds;
                    if (PdfExportHelper.CanSetBrush(this.Drawing.Brush))
                    {
                        constructor.SaveGraphicsState();
                        PdfExportHelper.SetBrush(this.Drawing.Brush, bounds, constructor);
                        DrawGeometry(constructor, geometry, true);
                        constructor.RestoreGraphicsState();
                    }
                    DXBrush brush = ((this.Drawing.Pen == null) || (this.Drawing.Pen.Thickness <= 0.0)) ? null : PdfExportHelper.ConvertToDxBrush(this.Drawing.Pen.Brush, bounds);
                    if (brush != null)
                    {
                        constructor.SaveGraphicsState();
                        constructor.SetPen(this.Drawing.Pen.ToPen(brush));
                        DrawGeometry(constructor, geometry, false);
                        constructor.RestoreGraphicsState();
                    }
                }, geometry.Transform, null);
            }
        }
    }
}

