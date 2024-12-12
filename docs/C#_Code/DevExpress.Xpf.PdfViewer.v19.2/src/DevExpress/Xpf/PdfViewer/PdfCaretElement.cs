namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class PdfCaretElement : PdfElement
    {
        private readonly TimeSpan CaretHideTimespan = TimeSpan.FromMilliseconds(300.0);
        private readonly bool useAnimation;
        private Pen pen;

        public PdfCaretElement(Brush caretBrush, PdfPageWrapper pageWrapper, double zoomFactor, double rotateAngle, PdfCaret caret, bool useAnimation)
        {
            this.Caret = caret;
            this.PageWrapper = pageWrapper;
            this.ZoomFactor = zoomFactor;
            this.RotateAngle = rotateAngle;
            this.CaretBrush = caretBrush;
            this.useAnimation = useAnimation;
            this.InitPen();
        }

        private void InitPen()
        {
            this.pen = new Pen(this.CaretBrush, 1.0);
            if (this.useAnimation)
            {
                DoubleAnimation animation1 = new DoubleAnimation(1.0, 0.0, new Duration(this.CaretHideTimespan));
                animation1.AutoReverse = true;
                animation1.RepeatBehavior = RepeatBehavior.Forever;
                DoubleAnimation timeline = animation1;
                Timeline.SetDesiredFrameRate(timeline, 10);
                AnimationClock clock = timeline.CreateClock();
                this.pen.ApplyAnimationClock(Pen.ThicknessProperty, clock);
            }
        }

        public override void Render(DrawingContext dc, Size renderSize)
        {
            PdfPoint topLeft = this.Caret.ViewData.TopLeft;
            PdfPoint pdfPoint = new PdfPoint(topLeft.X, topLeft.Y - this.Caret.ViewData.Height);
            if (!this.Caret.ViewData.Angle.AreClose(0.0))
            {
                double angle = this.Caret.ViewData.Angle;
                pdfPoint = new PdfPoint((topLeft.X + (Math.Cos(angle) * (pdfPoint.X - topLeft.X))) - (Math.Sin(angle) * (pdfPoint.Y - topLeft.Y)), (topLeft.Y + (Math.Sin(angle) * (pdfPoint.X - topLeft.X))) + (Math.Cos(angle) * (pdfPoint.Y - topLeft.Y)));
            }
            PdfPageViewModel page = (PdfPageViewModel) this.PageWrapper.Pages.Single<IPage>(x => (x.PageIndex == this.Caret.Position.PageIndex));
            Rect pageRect = this.PageWrapper.GetPageRect(page);
            Point point3 = page.GetPoint(topLeft, this.ZoomFactor, this.RotateAngle);
            Point point4 = page.GetPoint(pdfPoint, this.ZoomFactor, this.RotateAngle);
            Point point5 = new Point(point3.X + pageRect.Left, point3.Y + pageRect.Top);
            Point point6 = new Point(point4.X + pageRect.Left, point4.Y + pageRect.Top);
            dc.DrawLine(this.pen, point5, point6);
        }

        public PdfCaret Caret { get; private set; }

        public PdfPageWrapper PageWrapper { get; private set; }

        public double ZoomFactor { get; private set; }

        public double RotateAngle { get; private set; }

        public Brush CaretBrush { get; private set; }
    }
}

