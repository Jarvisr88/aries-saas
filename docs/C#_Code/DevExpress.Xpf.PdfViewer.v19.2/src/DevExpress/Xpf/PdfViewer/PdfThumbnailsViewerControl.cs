namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class PdfThumbnailsViewerControl : Control, IDocumentViewerControl, IVisualOwner, IInputElement, ILogicalOwner
    {
        public static readonly DependencyProperty SettingsProperty;
        private static readonly DependencyPropertyKey ActualSettingsPropertyKey;
        public static readonly DependencyProperty ActualSettingsProperty;
        public static readonly DependencyProperty SelectedPageNumberProperty;
        public static readonly DependencyProperty HighlightedPageNumbersProperty;
        public static readonly DependencyProperty ZoomFactorProperty;
        public static readonly DependencyProperty PageRotationProperty;
        private DockLayoutManager dockLayoutManager;

        event RoutedEventHandler ILogicalOwner.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        static PdfThumbnailsViewerControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            SettingsProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerControl.get_Settings)), parameters), null, (control, oldValue, newValue) => control.SettingsChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ActualSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerControl, PdfThumbnailsViewerSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerControl.get_ActualSettings)), expressionArray2), null, (control, oldValue, newValue) => control.ActualSettingsChanged(oldValue, newValue));
            ActualSettingsProperty = ActualSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            SelectedPageNumberProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerControl, int>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerControl, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerControl.get_SelectedPageNumber)), expressionArray3), 0, (control, oldValue, newValue) => control.SelectedPageNumberChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            ZoomFactorProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerControl, double>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerControl.get_ZoomFactor)), expressionArray4), 1.0, (control, oldValue, newValue) => control.ZoomFactorChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerControl), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            HighlightedPageNumbersProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerControl, ObservableCollection<int>>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerControl, ObservableCollection<int>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerControl.get_HighlightedPageNumbers)), expressionArray5), null, (control, oldValue, newValue) => control.HighlightedPageNumbersChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfThumbnailsViewerControl), "owner");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            PageRotationProperty = DependencyPropertyRegistrator.Register<PdfThumbnailsViewerControl, Rotation>(System.Linq.Expressions.Expression.Lambda<Func<PdfThumbnailsViewerControl, Rotation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfThumbnailsViewerControl.get_PageRotation)), expressionArray6), Rotation.Rotate0, (control, oldValue, newValue) => control.PageRotationChanged(oldValue, newValue));
        }

        public PdfThumbnailsViewerControl()
        {
            DocumentViewerControl.SetActualViewer(this, this);
            this.SetDefaultStyleKey(typeof(PdfThumbnailsViewerControl));
            this.VCContainer = new VisualChildrenContainer(this, this);
            this.LCContainer = new LogicalChildrenContainer(this);
            this.ActualSettings = this.CreateDefaultThumbnailsViewerSettings();
            this.UndoRedoManager = new DevExpress.Xpf.DocumentViewer.UndoRedoManager(base.Dispatcher);
            this.ActualCommandProvider = new ThumbnailsCommandProvider();
            this.ActualCommandProvider.DocumentViewer = this;
            this.ActualBehaviorProvider = new ThumbnailsBehaviorProvider();
            this.ActualBehaviorProvider.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.BehaviorProviderZoomChanged);
            this.HighlightedPageNumbers = new ObservableCollection<int>();
            this.HorizontalPageSpacing = 5.0;
            this.VerticalPageSpacing = 26.0;
            this.InitializeCommands();
        }

        protected virtual void ActualSettingsChanged(PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
        {
            if (oldValue != null)
            {
                this.LCContainer.RemoveLogicalChild(oldValue);
                this.VCContainer.RemoveChild(oldValue);
            }
            oldValue.Do<PdfThumbnailsViewerSettings>(new Action<PdfThumbnailsViewerSettings>(this.DetachThumbnailsViewerSettings));
            newValue.Do<PdfThumbnailsViewerSettings>(new Action<PdfThumbnailsViewerSettings>(this.AttachThumbnailsViewerSettings));
            if ((newValue != null) && (newValue.GetVisualParent() == null))
            {
                this.VCContainer.AddChild(newValue);
                this.LCContainer.AddLogicalChild(newValue);
            }
        }

        protected virtual void AssignDocumentPresenterProperties()
        {
            this.DocumentPresenter.Do<DocumentPresenterControl>(delegate (DocumentPresenterControl x) {
                x.Document = this.Document;
                x.BehaviorProvider = this.ActualBehaviorProvider;
                x.HorizontalPageSpacing = this.HorizontalPageSpacing;
                x.VerticalPageSpacing = this.VerticalPageSpacing;
            });
        }

        protected virtual void AttachDocumentPresenterControl(DocumentPresenterControl documentPresenter)
        {
            if (this.DocumentPresenter != null)
            {
                this.DetachDocumentPresenterControl();
            }
            this.DocumentPresenter = documentPresenter;
            this.AssignDocumentPresenterProperties();
        }

        protected virtual void AttachThumbnailsViewerSettings(PdfThumbnailsViewerSettings settings)
        {
            settings.Invalidate += new EventHandler(this.InvalidateSettings);
            this.LoadThumbnails();
        }

        private void BehaviorProviderZoomChanged(object sender, ZoomChangedEventArgs args)
        {
            this.ZoomFactor = args.ZoomFactor;
        }

        protected virtual bool CanSetZoomFactor(double value) => 
            true;

        protected virtual PdfThumbnailsViewerSettings CreateDefaultThumbnailsViewerSettings() => 
            new PdfThumbnailsViewerSettings();

        protected virtual void DetachDocumentPresenterControl()
        {
            Action<DocumentPresenterControl> action = <>c.<>9__82_0;
            if (<>c.<>9__82_0 == null)
            {
                Action<DocumentPresenterControl> local1 = <>c.<>9__82_0;
                action = <>c.<>9__82_0 = delegate (DocumentPresenterControl x) {
                    x.BehaviorProvider = null;
                    x.Document = null;
                };
            }
            this.DocumentPresenter.Do<DocumentPresenterControl>(action);
        }

        protected virtual void DetachThumbnailsViewerSettings(PdfThumbnailsViewerSettings settings)
        {
            settings.Invalidate -= new EventHandler(this.InvalidateSettings);
        }

        void ILogicalOwner.AddChild(object child)
        {
            base.AddLogicalChild(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            base.RemoveLogicalChild(child);
        }

        void IVisualOwner.AddChild(Visual child)
        {
            base.AddVisualChild(child);
        }

        void IVisualOwner.RemoveChild(Visual child)
        {
            base.RemoveVisualChild(child);
        }

        void IDocumentViewerControl.AttachDocumentPresenterControl(DocumentPresenterControl presenter)
        {
            this.AttachDocumentPresenterControl(presenter);
        }

        private void DockLayoutManagerOnDockItemExpanded(object sender, DockItemExpandedEventArgs args)
        {
            Func<AutoHideGroup, BaseLayoutItem> evaluator = <>c.<>9__102_0;
            if (<>c.<>9__102_0 == null)
            {
                Func<AutoHideGroup, BaseLayoutItem> local1 = <>c.<>9__102_0;
                evaluator = <>c.<>9__102_0 = x => x.SelectedItem;
            }
            Func<BaseLayoutItem, bool> func2 = <>c.<>9__102_1;
            if (<>c.<>9__102_1 == null)
            {
                Func<BaseLayoutItem, bool> local2 = <>c.<>9__102_1;
                func2 = <>c.<>9__102_1 = x => x.Name.Equals("thumbnailsViewerPanel");
            }
            if ((args.Item as AutoHideGroup).With<AutoHideGroup, BaseLayoutItem>(evaluator).Return<BaseLayoutItem, bool>(func2, <>c.<>9__102_2 ??= () => false))
            {
                ((ThumbnailsViewerPresenterControl) this.DocumentPresenter).InvalidateRender();
            }
        }

        protected virtual void HighlightedPageNumbersChanged(ObservableCollection<int> oldValue, ObservableCollection<int> newValue)
        {
            oldValue.Do<INotifyCollectionChanged>(delegate (INotifyCollectionChanged x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnHighlightedPageNumbersCollectionChanged);
            });
            newValue.Do<INotifyCollectionChanged>(delegate (INotifyCollectionChanged x) {
                x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnHighlightedPageNumbersCollectionChanged);
            });
            (this.Document as ThumbnailsDocumentViewModel).Do<ThumbnailsDocumentViewModel>(x => x.SetHighlightedPages(newValue));
        }

        protected virtual void InitializeCommands()
        {
            this.PrintPagesCommand = DelegateCommandFactory.Create(new Action(this.PrintPages));
            this.SetZoomFactorCommand = DelegateCommandFactory.Create<double>(new Action<double>(this.SetZoomFactor), new Func<double, bool>(this.CanSetZoomFactor));
            this.ZoomInCommand = DelegateCommandFactory.Create(new Action(this.ActualBehaviorProvider.ZoomIn), new Func<bool>(this.ActualBehaviorProvider.CanZoomIn));
            this.ZoomOutCommand = DelegateCommandFactory.Create(new Action(this.ActualBehaviorProvider.ZoomOut), new Func<bool>(this.ActualBehaviorProvider.CanZoomOut));
        }

        private void InvalidateSettings(object sender, EventArgs eventArgs)
        {
            this.LoadThumbnails();
            this.ActualSettings.UpdateProperties();
            Action<ThumbnailsViewerPresenterControl> action = <>c.<>9__97_0;
            if (<>c.<>9__97_0 == null)
            {
                Action<ThumbnailsViewerPresenterControl> local1 = <>c.<>9__97_0;
                action = <>c.<>9__97_0 = x => x.InvalidateRender();
            }
            (this.DocumentPresenter as ThumbnailsViewerPresenterControl).Do<ThumbnailsViewerPresenterControl>(action);
            Action<ThumbnailsViewerPresenterControl> action2 = <>c.<>9__97_1;
            if (<>c.<>9__97_1 == null)
            {
                Action<ThumbnailsViewerPresenterControl> local2 = <>c.<>9__97_1;
                action2 = <>c.<>9__97_1 = x => x.UpdatePagesInternal();
            }
            (this.DocumentPresenter as ThumbnailsViewerPresenterControl).Do<ThumbnailsViewerPresenterControl>(action2);
        }

        private void LoadThumbnails()
        {
            if (this.ActualSettings == null)
            {
                this.Document = null;
            }
            else
            {
                ThumbnailsDocumentViewModel model = (ThumbnailsDocumentViewModel) this.ActualSettings.CreateThumbnailsDocument();
                model.Initialize(this.ActualSettings.Document);
                this.Document = model;
                Action<ThumbnailsViewerPresenterControl> action = <>c.<>9__98_0;
                if (<>c.<>9__98_0 == null)
                {
                    Action<ThumbnailsViewerPresenterControl> local1 = <>c.<>9__98_0;
                    action = <>c.<>9__98_0 = x => x.UpdateNativeRendererInternal();
                }
                (this.DocumentPresenter as ThumbnailsViewerPresenterControl).Do<ThumbnailsViewerPresenterControl>(action);
                this.AssignDocumentPresenterProperties();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.dockLayoutManager.Do<DockLayoutManager>(delegate (DockLayoutManager x) {
                x.DockItemExpanded -= new DockItemExpandedEventHandler(this.DockLayoutManagerOnDockItemExpanded);
            });
            this.dockLayoutManager = LayoutHelper.FindParentObject<DockLayoutManager>(this);
            this.dockLayoutManager.Do<DockLayoutManager>(delegate (DockLayoutManager x) {
                x.DockItemExpanded += new DockItemExpandedEventHandler(this.DockLayoutManagerOnDockItemExpanded);
            });
        }

        protected virtual void OnHighlightedPageNumbersCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            (this.Document as ThumbnailsDocumentViewModel).SetHighlightedPages(this.HighlightedPageNumbers);
        }

        protected virtual void PageRotationChanged(Rotation oldValue, Rotation newValue)
        {
            this.ActualBehaviorProvider.Do<BehaviorProvider>(x => x.RotateAngle = (double) (((Rotation) 90) * newValue));
        }

        protected virtual void PrintPages()
        {
            Func<PdfThumbnailsViewerSettings, PdfViewerControl> evaluator = <>c.<>9__86_0;
            if (<>c.<>9__86_0 == null)
            {
                Func<PdfThumbnailsViewerSettings, PdfViewerControl> local1 = <>c.<>9__86_0;
                evaluator = <>c.<>9__86_0 = x => x.Owner;
            }
            this.ActualSettings.With<PdfThumbnailsViewerSettings, PdfViewerControl>(evaluator).Do<PdfViewerControl>(x => x.Print((this.HighlightedPageNumbers.Count > 0) ? this.HighlightedPageNumbers : null));
        }

        protected virtual void SelectedPageNumberChanged(int oldValue, int newValue)
        {
            this.ActualBehaviorProvider.PageIndex = newValue - 1;
            (this.Document as ThumbnailsDocumentViewModel).SetCurrentPage(newValue - 1);
        }

        protected virtual void SettingsChanged(PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
        {
            this.ActualSettings = newValue;
        }

        protected virtual void SetZoomFactor(double value)
        {
            this.ZoomFactor = value;
        }

        protected virtual void ZoomFactorChanged(double oldValue, double newValue)
        {
            this.ActualBehaviorProvider.Do<BehaviorProvider>(x => x.ZoomFactor = newValue);
        }

        public PdfThumbnailsViewerSettings ActualSettings
        {
            get => 
                (PdfThumbnailsViewerSettings) base.GetValue(ActualSettingsProperty);
            private set => 
                base.SetValue(ActualSettingsPropertyKey, value);
        }

        public PdfThumbnailsViewerSettings Settings
        {
            get => 
                (PdfThumbnailsViewerSettings) base.GetValue(SettingsProperty);
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

        public ObservableCollection<int> HighlightedPageNumbers
        {
            get => 
                (ObservableCollection<int>) base.GetValue(HighlightedPageNumbersProperty);
            set => 
                base.SetValue(HighlightedPageNumbersProperty, value);
        }

        public double ZoomFactor
        {
            get => 
                (double) base.GetValue(ZoomFactorProperty);
            set => 
                base.SetValue(ZoomFactorProperty, value);
        }

        public Rotation PageRotation
        {
            get => 
                (Rotation) base.GetValue(PageRotationProperty);
            set => 
                base.SetValue(PageRotationProperty, value);
        }

        protected internal IDocument Document { get; private set; }

        private VisualChildrenContainer VCContainer { get; set; }

        private LogicalChildrenContainer LCContainer { get; set; }

        protected DocumentPresenterControl DocumentPresenter { get; private set; }

        public DevExpress.Xpf.DocumentViewer.UndoRedoManager UndoRedoManager { get; private set; }

        public CommandProvider ActualCommandProvider { get; private set; }

        public BehaviorProvider ActualBehaviorProvider { get; private set; }

        public double HorizontalPageSpacing { get; private set; }

        public double VerticalPageSpacing { get; private set; }

        public ICommand PrintPagesCommand { get; private set; }

        public ICommand SetZoomFactorCommand { get; private set; }

        public ICommand ZoomInCommand { get; private set; }

        public ICommand ZoomOutCommand { get; private set; }

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfThumbnailsViewerControl.<>c <>9 = new PdfThumbnailsViewerControl.<>c();
            public static Action<DocumentPresenterControl> <>9__82_0;
            public static Func<PdfThumbnailsViewerSettings, PdfViewerControl> <>9__86_0;
            public static Action<ThumbnailsViewerPresenterControl> <>9__97_0;
            public static Action<ThumbnailsViewerPresenterControl> <>9__97_1;
            public static Action<ThumbnailsViewerPresenterControl> <>9__98_0;
            public static Func<AutoHideGroup, BaseLayoutItem> <>9__102_0;
            public static Func<BaseLayoutItem, bool> <>9__102_1;
            public static Func<bool> <>9__102_2;

            internal void <.cctor>b__7_0(PdfThumbnailsViewerControl control, PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
            {
                control.SettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_1(PdfThumbnailsViewerControl control, PdfThumbnailsViewerSettings oldValue, PdfThumbnailsViewerSettings newValue)
            {
                control.ActualSettingsChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_2(PdfThumbnailsViewerControl control, int oldValue, int newValue)
            {
                control.SelectedPageNumberChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_3(PdfThumbnailsViewerControl control, double oldValue, double newValue)
            {
                control.ZoomFactorChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_4(PdfThumbnailsViewerControl control, ObservableCollection<int> oldValue, ObservableCollection<int> newValue)
            {
                control.HighlightedPageNumbersChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_5(PdfThumbnailsViewerControl control, Rotation oldValue, Rotation newValue)
            {
                control.PageRotationChanged(oldValue, newValue);
            }

            internal void <DetachDocumentPresenterControl>b__82_0(DocumentPresenterControl x)
            {
                x.BehaviorProvider = null;
                x.Document = null;
            }

            internal BaseLayoutItem <DockLayoutManagerOnDockItemExpanded>b__102_0(AutoHideGroup x) => 
                x.SelectedItem;

            internal bool <DockLayoutManagerOnDockItemExpanded>b__102_1(BaseLayoutItem x) => 
                x.Name.Equals("thumbnailsViewerPanel");

            internal bool <DockLayoutManagerOnDockItemExpanded>b__102_2() => 
                false;

            internal void <InvalidateSettings>b__97_0(ThumbnailsViewerPresenterControl x)
            {
                x.InvalidateRender();
            }

            internal void <InvalidateSettings>b__97_1(ThumbnailsViewerPresenterControl x)
            {
                x.UpdatePagesInternal();
            }

            internal void <LoadThumbnails>b__98_0(ThumbnailsViewerPresenterControl x)
            {
                x.UpdateNativeRendererInternal();
            }

            internal PdfViewerControl <PrintPages>b__86_0(PdfThumbnailsViewerSettings x) => 
                x.Owner;
        }
    }
}

