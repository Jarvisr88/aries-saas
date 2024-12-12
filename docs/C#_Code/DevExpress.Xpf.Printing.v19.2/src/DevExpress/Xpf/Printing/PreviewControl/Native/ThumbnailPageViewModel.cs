namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class ThumbnailPageViewModel : PageViewModelBase
    {
        private readonly double thumbnailBaseSize;
        private readonly Thickness margin;

        protected ThumbnailPageViewModel(Page page) : base(page)
        {
            this.thumbnailBaseSize = 150.0;
            this.margin = new Thickness(10.0, 5.0, 10.0, 5.0);
        }

        public static ThumbnailPageViewModel Create(Page page)
        {
            <>c__DisplayClass4_0 class_;
            System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass4_0)), fieldof(<>c__DisplayClass4_0.page)) };
            return ViewModelSource.Create<ThumbnailPageViewModel>(System.Linq.Expressions.Expression.Lambda<Func<ThumbnailPageViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(ThumbnailPageViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), new ParameterExpression[0]));
        }

        protected override double GetScaleMultiplier()
        {
            float num = Math.Max(this.Page.PageSize.Width, this.Page.PageSize.Height);
            return (this.thumbnailBaseSize / ((double) num.DocToDip()));
        }

        internal override void SyncPageInfo()
        {
            base.SyncPageInfo();
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ThumbnailPageViewModel), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            this.RaisePropertyChanged<ThumbnailPageViewModel, int>(System.Linq.Expressions.Expression.Lambda<Func<ThumbnailPageViewModel, int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ThumbnailPageViewModel)), (MethodInfo) methodof(ThumbnailPageViewModel.get_PageNumber)), parameters));
        }

        public int PageNumber =>
            this.PageIndex + 1;

        protected override Thickness Margin =>
            this.margin;
    }
}

