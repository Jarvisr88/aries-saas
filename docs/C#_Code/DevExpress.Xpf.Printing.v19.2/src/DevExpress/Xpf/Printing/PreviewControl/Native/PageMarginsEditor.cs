namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PageMarginsEditor : Control<PageMarginsEditor>
    {
        public static readonly DependencyProperty PageProperty;
        public static readonly DependencyProperty ZoomProperty;
        public static readonly DependencyPropertyKey LeftMarginPropertyKey;
        public static readonly DependencyProperty LeftMarginProperty;
        public static readonly DependencyPropertyKey TopMarginPropertyKey;
        public static readonly DependencyProperty TopMarginProperty;
        public static readonly DependencyPropertyKey RightMarginPropertyKey;
        public static readonly DependencyProperty RightMarginProperty;
        public static readonly DependencyPropertyKey BottomMarginPropertyKey;
        public static readonly DependencyProperty BottomMarginProperty;

        static PageMarginsEditor()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageMarginsEditor), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<PageMarginsEditor> registrator1 = DependencyPropertyRegistrator<PageMarginsEditor>.New().Register<PageViewModel>(System.Linq.Expressions.Expression.Lambda<Func<PageMarginsEditor, PageViewModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageMarginsEditor.get_Page)), parameters), out PageProperty, null, (d, o, n) => d.OnPageChanged(o, n), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageMarginsEditor), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<PageMarginsEditor> registrator2 = registrator1.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<PageMarginsEditor, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageMarginsEditor.get_Zoom)), expressionArray2), out ZoomProperty, 1.0, d => d.OnZoomChanged(), (d, n) => (n <= 0.0) ? 1.0 : n, 0);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageMarginsEditor), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageMarginsEditor> registrator3 = registrator2.RegisterReadOnly<PageMarginModel>(System.Linq.Expressions.Expression.Lambda<Func<PageMarginsEditor, PageMarginModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageMarginsEditor.get_LeftMargin)), expressionArray3), out LeftMarginPropertyKey, out LeftMarginProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageMarginsEditor), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageMarginsEditor> registrator4 = registrator3.RegisterReadOnly<PageMarginModel>(System.Linq.Expressions.Expression.Lambda<Func<PageMarginsEditor, PageMarginModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageMarginsEditor.get_TopMargin)), expressionArray4), out TopMarginPropertyKey, out TopMarginProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageMarginsEditor), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<PageMarginsEditor> registrator5 = registrator4.RegisterReadOnly<PageMarginModel>(System.Linq.Expressions.Expression.Lambda<Func<PageMarginsEditor, PageMarginModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageMarginsEditor.get_RightMargin)), expressionArray5), out RightMarginPropertyKey, out RightMarginProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageMarginsEditor), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator5.RegisterReadOnly<PageMarginModel>(System.Linq.Expressions.Expression.Lambda<Func<PageMarginsEditor, PageMarginModel>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageMarginsEditor.get_BottomMargin)), expressionArray6), out BottomMarginPropertyKey, out BottomMarginProperty, null, frameworkOptions);
        }

        public PageMarginsEditor()
        {
            this.LeftMargin = PageMarginModel.Create(MarginSide.Left);
            this.TopMargin = PageMarginModel.Create(MarginSide.Top);
            this.RightMargin = PageMarginModel.Create(MarginSide.Right);
            this.BottomMargin = PageMarginModel.Create(MarginSide.Bottom);
        }

        private void ClearDefinitions()
        {
            this.LeftMargin.SetDefinitions(0.0, 0.0);
            this.TopMargin.SetDefinitions(0.0, 0.0);
            this.RightMargin.SetDefinitions(0.0, 0.0);
            this.BottomMargin.SetDefinitions(0.0, 0.0);
        }

        private void OnPageChanged(PageViewModel oldValue, PageViewModel newValue)
        {
            oldValue.Do<PageViewModel>(x => x.PageChanged -= new EventHandler(this.OnPagePropertyChanged));
            if (newValue == null)
            {
                this.ClearDefinitions();
            }
            else
            {
                this.SetDefinitions();
                oldValue.Do<PageViewModel>(x => x.PageChanged += new EventHandler(this.OnPagePropertyChanged));
            }
        }

        private void OnPagePropertyChanged(object sender, EventArgs e)
        {
            this.SetDefinitions();
        }

        private void OnZoomChanged()
        {
            this.LeftMargin.Zoom = this.Zoom;
            this.TopMargin.Zoom = this.Zoom;
            this.RightMargin.Zoom = this.Zoom;
            this.BottomMargin.Zoom = this.Zoom;
        }

        private void SetDefinitions()
        {
            this.LeftMargin.SetDefinitions(this.Page.PageMargins.Left, (this.Page.Page.PageSize.Width + this.Page.PageMargins.Left) - 50.0);
            this.TopMargin.SetDefinitions(this.Page.PageMargins.Top, (this.Page.Page.PageSize.Height + this.Page.PageMargins.Top) - 50.0);
            this.RightMargin.SetDefinitions(this.Page.PageMargins.Right, (this.Page.Page.PageSize.Width + this.Page.PageMargins.Right) - 50.0);
            this.BottomMargin.SetDefinitions(this.Page.PageMargins.Bottom, (this.Page.Page.PageSize.Height + this.Page.PageMargins.Bottom) - 50.0);
        }

        public PageViewModel Page
        {
            get => 
                (PageViewModel) base.GetValue(PageProperty);
            set => 
                base.SetValue(PageProperty, value);
        }

        public double Zoom
        {
            get => 
                (double) base.GetValue(ZoomProperty);
            set => 
                base.SetValue(ZoomProperty, value);
        }

        public PageMarginModel LeftMargin
        {
            get => 
                (PageMarginModel) base.GetValue(LeftMarginProperty);
            private set => 
                base.SetValue(LeftMarginPropertyKey, value);
        }

        public PageMarginModel TopMargin
        {
            get => 
                (PageMarginModel) base.GetValue(TopMarginProperty);
            private set => 
                base.SetValue(TopMarginPropertyKey, value);
        }

        public PageMarginModel RightMargin
        {
            get => 
                (PageMarginModel) base.GetValue(RightMarginProperty);
            private set => 
                base.SetValue(RightMarginPropertyKey, value);
        }

        public PageMarginModel BottomMargin
        {
            get => 
                (PageMarginModel) base.GetValue(BottomMarginProperty);
            private set => 
                base.SetValue(BottomMarginPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageMarginsEditor.<>c <>9 = new PageMarginsEditor.<>c();

            internal void <.cctor>b__28_0(PageMarginsEditor d, PageViewModel o, PageViewModel n)
            {
                d.OnPageChanged(o, n);
            }

            internal void <.cctor>b__28_1(PageMarginsEditor d)
            {
                d.OnZoomChanged();
            }

            internal double <.cctor>b__28_2(PageMarginsEditor d, double n) => 
                (n <= 0.0) ? 1.0 : n;
        }
    }
}

