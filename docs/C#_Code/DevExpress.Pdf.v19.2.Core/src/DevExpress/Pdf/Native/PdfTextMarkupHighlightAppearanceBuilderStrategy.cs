namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextMarkupHighlightAppearanceBuilderStrategy : PdfTextMarkupAppearanceBuilderStrategy
    {
        private const double bezierOffsetFactor = 0.235;
        private readonly PdfGroupForm groupForm;
        private readonly PdfFormCommandConstructor groupFormCommandConstructor;

        public PdfTextMarkupHighlightAppearanceBuilderStrategy(PdfTextMarkupAnnotation annotation, PdfFormCommandConstructor constructor) : base(annotation, constructor)
        {
            this.groupForm = new PdfGroupForm(constructor.DocumentCatalog, constructor.BoundingBox);
            this.groupFormCommandConstructor = new PdfFormCommandConstructor(this.groupForm);
        }

        protected override void BeginRebuildAppearance()
        {
            base.BeginRebuildAppearance();
            PdfGraphicsStateParameters parameters = new PdfGraphicsStateParameters();
            parameters.BlendMode = 2;
            base.Constructor.SetGraphicsStateParameters(parameters);
            PdfTextMarkupAnnotation annotation = base.Annotation;
            PdfRectangle rect = annotation.Rect;
            this.groupFormCommandConstructor.SetColorForNonStrokingOperations(annotation.Color);
            this.groupFormCommandConstructor.ModifyTransformationMatrix(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, -rect.Left, -rect.Bottom));
        }

        public override void Dispose()
        {
            this.groupFormCommandConstructor.Dispose();
        }

        protected override void EndRebuildAppearance()
        {
            this.groupForm.ReplaceCommands(this.groupFormCommandConstructor.Commands);
            base.Constructor.DrawForm(this.groupForm.DocumentCatalog.Objects.AddResolvedObject(this.groupForm), new PdfTransformationMatrix());
            base.EndRebuildAppearance();
        }

        protected override void RebuildQuad(PdfQuadrilateral quad)
        {
            PdfPoint startPoint = quad.P1;
            PdfPoint point2 = quad.P2;
            PdfPoint point3 = quad.P3;
            PdfPoint endPoint = quad.P4;
            PdfGraphicsPath path = new PdfGraphicsPath(startPoint);
            double num = -(startPoint.X - point3.X) * 0.235;
            double num2 = (startPoint.Y - point3.Y) * 0.235;
            PdfPoint point5 = PdfPoint.Lerp(startPoint, point3, 0.235);
            PdfPoint point6 = new PdfPoint(point5.X - num2, point5.Y - num);
            point5 = PdfPoint.Lerp(point3, startPoint, 0.235);
            path.AppendBezierSegment(point6, new PdfPoint(point5.X - num2, point5.Y - num), point3);
            path.AppendLineSegment(endPoint);
            num = -(point2.X - endPoint.X) * 0.235;
            num2 = (point2.Y - endPoint.Y) * 0.235;
            point5 = PdfPoint.Lerp(endPoint, point2, 0.235);
            point6 = new PdfPoint(point5.X + num2, point5.Y + num);
            point5 = PdfPoint.Lerp(point2, endPoint, 0.235);
            path.AppendBezierSegment(point6, new PdfPoint(point5.X + num2, point5.Y + num), point2);
            path.Closed = true;
            PdfGraphicsPath[] paths = new PdfGraphicsPath[] { path };
            this.groupFormCommandConstructor.FillPath(paths, true);
        }
    }
}

