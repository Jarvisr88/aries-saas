namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [ToolboxItem(false)]
    public class PopupListBox : EditorListBox
    {
        private readonly PopupListBoxNavigationHelper navigationHelper;

        public PopupListBox()
        {
            this.SetDefaultStyleKey(typeof(PopupListBox));
            base.Focusable = false;
            this.navigationHelper = new PopupListBoxNavigationHelper(this);
        }

        private bool CanCloseOnMouseUp(ComboBoxEditItem item) => 
            (this.OwnerEdit != null) ? (this.OwnerEdit.ItemsProvider.IsServerMode ? !((IServerModeCollectionView) base.ItemsSource).IsTempItem(base.ItemContainerGenerator.IndexFromContainer(item)) : true) : false;

        private void CloseOwnerEditPopupIfNeeded(ComboBoxEditItem item)
        {
            if ((this.OwnerEdit != null) && (this.CloseOnMouseUp && this.CanCloseOnMouseUp(item)))
            {
                if (this.CloseUsingDispatcher)
                {
                    base.Dispatcher.BeginInvoke(delegate {
                        Action<ComboBoxEdit> action = <>c.<>9__38_1;
                        if (<>c.<>9__38_1 == null)
                        {
                            Action<ComboBoxEdit> local1 = <>c.<>9__38_1;
                            action = <>c.<>9__38_1 = x => x.ClosePopup();
                        }
                        this.OwnerEdit.Do<ComboBoxEdit>(action);
                    }, new object[0]);
                }
                else
                {
                    this.OwnerEdit.ClosePopup();
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new ComboBoxEditItem();

        protected override IEnumerable<object> GetSelectedItemsInternal() => 
            !this.OwnerEdit.EditStrategy.IsLockedByValueChanging ? (this.TotalSelectedItems ?? new List<object>()) : base.GetSelectedItemsInternal();

        internal void NotifyComboBoxItemMouseDown(ComboBoxEditItem item)
        {
            if ((this.OwnerEdit != null) && (base.SelectionEventMode == SelectionEventMode.MouseDown))
            {
                item.SetCurrentValue(ListBoxItem.IsSelectedProperty, true);
            }
        }

        internal void NotifyComboBoxItemMouseUp(ComboBoxEditItem item)
        {
            if (!this.OwnerEdit.IsReadOnly)
            {
                this.CloseOwnerEditPopupIfNeeded(item);
            }
        }

        protected override void OnInplaceKeyDown(KeyEventArgs e)
        {
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if ((this.OwnerEdit != null) && this.OwnerEdit.IsPopupOpen)
            {
                base.SelectionLocker.DoLockedActionIfNotLocked(() => LookUpEditHelper.RaisePopupContentSelectionChangedEvent(this.OwnerEdit, CustomItem.FilterCustomItems(e.RemovedItems.Cast<object>()).ToList<object>(), CustomItem.FilterCustomItems(e.AddedItems.Cast<object>()).ToList<object>()));
                this.TotalSelectedItems = this.TotalSelectedItems.Union<object>(CustomItem.FilterCustomItems(e.AddedItems.Cast<object>()));
                base.SelectionLocker.DoLockedActionIfNotLocked(() => this.TotalSelectedItems = this.TotalSelectedItems.Except<object>(CustomItem.FilterCustomItems(e.RemovedItems.Cast<object>())));
                base.MakeVisibleIfNeeded();
            }
        }

        protected override void OnSelectionChangedInternal(SelectionChangedEventArgs e)
        {
            base.SynchronizationLocker.DoLockedActionIfNotLocked(() => this.SyncSelectAll(e));
        }

        internal bool ProcessDownKey(KeyEventArgs e)
        {
            if (base.IsEditorReadOnly())
            {
                return false;
            }
            if (ModifierKeysHelper.IsAltPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                return false;
            }
            if (this.OwnerEdit.EditStrategy.IsSingleSelection)
            {
                this.ProcessDownKeyInSingleSelection(e);
            }
            else
            {
                if (!this.OwnerEdit.IsTokenMode)
                {
                    return false;
                }
                this.ProcessKeyDownInMultipleSelectionInTokenMode(e);
            }
            return e.Handled;
        }

        private void ProcessDownKeyInSingleSelection(KeyEventArgs e)
        {
            Key systemKey = e.Key;
            if (e.Key == Key.System)
            {
                systemKey = e.SystemKey;
            }
            if (systemKey != Key.Return)
            {
                switch (systemKey)
                {
                    case Key.Prior:
                        e.Handled = true;
                        this.NavigationHelper.MovePageUp();
                        break;

                    case Key.Next:
                        e.Handled = true;
                        this.NavigationHelper.MovePageDown();
                        break;

                    case Key.End:
                        e.Handled = true;
                        this.NavigationHelper.MoveLast();
                        break;

                    case Key.Home:
                        e.Handled = true;
                        this.NavigationHelper.MoveFirst();
                        break;

                    case Key.Up:
                        e.Handled = true;
                        this.NavigationHelper.MovePrev();
                        break;

                    case Key.Down:
                        e.Handled = true;
                        this.NavigationHelper.MoveNext();
                        break;

                    default:
                        break;
                }
            }
            base.MakeSelectionVisible();
        }

        private void ProcessKeyDownInMultipleSelectionInTokenMode(KeyEventArgs e)
        {
            Key systemKey = e.Key;
            if (e.Key == Key.System)
            {
                systemKey = e.SystemKey;
            }
            if (systemKey != Key.Return)
            {
                switch (systemKey)
                {
                    case Key.End:
                        e.Handled = true;
                        this.NavigationHelper.MoveFocusLast();
                        break;

                    case Key.Home:
                        e.Handled = true;
                        this.NavigationHelper.MoveFocusFirst();
                        return;

                    case Key.Left:
                    case Key.Right:
                        break;

                    case Key.Up:
                        e.Handled = true;
                        this.NavigationHelper.MoveFocusPrev();
                        return;

                    case Key.Down:
                        e.Handled = true;
                        this.NavigationHelper.MoveFocusNext();
                        return;

                    default:
                        return;
                }
            }
        }

        protected override void SetPropertiesFromStyleSettings()
        {
            BaseComboBoxStyleSettings styleSettings = (BaseComboBoxStyleSettings) this.PropertyProvider.StyleSettings;
            base.ItemContainerStyle ??= this.PropertyProvider.GetItemContainerStyle();
            base.SelectionMode = styleSettings.GetSelectionMode(this.OwnerEdit);
            base.SelectionEventMode = styleSettings.GetSelectionEventMode(this.OwnerEdit);
            this.CloseOnMouseUp = styleSettings.GetClosePopupOnMouseUp(this.OwnerEdit);
            this.CloseUsingDispatcher = styleSettings.CloseUsingDispatcher;
            this.AssignGroupStyle();
        }

        internal void SetupEditor()
        {
            base.SelectionLocker.DoLockedAction(delegate {
                this.UpdateTotals();
                this.SyncWithOwnerEditWithSelectionLock(true);
            });
        }

        protected override bool ShouldMakeSelectionVisible() => 
            (this.OwnerEdit != null) && (this.OwnerEdit.EditStrategy.StyleSettings.GetActualScrollToSelectionOnPopup(this.OwnerEdit) || (this.OwnerEdit.Settings.AutoComplete && this.OwnerEdit.Settings.ImmediatePopup));

        protected override void SyncItemsSourceInternal()
        {
            base.SyncItemsSourceInternal();
            if (base.SelectionMode != SelectionMode.Single)
            {
                base.SelectedItems.Clear();
            }
        }

        protected override void SyncSelectedItemsInternal(bool updateTotals)
        {
            base.SyncSelectedItemsInternal(updateTotals);
            if (updateTotals)
            {
                this.UpdateTotals();
            }
        }

        protected internal override void SyncWithOwnerEditWithSelectionLock(bool updateSource)
        {
            base.SyncWithOwnerEditWithSelectionLock(updateSource);
            base.MakeVisibleIfNeeded();
        }

        private void UpdateTotals()
        {
            IList<object> currentSelectedItems = ((ISelectorEdit) this.OwnerEdit).GetCurrentSelectedItems() as IList<object>;
            if (this.OwnerEdit.ItemsProvider.IsServerMode)
            {
                this.TotalSelectedItems = base.GetSelectedItemsInServerMode(currentSelectedItems);
            }
            else
            {
                this.TotalSelectedItems = currentSelectedItems;
            }
        }

        private IEnumerable<object> TotalSelectedItems { get; set; }

        protected PopupListBoxNavigationHelper NavigationHelper =>
            this.navigationHelper;

        protected bool IsSelectionLocked =>
            base.SelectionLocker.IsLocked;

        private bool CloseOnMouseUp { get; set; }

        private bool CloseUsingDispatcher { get; set; }

        private LookUpEditBasePropertyProvider PropertyProvider =>
            (LookUpEditBasePropertyProvider) ActualPropertyProvider.GetProperties(this.OwnerEdit);

        public ComboBoxEdit OwnerEdit =>
            base.OwnerEdit as ComboBoxEdit;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupListBox.<>c <>9 = new PopupListBox.<>c();
            public static Action<ComboBoxEdit> <>9__38_1;

            internal void <CloseOwnerEditPopupIfNeeded>b__38_1(ComboBoxEdit x)
            {
                x.ClosePopup();
            }
        }
    }
}

