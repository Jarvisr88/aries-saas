namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ListBoxEditStrategy : EditStrategyBase, ISelectorEditStrategy, IEditStrategy
    {
        private readonly SelectorPropertiesCoercionHelper selectorUpdater;
        private readonly Locker selectionChangedLocker;
        private EditorTextSearchHelper textSearchHelper;

        public ListBoxEditStrategy(ListBoxEdit editor) : base(editor)
        {
            this.selectorUpdater = new SelectorPropertiesCoercionHelper();
            this.selectionChangedLocker = new Locker();
            this.SelectorUpdater.SetOwner(this);
            this.TextSearchEngine = this.CreateTextSearchEngine();
        }

        protected override void AfterApplyStyleSettings()
        {
            base.AfterApplyStyleSettings();
            this.SyncInnerEditorWithOwnerEdit(true);
        }

        public void AllowCollectionViewChanged(bool value)
        {
            this.Settings.AllowCollectionView = value;
        }

        public virtual void AllowRejectUnknownValuesChanged(bool newValue)
        {
            this.Settings.AllowRejectUnknownValues = newValue;
            if (!base.IsInSupportInitialize)
            {
                base.SyncWithValue();
            }
        }

        protected internal override void CancelTextSearch()
        {
            this.TextSearchHelper.CancelTextSearch();
        }

        private void ClearHighlightedText()
        {
            this.SetHighlightedText(string.Empty);
        }

        public virtual int CoerceSelectedIndex(int baseIndex)
        {
            this.CoerceValue(ListBoxEdit.SelectedIndexProperty, baseIndex);
            return this.SelectorUpdater.CoerceSelectedIndex(baseIndex);
        }

        public virtual object CoerceSelectedItem(object baseItem)
        {
            this.CoerceValue(ListBoxEdit.SelectedItemProperty, baseItem);
            return this.SelectorUpdater.CoerceSelectedItem(baseItem);
        }

        private object CreateEditableItem(object filteredItem)
        {
            object valueByRowKey = this.ItemsProvider.GetValueByRowKey(filteredItem, this.ItemsProvider.CurrentDataViewHandle);
            if (valueByRowKey != null)
            {
                return valueByRowKey;
            }
            LookUpEditableItem item1 = new LookUpEditableItem();
            item1.AsyncLoading = this.Editor.IsAsyncOperationInProgress;
            item1.EditValue = filteredItem;
            item1.DisplayValue = valueByRowKey;
            return item1;
        }

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new ListBoxEditValidator(this, this.SelectorUpdater, this.Editor);

        protected virtual DevExpress.Xpf.Editors.Native.TextSearchEngine CreateTextSearchEngine() => 
            new DevExpress.Xpf.Editors.Native.TextSearchEngine(new Func<string, int, bool, int>(this.TextSearchCallback), ListBoxEdit.TextSearchTimeOut);

        void ISelectorEditStrategy.BringToView()
        {
            this.Editor.ScrollIntoView(base.ValueContainer.EditValue);
        }

        object ISelectorEditStrategy.GetCurrentDataViewHandle() => 
            this.CurrentDataViewHandle;

        object ISelectorEditStrategy.GetCurrentEditableValue()
        {
            object provideValue = null;
            this.ProvideEditValue(base.EditValue, out provideValue, UpdateEditorSource.TextInput);
            return provideValue;
        }

        IEnumerable ISelectorEditStrategy.GetInnerEditorCustomItemsSource()
        {
            List<CustomItem> list = new List<CustomItem>();
            foreach (ICloneable cloneable in this.GetInnerEditorCustomItemsSource())
            {
                CustomItem item = (CustomItem) cloneable.Clone();
                item.UpdateOwner(this.Editor);
                list.Add(item);
            }
            return list;
        }

        object ISelectorEditStrategy.GetInnerEditorItemsSource() => 
            !this.IsAsyncServerMode ? (!this.IsSyncServerMode ? this.ItemsProvider.VisibleListSource : new SyncServerModeCollectionView((SyncVisibleListWrapper) this.ItemsProvider.VisibleListSource)) : new AsyncServerModeCollectionView((AsyncVisibleListWrapper) this.ItemsProvider.VisibleListSource);

        IEnumerable ISelectorEditStrategy.GetInnerEditorMRUItemsSource() => 
            new ObservableCollection<object>();

        object ISelectorEditStrategy.GetNextValueFromSearchText(int startIndex) => 
            this.GetNextValueFromSearchTextInternal(startIndex);

        object ISelectorEditStrategy.GetPrevValueFromSearchText(int startIndex) => 
            this.GetPrevValueFromSearchTextInternal(startIndex);

        protected internal override bool DoTextSearch(string text, int startIndex, ref object result) => 
            this.TextSearchHelper.DoTextSearch(text, startIndex, ref result);

        public virtual void FilterCriteriaChanged(CriteriaOperator criteriaOperator)
        {
            this.Settings.FilterCriteria = criteriaOperator;
        }

        public virtual object GetCurrentSelectedItem() => 
            this.SelectorUpdater.GetCurrentSelectedItem(base.ValueContainer);

        public virtual IEnumerable GetCurrentSelectedItems() => 
            this.SelectorUpdater.GetCurrentSelectedItems(base.ValueContainer);

        protected virtual IEnumerable GetInnerEditorCustomItemsSource() => 
            this.PropertyProvider.ShowCustomItems() ? this.StyleSettings.GetCustomItems(this.Editor) : Enumerable.Empty<CustomItem>();

        protected virtual object GetNextValueFromSearchTextInternal(int startIndex) => 
            this.GetValueFromSearchTextCore(startIndex, true);

        protected virtual object GetPrevValueFromSearchTextInternal(int startIndex) => 
            this.GetValueFromSearchTextCore(startIndex, false);

        public object GetSelectedItems(object editValue) => 
            this.SelectorUpdater.GetSelectedItemsFromEditValue(editValue);

        private object GetSelectedItemsFromListBox() => 
            !this.IsSingleSelection ? this.ListBox.SelectedItems.Cast<object>().Select<object, object>(new Func<object, object>(this.GetSelectedRowKey)) : this.GetSelectedRowKey(this.ListBox.SelectedItem);

        protected virtual object GetSelectedRowKey(object item)
        {
            if (!this.IsServerMode)
            {
                return item;
            }
            Func<DataProxy, object> evaluator = <>c.<>9__61_0;
            if (<>c.<>9__61_0 == null)
            {
                Func<DataProxy, object> local1 = <>c.<>9__61_0;
                evaluator = <>c.<>9__61_0 = x => x.f_RowKey;
            }
            return ((DataProxy) item).With<DataProxy, object>(evaluator);
        }

        private object GetValueFromSearchTextCore(int startIndex, bool isDown) => 
            this.HasHighlightedText ? this.TextSearchHelper.FindValueFromSearchText(startIndex, isDown, false) : null;

        public virtual void IsSynchronizedWithCurrentItemChanged(bool value)
        {
            this.Settings.IsSynchronizedWithCurrentItem = value;
        }

        public virtual void ItemSourceChanged(object itemsSource)
        {
            this.CancelTextSearch();
            this.Settings.AssignToEditCoreLocker.DoIfNotLocked(() => this.Settings.ItemsSource = itemsSource);
            EnumItemsSource.SetupEnumItemsSource(itemsSource, delegate {
                this.Editor.SetCurrentValue(ListBoxEdit.ValueMemberProperty, EnumSourceHelperCore.ValueMemberName);
                this.Editor.SetCurrentValue(ListBoxEdit.DisplayMemberProperty, EnumSourceHelperCore.DisplayMemberName);
            });
            base.SyncWithValue();
        }

        public virtual void ItemsProviderChanged(ItemsProviderChangedEventArgs e)
        {
            switch (e)
            {
                case (ItemsProviderRefreshedEventArgs _):
                    this.ItemsProviderRefreshed((ItemsProviderRefreshedEventArgs) e);
                    break;

                case (ItemsProviderDataChangedEventArgs _):
                    this.ItemsProviderDataChanged((ItemsProviderDataChangedEventArgs) e);
                    break;

                case (ItemsProviderCurrentChangedEventArgs _):
                    this.ItemsProviderCurrentChanged((ItemsProviderCurrentChangedEventArgs) e);
                    break;

                case (ItemsProviderSelectionChangedEventArgs _):
                    this.ItemsProviderSelectionChanged((ItemsProviderSelectionChangedEventArgs) e);
                    break;

                case (ItemsProviderRowLoadedEventArgs _):
                    this.ItemsProviderRowLoaded((ItemsProviderRowLoadedEventArgs) e);
                    break;

                case (ItemsProviderOnBusyChangedEventArgs _):
                    this.ItemsProviderOnBusyChanged((ItemsProviderOnBusyChangedEventArgs) e);
                    break;

                case (ItemsProviderViewRefreshedEventArgs _):
                    this.ItemsProviderViewRefreshed((ItemsProviderViewRefreshedEventArgs) e);
                    break;
            }
        }

        private void ItemsProviderCurrentChanged(ItemsProviderCurrentChangedEventArgs e)
        {
            this.SelectorUpdater.SyncWithICollectionView(e.CurrentItem);
            this.UpdateDisplayText();
        }

        public virtual void ItemsProviderDataChanged(ItemsProviderDataChangedEventArgs e)
        {
            bool flag = this.SelectorUpdater.OptimizedSyncWithValueOnDataChanged(e, base.ValueContainer.EditValue, new Action(this.SyncWithValue));
            if (this.ListBox != null)
            {
                this.ListBox.SyncWithOwnerEditWithSelectionLock(false);
            }
            if (!flag)
            {
                base.SyncWithValue();
            }
        }

        protected virtual void ItemsProviderOnBusyChanged(ItemsProviderOnBusyChangedEventArgs e)
        {
            this.Editor.IsAsyncOperationInProgress = e.IsBusy;
        }

        protected virtual void ItemsProviderRefreshed(ItemsProviderRefreshedEventArgs itemsProviderRefreshedEventArgs)
        {
            if (this.ListBox != null)
            {
                this.ListBox.SyncWithOwnerEditWithSelectionLock(true);
            }
        }

        public virtual void ItemsProviderRefreshedInternal()
        {
            if (this.ListBox != null)
            {
                this.ListBox.SyncWithOwnerEditWithSelectionLock(true);
            }
            base.SyncWithValue();
        }

        private void ItemsProviderRowLoaded(ItemsProviderRowLoadedEventArgs e)
        {
            object obj3;
            if ((e.Handle == null) && (this.ProvideEditValue(base.ValueContainer.EditValue, out obj3, UpdateEditorSource.TextInput) && Equals(obj3, e.Value)))
            {
                this.ServerModeSyncWithValue(obj3);
            }
        }

        protected virtual void ItemsProviderSelectionChanged(ItemsProviderSelectionChangedEventArgs e)
        {
            if (this.IsSingleSelection)
            {
                object editValue = e.IsSelected ? this.SelectorUpdater.GetEditValueFromBaseValue(e.CurrentValue) : null;
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.ValueChanging);
            }
            else
            {
                IEnumerable<object> editValueFromBaseValue = (IEnumerable<object>) this.SelectorUpdater.GetEditValueFromBaseValue(base.ValueContainer.EditValue);
                object[] second = new object[] { e.CurrentValue };
                IEnumerable<object> source = e.IsSelected ? editValueFromBaseValue.Append<object>(second).Distinct<object>() : ((editValueFromBaseValue == null) ? ((IEnumerable<object>) second) : editValueFromBaseValue.Except<object>(second));
                base.ValueContainer.SetEditValue(source.ToList<object>(), UpdateEditorSource.ValueChanging);
            }
            this.Editor.Items.UpdateSelection(this.Editor.SelectedItems);
            if (this.ListBox != null)
            {
                this.ListBox.SyncWithOwnerEditWithSelectionLock(false);
            }
        }

        private void ItemsProviderViewRefreshed(ItemsProviderViewRefreshedEventArgs e)
        {
            object handle = e.Handle;
            if (handle != this.CurrentDataViewHandle)
            {
                object obj1 = handle;
            }
            else if (base.ValueContainer.HasValueCandidate)
            {
                base.SyncWithEditor();
            }
        }

        public override void OnInitialized()
        {
            this.Editor.SubscribeToItemsProviderChanged();
            this.SelectorUpdater.SyncWithICollectionView();
            base.OnInitialized();
            if (this.SelectorUpdater.ShouldSyncWithItems && !base.PropertyUpdater.HasSyncValue)
            {
                base.ValueContainer.SetEditValue(this.SelectorUpdater.GetEditValueFromItems(), UpdateEditorSource.ValueChanging);
            }
        }

        public override void OnLostFocus()
        {
            base.OnLostFocus();
            this.ClearHighlightedText();
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            if (this.Editor.IsReadOnly && ((e.Key == Key.Return) || (e.Key == Key.Space)))
            {
                e.Handled = true;
            }
        }

        public override bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource)
        {
            object editValue = value;
            LookUpEditableItem item = value as LookUpEditableItem;
            if ((item == null) && !this.IsInLookUpMode)
            {
                return base.ProvideEditValue(value, out provideValue, updateSource);
            }
            if (item != null)
            {
                if (!this.IsInLookUpMode && !this.IsAsyncServerMode)
                {
                    return base.ProvideEditValue(item.EditValue, out provideValue, updateSource);
                }
                editValue = item.EditValue;
            }
            provideValue = editValue;
            return true;
        }

        public override void RaiseValueChangedEvents(object oldValue, object newValue)
        {
            base.RaiseValueChangedEvents(oldValue, newValue);
            if (!base.ShouldLockRaiseEvents)
            {
                this.SelectorUpdater.RaiseSelectedIndexChangedEvent(oldValue, newValue);
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__62_0;
            if (<>c.<>9__62_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__62_0;
                getBaseValueHandler = <>c.<>9__62_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(BaseEdit.EditValueProperty, getBaseValueHandler, baseValue => this.SelectorUpdater.GetEditValueFromBaseValue(baseValue));
            base.PropertyUpdater.Register(ListBoxEdit.SelectedIndexProperty, baseValue => this.SelectorUpdater.GetEditValueFromSelectedIndex(baseValue), (PropertyCoercionHandler) (baseValue => this.SelectorUpdater.GetIndexFromEditValue(baseValue)));
            base.PropertyUpdater.Register(ListBoxEdit.SelectedItemProperty, baseValue => this.SelectorUpdater.GetEditValueFromSelectedItem(baseValue), baseValue => this.SelectorUpdater.GetSelectedItemFromEditValue(baseValue));
            base.PropertyUpdater.Register(ListBoxEdit.SelectedItemsProperty, baseValue => this.SelectorUpdater.GetEditValueFromSelectedItems(baseValue), baseValue => this.SelectorUpdater.GetSelectedItemsFromEditValue(baseValue), baseValue => this.SelectorUpdater.UpdateSelectedItems(baseValue));
        }

        public virtual void SelectAll()
        {
            List<object> editValue = new List<object>();
            foreach (object obj2 in this.ItemsProvider.VisibleListSource)
            {
                editValue.Add(this.ItemsProvider.GetValueFromItem(obj2, this.ItemsProvider.CurrentDataViewHandle));
            }
            base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.ValueChanging);
        }

        public virtual void SelectedIndexChanged(int oldValue, int newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(ListBoxEdit.SelectedIndexProperty, oldValue, newValue);
            }
        }

        public virtual void SelectedItemChanged(object oldValue, object newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(ListBoxEdit.SelectedItemProperty, oldValue, newValue);
            }
        }

        public virtual void SelectedItemsChanged(IList oldSelectedItems, IList selectedItems)
        {
            this.CoerceValue(ListBoxEdit.SelectedItemsProperty, selectedItems);
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(ListBoxEdit.SelectedItemsProperty, oldSelectedItems, selectedItems);
            }
        }

        private void ServerModeSyncWithValue(object value)
        {
            base.ValueContainer.SetEditValue(value, UpdateEditorSource.ValueChanging);
            this.UpdateDisplayText();
        }

        private void SetHighlightedText(string text)
        {
            base.ImmediateActionsManager.EnqueueAction(() => this.Editor.SetValue(TextBlockService.HighlightedTextProperty, text));
        }

        public virtual void ShowCustomItemsChanged(bool? value)
        {
            this.SyncInnerEditorWithOwnerEdit(true);
        }

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.SyncInnerEditorWithOwnerEdit(true);
        }

        protected virtual void SyncInnerEditorWithOwnerEdit(bool updateSource)
        {
            if (this.ListBox != null)
            {
                this.ListBox.SyncWithOwnerEdit(updateSource);
            }
        }

        protected override void SyncWithEditorInternal()
        {
            if (this.ListBox != null)
            {
                object selectedItemsFromListBox = this.GetSelectedItemsFromListBox();
                if (this.IsSingleSelection)
                {
                    object item = selectedItemsFromListBox;
                    object obj4 = this.SelectorUpdater.FilterSelectedItem(item);
                    if (!this.IsServerMode && (CustomItemHelper.IsCustomItem(obj4) || (this.ItemsProvider.GetIndexByItem(obj4, this.ItemsProvider.CurrentDataViewHandle) > -1)))
                    {
                        base.ValueContainer.SetEditValue(this.SelectorUpdater.GetEditValueFromSelectedItem(obj4), UpdateEditorSource.TextInput);
                    }
                    else if (this.IsSyncServerMode)
                    {
                        base.ValueContainer.SetEditValue(this.ItemsProvider.GetValueByRowKey(obj4, this.ItemsProvider.CurrentDataViewHandle), UpdateEditorSource.TextInput);
                    }
                    else if (this.IsAsyncServerMode)
                    {
                        base.ValueContainer.SetEditValue(this.CreateEditableItem(obj4), UpdateEditorSource.TextInput);
                    }
                    else
                    {
                        base.ValueContainer.SetEditValue(null, UpdateEditorSource.TextInput);
                    }
                }
                else
                {
                    IEnumerable<object> items = (IEnumerable<object>) selectedItemsFromListBox;
                    IEnumerable<object> input = this.SelectorUpdater.FilterSelectedItems(items);
                    if (this.IsSyncServerMode)
                    {
                        List<object> editValue = (from x in input select this.ItemsProvider.GetValueByRowKey(x, this.ItemsProvider.CurrentDataViewHandle)).ToList<object>();
                        base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                    }
                    else if (this.IsAsyncServerMode)
                    {
                        List<object> editValue = (from x in input select this.ItemsProvider.GetValueByRowKey(x, this.ItemsProvider.CurrentDataViewHandle)).ToList<object>();
                        base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
                    }
                    else
                    {
                        Func<IEnumerable<object>, List<object>> evaluator = <>c.<>9__58_2;
                        if (<>c.<>9__58_2 == null)
                        {
                            Func<IEnumerable<object>, List<object>> local1 = <>c.<>9__58_2;
                            evaluator = <>c.<>9__58_2 = x => x.ToList<object>();
                        }
                        base.ValueContainer.SetEditValue(this.SelectorUpdater.GetEditValueFromSelectedItems(input.With<IEnumerable<object>, List<object>>(evaluator)), UpdateEditorSource.TextInput);
                    }
                }
            }
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.SelectorUpdater.SyncICollectionView(base.ValueContainer.EditValue);
            if (this.Editor.ListBoxCore != null)
            {
                this.Editor.ListBoxCore.SyncValuesWithOwnerEdit(false);
            }
        }

        protected virtual int TextSearchCallback(string prefix, int startIndex, bool ignoreStartIndex) => 
            this.ItemsProvider.FindItemIndexByText(prefix, false, true, this.ItemsProvider.CurrentDataViewHandle, startIndex, true, ignoreStartIndex);

        public virtual void UnSelectAll()
        {
            base.ValueContainer.SetEditValue(null, UpdateEditorSource.ValueChanging);
        }

        private EditorListBox ListBox =>
            this.Editor.ListBoxCore;

        private ListBoxEdit Editor =>
            (ListBoxEdit) base.Editor;

        private ListBoxEditSettings Settings =>
            this.Editor.Settings;

        private BaseListBoxEditStyleSettings StyleSettings =>
            (BaseListBoxEditStyleSettings) base.StyleSettings;

        public bool IsInLookUpMode =>
            this.ItemsProvider.IsInLookUpMode;

        public bool IsAsyncServerMode =>
            this.ItemsProvider.IsAsyncServerMode;

        public bool IsSyncServerMode =>
            this.ItemsProvider.IsSyncServerMode;

        public bool IsServerMode =>
            this.IsSyncServerMode || this.IsAsyncServerMode;

        internal DevExpress.Xpf.Editors.Native.TextSearchEngine TextSearchEngine { get; set; }

        private bool HasHighlightedText =>
            !string.IsNullOrEmpty((string) this.Editor.GetValue(TextBlockService.HighlightedTextProperty));

        private ListBoxEditBasePropertyProvider PropertyProvider =>
            base.PropertyProvider as ListBoxEditBasePropertyProvider;

        private EditorTextSearchHelper TextSearchHelper
        {
            get
            {
                this.textSearchHelper ??= new EditorTextSearchHelper(this);
                return this.textSearchHelper;
            }
        }

        private IItemsProvider2 ItemsProvider =>
            this.Editor.ItemsProvider;

        private bool IsSingleSelection =>
            this.Editor.Settings.SelectionMode == SelectionMode.Single;

        protected bool ShouldLockSelection =>
            this.selectionChangedLocker.IsLocked;

        protected internal SelectorPropertiesCoercionHelper SelectorUpdater =>
            this.selectorUpdater;

        internal object CurrentDataViewHandle =>
            this.ItemsProvider.CurrentDataViewHandle;

        DevExpress.Xpf.Editors.Native.TextSearchEngine ISelectorEditStrategy.TextSearchEngine =>
            this.TextSearchEngine;

        bool ISelectorEditStrategy.IsTokenMode =>
            false;

        object ISelectorEditStrategy.CurrentDataViewHandle =>
            this.CurrentDataViewHandle;

        object ISelectorEditStrategy.TokenDataViewHandle =>
            null;

        int ISelectorEditStrategy.EditableTokenIndex =>
            -1;

        bool ISelectorEditStrategy.IsInProcessNewValue =>
            false;

        string ISelectorEditStrategy.SearchText =>
            this.TextSearchEngine.Prefix;

        IItemsProvider2 ISelectorEditStrategy.ItemsProvider =>
            this.ItemsProvider;

        ISelectorEdit ISelectorEditStrategy.Editor =>
            this.Editor;

        bool ISelectorEditStrategy.IsSingleSelection =>
            this.IsSingleSelection;

        object ISelectorEditStrategy.EditValue
        {
            get => 
                base.ValueContainer.EditValue;
            set => 
                base.ValueContainer.SetEditValue(value, UpdateEditorSource.ValueChanging);
        }

        RoutedEvent ISelectorEditStrategy.SelectedIndexChangedEvent =>
            ListBoxEdit.SelectedIndexChangedEvent;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxEditStrategy.<>c <>9 = new ListBoxEditStrategy.<>c();
            public static Func<IEnumerable<object>, List<object>> <>9__58_2;
            public static Func<DataProxy, object> <>9__61_0;
            public static PropertyCoercionHandler <>9__62_0;

            internal object <GetSelectedRowKey>b__61_0(DataProxy x) => 
                x.f_RowKey;

            internal object <RegisterUpdateCallbacks>b__62_0(object baseValue) => 
                baseValue;

            internal List<object> <SyncWithEditorInternal>b__58_2(IEnumerable<object> x) => 
                x.ToList<object>();
        }
    }
}

