namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class GridViewInplaceEditorOwner : InplaceEditorOwnerBase
    {
        public GridViewInplaceEditorOwner(DataViewBase view) : base(view, false)
        {
        }

        protected override bool CommitEditing() => 
            this.View.CommitEditing();

        protected override bool CommitEditorForce() => 
            this.View.CommitEditing(true, true);

        protected override void EnqueueImmediateAction(IAction action)
        {
            this.View.ImmediateActionsManager.EnqueueAction(action);
        }

        protected override void FocusProcessMouseDown(DependencyObject originalSource)
        {
            if ((base.ActiveEditor != null) && ((base.ActiveEditor.EditCore != null) && ((originalSource != null) && ((FocusHelper.GetFocusedElement() != null) && (!ReferenceEquals(FocusManager.GetFocusScope((DependencyObject) FocusHelper.GetFocusedElement()), FocusManager.GetFocusScope(this.TopOwner)) && LayoutHelper.IsChildElement(base.ActiveEditor.EditCore, originalSource))))))
            {
                KeyboardHelper.Focus(base.ActiveEditor);
            }
            else if (this.IsKeyboardFocusInSearchPanel())
            {
                KeyboardHelper.Focus((UIElement) this.TopOwner);
            }
            else
            {
                base.FocusProcessMouseDown(originalSource);
            }
        }

        private EditGridCellData GetCellData(object o)
        {
            object dataContext = null;
            if (o is FrameworkElement)
            {
                dataContext = (o as FrameworkElement).DataContext;
            }
            if (o is FrameworkContentElement)
            {
                dataContext = (o as FrameworkContentElement).DataContext;
            }
            return (dataContext as EditGridCellData);
        }

        protected override string GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value) => 
            string.Empty;

        protected override bool? GetDisplayText(InplaceEditorBase inplaceEditor, string originalDisplayText, object value, out string displayText)
        {
            displayText = null;
            if (this.View.DataControl == null)
            {
                return false;
            }
            CellEditorBase base2 = (CellEditorBase) inplaceEditor;
            if (base2.CellData == null)
            {
                return false;
            }
            int? listSourceIndex = null;
            return this.View.RaiseCustomDisplayText(new int?(base2.RowHandle), listSourceIndex, base2.CellData.ColumnCore, value, originalDisplayText, out displayText);
        }

        private DependencyObject GetEditorChild() => 
            ((base.CurrentCellEditor == null) || (VisualTreeHelper.GetChildrenCount(base.CurrentCellEditor) <= 0)) ? null : VisualTreeHelper.GetChild(base.CurrentCellEditor, 0);

        private object GetRowIdentifierByTreeElement(DataViewBase view, DependencyObject originalSource)
        {
            int rowHandleByTreeElement = view.GetRowHandleByTreeElement(originalSource);
            return view.DataProviderBase.GetNodeIdentifier(rowHandleByTreeElement);
        }

        protected override bool IsActivationAction(ActivationAction action, RoutedEventArgs e, bool defaultValue)
        {
            GetIsEditorActivationActionEventArgs args = new GetIsEditorActivationActionEventArgs(action, e, this.GetEditorChild(), defaultValue, this.View, this.View.FocusedRowHandle, this.View.DataControl.CurrentColumn);
            this.View.EventTargetView.RaiseEvent(args);
            return args.IsActivationAction;
        }

        protected override bool IsChildOfCurrentEditor(DependencyObject source)
        {
            if (!this.Optimized)
            {
                return this.IsCurrentCellChild(source);
            }
            if (this.View.ActualAllowCellMerge || ((this.View.RowMarginControlDisplayMode == RowMarginControlDisplayMode.InCellsControl) && this.View.IsTreeColumn(this.View.DataControl.CurrentColumn)))
            {
                return this.View.RootView.CalcHitInfoCore(source).IsRowCell;
            }
            EditGridCellData cellData = this.GetCellData(source);
            if ((cellData == null) || (cellData.Column.DisplayTemplate == null))
            {
                return this.IsCurrentCellChild(source);
            }
            int num = cellData.RowData.RowHandle.Value;
            ColumnBase column = cellData.Column;
            return ((this.View.FocusedRowHandle == num) && ReferenceEquals(this.View.DataControl.CurrentColumn, column));
        }

        private bool IsCurrentCellChild(object source) => 
            (this.View.CurrentCell != null) && LayoutHelper.IsChildElement(this.View.CurrentCell, source as DependencyObject);

        private bool IsKeyboardFocusInSearchPanel() => 
            (this.View.SearchControl != null) && this.View.SearchControl.IsKeyboardFocusWithin;

        protected override bool IsNavigationKey(Key key) => 
            ((key != Key.Return) || (!this.View.EnterMoveNextColumn && !this.View.IsNewItemRowFocused)) ? base.IsNavigationKey(key) : true;

        protected override bool NeedActivateOnLeftMouseButtonEditor(DependencyObject obj)
        {
            if (!(base.CurrentCellEditor is FilterRowCellEditor))
            {
                return base.NeedActivateOnLeftMouseButtonEditor(obj);
            }
            DependencyObject objA = obj;
            while (true)
            {
                if ((objA != null) && !ReferenceEquals(objA, base.CurrentCellEditor))
                {
                    if (objA is FilterCriteriaControlBase)
                    {
                        return false;
                    }
                    if (!ReferenceEquals(objA, base.CurrentCellEditor))
                    {
                        objA = LayoutHelper.GetParent(objA, false);
                        continue;
                    }
                }
                if (base.CurrentCellEditor != null)
                {
                    base.CurrentCellEditor.UpdateEditorButtonVisibility();
                }
                return base.NeedActivateOnLeftMouseButtonEditor(obj);
            }
        }

        protected override bool NeedsKey(Key key, ModifierKeys modifiers, bool defaultValue)
        {
            if ((base.CurrentCellEditor == null) || !base.CurrentCellEditor.IsEditorVisible)
            {
                return defaultValue;
            }
            GetActiveEditorNeedsKeyEventArgs e = new GetActiveEditorNeedsKeyEventArgs(key, modifiers, this.GetEditorChild(), defaultValue, this.View, this.View.FocusedRowHandle, this.View.DataControl.CurrentColumn);
            this.View.EventTargetView.RaiseEvent(e);
            return e.NeedsKey;
        }

        protected override void OnActiveEditorChanged()
        {
            this.View.SetActiveEditor();
        }

        protected override bool PerformNavigationOnLeftButtonDown(MouseButtonEventArgs e)
        {
            DependencyObject originalSource = e.OriginalSource as DependencyObject;
            FrameworkElement rowElementByTreeElement = this.View.GetRowElementByTreeElement(originalSource);
            object rowIdentifierByTreeElement = this.GetRowIdentifierByTreeElement(this.View, rowElementByTreeElement);
            this.View.PerformNavigationOnLeftButtonDownCore(e);
            this.View.UpdateRowsState();
            return ((this.View.CanStartSelection() || (!ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers) && !ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))) && Equals(rowIdentifierByTreeElement, this.GetRowIdentifierByTreeElement(this.View, rowElementByTreeElement)));
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            this.View.ProcessKeyDown(e);
        }

        protected override bool ShouldReraiseActivationAction(ActivationAction action, RoutedEventArgs e, bool defaultValue)
        {
            ProcessEditorActivationActionEventArgs args = new ProcessEditorActivationActionEventArgs(action, e, this.GetEditorChild(), defaultValue, this.View, this.View.FocusedRowHandle, this.View.DataControl.CurrentColumn);
            this.View.EventTargetView.RaiseEvent(args);
            return args.RaiseEventAgain;
        }

        protected override bool ShowEditor(bool selectAll)
        {
            this.View.OnOpeningEditor();
            return base.ShowEditor(selectAll);
        }

        private DataViewBase View =>
            (DataViewBase) base.owner;

        public override bool EditorWasClosed
        {
            get => 
                this.View.IsRootView ? base.EditorWasClosed : this.View.RootView.InplaceEditorOwner.EditorWasClosed;
            set
            {
                if (this.View.IsRootView)
                {
                    base.EditorWasClosed = value;
                }
                else
                {
                    this.View.RootView.InplaceEditorOwner.EditorWasClosed = value;
                }
            }
        }

        public override FrameworkElement TopOwner =>
            this.View.RootView;

        protected override FrameworkElement FocusOwner =>
            this.View.RootView.DataControl;

        protected override Type OwnerBaseType =>
            typeof(DataViewBase);

        protected override DevExpress.Xpf.Core.EditorShowMode EditorShowMode
        {
            get
            {
                DevExpress.Xpf.Core.EditorShowMode editorShowMode = this.View.RootView.EditorShowMode;
                return ((editorShowMode == DevExpress.Xpf.Core.EditorShowMode.Default) ? ((this.View.ShowUpdateRowButtonsCore != ShowUpdateRowButtons.OnCellEditorOpen) ? editorShowMode : DevExpress.Xpf.Core.EditorShowMode.MouseDownFocused) : editorShowMode);
            }
        }

        protected override bool EditorSetInactiveAfterClick =>
            this.View.EditorSetInactiveAfterClick;

        protected override bool UseMouseUpFocusedEditorShowModeStrategy =>
            this.View.UseMouseUpFocusedEditorShowModeStrategy;

        protected override bool CanCommitEditing =>
            (base.CanCommitEditing || this.View.DataProviderBase.IsCurrentRowEditing) ? !this.View.IsEditFormVisible : false;

        protected override bool CanFocusEditor =>
            !FloatingContainer.IsModalContainerOpened ? (base.CanFocusEditor && (!this.View.IsColumnFilterOpened && (this.View.IsFocusedView && (!this.View.IsKeyboardFocusInSearchPanel() && ((!this.View.IsKeyboardFocusInHeadersPanel() || this.View.Navigation.NavigationMouseLocker.IsLocked) && !this.View.ColumnChooserIsKeyboardFocus()))))) : false;

        private bool Optimized
        {
            get
            {
                ITableView view = this.View as ITableView;
                return ((view != null) && ((view.UseLightweightTemplates == null) || (((UseLightweightTemplates) view.UseLightweightTemplates.Value) != UseLightweightTemplates.None)));
            }
        }
    }
}

