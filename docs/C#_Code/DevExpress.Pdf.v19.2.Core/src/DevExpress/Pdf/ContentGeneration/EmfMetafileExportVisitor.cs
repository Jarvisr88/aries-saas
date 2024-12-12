namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing.Drawing2D;

    public class EmfMetafileExportVisitor : PdfDisposableObject, IEmfMetafileVisitor
    {
        private readonly EmfMetafileGraphics context;
        private readonly PdfRectangle destinationRectangle;

        public EmfMetafileExportVisitor(PdfGraphicsCommandConstructor constructor, PdfExportFontManager fontCache, PdfRectangle destinationRectangle)
        {
            this.context = new EmfMetafileGraphics(constructor, fontCache);
            this.destinationRectangle = destinationRectangle;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }
        }

        private void PerformFillAction(EmfPlusFillBase record, Action action)
        {
            this.PerformIsolatedAction(delegate {
                ARGBColor? color = record.Color;
                if (color != null)
                {
                    this.context.SetFillColor(color.Value);
                }
                else
                {
                    this.context.SetBrush(record.BrushId);
                }
                action();
            });
        }

        private void PerformIsolatedAction(Action action)
        {
            this.context.SaveGraphicsState();
            this.context.ApplyClip();
            action();
            this.context.RestoreGraphicsState();
        }

        private void PerformPenDrawAction(EmfPlusPenDrawingRecord record, Action action)
        {
            this.PerformIsolatedAction(delegate {
                this.context.SetPen(record.PenId);
                action();
            });
        }

        public void Visit(EmfMetafileHeaderRecord headerRecord)
        {
            float num = (headerRecord.Device.Cx * 25.4f) / ((float) headerRecord.Millimeters.Cx);
            float num2 = (headerRecord.Device.Cy * 25.4f) / ((float) headerRecord.Millimeters.Cy);
            EmfRectL frame = headerRecord.Frame;
            float num4 = (((float) frame.Top) / 2540f) * num2;
            float num5 = ((1f / num) + (((float) (frame.Right - frame.Left)) / 2540f)) * 72f;
            float num6 = ((1f / num2) + (((float) (frame.Bottom - frame.Top)) / 2540f)) * 72f;
            PdfTransformationMatrix matrix = PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, (double) -((((float) frame.Left) / 2540f) * num), (double) -num4), new PdfTransformationMatrix((double) (72f / num), 0.0, 0.0, (double) (72f / num2), 0.0, 0.0)), new PdfTransformationMatrix(this.destinationRectangle.Width / ((double) num5), 0.0, 0.0, this.destinationRectangle.Height / ((double) num6), 0.0, 0.0));
            this.context.SetInitialOffset(matrix);
        }

        public void Visit(EmfPlusClearRecord clearRecord)
        {
            this.context.Clear(clearRecord.Color);
        }

        public void Visit(EmfPlusContinuedObjectRecord record)
        {
            this.context.AddEmfPlusContinuedObject(record.ObjectId, record.Value);
        }

        public void Visit(EmfPlusDrawArcRecord record)
        {
            this.PerformPenDrawAction(record, delegate {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(PdfGDIPlusGraphicsConverter.ConvertRectangle(record.Bounds), record.StartAngle, record.SweepAngle);
                    this.context.DrawPath(path.PathPoints, path.PathTypes);
                }
            });
        }

        public void Visit(EmfPlusDrawBeziersRecord record)
        {
            this.PerformPenDrawAction(record, () => this.context.DrawBeziers(record.Points));
        }

        public void Visit(EmfPlusDrawDriverStringRecord record)
        {
            if (record.StringOptions.HasFlag(EmfPlusDriverStringOptions.DriverStringOptionsCMapLookup))
            {
                this.context.DrawUnicodeString(record.Glyphs, record.Positions, record.FontId, record.BrushId, record.BrushColor);
            }
        }

        public void Visit(EmfPlusDrawEllipseRecord record)
        {
            this.PerformPenDrawAction(record, () => this.context.DrawEllipse(record.Bounds));
        }

        public void Visit(EmfPlusDrawImagePointsRecord record)
        {
            this.PerformIsolatedAction(() => this.context.DrawImage(record.ObjectId, record.Points, record.SrcRectangle));
        }

        public void Visit(EmfPlusDrawImageRecord record)
        {
            this.PerformIsolatedAction(() => this.context.DrawImage(record.ObjectId, record.BoundingBox, record.SrcRectangle));
        }

        public void Visit(EmfPlusDrawLinesRecord record)
        {
            this.PerformPenDrawAction(record, delegate {
                if (record.IsPolygon)
                {
                    this.context.DrawPolygon(record.Points);
                }
                else
                {
                    this.context.DrawLines(record.Points);
                }
            });
        }

        public void Visit(EmfPlusDrawPathRecord record)
        {
            this.PerformIsolatedAction(delegate {
                this.context.SetPen(record.PenId);
                this.context.DrawPath(record.ObjectId);
            });
        }

        public void Visit(EmfPlusDrawPieRecord record)
        {
            this.PerformPenDrawAction(record, delegate {
                using (GraphicsPath path = new GraphicsPath())
                {
                    DXRectangleF bounds = record.Bounds;
                    path.AddPie(bounds.X, bounds.Y, bounds.Width, bounds.Height, record.StartAngle, record.SweepAngle);
                    this.context.DrawPath(path.PathPoints, path.PathTypes);
                }
            });
        }

        public void Visit(EmfPlusDrawRectsRecord record)
        {
            this.PerformPenDrawAction(record, () => this.context.DrawRectangles(record.Rectangles));
        }

        public void Visit(EmfPlusDrawStringRecord record)
        {
            this.PerformIsolatedAction(() => this.context.DrawString(record.Text, record.LayoutRect, record.ObjectId, record.BrushColor, record.BrushId, record.FormatId));
        }

        public void Visit(EmfPlusFillEllipseRecord record)
        {
            this.PerformFillAction(record, () => this.context.FillEllipse(record.Bounds));
        }

        public void Visit(EmfPlusFillPathRecord record)
        {
            this.PerformFillAction(record, () => this.context.FillPath(record.ObjectId));
        }

        public void Visit(EmfPlusFillPieRecord record)
        {
            this.PerformFillAction(record, delegate {
                using (GraphicsPath path = new GraphicsPath())
                {
                    DXRectangleF bounds = record.Bounds;
                    path.AddPie(bounds.X, bounds.Y, bounds.Width, bounds.Height, record.StartAngle, record.SweepAngle);
                    this.context.FillPath(path.PathPoints, path.PathTypes);
                }
            });
        }

        public void Visit(EmfPlusFillPolygonRecord record)
        {
            this.PerformFillAction(record, () => this.context.FillPolygon(record.Points));
        }

        public void Visit(EmfPlusFillRectsRecord record)
        {
            this.PerformFillAction(record, () => this.context.FillRects(record.Rectangles));
        }

        public void Visit(EmfPlusHeaderRecord headerRecord)
        {
            this.context.SetLogicalDpi((float) headerRecord.LogicalDpiX, (float) headerRecord.LogicalDpiY, headerRecord.IsVideoDisplay);
            this.context.SetPageUnit(DXGraphicsUnit.Display, 1f);
        }

        public void Visit(EmfPlusModifyWorldTransform record)
        {
            this.context.MultiplyWorldTransform(record.Matrix, record.IsPostMultiplied);
        }

        public void Visit(EmfPlusObjectRecord record)
        {
            this.context.AddEmfPlusObject(record.ObjectId, record.Value);
        }

        public void Visit(EmfPlusResetClipRecord record)
        {
            this.context.ResetClip();
        }

        public void Visit(EmfPlusResetWorldTransformRecord record)
        {
            this.context.SetWorldTransform(new DXTransformationMatrix());
        }

        public void Visit(EmfPlusRestoreRecord record)
        {
            this.context.Restore(record.Id);
        }

        public void Visit(EmfPlusRotateWorldTransformRecord record)
        {
            this.context.RotateTransform(record.Angle, record.IsPostMultiplied);
        }

        public void Visit(EmfPlusSaveRecord record)
        {
            this.context.Save(record.Id);
        }

        public void Visit(EmfPlusSetClipPathRecord record)
        {
            this.context.SetClipPath(record.ObjectId, record.CombineMode);
        }

        public void Visit(EmfPlusSetClipRectRecord record)
        {
            this.context.SetClipRectangle(record.Rectangle, record.CombineMode);
        }

        public void Visit(EmfPlusSetClipRegionRecord record)
        {
            this.context.SetClipRegion(record.ObjectId, record.CombineMode);
        }

        public void Visit(EmfPlusSetPageTransformRecord record)
        {
            this.context.SetPageUnit(record.Unit, record.ScaleFactor);
        }

        public void Visit(EmfPlusSetWorldTransformRecord record)
        {
            this.context.SetWorldTransform(record.Matrix);
        }
    }
}

