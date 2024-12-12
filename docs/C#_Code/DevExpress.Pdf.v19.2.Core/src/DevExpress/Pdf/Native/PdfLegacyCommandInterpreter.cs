namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfLegacyCommandInterpreter : PdfCommandInterpreter
    {
        private readonly Stack<PdfTransformationMatrix> tilingPatternTransformationMatrixStack;
        private readonly PdfPolygonClipper boundingBoxClipper;
        private int? currentFormObjectNumber;
        private PdfTransformationMatrix patternTransformationMatrix;
        private int graphicsStateStackLock;

        protected PdfLegacyCommandInterpreter(PdfPage page, int rotateAngle, PdfRectangle boundingBox) : base(page, boundingBox)
        {
            this.tilingPatternTransformationMatrixStack = new Stack<PdfTransformationMatrix>();
            PdfTransformationMatrix matrix = (rotateAngle == 90) ? new PdfTransformationMatrix(0.0, -1.0, 1.0, 0.0, -boundingBox.Bottom, boundingBox.Left) : ((rotateAngle == 180) ? new PdfTransformationMatrix(-1.0, 0.0, 0.0, -1.0, boundingBox.Right, boundingBox.Top) : ((rotateAngle == 270) ? new PdfTransformationMatrix(0.0, 1.0, -1.0, 0.0, boundingBox.Top, -boundingBox.Right) : new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, -boundingBox.Left, -boundingBox.Bottom)));
            this.boundingBoxClipper = new PdfPolygonClipper(new PdfRectangle(matrix.Transform(boundingBox.BottomLeft), matrix.Transform(boundingBox.TopRight)));
            base.GraphicsState.TransformationMatrix = PdfTransformationMatrix.Scale(matrix, page.UserUnit, page.UserUnit);
            this.UpdatePatternTransformationMatrix();
        }

        public override void DrawForm(PdfForm form)
        {
            int? currentFormObjectNumber = this.currentFormObjectNumber;
            if (!((form.ObjectNumber == currentFormObjectNumber.GetValueOrDefault()) ? (currentFormObjectNumber != null) : false))
            {
                this.SaveGraphicsState();
                this.UpdateTransformationMatrix(form.Matrix);
                this.tilingPatternTransformationMatrixStack.Push(this.patternTransformationMatrix);
                int graphicsStateStackLock = this.graphicsStateStackLock;
                this.graphicsStateStackLock = base.GraphicsStateStackSize;
                int? nullable = this.currentFormObjectNumber;
                this.currentFormObjectNumber = new int?(form.ObjectNumber);
                try
                {
                    this.UpdatePatternTransformationMatrix();
                    base.ClipRectangle(form.BBox);
                    base.Execute(form.Commands);
                }
                finally
                {
                    this.currentFormObjectNumber = nullable;
                    int num2 = base.GraphicsStateStackSize - this.graphicsStateStackLock;
                    for (int i = 0; i < num2; i++)
                    {
                        this.RestoreGraphicsState();
                    }
                    this.graphicsStateStackLock = graphicsStateStackLock;
                    if (this.tilingPatternTransformationMatrixStack.Count > 0)
                    {
                        this.patternTransformationMatrix = this.tilingPatternTransformationMatrixStack.Pop();
                    }
                    this.RestoreGraphicsState();
                }
            }
        }

        public virtual void DrawImage(PdfImageDataCacheItem imageData, byte[] data)
        {
        }

        public virtual void DrawImage(PdfImageDataCacheItem imageData, PdfImageDataSource dataSource)
        {
        }

        protected void UpdatePatternTransformationMatrix()
        {
            this.patternTransformationMatrix = base.GraphicsState.TransformationMatrix;
        }

        public PdfTransformationMatrix PatternTransformationMatrix =>
            this.patternTransformationMatrix;

        protected override int MinGraphicsStateCount =>
            this.graphicsStateStackLock;

        protected virtual IList<PdfGraphicsPath> TransformedPaths =>
            PdfGraphicsPath.Transform(base.Paths, base.GraphicsState.TransformationMatrix);

        protected IList<PdfGraphicsPath> BoundsClippedTransformedPaths
        {
            get
            {
                List<PdfGraphicsPath> list = new List<PdfGraphicsPath>();
                foreach (PdfGraphicsPath path in this.TransformedPaths)
                {
                    list.Add(this.boundingBoxClipper.Clip(path));
                }
                return list;
            }
        }
    }
}

