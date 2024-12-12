namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PageSelector : DocumentViewerItemsControl
    {
        public static readonly DependencyProperty IsSearchControlVisibleProperty;
        public static readonly DependencyProperty SearchParameterProperty;

        static PageSelector()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSelector), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<PageSelector> registrator1 = DependencyPropertyRegistrator<PageSelector>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<PageSelector, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSelector.get_IsSearchControlVisible)), parameters), out IsSearchControlVisibleProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PageSelector), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<TextSearchParameter>(System.Linq.Expressions.Expression.Lambda<Func<PageSelector, TextSearchParameter>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageSelector.get_SearchParameter)), expressionArray2), out SearchParameterProperty, null, frameworkOptions).OverrideMetadata(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto, null, FrameworkPropertyMetadataOptions.None).OverrideMetadata(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto, null, FrameworkPropertyMetadataOptions.None);
        }

        public PageSelector()
        {
            base.DefaultStyleKey = typeof(PageSelector);
            this.InitScrolling();
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            PreviewPageControl control = (PreviewPageControl) element;
            control.Pages = null;
            control.DataContext = null;
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new PreviewPageControl();

        private void InitScrolling()
        {
            FieldInfo field = typeof(VirtualizingPanel).GetField("ScrollUnitProperty");
            if (field != null)
            {
                DependencyProperty dp = (DependencyProperty) field.GetValue(this);
                base.SetValue(dp, Enum.GetValues(dp.DefaultMetadata.DefaultValue.GetType()).Cast<object>().First<object>());
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is PreviewPageControl;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PresenterDecorator = (Decorator) base.GetTemplateChild("PART_Decorator");
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            PreviewPageControl control = (PreviewPageControl) element;
            PageWrapper wrapper = item as PageWrapper;
            if (control != null)
            {
                control.Pages = wrapper.Pages.Cast<PageViewModel>();
                control.DataContext = wrapper;
            }
        }

        public bool IsSearchControlVisible
        {
            get => 
                (bool) base.GetValue(IsSearchControlVisibleProperty);
            set => 
                base.SetValue(IsSearchControlVisibleProperty, value);
        }

        public TextSearchParameter SearchParameter
        {
            get => 
                (TextSearchParameter) base.GetValue(SearchParameterProperty);
            set => 
                base.SetValue(SearchParameterProperty, value);
        }

        protected internal Decorator PresenterDecorator { get; private set; }
    }
}

