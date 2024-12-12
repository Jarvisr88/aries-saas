namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    public class DocumentViewerControl : Control, IDocumentViewerControl
    {
        public static readonly DependencyProperty BarItemNameProperty;
        public static readonly DependencyProperty ActualViewerProperty;
        public static readonly DependencyProperty DocumentSourceProperty;
        public static readonly DependencyProperty ZoomFactorProperty;
        public static readonly DependencyProperty ZoomModeProperty;
        public static readonly DependencyProperty PageRotationProperty;
        public static readonly DependencyProperty CurrentPageNumberProperty;
        public static readonly DependencyProperty BehaviorProviderProperty;
        public static readonly DependencyProperty CommandProviderProperty;
        public static readonly DependencyProperty CommandBarStyleProperty;
        public static readonly DependencyProperty BarsTemplateProperty;
        public static readonly DependencyProperty RibbonTemplateProperty;
        public static readonly DependencyProperty PresenterTemplateProperty;
        public static readonly DependencyProperty ResetSettingsOnDocumentCloseProperty;
        public static readonly DependencyProperty OpenFileDialogTemplateProperty;
        public static readonly DependencyProperty HorizontalPageSpacingProperty;
        public static readonly DependencyProperty DisposeDocumentOnUnloadProperty;
        public static readonly DependencyProperty DocumentProperty;
        private static readonly DependencyPropertyKey DocumentPropertyKey;
        public static readonly DependencyProperty PageCountProperty;
        private static readonly DependencyPropertyKey PageCountPropertyKey;
        public static readonly DependencyProperty ActualBehaviorProviderProperty;
        private static readonly DependencyPropertyKey ActualBehaviorProviderPropertyKey;
        public static readonly DependencyProperty ActualCommandProviderProperty;
        private static readonly DependencyPropertyKey ActualCommandProviderPropertyKey;
        public static readonly DependencyProperty ActualBarsTemplateProperty;
        private static readonly DependencyPropertyKey ActualBarsTemplatePropertyKey;
        public static readonly DependencyProperty UndoRedoManagerProperty;
        private static readonly DependencyPropertyKey UndoRedoManagerPropertyKey;
        public static readonly DependencyProperty IsSearchControlVisibleProperty;
        private static readonly DependencyPropertyKey IsSearchControlVisiblePropertyKey;
        private static readonly DependencyPropertyKey ActualDocumentMapSettingsPropertyKey;
        public static readonly DependencyProperty ActualDocumentMapSettingsProperty;
        private static readonly DependencyPropertyKey PropertyProviderPropertyKey;
        public static readonly DependencyProperty PropertyProviderProperty;
        public static RoutedEvent DocumentChangedEvent;
        public static RoutedEvent ZoomChangedEvent;
        public static RoutedEvent PageRotationChangedEvent;
        public static RoutedEvent CurrentPageNumberChangedEvent;
        protected DevExpress.Xpf.DocumentViewer.IDocument documentInternal;

        public event RoutedEventHandler CurrentPageNumberChanged
        {
            add
            {
                base.AddHandler(CurrentPageNumberChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CurrentPageNumberChangedEvent, value);
            }
        }

        public event RoutedEventHandler DocumentChanged
        {
            add
            {
                base.AddHandler(DocumentChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(DocumentChangedEvent, value);
            }
        }

        public event RoutedEventHandler PageRotationChanged
        {
            add
            {
                base.AddHandler(PageRotationChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(PageRotationChangedEvent, value);
            }
        }

        public event RoutedEventHandler ZoomChanged
        {
            add
            {
                base.AddHandler(ZoomChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ZoomChangedEvent, value);
            }
        }

        static DocumentViewerControl()
        {
            Type ownerType = typeof(DocumentViewerControl);
            BarItemNameProperty = DependencyPropertyManager.RegisterAttached("BarItemName", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(DocumentViewerControl.OnBarItemNameChanged)));
            ActualViewerProperty = DependencyPropertyManager.RegisterAttached("ActualViewer", typeof(IDocumentViewerControl), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            DocumentSourceProperty = DependencyPropertyManager.Register("DocumentSource", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnDocumentSourceChanged(args.OldValue, args.NewValue)));
            ZoomFactorProperty = DependencyPropertyManager.Register("ZoomFactor", typeof(double), ownerType, new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnZoomFactorChangedInternal((double) args.OldValue, (double) args.NewValue), (obj, arg) => ((DocumentViewerControl) obj).CoerceZoomFactor(arg)));
            ZoomModeProperty = DependencyPropertyManager.Register("ZoomMode", typeof(DevExpress.Xpf.DocumentViewer.ZoomMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.DocumentViewer.ZoomMode.ActualSize, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnZoomModeChangedInternal((DevExpress.Xpf.DocumentViewer.ZoomMode) args.OldValue, (DevExpress.Xpf.DocumentViewer.ZoomMode) args.NewValue)));
            PageRotationProperty = DependencyPropertyManager.Register("PageRotation", typeof(Rotation), ownerType, new FrameworkPropertyMetadata(Rotation.Rotate0, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnPageRotationChangedInternal((Rotation) args.OldValue, (Rotation) args.NewValue)));
            CurrentPageNumberProperty = DependencyPropertyManager.Register("CurrentPageNumber", typeof(int), ownerType, new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnCurrentPageNumberChangedInternal((int) args.OldValue, (int) args.NewValue), (obj, arg) => ((DocumentViewerControl) obj).CoerceCurrentPageNumber(arg)));
            BehaviorProviderProperty = DependencyPropertyManager.Register("BehaviorProvider", typeof(DevExpress.Xpf.DocumentViewer.BehaviorProvider), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnBehaviorProviderChangedInternal((DevExpress.Xpf.DocumentViewer.BehaviorProvider) args.OldValue, (DevExpress.Xpf.DocumentViewer.BehaviorProvider) args.NewValue)));
            CommandProviderProperty = DependencyPropertyManager.Register("CommandProvider", typeof(DevExpress.Xpf.DocumentViewer.CommandProvider), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnCommandProviderChangedInternal((DevExpress.Xpf.DocumentViewer.CommandProvider) args.OldValue, (DevExpress.Xpf.DocumentViewer.CommandProvider) args.NewValue)));
            CommandBarStyleProperty = DependencyPropertyManager.Register("CommandBarStyle", typeof(DevExpress.Xpf.DocumentViewer.CommandBarStyle), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.DocumentViewer.CommandBarStyle.Ribbon, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnCommandBarStyleChangedInternal((DevExpress.Xpf.DocumentViewer.CommandBarStyle) args.OldValue, (DevExpress.Xpf.DocumentViewer.CommandBarStyle) args.NewValue)));
            BarsTemplateProperty = DependencyPropertyManager.Register("BarsTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnBarsTemplateChangedInternal((DataTemplate) args.NewValue)));
            RibbonTemplateProperty = DependencyPropertyManager.Register("RibbonTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnRibbonTemplateChangedInternal((DataTemplate) args.NewValue)));
            PresenterTemplateProperty = DependencyPropertyManager.Register("PresenterTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).PresenterTemplateChanged((DataTemplate) args.NewValue)));
            ResetSettingsOnDocumentCloseProperty = DependencyPropertyManager.Register("ResetSettingsOnDocumentClose", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            OpenFileDialogTemplateProperty = DependencyPropertyManager.Register("OpenFileDialogTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentViewerControl), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            HorizontalPageSpacingProperty = DependencyPropertyRegistrator.Register<DocumentViewerControl, double>(System.Linq.Expressions.Expression.Lambda<Func<DocumentViewerControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentViewerControl.get_HorizontalPageSpacing)), parameters), 10.0, (owner, oldValue, newValue) => owner.HorizontalPageSpacingPropertyChanged(oldValue, newValue));
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentViewerControl), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            DisposeDocumentOnUnloadProperty = DependencyPropertyRegistrator.Register<DocumentViewerControl, bool>(System.Linq.Expressions.Expression.Lambda<Func<DocumentViewerControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentViewerControl.get_DisposeDocumentOnUnload)), expressionArray2), true);
            DocumentPropertyKey = DependencyPropertyManager.RegisterReadOnly("Document", typeof(DevExpress.Xpf.DocumentViewer.IDocument), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnDocumentChangedInternal((DevExpress.Xpf.DocumentViewer.IDocument) args.OldValue, (DevExpress.Xpf.DocumentViewer.IDocument) args.NewValue)));
            DocumentProperty = DocumentPropertyKey.DependencyProperty;
            PageCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("PageCount", typeof(int), ownerType, new FrameworkPropertyMetadata(0));
            PageCountProperty = PageCountPropertyKey.DependencyProperty;
            ActualBehaviorProviderPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualBehaviorProvider", typeof(DevExpress.Xpf.DocumentViewer.BehaviorProvider), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnActualBehaviorProviderChangedInternal((DevExpress.Xpf.DocumentViewer.BehaviorProvider) args.OldValue, (DevExpress.Xpf.DocumentViewer.BehaviorProvider) args.NewValue)));
            ActualBehaviorProviderProperty = ActualBehaviorProviderPropertyKey.DependencyProperty;
            ActualCommandProviderPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualCommandProvider", typeof(DevExpress.Xpf.DocumentViewer.CommandProvider), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnActualCommandProviderChangedInternal((DevExpress.Xpf.DocumentViewer.CommandProvider) args.OldValue, (DevExpress.Xpf.DocumentViewer.CommandProvider) args.NewValue)));
            ActualCommandProviderProperty = ActualCommandProviderPropertyKey.DependencyProperty;
            ActualBarsTemplatePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualBarsTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));
            ActualBarsTemplateProperty = ActualBarsTemplatePropertyKey.DependencyProperty;
            UndoRedoManagerPropertyKey = DependencyPropertyManager.RegisterReadOnly("UndoRedoManager", typeof(DevExpress.Xpf.DocumentViewer.UndoRedoManager), ownerType, new FrameworkPropertyMetadata(null));
            UndoRedoManagerProperty = UndoRedoManagerPropertyKey.DependencyProperty;
            IsSearchControlVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsSearchControlVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DocumentViewerControl) obj).OnSearchControlVisibleChanged((bool) args.NewValue)));
            IsSearchControlVisibleProperty = IsSearchControlVisiblePropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentViewerControl), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            ActualDocumentMapSettingsPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<DocumentViewerControl, DocumentMapSettings>(System.Linq.Expressions.Expression.Lambda<Func<DocumentViewerControl, DocumentMapSettings>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentViewerControl.get_ActualDocumentMapSettings)), expressionArray3), null, (owner, oldValue, newValue) => owner.ActualDocumentMapSettingsChanged(oldValue, newValue));
            ActualDocumentMapSettingsProperty = ActualDocumentMapSettingsPropertyKey.DependencyProperty;
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentViewerControl), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            PropertyProviderPropertyKey = DependencyPropertyRegistrator.RegisterReadOnly<DocumentViewerControl, DevExpress.Xpf.DocumentViewer.PropertyProvider>(System.Linq.Expressions.Expression.Lambda<Func<DocumentViewerControl, DevExpress.Xpf.DocumentViewer.PropertyProvider>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentViewerControl.get_PropertyProvider)), expressionArray4), null, null);
            PropertyProviderProperty = PropertyProviderPropertyKey.DependencyProperty;
            DocumentChangedEvent = EventManager.RegisterRoutedEvent("DocumentChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            ZoomChangedEvent = EventManager.RegisterRoutedEvent("ZoomChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            PageRotationChangedEvent = EventManager.RegisterRoutedEvent("PageRotationChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
            CurrentPageNumberChangedEvent = EventManager.RegisterRoutedEvent("CurrentPageNumberChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), ownerType);
        }

        public DocumentViewerControl()
        {
            base.DefaultStyleKey = typeof(DocumentViewerControl);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            this.PropertyProvider = this.CreatePropertyProvider();
            this.ActualBehaviorProvider = this.CreateBehaviorProvider();
            this.ActualCommandProvider = this.CreateCommandProvider();
            this.UndoRedoManager = this.CreateUndoRedoManager();
            this.ActualDocumentMapSettings = this.CreateDefaultDocumentMapSettings();
            SetActualViewer(this, this);
            this.InitializeCommands();
            this.ThrowOpenFileExceptionLocker = new Locker();
        }

        protected virtual void ActualDocumentMapSettingsChanged(DocumentMapSettings oldValue, DocumentMapSettings newValue)
        {
            Action<DocumentMapSettings> action = <>c.<>9__305_0;
            if (<>c.<>9__305_0 == null)
            {
                Action<DocumentMapSettings> local1 = <>c.<>9__305_0;
                action = <>c.<>9__305_0 = x => x.Release();
            }
            oldValue.Do<DocumentMapSettings>(action);
            newValue.Do<DocumentMapSettings>(x => x.Initialize(this));
        }

        protected virtual void AssignDocumentPresenterProperties()
        {
            this.DocumentPresenter.Do<DocumentPresenterControl>(delegate (DocumentPresenterControl x) {
                x.Document = this.Document;
                x.BehaviorProvider = this.ActualBehaviorProvider;
                x.IsSearchControlVisible = this.IsSearchControlVisible;
                x.HorizontalPageSpacing = this.HorizontalPageSpacing;
                x.VerticalPageSpacing = ((IDocumentViewerControl) this).VerticalPageSpacing;
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

        protected virtual bool CanClockwiseRotate() => 
            this.IsDocumentContainPages();

        protected virtual bool CanCloseDocument() => 
            this.DocumentSource != null;

        protected virtual bool CanCounterClockwiseRotate() => 
            this.IsDocumentContainPages();

        protected virtual bool CanExecuteNavigate(object parameter) => 
            parameter != null;

        protected virtual bool CanFindNextText(TextSearchParameter parameter) => 
            this.IsDocumentContainPages();

        protected virtual bool CanNavigateNextPage()
        {
            Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> func1 = <>c.<>9__259_0;
            if (<>c.<>9__259_0 == null)
            {
                Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> local1 = <>c.<>9__259_0;
                func1 = <>c.<>9__259_0 = x => x.IsLoaded;
            }
            return (((DevExpress.Xpf.DocumentViewer.IDocument) func1).Return<DevExpress.Xpf.DocumentViewer.IDocument, bool>(((Func<DevExpress.Xpf.DocumentViewer.IDocument, bool>) (<>c.<>9__259_1 ??= () => false)), (<>c.<>9__259_1 ??= () => false)) && (this.CurrentPageNumber < this.PageCount));
        }

        protected virtual bool CanNavigatePreviousPage()
        {
            Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> func1 = <>c.<>9__261_0;
            if (<>c.<>9__261_0 == null)
            {
                Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> local1 = <>c.<>9__261_0;
                func1 = <>c.<>9__261_0 = x => x.IsLoaded;
            }
            return (((DevExpress.Xpf.DocumentViewer.IDocument) func1).Return<DevExpress.Xpf.DocumentViewer.IDocument, bool>(((Func<DevExpress.Xpf.DocumentViewer.IDocument, bool>) (<>c.<>9__261_1 ??= () => false)), (<>c.<>9__261_1 ??= () => false)) && (this.CurrentPageNumber > 1));
        }

        protected virtual bool CanOpenDocument(string filePath = null) => 
            true;

        protected virtual bool CanRedo() => 
            this.UndoRedoManager.CanRedo;

        protected virtual bool CanScroll(DevExpress.Xpf.DocumentViewer.ScrollCommand command)
        {
            Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> evaluator = <>c.<>9__269_0;
            if (<>c.<>9__269_0 == null)
            {
                Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> local1 = <>c.<>9__269_0;
                evaluator = <>c.<>9__269_0 = x => x.IsLoaded;
            }
            return this.Document.Return<DevExpress.Xpf.DocumentViewer.IDocument, bool>(evaluator, (<>c.<>9__269_1 ??= () => false));
        }

        protected virtual bool CanSetPageNumber(int pageNumber) => 
            this.IsDocumentContainPages();

        protected virtual bool CanSetZoomFactor(double zoomFactor) => 
            this.IsDocumentContainPages();

        protected virtual bool CanSetZoomMode(DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode) => 
            this.IsDocumentContainPages();

        protected virtual bool CanShowFindText(bool? show) => 
            this.IsDocumentContainPages();

        protected virtual bool CanUndo() => 
            this.UndoRedoManager.CanUndo;

        protected virtual bool CanZoomIn() => 
            this.IsDocumentContainPages() && this.ActualBehaviorProvider.CanZoomIn();

        protected virtual bool CanZoomOut() => 
            this.IsDocumentContainPages() && this.ActualBehaviorProvider.CanZoomOut();

        private bool ChooseFile(string documentName, out string fileName)
        {
            OpenFileDialogService service = this.OpenFileDialogTemplate.Return<DataTemplate, OpenFileDialogService>(new Func<DataTemplate, OpenFileDialogService>(TemplateHelper.LoadFromTemplate<OpenFileDialogService>), new Func<OpenFileDialogService>(this.CreateDefaultOpenFileDialogService));
            IFileInfo fileInfo = null;
            AssignableServiceHelper2<DocumentViewerControl, OpenFileDialogService>.DoServiceAction(this, service, delegate (OpenFileDialogService service) {
                IOpenFileDialogService service2 = service;
                fileInfo = service2.ShowDialog() ? service2.Files.FirstOrDefault<IFileInfo>() : null;
            });
            if (fileInfo == null)
            {
                fileName = null;
                return false;
            }
            fileName = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
            return true;
        }

        protected virtual void ClockwiseRotate()
        {
            this.PageRotation = this.GetClockwiseRotation(this.PageRotation);
        }

        protected virtual void CloseDocument()
        {
            this.DocumentSource = null;
        }

        protected virtual object CoerceCurrentPageNumber(object value)
        {
            int num = (int) value;
            Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> func1 = <>c.<>9__303_0;
            if (<>c.<>9__303_0 == null)
            {
                Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> local1 = <>c.<>9__303_0;
                func1 = <>c.<>9__303_0 = x => x.IsLoaded;
            }
            return (((DevExpress.Xpf.DocumentViewer.IDocument) func1).Return<DevExpress.Xpf.DocumentViewer.IDocument, bool>(((Func<DevExpress.Xpf.DocumentViewer.IDocument, bool>) (<>c.<>9__303_1 ??= () => false)), (<>c.<>9__303_1 ??= () => false)) ? Math.Max(1, Math.Min(this.PageCount, num)) : 0);
        }

        protected virtual object CoerceZoomFactor(object value) => 
            value;

        protected virtual void CounterClockwiseRotate()
        {
            this.PageRotation = this.GetCounterClockwiseRotation(this.PageRotation);
        }

        protected virtual DevExpress.Xpf.DocumentViewer.BehaviorProvider CreateBehaviorProvider() => 
            new DevExpress.Xpf.DocumentViewer.BehaviorProvider();

        protected virtual DevExpress.Xpf.DocumentViewer.CommandProvider CreateCommandProvider() => 
            new DevExpress.Xpf.DocumentViewer.CommandProvider();

        protected virtual DocumentMapSettings CreateDefaultDocumentMapSettings() => 
            new DocumentMapSettings();

        protected virtual OpenFileDialogService CreateDefaultOpenFileDialogService()
        {
            OpenFileDialogService service1 = new OpenFileDialogService();
            service1.Title = DocumentViewerLocalizer.GetString(DocumentViewerStringId.OpenFileDialogTitle);
            service1.Filter = this.GetOpenFileFilter();
            service1.RestoreDirectory = true;
            return service1;
        }

        protected virtual DevExpress.Xpf.DocumentViewer.IDocument CreateDocument(object source) => 
            null;

        protected virtual DevExpress.Xpf.DocumentViewer.PropertyProvider CreatePropertyProvider() => 
            new DevExpress.Xpf.DocumentViewer.PropertyProvider();

        protected virtual DevExpress.Xpf.DocumentViewer.UndoRedoManager CreateUndoRedoManager() => 
            new DevExpress.Xpf.DocumentViewer.UndoRedoManager(base.Dispatcher);

        protected virtual void DetachDocumentPresenterControl()
        {
            Action<DocumentPresenterControl> action = <>c.<>9__302_0;
            if (<>c.<>9__302_0 == null)
            {
                Action<DocumentPresenterControl> local1 = <>c.<>9__302_0;
                action = <>c.<>9__302_0 = delegate (DocumentPresenterControl x) {
                    x.BehaviorProvider = null;
                    x.Document = null;
                };
            }
            this.DocumentPresenter.Do<DocumentPresenterControl>(action);
        }

        void IDocumentViewerControl.AttachDocumentPresenterControl(DocumentPresenterControl presenter)
        {
            this.AttachDocumentPresenterControl(presenter);
        }

        protected virtual void ExecuteNavigate(object parameter)
        {
        }

        protected virtual void FindNextText(TextSearchParameter parameter)
        {
        }

        protected virtual object FindTextCore(TextSearchParameter search) => 
            null;

        public static IDocumentViewerControl GetActualViewer(DependencyObject d) => 
            (IDocumentViewerControl) d.GetValue(ActualViewerProperty);

        public static string GetBarItemName(DependencyObject d) => 
            (string) d.GetValue(BarItemNameProperty);

        private Rotation GetClockwiseRotation(Rotation rotation)
        {
            int num = ((int) rotation) + 1;
            return ((num > (Enum.GetValues(typeof(Rotation)).Length - 1)) ? Rotation.Rotate0 : ((Rotation) num));
        }

        private Rotation GetCounterClockwiseRotation(Rotation rotation)
        {
            int num = ((int) rotation) - 1;
            return ((num < 0) ? Rotation.Rotate270 : ((Rotation) num));
        }

        protected virtual string GetOpenFileFilter() => 
            "";

        protected virtual void HorizontalPageSpacingPropertyChanged(double oldValue, double newValue)
        {
            this.AssignDocumentPresenterProperties();
        }

        protected virtual void InitializeCommands()
        {
            this.OpenDocumentCommand = DelegateCommandFactory.Create<string>(new Action<string>(this.OpenDocument), new Func<string, bool>(this.CanOpenDocument));
            this.CloseDocumentCommand = DelegateCommandFactory.Create(new Action(this.CloseDocument), new Func<bool>(this.CanCloseDocument));
            this.ZoomInCommand = DelegateCommandFactory.Create(() => this.ActualBehaviorProvider.ZoomIn(), new Func<bool>(this.CanZoomIn));
            this.ZoomOutCommand = DelegateCommandFactory.Create(() => this.ActualBehaviorProvider.ZoomOut(), new Func<bool>(this.CanZoomOut));
            this.ClockwiseRotateCommand = DelegateCommandFactory.Create(new Action(this.ClockwiseRotate), new Func<bool>(this.CanClockwiseRotate));
            this.CounterClockwiseRotateCommand = DelegateCommandFactory.Create(new Action(this.CounterClockwiseRotate), new Func<bool>(this.CanCounterClockwiseRotate));
            this.NextPageCommand = DelegateCommandFactory.Create(new Action(this.NavigateNextPage), new Func<bool>(this.CanNavigateNextPage));
            this.PreviousPageCommand = DelegateCommandFactory.Create(new Action(this.NavigatePreviousPage), new Func<bool>(this.CanNavigatePreviousPage));
            this.SetPageNumberCommand = DelegateCommandFactory.Create<int>(new Action<int>(this.SetPageNumber), new Func<int, bool>(this.CanSetPageNumber));
            this.SetZoomFactorCommand = DelegateCommandFactory.Create<double>(new Action<double>(this.SetZoomFactor), new Func<double, bool>(this.CanSetZoomFactor));
            this.SetZoomModeCommand = DelegateCommandFactory.Create<DevExpress.Xpf.DocumentViewer.ZoomMode>(new Action<DevExpress.Xpf.DocumentViewer.ZoomMode>(this.SetZoomMode), new Func<DevExpress.Xpf.DocumentViewer.ZoomMode, bool>(this.CanSetZoomMode));
            this.ScrollCommand = DelegateCommandFactory.Create<DevExpress.Xpf.DocumentViewer.ScrollCommand>(new Action<DevExpress.Xpf.DocumentViewer.ScrollCommand>(this.Scroll), new Func<DevExpress.Xpf.DocumentViewer.ScrollCommand, bool>(this.CanScroll));
            this.NextViewCommand = DelegateCommandFactory.Create(new Action(this.Redo), new Func<bool>(this.CanRedo));
            this.PreviousViewCommand = DelegateCommandFactory.Create(new Action(this.Undo), new Func<bool>(this.CanUndo));
            this.ShowFindTextCommand = DelegateCommandFactory.Create<bool?>(new Action<bool?>(this.ShowFindText), new Func<bool?, bool>(this.CanShowFindText));
            this.FindTextCommand = DelegateCommandFactory.Create<TextSearchParameter>(new Action<TextSearchParameter>(this.FindNextText), new Func<TextSearchParameter, bool>(this.CanFindNextText));
            this.NavigateCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.ExecuteNavigate), new Func<object, bool>(this.CanExecuteNavigate));
        }

        protected internal bool IsDocumentContainPages()
        {
            Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> func1 = <>c.<>9__237_0;
            if (<>c.<>9__237_0 == null)
            {
                Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> local1 = <>c.<>9__237_0;
                func1 = <>c.<>9__237_0 = x => x.IsLoaded;
            }
            return (((DevExpress.Xpf.DocumentViewer.IDocument) func1).Return<DevExpress.Xpf.DocumentViewer.IDocument, bool>(((Func<DevExpress.Xpf.DocumentViewer.IDocument, bool>) (<>c.<>9__237_1 ??= () => false)), (<>c.<>9__237_1 ??= () => false)) && this.Document.Pages.Any<IPage>());
        }

        protected virtual void LoadDocument(object source)
        {
        }

        protected virtual void NavigateNextPage()
        {
            this.SetCurrentPageNumber(this.CurrentPageNumber + 1);
        }

        protected virtual void NavigatePreviousPage()
        {
            this.SetCurrentPageNumber(this.CurrentPageNumber - 1);
        }

        protected virtual void OnActualBehaviorProviderChanged(DevExpress.Xpf.DocumentViewer.BehaviorProvider oldValue, DevExpress.Xpf.DocumentViewer.BehaviorProvider newValue)
        {
        }

        private void OnActualBehaviorProviderChangedInternal(DevExpress.Xpf.DocumentViewer.BehaviorProvider oldValue, DevExpress.Xpf.DocumentViewer.BehaviorProvider newValue)
        {
            oldValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.ZoomChanged -= new EventHandler<ZoomChangedEventArgs>(this.OnBehaviorProviderZoomChanged);
            });
            oldValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.RotateAngleChanged -= new EventHandler<RotateAngleChangedEventArgs>(this.OnBehaviorProviderRotateAngleChanged);
            });
            oldValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.PageIndexChanged -= new EventHandler<PageIndexChangedEventArgs>(this.OnBehaviorProviderPageIndexChanged);
            });
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.ZoomFactor = this.ZoomFactor);
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(this.OnBehaviorProviderZoomChanged);
            });
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.RotateAngleChanged += new EventHandler<RotateAngleChangedEventArgs>(this.OnBehaviorProviderRotateAngleChanged);
            });
            newValue.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(delegate (DevExpress.Xpf.DocumentViewer.BehaviorProvider x) {
                x.PageIndexChanged += new EventHandler<PageIndexChangedEventArgs>(this.OnBehaviorProviderPageIndexChanged);
            });
            this.AssignDocumentPresenterProperties();
            this.OnActualBehaviorProviderChanged(oldValue, newValue);
        }

        protected virtual void OnActualCommandProviderChanged(DevExpress.Xpf.DocumentViewer.CommandProvider oldValue, DevExpress.Xpf.DocumentViewer.CommandProvider newValue)
        {
        }

        private void OnActualCommandProviderChangedInternal(DevExpress.Xpf.DocumentViewer.CommandProvider oldValue, DevExpress.Xpf.DocumentViewer.CommandProvider newValue)
        {
            Action<DevExpress.Xpf.DocumentViewer.CommandProvider> action = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Action<DevExpress.Xpf.DocumentViewer.CommandProvider> local1 = <>c.<>9__51_0;
                action = <>c.<>9__51_0 = x => x.DocumentViewer = null;
            }
            oldValue.Do<DevExpress.Xpf.DocumentViewer.CommandProvider>(action);
            newValue.Do<DevExpress.Xpf.DocumentViewer.CommandProvider>(x => x.DocumentViewer = this);
            this.OnActualCommandProviderChanged(oldValue, newValue);
        }

        private static void OnBarItemNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!d.IsPropertySet(FrameworkElement.NameProperty))
            {
                d.SetValue(FrameworkElement.NameProperty, e.NewValue);
            }
        }

        protected virtual void OnBarsTemplateChanged(DataTemplate newValue)
        {
        }

        private void OnBarsTemplateChangedInternal(DataTemplate newValue)
        {
            this.OnBarsTemplateChanged(newValue);
            this.UpdateCommandBar();
        }

        protected virtual void OnBehaviorProviderChanged(DevExpress.Xpf.DocumentViewer.BehaviorProvider oldValue, DevExpress.Xpf.DocumentViewer.BehaviorProvider newValue)
        {
        }

        private void OnBehaviorProviderChangedInternal(DevExpress.Xpf.DocumentViewer.BehaviorProvider oldValue, DevExpress.Xpf.DocumentViewer.BehaviorProvider newValue)
        {
            this.OnBehaviorProviderChanged(oldValue, newValue);
            this.ActualBehaviorProvider = newValue;
        }

        private void OnBehaviorProviderPageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.SetCurrentPageNumber(e.PageIndex + 1);
        }

        private void OnBehaviorProviderRotateAngleChanged(object sender, RotateAngleChangedEventArgs e)
        {
            int num = (((int) e.NewValue) % 360) / 90;
            this.PageRotation = (Rotation) num;
            this.RaisePageRotationChanged();
        }

        private void OnBehaviorProviderZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            this.SetZoomModeAndFactor(e.ZoomMode, e.ZoomFactor);
            this.RaiseZoomChanged();
        }

        protected virtual void OnCommandBarStyleChanged(DevExpress.Xpf.DocumentViewer.CommandBarStyle oldValue, DevExpress.Xpf.DocumentViewer.CommandBarStyle newValue)
        {
        }

        private void OnCommandBarStyleChangedInternal(DevExpress.Xpf.DocumentViewer.CommandBarStyle oldValue, DevExpress.Xpf.DocumentViewer.CommandBarStyle newValue)
        {
            this.OnCommandBarStyleChanged(oldValue, newValue);
            this.UpdateCommandBar();
        }

        protected virtual void OnCommandProviderChanged(DevExpress.Xpf.DocumentViewer.CommandProvider oldValue, DevExpress.Xpf.DocumentViewer.CommandProvider newValue)
        {
        }

        private void OnCommandProviderChangedInternal(DevExpress.Xpf.DocumentViewer.CommandProvider oldValue, DevExpress.Xpf.DocumentViewer.CommandProvider newValue)
        {
            this.OnCommandProviderChanged(oldValue, newValue);
            this.ActualCommandProvider = newValue;
        }

        protected virtual void OnCurrentPageNumberChanged(int oldValue, int newValue)
        {
        }

        private void OnCurrentPageNumberChangedInternal(int oldValue, int newValue)
        {
            this.ActualBehaviorProvider.PageIndex = newValue - 1;
            this.OnCurrentPageNumberChanged(oldValue, newValue);
            this.RaiseCurrentPageNumberChanged();
        }

        protected virtual void OnDocumentChanged(DevExpress.Xpf.DocumentViewer.IDocument oldValue, DevExpress.Xpf.DocumentViewer.IDocument newValue)
        {
        }

        internal virtual void OnDocumentChangedInternal(DevExpress.Xpf.DocumentViewer.IDocument oldValue, DevExpress.Xpf.DocumentViewer.IDocument newValue)
        {
            this.documentInternal = newValue;
            Func<DevExpress.Xpf.DocumentViewer.IDocument, IEnumerable<IPage>> evaluator = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                Func<DevExpress.Xpf.DocumentViewer.IDocument, IEnumerable<IPage>> local1 = <>c.<>9__50_0;
                evaluator = <>c.<>9__50_0 = x => x.Pages;
            }
            Func<IEnumerable<IPage>, int> func2 = <>c.<>9__50_1;
            if (<>c.<>9__50_1 == null)
            {
                Func<IEnumerable<IPage>, int> local2 = <>c.<>9__50_1;
                func2 = <>c.<>9__50_1 = x => x.Count<IPage>();
            }
            this.PageCount = newValue.With<DevExpress.Xpf.DocumentViewer.IDocument, IEnumerable<IPage>>(evaluator).Return<IEnumerable<IPage>, int>(func2, <>c.<>9__50_2 ??= () => 0);
            this.SetCurrentPageNumber(1);
            this.AssignDocumentPresenterProperties();
            this.OnDocumentChanged(oldValue, newValue);
            this.RaiseDocumentChanged();
        }

        protected virtual void OnDocumentSourceChanged(object oldValue, object newValue)
        {
            this.ReleaseDocument(this.documentInternal);
            if (newValue == null)
            {
                this.SetCurrentPageNumber(0);
                this.Document = null;
                this.documentInternal = null;
            }
            else
            {
                if ((oldValue != null) && this.ResetSettingsOnDocumentClose)
                {
                    this.SetZoomModeAndFactor(DevExpress.Xpf.DocumentViewer.ZoomMode.ActualSize, 1.0);
                }
                this.PageRotation = Rotation.Rotate0;
                this.SetCurrentPageNumber(1);
                this.Document = this.CreateDocument(newValue);
                this.LoadDocument(newValue);
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (this.ActualBarsTemplate == null)
            {
                this.UpdateCommandBar();
            }
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs args)
        {
            if ((this.DocumentSource != null) && (this.Document == null))
            {
                this.Document = this.CreateDocument(this.DocumentSource);
                this.LoadDocument(this.DocumentSource);
            }
        }

        protected virtual void OnPageRotationChanged(Rotation oldValue, Rotation newValue)
        {
        }

        private void OnPageRotationChangedInternal(Rotation oldValue, Rotation newValue)
        {
            this.ActualBehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.RotateAngle = (double) (((Rotation) 90) * newValue));
            this.OnPageRotationChanged(oldValue, newValue);
        }

        protected virtual void OnRibbonTemplateChanged(DataTemplate newValue)
        {
        }

        private void OnRibbonTemplateChangedInternal(DataTemplate newValue)
        {
            this.OnRibbonTemplateChanged(newValue);
            this.UpdateCommandBar();
        }

        private void OnSearchControlVisibleChanged(bool newValue)
        {
            this.AssignDocumentPresenterProperties();
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs args)
        {
            if (this.DisposeDocumentOnUnload)
            {
                this.ReleaseDocument(this.documentInternal);
                this.Document = null;
                this.documentInternal = null;
            }
        }

        protected virtual void OnZoomFactorChanged(double oldValue, double newValue)
        {
        }

        private void OnZoomFactorChangedInternal(double oldValue, double newValue)
        {
            this.ActualBehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.ZoomFactor = newValue);
            this.OnZoomFactorChanged(oldValue, newValue);
        }

        protected virtual void OnZoomModeChanged(DevExpress.Xpf.DocumentViewer.ZoomMode oldValue, DevExpress.Xpf.DocumentViewer.ZoomMode newValue)
        {
        }

        private void OnZoomModeChangedInternal(DevExpress.Xpf.DocumentViewer.ZoomMode oldValue, DevExpress.Xpf.DocumentViewer.ZoomMode newValue)
        {
            this.ActualBehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.ZoomMode = newValue);
            this.OnZoomModeChanged(oldValue, newValue);
        }

        public virtual void OpenDocument(string filePath = null)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                this.DocumentSource = filePath;
            }
            else
            {
                string str;
                if (this.ChooseFile(filePath, out str))
                {
                    this.ThrowOpenFileExceptionLocker.LockOnce();
                    this.DocumentSource = str;
                }
            }
        }

        protected virtual void PerformFindText(TextSearchParameter search)
        {
            this.FindTextCommand.TryExecute(search);
        }

        protected virtual void PresenterTemplateChanged(DataTemplate newValue)
        {
            this.UpdateCommandBar();
        }

        private void RaiseCurrentPageNumberChanged()
        {
            base.RaiseEvent(new RoutedEventArgs(CurrentPageNumberChangedEvent));
        }

        private void RaiseDocumentChanged()
        {
            base.RaiseEvent(new RoutedEventArgs(DocumentChangedEvent));
        }

        private void RaisePageRotationChanged()
        {
            base.RaiseEvent(new RoutedEventArgs(PageRotationChangedEvent));
        }

        private void RaiseZoomChanged()
        {
            base.RaiseEvent(new RoutedEventArgs(ZoomChangedEvent));
        }

        protected virtual void Redo()
        {
            this.UndoRedoManager.Redo();
        }

        protected virtual void ReleaseDocument(DevExpress.Xpf.DocumentViewer.IDocument document)
        {
        }

        protected virtual void Scroll(DevExpress.Xpf.DocumentViewer.ScrollCommand command)
        {
            this.DocumentPresenter.Do<DocumentPresenterControl>(x => x.Scroll(command));
        }

        public virtual void ScrollToHorizontalOffset(double offset)
        {
            this.DocumentPresenter.Do<DocumentPresenterControl>(x => x.ScrollToHorizontalOffset(offset));
        }

        public virtual void ScrollToVerticalOffset(double offset)
        {
            this.DocumentPresenter.Do<DocumentPresenterControl>(x => x.ScrollToVerticalOffset(offset));
        }

        public static void SetActualViewer(DependencyObject d, IDocumentViewerControl value)
        {
            d.SetValue(ActualViewerProperty, value);
        }

        public static void SetBarItemName(DependencyObject d, string value)
        {
            d.SetValue(BarItemNameProperty, value);
        }

        protected void SetCurrentPageNumber(int currentPageNumber)
        {
            base.SetCurrentValue(CurrentPageNumberProperty, currentPageNumber);
        }

        protected virtual void SetPageNumber(int pageNumber)
        {
            this.SetCurrentPageNumber(pageNumber);
        }

        protected virtual void SetZoomFactor(double zoomFactor)
        {
            this.ActualBehaviorProvider.ZoomFactor = zoomFactor;
        }

        protected virtual void SetZoomMode(DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode)
        {
            this.ActualBehaviorProvider.ZoomMode = zoomMode;
            this.SetZoomModeAndFactor(this.ActualBehaviorProvider.ZoomMode, this.ActualBehaviorProvider.ZoomFactor);
        }

        private void SetZoomModeAndFactor(DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode, double zoomFactor)
        {
            base.SetCurrentValue(ZoomModeProperty, zoomMode);
            base.SetCurrentValue(ZoomFactorProperty, zoomFactor);
            Action<DevExpress.Xpf.DocumentViewer.CommandProvider> action = <>c.<>9__223_0;
            if (<>c.<>9__223_0 == null)
            {
                Action<DevExpress.Xpf.DocumentViewer.CommandProvider> local1 = <>c.<>9__223_0;
                action = <>c.<>9__223_0 = x => x.UpdateZoomCommand();
            }
            this.ActualCommandProvider.Do<DevExpress.Xpf.DocumentViewer.CommandProvider>(action);
        }

        protected virtual void ShowFindText(bool? show)
        {
            this.IsSearchControlVisible = (show == null) ? true : show.Value;
            if (!this.IsSearchControlVisible)
            {
                Action<DocumentPresenterControl> action = <>c.<>9__277_0;
                if (<>c.<>9__277_0 == null)
                {
                    Action<DocumentPresenterControl> local1 = <>c.<>9__277_0;
                    action = <>c.<>9__277_0 = x => x.Focus();
                }
                this.DocumentPresenter.Do<DocumentPresenterControl>(action);
            }
        }

        protected virtual void Undo()
        {
            this.UndoRedoManager.Undo();
        }

        private void UpdateCommandBar()
        {
            switch (this.CommandBarStyle)
            {
                case DevExpress.Xpf.DocumentViewer.CommandBarStyle.None:
                    this.ActualBarsTemplate = this.PresenterTemplate;
                    return;

                case DevExpress.Xpf.DocumentViewer.CommandBarStyle.Bars:
                    this.ActualBarsTemplate = this.BarsTemplate;
                    return;

                case DevExpress.Xpf.DocumentViewer.CommandBarStyle.Ribbon:
                    this.ActualBarsTemplate = this.RibbonTemplate;
                    return;
            }
        }

        protected virtual void VerticalPageSpacingPropertyChanged(double oldValue, double newValue)
        {
            this.AssignDocumentPresenterProperties();
        }

        public DocumentMapSettings ActualDocumentMapSettings
        {
            get => 
                (DocumentMapSettings) base.GetValue(ActualDocumentMapSettingsProperty);
            protected set => 
                base.SetValue(ActualDocumentMapSettingsPropertyKey, value);
        }

        public DevExpress.Xpf.DocumentViewer.IDocument Document
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.IDocument) base.GetValue(DocumentProperty);
            protected set => 
                base.SetValue(DocumentPropertyKey, value);
        }

        public object DocumentSource
        {
            get => 
                base.GetValue(DocumentSourceProperty);
            set => 
                base.SetValue(DocumentSourceProperty, value);
        }

        public double ZoomFactor
        {
            get => 
                (double) base.GetValue(ZoomFactorProperty);
            set => 
                base.SetValue(ZoomFactorProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.ZoomMode ZoomMode
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.ZoomMode) base.GetValue(ZoomModeProperty);
            set => 
                base.SetValue(ZoomModeProperty, value);
        }

        public Rotation PageRotation
        {
            get => 
                (Rotation) base.GetValue(PageRotationProperty);
            set => 
                base.SetValue(PageRotationProperty, value);
        }

        public int CurrentPageNumber
        {
            get => 
                (int) base.GetValue(CurrentPageNumberProperty);
            set => 
                base.SetValue(CurrentPageNumberProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.BehaviorProvider BehaviorProvider
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.BehaviorProvider) base.GetValue(BehaviorProviderProperty);
            set => 
                base.SetValue(BehaviorProviderProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.CommandProvider CommandProvider
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.CommandProvider) base.GetValue(CommandProviderProperty);
            set => 
                base.SetValue(CommandProviderProperty, value);
        }

        public DevExpress.Xpf.DocumentViewer.CommandBarStyle CommandBarStyle
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.CommandBarStyle) base.GetValue(CommandBarStyleProperty);
            set => 
                base.SetValue(CommandBarStyleProperty, value);
        }

        public DataTemplate BarsTemplate
        {
            get => 
                (DataTemplate) base.GetValue(BarsTemplateProperty);
            set => 
                base.SetValue(BarsTemplateProperty, value);
        }

        public DataTemplate RibbonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(RibbonTemplateProperty);
            set => 
                base.SetValue(RibbonTemplateProperty, value);
        }

        public DataTemplate PresenterTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PresenterTemplateProperty);
            set => 
                base.SetValue(PresenterTemplateProperty, value);
        }

        public bool ResetSettingsOnDocumentClose
        {
            get => 
                (bool) base.GetValue(ResetSettingsOnDocumentCloseProperty);
            set => 
                base.SetValue(ResetSettingsOnDocumentCloseProperty, value);
        }

        public DataTemplate OpenFileDialogTemplate
        {
            get => 
                (DataTemplate) base.GetValue(OpenFileDialogTemplateProperty);
            set => 
                base.SetValue(OpenFileDialogTemplateProperty, value);
        }

        public double HorizontalPageSpacing
        {
            get => 
                (double) base.GetValue(HorizontalPageSpacingProperty);
            set => 
                base.SetValue(HorizontalPageSpacingProperty, value);
        }

        public int PageCount
        {
            get => 
                (int) base.GetValue(PageCountProperty);
            protected set => 
                base.SetValue(PageCountPropertyKey, value);
        }

        public DevExpress.Xpf.DocumentViewer.BehaviorProvider ActualBehaviorProvider
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.BehaviorProvider) base.GetValue(ActualBehaviorProviderProperty);
            private set => 
                base.SetValue(ActualBehaviorProviderPropertyKey, value);
        }

        public DevExpress.Xpf.DocumentViewer.CommandProvider ActualCommandProvider
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.CommandProvider) base.GetValue(ActualCommandProviderProperty);
            private set => 
                base.SetValue(ActualCommandProviderPropertyKey, value);
        }

        public DataTemplate ActualBarsTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualBarsTemplateProperty);
            private set => 
                base.SetValue(ActualBarsTemplatePropertyKey, value);
        }

        public DevExpress.Xpf.DocumentViewer.UndoRedoManager UndoRedoManager
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.UndoRedoManager) base.GetValue(UndoRedoManagerProperty);
            private set => 
                base.SetValue(UndoRedoManagerPropertyKey, value);
        }

        public bool IsSearchControlVisible
        {
            get => 
                (bool) base.GetValue(IsSearchControlVisibleProperty);
            private set => 
                base.SetValue(IsSearchControlVisiblePropertyKey, value);
        }

        public DevExpress.Xpf.DocumentViewer.PropertyProvider PropertyProvider
        {
            get => 
                (DevExpress.Xpf.DocumentViewer.PropertyProvider) base.GetValue(PropertyProviderProperty);
            private set => 
                base.SetValue(PropertyProviderPropertyKey, value);
        }

        public bool DisposeDocumentOnUnload
        {
            get => 
                (bool) base.GetValue(DisposeDocumentOnUnloadProperty);
            set => 
                base.SetValue(DisposeDocumentOnUnloadProperty, value);
        }

        public ICommand OpenDocumentCommand { get; private set; }

        public ICommand CloseDocumentCommand { get; private set; }

        public ICommand ZoomInCommand { get; private set; }

        public ICommand ZoomOutCommand { get; private set; }

        public ICommand ClockwiseRotateCommand { get; private set; }

        public ICommand CounterClockwiseRotateCommand { get; private set; }

        public ICommand NextPageCommand { get; private set; }

        public ICommand PreviousPageCommand { get; private set; }

        public ICommand SetPageNumberCommand { get; private set; }

        public ICommand SetZoomFactorCommand { get; private set; }

        public ICommand SetZoomModeCommand { get; private set; }

        public ICommand ScrollCommand { get; private set; }

        public ICommand NextViewCommand { get; private set; }

        public ICommand PreviousViewCommand { get; private set; }

        public ICommand ShowFindTextCommand { get; private set; }

        public ICommand FindTextCommand { get; private set; }

        public ICommand NavigateCommand { get; private set; }

        protected internal DocumentPresenterControl DocumentPresenter { get; private set; }

        protected Locker ThrowOpenFileExceptionLocker { get; private set; }

        double IDocumentViewerControl.VerticalPageSpacing =>
            0.0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentViewerControl.<>c <>9 = new DocumentViewerControl.<>c();
            public static Func<DevExpress.Xpf.DocumentViewer.IDocument, IEnumerable<IPage>> <>9__50_0;
            public static Func<IEnumerable<IPage>, int> <>9__50_1;
            public static Func<int> <>9__50_2;
            public static Action<CommandProvider> <>9__51_0;
            public static Action<CommandProvider> <>9__223_0;
            public static Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> <>9__237_0;
            public static Func<bool> <>9__237_1;
            public static Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> <>9__259_0;
            public static Func<bool> <>9__259_1;
            public static Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> <>9__261_0;
            public static Func<bool> <>9__261_1;
            public static Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> <>9__269_0;
            public static Func<bool> <>9__269_1;
            public static Action<DocumentPresenterControl> <>9__277_0;
            public static Action<DocumentPresenterControl> <>9__302_0;
            public static Func<DevExpress.Xpf.DocumentViewer.IDocument, bool> <>9__303_0;
            public static Func<bool> <>9__303_1;
            public static Action<DocumentMapSettings> <>9__305_0;

            internal void <.cctor>b__39_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnDocumentSourceChanged(args.OldValue, args.NewValue);
            }

            internal void <.cctor>b__39_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnZoomFactorChangedInternal((double) args.OldValue, (double) args.NewValue);
            }

            internal void <.cctor>b__39_10(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnBarsTemplateChangedInternal((DataTemplate) args.NewValue);
            }

            internal void <.cctor>b__39_11(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnRibbonTemplateChangedInternal((DataTemplate) args.NewValue);
            }

            internal void <.cctor>b__39_12(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).PresenterTemplateChanged((DataTemplate) args.NewValue);
            }

            internal void <.cctor>b__39_13(DocumentViewerControl owner, double oldValue, double newValue)
            {
                owner.HorizontalPageSpacingPropertyChanged(oldValue, newValue);
            }

            internal void <.cctor>b__39_14(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnDocumentChangedInternal((DevExpress.Xpf.DocumentViewer.IDocument) args.OldValue, (DevExpress.Xpf.DocumentViewer.IDocument) args.NewValue);
            }

            internal void <.cctor>b__39_15(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnActualBehaviorProviderChangedInternal((BehaviorProvider) args.OldValue, (BehaviorProvider) args.NewValue);
            }

            internal void <.cctor>b__39_16(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnActualCommandProviderChangedInternal((CommandProvider) args.OldValue, (CommandProvider) args.NewValue);
            }

            internal void <.cctor>b__39_17(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnSearchControlVisibleChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__39_18(DocumentViewerControl owner, DocumentMapSettings oldValue, DocumentMapSettings newValue)
            {
                owner.ActualDocumentMapSettingsChanged(oldValue, newValue);
            }

            internal object <.cctor>b__39_2(DependencyObject obj, object arg) => 
                ((DocumentViewerControl) obj).CoerceZoomFactor(arg);

            internal void <.cctor>b__39_3(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnZoomModeChangedInternal((ZoomMode) args.OldValue, (ZoomMode) args.NewValue);
            }

            internal void <.cctor>b__39_4(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnPageRotationChangedInternal((Rotation) args.OldValue, (Rotation) args.NewValue);
            }

            internal void <.cctor>b__39_5(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnCurrentPageNumberChangedInternal((int) args.OldValue, (int) args.NewValue);
            }

            internal object <.cctor>b__39_6(DependencyObject obj, object arg) => 
                ((DocumentViewerControl) obj).CoerceCurrentPageNumber(arg);

            internal void <.cctor>b__39_7(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnBehaviorProviderChangedInternal((BehaviorProvider) args.OldValue, (BehaviorProvider) args.NewValue);
            }

            internal void <.cctor>b__39_8(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnCommandProviderChangedInternal((CommandProvider) args.OldValue, (CommandProvider) args.NewValue);
            }

            internal void <.cctor>b__39_9(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DocumentViewerControl) obj).OnCommandBarStyleChangedInternal((CommandBarStyle) args.OldValue, (CommandBarStyle) args.NewValue);
            }

            internal void <ActualDocumentMapSettingsChanged>b__305_0(DocumentMapSettings x)
            {
                x.Release();
            }

            internal bool <CanNavigateNextPage>b__259_0(DevExpress.Xpf.DocumentViewer.IDocument x) => 
                x.IsLoaded;

            internal bool <CanNavigateNextPage>b__259_1() => 
                false;

            internal bool <CanNavigatePreviousPage>b__261_0(DevExpress.Xpf.DocumentViewer.IDocument x) => 
                x.IsLoaded;

            internal bool <CanNavigatePreviousPage>b__261_1() => 
                false;

            internal bool <CanScroll>b__269_0(DevExpress.Xpf.DocumentViewer.IDocument x) => 
                x.IsLoaded;

            internal bool <CanScroll>b__269_1() => 
                false;

            internal bool <CoerceCurrentPageNumber>b__303_0(DevExpress.Xpf.DocumentViewer.IDocument x) => 
                x.IsLoaded;

            internal bool <CoerceCurrentPageNumber>b__303_1() => 
                false;

            internal void <DetachDocumentPresenterControl>b__302_0(DocumentPresenterControl x)
            {
                x.BehaviorProvider = null;
                x.Document = null;
            }

            internal bool <IsDocumentContainPages>b__237_0(DevExpress.Xpf.DocumentViewer.IDocument x) => 
                x.IsLoaded;

            internal bool <IsDocumentContainPages>b__237_1() => 
                false;

            internal void <OnActualCommandProviderChangedInternal>b__51_0(CommandProvider x)
            {
                x.DocumentViewer = null;
            }

            internal IEnumerable<IPage> <OnDocumentChangedInternal>b__50_0(DevExpress.Xpf.DocumentViewer.IDocument x) => 
                x.Pages;

            internal int <OnDocumentChangedInternal>b__50_1(IEnumerable<IPage> x) => 
                x.Count<IPage>();

            internal int <OnDocumentChangedInternal>b__50_2() => 
                0;

            internal void <SetZoomModeAndFactor>b__223_0(CommandProvider x)
            {
                x.UpdateZoomCommand();
            }

            internal void <ShowFindText>b__277_0(DocumentPresenterControl x)
            {
                x.Focus();
            }
        }
    }
}

