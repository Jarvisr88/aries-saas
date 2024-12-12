namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public abstract class GridViewNavigationBase
    {
        private DataViewBase view;
        protected internal Locker NavigationMouseLocker = new Locker();

        protected GridViewNavigationBase(DataViewBase view)
        {
            this.view = view;
        }

        protected internal virtual void ClearAllStates()
        {
        }

        private void DoActionAndScrollIntoViewIfNeeded(Action action)
        {
            int focusedRowHandle = this.View.FocusedRowHandle;
            DataViewBase focusedView = this.View.MasterRootRowsContainer.FocusedView;
            if (this.View != null)
            {
                this.View.CanSelectLocker.DoLockedAction(() => action());
            }
            else
            {
                action();
            }
            DataViewBase objB = this.View.MasterRootRowsContainer.FocusedView;
            if (!objB.AllowScrollToFocusedRow && (!ReferenceEquals(focusedView, objB) || (focusedRowHandle != focusedView.FocusedRowHandle)))
            {
                objB.ScrollIntoView(objB.FocusedRowHandle);
            }
        }

        public virtual bool GetIsFocusedCell(int rowHandle, ColumnBase column) => 
            false;

        public virtual void OnDown()
        {
        }

        public virtual void OnEnd(KeyEventArgs e)
        {
        }

        public virtual void OnHome(KeyEventArgs e)
        {
        }

        public virtual void OnLeft(bool isCtrlPressed)
        {
        }

        public virtual bool OnMinus(bool isCtrlPressed) => 
            false;

        public virtual void OnPageDown()
        {
        }

        public virtual void OnPageDown(KeyEventArgs e)
        {
            this.OnPageDown();
        }

        public virtual void OnPageUp()
        {
        }

        public virtual void OnPageUp(KeyEventArgs e)
        {
            this.OnPageUp();
        }

        public virtual bool OnPlus(bool isCtrlPressed) => 
            false;

        public virtual void OnRight(bool isCtrlPressed)
        {
        }

        public virtual void OnTab(bool isShiftPressed)
        {
        }

        public virtual void OnUp(bool isCtrlPressed)
        {
        }

        protected internal virtual void ProcessKey(KeyEventArgs e)
        {
            if (this.View.IsEditFormVisible)
            {
                return;
            }
            if (RightToLeftHelper.IsLeftKey(e.Key, this.IsRightToLeft))
            {
                this.OnLeft(ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                e.Handled = true;
                return;
            }
            if (RightToLeftHelper.IsRightKey(e.Key, this.IsRightToLeft))
            {
                this.OnRight(ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                e.Handled = true;
                return;
            }
            Key key = e.Key;
            if (key > Key.Add)
            {
                if (key != Key.Subtract)
                {
                    if (key == Key.OemPlus)
                    {
                        goto TR_0006;
                    }
                    else if (key != Key.OemMinus)
                    {
                        return;
                    }
                }
                if (!this.View.IsKeyboardFocusInSearchPanel())
                {
                    e.Handled = this.OnMinus(ModifierKeysHelper.IsOnlyCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                    return;
                }
                return;
            }
            else if (key == Key.Tab)
            {
                if (!ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && (!this.View.AllowLeaveFocusOnTab && !this.View.IsKeyboardFocusInSearchPanel()))
                {
                    this.OnTab(ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                    e.Handled = true;
                }
                return;
            }
            else
            {
                switch (key)
                {
                    case Key.Prior:
                        this.OnPageUp(e);
                        e.Handled = true;
                        return;

                    case Key.Next:
                        this.OnPageDown(e);
                        e.Handled = true;
                        return;

                    case Key.End:
                        this.DoActionAndScrollIntoViewIfNeeded(() => this.OnEnd(e));
                        e.Handled = true;
                        return;

                    case Key.Home:
                        this.DoActionAndScrollIntoViewIfNeeded(() => this.OnHome(e));
                        e.Handled = true;
                        return;

                    case Key.Left:
                    case Key.Right:
                        return;

                    case Key.Up:
                        if (this.View.AreUpdateRowButtonsShown)
                        {
                            this.OnUp(ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                        }
                        else
                        {
                            this.DoActionAndScrollIntoViewIfNeeded(() => this.OnUp(ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e))));
                        }
                        e.Handled = true;
                        return;

                    case Key.Down:
                        if (this.View.AreUpdateRowButtonsShown)
                        {
                            this.OnDown();
                        }
                        else
                        {
                            this.DoActionAndScrollIntoViewIfNeeded(new Action(this.OnDown));
                        }
                        e.Handled = true;
                        return;

                    default:
                        if (key == Key.Add)
                        {
                            break;
                        }
                        return;
                }
            }
        TR_0006:
            if (!this.View.IsKeyboardFocusInSearchPanel())
            {
                e.Handled = this.OnPlus(ModifierKeysHelper.IsOnlyCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
            }
            else
            {
                return;
            }
        }

        protected internal virtual void ProcessMouse(DependencyObject originalSource)
        {
        }

        protected virtual bool ShouldCollapseRow() => 
            this.View.IsExpandableRowFocused() && this.View.IsExpanded(this.View.FocusedRowHandle);

        protected virtual bool ShouldExpandRow() => 
            this.View.IsExpandableRowFocused() && !this.View.IsExpanded(this.View.FocusedRowHandle);

        protected internal virtual void UpdateRowsState()
        {
        }

        public DataViewBase View =>
            this.view;

        private bool IsRightToLeft =>
            (this.view.DataControl != null) && (this.view.DataControl.FlowDirection == FlowDirection.RightToLeft);

        protected DataControlBase DataControl =>
            this.view.DataControl;

        public virtual bool CanSelectCell =>
            false;

        public virtual bool ShouldRaiseRowAutomationEvents =>
            false;
    }
}

