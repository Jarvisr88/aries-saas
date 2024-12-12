namespace DevExpress.Mvvm.UI
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class DocumentUIServiceBase : ViewServiceBase
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TitleListenProperty;
        public static readonly DependencyProperty DocumentProperty;

        static DocumentUIServiceBase()
        {
            TitleListenProperty = DependencyProperty.RegisterAttached("DocumentTitleListen", typeof(object), typeof(DocumentUIServiceBase), new PropertyMetadata(null, (d, e) => ViewModelExtensions.SetDocumentTitle(d, e.NewValue)));
            DocumentProperty = DependencyProperty.RegisterAttached("Document", typeof(IDocument), typeof(DocumentUIServiceBase), new PropertyMetadata(null));
        }

        protected DocumentUIServiceBase()
        {
        }

        public static void CheckDocumentAccess(bool isDocumentAlive)
        {
            if (!isDocumentAlive)
            {
                throw new InvalidOperationException("Cannot access the destroyed document.");
            }
        }

        public static void ClearTitleBinding(DependencyProperty property, DependencyObject target)
        {
            BindingOperations.ClearBinding(target, TitleListenProperty);
            BindingOperations.ClearBinding(target, property);
        }

        public static void CloseDocument(IDocumentManagerService documentManagerService, IDocumentContent documentContent, bool force)
        {
            IDocument document = documentManagerService.FindDocument(documentContent);
            if (document != null)
            {
                document.Close(force);
            }
        }

        public static IDocument GetDocument(DependencyObject obj) => 
            (IDocument) obj.GetValue(DocumentProperty);

        public static void SetDocument(DependencyObject obj, IDocument value)
        {
            obj.SetValue(DocumentProperty, value);
        }

        public static void SetTitleBinding(object documentContentView, DependencyProperty property, DependencyObject target, bool convertToString = false)
        {
            Binding binding1 = new Binding();
            binding1.Source = target;
            binding1.Path = new PropertyPath(property);
            binding1.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(target, TitleListenProperty, binding1);
            object viewModelFromView = ViewHelper.GetViewModelFromView(documentContentView);
            if (DocumentViewModelHelper.IsDocumentContentOrDocumentViewModel(viewModelFromView))
            {
                if (!DocumentViewModelHelper.TitlePropertyHasImplicitImplementation(viewModelFromView))
                {
                    new TitleUpdater(convertToString, viewModelFromView, target, property).Update(target, viewModelFromView);
                }
                else
                {
                    Binding binding = new Binding {
                        Path = new PropertyPath("Title", new object[0]),
                        Source = viewModelFromView
                    };
                    if (convertToString)
                    {
                        binding.Converter = new ObjectToStringConverter();
                    }
                    BindingOperations.SetBinding(target, property, binding);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentUIServiceBase.<>c <>9 = new DocumentUIServiceBase.<>c();

            internal void <.cctor>b__11_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ViewModelExtensions.SetDocumentTitle(d, e.NewValue);
            }
        }

        private class ObjectToStringConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
                (value == null) ? string.Empty : value.ToString();

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }

        private class TitleUpdater : IDisposable
        {
            private static readonly DependencyProperty TitleUpdaterInternalProperty = DependencyProperty.RegisterAttached("TitleUpdaterInternal", typeof(DocumentUIServiceBase.TitleUpdater), typeof(DocumentUIServiceBase.TitleUpdater), new PropertyMetadata(null));
            private bool convertToString;
            private DependencyProperty targetProperty;
            private PropertyChangedWeakEventHandler<DependencyObject> updateHandler;
            private WeakReference documentContentRef;

            public TitleUpdater(bool convertToString, object documentContentOrDocumentViewModel, DependencyObject target, DependencyProperty targetProperty)
            {
                DocumentUIServiceBase.TitleUpdater updater = (DocumentUIServiceBase.TitleUpdater) target.GetValue(TitleUpdaterInternalProperty);
                if (updater != null)
                {
                    updater.Dispose();
                }
                this.convertToString = convertToString;
                target.SetValue(TitleUpdaterInternalProperty, this);
                this.targetProperty = targetProperty;
                this.updateHandler = new PropertyChangedWeakEventHandler<DependencyObject>(target, new Action<DependencyObject, object, PropertyChangedEventArgs>(DocumentUIServiceBase.TitleUpdater.OnDocumentViewModelPropertyChanged));
                INotifyPropertyChanged changed = documentContentOrDocumentViewModel as INotifyPropertyChanged;
                this.DocumentContent = changed;
                if (changed != null)
                {
                    changed.PropertyChanged += this.updateHandler.Handler;
                }
            }

            public void Dispose()
            {
                INotifyPropertyChanged documentContent = this.DocumentContent;
                if (documentContent != null)
                {
                    documentContent.PropertyChanged -= this.updateHandler.Handler;
                }
                this.updateHandler = null;
                this.DocumentContent = null;
            }

            private static void OnDocumentViewModelPropertyChanged(DependencyObject target, object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Title")
                {
                    ((DocumentUIServiceBase.TitleUpdater) target.GetValue(TitleUpdaterInternalProperty)).Update(target, sender);
                }
            }

            public void Update(DependencyObject target, object documentContentOrDocumentViewModel)
            {
                object title = DocumentViewModelHelper.GetTitle(documentContentOrDocumentViewModel);
                if (this.convertToString)
                {
                    title = (title == null) ? string.Empty : title.ToString();
                }
                target.SetValue(this.targetProperty, title);
            }

            private INotifyPropertyChanged DocumentContent
            {
                get => 
                    (this.documentContentRef == null) ? null : ((INotifyPropertyChanged) this.documentContentRef.Target);
                set => 
                    this.documentContentRef = (value == null) ? null : new WeakReference(value);
            }
        }
    }
}

