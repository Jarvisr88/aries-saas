namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PreviewBehaviorProvider : BehaviorProvider
    {
        private Size realPageSize;
        private DevExpress.Xpf.DocumentViewer.PageDisplayMode pageDisplayMode;

        internal override DevExpress.Xpf.DocumentViewer.ZoomHelper CreateZoomHelper()
        {
            PreviewZoomHelper helper;
            this.ZoomHelper = helper = new PreviewZoomHelper();
            return helper;
        }

        private void OnPageDisplayModeChanged()
        {
            this.ZoomHelper.PageDisplayMode = this.PageDisplayMode;
            base.UpdateZoomFactor();
        }

        private void OnRealPageSizeChanged()
        {
            this.ZoomHelper.RealPageSize = this.RealPageSize;
            base.UpdateZoomFactor();
        }

        private PreviewZoomHelper ZoomHelper { get; set; }

        public Size RealPageSize
        {
            get => 
                this.realPageSize;
            set => 
                base.SetProperty<Size>(ref this.realPageSize, value, System.Linq.Expressions.Expression.Lambda<Func<Size>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewBehaviorProvider)), (MethodInfo) methodof(PreviewBehaviorProvider.get_RealPageSize)), new ParameterExpression[0]), new Action(this.OnRealPageSizeChanged));
        }

        public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode
        {
            get => 
                this.pageDisplayMode;
            set => 
                base.SetProperty<DevExpress.Xpf.DocumentViewer.PageDisplayMode>(ref this.pageDisplayMode, value, System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.DocumentViewer.PageDisplayMode>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PreviewBehaviorProvider)), (MethodInfo) methodof(PreviewBehaviorProvider.get_PageDisplayMode)), new ParameterExpression[0]), new Action(this.OnPageDisplayModeChanged));
        }

        private class PreviewZoomHelper : ZoomHelper
        {
            protected override double CalcViewPageWidth() => 
                (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap) ? ((base.Viewport.IsEmpty || this.RealPageSize.IsEmpty) ? 1.0 : (base.Viewport.Width / this.RealPageSize.Width)) : base.CalcViewPageWidth();

            protected override double CalcViewWholePage() => 
                (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap) ? ((base.Viewport.IsEmpty || this.RealPageSize.IsEmpty) ? 1.0 : Math.Min((double) (base.Viewport.Width / this.RealPageSize.Width), (double) (base.Viewport.Height / this.RealPageSize.Height))) : base.CalcViewWholePage();

            public Size RealPageSize { get; set; }

            public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode { get; set; }
        }
    }
}

