namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class LookUpEditStrategyBase : ButtonEditStrategy, ISelectorEditStrategy, IEditStrategy
    {
        protected internal static object SpecialNull = new object();
        private readonly SelectorPropertiesCoercionHelper selectorUpdater;
        private EditorTextSearchHelper textSearchHelper;

        protected LookUpEditStrategyBase(LookUpEditBase editor) : base(editor)
        {
            this.selectorUpdater = new SelectorPropertiesCoercionHelper();
            this.SelectorUpdater.SetOwner(this);
            this.ProcessNewValueHelper = new UpdateDataSourceOnAsyncProcessNewValueEventHelper(this.Editor);
            this.ProcessValidateOnEnterLocker = new Locker();
            this.AsyncServerModeUpdateLocker = new Locker();
            this.FilterChangedLocker = new Locker();
            this.AcceptPopupValueAction = new PostponedAction(() => this.VisualClient.IsPopupOpened);
            this.TextSearchEngine = this.CreateTextSearchEngine(editor);
        }

        public virtual void AcceptPopupValue(bool force = false)
        {
            if (!this.Editor.IsReadOnly)
            {
                object selectedItems = this.IsSingleSelection ? this.VisualClient.GetSelectedItem() : this.VisualClient.GetSelectedItems().Cast<object>().ToList<object>();
                if (force)
                {
                    this.AcceptPopupValueInternal(selectedItems);
                }
                else
                {
                    this.AcceptPopupValueAction.PerformPostpone(() => this.AcceptPopupValueInternal(selectedItems));
                }
            }
        }

        protected virtual void AcceptPopupValueInternal(object selectedItems)
        {
            base.EditBox.BeforeAcceptPopupValue();
            object filteredValueFromSelectedItems = this.GetFilteredValueFromSelectedItems(selectedItems);
            if (filteredValueFromSelectedItems != SpecialNull)
            {
                if (this.IsInTokenMode && this.IsSingleSelection)
                {
                    this.AcceptPopupValueInternalInSingleTokenMode(filteredValueFromSelectedItems);
                }
                else
                {
                    base.ValueContainer.SetEditValue(this.CreateEditValueForAcceptPopupValue(selectedItems, filteredValueFromSelectedItems), UpdateEditorSource.ValueChanging);
                }
                base.EditBox.AfterAcceptPopupValue();
                this.UpdateDisplayText();
                if (this.PropertyProvider.SelectAllOnAcceptPopup)
                {
                    this.SelectAll();
                }
            }
        }

        private void AcceptPopupValueInternalInSingleTokenMode(object newValue)
        {
            if (newValue != null)
            {
                IEnumerable source = (base.ValueContainer.EditValue is LookUpEditableItem) ? (((LookUpEditableItem) base.ValueContainer.EditValue).EditValue as IEnumerable) : (base.ValueContainer.EditValue as IEnumerable);
                List<object> list = new List<object>();
                if (source == null)
                {
                    list.Add(newValue);
                }
                else
                {
                    TokenEditorCustomItem item = base.EditBox.EditValue as TokenEditorCustomItem;
                    int index = (item != null) ? item.EditableTokenIndex : -1;
                    list.AddRange(source.Cast<object>());
                    if ((index <= -1) || (index >= list.Count))
                    {
                        list.Add(newValue);
                    }
                    else
                    {
                        IList list2 = item.EditValue as IList;
                        if ((list2 != null) && (list2.Count == list.Count))
                        {
                            list[index] = newValue;
                        }
                        else
                        {
                            list.Insert(index, newValue);
                        }
                    }
                }
                LookUpEditableItem editValue = new LookUpEditableItem();
                editValue.EditValue = list;
                editValue.DisplayValue = this.GetDisplayTextForTokenEditor(newValue);
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.ValueChanging);
            }
        }

        protected internal override void AddILogicalOwnerChild(object child)
        {
            base.AddILogicalOwnerChild(child);
            this.ProcessNewValueHelper.SetFloatingContainer(child);
        }

        public virtual void AddNew(object parameter)
        {
            string editText = string.IsNullOrEmpty(parameter as string) ? base.EditBox.Text : ((string) parameter);
            this.ProcessNewValueCore(editText);
        }

        public virtual void AddNewCommandChanged()
        {
            this.PropertyProvider.AddNewCommand = this.Editor.AddNewCommand;
        }

        protected override void AfterApplyStyleSettings()
        {
            base.AfterApplyStyleSettings();
            if (!this.PropertyProvider.CreatedFromSettings || (this.Editor.EditMode != EditMode.InplaceActive))
            {
                base.SyncWithValue();
            }
            this.VisualClient.SyncProperties(true);
        }

        public override void AfterOnGotFocus()
        {
            if (!this.IncrementalFiltering || string.IsNullOrEmpty(this.Editor.AutoSearchText))
            {
                base.AfterOnGotFocus();
            }
        }

        public virtual void AllowCollectionViewChanged(bool value)
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

        public virtual void AutoCompleteChanged(bool value)
        {
            base.ItemsProvider.ResetDisplayTextCache();
        }

        public virtual void AutoSeachTextChanged(string text)
        {
            this.UpdateDisplayFilter();
            this.VisualClient.SyncProperties(false);
        }

        protected internal void BeforePopupOpened()
        {
            if (this.IsInTokenMode && !this.Editor.GetIsEditorKeyboardFocused())
            {
                if ((this.TokenCurrentDataViewHandle == null) || (this.TokenFilterDataViewHandle == null))
                {
                    this.UpdateTokenEditorSnapshot(true);
                }
                this.UpdateDisplayFilter();
            }
        }

        private bool CalcLookUpEditableItemCompleted(int index) => 
            !this.IsAsyncServerMode ? (!this.IsInLookUpMode || (index > -1)) : (index > -1);

        private int CalcOffset(bool searchNext)
        {
            int num2;
            int selectedIndex = base.ItemsProvider.IndexOfValue(this.GetActualEditValue(), this.CurrentDataViewHandle);
            if (base.ItemsProvider.GetControllerIndexByIndex(selectedIndex, this.CurrentDataViewHandle) < 0)
            {
                num2 = -1;
            }
            int num3 = 0;
            while (true)
            {
                num3 += searchNext ? 1 : -1;
                int indexByControllerIndex = base.ItemsProvider.GetIndexByControllerIndex(num2 + num3, this.CurrentDataViewHandle);
                if (!this.IndexInRange(indexByControllerIndex))
                {
                    return 0;
                }
                if (this.IsEnabledContainer(num2 + num3))
                {
                    return num3;
                }
            }
        }

        private bool CalcUpdateAutoCompleteSelection() => 
            this.IsInAutoCompleteSelection;

        protected internal override void CancelTextSearch()
        {
            this.TextSearchHelper.CancelTextSearch();
        }

        private bool CanProcessSpecialNullInTokenMode(IEnumerable<object> filteredItems)
        {
            if ((filteredItems == null) || (filteredItems.Count<object>() == 0))
            {
                return true;
            }
            object provideValue = null;
            if (!this.ProvideEditValue(base.ValueContainer.EditValue, out provideValue, UpdateEditorSource.TextInput))
            {
                return false;
            }
            IList<object> first = provideValue as IList<object>;
            return ((first != null) && first.SequenceEqual<object>((this.SelectorUpdater.GetEditValueFromSelectedItems(filteredItems.ToList<object>()) as IList<object>)));
        }

        protected internal override bool CanSpinDown() => 
            base.CanSpinDown() && (this.PropertyProvider.IsSingleSelection || this.IsInTokenMode);

        protected internal override bool CanSpinUp() => 
            base.CanSpinUp() && (this.PropertyProvider.IsSingleSelection || this.IsInTokenMode);

        protected virtual bool ChangeIndexAndHandle(int d)
        {
            object dataViewHandleInTokenMode = this.GetDataViewHandleInTokenMode();
            object actualEditValue = this.GetActualEditValue();
            int index = this.FindNextIndex(actualEditValue, Math.Sign(d) > 0, dataViewHandleInTokenMode);
            if (this.IndexInRange(index))
            {
                object valueByIndex;
                if (!this.IsInTokenMode)
                {
                    valueByIndex = base.ItemsProvider.GetValueByIndex(index, dataViewHandleInTokenMode);
                }
                else
                {
                    ChangeTextItem editItem = new ChangeTextItem();
                    editItem.Text = this.GetDisplayTextForTokenEditor(base.ItemsProvider.GetValueByIndex(index, dataViewHandleInTokenMode));
                    valueByIndex = this.CreateEditableItemInTokenMode(index, editItem);
                }
                object editValue = valueByIndex;
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
            }
            return true;
        }

        public override void Clear()
        {
            base.ValueContainer.ResetUpdateSource();
            base.Clear();
        }

        public virtual void ClearSelectionOnBackspace(bool value)
        {
        }

        public virtual int CoerceSelectedIndex(int baseIndex)
        {
            this.CoerceValue(LookUpEditBase.SelectedIndexProperty, baseIndex);
            return this.SelectorUpdater.CoerceSelectedIndex(baseIndex);
        }

        public virtual object CoerceSelectedItem(object baseItem)
        {
            this.CoerceValue(LookUpEditBase.SelectedItemProperty, baseItem);
            return this.SelectorUpdater.CoerceSelectedItem(baseItem);
        }

        protected internal override object ConvertEditValueForFormatDisplayText(object editValue)
        {
            object displayValue = base.ConvertEditValueForFormatDisplayText(editValue);
            displayValue = this.GetDisplayValue(displayValue);
            IList list = displayValue as IList;
            if ((list != null) && (list.Count == 0))
            {
                displayValue = null;
            }
            return displayValue;
        }

        protected virtual CriteriaOperator CreateDisplayFilter()
        {
            if (this.PropertyProvider.EditMode == EditMode.InplaceInactive)
            {
                return null;
            }
            string searchText = this.IncrementalFiltering ? this.AutoSearchText : string.Empty;
            CriteriaOperator displayFilter = base.ItemsProvider.CreateDisplayFilterCriteria(searchText, this.PropertyProvider.FilterCondition);
            SubstituteDisplayFilterEventArgs args = new SubstituteDisplayFilterEventArgs(displayFilter, searchText);
            this.Editor.RaiseSubstituteDisplayFilter(args);
            return (args.Handled ? args.DisplayFilter : displayFilter);
        }

        private CriteriaOperator CreateDisplayFilterInTokenMode(IEnumerable editValue) => 
            (editValue != null) ? CriteriaOperator.And(this.CreateDisplayFilter(), this.CreateValueFilter(editValue)) : this.CreateDisplayFilter();

        private CriteriaOperator CreateDisplayMemberFilter(IEnumerable editValue)
        {
            List<object> values = (from x in editValue.Cast<object>() select base.ItemsProvider.GetDisplayValueByEditValue(x, this.CurrentDataViewHandle)).ToList<object>();
            return this.CreateValueFilterCore(values, base.ItemsProvider.GetDisplayPropertyName(this.GetDataViewHandleInTokenMode()));
        }

        protected virtual object CreateEditableItem(int index, ChangeTextItem item) => 
            !this.IsInTokenMode ? this.CreateSingleEditableItem(index, item) : this.CreateEditableItemInTokenMode(index, item);

        private LookUpEditableItem CreateEditableItemForExistentValue(int index, object editValue, ChangeTextItem item)
        {
            // Unresolved stack state at '000000AE'
        }

        protected virtual LookUpEditableItem CreateEditableItemForNonexistentValue(int index, object value, ChangeTextItem item)
        {
            // Unresolved stack state at '000000E6'
        }

        private object CreateEditableItemInTokenMode(int index, ChangeTextItem editItem)
        {
            TokenEditorCustomItem editValue = base.EditBox.EditValue as TokenEditorCustomItem;
            if ((editValue == null) || (editValue.EditValue == null))
            {
                return null;
            }
            IList<CustomItem> list = editValue.EditValue as IList<CustomItem>;
            if ((list == null) || (list.Count <= 0))
            {
                List<object> list5;
                object item = null;
                int num3 = (editValue != null) ? editValue.EditableTokenIndex : -1;
                item = this.CreateSingleEditableItem(index, editItem);
                if ((item == this.Editor.EditValue) || ((item is LookUpEditableItem) && (((LookUpEditableItem) item).EditValue == this.Editor.EditValue)))
                {
                    object valueFromEditValueByIndex = this.GetValueFromEditValueByIndex(num3, editItem.Text);
                    if ((valueFromEditValueByIndex == null) && string.IsNullOrEmpty(editItem.Text))
                    {
                        item = null;
                    }
                    else
                    {
                        LookUpEditableItem item4 = new LookUpEditableItem();
                        item4.EditValue = valueFromEditValueByIndex;
                        item4.DisplayValue = editItem.Text;
                        item = item4;
                    }
                }
                if ((item == null) || (!(item is LookUpEditableItem) || (((LookUpEditableItem) item).EditValue == null)))
                {
                    return null;
                }
                if (item is IList<object>)
                {
                    list5 = new List<object>((IList<object>) item);
                }
                else
                {
                    List<object> list1 = new List<object>();
                    list1.Add(item);
                    list5 = list1;
                }
                List<object> list3 = list5;
                List<object> list4 = new List<object>();
                list4.Add(list3);
                LookUpEditableItem item5 = new LookUpEditableItem();
                item5.EditValue = list4;
                item5.DisplayValue = editItem.Text;
                return item5;
            }
            List<object> list2 = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                CustomItem item2 = list[i];
                if (!editValue.EditableTokens.Contains(i))
                {
                    if (item2.EditValue != null)
                    {
                        list2.Add(item2.EditValue);
                    }
                }
                else
                {
                    string text = editItem.Text;
                    if (editValue.UseTokenDisplayText)
                    {
                        editItem.Text = item2.DisplayText;
                    }
                    int num2 = this.FindItemIndexByText(editItem.Text, this.Editor.AutoComplete);
                    object item = this.CreateSingleEditableItem(num2, editItem);
                    if ((item == this.Editor.EditValue) || ((item is LookUpEditableItem) && (((LookUpEditableItem) item).EditValue == this.Editor.EditValue)))
                    {
                        LookUpEditableItem item1 = new LookUpEditableItem();
                        item1.EditValue = this.GetValueFromEditValueByIndex(i, editItem.Text);
                        item1.DisplayValue = editItem.Text;
                        item = item1;
                    }
                    if (item != null)
                    {
                        list2.Add(item);
                    }
                    else
                    {
                        list2.Add(new LookUpEditableItem());
                    }
                    editItem.Text = text;
                }
            }
            if (list2.Count <= 0)
            {
                return null;
            }
            LookUpEditableItem item3 = new LookUpEditableItem();
            item3.EditValue = list2;
            item3.DisplayValue = editItem.Text;
            return item3;
        }

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new LookUpEditValidator(this, this.selectorUpdater, this.Editor);

        private object CreateEditValueForAcceptPopupValue(object selectedItems, object filteredItems)
        {
            LookUpEditableItem item1;
            if (!this.IsAsyncServerMode)
            {
                return (!this.IsSingleSelection ? this.FilterSelectedItemsFromEmptyList(filteredItems) : filteredItems);
            }
            int index = base.ItemsProvider.IndexOfValue(filteredItems, this.CurrentDataViewHandle);
            LookUpEditableItem editValue = base.ValueContainer.EditValue as LookUpEditableItem;
            if (index <= -1)
            {
                Func<LookUpEditableItem, ChangeTextItem> evaluator = <>c.<>9__182_1;
                if (<>c.<>9__182_1 == null)
                {
                    Func<LookUpEditableItem, ChangeTextItem> local1 = <>c.<>9__182_1;
                    evaluator = <>c.<>9__182_1 = x => x.ChangeTextItem;
                }
                item1 = this.CreateEditableItemForNonexistentValue(index, this.FilterSelectedItemFromUnsetValue(selectedItems), editValue.With<LookUpEditableItem, ChangeTextItem>(evaluator));
            }
            else
            {
                Func<LookUpEditableItem, ChangeTextItem> evaluator = <>c.<>9__182_0;
                if (<>c.<>9__182_0 == null)
                {
                    Func<LookUpEditableItem, ChangeTextItem> local2 = <>c.<>9__182_0;
                    evaluator = <>c.<>9__182_0 = x => x.ChangeTextItem;
                }
                item1 = this.CreateEditableItemForExistentValue(index, filteredItems, editValue.With<LookUpEditableItem, ChangeTextItem>(evaluator));
            }
            LookUpEditableItem item2 = item1;
            item2.ForbidFindIncremental = true;
            item2.AcceptedFromPopup = true;
            return item2;
        }

        private object CreateSingleEditableItem(int index, ChangeTextItem item)
        {
            object obj2 = this.ShouldAssignNullValueWithText(index, item.Text) ? this.Editor.NullValue : item.Text;
            if (!this.IsInLookUpMode)
            {
                return (!item.AutoCompleteTextDeleted ? ((index > -1) ? this.CreateEditableItemForExistentValue(index, this.SelectorUpdater.GetEditValueFromSelectedIndex(index), item) : this.CreateEditableItemForNonexistentValue(index, obj2, item)) : this.CreateEditableItemForNonexistentValue(index, obj2, item));
            }
            object obj3 = (index > -1) ? this.SelectorUpdater.GetEditValueFromSelectedIndex(index) : this.Editor.EditValue;
            if (this.ShouldAssignNullValueWithText(index, item.Text))
            {
                obj3 = obj2;
            }
            if (((this.Editor.ValidateOnTextInput || this.Editor.AutoComplete) && !item.AutoCompleteTextDeleted) && !this.ProcessValidateOnEnterLocker)
            {
                return ((index <= -1) ? ((!this.Editor.AssignNullValueOnClearingEditText || !string.IsNullOrEmpty(item.Text)) ? this.CreateEditableItemForNonexistentValue(-1, obj3, item) : this.CreateEditableItemForExistentValue(index, obj3, item)) : this.CreateEditableItemForExistentValue(index, obj3, item));
            }
            LookUpEditableItem item1 = new LookUpEditableItem();
            item1.DisplayValue = item.Text;
            item1.EditValue = obj3;
            item1.ChangeTextItem = item;
            item1.AsyncLoading = this.Editor.IsAsyncOperationInProgress;
            item1.AutoCompleteTextDeleted = item.AutoCompleteTextDeleted;
            item1.Completed = false;
            item1.AcceptedFromPopup = item.AcceptedFromPopup;
            return item1;
        }

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new LookUpEditBaseSettingsService(this.Editor);

        protected virtual DevExpress.Xpf.Editors.Native.TextSearchEngine CreateTextSearchEngine(LookUpEditBase editor) => 
            new DevExpress.Xpf.Editors.Native.TextSearchEngine(new Func<string, int, bool, int>(this.TextSearchCallback), LookUpEditBase.TextSearchTimeOut);

        private CriteriaOperator CreateValueFilter(IEnumerable editValue) => 
            base.ItemsProvider.HasValueMember ? this.CreateValueMemberFilter(editValue) : this.CreateDisplayMemberFilter(editValue);

        private CriteriaOperator CreateValueFilterCore(List<object> values, string propertyName)
        {
            this.RemoveCurrentEditableValueFromFilter(values);
            return ((values.Count > 0) ? new NotOperator(new InOperator(propertyName, values)) : null);
        }

        private CriteriaOperator CreateValueMemberFilter(IEnumerable editValue)
        {
            string valuePropertyName = base.ItemsProvider.GetValuePropertyName(this.GetDataViewHandleInTokenMode());
            return this.CreateValueFilterCore(new List<object>(editValue.Cast<object>()), valuePropertyName);
        }

        protected virtual void DestroyVisibleListSource()
        {
            if (this.IsServerMode)
            {
                base.ItemsProvider.DestroyVisibleListSource(this.VisibleListHandle);
            }
        }

        void ISelectorEditStrategy.BringToView()
        {
        }

        object ISelectorEditStrategy.GetCurrentDataViewHandle() => 
            !this.IsInTokenMode ? this.CurrentDataViewHandle : this.TokenCurrentDataViewHandle;

        object ISelectorEditStrategy.GetCurrentEditableValue()
        {
            object provideValue = null;
            if (!this.ProvideEditValue(base.EditValue, out provideValue, UpdateEditorSource.TextInput) || !this.IsInTokenMode)
            {
                return provideValue;
            }
            int currentEditableTokenIndex = this.GetCurrentEditableTokenIndex();
            IList list = provideValue as IList;
            return (((currentEditableTokenIndex == -1) || ((list == null) || (list.Count <= currentEditableTokenIndex))) ? list : list[currentEditableTokenIndex]);
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
            this.GetVisibleListSource();

        IEnumerable ISelectorEditStrategy.GetInnerEditorMRUItemsSource() => 
            new List<CustomItem>();

        object ISelectorEditStrategy.GetNextValueFromSearchText(int startIndex) => 
            this.GetNextValueFromSearchTextInternal(startIndex);

        object ISelectorEditStrategy.GetPrevValueFromSearchText(int startIndex) => 
            this.GetPrevValueFromSearchTextInternal(startIndex);

        public virtual void DisplayMemberChanged(string displayMember)
        {
            this.Settings.DisplayMember = displayMember;
            this.UpdateDisplayText();
        }

        protected internal override bool DoTextSearch(string text, int startIndex, ref object result) => 
            this.TextSearchHelper.DoTextSearch(text, startIndex, ref result);

        public virtual void FilterByColumnsModeChanged(FilterByColumnsMode? value)
        {
            this.VisualClient.SyncProperties(false);
        }

        public virtual void FilterConditionChanged(FilterCondition? filterCondition)
        {
            this.Settings.FilterCondition = filterCondition;
            this.UpdateDisplayFilter();
        }

        public virtual void FilterCriteriaChanged(CriteriaOperator criteriaOperator)
        {
            this.Settings.FilterCriteria = criteriaOperator;
            base.SyncWithValue();
        }

        private object FilterSelectedItemFromUnsetValue(object selectedItem) => 
            LookUpPropertyDescriptorBase.IsUnsetValue(selectedItem) ? null : selectedItem;

        private object FilterSelectedItemsFromEmptyList(object filteredItems)
        {
            Func<IList, bool> evaluator = <>c.<>9__183_0;
            if (<>c.<>9__183_0 == null)
            {
                Func<IList, bool> local1 = <>c.<>9__183_0;
                evaluator = <>c.<>9__183_0 = x => x.Count > 0;
            }
            return (filteredItems as IList).If<IList>(evaluator).Return<IList, object>(x => filteredItems, null);
        }

        public virtual void Find(object parameter)
        {
            CriteriaOperator @operator;
            if (parameter is CriteriaOperator)
            {
                @operator = parameter as CriteriaOperator;
            }
            else
            {
                string searchText = Convert.ToString(parameter);
                @operator = base.ItemsProvider.CreateDisplayFilterCriteria(searchText, this.PropertyProvider.FilterCondition);
            }
            base.ItemsProvider.SetDisplayFilterCriteria(@operator, this.CurrentDataViewHandle);
        }

        private object FindEditValueFromText(object baseValue) => 
            !this.IsInLookUpMode ? Convert.ToString(baseValue) : base.ItemsProvider.GetValueByIndex(base.ItemsProvider.FindItemIndexByText(Convert.ToString(baseValue), true, false, this.CurrentDataViewHandle, -1, true, false), this.CurrentDataViewHandle);

        public virtual int FindItemIndexByText(string text, bool autoComplete) => 
            base.ItemsProvider.FindItemIndexByText(text, this.Editor.IsCaseSensitiveSearch, autoComplete, this.GetDataViewHandleInTokenMode(), -1, true, false);

        private int FindItemIndexByText(string text, bool autoComplete, object handle) => 
            base.ItemsProvider.FindItemIndexByText(text, this.Editor.IsCaseSensitiveSearch, autoComplete, handle, -1, true, false);

        public virtual void FindModeChanged(FindMode? findMode)
        {
            this.Settings.FindMode = findMode;
        }

        private int FindNextIndex(object editValue, bool searchNext, object handle)
        {
            int startIndex = base.ItemsProvider.IndexOfValue(editValue, handle);
            if (this.IncrementByFindIncremental)
            {
                string autoSearchText = this.Editor.AutoSearchText;
                return base.ItemsProvider.FindItemIndexByText(autoSearchText, this.Editor.IsCaseSensitiveSearch, true, handle, startIndex, searchNext, true);
            }
            int controllerIndexByIndex = base.ItemsProvider.GetControllerIndexByIndex(startIndex, handle);
            if (controllerIndexByIndex < 0)
            {
                controllerIndexByIndex = -1;
            }
            int newControllerIndex = controllerIndexByIndex + this.CalcOffset(searchNext);
            return base.ItemsProvider.GetIndexByControllerIndex(newControllerIndex, handle);
        }

        private void FlushAutoSearchText()
        {
            this.AutoSearchText = string.Empty;
        }

        internal override void FlushPendingEditActions(UpdateEditorSource updateSource)
        {
            base.FlushPendingEditActions(updateSource);
            this.FlushAutoSearchText();
        }

        private object GetActualEditValue()
        {
            object currentEditableValue = this.GetCurrentEditableValue();
            return ((currentEditableValue is LookUpEditableItem) ? ((LookUpEditableItem) currentEditableValue).EditValue : currentEditableValue);
        }

        internal int GetCurrentEditableTokenIndex()
        {
            Func<TokenEditorCustomItem, int> evaluator = <>c.<>9__206_0;
            if (<>c.<>9__206_0 == null)
            {
                Func<TokenEditorCustomItem, int> local1 = <>c.<>9__206_0;
                evaluator = <>c.<>9__206_0 = x => x.EditableTokenIndex;
            }
            return (base.EditBox.EditValue as TokenEditorCustomItem).Return<TokenEditorCustomItem, int>(evaluator, (<>c.<>9__206_1 ??= () => -1));
        }

        private object GetCurrentEditableValue()
        {
            object obj2;
            return (!this.IsInTokenMode ? (!this.ProvideEditValue(base.ValueContainer.EditValue, out obj2, UpdateEditorSource.TextInput) ? null : obj2) : this.GetEditableItemInTokenMode());
        }

        public virtual object GetCurrentSelectedItem() => 
            this.SelectorUpdater.GetCurrentSelectedItem(base.ValueContainer);

        public virtual IEnumerable GetCurrentSelectedItems() => 
            this.SelectorUpdater.GetCurrentSelectedItems(base.ValueContainer);

        private CustomItem GetCustomItemFromValue(object value, string text = null)
        {
            CustomItem item = new CustomItem();
            if (value != null)
            {
                if (!(value is LookUpEditableItem))
                {
                    item.EditValue = value;
                    string displayTextForTokenEditor = text;
                    if (text == null)
                    {
                        string local3 = text;
                        displayTextForTokenEditor = this.GetDisplayTextForTokenEditor(value);
                    }
                    item.DisplayText = displayTextForTokenEditor;
                }
                else
                {
                    LookUpEditableItem item2 = value as LookUpEditableItem;
                    item.EditValue = item2;
                    Func<object, string> evaluator = <>c.<>9__101_0;
                    if (<>c.<>9__101_0 == null)
                    {
                        Func<object, string> local1 = <>c.<>9__101_0;
                        evaluator = <>c.<>9__101_0 = x => x.ToString();
                    }
                    item.DisplayText = item2.DisplayValue.Return<object, string>(evaluator, <>c.<>9__101_1 ??= () => string.Empty);
                }
            }
            return item;
        }

        internal object GetDataViewHandleInTokenMode() => 
            this.IsInTokenMode ? this.TokenFilterDataViewHandle : this.CurrentDataViewHandle;

        private string GetDisplayTextForTokenEditor(object value)
        {
            bool applyFormatting = this.ApplyDisplayTextConversion || (this.GetCurrentEditableTokenIndex() == -1);
            if (this.IncrementalFiltering)
            {
                return this.Editor.GetDisplayText(value, applyFormatting);
            }
            int index = base.ItemsProvider.IndexOfValue(value, this.CurrentDataViewHandle);
            if (index > -1)
            {
                return base.ItemsProvider.GetDisplayValueByIndex(index, this.CurrentDataViewHandle).ToString();
            }
            Func<object, string> evaluator = <>c.<>9__102_0;
            if (<>c.<>9__102_0 == null)
            {
                Func<object, string> local1 = <>c.<>9__102_0;
                evaluator = <>c.<>9__102_0 = x => x.ToString();
            }
            return value.Return<object, string>(evaluator, () => ((ChangeTextItem) this.GetEditableObject()).Text);
        }

        protected virtual object GetDisplayValue(object editValue)
        {
            LookUpEditableItem item = editValue as LookUpEditableItem;
            return ((item != null) ? item.DisplayValue : base.ItemsProvider.GetDisplayValueByEditValue(editValue, this.CurrentDataViewHandle));
        }

        private object GetEditableItemInTokenMode()
        {
            if (base.ValueContainer.EditValue is LookUpEditableItem)
            {
                int num = this.GetCurrentEditableTokenIndex();
                IList<object> list = (IList<object>) (base.ValueContainer.EditValue as LookUpEditableItem).EditValue;
                return (((num <= -1) || (num >= list.Count)) ? null : list[num]);
            }
            IList<object> editValue = base.ValueContainer.EditValue as IList<object>;
            int currentEditableTokenIndex = this.GetCurrentEditableTokenIndex();
            return (((editValue == null) || ((currentEditableTokenIndex <= -1) || (currentEditableTokenIndex >= editValue.Count))) ? null : editValue[currentEditableTokenIndex]);
        }

        protected override object GetEditableObject()
        {
            string text = base.EditBox.Text;
            if (!this.IsInTokenMode && (this.Editor.AutoComplete && this.IsInAutoCompleteSelection))
            {
                text = text.Substring(0, base.EditBox.SelectionStart);
            }
            if (!this.AsyncServerModeUpdateLocker.IsLocked)
            {
                ChangeTextItem item1 = new ChangeTextItem();
                item1.Text = text;
                item1.UpdateAutoCompleteSelection = this.CalcUpdateAutoCompleteSelection();
                return item1;
            }
            LookUpEditableItem editValue = base.ValueContainer.EditValue as LookUpEditableItem;
            if (editValue == null)
            {
                ChangeTextItem item3 = new ChangeTextItem();
                item3.Text = text;
                return item3;
            }
            ChangeTextItem item2 = editValue.ChangeTextItem ?? new ChangeTextItem();
            item2.AsyncLoading = editValue.AsyncLoading;
            item2.AcceptedFromPopup = editValue.AcceptedFromPopup;
            if (!editValue.AsyncLoading && !editValue.AutoCompleteTextDeleted)
            {
                item2.Text = text;
                item2.UpdateAutoCompleteSelection = this.CalcUpdateAutoCompleteSelection();
            }
            return item2;
        }

        private object GetEditValueForTokenEditor(string displayText)
        {
            object editValue = base.ValueContainer.EditValue;
            object obj3 = (editValue is LookUpEditableItem) ? ((LookUpEditableItem) editValue).EditValue : editValue;
            TokenEditorCustomItem item = base.EditBox.EditValue as TokenEditorCustomItem;
            IList list = obj3 as IList;
            if ((item != null) && ((item.EditableTokenIndex != -1) && (item.EditValue != null)))
            {
                IList<CustomItem> list2 = item.EditValue as IList<CustomItem>;
                foreach (int num in item.EditableTokens)
                {
                    string local3;
                    object obj4 = null;
                    if ((list != null) && (list2.Count <= list.Count))
                    {
                        obj4 = (num < list.Count) ? list[num] : null;
                    }
                    if (obj4 is LookUpEditableItem)
                    {
                        Func<object, string> evaluator = <>c.<>9__98_0;
                        if (<>c.<>9__98_0 == null)
                        {
                            Func<object, string> local1 = <>c.<>9__98_0;
                            evaluator = <>c.<>9__98_0 = x => x.ToString();
                        }
                        local3 = ((LookUpEditableItem) obj4).DisplayValue.Return<object, string>(evaluator, <>c.<>9__98_1 ??= () => string.Empty);
                    }
                    else
                    {
                        local3 = null;
                    }
                    list2[num] = this.GetCustomItemFromValue(obj4, local3);
                }
                TokenEditorCustomItem item1 = new TokenEditorCustomItem();
                item1.EditableTokens = item.EditableTokens;
                item1.EditValue = list2;
                return item1;
            }
            if (base.ShouldShowNullText)
            {
                TokenEditorCustomItem item4 = new TokenEditorCustomItem();
                item4.NullText = displayText;
                return item4;
            }
            if (obj3 == null)
            {
                return null;
            }
            TokenEditorCustomItem item2 = null;
            if (list == null)
            {
                List<int> list4 = new List<int>();
                list4.Add(this.GetCurrentEditableTokenIndex());
                TokenEditorCustomItem item6 = new TokenEditorCustomItem();
                item6.EditableTokens = list4;
                item2 = item6;
                CustomItem item7 = new CustomItem();
                item7.EditValue = obj3;
                item7.DisplayText = this.GetDisplayTextForTokenEditor(obj3);
                List<CustomItem> list5 = new List<CustomItem>();
                list5.Add(item7);
                item2.EditValue = list5;
            }
            else
            {
                List<CustomItem> list3 = new List<CustomItem>();
                foreach (object obj5 in list)
                {
                    CustomItem customItemFromValue = this.GetCustomItemFromValue(obj5, null);
                    if (customItemFromValue != null)
                    {
                        list3.Add(customItemFromValue);
                    }
                }
                if (list3.Count > 0)
                {
                    List<int> list1 = new List<int>();
                    list1.Add(this.GetCurrentEditableTokenIndex());
                    TokenEditorCustomItem item5 = new TokenEditorCustomItem();
                    item5.EditableTokens = list1;
                    item2 = item5;
                    item2.EditValue = list3;
                }
            }
            return item2;
        }

        public object GetEditValueFromText(object baseValue)
        {
            string str = baseValue as string;
            if (str == null)
            {
                return (this.IsSingleEditValue ? null : new List<object>());
            }
            if (this.IsSingleEditValue)
            {
                return this.FindEditValueFromText(baseValue);
            }
            List<object> list = new List<object>();
            string[] separator = new string[] { this.Editor.SeparatorString };
            list.AddRange(from editValue in str.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select<string, object>(new Func<string, object>(this.FindEditValueFromText))
                where base.ItemsProvider.IndexOfValue(editValue, this.CurrentDataViewHandle) > -1
                select editValue);
            return list;
        }

        protected internal object GetFilteredValueFromSelectedItems(object selectedItems)
        {
            if (this.IsSingleSelection)
            {
                object item = selectedItems;
                object obj3 = this.SelectorUpdater.FilterSelectedItem(item);
                return ((this.IsServerMode || (!CustomItemHelper.IsCustomItem(obj3) && (base.ItemsProvider.GetIndexByItem(obj3, this.CurrentDataViewHandle) <= -1))) ? (!this.IsServerMode ? SpecialNull : base.ItemsProvider.GetValueByRowKey(obj3, this.CurrentDataViewHandle)) : this.SelectorUpdater.GetEditValueFromSelectedItem(obj3));
            }
            IEnumerable<object> items = (IEnumerable<object>) selectedItems;
            IEnumerable<object> filteredItems = this.SelectorUpdater.FilterSelectedItems(items);
            if (this.IsServerMode)
            {
                return (from x in filteredItems select base.ItemsProvider.GetValueByRowKey(x, this.CurrentDataViewHandle)).ToList<object>();
            }
            if (this.IsInTokenMode)
            {
                LookUpEditableItem currentEditableValue = this.GetCurrentEditableValue() as LookUpEditableItem;
                if ((currentEditableValue != null) && ((currentEditableValue.EditValue == null) && ((currentEditableValue.DisplayValue != null) && this.CanProcessSpecialNullInTokenMode(filteredItems))))
                {
                    return SpecialNull;
                }
            }
            return this.SelectorUpdater.GetEditValueFromSelectedItems(filteredItems.ToList<object>());
        }

        protected virtual IEnumerable GetInnerEditorCustomItemsSource()
        {
            List<CustomItem> list = new List<CustomItem>();
            if (this.StyleSettings.ShowCustomItem(this.Editor) && this.StyleSettings.ShowCustomItem(this.Editor))
            {
                list.AddRange(this.StyleSettings.GetCustomItems(this.Editor));
            }
            return list;
        }

        private bool GetIsEnabled(object container) => 
            !(container is FrameworkElement) || ((FrameworkElement) container).IsEnabled;

        protected virtual object GetNextValueFromSearchTextInternal(int startIndex) => 
            this.GetValueFromSearchTextCore(startIndex, true);

        public virtual object GetPopupContentItemsSource() => 
            base.ItemsProvider.VisibleListSource;

        protected virtual object GetPrevValueFromSearchTextInternal(int startIndex) => 
            this.GetValueFromSearchTextCore(startIndex, false);

        internal object GetSelectedItemInternal() => 
            this.SelectorUpdater.GetSelectedItemFromEditValue(base.EditValue);

        public object GetSelectedItems(object editValue) => 
            this.SelectorUpdater.GetSelectedItemsFromEditValue(editValue);

        private int GetStartIndexToSeachText()
        {
            object obj2;
            this.ProvideEditValue(base.ValueContainer.EditValue, out obj2, UpdateEditorSource.TextInput);
            return this.SelectorUpdater.GetIndexFromEditValue(obj2);
        }

        public object GetTextFromEditValue(object baseValue) => 
            this.Editor.GetDisplayText(baseValue, false);

        private object GetValueFromEditValueByIndex(int index, string editText)
        {
            if (this.Editor.AssignNullValueOnClearingEditText && ((index == -1) && string.IsNullOrEmpty(editText)))
            {
                return this.Editor.NullValue;
            }
            IList<object> editValue = this.Editor.EditValue as IList<object>;
            return ((editValue == null) ? null : (((index <= -1) || (index >= editValue.Count)) ? null : editValue[index]));
        }

        private object GetValueFromSearchTextCore(int startIndex, bool isDown) => 
            this.HasHighlightedText ? this.TextSearchHelper.FindValueFromSearchText(startIndex, isDown, false) : null;

        protected virtual object GetVisibleListSource() => 
            base.ItemsProvider.GetVisibleListSource(this.VisibleListHandle);

        internal VisualClientOwner GetVisualClient() => 
            this.VisualClient;

        private bool IndexInRange(int index)
        {
            int controllerIndexByIndex = base.ItemsProvider.GetControllerIndexByIndex(index, this.CurrentDataViewHandle);
            return ((controllerIndexByIndex > -1) && (controllerIndexByIndex < base.ItemsProvider.GetCount(this.CurrentDataViewHandle)));
        }

        public virtual void IsCaseSensitiveFilterChanged(bool newValue)
        {
            this.Settings.IsCaseSensitiveFilter = newValue;
            this.UpdateDisplayText();
        }

        public virtual bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            this.VisualClient.IsClosePopupWithAcceptGesture(key, modifiers);

        public virtual bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            this.VisualClient.IsClosePopupWithCancelGesture(key, modifiers);

        protected override bool IsComputedNullValue(object value)
        {
            bool flag = base.IsComputedNullValue(value);
            return ((flag || this.IsSingleSelection) ? flag : (this.PropertyProvider.ShowNullTextForEmptyValue && this.IsEmptyValueInMultiSelectMode(value)));
        }

        private bool IsEmptyValueInMultiSelectMode(object value)
        {
            IList list = value as IList;
            return ((list != null) && (list.Count == 0));
        }

        protected virtual bool IsEnabledContainer(int controllerIndex)
        {
            object itemByControllerIndex = base.ItemsProvider.GetItemByControllerIndex(controllerIndex, this.CurrentDataViewHandle);
            return this.GetIsEnabled(itemByControllerIndex);
        }

        public virtual void IsSynchronizedWithCurrentItemChanged(bool value)
        {
            this.Settings.IsSynchronizedWithCurrentItem = value;
        }

        public virtual void ItemProviderChanged(ItemsProviderChangedEventArgs e)
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

                case (ItemsProviderFindIncrementalCompletedEventArgs _):
                    this.ItemsProviderFindIncrementalCompleted((ItemsProviderFindIncrementalCompletedEventArgs) e);
                    break;
            }
        }

        public virtual void ItemSourceChanged(object itemsSource)
        {
            bool isAsyncServerMode = this.IsAsyncServerMode;
            this.CancelTextSearch();
            this.Settings.AssignToEditCoreLocker.DoIfNotLocked(() => this.Settings.ItemsSource = itemsSource);
            if (this.IsAsyncServerMode != isAsyncServerMode)
            {
                this.Editor.UpdateButtonInfoCollections();
            }
            base.SyncWithValue();
        }

        protected virtual void ItemsProviderCurrentChanged(ItemsProviderCurrentChangedEventArgs e)
        {
            if (!this.FilterChangedLocker)
            {
                this.SelectorUpdater.SyncWithICollectionView(e.CurrentItem);
                this.UpdateDisplayText();
            }
        }

        protected virtual void ItemsProviderDataChanged(ItemsProviderDataChangedEventArgs e)
        {
            if (!this.SelectorUpdater.OptimizedSyncWithValueOnDataChanged(e, base.ValueContainer.EditValue, new Action(this.SyncWithValueOnDataChanged)))
            {
                this.SyncWithValueOnDataChanged();
                this.VisualClient.SyncProperties(true);
            }
        }

        private void ItemsProviderFindIncrementalCompleted(ItemsProviderFindIncrementalCompletedEventArgs e)
        {
            if (base.ValueContainer.HasValueCandidate)
            {
                this.ServerModeSyncWithEditor();
            }
        }

        protected virtual void ItemsProviderOnBusyChanged(ItemsProviderOnBusyChangedEventArgs e)
        {
            this.Editor.IsAsyncOperationInProgress = e.IsBusy;
            if (!e.IsBusy && !this.IsInTokenMode)
            {
                LookUpEditableItem editValue = base.ValueContainer.EditValue as LookUpEditableItem;
                Func<LookUpEditableItem, bool> evaluator = <>c.<>9__232_0;
                if (<>c.<>9__232_0 == null)
                {
                    Func<LookUpEditableItem, bool> local1 = <>c.<>9__232_0;
                    evaluator = <>c.<>9__232_0 = x => x.AsyncLoading;
                }
                bool flag = editValue.If<LookUpEditableItem>(evaluator).ReturnSuccess<LookUpEditableItem>();
                if ((editValue == null) || !flag)
                {
                    if (!this.VisualClient.IsPopupOpened)
                    {
                        base.SyncWithValue();
                    }
                }
                else
                {
                    Func<LookUpEditableItem, bool> func2 = <>c.<>9__232_1;
                    if (<>c.<>9__232_1 == null)
                    {
                        Func<LookUpEditableItem, bool> local2 = <>c.<>9__232_1;
                        func2 = <>c.<>9__232_1 = x => x.AcceptedFromPopup;
                    }
                    if (editValue.If<LookUpEditableItem>(func2).ReturnSuccess<LookUpEditableItem>())
                    {
                        object provideValue = null;
                        if (this.ProvideEditValue(editValue, out provideValue, UpdateEditorSource.TextInput))
                        {
                            this.ServerModeSyncWithValue(provideValue);
                            return;
                        }
                    }
                    this.ServerModeSyncWithEditor();
                }
            }
        }

        protected virtual void ItemsProviderRefreshed(ItemsProviderRefreshedEventArgs e)
        {
            if (!this.IsAsyncServerMode)
            {
                if (this.IsServerMode)
                {
                    if (base.ValueContainer.HasValueCandidate)
                    {
                        return;
                    }
                    base.SyncWithValue();
                }
            }
            else
            {
                Func<LookUpEditableItem, bool> evaluator = <>c.<>9__225_0;
                if (<>c.<>9__225_0 == null)
                {
                    Func<LookUpEditableItem, bool> local1 = <>c.<>9__225_0;
                    evaluator = <>c.<>9__225_0 = x => x.AsyncLoading && !x.AcceptedFromPopup;
                }
                if ((base.ValueContainer.EditValue as LookUpEditableItem).If<LookUpEditableItem>(evaluator).ReturnSuccess<LookUpEditableItem>())
                {
                    this.ServerModeSyncWithEditor();
                }
            }
            this.VisualClient.SyncProperties(true);
        }

        protected virtual void ItemsProviderRowLoaded(ItemsProviderRowLoadedEventArgs e)
        {
            object obj3;
            if ((e.Handle == null) && (this.ProvideEditValue(base.ValueContainer.EditValue, out obj3, UpdateEditorSource.TextInput) && Equals(obj3, e.Value)))
            {
                this.ServerModeSyncWithValue(obj3);
            }
        }

        protected virtual void ItemsProviderSelectionChanged(ItemsProviderSelectionChangedEventArgs e)
        {
            if (!this.Editor.IsPopupOpen)
            {
                if (!this.IsSingleSelection)
                {
                    IEnumerable<object> editValueFromBaseValue = (IEnumerable<object>) this.SelectorUpdater.GetEditValueFromBaseValue(base.ValueContainer.EditValue);
                    object[] second = new object[] { e.CurrentValue };
                    IEnumerable<object> source = e.IsSelected ? editValueFromBaseValue.Append<object>(second).Distinct<object>() : ((editValueFromBaseValue == null) ? new List<object>() : editValueFromBaseValue.Except<object>(second));
                    base.ValueContainer.SetEditValue(source.ToList<object>(), UpdateEditorSource.ValueChanging);
                }
                else
                {
                    object editValueFromBaseValue;
                    if (e.IsSelected)
                    {
                        editValueFromBaseValue = this.SelectorUpdater.GetEditValueFromBaseValue(e.CurrentValue);
                    }
                    else
                    {
                        ComboBoxEditItem item = base.ItemsProvider.GetItem(base.ValueContainer.EditValue, this.CurrentDataViewHandle) as ComboBoxEditItem;
                        editValueFromBaseValue = ((item == null) || !item.IsSelected) ? null : base.ValueContainer.EditValue;
                    }
                    base.ValueContainer.SetEditValue(editValueFromBaseValue, UpdateEditorSource.ValueChanging);
                }
                ((ISelectorEdit) this.Editor).Items.UpdateSelection(this.SelectorUpdater.GetSelectedItemsFromEditValue(base.ValueContainer.EditValue));
                this.VisualClient.SyncProperties(false);
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
                this.ServerModeSyncWithEditor();
            }
        }

        private void NavigateToNextSearchedItem()
        {
            this.UpdateValueAndHighlightedText(this.GetNextValueFromSearchTextInternal(this.GetStartIndexToSeachText()), false);
        }

        private void NavigateToPrevSearchedItem()
        {
            this.UpdateValueAndHighlightedText(this.GetPrevValueFromSearchTextInternal(this.GetStartIndexToSeachText()), false);
        }

        protected internal override bool NeedsEnterKey(ModifierKeys modifiers) => 
            (!this.IsInTokenMode || (this.Editor.EditMode != EditMode.InplaceActive)) ? base.NeedsEnterKey(modifiers) : (ModifierKeysHelper.IsCtrlPressed(modifiers) && base.EditBox.NeedsEnterKey());

        public override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            (!this.IsInTokenMode || !base.EditBox.NeedsKey(key, modifiers)) ? (((key == Key.Up) || (key == Key.Down)) ? ModifierKeysHelper.ContainsModifiers(modifiers) : base.NeedsKey(key, modifiers)) : true;

        public override void OnGotFocus()
        {
            base.OnGotFocus();
            if (this.Editor.IsTokenMode)
            {
                this.UpdateTokenEditorSnapshot(true);
                this.UpdateDisplayFilterInTokenMode();
            }
        }

        public void OnIncrementalFilteringChanged()
        {
            this.UpdateIncrementalFilteringSnapshot(this.PropertyProvider.EditMode == EditMode.InplaceActive);
            this.UpdateDisplayFilter();
        }

        public override void OnInitialized()
        {
            this.Editor.UnsubscribeToItemsProviderChanged();
            this.Editor.SubscribeToItemsProviderChanged();
            this.SelectorUpdater.SyncWithICollectionView();
            base.OnInitialized();
            if (this.SelectorUpdater.ShouldSyncWithItems && !base.PropertyUpdater.HasSyncValue)
            {
                base.ValueContainer.SetEditValue(this.SelectorUpdater.GetEditValueFromItems(), UpdateEditorSource.ValueChanging);
            }
        }

        public override void OnLoaded()
        {
            if (!this.PropertyProvider.CreatedFromSettings)
            {
                ((IItemsProviderOwner) this.Settings).IsLoaded = true;
                if (base.ItemsProvider.IsServerMode || base.ItemsProvider.NeedsRefresh)
                {
                    base.ItemsProvider.DoRefresh();
                }
            }
            base.OnLoaded();
        }

        public override void OnLostFocus()
        {
            this.FlushAutoSearchText();
            if (this.Editor.IsTokenMode)
            {
                this.UpdateTokenEditorSnapshot(false);
            }
            base.OnLostFocus();
            this.SetHighlightedText(string.Empty);
            base.ValueContainer.ResetUpdateSource();
        }

        public override void OnPreviewLostFocus(DependencyObject oldFocus, DependencyObject newFocus)
        {
            bool? nullable = this.Editor.IsEditorLostFocus(oldFocus, newFocus);
            base.EditBox.OnEditorPreviewLostFocus(nullable.GetValueOrDefault(true));
        }

        public override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            if (!e.Handled && !TextEditStrategyTextInputHelper.ShouldIgnoreTextInput(e.Text))
            {
                if (this.CanProcessReadOnlyAutoSearchText)
                {
                    string text = e.Text;
                    if (!string.IsNullOrEmpty(text))
                    {
                        this.ProcessTextSearch(text);
                        e.Handled = true;
                        return;
                    }
                }
                this.VisualClient.PreviewTextInput(e);
            }
        }

        internal void OnTokenEditorTokenClosed()
        {
            if ((this.Editor.EditMode != EditMode.InplaceInactive) || this.Settings.IsLoaded)
            {
                this.UpdateDisplayFilterInTokenMode();
                ChangeTextItem item = new ChangeTextItem();
                item.Text = string.Empty;
                this.UpdateAutoSearchText(item, false);
                this.UpdateDisplayText();
            }
        }

        public void OnTokenModeChanged()
        {
        }

        protected internal override void OnUnloaded()
        {
            base.OnUnloaded();
            if (!this.PropertyProvider.CreatedFromSettings)
            {
                ((IItemsProviderOwner) this.Settings).IsLoaded = false;
                if (base.ItemsProvider.IsServerMode)
                {
                    base.ItemsProvider.DoRefresh();
                }
            }
        }

        public virtual void PopupClosed()
        {
            this.VisualClient.PopupClosed();
        }

        public virtual void PopupDestroyed()
        {
            this.DestroyVisibleListSource();
            this.VisualClient.PopupDestroyed();
            this.AcceptPopupValueAction.PerformForce();
        }

        public override void PreviewMouseDown(MouseButtonEventArgs e)
        {
            base.PreviewMouseDown(e);
        }

        public override void PreviewMouseUp(MouseButtonEventArgs e)
        {
            base.PreviewMouseUp(e);
            this.FlushAutoSearchText();
        }

        public virtual void ProcessAutoCompleteNavKey(KeyEventArgs e)
        {
            if (!e.Handled)
            {
                if (this.CanProcessReadOnlyAutoSearchText)
                {
                    this.ProcessReadOnlyAutoSearchText(e);
                }
                else if (this.CanProcessAutoSearchText && base.AllowEditing)
                {
                    this.ProcessEditableAutoSearchText(e);
                }
            }
        }

        protected virtual void ProcessChangeText(ChangeTextItem item, bool showPopup = true)
        {
            string text = item.Text;
            bool updateAutoCompleteSelection = item.UpdateAutoCompleteSelection;
            this.UpdateAutoSearchBeforeValidate(item);
            int index = this.FindItemIndexByText(text, this.Editor.AutoComplete);
            object editValue = this.CreateEditableItem(index, item);
            base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
            this.UpdateAutoSearchAfterValidate(item);
            this.UpdateDisplayText();
            this.UpdateAutoSearchSelection(updateAutoCompleteSelection);
            if (showPopup)
            {
                this.ShowImmediatePopup();
            }
        }

        protected virtual void ProcessChangeText(string editText, bool updateAutoSearchSelection)
        {
            ChangeTextItem item = new ChangeTextItem();
            item.Text = editText;
            item.UpdateAutoCompleteSelection = updateAutoSearchSelection;
            this.ProcessChangeText(item, true);
        }

        private void ProcessEditableAutoSearchText(KeyEventArgs e)
        {
            if ((e.Key == Key.Escape) && (!this.AllTextSelected && (this.Editor.SelectionLength > 0)))
            {
                this.AutoSearchText = string.Empty;
                this.Editor.SelectAll();
                e.Handled = true;
            }
            else
            {
                if (!this.IsInAutoCompleteSelection)
                {
                    if (e.Key == Key.Delete)
                    {
                        string str4 = base.EditBox.Text.Remove(base.EditBox.CaretIndex, Math.Max(1, base.EditBox.SelectionLength));
                        ChangeTextItem item = new ChangeTextItem();
                        item.Text = str4;
                        item.AutoCompleteTextDeleted = true;
                        item.UpdateAutoCompleteSelection = false;
                        this.ProcessChangeText(item, true);
                        this.FlushAutoSearchText();
                        e.Handled = true;
                    }
                }
                else
                {
                    if (ModifierKeysHelper.NoModifiers(ModifierKeysHelper.GetKeyboardModifiers(e)) && ((e.Key == Key.Left) || (e.Key == Key.Right)))
                    {
                        int selectionStart = this.Editor.SelectionStart;
                        int selectionLength = this.Editor.SelectionLength;
                        if (e.Key == Key.Left)
                        {
                            if (selectionStart > 0)
                            {
                                selectionStart--;
                                selectionLength++;
                            }
                        }
                        else if (selectionStart < base.EditBox.Text.Length)
                        {
                            selectionStart++;
                            selectionLength = Math.Max(0, selectionLength - 1);
                        }
                        bool flag2 = base.ItemsProvider.FindItemIndexByText(base.EditBox.Text, this.Editor.IsCaseSensitiveSearch, this.Editor.AutoComplete, this.CurrentDataViewHandle, -1, true, false) != -1;
                        if ((selectionLength > 0) && !flag2)
                        {
                            selectionLength = 0;
                        }
                        this.Editor.SelectionStart = selectionStart;
                        this.Editor.SelectionLength = selectionLength;
                        if (flag2 && ((selectionStart > 0) || (selectionLength > 0)))
                        {
                            this.AutoSearchText = base.EditBox.Text.Substring(0, Math.Min(selectionStart, base.EditBox.Text.Length));
                        }
                        e.Handled = true;
                    }
                    if ((e.Key == Key.A) && (ModifierKeysHelper.IsOnlyCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && !ModifierKeysHelper.IsAltPressed(ModifierKeysHelper.GetKeyboardModifiers(e))))
                    {
                        this.SelectAll();
                        e.Handled = true;
                    }
                    if ((e.Key == Key.Back) && (this.Editor.SelectionStart > 0))
                    {
                        string str;
                        if (this.Editor.ClearSelectionOnBackspace)
                        {
                            string str2 = !string.IsNullOrEmpty(this.AutoSearchText) ? this.AutoSearchText : base.EditBox.Text;
                            str = (this.Editor.SelectionLength > 0) ? str2.Substring(0, str2.Length) : str2.Substring(0, Math.Max(0, str2.Length - 1));
                            ChangeTextItem item = new ChangeTextItem();
                            item.Text = str;
                            item.AutoCompleteTextDeleted = true;
                            item.UpdateAutoCompleteSelection = this.CalcUpdateAutoCompleteSelection();
                            this.ProcessChangeText(item, true);
                        }
                        else
                        {
                            str = base.EditBox.Text.Substring(0, Math.Max(0, this.Editor.SelectionStart - 1));
                            this.AutoSearchText = str;
                            ChangeTextItem item = new ChangeTextItem();
                            item.Text = str;
                            item.UpdateAutoCompleteSelection = true;
                            this.ProcessChangeText(item, true);
                            this.VisualClient.SyncProperties(false);
                        }
                        e.Handled = true;
                    }
                    if (e.Key == Key.Delete)
                    {
                        string str3 = base.EditBox.Text.Substring(0, Math.Min(base.EditBox.SelectionStart, base.EditBox.Text.Length));
                        ChangeTextItem item = new ChangeTextItem();
                        item.Text = str3;
                        item.AutoCompleteTextDeleted = true;
                        item.UpdateAutoCompleteSelection = true;
                        this.ProcessChangeText(item, true);
                        e.Handled = true;
                    }
                    if (((e.Key == Key.Home) && (base.EditBox.SelectedText != base.EditBox.Text)) && (this.FindItemIndexByText(base.EditBox.Text, this.Editor.AutoComplete) != -1))
                    {
                        this.SelectAll();
                        e.Handled = true;
                    }
                    if ((e.Key == Key.End) && (!string.IsNullOrEmpty(base.EditBox.Text) && (base.EditBox.SelectionStart != base.EditBox.Text.Length)))
                    {
                        this.ProcessChangeText(base.EditBox.Text, this.Editor.AutoComplete);
                        e.Handled = true;
                    }
                }
                if ((e.Key == Key.End) && (ModifierKeysHelper.GetKeyboardModifiers(e) != ModifierKeys.Shift))
                {
                    string text = base.EditBox.Text;
                    this.ProcessChangeText(text, true);
                    e.Handled = true;
                }
            }
        }

        protected internal override void ProcessEditModeChanged(EditMode oldValue, EditMode newValue)
        {
            if (newValue == EditMode.InplaceInactive)
            {
                this.UpdateIncrementalFilteringSnapshot(false);
            }
            else if (newValue == EditMode.InplaceActive)
            {
                this.UpdateIncrementalFilteringSnapshot(true);
            }
            base.ProcessEditModeChanged(oldValue, newValue);
        }

        protected override void ProcessKeyDownInternal(KeyEventArgs e)
        {
            this.VisualClient.ProcessKeyDown(e);
        }

        public override bool ProcessNewValue(string editText) => 
            !string.IsNullOrEmpty(editText) && (!this.ProcessNewValueHelper.IsInProcessNewValue && this.ProcessNewValueCore(editText));

        public virtual bool ProcessNewValueCore(string editText)
        {
            bool flag;
            using (this.ProcessNewValueHelper.Subscribe())
            {
                ProcessNewValueEventArgs args1 = new ProcessNewValueEventArgs(LookUpEditBase.ProcessNewValueEvent);
                args1.DisplayText = editText;
                ProcessNewValueEventArgs e = args1;
                this.Editor.RaiseEvent(e);
                if (!e.Handled)
                {
                    flag = false;
                }
                else
                {
                    if (e.PostponedValidation)
                    {
                        this.ProcessNewValueHelper.UpdateDataAsync();
                    }
                    else
                    {
                        base.ItemsProvider.DoRefresh();
                    }
                    flag = true;
                }
            }
            return flag;
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            if (!this.ProcessTokenEditorPreviewKeyDown(e))
            {
                this.VisualClient.BeforeProcessKeyDown();
                this.ProcessAutoCompleteNavKey(e);
                if (!this.Editor.LeavePopupGesture(e))
                {
                    base.ProcessPreviewKeyDownInternal(e);
                    this.VisualClient.ProcessPreviewKeyDown(e);
                }
            }
        }

        private void ProcessReadOnlyAutoSearchText(KeyEventArgs e)
        {
            if ((e.Key == Key.Escape) && this.HasHighlightedText)
            {
                this.CancelTextSearch();
                this.SetHighlightedText(string.Empty);
                e.Handled = true;
            }
            else if (e.Key == Key.Back)
            {
                if (this.TextSearchEngine.DeleteLastCharacter())
                {
                    this.UpdateValueAndHighlightedText(base.ItemsProvider.GetValueByIndex(this.TextSearchEngine.MatchedItemIndex, this.CurrentDataViewHandle), false);
                    e.Handled = true;
                }
                else if (this.HasHighlightedText)
                {
                    this.SetHighlightedText(string.Empty);
                    e.Handled = true;
                }
            }
            else if (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && (this.Editor.EditMode != EditMode.InplaceActive))
            {
                if (e.Key == Key.Down)
                {
                    this.NavigateToNextSearchedItem();
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    this.NavigateToPrevSearchedItem();
                    e.Handled = true;
                }
            }
        }

        private void ProcessTextSearch(string text)
        {
            object result = null;
            if (this.DoTextSearch(text, this.GetStartIndexToSeachText(), ref result))
            {
                this.UpdateValueAndHighlightedText(result, true);
            }
            else if (string.IsNullOrEmpty(this.TextSearchEngine.Prefix))
            {
                this.SetHighlightedText(this.TextSearchEngine.Prefix);
            }
        }

        private bool ProcessTokenEditorPreviewKeyDown(KeyEventArgs e) => 
            this.IsInTokenMode && (base.EditBox.NeedsNavigationKey(e.Key, ModifierKeysHelper.GetKeyboardModifiers(e)) && base.EditBox.ProccessKeyDown(e));

        public override bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource)
        {
            if (this.IsInProcessNewValueDialog)
            {
                base.ProvideEditValue(value, out provideValue, updateSource);
                return false;
            }
            object obj2 = value;
            LookUpEditableItem item = value as LookUpEditableItem;
            if ((item == null) && !this.IsInLookUpMode)
            {
                return base.ProvideEditValue(value, out provideValue, updateSource);
            }
            bool flag = true;
            if (item != null)
            {
                if (this.IsInTokenMode)
                {
                    provideValue = this.ProvideTokenEditValue(item);
                    return !this.IsInProcessNewValueDialog;
                }
                if (!this.IsInLookUpMode && !this.IsAsyncServerMode)
                {
                    return base.ProvideEditValue(item.EditValue, out provideValue, updateSource);
                }
                obj2 = this.ProvideSingleEditValue(item);
                flag = !this.IsInProcessNewValueDialog;
            }
            provideValue = obj2;
            return flag;
        }

        private object ProvideSingleEditValue(LookUpEditableItem item)
        {
            if (item.ForbidFindIncremental)
            {
                return item.EditValue;
            }
            Func<object, string> evaluator = <>c.<>9__271_0;
            if (<>c.<>9__271_0 == null)
            {
                Func<object, string> local1 = <>c.<>9__271_0;
                evaluator = <>c.<>9__271_0 = x => x.ToString();
            }
            int index = this.FindItemIndexByText(item.DisplayValue.With<object, string>(evaluator), this.Editor.AutoComplete);
            return ((index > -1) ? this.SelectorUpdater.GetEditValueFromSelectedIndex(index) : item.EditValue);
        }

        private List<object> ProvideTokenEditValue(LookUpEditableItem item)
        {
            List<object> list = new List<object>();
            IList<object> editValue = item.EditValue as IList<object>;
            if (editValue != null)
            {
                foreach (object obj2 in editValue)
                {
                    object obj3 = (obj2 is LookUpEditableItem) ? ((LookUpEditableItem) obj2).EditValue : obj2;
                    int index = base.ItemsProvider.IndexOfValue(obj3, this.CurrentDataViewHandle);
                    object obj4 = null;
                    obj4 = (index <= -1) ? obj3 : this.SelectorUpdater.GetEditValueFromSelectedIndex(index);
                    if ((obj4 != null) && !string.IsNullOrEmpty(obj4.ToString()))
                    {
                        list.Add(obj4);
                    }
                }
            }
            return ((list.Count > 0) ? list : null);
        }

        public void RaisePopupContentSelectionChanged(IList removedItems, IList addedItems)
        {
            SelectionChangedEventArgs e = new SelectionChangedEventArgs(LookUpEditBase.PopupContentSelectionChangedEvent, removedItems, addedItems);
            if (this.StyleSettings.ProcessContentSelectionChanged(this.VisualClient.InnerEditor, e))
            {
                this.Editor.RaiseEvent(e);
            }
        }

        public override void RaiseValueChangedEvents(object oldValue, object newValue)
        {
            base.RaiseValueChangedEvents(oldValue, newValue);
            if (!base.ShouldLockRaiseEvents)
            {
                this.SelectorUpdater.RaiseSelectedIndexChangedEvent(oldValue, newValue);
            }
        }

        private void RegisterTokenSnapshot()
        {
            if (this.TokenCurrentDataViewHandle != null)
            {
                base.ItemsProvider.ReleaseSnapshot(this.TokenCurrentDataViewHandle);
            }
            if (this.TokenFilterDataViewHandle != null)
            {
                base.ItemsProvider.ReleaseSnapshot(this.TokenFilterDataViewHandle);
            }
            this.PropertyProvider.TokenFilterDataViewHandle = this.PropertyProvider.GenerateHandle();
            base.ItemsProvider.RegisterSnapshot(this.TokenFilterDataViewHandle);
            this.PropertyProvider.TokenCurrentDataViewHandle = this.PropertyProvider.GenerateHandle();
            base.ItemsProvider.RegisterSnapshot(this.TokenCurrentDataViewHandle);
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__107_0;
            if (<>c.<>9__107_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__107_0;
                getBaseValueHandler = <>c.<>9__107_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(BaseEdit.EditValueProperty, getBaseValueHandler, baseValue => this.SelectorUpdater.GetEditValueFromBaseValue(baseValue));
            base.PropertyUpdater.Register(TextEditBase.TextProperty, new PropertyCoercionHandler(this.GetEditValueFromText), new PropertyCoercionHandler(this.GetTextFromEditValue));
            base.PropertyUpdater.Register(LookUpEditBase.SelectedIndexProperty, baseValue => this.SelectorUpdater.GetEditValueFromSelectedIndex(baseValue), (PropertyCoercionHandler) (baseValue => this.SelectorUpdater.GetIndexFromEditValue(baseValue)));
            base.PropertyUpdater.Register(LookUpEditBase.SelectedItemProperty, baseValue => this.SelectorUpdater.GetEditValueFromSelectedItem(baseValue), baseValue => this.SelectorUpdater.GetSelectedItemFromEditValue(baseValue));
            base.PropertyUpdater.Register(LookUpEditBase.SelectedItemsProperty, baseValue => this.SelectorUpdater.GetEditValueFromSelectedItems(baseValue), baseValue => this.SelectorUpdater.GetSelectedItemsFromEditValue(baseValue), baseValue => this.SelectorUpdater.UpdateSelectedItems(baseValue));
        }

        private void RemoveCurrentEditableValueFromFilter(List<object> values)
        {
            int currentEditableTokenIndex = this.GetCurrentEditableTokenIndex();
            if ((currentEditableTokenIndex != -1) && (currentEditableTokenIndex < values.Count))
            {
                values.RemoveAt(currentEditableTokenIndex);
            }
        }

        protected internal override void RemoveILogicalOwnerChild(object child)
        {
            base.RemoveILogicalOwnerChild(child);
            this.ProcessNewValueHelper.ClearFloatingContainer(child);
        }

        private void ReraiseKeyDown(KeyEventArgs e)
        {
            Func<KeyEventArgs, KeyEventArgs> cloneFunc = <>c.<>9__242_0;
            if (<>c.<>9__242_0 == null)
            {
                Func<KeyEventArgs, KeyEventArgs> local1 = <>c.<>9__242_0;
                cloneFunc = <>c.<>9__242_0 = x => new KeyEventArgs(x.KeyboardDevice, x.InputSource, x.Timestamp, x.Key);
            }
            ReraiseEventHelper.ReraiseEvent<KeyEventArgs>(e, this.Editor.EditCore, UIElement.PreviewKeyDownEvent, UIElement.KeyDownEvent, cloneFunc);
        }

        protected internal override void ResetOnValueChanging()
        {
            base.ResetOnValueChanging();
            this.FlushAutoSearchText();
        }

        protected internal override bool? RestoreDisplayText()
        {
            base.EditBox.OnRestoreDisplayText();
            return base.RestoreDisplayText();
        }

        public override void Select(int start, int length)
        {
            base.Select(start, length);
            this.SetAutoCompleteAutoSearchText(start);
        }

        public override void SelectAll()
        {
            base.SelectAll();
            this.AutoSearchText = string.Empty;
        }

        public virtual void SelectAllItems()
        {
            if (!this.IsSingleSelection)
            {
                List<object> selectedItems = new List<object>();
                selectedItems.AddRange(base.ItemsProvider.VisibleListSource.Cast<object>());
                base.ValueContainer.SetEditValue(this.SelectorUpdater.GetEditValueFromSelectedItems(selectedItems), UpdateEditorSource.ValueChanging);
                this.UpdateDisplayText();
            }
        }

        public virtual void SelectedIndexChanged(int oldValue, int newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(LookUpEditBase.SelectedIndexProperty, oldValue, newValue);
            }
        }

        public virtual void SelectedItemChanged(object oldValue, object newValue)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(LookUpEditBase.SelectedItemProperty, oldValue, newValue);
            }
        }

        public virtual void SelectedItemsChanged(IList oldSelectedItems, IList selectedItems)
        {
            this.CoerceValue(LookUpEditBase.SelectedItemsProperty, selectedItems);
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(LookUpEditBase.SelectedItemsProperty, oldSelectedItems, selectedItems);
            }
        }

        public virtual void SelectedItemsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.CoerceValue(LookUpEditBase.SelectedItemsProperty, this.Editor.SelectedItems);
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(LookUpEditBase.SelectedItemsProperty, this.SelectorUpdater.GetPreviousSelectedItems(e), this.Editor.SelectedItems);
            }
        }

        public virtual void SelectionModeChanged(SelectionMode? value)
        {
            this.VisualClient.SyncProperties(false);
        }

        public virtual void SelectItemWithNullValueChanged(bool newValue)
        {
            this.Settings.SelectItemWithNullValue = newValue;
        }

        private void ServerModeSyncWithEditor()
        {
            this.AsyncServerModeUpdateLocker.DoLockedAction(new Action(this.SyncWithEditor));
        }

        private void ServerModeSyncWithValue(object value)
        {
            ChangeTextItem item = null;
            this.AsyncServerModeUpdateLocker.DoLockedAction<ChangeTextItem>(delegate {
                ChangeTextItem item;
                item = item = this.GetEditableObject() as ChangeTextItem;
                return item;
            });
            int index = base.ItemsProvider.IndexOfValue(value, this.CurrentDataViewHandle);
            LookUpEditableItem editValue = (index > -1) ? this.CreateEditableItemForExistentValue(index, value, item) : this.CreateEditableItemForNonexistentValue(index, value, item);
            editValue.ForbidFindIncremental = true;
            base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.ValueChanging);
            this.UpdateDisplayText();
        }

        protected virtual void SetAutoCompleteAutoSearchText(int start)
        {
            if (this.Editor.AutoComplete)
            {
                if (!this.IsInAutoCompleteSelection)
                {
                    this.SetAutoSearchText(string.Empty);
                }
                else
                {
                    string text = ((ChangeTextItem) this.GetEditableObject()).Text;
                    this.AutoSearchText = text.Substring(0, Math.Min((start > 0) ? start : 0, text.Length));
                }
            }
        }

        protected virtual void SetAutoSearchText(string text)
        {
            this.AutoSearchText = text;
        }

        private void SetEditBoxEditValue()
        {
            this.PropertyProvider.SetDisplayText(this.CoerceDisplayText(null));
            base.EditBox.EditValue = this.GetEditValueForTokenEditor(this.PropertyProvider.DisplayText);
            if (!this.PropertyProvider.SuppressFeatures)
            {
                this.Editor.DisplayText = this.PropertyProvider.DisplayText;
            }
        }

        internal void SetEditValueOnTokenEditorValueChanged()
        {
            base.ValueContainer.SetEditValue(this.CreateEditableItemInTokenMode(-1, new ChangeTextItem()), UpdateEditorSource.ValueChanging);
        }

        private void SetHighlightedText(string text)
        {
            base.ImmediateActionsManager.EnqueueAction(() => this.Editor.SetValue(TextBlockService.HighlightedTextProperty, text));
        }

        public override void SetNullValue(object parameter)
        {
            base.SetNullValue(parameter);
            this.SetAutoSearchText(string.Empty);
        }

        private bool ShouldAssignNullValueWithText(int index, string text) => 
            (index == -1) && (string.IsNullOrEmpty(text) && this.Editor.AssignNullValueOnClearingEditText);

        private bool ShouldProcessNewValueOnEnter()
        {
            string text = base.EditBox.Text;
            return (this.IsInLookUpMode && ((base.ItemsProvider.FindItemIndexByText(base.EditBox.Text, this.Editor.IsCaseSensitiveSearch, false, this.CurrentDataViewHandle, -1, true, false) == -1) && (this.IsSingleSelection || (!string.IsNullOrEmpty(text) && !text.Contains(this.Editor.SeparatorString)))));
        }

        protected override bool ShouldRestoreCursorPosition() => 
            base.ShouldRestoreCursorPosition() || this.AsyncServerModeUpdateLocker.IsLocked;

        protected internal override bool ShouldShowEmptyTextInternal(object editValue) => 
            base.ShouldShowEmptyTextInternal(editValue) ? (!this.Editor.SelectItemWithNullValue || (!this.IsNativeNullValue(editValue) || (base.ItemsProvider.IndexOfValue(editValue, this.CurrentDataViewHandle) < 0))) : false;

        protected internal override bool ShouldShowNullTextInternal(object editValue) => 
            base.ShouldShowNullTextInternal(editValue) ? (!this.Editor.SelectItemWithNullValue || (!this.IsNullValue(editValue) || (base.ItemsProvider.IndexOfValue(editValue, this.CurrentDataViewHandle) < 0))) : false;

        public virtual void ShowCustomItemsChanged(bool? value)
        {
            this.VisualClient.SyncProperties(true);
        }

        public virtual void ShowImmediatePopup()
        {
            if (this.IsImmediatePopup && (!this.Editor.IsPopupOpen && !this.AsyncServerModeUpdateLocker.IsLocked))
            {
                this.Editor.ShowPopup();
            }
            else if (this.Editor.IsPopupOpen)
            {
                this.VisualClient.SyncValues(this.PropertyProvider.SyncValuesWithPopup);
            }
        }

        protected override bool SpinDown()
        {
            bool flag = this.ChangeIndexAndHandle(1);
            this.UpdateDisplayText();
            return flag;
        }

        protected override bool SpinUp()
        {
            bool flag = this.ChangeIndexAndHandle(-1);
            this.UpdateDisplayText();
            return flag;
        }

        protected override void SyncWithEditorInternal()
        {
            this.ProcessChangeText((ChangeTextItem) this.GetEditableObject(), true);
            if (this.CanProcessAutoSearchText && !this.IsInAutoCompleteSelection)
            {
                this.FlushAutoSearchText();
            }
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.VisualClient.SyncValues(false);
            base.EditBox.SyncWithValue(base.ValueContainer.UpdateSource);
            this.UpdateSelectedItemValue();
            this.SelectorUpdater.SyncICollectionView(base.ValueContainer.EditValue);
            this.UpdateDisplayFilterInTokenMode();
        }

        protected virtual void SyncWithValueOnDataChanged()
        {
            if (!base.ValueContainer.HasValueCandidate)
            {
                this.FilterChangedLocker.DoIfNotLocked(new Action(this.SyncWithValue));
            }
        }

        protected virtual int TextSearchCallback(string prefix, int startIndex, bool ignoreStartIndex) => 
            base.ItemsProvider.FindItemIndexByText(prefix, this.Editor.IsCaseSensitiveSearch, this.Editor.IncrementalSearch, this.CurrentDataViewHandle, startIndex, true, ignoreStartIndex);

        private void UnregisterTokenSnapshot()
        {
            if (this.TokenFilterDataViewHandle != null)
            {
                base.ItemsProvider.ReleaseSnapshot(this.TokenFilterDataViewHandle);
                this.PropertyProvider.TokenFilterDataViewHandle = null;
            }
            if (this.TokenCurrentDataViewHandle != null)
            {
                base.ItemsProvider.ReleaseSnapshot(this.TokenCurrentDataViewHandle);
                this.PropertyProvider.TokenCurrentDataViewHandle = null;
            }
        }

        public virtual void UnselectAllItems()
        {
            object editValue = this.IsSingleSelection ? null : new List<object>();
            base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.ValueChanging);
            this.UpdateDisplayText();
        }

        protected virtual void UpdateAutoSearchAfterValidate(ChangeTextItem item)
        {
            if (this.ShouldUpdateAutoSearchText && !this.ShouldUpdateAutoSearchBeforeValidating)
            {
                this.UpdateAutoSearchText(item, false);
            }
        }

        protected virtual void UpdateAutoSearchBeforeValidate(ChangeTextItem item)
        {
            if (!this.ShouldUpdateAutoSearchText)
            {
                this.AutoSearchText = string.Empty;
            }
            else if (this.ShouldUpdateAutoSearchBeforeValidating)
            {
                this.UpdateAutoSearchText(item, this.IsInLookUpMode);
            }
        }

        private void UpdateAutoSearchSelection(bool updateSelection)
        {
            if ((this.Editor.AutoComplete && !this.Editor.IsAsyncOperationInProgress) && updateSelection)
            {
                object currentEditableValue = this.GetCurrentEditableValue();
                string str = Convert.ToString(this.GetDisplayValue(currentEditableValue));
                base.EditBox.Select(this.AutoSearchText.Length, Math.Max(0, str.Length - this.AutoSearchText.Length));
            }
        }

        public virtual void UpdateAutoSearchText(ChangeTextItem item, bool reverse)
        {
            if (this.ShouldUpdateAutoSearchText)
            {
                string text = item.Text;
                string startWithPart = reverse ? this.AutoSearchText : text;
                int index = this.FindItemIndexByText(text, this.Editor.AutoComplete);
                if (index > -1)
                {
                    startWithPart = TextBlockService.GetStartWithPart(Convert.ToString(base.ItemsProvider.GetDisplayValueByIndex(index, this.CurrentDataViewHandle)), text);
                }
                else if (item.AsyncLoading)
                {
                    return;
                }
                this.AutoSearchText = startWithPart;
            }
        }

        public override void UpdateDataContext(DependencyObject target)
        {
            base.UpdateDataContext(target);
            if ((this.PropertyProvider.EditMode == EditMode.InplaceInactive) && (this.PropertyProvider.ApplyItemTemplateToSelectedItem && (this.Editor.EditCore != null)))
            {
                this.Editor.EditCore.DataContext = this.Editor.SelectedItemValue;
            }
        }

        public virtual void UpdateDisplayFilter()
        {
            this.FilterChangedLocker.DoLockedAction(delegate {
                if (!this.IsInTokenMode)
                {
                    base.ItemsProvider.SetDisplayFilterCriteria(this.CreateDisplayFilter(), this.CurrentDataViewHandle);
                }
                else
                {
                    this.UpdateDisplayFilterInTokenMode();
                }
            });
        }

        private void UpdateDisplayFilterInTokenMode()
        {
            if (this.IsInTokenMode && this.IncrementalFiltering)
            {
                object dataViewHandleInTokenMode = this.GetDataViewHandleInTokenMode();
                if (dataViewHandleInTokenMode != null)
                {
                    object provideValue = null;
                    IEnumerable editValue = provideValue as IEnumerable;
                    if (this.ProvideEditValue(base.EditValue, out provideValue, UpdateEditorSource.TextInput))
                    {
                        CriteriaOperator criteria = this.CreateDisplayFilterInTokenMode(editValue);
                        CriteriaOperator operator2 = this.FilterOutSelectedTokens ? criteria : this.CreateDisplayFilter();
                        base.ItemsProvider.SetFilterCriteria(criteria, dataViewHandleInTokenMode);
                        base.ItemsProvider.SetFilterCriteria(operator2, this.TokenCurrentDataViewHandle);
                    }
                    else
                    {
                        base.ItemsProvider.SetFilterCriteria(null, dataViewHandleInTokenMode);
                        base.ItemsProvider.SetFilterCriteria(null, this.TokenCurrentDataViewHandle);
                    }
                }
            }
        }

        protected override void UpdateDisplayTextAndRestoreCursorPosition()
        {
            CursorPositionSnapshot snapshot = new CursorPositionSnapshot(base.EditBox.SelectionStart, base.EditBox.SelectionLength, this.Editor.EditBox.Text, this.Editor.AutoComplete);
            this.UpdateDisplayTextInternal();
            snapshot.ApplyToEdit(this.Editor);
        }

        protected override void UpdateDisplayTextInternal()
        {
            if (!this.IsInImeInput || !this.IsInLookUpMode)
            {
                if (this.IsInTokenMode)
                {
                    this.SetEditBoxEditValue();
                }
                else
                {
                    base.UpdateDisplayTextInternal();
                }
            }
        }

        protected internal virtual void UpdateIncrementalFilteringSnapshot(bool subscribe)
        {
            if (this.ShouldUseAdditionalIncrementalFilteringSnapshot)
            {
                if (subscribe)
                {
                    base.ItemsProvider.RegisterSnapshot(this.PropertyProvider.IncrementalFilteringHandle);
                }
                else
                {
                    base.ItemsProvider.ReleaseSnapshot(this.PropertyProvider.IncrementalFilteringHandle);
                }
            }
        }

        protected virtual void UpdateSelectedItemValue()
        {
            object selectedItem = this.Editor.SelectedItem;
            ContentControl control = selectedItem as ContentControl;
            if (control != null)
            {
                selectedItem = control.Content;
                if ((this.Editor != null) && ((this.Editor.ItemTemplate == null) && (this.Editor.ItemTemplateSelector == null)))
                {
                    UIElement element = selectedItem as UIElement;
                    if (element != null)
                    {
                        this.Editor.SelectedValueRenderer.Element = element;
                        this.Editor.SelectedValueRenderer.Render();
                        return;
                    }
                    this.Editor.SelectedValueRenderer.Element = null;
                }
            }
            this.Editor.SelectedItemValue = selectedItem;
        }

        protected internal void UpdateTokenEditorSnapshot(bool subcribe)
        {
            if (subcribe)
            {
                this.RegisterTokenSnapshot();
            }
            else
            {
                this.UnregisterTokenSnapshot();
            }
        }

        private void UpdateValueAndHighlightedText(object value, bool immediatePopup = false)
        {
            if (value != null)
            {
                base.ValueContainer.SetEditValue(value, UpdateEditorSource.TextInput);
                this.UpdateDisplayText();
                if (immediatePopup)
                {
                    this.ShowImmediatePopup();
                }
                this.SetHighlightedText(this.TextSearchEngine.SeachText);
            }
        }

        protected internal override void ValidateOnEnterKeyPressed(KeyEventArgs e)
        {
            if ((((e == null) || !e.Handled) && !this.VisualClient.PostPopupValue) && !this.Editor.IsPopupOpen)
            {
                if (base.AllowEditing && this.ShouldProcessNewValueOnEnter())
                {
                    this.ProcessValidateOnEnterLocker.DoLockedAction(delegate {
                        ChangeTextItem item = new ChangeTextItem();
                        item.Text = base.EditBox.Text;
                        this.ProcessChangeText(item, false);
                    });
                }
                string text = base.EditBox.Text;
                base.ValidateOnEnterKeyPressed(e);
                if (text != base.EditBox.Text)
                {
                    ChangeTextItem item = new ChangeTextItem();
                    item.Text = base.EditBox.Text;
                    this.UpdateAutoSearchAfterValidate(item);
                }
                this.UpdateAutoSearchSelection(true);
            }
        }

        public virtual void ValueMemberChanged(string valueMember)
        {
            object item = base.ItemsProvider.GetItem(base.ValueContainer.EditValue, this.CurrentDataViewHandle);
            this.Settings.ValueMember = valueMember;
            if (item != null)
            {
                base.ValueContainer.SetEditValue(base.ItemsProvider.GetValueFromItem(item, this.CurrentDataViewHandle), UpdateEditorSource.ValueChanging);
            }
            this.UpdateDisplayText();
        }

        internal DevExpress.Xpf.Editors.Native.TextSearchEngine TextSearchEngine { get; set; }

        protected internal override bool AllowSpin =>
            base.AllowSpin && !this.Editor.IsPopupOpen;

        private LookUpEditBase Editor =>
            (LookUpEditBase) base.Editor;

        private LookUpEditSettingsBase Settings =>
            this.Editor.Settings;

        internal BaseLookUpStyleSettings StyleSettings =>
            (BaseLookUpStyleSettings) base.StyleSettings;

        protected internal SelectorPropertiesCoercionHelper SelectorUpdater =>
            this.selectorUpdater;

        protected bool IsSingleSelection =>
            LookUpEditHelper.GetIsSingleSelection(this.Editor);

        private bool IsSingleEditValue =>
            this.IsSingleSelection && !this.IsInTokenMode;

        private bool ShouldUpdateAutoSearchText =>
            this.Editor.AutoComplete || this.IncrementalFiltering;

        protected SelectorVisualClientOwner VisualClient =>
            this.Editor.VisualClient;

        public bool IsInLookUpMode =>
            base.ItemsProvider.IsInLookUpMode;

        private bool ShouldUpdateAutoSearchBeforeValidating =>
            this.Editor.ValidateOnTextInput;

        private UpdateDataSourceOnAsyncProcessNewValueEventHelper ProcessNewValueHelper { get; set; }

        public virtual bool IsEditingCompleted =>
            !(base.ValueContainer.EditValue is LookUpEditableItem);

        public override bool IsInProcessNewValueDialog =>
            this.ProcessNewValueHelper.LockerByProcessNewValueWindow;

        private LookUpEditBasePropertyProvider PropertyProvider =>
            base.PropertyProvider as LookUpEditBasePropertyProvider;

        protected internal bool IsInTokenMode =>
            this.Editor.IsTokenMode && (this.Editor.EditCore is TokenEditor);

        private bool ShouldUseAdditionalIncrementalFilteringSnapshot =>
            this.PropertyProvider.IncrementalFiltering && this.PropertyProvider.CreatedFromSettings;

        public virtual object CurrentDataViewHandle =>
            ((this.PropertyProvider.EditMode != EditMode.InplaceActive) || !this.ShouldUseAdditionalIncrementalFilteringSnapshot) ? base.ItemsProvider.CurrentDataViewHandle : this.PropertyProvider.IncrementalFilteringHandle;

        private object TokenFilterDataViewHandle =>
            this.PropertyProvider.TokenFilterDataViewHandle;

        private object TokenCurrentDataViewHandle =>
            this.PropertyProvider.TokenCurrentDataViewHandle;

        private object VisibleListHandle =>
            this.IsInTokenMode ? (this.FilterOutSelectedTokens ? this.GetDataViewHandleInTokenMode() : this.TokenCurrentDataViewHandle) : this.CurrentDataViewHandle;

        private bool FilterOutSelectedTokens =>
            this.PropertyProvider.FilterOutSelectedTokens;

        public virtual bool ShouldRaiseProcessValueConversion =>
            this.IsInLookUpMode && !this.IsServerMode;

        protected internal bool IsAsyncServerMode =>
            base.ItemsProvider.IsAsyncServerMode;

        protected internal bool IsSyncServerMode =>
            base.ItemsProvider.IsSyncServerMode;

        protected bool IsServerMode =>
            this.PropertyProvider.IsServerMode;

        protected virtual bool IncrementByFindIncremental =>
            this.PropertyProvider.IncrementalFiltering && !string.IsNullOrEmpty(this.Editor.AutoSearchText);

        private bool HasHighlightedText =>
            !string.IsNullOrEmpty((string) this.Editor.GetValue(TextBlockService.HighlightedTextProperty));

        private EditorTextSearchHelper TextSearchHelper
        {
            get
            {
                this.textSearchHelper ??= new EditorTextSearchHelper(this);
                return this.textSearchHelper;
            }
        }

        private bool IsInImeInput =>
            !this.IsInTokenMode ? this.Editor.IsInImeInput : base.EditBox.GetIsInImeInput();

        protected Locker ProcessValidateOnEnterLocker { get; set; }

        private Locker AsyncServerModeUpdateLocker { get; set; }

        private PostponedAction AcceptPopupValueAction { get; set; }

        private Locker FilterChangedLocker { get; set; }

        protected virtual bool CanProcessAutoSearchText =>
            this.Editor.AutoComplete && (this.Editor.IsEditorActive && (!this.VisualClient.IsKeyboardFocusWithin && base.AllowEditing));

        protected virtual bool CanProcessReadOnlyAutoSearchText =>
            this.Editor.IncrementalSearch && (this.Editor.IsEditorActive && (!this.VisualClient.IsKeyboardFocusWithin && (base.AllowKeyHandling && (!this.Editor.IsReadOnly && (!this.PropertyProvider.IsTextEditable && (!this.IsInTokenMode && (this.PropertyProvider.SelectionMode == SelectionMode.Single)))))));

        protected string AutoSearchText
        {
            get => 
                this.Editor.AutoSearchText;
            set => 
                this.Editor.AutoSearchText = value;
        }

        protected bool IncrementalFiltering =>
            this.PropertyProvider.IncrementalFiltering;

        protected virtual bool IsImmediatePopup =>
            this.Editor.ImmediatePopup;

        protected bool AllTextSelected =>
            (this.Editor.SelectionLength == base.EditBox.Text.Length) && (this.Editor.SelectionLength > 0);

        protected internal bool IsInAutoCompleteSelection =>
            (base.EditBox.SelectionStart + base.EditBox.SelectionLength) == base.EditBox.Text.Length;

        DevExpress.Xpf.Editors.Native.TextSearchEngine ISelectorEditStrategy.TextSearchEngine =>
            this.TextSearchEngine;

        bool ISelectorEditStrategy.IsTokenMode =>
            this.Editor.IsTokenMode;

        object ISelectorEditStrategy.TokenDataViewHandle =>
            this.GetDataViewHandleInTokenMode();

        int ISelectorEditStrategy.EditableTokenIndex =>
            this.GetCurrentEditableTokenIndex();

        bool ISelectorEditStrategy.IsInProcessNewValue =>
            this.ProcessNewValueHelper.IsInProcessNewValue;

        string ISelectorEditStrategy.SearchText =>
            this.TextSearchEngine.Prefix;

        IItemsProvider2 ISelectorEditStrategy.ItemsProvider =>
            base.ItemsProvider;

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
            LookUpEditBase.SelectedIndexChangedEvent;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookUpEditStrategyBase.<>c <>9 = new LookUpEditStrategyBase.<>c();
            public static Func<object, string> <>9__98_0;
            public static Func<string> <>9__98_1;
            public static Func<object, string> <>9__101_0;
            public static Func<string> <>9__101_1;
            public static Func<object, string> <>9__102_0;
            public static PropertyCoercionHandler <>9__107_0;
            public static Func<LookUpEditableItem, ChangeTextItem> <>9__182_0;
            public static Func<LookUpEditableItem, ChangeTextItem> <>9__182_1;
            public static Func<IList, bool> <>9__183_0;
            public static Func<ChangeTextItem, bool> <>9__203_0;
            public static Func<bool> <>9__203_1;
            public static Func<ChangeTextItem, bool> <>9__203_2;
            public static Func<bool> <>9__203_3;
            public static Func<TokenEditorCustomItem, int> <>9__206_0;
            public static Func<int> <>9__206_1;
            public static Func<ChangeTextItem, string> <>9__207_0;
            public static Func<string> <>9__207_1;
            public static Func<ChangeTextItem, bool> <>9__207_2;
            public static Func<bool> <>9__207_3;
            public static Func<ChangeTextItem, bool> <>9__207_4;
            public static Func<bool> <>9__207_5;
            public static Func<LookUpEditableItem, bool> <>9__225_0;
            public static Func<LookUpEditableItem, bool> <>9__232_0;
            public static Func<LookUpEditableItem, bool> <>9__232_1;
            public static Func<KeyEventArgs, KeyEventArgs> <>9__242_0;
            public static Func<object, string> <>9__271_0;

            internal bool <CreateEditableItemForExistentValue>b__203_0(ChangeTextItem x) => 
                x.AutoCompleteTextDeleted;

            internal bool <CreateEditableItemForExistentValue>b__203_1() => 
                false;

            internal bool <CreateEditableItemForExistentValue>b__203_2(ChangeTextItem x) => 
                x.AcceptedFromPopup;

            internal bool <CreateEditableItemForExistentValue>b__203_3() => 
                false;

            internal string <CreateEditableItemForNonexistentValue>b__207_0(ChangeTextItem x) => 
                x.Text;

            internal string <CreateEditableItemForNonexistentValue>b__207_1() => 
                string.Empty;

            internal bool <CreateEditableItemForNonexistentValue>b__207_2(ChangeTextItem x) => 
                x.AutoCompleteTextDeleted;

            internal bool <CreateEditableItemForNonexistentValue>b__207_3() => 
                false;

            internal bool <CreateEditableItemForNonexistentValue>b__207_4(ChangeTextItem x) => 
                x.AcceptedFromPopup;

            internal bool <CreateEditableItemForNonexistentValue>b__207_5() => 
                false;

            internal ChangeTextItem <CreateEditValueForAcceptPopupValue>b__182_0(LookUpEditableItem x) => 
                x.ChangeTextItem;

            internal ChangeTextItem <CreateEditValueForAcceptPopupValue>b__182_1(LookUpEditableItem x) => 
                x.ChangeTextItem;

            internal bool <FilterSelectedItemsFromEmptyList>b__183_0(IList x) => 
                x.Count > 0;

            internal int <GetCurrentEditableTokenIndex>b__206_0(TokenEditorCustomItem x) => 
                x.EditableTokenIndex;

            internal int <GetCurrentEditableTokenIndex>b__206_1() => 
                -1;

            internal string <GetCustomItemFromValue>b__101_0(object x) => 
                x.ToString();

            internal string <GetCustomItemFromValue>b__101_1() => 
                string.Empty;

            internal string <GetDisplayTextForTokenEditor>b__102_0(object x) => 
                x.ToString();

            internal string <GetEditValueForTokenEditor>b__98_0(object x) => 
                x.ToString();

            internal string <GetEditValueForTokenEditor>b__98_1() => 
                string.Empty;

            internal bool <ItemsProviderOnBusyChanged>b__232_0(LookUpEditableItem x) => 
                x.AsyncLoading;

            internal bool <ItemsProviderOnBusyChanged>b__232_1(LookUpEditableItem x) => 
                x.AcceptedFromPopup;

            internal bool <ItemsProviderRefreshed>b__225_0(LookUpEditableItem x) => 
                x.AsyncLoading && !x.AcceptedFromPopup;

            internal string <ProvideSingleEditValue>b__271_0(object x) => 
                x.ToString();

            internal object <RegisterUpdateCallbacks>b__107_0(object baseValue) => 
                baseValue;

            internal KeyEventArgs <ReraiseKeyDown>b__242_0(KeyEventArgs x) => 
                new KeyEventArgs(x.KeyboardDevice, x.InputSource, x.Timestamp, x.Key);
        }
    }
}

