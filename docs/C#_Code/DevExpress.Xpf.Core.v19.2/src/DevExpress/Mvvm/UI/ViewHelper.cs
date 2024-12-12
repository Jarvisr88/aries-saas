namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public static class ViewHelper
    {
        internal const string Error_CreateViewMissArguments = "It is impossible to create a view based on passed parameters. ViewTemplate/ViewTemplateSelector or DocumentType should be set.";
        internal const string HelpLink_CreateViewMissArguments = "https://documentation.devexpress.com/#WPF/CustomDocument17469";
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ViewProperty = DependencyProperty.RegisterAttached("View", typeof(FrameworkElement), typeof(ViewHelper), new PropertyMetadata(null));

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method is obsolete. Use other method overloads.")]
        public static object CreateAndInitializeView(IViewLocator viewLocator, string documentType, object parameter, object parentViewModel = null, bool useParameterAsViewModel = false, DataTemplate viewTemplate = null, DataTemplateSelector viewTemplateSelector = null) => 
            !useParameterAsViewModel ? CreateAndInitializeView(viewLocator, documentType, null, parameter, parentViewModel, viewTemplate, viewTemplateSelector) : CreateAndInitializeView(viewLocator, documentType, parameter, parameter, parentViewModel, viewTemplate, viewTemplateSelector);

        public static object CreateAndInitializeView(IViewLocator viewLocator, string documentType, object viewModel, object parameter, object parentViewModel, DataTemplate viewTemplate = null, DataTemplateSelector viewTemplateSelector = null) => 
            CreateAndInitializeView(viewLocator, documentType, viewModel, parameter, parentViewModel, null, viewTemplate, viewTemplateSelector);

        public static object CreateAndInitializeView(IViewLocator viewLocator, string documentType, object viewModel, object parameter, object parentViewModel, IDocumentOwner documentOwner, DataTemplate viewTemplate = null, DataTemplateSelector viewTemplateSelector = null)
        {
            object view = CreateView(viewLocator, documentType, viewTemplate, viewTemplateSelector);
            InitializeView(view, viewModel, parameter, parentViewModel, documentOwner);
            return view;
        }

        public static object CreateView(IViewLocator viewLocator, string documentType, DataTemplate viewTemplate = null, DataTemplateSelector viewTemplateSelector = null)
        {
            if ((documentType == null) && ((viewTemplate == null) && (viewTemplateSelector == null)))
            {
                InvalidOperationException exception = new InvalidOperationException($"{"It is impossible to create a view based on passed parameters. ViewTemplate/ViewTemplateSelector or DocumentType should be set."}{Environment.NewLine}To learn more, see: {"https://documentation.devexpress.com/#WPF/CustomDocument17469"}") {
                    HelpLink = "https://documentation.devexpress.com/#WPF/CustomDocument17469"
                };
                throw exception;
            }
            if ((viewTemplate != null) || (viewTemplateSelector != null))
            {
                return new ViewPresenter(viewTemplate, viewTemplateSelector);
            }
            IViewLocator instance = viewLocator;
            if (viewLocator == null)
            {
                IViewLocator local1 = viewLocator;
                IViewLocator locator1 = ViewLocator.Default;
                instance = locator1;
                if (locator1 == null)
                {
                    IViewLocator local2 = locator1;
                    instance = ViewLocator.Instance;
                }
            }
            return instance.ResolveView(documentType);
        }

        public static object GetViewModelFromView(object view)
        {
            Func<FrameworkElement, object> evaluator = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<FrameworkElement, object> local1 = <>c.<>9__7_0;
                evaluator = <>c.<>9__7_0 = x => x.DataContext;
            }
            object local2 = (view as FrameworkElement).Return<FrameworkElement, object>(evaluator, null);
            object local5 = local2;
            if (local2 == null)
            {
                object local3 = local2;
                local5 = (view as FrameworkContentElement).Return<FrameworkContentElement, object>(<>c.<>9__7_1 ??= x => x.DataContext, null);
            }
            return local5;
        }

        public static void InitializeView(object view, object viewModel, object parameter, object parentViewModel, IDocumentOwner documentOwner = null)
        {
            if (documentOwner is DependencyObject)
            {
                documentOwner = DocumentOwnerWrapper.Create(documentOwner);
            }
            if (viewModel == null)
            {
                if (view is DependencyObject)
                {
                    ViewModelInitializer.SetViewModelProperties((DependencyObject) view, parameter, parentViewModel, documentOwner);
                }
            }
            else
            {
                ViewModelInitializer.SetViewModelProperties(viewModel, parameter, parentViewModel, documentOwner);
                Func<object, FrameworkElement> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<object, FrameworkElement> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x as FrameworkElement;
                }
                view.With<object, FrameworkElement>(evaluator).Do<FrameworkElement>(x => x.DataContext = viewModel);
                Func<object, FrameworkContentElement> func2 = <>c.<>9__5_2;
                if (<>c.<>9__5_2 == null)
                {
                    Func<object, FrameworkContentElement> local2 = <>c.<>9__5_2;
                    func2 = <>c.<>9__5_2 = x => x as FrameworkContentElement;
                }
                view.With<object, FrameworkContentElement>(func2).Do<FrameworkContentElement>(x => x.DataContext = viewModel);
                Func<object, ContentPresenter> func3 = <>c.<>9__5_4;
                if (<>c.<>9__5_4 == null)
                {
                    Func<object, ContentPresenter> local3 = <>c.<>9__5_4;
                    func3 = <>c.<>9__5_4 = x => x as ContentPresenter;
                }
                view.With<object, ContentPresenter>(func3).Do<ContentPresenter>(x => x.Content = viewModel);
            }
        }

        public static void SetBindingToViewModel(DependencyObject target, DependencyProperty targetProperty, PropertyPath viewPropertyPath)
        {
            Binding binding1 = new Binding();
            binding1.Path = viewPropertyPath;
            binding1.Source = target;
            binding1.Mode = BindingMode.OneWay;
            binding1.Converter = new AsFrameworkElementConverter();
            BindingOperations.SetBinding(target, ViewProperty, binding1);
            Binding binding = new Binding();
            object[] pathParameters = new object[] { ViewProperty, FrameworkElement.DataContextProperty };
            binding.Path = new PropertyPath("(0).(1)", pathParameters);
            binding.Source = target;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(target, targetProperty, binding);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewHelper.<>c <>9 = new ViewHelper.<>c();
            public static Func<object, FrameworkElement> <>9__5_0;
            public static Func<object, FrameworkContentElement> <>9__5_2;
            public static Func<object, ContentPresenter> <>9__5_4;
            public static Func<FrameworkElement, object> <>9__7_0;
            public static Func<FrameworkContentElement, object> <>9__7_1;

            internal object <GetViewModelFromView>b__7_0(FrameworkElement x) => 
                x.DataContext;

            internal object <GetViewModelFromView>b__7_1(FrameworkContentElement x) => 
                x.DataContext;

            internal FrameworkElement <InitializeView>b__5_0(object x) => 
                x as FrameworkElement;

            internal FrameworkContentElement <InitializeView>b__5_2(object x) => 
                x as FrameworkContentElement;

            internal ContentPresenter <InitializeView>b__5_4(object x) => 
                x as ContentPresenter;
        }

        private class AsFrameworkElementConverter : IValueConverter
        {
            object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
                value as FrameworkElement;

            object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }

        public sealed class DocumentOwnerWrapper : IDocumentOwner
        {
            private readonly IDocumentOwner actualOwner;

            private DocumentOwnerWrapper(IDocumentOwner actualOwner)
            {
                this.actualOwner = actualOwner;
            }

            public void Close(IDocumentContent documentContent, bool force = true)
            {
                this.ActualOwner.Close(documentContent, force);
            }

            public static ViewHelper.DocumentOwnerWrapper Create(IDocumentOwner owner) => 
                (owner != null) ? ((owner as ViewHelper.DocumentOwnerWrapper) ?? new ViewHelper.DocumentOwnerWrapper(owner)) : null;

            public override bool Equals(object obj) => 
                !(obj is ViewHelper.DocumentOwnerWrapper) ? ((obj is IDocumentOwner) && this.ActualOwner.Equals(obj)) : this.ActualOwner.Equals((obj as ViewHelper.DocumentOwnerWrapper).ActualOwner);

            public override int GetHashCode() => 
                this.actualOwner.GetHashCode();

            public IDocumentOwner ActualOwner =>
                this.actualOwner;
        }

        private class ViewPresenter : ContentPresenter
        {
            public ViewPresenter(DataTemplate viewTemplate, DataTemplateSelector viewTemplateSelector)
            {
                base.ContentTemplate = viewTemplate;
                base.ContentTemplateSelector = viewTemplateSelector;
                base.Loaded += new RoutedEventHandler(this.ViewPresenter_Loaded);
            }

            private void ViewPresenter_Loaded(object sender, RoutedEventArgs e)
            {
                if (VisualTreeHelper.GetChildrenCount(this) != 0)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(this, 0);
                    object parameter = ViewModelExtensions.GetParameter(this);
                    object parentViewModel = ViewModelExtensions.GetParentViewModel(this);
                    IDocumentOwner documentOwner = ViewModelExtensions.GetDocumentOwner(this);
                    if (parameter != null)
                    {
                        ViewModelExtensions.SetParameter(child, parameter);
                    }
                    if (parentViewModel != null)
                    {
                        ViewModelExtensions.SetParentViewModel(child, parentViewModel);
                    }
                    if (documentOwner != null)
                    {
                        ViewModelExtensions.SetDocumentOwner(child, documentOwner);
                    }
                }
            }
        }
    }
}

