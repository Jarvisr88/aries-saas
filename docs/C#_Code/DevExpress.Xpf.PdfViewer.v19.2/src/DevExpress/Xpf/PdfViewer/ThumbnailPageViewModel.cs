namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Pdf;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ThumbnailPageViewModel : BindableBase, IThumbnailPage, ISupportDefferedRendering, IPage
    {
        private readonly double thumbnailBaseSize = 150.0;
        private const double DpiMultiplierInternal = 1.5833333333333333;
        private static readonly double ScaleX = ScreenHelper.ScaleX;
        private double userUnit;
        private double sizeScale;

        public ThumbnailPageViewModel(PdfPage page, int pageIndex)
        {
            this.Page = page;
            this.PageIndex = pageIndex;
            this.PageNumber = this.PageIndex + 1;
            this.userUnit = page.UserUnit;
            Func<PdfPage, PdfRectangle> getRectHandle = <>c.<>9__44_0;
            if (<>c.<>9__44_0 == null)
            {
                Func<PdfPage, PdfRectangle> local1 = <>c.<>9__44_0;
                getRectHandle = <>c.<>9__44_0 = x => x.CropBox;
            }
            Size pageSize = this.GetPageSize(getRectHandle);
            this.sizeScale = this.CalcThumbnailsScale(pageSize);
            this.PageSize = this.Multiply(pageSize, this.sizeScale);
            Func<PdfPage, PdfRectangle> func2 = <>c.<>9__44_1;
            if (<>c.<>9__44_1 == null)
            {
                Func<PdfPage, PdfRectangle> local2 = <>c.<>9__44_1;
                func2 = <>c.<>9__44_1 = x => x.BleedBox;
            }
            this.VisibleSize = this.Multiply(this.GetPageSize(func2), this.sizeScale);
        }

        private double CalcThumbnailsScale(Size size)
        {
            double num = Math.Max(size.Width, size.Height);
            return (this.thumbnailBaseSize / num);
        }

        public PdfPoint CalcTopLeftPoint(double angle) => 
            this.Page.GetTopLeftCorner((int) angle);

        public Size GetPageSize(Func<PdfPage, PdfRectangle> getRectHandle)
        {
            double width;
            double height;
            PdfRectangle rectangle = getRectHandle(this.Page);
            int rotate = this.Page.Rotate;
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
            return new Size(((width * this.Page.UserUnit) * 1.5833333333333333) / ScaleX, ((height * this.Page.UserUnit) * 1.5833333333333333) / ScaleX);
        }

        public PdfPoint GetPdfPoint(Point point, double zoomFactor, double angle) => 
            this.Page.FromUserSpace(new PdfPoint(point.X, point.Y), zoomFactor * this.DpiMultiplier, (int) angle);

        public Point GetPoint(PdfPoint pdfPoint, double zoomFactor, double angle)
        {
            PdfPoint point = this.Page.ToUserSpace(pdfPoint, zoomFactor * this.DpiMultiplier, (int) angle);
            return new Point(point.X, point.Y);
        }

        private Size Multiply(Size size, double scale) => 
            new Size(size.Width * scale, size.Height * scale);

        public bool NeedsInvalidate { get; set; }

        public bool ForceInvalidate { get; set; }

        public Size VisibleSize { get; private set; }

        public Size PageSize { get; private set; }

        public bool IsLoading { get; private set; }

        public int PageIndex { get; private set; }

        public int PageNumber
        {
            get => 
                base.GetProperty<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_PageNumber)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_PageNumber)), new ParameterExpression[0]), value);
        }

        public bool IsSelected
        {
            get => 
                base.GetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_IsSelected)), new ParameterExpression[0]));
            internal set => 
                base.SetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_IsSelected)), new ParameterExpression[0]), value);
        }

        public bool IsHighlighted
        {
            get => 
                base.GetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_IsHighlighted)), new ParameterExpression[0]));
            internal set => 
                base.SetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_IsHighlighted)), new ParameterExpression[0]), value);
        }

        public Thickness Margin =>
            new Thickness(10.0, 10.0, 10.0, 5.0);

        private PdfPage Page { get; set; }

        public double DpiMultiplier =>
            ((this.sizeScale * this.Page.UserUnit) * 1.5833333333333333) / ScaleX;

        bool IPage.IsLoading =>
            false;

        TextureKey ISupportDefferedRendering.TextureKey { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailPageViewModel.<>c <>9 = new ThumbnailPageViewModel.<>c();
            public static Func<PdfPage, PdfRectangle> <>9__44_0;
            public static Func<PdfPage, PdfRectangle> <>9__44_1;

            internal PdfRectangle <.ctor>b__44_0(PdfPage x) => 
                x.CropBox;

            internal PdfRectangle <.ctor>b__44_1(PdfPage x) => 
                x.BleedBox;
        }
    }
}

