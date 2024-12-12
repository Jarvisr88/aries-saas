namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PageControl : ItemsControl
    {
        public static readonly DependencyProperty PageDisplayModeProperty;
        public static readonly DependencyProperty MaxPageCountProperty;

        static PageControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            PageDisplayModeProperty = DependencyPropertyRegistrator.Register<PageControl, DevExpress.Xpf.DocumentViewer.PageDisplayMode>(System.Linq.Expressions.Expression.Lambda<Func<PageControl, DevExpress.Xpf.DocumentViewer.PageDisplayMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControl.get_PageDisplayMode)), parameters));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            MaxPageCountProperty = DependencyPropertyRegistrator.Register<PageControl, int>(System.Linq.Expressions.Expression.Lambda<Func<PageControl, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageControl.get_MaxPageCount)), expressionArray2));
        }

        public PageControl()
        {
            base.DefaultStyleKey = typeof(PageControl);
            base.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        [CompilerGenerated, DebuggerHidden]
        private Size <>n__0(Size constraint) => 
            base.MeasureOverride(constraint);

        protected override DependencyObject GetContainerForItemOverride() => 
            new PageControlItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PageControlItem;

        protected override Size MeasureOverride(Size constraint)
        {
            foreach (object obj2 in (IEnumerable) base.Items)
            {
                Action<UIElement> action = <>c.<>9__23_0;
                if (<>c.<>9__23_0 == null)
                {
                    Action<UIElement> local1 = <>c.<>9__23_0;
                    action = <>c.<>9__23_0 = delegate (UIElement x) {
                        x.InvalidateMeasure();
                    };
                }
                (base.ItemContainerGenerator.ContainerFromItem(obj2) as UIElement).Do<UIElement>(action);
            }
            this.Panel.Do<System.Windows.Controls.Panel>(delegate (System.Windows.Controls.Panel x) {
                Func<PageWrapper, double> func1 = <>c.<>9__23_2;
                if (<>c.<>9__23_2 == null)
                {
                    Func<PageWrapper, double> local1 = <>c.<>9__23_2;
                    func1 = <>c.<>9__23_2 = y => y.CalcFirstPageLeftOffset();
                }
                x.Margin = new Thickness((base.DataContext as PageWrapper).Return<PageWrapper, double>(func1, <>c.<>9__23_3 ??= () => 0.0), 0.0, 0.0, 0.0);
            });
            if (this.PageDisplayMode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Wrap)
            {
                return base.MeasureOverride(constraint);
            }
            if (base.DataContext == null)
            {
                return base.MeasureOverride(constraint);
            }
            Func<PageWrapper, Size> evaluator = <>c.<>9__23_4;
            if (<>c.<>9__23_4 == null)
            {
                Func<PageWrapper, Size> local2 = <>c.<>9__23_4;
                evaluator = <>c.<>9__23_4 = x => x.RenderSize;
            }
            return (base.DataContext as PageWrapper).Return<PageWrapper, Size>(evaluator, () => this.<>n__0(constraint));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Panel.Do<System.Windows.Controls.Panel>(delegate (System.Windows.Controls.Panel x) {
                Func<PageWrapper, double> evaluator = <>c.<>9__18_1;
                if (<>c.<>9__18_1 == null)
                {
                    Func<PageWrapper, double> local1 = <>c.<>9__18_1;
                    evaluator = <>c.<>9__18_1 = y => y.CalcFirstPageLeftOffset();
                }
                x.Margin = new Thickness((base.DataContext as PageWrapper).Return<PageWrapper, double>(evaluator, <>c.<>9__18_2 ??= () => 0.0), 0.0, 0.0, 0.0);
            });
        }

        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Func<PageWrapper, IEnumerable<IPage>> evaluator = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<PageWrapper, IEnumerable<IPage>> local1 = <>c.<>9__19_0;
                evaluator = <>c.<>9__19_0 = x => x.Pages;
            }
            this.ItemsSource = (e.NewValue as PageWrapper).With<PageWrapper, IEnumerable<IPage>>(evaluator);
            this.Panel.Do<System.Windows.Controls.Panel>(delegate (System.Windows.Controls.Panel x) {
                Func<PageWrapper, double> func1 = <>c.<>9__19_2;
                if (<>c.<>9__19_2 == null)
                {
                    Func<PageWrapper, double> local1 = <>c.<>9__19_2;
                    func1 = <>c.<>9__19_2 = y => y.CalcFirstPageLeftOffset();
                }
                x.Margin = new Thickness((e.NewValue as PageWrapper).Return<PageWrapper, double>(func1, <>c.<>9__19_3 ??= () => 0.0), 0.0, 0.0, 0.0);
            });
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            ItemsPresenter templateChild = (ItemsPresenter) base.GetTemplateChild("PART_ItemsPresenter");
            if (templateChild != null)
            {
                Predicate<FrameworkElement> predicate = <>c.<>9__17_0;
                if (<>c.<>9__17_0 == null)
                {
                    Predicate<FrameworkElement> local1 = <>c.<>9__17_0;
                    predicate = <>c.<>9__17_0 = x => x is System.Windows.Controls.Panel;
                }
                this.Panel = (System.Windows.Controls.Panel) LayoutHelper.FindElement(templateChild, predicate);
                this.Panel.Do<System.Windows.Controls.Panel>(delegate (System.Windows.Controls.Panel x) {
                    Func<PageWrapper, double> evaluator = <>c.<>9__17_2;
                    if (<>c.<>9__17_2 == null)
                    {
                        Func<PageWrapper, double> local1 = <>c.<>9__17_2;
                        evaluator = <>c.<>9__17_2 = y => y.CalcFirstPageLeftOffset();
                    }
                    x.Margin = new Thickness((base.DataContext as PageWrapper).Return<PageWrapper, double>(evaluator, <>c.<>9__17_3 ??= () => 0.0), 0.0, 0.0, 0.0);
                });
                (this.Panel as PageControlPanel).Do<PageControlPanel>(x => x.PageWrapper = base.DataContext as PageWrapper);
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            ((PageControlItem) element).PageWrapper = (PageWrapper) base.DataContext;
        }

        private System.Windows.Controls.Panel Panel { get; set; }

        internal double PageSpacing { get; set; }

        public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.PageDisplayMode) base.GetValue(PageDisplayModeProperty);
            set => 
                base.SetValue(PageDisplayModeProperty, value);
        }

        public int MaxPageCount
        {
            get => 
                (int) base.GetValue(MaxPageCountProperty);
            set => 
                base.SetValue(MaxPageCountProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageControl.<>c <>9 = new PageControl.<>c();
            public static Predicate<FrameworkElement> <>9__17_0;
            public static Func<PageWrapper, double> <>9__17_2;
            public static Func<double> <>9__17_3;
            public static Func<PageWrapper, double> <>9__18_1;
            public static Func<double> <>9__18_2;
            public static Func<PageWrapper, IEnumerable<IPage>> <>9__19_0;
            public static Func<PageWrapper, double> <>9__19_2;
            public static Func<double> <>9__19_3;
            public static Action<UIElement> <>9__23_0;
            public static Func<PageWrapper, double> <>9__23_2;
            public static Func<double> <>9__23_3;
            public static Func<PageWrapper, Size> <>9__23_4;

            internal void <MeasureOverride>b__23_0(UIElement x)
            {
                x.InvalidateMeasure();
            }

            internal double <MeasureOverride>b__23_2(PageWrapper y) => 
                y.CalcFirstPageLeftOffset();

            internal double <MeasureOverride>b__23_3() => 
                0.0;

            internal Size <MeasureOverride>b__23_4(PageWrapper x) => 
                x.RenderSize;

            internal double <OnApplyTemplate>b__18_1(PageWrapper y) => 
                y.CalcFirstPageLeftOffset();

            internal double <OnApplyTemplate>b__18_2() => 
                0.0;

            internal IEnumerable<IPage> <OnDataContextChanged>b__19_0(PageWrapper x) => 
                x.Pages;

            internal double <OnDataContextChanged>b__19_2(PageWrapper y) => 
                y.CalcFirstPageLeftOffset();

            internal double <OnDataContextChanged>b__19_3() => 
                0.0;

            internal bool <OnLoaded>b__17_0(FrameworkElement x) => 
                x is Panel;

            internal double <OnLoaded>b__17_2(PageWrapper y) => 
                y.CalcFirstPageLeftOffset();

            internal double <OnLoaded>b__17_3() => 
                0.0;
        }
    }
}

