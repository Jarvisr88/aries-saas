namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Pdf;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfPageViewModel : BindableBase, IPdfPage, IPage, ISupportDefferedRendering
    {
        private const double DpiMultiplierInternal = 1.5277777777777777;
        private static double ScaleX = ScreenHelper.ScaleX;
        private readonly PdfPage page;
        private readonly int pageIndex;
        private readonly int pageNumber;
        private readonly Size pageSize;
        private Size visibleSize;
        private readonly double userUnit;
        private readonly Size inchPageSize;
        private bool isSelected;
        private IEnumerable<PdfElement> renderContent;
        private bool needsInvalidate;
        private bool forceInvalidate;

        public PdfPageViewModel(PdfPage page, int pageIndex)
        {
            this.page = page;
            this.pageIndex = pageIndex;
            this.pageNumber = pageIndex + 1;
            this.userUnit = page.UserUnit;
            Func<PdfPage, PdfRectangle> getRectHandle = <>c.<>9__44_0;
            if (<>c.<>9__44_0 == null)
            {
                Func<PdfPage, PdfRectangle> local1 = <>c.<>9__44_0;
                getRectHandle = <>c.<>9__44_0 = x => x.CropBox;
            }
            this.pageSize = this.GetPageSize(getRectHandle);
            Func<PdfPage, PdfRectangle> func2 = <>c.<>9__44_1;
            if (<>c.<>9__44_1 == null)
            {
                Func<PdfPage, PdfRectangle> local2 = <>c.<>9__44_1;
                func2 = <>c.<>9__44_1 = x => x.BleedBox;
            }
            this.visibleSize = this.GetPageSize(func2);
            Func<PdfPage, PdfRectangle> func3 = <>c.<>9__44_2;
            if (<>c.<>9__44_2 == null)
            {
                Func<PdfPage, PdfRectangle> local3 = <>c.<>9__44_2;
                func3 = <>c.<>9__44_2 = x => x.MediaBox;
            }
            this.inchPageSize = this.GetInchPageSize(func3);
        }

        public PdfPoint CalcTopLeftPoint(double angle) => 
            this.page.GetTopLeftCorner((int) angle);

        public Size GetInchPageSize(Func<PdfPage, PdfRectangle> getRectHandle)
        {
            PdfRectangle rectangle = getRectHandle(this.page);
            int rotate = this.page.Rotate;
            double num2 = this.page.UserUnit / 72.0;
            return (((rotate == 90) || (rotate == 270)) ? new Size(rectangle.Height * num2, rectangle.Width * num2) : new Size(rectangle.Width * num2, rectangle.Height * num2));
        }

        public Size GetPageSize(Func<PdfPage, PdfRectangle> getRectHandle)
        {
            double width;
            double height;
            PdfRectangle rectangle = getRectHandle(this.Page);
            int rotate = this.page.Rotate;
            if ((rotate != 90) && (rotate != 270))
            {
                width = rectangle.Width;
                height = rectangle.Height;
            }
            else
            {
                width = rectangle.Height;
                height = rectangle.Width;
            }
            return new Size(width * this.DpiMultiplier, height * this.DpiMultiplier);
        }

        public PdfPoint GetPdfPoint(Point point, double zoomFactor, double angle) => 
            this.Page.FromUserSpace(new PdfPoint(point.X, point.Y), zoomFactor * this.DpiMultiplier, (int) angle);

        public Point GetPoint(PdfPoint pdfPoint, double zoomFactor, double angle)
        {
            PdfPoint point = this.Page.ToUserSpace(pdfPoint, zoomFactor * this.DpiMultiplier, (int) angle);
            return new Point(point.X, point.Y);
        }

        public PdfPage Page =>
            this.page;

        public PdfDocumentArea PageArea =>
            new PdfDocumentArea(this.PageNumber, this.Page.CropBox);

        public int PageIndex =>
            this.pageIndex;

        public int PageNumber =>
            this.pageNumber;

        public Size PageSize =>
            this.pageSize;

        public double UserUnit =>
            this.userUnit;

        public Size InchPageSize =>
            this.inchPageSize;

        public Thickness Margin =>
            new Thickness(5.0);

        public bool NeedsInvalidate
        {
            get => 
                this.needsInvalidate;
            set => 
                base.SetProperty<bool>(ref this.needsInvalidate, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfPageViewModel)), (MethodInfo) methodof(PdfPageViewModel.get_NeedsInvalidate)), new ParameterExpression[0]));
        }

        public bool ForceInvalidate
        {
            get => 
                this.forceInvalidate;
            set => 
                base.SetProperty<bool>(ref this.forceInvalidate, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfPageViewModel)), (MethodInfo) methodof(PdfPageViewModel.get_ForceInvalidate)), new ParameterExpression[0]));
        }

        public IEnumerable<PdfElement> RenderContent
        {
            get => 
                this.renderContent;
            internal set => 
                base.SetProperty<IEnumerable<PdfElement>>(ref this.renderContent, value, System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<PdfElement>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfPageViewModel)), (MethodInfo) methodof(PdfPageViewModel.get_RenderContent)), new ParameterExpression[0]));
        }

        public Size VisibleSize
        {
            get => 
                this.visibleSize;
            private set => 
                base.SetProperty<Size>(ref this.visibleSize, value, System.Linq.Expressions.Expression.Lambda<Func<Size>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfPageViewModel)), (MethodInfo) methodof(PdfPageViewModel.get_VisibleSize)), new ParameterExpression[0]));
        }

        public bool IsSelected
        {
            get => 
                this.isSelected;
            internal set => 
                base.SetProperty<bool>(ref this.isSelected, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfPageViewModel)), (MethodInfo) methodof(PdfPageViewModel.get_IsSelected)), new ParameterExpression[0]));
        }

        public double DpiMultiplier =>
            this.page.UserUnit * 1.5277777777777777;

        bool IPage.IsLoading =>
            false;

        TextureKey ISupportDefferedRendering.TextureKey { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPageViewModel.<>c <>9 = new PdfPageViewModel.<>c();
            public static Func<PdfPage, PdfRectangle> <>9__44_0;
            public static Func<PdfPage, PdfRectangle> <>9__44_1;
            public static Func<PdfPage, PdfRectangle> <>9__44_2;

            internal PdfRectangle <.ctor>b__44_0(PdfPage x) => 
                x.CropBox;

            internal PdfRectangle <.ctor>b__44_1(PdfPage x) => 
                x.BleedBox;

            internal PdfRectangle <.ctor>b__44_2(PdfPage x) => 
                x.MediaBox;
        }
    }
}

