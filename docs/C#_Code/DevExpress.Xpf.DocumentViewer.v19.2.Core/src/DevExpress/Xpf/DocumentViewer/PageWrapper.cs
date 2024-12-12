namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PageWrapper : BindableBase
    {
        private double zoomFactor;
        private double rotateAngle;
        private double horizontalPageSpacing;
        private double verticalPageSpacing;
        private readonly IEnumerable<IPage> pages;

        public PageWrapper(IPage page) : this(pageArray1)
        {
            IPage[] pageArray1 = new IPage[] { page };
        }

        public PageWrapper(IEnumerable<IPage> pages)
        {
            this.pages = new ObservableCollection<IPage>(pages);
            this.ZoomFactor = 1.0;
            this.Initialize();
        }

        protected internal virtual double CalcFirstPageLeftOffset() => 
            0.0;

        protected internal virtual Size CalcMarginSize()
        {
            double num = 0.0;
            if (!this.RotateAngle.AreClose(90.0))
            {
                num = !this.RotateAngle.AreClose(180.0) ? (!this.RotateAngle.AreClose(270.0) ? (this.Pages.First<IPage>().Margin.Left + this.Pages.Last<IPage>().Margin.Right) : (this.Pages.First<IPage>().Margin.Top + this.Pages.Last<IPage>().Margin.Bottom)) : (this.Pages.First<IPage>().Margin.Right + this.Pages.Last<IPage>().Margin.Left);
            }
            else
            {
                Thickness margin = this.Pages.Last<IPage>().Margin;
                num = this.Pages.First<IPage>().Margin.Bottom + margin.Top;
            }
            double width = num + ((this.Pages.Count<IPage>() - 1) * this.HorizontalPageSpacing);
            double num3 = 0.0;
            foreach (IPage page in this.Pages)
            {
                num3 = Math.Max(this.IsVertical ? (page.Margin.Top + page.Margin.Bottom) : (page.Margin.Left + page.Margin.Right), num3);
            }
            return new Size(width, num3);
        }

        protected virtual Size CalcPageSize()
        {
            Func<IPage, Size> sizeHandler = <>c.<>9__63_0;
            if (<>c.<>9__63_0 == null)
            {
                Func<IPage, Size> local1 = <>c.<>9__63_0;
                sizeHandler = <>c.<>9__63_0 = x => x.PageSize;
            }
            return this.CalcSize(sizeHandler);
        }

        protected virtual double CalcPageTopOffset(IPage page) => 
            0.0;

        protected virtual Size CalcRenderSize()
        {
            Size size = this.CalcMarginSize();
            return new Size((this.PageSize.Width * this.ZoomFactor) + size.Width, ((this.PageSize.Height * this.ZoomFactor) + size.Height) + this.VerticalPageSpacing);
        }

        private Size CalcSize(Func<IPage, Size> sizeHandler)
        {
            double width = 0.0;
            double num2 = 0.0;
            foreach (IPage page in this.pages)
            {
                Size size = sizeHandler(page);
                width += this.IsVertical ? size.Width : size.Height;
                num2 = Math.Max(num2, this.IsVertical ? size.Height : size.Width);
            }
            return new Size(width, num2);
        }

        protected virtual Size CalcVisibleSize()
        {
            Func<IPage, Size> sizeHandler = <>c.<>9__62_0;
            if (<>c.<>9__62_0 == null)
            {
                Func<IPage, Size> local1 = <>c.<>9__62_0;
                sizeHandler = <>c.<>9__62_0 = x => x.VisibleSize;
            }
            return this.CalcSize(sizeHandler);
        }

        public Rect GetPageRect(IPage page) => 
            !this.Pages.All<IPage>(x => (x.PageIndex != page.PageIndex)) ? ((this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap) ? this.GetWrapModePageRect(page) : this.GetPageRectCore(page)) : Rect.Empty;

        protected Rect GetPageRectCore(IPage page)
        {
            double bottom;
            double left;
            if (this.RotateAngle.AreClose(90.0))
            {
                bottom = this.Pages.First<IPage>().Margin.Bottom;
                left = page.Margin.Left;
            }
            else if (this.RotateAngle.AreClose(180.0))
            {
                bottom = this.Pages.First<IPage>().Margin.Right;
                left = page.Margin.Bottom;
            }
            else if (this.RotateAngle.AreClose(270.0))
            {
                bottom = this.Pages.First<IPage>().Margin.Top;
                left = page.Margin.Right;
            }
            else
            {
                bottom = this.Pages.First<IPage>().Margin.Left;
                left = page.Margin.Top;
            }
            double x = this.CalcFirstPageLeftOffset() + bottom;
            double y = this.CalcPageTopOffset(page) + left;
            foreach (IPage page2 in this.Pages)
            {
                if (page2.PageIndex == page.PageIndex)
                {
                    break;
                }
                x += this.HorizontalPageSpacing + ((this.IsVertical ? page2.PageSize.Width : page2.PageSize.Height) * this.ZoomFactor);
            }
            return (this.IsVertical ? new Rect(x, y, page.PageSize.Width * this.ZoomFactor, page.PageSize.Height * this.ZoomFactor) : new Rect(x, y, page.PageSize.Height * this.ZoomFactor, page.PageSize.Width * this.ZoomFactor));
        }

        internal virtual Rect GetWrapModePageRect(IPage page)
        {
            double bottom;
            double left;
            Size viewport = this.Viewport;
            double num = viewport.Width / ((double) this.MaxPageCount);
            if (this.RotateAngle.AreClose(90.0))
            {
                bottom = this.Pages.First<IPage>().Margin.Bottom;
                left = page.Margin.Left;
            }
            else if (this.RotateAngle.AreClose(180.0))
            {
                bottom = this.Pages.First<IPage>().Margin.Right;
                left = page.Margin.Bottom;
            }
            else if (this.RotateAngle.AreClose(270.0))
            {
                bottom = this.Pages.First<IPage>().Margin.Top;
                left = page.Margin.Right;
            }
            else
            {
                bottom = this.Pages.First<IPage>().Margin.Left;
                left = page.Margin.Top;
            }
            double x = 0.0;
            foreach (IPage page2 in this.Pages)
            {
                double width;
                if (this.IsVertical)
                {
                    width = page2.PageSize.Width;
                }
                else
                {
                    viewport = page2.PageSize;
                    width = viewport.Height;
                }
                double num5 = width * this.ZoomFactor;
                double num6 = (num - num5) / 2.0;
                if (num6.LessThan(0.0))
                {
                    num6 = bottom;
                }
                x += num6;
                if (page2.PageIndex == page.PageIndex)
                {
                    break;
                }
                x += num5 + num6;
            }
            return (this.IsVertical ? new Rect(x, left, page.PageSize.Width * this.ZoomFactor, page.PageSize.Height * this.ZoomFactor) : new Rect(x, left, page.PageSize.Height * this.ZoomFactor, page.PageSize.Width * this.ZoomFactor));
        }

        protected virtual void HorizontalPageSpacingChanged()
        {
            this.RenderSize = this.CalcRenderSize();
        }

        protected virtual void Initialize()
        {
            this.PageSize = this.CalcPageSize();
            this.VisibleSize = this.CalcVisibleSize();
            this.RenderSize = this.CalcRenderSize();
        }

        protected virtual void OnRotateAngleChanged()
        {
            this.PageSize = this.CalcPageSize();
            this.VisibleSize = this.CalcVisibleSize();
            this.RenderSize = this.CalcRenderSize();
        }

        protected virtual void OnZoomFactorChanged()
        {
            this.RenderSize = this.CalcRenderSize();
        }

        protected virtual void VerticalPageSpacingChanged()
        {
            this.RenderSize = this.CalcRenderSize();
        }

        public Size VisibleSize { get; protected set; }

        public Size RenderSize { get; protected set; }

        public Size PageSize { get; protected set; }

        public IEnumerable<IPage> Pages =>
            this.pages;

        protected internal DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode { get; set; }

        protected internal Size Viewport { get; set; }

        protected internal int MaxPageCount { get; set; }

        protected internal bool IsCoverPage { get; set; }

        protected internal bool IsColumnMode { get; set; }

        protected internal bool IsVertical =>
            ((this.RotateAngle / 90.0) % 2.0) == 0.0;

        protected internal double HorizontalPageSpacing
        {
            get => 
                this.horizontalPageSpacing;
            set => 
                base.SetProperty<double>(ref this.horizontalPageSpacing, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PageWrapper)), (MethodInfo) methodof(PageWrapper.get_HorizontalPageSpacing)), new ParameterExpression[0]), new Action(this.HorizontalPageSpacingChanged));
        }

        protected internal double VerticalPageSpacing
        {
            get => 
                this.verticalPageSpacing;
            set => 
                base.SetProperty<double>(ref this.verticalPageSpacing, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PageWrapper)), (MethodInfo) methodof(PageWrapper.get_VerticalPageSpacing)), new ParameterExpression[0]), new Action(this.VerticalPageSpacingChanged));
        }

        public double ZoomFactor
        {
            get => 
                this.zoomFactor;
            set => 
                base.SetProperty<double>(ref this.zoomFactor, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PageWrapper)), (MethodInfo) methodof(PageWrapper.get_ZoomFactor)), new ParameterExpression[0]), new Action(this.OnZoomFactorChanged));
        }

        public double RotateAngle
        {
            get => 
                this.rotateAngle;
            set => 
                base.SetProperty<double>(ref this.rotateAngle, value, System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PageWrapper)), (MethodInfo) methodof(PageWrapper.get_RotateAngle)), new ParameterExpression[0]), new Action(this.OnRotateAngleChanged));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageWrapper.<>c <>9 = new PageWrapper.<>c();
            public static Func<IPage, Size> <>9__62_0;
            public static Func<IPage, Size> <>9__63_0;

            internal Size <CalcPageSize>b__63_0(IPage x) => 
                x.PageSize;

            internal Size <CalcVisibleSize>b__62_0(IPage x) => 
                x.VisibleSize;
        }
    }
}

