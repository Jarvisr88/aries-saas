namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class CommandProvider : ViewModelBase
    {
        private ObservableCollection<IControllerAction> actions;
        private ObservableCollection<IControllerAction> ribbonActions;
        private ObservableCollection<IControllerAction> contextMenuActions;
        private IDocumentViewerControl documentViewer;

        protected virtual ICommand CreateZoomModeAndZoomFactorItem(string dllName)
        {
            ObservableCollection<CommandToggleButton> zoomModeAndFactorsItems = new ObservableCollection<CommandToggleButton>();
            CommandCheckItems items1 = new CommandCheckItems();
            items1.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomCaption);
            items1.Hint = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoomDescription);
            items1.Group = DocumentViewerLocalizer.GetString(DocumentViewerStringId.ZoomRibbonGroupCaption);
            CommandCheckItems items2 = items1;
            Action executeMethod = <>c.<>9__122_0;
            if (<>c.<>9__122_0 == null)
            {
                Action local1 = <>c.<>9__122_0;
                executeMethod = <>c.<>9__122_0 = delegate {
                };
            }
            items2.Command = DelegateCommandFactory.Create(executeMethod, delegate {
                Func<CommandToggleButton, bool> predicate = <>c.<>9__122_2;
                if (<>c.<>9__122_2 == null)
                {
                    Func<CommandToggleButton, bool> local1 = <>c.<>9__122_2;
                    predicate = <>c.<>9__122_2 = x => x.CanExecute(null);
                }
                return zoomModeAndFactorsItems.Any<CommandToggleButton>(predicate);
            });
            CommandCheckItems local2 = items2;
            local2.Items = zoomModeAndFactorsItems;
            local2.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, @"\Images\Zoom_16x16.png");
            local2.LargeGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, @"\Images\Zoom_32x32.png");
            CommandCheckItems items = local2;
            DelegateCommand<double> setZoomFactorCommand = DelegateCommandFactory.Create<double>(delegate (double x) {
                this.SetZoomFactorCommandInternal.Execute(x);
                this.UpdateZoomCommand();
            }, x => this.SetZoomFactorCommandInternal.CanExecute(x));
            DelegateCommand<ZoomMode> setZoomModeCommand = DelegateCommandFactory.Create<ZoomMode>(delegate (ZoomMode x) {
                this.SetZoomModeCommandInternal.Execute(x);
                this.UpdateZoomCommand();
            }, x => this.SetZoomModeCommandInternal.CanExecute(x));
            CommandSetZoomFactorAndModeItem item = new CommandSetZoomFactorAndModeItem();
            item.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom25Caption);
            item.Command = new CommandWrapper(() => setZoomFactorCommand);
            item.ZoomFactor = 0.25;
            item.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item);
            CommandSetZoomFactorAndModeItem item2 = new CommandSetZoomFactorAndModeItem();
            item2.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom50Caption);
            item2.Command = new CommandWrapper(() => setZoomFactorCommand);
            item2.ZoomFactor = 0.5;
            item2.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item2);
            CommandSetZoomFactorAndModeItem item3 = new CommandSetZoomFactorAndModeItem();
            item3.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom75Caption);
            item3.Command = new CommandWrapper(() => setZoomFactorCommand);
            item3.ZoomFactor = 0.75;
            item3.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item3);
            CommandSetZoomFactorAndModeItem item4 = new CommandSetZoomFactorAndModeItem();
            item4.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom100Caption);
            item4.Command = new CommandWrapper(() => setZoomFactorCommand);
            item4.ZoomFactor = 1.0;
            item4.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item4);
            CommandSetZoomFactorAndModeItem item5 = new CommandSetZoomFactorAndModeItem();
            item5.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom125Caption);
            item5.Command = new CommandWrapper(() => setZoomFactorCommand);
            item5.ZoomFactor = 1.25;
            item5.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item5);
            CommandSetZoomFactorAndModeItem item6 = new CommandSetZoomFactorAndModeItem();
            item6.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom150Caption);
            item6.Command = new CommandWrapper(() => setZoomFactorCommand);
            item6.ZoomFactor = 1.5;
            item6.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item6);
            CommandSetZoomFactorAndModeItem item7 = new CommandSetZoomFactorAndModeItem();
            item7.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom200Caption);
            item7.Command = new CommandWrapper(() => setZoomFactorCommand);
            item7.ZoomFactor = 2.0;
            item7.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item7);
            CommandSetZoomFactorAndModeItem item8 = new CommandSetZoomFactorAndModeItem();
            item8.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom400Caption);
            item8.Command = new CommandWrapper(() => setZoomFactorCommand);
            item8.ZoomFactor = 4.0;
            item8.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item8);
            CommandSetZoomFactorAndModeItem item9 = new CommandSetZoomFactorAndModeItem();
            item9.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandZoom500Caption);
            item9.Command = new CommandWrapper(() => setZoomFactorCommand);
            item9.ZoomFactor = 5.0;
            item9.GroupIndex = 1;
            zoomModeAndFactorsItems.Add(item9);
            CommandSetZoomFactorAndModeItem item10 = new CommandSetZoomFactorAndModeItem();
            item10.IsSeparator = true;
            zoomModeAndFactorsItems.Add(item10);
            CommandSetZoomFactorAndModeItem item11 = new CommandSetZoomFactorAndModeItem();
            item11.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandSetActualSizeZoomModeCaption);
            item11.Command = new CommandWrapper(() => setZoomModeCommand);
            item11.ZoomMode = ZoomMode.ActualSize;
            item11.KeyGesture = new KeyGesture(Key.D1, ModifierKeys.Control);
            item11.GroupIndex = 2;
            zoomModeAndFactorsItems.Add(item11);
            CommandSetZoomFactorAndModeItem item12 = new CommandSetZoomFactorAndModeItem();
            item12.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandSetPageLevelZoomModeCaption);
            item12.Command = new CommandWrapper(() => setZoomModeCommand);
            item12.ZoomMode = ZoomMode.PageLevel;
            item12.KeyGesture = new KeyGesture(Key.D0, ModifierKeys.Control);
            item12.GroupIndex = 2;
            zoomModeAndFactorsItems.Add(item12);
            CommandSetZoomFactorAndModeItem item13 = new CommandSetZoomFactorAndModeItem();
            item13.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandSetFitWidthZoomModeCaption);
            item13.Command = new CommandWrapper(() => setZoomModeCommand);
            item13.ZoomMode = ZoomMode.FitToWidth;
            item13.KeyGesture = new KeyGesture(Key.D2, ModifierKeys.Control);
            item13.GroupIndex = 2;
            zoomModeAndFactorsItems.Add(item13);
            CommandSetZoomFactorAndModeItem item14 = new CommandSetZoomFactorAndModeItem();
            item14.Caption = DocumentViewerLocalizer.GetString(DocumentViewerStringId.CommandSetFitVisibleZoomModeCaption);
            item14.Command = new CommandWrapper(() => setZoomModeCommand);
            item14.ZoomMode = ZoomMode.FitToVisible;
            item14.KeyGesture = new KeyGesture(Key.D3, ModifierKeys.Control);
            item14.GroupIndex = 2;
            zoomModeAndFactorsItems.Add(item14);
            return items;
        }

        protected virtual void InitializeElements()
        {
            this.NavigateCommand = new CommandWrapper(() => this.NavigateCommandInternal);
        }

        private void InitializeElementsInternal()
        {
            // Unresolved stack state at '00000233'
        }

        protected virtual void OnCurrentPageNumberChanged(object sender, RoutedEventArgs e)
        {
            this.UpdatePagination();
        }

        protected virtual void OnDocumentChanged(object sender, RoutedEventArgs e)
        {
            this.UpdatePagination();
        }

        protected virtual void OnZoomChanged(object sender, RoutedEventArgs e)
        {
            this.UpdateZoomCommand();
        }

        protected virtual void SubscribeToEvents()
        {
            this.ActualDocumentViewer.Do<DocumentViewerControl>(delegate (DocumentViewerControl x) {
                x.ZoomChanged += new RoutedEventHandler(this.OnZoomChanged);
            });
            this.ActualDocumentViewer.Do<DocumentViewerControl>(delegate (DocumentViewerControl x) {
                x.CurrentPageNumberChanged += new RoutedEventHandler(this.OnCurrentPageNumberChanged);
            });
            this.ActualDocumentViewer.Do<DocumentViewerControl>(delegate (DocumentViewerControl x) {
                x.DocumentChanged += new RoutedEventHandler(this.OnDocumentChanged);
            });
        }

        protected virtual void UnsubscribeFromEvents()
        {
            this.ActualDocumentViewer.Do<DocumentViewerControl>(delegate (DocumentViewerControl x) {
                x.ZoomChanged -= new RoutedEventHandler(this.OnZoomChanged);
            });
            this.ActualDocumentViewer.Do<DocumentViewerControl>(delegate (DocumentViewerControl x) {
                x.CurrentPageNumberChanged -= new RoutedEventHandler(this.OnCurrentPageNumberChanged);
            });
            this.ActualDocumentViewer.Do<DocumentViewerControl>(delegate (DocumentViewerControl x) {
                x.DocumentChanged -= new RoutedEventHandler(this.OnDocumentChanged);
            });
        }

        protected void UpdatePagination()
        {
            // Unresolved stack state at '00000092'
        }

        internal void UpdateZoomCommand()
        {
            (this.ZoomCommand as CommandCheckItems).Do<CommandCheckItems>(x => x.UpdateCheckState(new Func<CommandToggleButton, bool>(this.UpdateZoomFactorCheckState)));
        }

        protected virtual bool UpdateZoomFactorCheckState(CommandToggleButton item)
        {
            if (this.DocumentViewer == null)
            {
                return false;
            }
            Func<bool> fallback = <>c.<>9__128_5;
            if (<>c.<>9__128_5 == null)
            {
                Func<bool> local1 = <>c.<>9__128_5;
                fallback = <>c.<>9__128_5 = () => false;
            }
            return (item as CommandSetZoomFactorAndModeItem).Return<CommandSetZoomFactorAndModeItem, bool>(delegate (CommandSetZoomFactorAndModeItem x) {
                Func<DocumentViewerControl, ZoomMode> evaluator = <>c.<>9__128_1;
                if (<>c.<>9__128_1 == null)
                {
                    Func<DocumentViewerControl, ZoomMode> local1 = <>c.<>9__128_1;
                    evaluator = <>c.<>9__128_1 = z => z.ZoomMode;
                }
                if (((ZoomMode) this.ActualDocumentViewer.Return<DocumentViewerControl, ZoomMode>(evaluator, (<>c.<>9__128_2 ??= () => ZoomMode.Custom))) != ZoomMode.Custom)
                {
                    Func<DocumentViewerControl, ZoomMode> func2 = <>c.<>9__128_3;
                    if (<>c.<>9__128_3 == null)
                    {
                        Func<DocumentViewerControl, ZoomMode> local3 = <>c.<>9__128_3;
                        func2 = <>c.<>9__128_3 = z => z.ZoomMode;
                    }
                    if (x.ZoomMode == ((ZoomMode) this.ActualDocumentViewer.Return<DocumentViewerControl, ZoomMode>(func2, (<>c.<>9__128_4 ??= () => ZoomMode.Custom))))
                    {
                        return true;
                    }
                }
                return x.ZoomFactor.AreClose(this.DocumentViewer.ZoomFactor);
            }, fallback);
        }

        public ICommand OpenDocumentCommand { get; private set; }

        public ICommand CloseDocumentCommand { get; private set; }

        public ICommand NextPageCommand { get; private set; }

        public ICommand PreviousPageCommand { get; private set; }

        public ICommand PaginationCommand { get; private set; }

        public ICommand ZoomInCommand { get; private set; }

        public ICommand ZoomOutCommand { get; private set; }

        public ICommand ZoomCommand { get; private set; }

        public ICommand ClockwiseRotateCommand { get; private set; }

        public ICommand CounterClockwiseRotateCommand { get; private set; }

        public ICommand ScrollCommand { get; private set; }

        public ICommand PreviousViewCommand { get; private set; }

        public ICommand NextViewCommand { get; private set; }

        public ICommand ShowFindTextCommand { get; private set; }

        public ICommand FindNextTextCommand { get; private set; }

        public ICommand FindPreviousTextCommand { get; private set; }

        public ICommand NavigateCommand { get; protected set; }

        public ObservableCollection<IControllerAction> Actions
        {
            get
            {
                ObservableCollection<IControllerAction> actions = this.actions;
                if (this.actions == null)
                {
                    ObservableCollection<IControllerAction> local1 = this.actions;
                    actions = this.actions = new ObservableCollection<IControllerAction>();
                }
                return actions;
            }
            set => 
                base.SetProperty<ObservableCollection<IControllerAction>>(ref this.actions, value, System.Linq.Expressions.Expression.Lambda<Func<ObservableCollection<IControllerAction>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CommandProvider)), (MethodInfo) methodof(CommandProvider.get_Actions)), new ParameterExpression[0]));
        }

        public ObservableCollection<IControllerAction> RibbonActions
        {
            get
            {
                ObservableCollection<IControllerAction> ribbonActions = this.ribbonActions;
                if (this.ribbonActions == null)
                {
                    ObservableCollection<IControllerAction> local1 = this.ribbonActions;
                    ribbonActions = this.ribbonActions = new ObservableCollection<IControllerAction>();
                }
                return ribbonActions;
            }
            set => 
                base.SetProperty<ObservableCollection<IControllerAction>>(ref this.ribbonActions, value, System.Linq.Expressions.Expression.Lambda<Func<ObservableCollection<IControllerAction>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CommandProvider)), (MethodInfo) methodof(CommandProvider.get_RibbonActions)), new ParameterExpression[0]));
        }

        public ObservableCollection<IControllerAction> ContextMenuActions
        {
            get
            {
                ObservableCollection<IControllerAction> contextMenuActions = this.contextMenuActions;
                if (this.contextMenuActions == null)
                {
                    ObservableCollection<IControllerAction> local1 = this.contextMenuActions;
                    contextMenuActions = this.contextMenuActions = new ObservableCollection<IControllerAction>();
                }
                return contextMenuActions;
            }
            internal set => 
                base.SetProperty<ObservableCollection<IControllerAction>>(ref this.contextMenuActions, value, System.Linq.Expressions.Expression.Lambda<Func<ObservableCollection<IControllerAction>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CommandProvider)), (MethodInfo) methodof(CommandProvider.get_ContextMenuActions)), new ParameterExpression[0]));
        }

        protected internal IDocumentViewerControl DocumentViewer
        {
            get => 
                this.documentViewer;
            set
            {
                if (!Equals(this.documentViewer, value))
                {
                    this.UnsubscribeFromEvents();
                    this.documentViewer = value;
                    this.InitializeElementsInternal();
                    this.SubscribeToEvents();
                }
            }
        }

        private DocumentViewerControl ActualDocumentViewer =>
            this.DocumentViewer as DocumentViewerControl;

        protected virtual ICommand OpenDocumentCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__88_0;
                if (<>c.<>9__88_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__88_0;
                    evaluator = <>c.<>9__88_0 = x => x.OpenDocumentCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand CloseDocumentCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__90_0;
                if (<>c.<>9__90_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__90_0;
                    evaluator = <>c.<>9__90_0 = x => x.CloseDocumentCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand PreviousPageCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__92_0;
                if (<>c.<>9__92_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__92_0;
                    evaluator = <>c.<>9__92_0 = x => x.PreviousPageCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand NextPageCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__94_0;
                if (<>c.<>9__94_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__94_0;
                    evaluator = <>c.<>9__94_0 = x => x.NextPageCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand SetPageNumberCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__96_0;
                if (<>c.<>9__96_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__96_0;
                    evaluator = <>c.<>9__96_0 = x => x.SetPageNumberCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand ZoomInCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__98_0;
                if (<>c.<>9__98_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__98_0;
                    evaluator = <>c.<>9__98_0 = x => x.ZoomInCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand ZoomOutCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__100_0;
                if (<>c.<>9__100_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__100_0;
                    evaluator = <>c.<>9__100_0 = x => x.ZoomOutCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand SetZoomModeCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__102_0;
                if (<>c.<>9__102_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__102_0;
                    evaluator = <>c.<>9__102_0 = x => x.SetZoomModeCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand SetZoomFactorCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__104_0;
                if (<>c.<>9__104_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__104_0;
                    evaluator = <>c.<>9__104_0 = x => x.SetZoomFactorCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand ClockwiseRotateCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__106_0;
                if (<>c.<>9__106_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__106_0;
                    evaluator = <>c.<>9__106_0 = x => x.ClockwiseRotateCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand CounterClockwiseRotateCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__108_0;
                if (<>c.<>9__108_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__108_0;
                    evaluator = <>c.<>9__108_0 = x => x.CounterClockwiseRotateCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand ScrollCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__110_0;
                if (<>c.<>9__110_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__110_0;
                    evaluator = <>c.<>9__110_0 = x => x.ScrollCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand PreviousViewCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__112_0;
                if (<>c.<>9__112_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__112_0;
                    evaluator = <>c.<>9__112_0 = x => x.PreviousViewCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand NextViewCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__114_0;
                if (<>c.<>9__114_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__114_0;
                    evaluator = <>c.<>9__114_0 = x => x.NextViewCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand ShowFindTextCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__116_0;
                if (<>c.<>9__116_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__116_0;
                    evaluator = <>c.<>9__116_0 = x => x.ShowFindTextCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand FindTextCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__118_0;
                if (<>c.<>9__118_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__118_0;
                    evaluator = <>c.<>9__118_0 = x => x.FindTextCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        protected internal virtual ICommand NavigateCommandInternal
        {
            get
            {
                Func<DocumentViewerControl, ICommand> evaluator = <>c.<>9__120_0;
                if (<>c.<>9__120_0 == null)
                {
                    Func<DocumentViewerControl, ICommand> local1 = <>c.<>9__120_0;
                    evaluator = <>c.<>9__120_0 = x => x.NavigateCommand;
                }
                return this.ActualDocumentViewer.With<DocumentViewerControl, ICommand>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CommandProvider.<>c <>9 = new CommandProvider.<>c();
            public static Func<DocumentViewerControl, ICommand> <>9__88_0;
            public static Func<DocumentViewerControl, ICommand> <>9__90_0;
            public static Func<DocumentViewerControl, ICommand> <>9__92_0;
            public static Func<DocumentViewerControl, ICommand> <>9__94_0;
            public static Func<DocumentViewerControl, ICommand> <>9__96_0;
            public static Func<DocumentViewerControl, ICommand> <>9__98_0;
            public static Func<DocumentViewerControl, ICommand> <>9__100_0;
            public static Func<DocumentViewerControl, ICommand> <>9__102_0;
            public static Func<DocumentViewerControl, ICommand> <>9__104_0;
            public static Func<DocumentViewerControl, ICommand> <>9__106_0;
            public static Func<DocumentViewerControl, ICommand> <>9__108_0;
            public static Func<DocumentViewerControl, ICommand> <>9__110_0;
            public static Func<DocumentViewerControl, ICommand> <>9__112_0;
            public static Func<DocumentViewerControl, ICommand> <>9__114_0;
            public static Func<DocumentViewerControl, ICommand> <>9__116_0;
            public static Func<DocumentViewerControl, ICommand> <>9__118_0;
            public static Func<DocumentViewerControl, ICommand> <>9__120_0;
            public static Func<DocumentViewerControl, int> <>9__121_4;
            public static Func<int> <>9__121_5;
            public static Func<DocumentViewerControl, int> <>9__121_6;
            public static Func<int> <>9__121_7;
            public static Action <>9__122_0;
            public static Func<CommandToggleButton, bool> <>9__122_2;
            public static Func<DocumentViewerControl, int> <>9__124_0;
            public static Func<int> <>9__124_1;
            public static Func<DocumentViewerControl, int> <>9__124_2;
            public static Func<int> <>9__124_3;
            public static Func<DocumentViewerControl, ZoomMode> <>9__128_1;
            public static Func<ZoomMode> <>9__128_2;
            public static Func<DocumentViewerControl, ZoomMode> <>9__128_3;
            public static Func<ZoomMode> <>9__128_4;
            public static Func<bool> <>9__128_5;

            internal void <CreateZoomModeAndZoomFactorItem>b__122_0()
            {
            }

            internal bool <CreateZoomModeAndZoomFactorItem>b__122_2(CommandToggleButton x) => 
                x.CanExecute(null);

            internal ICommand <get_ClockwiseRotateCommandInternal>b__106_0(DocumentViewerControl x) => 
                x.ClockwiseRotateCommand;

            internal ICommand <get_CloseDocumentCommandInternal>b__90_0(DocumentViewerControl x) => 
                x.CloseDocumentCommand;

            internal ICommand <get_CounterClockwiseRotateCommandInternal>b__108_0(DocumentViewerControl x) => 
                x.CounterClockwiseRotateCommand;

            internal ICommand <get_FindTextCommandInternal>b__118_0(DocumentViewerControl x) => 
                x.FindTextCommand;

            internal ICommand <get_NavigateCommandInternal>b__120_0(DocumentViewerControl x) => 
                x.NavigateCommand;

            internal ICommand <get_NextPageCommandInternal>b__94_0(DocumentViewerControl x) => 
                x.NextPageCommand;

            internal ICommand <get_NextViewCommandInternal>b__114_0(DocumentViewerControl x) => 
                x.NextViewCommand;

            internal ICommand <get_OpenDocumentCommandInternal>b__88_0(DocumentViewerControl x) => 
                x.OpenDocumentCommand;

            internal ICommand <get_PreviousPageCommandInternal>b__92_0(DocumentViewerControl x) => 
                x.PreviousPageCommand;

            internal ICommand <get_PreviousViewCommandInternal>b__112_0(DocumentViewerControl x) => 
                x.PreviousViewCommand;

            internal ICommand <get_ScrollCommandInternal>b__110_0(DocumentViewerControl x) => 
                x.ScrollCommand;

            internal ICommand <get_SetPageNumberCommandInternal>b__96_0(DocumentViewerControl x) => 
                x.SetPageNumberCommand;

            internal ICommand <get_SetZoomFactorCommandInternal>b__104_0(DocumentViewerControl x) => 
                x.SetZoomFactorCommand;

            internal ICommand <get_SetZoomModeCommandInternal>b__102_0(DocumentViewerControl x) => 
                x.SetZoomModeCommand;

            internal ICommand <get_ShowFindTextCommandInternal>b__116_0(DocumentViewerControl x) => 
                x.ShowFindTextCommand;

            internal ICommand <get_ZoomInCommandInternal>b__98_0(DocumentViewerControl x) => 
                x.ZoomInCommand;

            internal ICommand <get_ZoomOutCommandInternal>b__100_0(DocumentViewerControl x) => 
                x.ZoomOutCommand;

            internal int <InitializeElementsInternal>b__121_4(DocumentViewerControl x) => 
                x.PageCount;

            internal int <InitializeElementsInternal>b__121_5() => 
                0;

            internal int <InitializeElementsInternal>b__121_6(DocumentViewerControl x) => 
                x.CurrentPageNumber;

            internal int <InitializeElementsInternal>b__121_7() => 
                0;

            internal int <UpdatePagination>b__124_0(DocumentViewerControl x) => 
                x.CurrentPageNumber;

            internal int <UpdatePagination>b__124_1() => 
                0;

            internal int <UpdatePagination>b__124_2(DocumentViewerControl x) => 
                x.PageCount;

            internal int <UpdatePagination>b__124_3() => 
                0;

            internal ZoomMode <UpdateZoomFactorCheckState>b__128_1(DocumentViewerControl z) => 
                z.ZoomMode;

            internal ZoomMode <UpdateZoomFactorCheckState>b__128_2() => 
                ZoomMode.Custom;

            internal ZoomMode <UpdateZoomFactorCheckState>b__128_3(DocumentViewerControl z) => 
                z.ZoomMode;

            internal ZoomMode <UpdateZoomFactorCheckState>b__128_4() => 
                ZoomMode.Custom;

            internal bool <UpdateZoomFactorCheckState>b__128_5() => 
                false;
        }
    }
}

