namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PdfDataRecognizer : PdfLegacyCommandInterpreter
    {
        private readonly PdfTextParser pageTextData;
        private readonly Dictionary<PdfPageImageData, int> pageImageData;
        private int imageIndex;

        private PdfDataRecognizer(PdfPage page, PdfRectangle textClipBox) : base(page, 0, page.CropBox)
        {
            this.pageTextData = new PdfTextParser(textClipBox);
            this.pageImageData = new Dictionary<PdfPageImageData, int>();
            double userUnit = page.UserUnit;
            PdfRectangle cropBox = page.CropBox;
            base.GraphicsState.TransformationMatrix = PdfTransformationMatrix.Scale(PdfTransformationMatrix.Translate(new PdfTransformationMatrix(), -cropBox.Left, -cropBox.Bottom), userUnit, userUnit);
        }

        public override void AppendPathBezierSegment(PdfPoint controlPoint2, PdfPoint endPoint)
        {
        }

        public override void AppendPathBezierSegment(PdfPoint controlPoint1, PdfPoint controlPoint2, PdfPoint endPoint)
        {
        }

        public override void AppendPathLineSegment(PdfPoint endPoint)
        {
        }

        public override void AppendRectangle(double x, double y, double width, double height)
        {
        }

        public override void BeginPath(PdfPoint startPoint)
        {
        }

        public override void ClipAndClearPaths()
        {
        }

        protected override void ClipPaths()
        {
        }

        public override void ClosePath()
        {
        }

        private void DrawAnnotation(PdfAnnotation annotation)
        {
            PdfForm appearanceForm = annotation.GetAppearanceForm(PdfAnnotationAppearanceState.Normal);
            if ((appearanceForm != null) && ((annotation.Flags & (PdfAnnotationFlags.NoView | PdfAnnotationFlags.Hidden)) == PdfAnnotationFlags.None))
            {
                base.DrawForm(appearanceForm.GetTrasformationMatrix(annotation.Rect), appearanceForm);
            }
        }

        public override void DrawImage(PdfImage image)
        {
            int imageIndex = this.imageIndex;
            this.imageIndex = imageIndex + 1;
            this.pageImageData[new PdfPageImageData(image, base.GraphicsState.TransformationMatrix)] = imageIndex;
        }

        public override void DrawShading(PdfShading shading)
        {
        }

        protected override void DrawString(PdfStringData stringData)
        {
            this.pageTextData.AddBlock(stringData, base.GraphicsState);
        }

        public override void FillPaths(bool useNonzeroWindingRule)
        {
        }

        public static PdfPageData Recognize(PdfPage page, bool recognizeAnnotationsData, bool clipTextByCropBox = true)
        {
            using (PdfDataRecognizer recognizer = new PdfDataRecognizer(page, clipTextByCropBox ? page.CropBox : new PdfRectangle(double.MinValue, double.MinValue, double.MaxValue, double.MaxValue)))
            {
                recognizer.SaveGraphicsState();
                recognizer.Execute();
                recognizer.RestoreGraphicsState();
                if (recognizeAnnotationsData)
                {
                    foreach (PdfAnnotation annotation in page.Annotations)
                    {
                        recognizer.DrawAnnotation(annotation);
                    }
                }
                Func<KeyValuePair<PdfPageImageData, int>, int> keySelector = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<KeyValuePair<PdfPageImageData, int>, int> local1 = <>c.<>9__0_0;
                    keySelector = <>c.<>9__0_0 = v => v.Value;
                }
                Func<KeyValuePair<PdfPageImageData, int>, PdfPageImageData> selector = <>c.<>9__0_1;
                if (<>c.<>9__0_1 == null)
                {
                    Func<KeyValuePair<PdfPageImageData, int>, PdfPageImageData> local2 = <>c.<>9__0_1;
                    selector = <>c.<>9__0_1 = v => v.Key;
                }
                return new PdfPageData(recognizer.pageTextData.Parse(), recognizer.pageImageData.OrderByDescending<KeyValuePair<PdfPageImageData, int>, int>(keySelector).Select<KeyValuePair<PdfPageImageData, int>, PdfPageImageData>(selector).ToArray<PdfPageImageData>());
            }
        }

        public override void SetColorForNonStrokingOperations(PdfColor color)
        {
        }

        public override void SetColorForStrokingOperations(PdfColor color)
        {
        }

        public override void StrokePaths()
        {
        }

        protected override void UpdateGraphicsState(PdfGraphicsStateChange change)
        {
        }

        protected override IList<PdfGraphicsPath> TransformedPaths =>
            new PdfGraphicsPath[0];

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDataRecognizer.<>c <>9 = new PdfDataRecognizer.<>c();
            public static Func<KeyValuePair<PdfPageImageData, int>, int> <>9__0_0;
            public static Func<KeyValuePair<PdfPageImageData, int>, PdfPageImageData> <>9__0_1;

            internal int <Recognize>b__0_0(KeyValuePair<PdfPageImageData, int> v) => 
                v.Value;

            internal PdfPageImageData <Recognize>b__0_1(KeyValuePair<PdfPageImageData, int> v) => 
                v.Key;
        }
    }
}

