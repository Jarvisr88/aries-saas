namespace DevExpress.Xpf.Printing
{
    using DevExpress.Data.Browsing;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class DocumentPreviewModelBase : PreviewModelBase, IDocumentPreviewModel, IPreviewModel, INotifyPropertyChanged
    {
        private readonly DevExpress.Xpf.Printing.InputController inputController;
        private int currentPageIndex = -1;
        private bool isDocumentMapVisible;
        private bool isParametersPanelVisible;
        private bool isSearchPanelVisible;
        private bool requiredScrollingToHighlightedElement;
        private bool isDocumentMapVisibilityToggledOn = true;
        private bool isOpenButtonVisible = true;
        private bool isSaveButtonVisible = true;
        private bool isSendVisible = true;
        private DocumentMapTreeViewNode documentMapSelectedNode;
        private HighlightingPriority highLightingPriority;
        private BrickInfo foundBrickInfo;
        private IExportOptionsConfiguratorService exportOptionsConfiguratorService;

        public event EventHandler<PreviewClickEventArgs> PreviewClick;

        public event EventHandler<PreviewClickEventArgs> PreviewDoubleClick;

        public event EventHandler<PreviewClickEventArgs> PreviewMouseMove;

        public DocumentPreviewModelBase()
        {
            this.CreateCommands();
            DocumentPreviewInputController controller1 = new DocumentPreviewInputController();
            controller1.Model = this;
            this.inputController = controller1;
        }

        protected virtual void BeginExport(ExportFormat format)
        {
        }

        private static void BringIntoView(FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            IScrollInfo info = LayoutHelper.FindParentObject<IScrollInfo>(element);
            if (info != null)
            {
                info.MakeVisible(element, Rect.Empty);
            }
        }

        protected virtual bool CanExport(object parameter)
        {
            ExportFormat exportFormatByExportParameter = this.GetExportFormatByExportParameter(parameter);
            bool flag = (this.PageCount > 0) && (this.DocumentExportOptions != null);
            if (!flag)
            {
                return false;
            }
            AvailableExportModes documentExportModes = this.DocumentExportModes;
            switch (exportFormatByExportParameter)
            {
                case ExportFormat.Pdf:
                    return flag;

                case ExportFormat.Htm:
                    return (flag && ((documentExportModes != null) && documentExportModes.Html.Any<HtmlExportMode>()));

                case ExportFormat.Mht:
                    return (flag && ((documentExportModes != null) && !documentExportModes.Html.Any<HtmlExportMode>()));

                case ExportFormat.Rtf:
                    return (flag && ((documentExportModes != null) && documentExportModes.Rtf.Any<RtfExportMode>()));

                case ExportFormat.Xls:
                    return (flag && ((documentExportModes != null) && documentExportModes.Xls.Any<XlsExportMode>()));

                case ExportFormat.Xlsx:
                    return (flag && ((documentExportModes != null) && documentExportModes.Xlsx.Any<XlsxExportMode>()));

                case ExportFormat.Csv:
                    return flag;

                case ExportFormat.Txt:
                    return flag;

                case ExportFormat.Image:
                    return (flag && ((documentExportModes != null) && documentExportModes.Image.Any<ImageExportMode>()));

                case ExportFormat.Xps:
                    return flag;

                case ExportFormat.Prnx:
                    return flag;
            }
            throw new ArgumentException("format");
        }

        private bool CanGoToFirstPage(object parameter) => 
            this.CurrentPageIndex > 0;

        private bool CanGoToLastPage(object parameter) => 
            this.CurrentPageIndex < (this.PageCount - 1);

        private bool CanGoToNextPage(object parameter) => 
            (this.CurrentPageIndex + 1) < this.PageCount;

        private bool CanGoToPreviousPage(object parameter) => 
            this.CurrentPageIndex > 0;

        protected abstract bool CanOpen(object parameter);
        protected abstract bool CanPageSetup(object parameter);
        protected abstract bool CanPrint(object parameter);
        protected abstract bool CanPrintDirect(object parameter);
        protected abstract bool CanSave(object parameter);
        protected abstract bool CanScale(object parameter);
        protected abstract bool CanSend(object parameter);
        protected abstract bool CanSetWatermark(object parameter);
        protected abstract bool CanShowDocumentMap(object parameter);
        protected abstract bool CanShowParametersPanel(object parameter);
        protected abstract bool CanShowSearchPanel(object parameter);
        protected abstract bool CanStop(object parameter);
        private void CreateCommands()
        {
            base.commands.Add(PrintingSystemCommand.Print, DelegateCommandFactory.Create<object>(parameter => base.SafeCommandHandler(() => this.Print(parameter)), new Func<object, bool>(this.CanPrint), false));
            base.commands.Add(PrintingSystemCommand.PageSetup, DelegateCommandFactory.Create<object>(new Action<object>(this.PageSetup), new Func<object, bool>(this.CanPageSetup), false));
            base.commands.Add(PrintingSystemCommand.ShowFirstPage, DelegateCommandFactory.Create<object>(new Action<object>(this.GoToFirstPage), new Func<object, bool>(this.CanGoToFirstPage), false));
            base.commands.Add(PrintingSystemCommand.ShowPrevPage, DelegateCommandFactory.Create<object>(new Action<object>(this.GoToPreviousPage), new Func<object, bool>(this.CanGoToPreviousPage), false));
            base.commands.Add(PrintingSystemCommand.ShowNextPage, DelegateCommandFactory.Create<object>(new Action<object>(this.GoToNextPage), new Func<object, bool>(this.CanGoToNextPage), false));
            base.commands.Add(PrintingSystemCommand.ShowLastPage, DelegateCommandFactory.Create<object>(new Action<object>(this.GoToLastPage), new Func<object, bool>(this.CanGoToLastPage), false));
            base.commands.Add(PrintingSystemCommand.Scale, DelegateCommandFactory.Create<object>(new Action<object>(this.Scale), new Func<object, bool>(this.CanScale), false));
            base.commands.Add(PrintingSystemCommand.ExportFile, DelegateCommandFactory.Create<object>(parameter => base.SafeCommandHandler(() => this.Export(parameter)), new Func<object, bool>(this.CanExport), false));
            base.commands.Add(PrintingSystemCommand.Watermark, DelegateCommandFactory.Create<object>(new Action<object>(this.SetWatermark), new Func<object, bool>(this.CanSetWatermark), false));
            base.commands.Add(PrintingSystemCommand.StopPageBuilding, DelegateCommandFactory.Create<object>(new Action<object>(this.Stop), new Func<object, bool>(this.CanStop), false));
            base.commands.Add(PrintingSystemCommand.DocumentMap, DelegateCommandFactory.Create<object>(new Action<object>(this.ToggleIsDocumentMapVisible), new Func<object, bool>(this.CanShowDocumentMap), false));
            base.commands.Add(PrintingSystemCommand.Parameters, DelegateCommandFactory.Create<object>(new Action<object>(this.ToggleIsParametersPanelVisible), new Func<object, bool>(this.CanShowParametersPanel), false));
            base.commands.Add(PrintingSystemCommand.Find, DelegateCommandFactory.Create<object>(new Action<object>(this.ToggleSearchPanelVisibility), new Func<object, bool>(this.CanShowSearchPanel), false));
            base.commands.Add(PrintingSystemCommand.PrintDirect, DelegateCommandFactory.Create<object>(parameter => base.SafeCommandHandler(() => this.PrintDirect(parameter)), new Func<object, bool>(this.CanPrintDirect), false));
            base.commands.Add(PrintingSystemCommand.SendFile, DelegateCommandFactory.Create<object>(parameter => base.SafeCommandHandler(() => this.Send(parameter)), new Func<object, bool>(this.CanSend), false));
            base.commands.Add(PrintingSystemCommand.Open, DelegateCommandFactory.Create<object>(parameter => base.SafeCommandHandler(() => this.Open(parameter)), new Func<object, bool>(this.CanOpen), false));
            base.commands.Add(PrintingSystemCommand.Save, DelegateCommandFactory.Create<object>(parameter => base.SafeCommandHandler(() => this.Save(parameter)), new Func<object, bool>(this.CanSave), false));
        }

        protected virtual void EndExport()
        {
        }

        protected abstract void Export(ExportFormat format);
        protected virtual void Export(object parameter)
        {
            this.Export(parameter, new Action<ExportFormat>(this.BeginExport), new Action<ExportFormat>(this.Export), new Action(this.EndExport));
        }

        private void Export(object parameter, Action<ExportFormat> beginExport, Action<ExportFormat> doExport, Action endExport)
        {
            ExportFormat exportFormatByExportParameter = this.GetExportFormatByExportParameter(parameter);
            this.DefaultExportFormat = exportFormatByExportParameter;
            this.ExportOptionsConfiguratorService.Completed += new EventHandler<AsyncCompletedEventArgs>(this.ExportOptionsConfiguratorService_Completed);
            this.ExportOptionsConfiguratorService.Context = new ExportOptionContext(exportFormatByExportParameter, beginExport, doExport, endExport);
            this.ExportOptionsConfiguratorService.Configure(this.DocumentExportOptions.GetByFormat(exportFormatByExportParameter), this.DocumentExportOptions.PrintPreview, this.DocumentExportModes, this.DocumentHiddenOptions);
        }

        private void ExportOptionsConfiguratorService_Completed(object sender, AsyncCompletedEventArgs e)
        {
            this.ExportOptionsConfiguratorService.Completed -= new EventHandler<AsyncCompletedEventArgs>(this.ExportOptionsConfiguratorService_Completed);
            if (!e.Cancelled && (e.Error == null))
            {
                ExportOptionContext context = this.ExportOptionsConfiguratorService.Context;
                ExportFormat format = context.Format;
                if (context.BeginExportAction != null)
                {
                    context.BeginExportAction(format);
                }
                if (context.ExportAction != null)
                {
                    context.ExportAction(format);
                }
                if (context.EndExportAction != null)
                {
                    context.EndExportAction();
                }
            }
        }

        protected virtual DataContext GetDataContext() => 
            null;

        protected ExportFormat GetExportFormatByExportParameter(object parameter) => 
            (parameter == null) ? this.DefaultExportFormat : ((ExportFormat) Enum.Parse(typeof(ExportFormat), (string) parameter, false));

        private FrameworkElement GetHitTestResult(MouseEventArgs e, FrameworkElement source)
        {
            UIElement topLevelVisual = LayoutHelper.GetTopLevelVisual(source);
            if (topLevelVisual == null)
            {
                return null;
            }
            FrameworkElement result = null;
            HitTestResultCallback resultCallback = <>c.<>9__128_1;
            if (<>c.<>9__128_1 == null)
            {
                HitTestResultCallback local1 = <>c.<>9__128_1;
                resultCallback = <>c.<>9__128_1 = res => HitTestResultBehavior.Continue;
            }
            VisualTreeHelper.HitTest(topLevelVisual, delegate (DependencyObject d) {
                FrameworkElement control = d as FrameworkElement;
                if ((control != null) && (this.IsHitTestResult(control) && control.FindIsInParents(source)))
                {
                    result = control;
                }
                return HitTestFilterBehavior.Continue;
            }, resultCallback, new PointHitTestParameters(e.GetPosition(topLevelVisual)));
            return result;
        }

        protected virtual void GoToFirstPage(object parameter)
        {
            this.CurrentPageIndex = 0;
        }

        private void GoToLastPage(object parameter)
        {
            this.CurrentPageIndex = this.PageCount - 1;
        }

        private void GoToNextPage(object parameter)
        {
            int currentPageIndex = this.CurrentPageIndex;
            this.CurrentPageIndex = currentPageIndex + 1;
        }

        private void GoToPreviousPage(object parameter)
        {
            int currentPageIndex = this.CurrentPageIndex;
            this.CurrentPageIndex = currentPageIndex - 1;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void HandlePreviewDoubleClick(MouseEventArgs e, FrameworkElement source)
        {
            if (this.PreviewDoubleClick != null)
            {
                FrameworkElement hitTestResult = this.GetHitTestResult(e, source);
                string elementTag = (hitTestResult != null) ? PreviewClickHelper.GetTag(hitTestResult) : null;
                this.PreviewDoubleClick(this, new PreviewClickEventArgs(elementTag, hitTestResult));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void HandlePreviewMouseLeftButtonDown(MouseButtonEventArgs e, FrameworkElement source)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void HandlePreviewMouseLeftButtonUp(MouseButtonEventArgs e, FrameworkElement source)
        {
            FrameworkElement hitTestResult = this.GetHitTestResult(e, source);
            if (hitTestResult != null)
            {
                string navigationPair = PreviewClickHelper.GetNavigationPair(hitTestResult);
                if (!string.IsNullOrEmpty(navigationPair))
                {
                    int pageIndex = Convert.ToInt32(DocumentMapTreeViewNodeHelper.GetPageIndexByTag(navigationPair));
                    if ((pageIndex >= 0) && (pageIndex < this.PageCount))
                    {
                        this.FoundBrickInfo = new BrickInfo(navigationPair, pageIndex);
                    }
                }
            }
            if (this.PreviewClick != null)
            {
                string elementTag = (hitTestResult != null) ? PreviewClickHelper.GetTag(hitTestResult) : null;
                this.PreviewClick(this, new PreviewClickEventArgs(elementTag, hitTestResult));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void HandlePreviewMouseMove(MouseEventArgs e, FrameworkElement source)
        {
            if (this.PreviewMouseMove != null)
            {
                FrameworkElement hitTestResult = this.GetHitTestResult(e, source);
                string elementTag = (hitTestResult != null) ? PreviewClickHelper.GetTag(hitTestResult) : null;
                this.PreviewMouseMove(this, new PreviewClickEventArgs(elementTag, hitTestResult));
            }
        }

        private void HighlightElement(FrameworkElement pageContent, string brickTag)
        {
            if ((this.HighlightingService != null) && (pageContent != null))
            {
                this.HighlightingService.HideHighlighting();
                new OnLoadedScheduler().Schedule(delegate {
                    Predicate<FrameworkElement> <>9__1;
                    Predicate<FrameworkElement> predicate = <>9__1;
                    if (<>9__1 == null)
                    {
                        Predicate<FrameworkElement> local1 = <>9__1;
                        predicate = <>9__1 = x => ((string) x.Tag) == brickTag;
                    }
                    FrameworkElement target = LayoutHelper.FindElement(pageContent, predicate);
                    if (target != null)
                    {
                        if (this.requiredScrollingToHighlightedElement)
                        {
                            this.requiredScrollingToHighlightedElement = false;
                            new OnLoadedScheduler().Schedule(() => BringIntoView(target), target);
                        }
                        this.HighlightingService.ShowHighlighting(pageContent, target);
                    }
                }, pageContent);
            }
        }

        protected virtual bool IsHitTestResult(FrameworkElement control) => 
            false;

        protected virtual void OnCurrentPageIndexChanged()
        {
        }

        protected abstract void Open(object parameter);
        protected abstract void PageSetup(object parameter);
        protected abstract void Print(object parameter);
        protected abstract void PrintDirect(object parameter);
        protected override void ProcessPageContent(FrameworkElement pageContent)
        {
            switch (this.highLightingPriority)
            {
                case HighlightingPriority.None:
                    if (this.HighlightingService == null)
                    {
                        break;
                    }
                    this.HighlightingService.HideHighlighting();
                    return;

                case HighlightingPriority.DocumentMap:
                    if ((this.DocumentMapSelectedNode == null) || (this.CurrentPageIndex != this.DocumentMapSelectedNode.PageIndex))
                    {
                        break;
                    }
                    this.HighlightElement(pageContent, this.DocumentMapSelectedNode.AssociatedElementTag);
                    return;

                case HighlightingPriority.Search:
                    if ((this.FoundBrickInfo == null) || string.IsNullOrEmpty(this.FoundBrickInfo.BrickTag))
                    {
                        break;
                    }
                    this.HighlightElement(pageContent, this.FoundBrickInfo.BrickTag);
                    return;

                default:
                    throw new NotSupportedException("HighLightingPriority");
            }
        }

        protected void RaiseNavigationCommandsCanExecuteChanged()
        {
            base.commands[PrintingSystemCommand.ShowFirstPage].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.ShowPrevPage].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.ShowNextPage].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.ShowLastPage].RaiseCanExecuteChanged();
        }

        protected abstract void Save(object parameter);
        protected abstract void Scale(object parameter);
        protected abstract void Send(object parameter);
        protected internal void SetCurrentPageIndex(int value)
        {
            if (this.currentPageIndex != value)
            {
                this.currentPageIndex = value;
                this.OnCurrentPageIndexChanged();
                base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(DocumentPreviewModelBase.get_CurrentPageIndex)), new ParameterExpression[0]));
                base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(DocumentPreviewModelBase.get_CurrentPageNumber)), new ParameterExpression[0]));
                base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
                base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageViewWidth)), new ParameterExpression[0]));
                base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageViewHeight)), new ParameterExpression[0]));
                this.RaiseNavigationCommandsCanExecuteChanged();
            }
        }

        protected abstract void SetWatermark(object parameter);
        protected Stream ShowSaveExportedFileDialog(ExportFormat format)
        {
            ExportOptionsBase byFormat = this.DocumentExportOptions.GetByFormat(format);
            ExportOptionsControllerBase controllerByOptions = ExportOptionsControllerBase.GetControllerByOptions(byFormat);
            int filterIndex = Array.IndexOf<string>(controllerByOptions.FileExtensions, controllerByOptions.GetFileExtension(byFormat)) + 1;
            return this.DialogService.ShowSaveFileDialog(PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title), controllerByOptions.Filter, filterIndex, null, this.DocumentExportOptions.PrintPreview.DefaultFileName);
        }

        protected abstract void Stop(object parameter);
        private void ToggleIsDocumentMapVisible(object parameter)
        {
            this.IsDocumentMapVisible = !this.IsDocumentMapVisible;
            if (this.IsDocumentMapVisible || (this.HighlightingService == null))
            {
                base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
            }
            else if (this.highLightingPriority == HighlightingPriority.DocumentMap)
            {
                this.HighlightingService.HideHighlighting();
                this.highLightingPriority = HighlightingPriority.None;
            }
        }

        private void ToggleIsParametersPanelVisible(object parameter)
        {
            this.IsParametersPanelVisible = !this.IsParametersPanelVisible;
        }

        private void ToggleSearchPanelVisibility(object parameter)
        {
            this.IsSearchPanelVisible = !this.IsSearchPanelVisible;
            if (!this.IsSearchPanelVisible && ((this.HighlightingService != null) && (this.highLightingPriority == HighlightingPriority.Search)))
            {
                this.HighlightingService.HideHighlighting();
                this.highLightingPriority = HighlightingPriority.None;
            }
        }

        public override DevExpress.Xpf.Printing.InputController InputController =>
            this.inputController;

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool IsDocumentMapToggledDuringDocumentCreating { get; set; }

        public abstract bool IsEmptyDocument { get; }

        public abstract bool ProgressVisibility { get; }

        public abstract int ProgressMaximum { get; }

        public abstract int ProgressValue { get; }

        public abstract bool ProgressMarqueeVisibility { get; }

        public abstract DocumentMapTreeViewNode DocumentMapRootNode { get; }

        public abstract DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel { get; }

        protected ExportFormat DefaultExportFormat
        {
            get => 
                ExportFormatConverter.ToExportFormat(this.DocumentExportOptions.PrintPreview.DefaultExportFormat);
            set => 
                this.DocumentExportOptions.PrintPreview.DefaultExportFormat = ExportFormatConverter.ToExportCommand(value);
        }

        protected ExportFormat DefaultSendFormat
        {
            get => 
                ExportFormatConverter.ToExportFormat(this.DocumentExportOptions.PrintPreview.DefaultSendFormat);
            set => 
                this.DocumentExportOptions.PrintPreview.DefaultSendFormat = ExportFormatConverter.ToSendCommand(value);
        }

        public int CurrentPageIndex
        {
            get => 
                this.currentPageIndex;
            set
            {
                if ((value >= 0) && (value < this.PageCount))
                {
                    this.SetCurrentPageIndex(value);
                }
                else
                {
                    this.DialogService.ShowError(string.Format(PreviewLocalizer.GetString(PreviewStringId.Msg_GoToNonExistentPage), value + 1), PrintingLocalizer.GetString(PrintingStringId.Error));
                }
            }
        }

        public virtual bool IsScaleVisible =>
            false;

        public virtual bool IsSearchVisible =>
            false;

        public virtual bool IsSetWatermarkVisible =>
            false;

        public int CurrentPageNumber
        {
            get => 
                this.CurrentPageIndex + 1;
            set => 
                this.CurrentPageIndex = value - 1;
        }

        protected virtual IHighlightingService HighlightingService =>
            null;

        public virtual bool IsDocumentMapVisible
        {
            get => 
                this.isDocumentMapVisible;
            set
            {
                if (!this.IsDocumentMapToggledDuringDocumentCreating)
                {
                    this.isDocumentMapVisibilityToggledOn = value;
                }
                if (!((!this.CanShowDocumentMap(null) || !this.isDocumentMapVisibilityToggledOn) & value))
                {
                    this.isDocumentMapVisible = value;
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsDocumentMapVisible)), new ParameterExpression[0]));
                }
            }
        }

        public virtual bool IsOpenButtonVisible
        {
            get => 
                this.isOpenButtonVisible;
            set => 
                this.isOpenButtonVisible = value;
        }

        public virtual bool IsSaveButtonVisible
        {
            get => 
                this.isSaveButtonVisible;
            set => 
                this.isSaveButtonVisible = value;
        }

        public virtual bool IsSendVisible
        {
            get => 
                this.isSendVisible;
            set => 
                this.isSendVisible = value;
        }

        public virtual bool IsParametersPanelVisible
        {
            get => 
                this.isParametersPanelVisible;
            set
            {
                if (!(!this.CanShowParametersPanel(null) & value))
                {
                    this.isParametersPanelVisible = value;
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsParametersPanelVisible)), new ParameterExpression[0]));
                }
            }
        }

        public bool IsSearchPanelVisible
        {
            get => 
                this.isSearchPanelVisible;
            set
            {
                if (!(!this.CanShowSearchPanel(null) & value))
                {
                    this.isSearchPanelVisible = value;
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsSearchPanelVisible)), new ParameterExpression[0]));
                }
            }
        }

        public DocumentMapTreeViewNode DocumentMapSelectedNode
        {
            get => 
                this.documentMapSelectedNode;
            set
            {
                this.documentMapSelectedNode = value;
                if (this.HighlightingService != null)
                {
                    this.HighlightingService.HideHighlighting();
                }
                if (value != null)
                {
                    this.requiredScrollingToHighlightedElement = true;
                    this.highLightingPriority = HighlightingPriority.DocumentMap;
                    this.CurrentPageIndex = (this.DocumentMapSelectedNode.PageIndex >= 0) ? this.DocumentMapSelectedNode.PageIndex : 0;
                    base.RaisePropertyChanged<DocumentMapTreeViewNode>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapTreeViewNode>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(DocumentPreviewModelBase.get_DocumentMapSelectedNode)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public BrickInfo FoundBrickInfo
        {
            get => 
                this.foundBrickInfo;
            set
            {
                this.foundBrickInfo = value;
                if (this.foundBrickInfo == null)
                {
                    this.highLightingPriority = HighlightingPriority.None;
                }
                else
                {
                    if (string.IsNullOrEmpty(this.foundBrickInfo.BrickTag))
                    {
                        this.highLightingPriority = HighlightingPriority.None;
                    }
                    else
                    {
                        this.requiredScrollingToHighlightedElement = true;
                        this.highLightingPriority = HighlightingPriority.Search;
                        this.CurrentPageIndex = (this.foundBrickInfo.PageIndex >= 0) ? this.foundBrickInfo.PageIndex : 0;
                    }
                    base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DocumentPreviewModelBase)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
                }
            }
        }

        public ICommand PrintCommand =>
            base.commands[PrintingSystemCommand.Print];

        public ICommand FirstPageCommand =>
            base.commands[PrintingSystemCommand.ShowFirstPage];

        public ICommand PreviousPageCommand =>
            base.commands[PrintingSystemCommand.ShowPrevPage];

        public ICommand NextPageCommand =>
            base.commands[PrintingSystemCommand.ShowNextPage];

        public ICommand LastPageCommand =>
            base.commands[PrintingSystemCommand.ShowLastPage];

        public ICommand ExportCommand =>
            base.commands[PrintingSystemCommand.ExportFile];

        public ICommand WatermarkCommand =>
            base.commands[PrintingSystemCommand.Watermark];

        public ICommand StopCommand =>
            base.commands[PrintingSystemCommand.StopPageBuilding];

        public ICommand ToggleDocumentMapCommand =>
            base.commands[PrintingSystemCommand.DocumentMap];

        public ICommand ToggleParametersPanelCommand =>
            base.commands[PrintingSystemCommand.Parameters];

        public ICommand PageSetupCommand =>
            base.commands[PrintingSystemCommand.PageSetup];

        public ICommand ScaleCommand =>
            base.commands[PrintingSystemCommand.Scale];

        public ICommand ToggleSearchPanelCommand =>
            base.commands[PrintingSystemCommand.Find];

        public ICommand PrintDirectCommand =>
            base.commands[PrintingSystemCommand.PrintDirect];

        public ICommand SendCommand =>
            base.commands[PrintingSystemCommand.SendFile];

        public ICommand OpenCommand =>
            base.commands[PrintingSystemCommand.Open];

        public ICommand SaveCommand =>
            base.commands[PrintingSystemCommand.Save];

        protected abstract ExportOptions DocumentExportOptions { get; set; }

        protected abstract AvailableExportModes DocumentExportModes { get; }

        protected abstract List<ExportOptionKind> DocumentHiddenOptions { get; }

        protected virtual IExportOptionsConfiguratorService ExportOptionsConfiguratorService
        {
            get
            {
                this.exportOptionsConfiguratorService ??= new DevExpress.Xpf.Printing.Native.ExportOptionsConfiguratorService();
                return this.exportOptionsConfiguratorService;
            }
            set => 
                this.exportOptionsConfiguratorService = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPreviewModelBase.<>c <>9 = new DocumentPreviewModelBase.<>c();
            public static HitTestResultCallback <>9__128_1;

            internal HitTestResultBehavior <GetHitTestResult>b__128_1(HitTestResult res) => 
                HitTestResultBehavior.Continue;
        }
    }
}

