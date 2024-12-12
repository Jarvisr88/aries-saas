namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Grid.Printing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class DataViewCommandsBase : INotifyPropertyChanged
    {
        private List<IDelegateCommand> commands = new List<IDelegateCommand>();
        private ICommand showSearchPanel;
        private ICommand hideSearchPanel;
        private readonly DataViewBase view;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        protected DataViewCommandsBase(DataViewBase view)
        {
            this.view = view;
            this.ShowFilterEditor = this.CreateDelegateCommand(o => view.ShowFilterEditor(o), o => view.CanShowFilterEditor(o));
            this.ShowColumnChooser = this.CreateDelegateCommand(o => view.ShowColumnChooser(), o => view.CanShowColumnChooser());
            this.HideColumnChooser = this.CreateDelegateCommand(o => view.HideColumnChooser(), o => view.CanHideColumnChooser());
            this.MovePrevCell = this.CreateDelegateCommand(o => view.MovePrevCell(), o => view.CanMovePrevCell());
            this.MoveNextCell = this.CreateDelegateCommand(o => view.MoveNextCell(), o => view.CanMoveNextCell());
            this.MovePrevRow = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.Navigation.OnUp(false), o => view.MasterRootRowsContainer.FocusedView.CanPrevRow());
            this.MoveNextRow = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.Navigation.OnDown(), o => view.MasterRootRowsContainer.FocusedView.CanNextRow());
            this.MoveFirstRow = this.CreateDelegateCommand(o => view.MoveFirstRow(), o => view.MasterRootRowsContainer.FocusedView.CanPrevRow());
            this.MoveLastRow = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.MoveLastOrLastMasterRow(), o => view.MasterRootRowsContainer.FocusedView.CanNextRow());
            this.MovePrevPage = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.Navigation.OnPageUp(), o => view.MasterRootRowsContainer.FocusedView.CanPrevRow());
            this.MoveNextPage = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.Navigation.OnPageDown(), o => view.MasterRootRowsContainer.FocusedView.CanNextRow());
            this.MoveFirstCell = this.CreateDelegateCommand(o => view.MoveFirstCell(), o => view.CanMoveFirstCell());
            this.MoveLastCell = this.CreateDelegateCommand(o => view.MoveLastCell(), o => view.CanMoveLastCell());
            this.ClearFilter = this.CreateDelegateCommand(o => view.ClearFilter(), o => view.CanClearFilter());
            Func<ChangeColumnsSortOrderMode?, bool> canExecuteMethod = <>c.<>9__146_29;
            if (<>c.<>9__146_29 == null)
            {
                Func<ChangeColumnsSortOrderMode?, bool> local1 = <>c.<>9__146_29;
                canExecuteMethod = <>c.<>9__146_29 = o => true;
            }
            this.ChangeColumnsSortOrder = this.CreateDelegateCommand<ChangeColumnsSortOrderMode?>(o => view.ChangeColumnsSortOrder(o.GetValueOrDefault(ChangeColumnsSortOrderMode.SortedColumns)), canExecuteMethod);
            this.DeleteFocusedRow = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.DeleteFocusedRowCommand(), o => view.MasterRootRowsContainer.FocusedView.CanDeleteFocusedRow());
            this.EditFocusedRow = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.EditFocusedRow(), o => view.MasterRootRowsContainer.FocusedView.CanEditFocusedRow());
            this.CancelEditFocusedRow = this.CreateDelegateCommand(o => view.CancelEditFocusedRow(), o => view.CanCancelEditFocusedRow());
            this.EndEditFocusedRow = this.CreateDelegateCommand(o => view.EndEditFocusedRow(), o => view.CanEndEditFocusedRow());
            this.ShowUnboundExpressionEditor = this.CreateDelegateCommand(o => view.ShowUnboundExpressionEditor(o), o => view.CanShowUnboundExpressionEditor(o));
            this.ShowTotalSummaryEditor = this.CreateDelegateCommand(o => view.ShowTotalSummaryEditor(o), o => view.CanShowTotalSummaryEditor(o));
            this.ShowFixedTotalSummaryEditor = this.CreateDelegateCommand(o => view.ShowFixedTotalSummaryEditor());
            this.ChangeMasterRowExpanded = this.CreateDelegateCommand(o => view.DataControl.ChangeMasterRowExpanded(o));
            this.ExpandMasterRow = this.CreateDelegateCommand(o => view.DataControl.SetMasterRowExpanded(o, true));
            this.CollapseMasterRow = this.CreateDelegateCommand(o => view.DataControl.SetMasterRowExpanded(o, false));
            this.ShowSearchPanel = this.CreateDelegateCommand(o => view.ShowSearchPanel(view.ConvertCommandParameterToBool(o)));
            this.HideSearchPanel = this.CreateDelegateCommand(o => view.HideSearchPanel());
            this.ShowPrintPreview = this.CreateDelegateCommand(o => PrintHelper.ShowPrintPreview(Window.GetWindow(view), view));
            this.ShowPrintPreviewDialog = this.CreateDelegateCommand(o => PrintHelper.ShowPrintPreviewDialog(Window.GetWindow(view), view));
            this.ShowRibbonPrintPreview = this.CreateDelegateCommand(o => PrintHelper.ShowRibbonPrintPreview(Window.GetWindow(view), view));
            this.ShowRibbonPrintPreviewDialog = this.CreateDelegateCommand(o => PrintHelper.ShowRibbonPrintPreviewDialog(Window.GetWindow(view), view));
            this.SearchResultNext = this.CreateDelegateCommand(o => view.SearchResultNext(), o => view.CanMoveSearchResult());
            this.SearchResultPrev = this.CreateDelegateCommand(o => view.SearchResultPrev(), o => view.CanMoveSearchResult());
            this.IncrementalSearchEnd = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.IncrementalSearchEnd());
            this.IncrementalSearchMoveNext = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.IncrementalSearchMoveNext(), o => view.MasterRootRowsContainer.FocusedView.CanStartIncrementalSearch);
            this.IncrementalSearchMovePrev = this.CreateDelegateCommand(o => view.MasterRootRowsContainer.FocusedView.IncrementalSearchMovePrev(), o => view.MasterRootRowsContainer.FocusedView.CanStartIncrementalSearch);
        }

        protected DelegateCommand<object> CreateDelegateCommand(Action<object> executeMethod) => 
            this.CreateDelegateCommand(executeMethod, null);

        protected DelegateCommand<object> CreateDelegateCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod) => 
            this.CreateDelegateCommand<object>(executeMethod, canExecuteMethod);

        protected DelegateCommand<T> CreateDelegateCommand<T>(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            DelegateCommand<T> item = DelegateCommandFactory.Create<T>(executeMethod, canExecuteMethod, false);
            this.commands.Add(item);
            return item;
        }

        internal void RaiseCanExecutedChanged()
        {
            Action<IDelegateCommand> action = <>c.<>9__150_0;
            if (<>c.<>9__150_0 == null)
            {
                Action<IDelegateCommand> local1 = <>c.<>9__150_0;
                action = <>c.<>9__150_0 = command => command.RaiseCanExecuteChanged();
            }
            this.commands.ForEach(action);
        }

        [Description("")]
        public ICommand ShowFilterEditor { get; private set; }

        [Description("")]
        public ICommand ShowColumnChooser { get; private set; }

        [Description("")]
        public ICommand HideColumnChooser { get; private set; }

        [Description("")]
        public ICommand MovePrevCell { get; private set; }

        [Description("")]
        public ICommand MoveNextCell { get; private set; }

        [Description("")]
        public ICommand MovePrevRow { get; private set; }

        [Description("")]
        public ICommand MoveNextRow { get; private set; }

        [Description("")]
        public ICommand MoveFirstRow { get; private set; }

        [Description("")]
        public ICommand MoveLastRow { get; private set; }

        [Description("")]
        public ICommand MovePrevPage { get; private set; }

        [Description("")]
        public ICommand MoveNextPage { get; private set; }

        [Description("")]
        public ICommand MoveFirstCell { get; private set; }

        [Description("")]
        public ICommand MoveLastCell { get; private set; }

        [Description("")]
        public ICommand ClearFilter { get; private set; }

        public ICommand ChangeColumnsSortOrder { get; private set; }

        [Description("")]
        public ICommand DeleteFocusedRow { get; private set; }

        [Description("")]
        public ICommand EditFocusedRow { get; private set; }

        [Description("")]
        public ICommand CancelEditFocusedRow { get; private set; }

        [Description("")]
        public ICommand EndEditFocusedRow { get; private set; }

        [Description("")]
        public ICommand ShowUnboundExpressionEditor { get; private set; }

        internal ICommand ChangeMasterRowExpanded { get; private set; }

        internal ICommand ExpandMasterRow { get; private set; }

        internal ICommand CollapseMasterRow { get; private set; }

        public ICommand ShowPrintPreviewDialog { get; private set; }

        public ICommand ShowPrintPreview { get; private set; }

        public ICommand ShowRibbonPrintPreviewDialog { get; private set; }

        public ICommand ShowRibbonPrintPreview { get; private set; }

        public ICommand SearchResultNext { get; private set; }

        public ICommand SearchResultPrev { get; private set; }

        public ICommand ShowTotalSummaryEditor { get; private set; }

        public ICommand ShowFixedTotalSummaryEditor { get; private set; }

        public ICommand IncrementalSearchEnd { get; private set; }

        public ICommand IncrementalSearchMoveNext { get; private set; }

        public ICommand IncrementalSearchMovePrev { get; private set; }

        public ICommand ShowSearchPanel
        {
            get => 
                this.showSearchPanel;
            private set => 
                this.showSearchPanel = value;
        }

        public ICommand HideSearchPanel
        {
            get => 
                this.hideSearchPanel;
            private set => 
                this.hideSearchPanel = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataViewCommandsBase.<>c <>9 = new DataViewCommandsBase.<>c();
            public static Func<ChangeColumnsSortOrderMode?, bool> <>9__146_29;
            public static Action<IDelegateCommand> <>9__150_0;

            internal bool <.ctor>b__146_29(ChangeColumnsSortOrderMode? o) => 
                true;

            internal void <RaiseCanExecutedChanged>b__150_0(IDelegateCommand command)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }
}

