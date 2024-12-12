namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Rendering;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ThumbnailsViewerControl : Control, IDocumentViewerControl
    {
        public static readonly DependencyProperty SettingsProperty;
        public static readonly DependencyProperty SelectedPageNumberProperty;
        public static readonly DependencyProperty FilteredPageIndicesProperty;

        static ThumbnailsViewerControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ThumbnailsViewerControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<ThumbnailsViewerControl> registrator1 = DependencyPropertyRegistrator<ThumbnailsViewerControl>.New().Register<ThumbnailsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<ThumbnailsViewerControl, ThumbnailsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ThumbnailsViewerControl.get_Settings)), parameters), out SettingsProperty, null, (d, o, n) => d.OnSettingsChanged(o, n), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ThumbnailsViewerControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<ThumbnailsViewerControl> registrator2 = registrator1.Register<ObservableCollection<int>>(System.Linq.Expressions.Expression.Lambda<Func<ThumbnailsViewerControl, ObservableCollection<int>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ThumbnailsViewerControl.get_FilteredPageIndices)), expressionArray2), out FilteredPageIndicesProperty, null, (d, o, n) => d.OnFilteredPagesChanged(o, n), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ThumbnailsViewerControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<int>(System.Linq.Expressions.Expression.Lambda<Func<ThumbnailsViewerControl, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ThumbnailsViewerControl.get_SelectedPageNumber)), expressionArray3), out SelectedPageNumberProperty, 0, d => d.OnSelectedPageNumberChanged(), frameworkOptions).OverrideDefaultStyleKey();
        }

        public ThumbnailsViewerControl()
        {
            DocumentViewerControl.SetActualViewer(this, this);
            this.UndoRedoManager = new DevExpress.Xpf.DocumentViewer.UndoRedoManager(base.Dispatcher);
            this.ActualCommandProvider = new DocumentCommandProvider();
            BehaviorProvider provider1 = new BehaviorProvider();
            provider1.ZoomMode = ZoomMode.ActualSize;
            this.ActualBehaviorProvider = provider1;
            this.ZoomFactor = this.ActualBehaviorProvider.ZoomFactor;
            this.ActualBehaviorProvider.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.ActualBehaviorProvider_ZoomChanged);
            this.HorizontalPageSpacing = 5.0;
            this.VerticalPageSpacing = 26.0;
        }

        private void ActualBehaviorProvider_ZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            this.ZoomFactor = e.ZoomFactor;
        }

        protected virtual void AssignDocumentPresenterProperties()
        {
            this.DocumentPresenter.Do<DevExpress.Xpf.DocumentViewer.DocumentPresenterControl>(delegate (DevExpress.Xpf.DocumentViewer.DocumentPresenterControl x) {
                x.Document = this.Document;
                x.BehaviorProvider = this.ActualBehaviorProvider;
                x.HorizontalPageSpacing = this.HorizontalPageSpacing;
                x.VerticalPageSpacing = this.VerticalPageSpacing;
            });
        }

        protected virtual ThumbnailsViewerSettings CreateDefaultThumbnailsViewerSettings() => 
            new ThumbnailsViewerSettings();

        protected virtual void DetachDocumentPresenterControl()
        {
            Action<DevExpress.Xpf.DocumentViewer.DocumentPresenterControl> action = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Action<DevExpress.Xpf.DocumentViewer.DocumentPresenterControl> local1 = <>c.<>9__54_0;
                action = <>c.<>9__54_0 = delegate (DevExpress.Xpf.DocumentViewer.DocumentPresenterControl x) {
                    x.BehaviorProvider = null;
                    x.Document = null;
                };
            }
            this.DocumentPresenter.Do<DevExpress.Xpf.DocumentViewer.DocumentPresenterControl>(action);
        }

        void IDocumentViewerControl.AttachDocumentPresenterControl(DevExpress.Xpf.DocumentViewer.DocumentPresenterControl presenter)
        {
            if (this.DocumentPresenter != null)
            {
                this.DetachDocumentPresenterControl();
            }
            this.DocumentPresenter = presenter;
            this.AssignDocumentPresenterProperties();
        }

        private void OnFilteredPagesChanged(ObservableCollection<int> oldPages, ObservableCollection<int> newPages)
        {
            oldPages.Do<ObservableCollection<int>>(delegate (ObservableCollection<int> x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnFilteredPagesCollectionChanged);
            });
            Action<ThumbnailsPresenterControl> action = <>c.<>9__47_1;
            if (<>c.<>9__47_1 == null)
            {
                Action<ThumbnailsPresenterControl> local1 = <>c.<>9__47_1;
                action = <>c.<>9__47_1 = x => x.UpdatePagesInternal();
            }
            this.ThumbnailsPresenter.Do<ThumbnailsPresenterControl>(action);
            newPages.Do<ObservableCollection<int>>(delegate (ObservableCollection<int> x) {
                x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnFilteredPagesCollectionChanged);
            });
        }

        private void OnFilteredPagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Action<ThumbnailsPresenterControl> action = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Action<ThumbnailsPresenterControl> local1 = <>c.<>9__48_0;
                action = <>c.<>9__48_0 = x => x.UpdatePagesInternal();
            }
            this.ThumbnailsPresenter.Do<ThumbnailsPresenterControl>(action);
        }

        private void OnInvalidateTextureCache(object sender, EventArgs e)
        {
            this.TextureCache.Reset();
        }

        private void OnInvalidateThumbnails(object sender, EventArgs e)
        {
            if (this.Settings == null)
            {
                this.Document = null;
            }
            else
            {
                this.Document = this.Settings.ThumbnailsDocument;
                this.AssignDocumentPresenterProperties();
            }
        }

        private void OnSelectedPageNumberChanged()
        {
            this.Document.Do<ThumbnailsDocumentViewModel>(x => x.SetCurrentPage(this.SelectedPageNumber - 1));
            if (this.SelectedPageNumber >= 1)
            {
                this.DocumentPresenter.Do<DevExpress.Xpf.DocumentViewer.DocumentPresenterControl>(delegate (DevExpress.Xpf.DocumentViewer.DocumentPresenterControl x) {
                    int pageWrapperIndex = x.NavigationStrategy.PositionCalculator.GetPageWrapperIndex(this.SelectedPageNumber - 1);
                    x.NavigationStrategy.ScrollIntoView(pageWrapperIndex, Rect.Empty, ScrollIntoViewMode.Edge);
                });
            }
        }

        protected virtual void OnSettingsChanged(ThumbnailsViewerSettings oldValue, ThumbnailsViewerSettings newValue)
        {
            oldValue.Do<ThumbnailsViewerSettings>(delegate (ThumbnailsViewerSettings x) {
                x.InvalidateTextureCache -= new EventHandler(this.OnInvalidateTextureCache);
            });
            newValue.Do<ThumbnailsViewerSettings>(delegate (ThumbnailsViewerSettings x) {
                x.InvalidateTextureCache += new EventHandler(this.OnInvalidateTextureCache);
            });
            oldValue.Do<ThumbnailsViewerSettings>(delegate (ThumbnailsViewerSettings x) {
                x.Invalidate -= new EventHandler(this.OnInvalidateThumbnails);
            });
            newValue.Do<ThumbnailsViewerSettings>(delegate (ThumbnailsViewerSettings x) {
                x.Invalidate += new EventHandler(this.OnInvalidateThumbnails);
            });
            Func<ThumbnailsViewerSettings, ThumbnailsDocumentViewModel> evaluator = <>c.<>9__55_4;
            if (<>c.<>9__55_4 == null)
            {
                Func<ThumbnailsViewerSettings, ThumbnailsDocumentViewModel> local1 = <>c.<>9__55_4;
                evaluator = <>c.<>9__55_4 = x => x.ThumbnailsDocument;
            }
            this.Document = this.Settings.With<ThumbnailsViewerSettings, ThumbnailsDocumentViewModel>(evaluator);
            this.TextureCache = new DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.TextureCache();
        }

        private ThumbnailsPresenterControl ThumbnailsPresenter =>
            (ThumbnailsPresenterControl) this.DocumentPresenter;

        protected internal ThumbnailsDocumentViewModel Document { get; private set; }

        protected DevExpress.Xpf.DocumentViewer.DocumentPresenterControl DocumentPresenter { get; private set; }

        public BehaviorProvider ActualBehaviorProvider { get; private set; }

        public CommandProvider ActualCommandProvider { get; private set; }

        public double HorizontalPageSpacing { get; private set; }

        public double VerticalPageSpacing { get; private set; }

        public DevExpress.Xpf.DocumentViewer.UndoRedoManager UndoRedoManager { get; private set; }

        public double ZoomFactor { get; private set; }

        public ThumbnailsViewerSettings Settings
        {
            get => 
                (ThumbnailsViewerSettings) base.GetValue(SettingsProperty);
            set => 
                base.SetValue(SettingsProperty, value);
        }

        public int SelectedPageNumber
        {
            get => 
                (int) base.GetValue(SelectedPageNumberProperty);
            set => 
                base.SetValue(SelectedPageNumberProperty, value);
        }

        public ObservableCollection<int> FilteredPageIndices
        {
            get => 
                (ObservableCollection<int>) base.GetValue(FilteredPageIndicesProperty);
            set => 
                base.SetValue(FilteredPageIndicesProperty, value);
        }

        internal DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.TextureCache TextureCache { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsViewerControl.<>c <>9 = new ThumbnailsViewerControl.<>c();
            public static Action<ThumbnailsPresenterControl> <>9__47_1;
            public static Action<ThumbnailsPresenterControl> <>9__48_0;
            public static Action<DevExpress.Xpf.DocumentViewer.DocumentPresenterControl> <>9__54_0;
            public static Func<ThumbnailsViewerSettings, ThumbnailsDocumentViewModel> <>9__55_4;

            internal void <.cctor>b__46_0(ThumbnailsViewerControl d, ThumbnailsViewerSettings o, ThumbnailsViewerSettings n)
            {
                d.OnSettingsChanged(o, n);
            }

            internal void <.cctor>b__46_1(ThumbnailsViewerControl d, ObservableCollection<int> o, ObservableCollection<int> n)
            {
                d.OnFilteredPagesChanged(o, n);
            }

            internal void <.cctor>b__46_2(ThumbnailsViewerControl d)
            {
                d.OnSelectedPageNumberChanged();
            }

            internal void <DetachDocumentPresenterControl>b__54_0(DevExpress.Xpf.DocumentViewer.DocumentPresenterControl x)
            {
                x.BehaviorProvider = null;
                x.Document = null;
            }

            internal void <OnFilteredPagesChanged>b__47_1(ThumbnailsPresenterControl x)
            {
                x.UpdatePagesInternal();
            }

            internal void <OnFilteredPagesCollectionChanged>b__48_0(ThumbnailsPresenterControl x)
            {
                x.UpdatePagesInternal();
            }

            internal ThumbnailsDocumentViewModel <OnSettingsChanged>b__55_4(ThumbnailsViewerSettings x) => 
                x.ThumbnailsDocument;
        }
    }
}

