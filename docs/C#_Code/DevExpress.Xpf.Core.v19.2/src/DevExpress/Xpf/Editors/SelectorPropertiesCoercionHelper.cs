namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectorPropertiesCoercionHelper
    {
        public SelectorPropertiesCoercionHelper()
        {
            this.SyncWithICollectionViewLocker = new Locker();
        }

        public int CoerceSelectedIndex(int baseIndex) => 
            baseIndex;

        public object CoerceSelectedItem(object baseItem) => 
            baseItem;

        public IList CoerceSelectedItemsSource(IList value) => 
            value;

        private bool ContainsValue(object value) => 
            !this.IsInLookUpMode || (this.ItemsProvider.IndexOfValue(value, this.EditStrategy.CurrentDataViewHandle) > -1);

        public object FilterSelectedItem(object item) => 
            CustomItem.FilterCustomItem(item);

        public IEnumerable<object> FilterSelectedItems(IEnumerable<object> items) => 
            CustomItem.FilterCustomItems(items);

        public object GetCurrentSelectedItem(ValueContainerService valueContainer)
        {
            object obj2;
            if (!this.EditStrategy.ProvideEditValue(valueContainer.EditValue, out obj2, UpdateEditorSource.TextInput))
            {
                return null;
            }
            if (!this.EditStrategy.IsTokenMode)
            {
                return this.ItemsProvider.GetItem(obj2, this.EditStrategy.CurrentDataViewHandle);
            }
            int editableTokenIndex = this.EditStrategy.EditableTokenIndex;
            IList list = obj2 as IList;
            return ((list != null) ? (((editableTokenIndex >= list.Count) || (editableTokenIndex <= -1)) ? null : this.ItemsProvider.GetItem(list[editableTokenIndex], this.EditStrategy.TokenDataViewHandle)) : this.ItemsProvider.GetItem(obj2, this.EditStrategy.CurrentDataViewHandle));
        }

        public IEnumerable GetCurrentSelectedItems(ValueContainerService valueContainer)
        {
            object obj2;
            List<object> result = new List<object>();
            return (!this.EditStrategy.ProvideEditValue(valueContainer.EditValue, out obj2, UpdateEditorSource.TextInput) ? result : ((IEnumerable) (obj2 as IEnumerable).Return<IEnumerable, List<object>>(x => (from item in x.Cast<object>() select this.ItemsProvider.GetItem(item, this.EditStrategy.CurrentDataViewHandle)).ToList<object>(), () => result)));
        }

        public object GetEditValueFromBaseValue(object baseValue)
        {
            if (this.EditStrategy.IsSingleSelection && !this.EditStrategy.IsTokenMode)
            {
                return (this.ContainsValue(baseValue) ? baseValue : this.Editor.NullValue);
            }
            if (baseValue == null)
            {
                return null;
            }
            List<object> list = new List<object>();
            IList list2 = baseValue as IList;
            if ((list2 == null) && this.ContainsValue(baseValue))
            {
                list.Add(baseValue);
            }
            else if (list2 != null)
            {
                foreach (object obj2 in list2)
                {
                    if (this.ContainsValue(obj2))
                    {
                        list.Add(obj2);
                    }
                }
            }
            return list;
        }

        public object GetEditValueFromItems()
        {
            ListItemCollection source = this.Editor.Items;
            if (source.Count == 0)
            {
                return null;
            }
            Func<object, bool> predicate = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__35_0;
                predicate = <>c.<>9__35_0 = element => (element is ListBoxEditItem) && ((ListBoxEditItem) element).IsSelected;
            }
            List<object> selectedItems = source.Cast<object>().Where<object>(predicate).ToList<object>();
            return this.GetEditValueFromSelectedItems(selectedItems);
        }

        public object GetEditValueFromSelectedIndex(object index) => 
            this.ItemsProvider.GetValueByIndex((int) index, this.EditStrategy.CurrentDataViewHandle);

        public object GetEditValueFromSelectedItem(object selectedItem)
        {
            object item = this.FilterSelectedItem(selectedItem);
            return this.ItemsProvider.GetValueFromItem(item, this.EditStrategy.CurrentDataViewHandle);
        }

        public object GetEditValueFromSelectedItems(object selectedItems)
        {
            IList list = selectedItems as IList;
            if (this.EditStrategy.IsSingleSelection && !this.EditStrategy.IsTokenMode)
            {
                return (((list == null) || (list.Count < 1)) ? null : this.GetEditValueFromSelectedItem(list[0]));
            }
            List<object> source = new List<object>();
            if (list == null)
            {
                if ((selectedItems != null) && (this.ItemsProvider.IndexOfValue(selectedItems, this.EditStrategy.CurrentDataViewHandle) != -1))
                {
                    source.Add(selectedItems);
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if ((list[i] != null) && (this.ItemsProvider.GetIndexByItem(list[i], this.EditStrategy.CurrentDataViewHandle) > -1))
                    {
                        source.Add(this.ItemsProvider.GetValueFromItem(list[i], this.EditStrategy.CurrentDataViewHandle));
                    }
                }
            }
            source = source.Distinct<object>().ToList<object>();
            return (source.Any<object>() ? source : null);
        }

        public object GetEditValueFromSelectedItemsSource(object baseValue) => 
            this.GetEditValueFromSelectedItems(baseValue);

        public int GetIndexFromEditValue(object editValue)
        {
            IList list = editValue as IList;
            if ((list != null) && (list.Count > 0))
            {
                using (IEnumerator enumerator = list.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        object current = enumerator.Current;
                        int num = this.ItemsProvider.IndexOfValue(current, this.EditStrategy.CurrentDataViewHandle);
                        if (num > -1)
                        {
                            return num;
                        }
                    }
                }
            }
            return ((this.Editor.SelectItemWithNullValue || !this.EditStrategy.IsNullValue(editValue)) ? this.ItemsProvider.IndexOfValue(editValue, this.EditStrategy.CurrentDataViewHandle) : -1);
        }

        public IList GetPreviousSelectedItems(NotifyCollectionChangedEventArgs e)
        {
            object selectedItemsFromEditValue = this.GetSelectedItemsFromEditValue(this.EditStrategy.EditValue);
            if (selectedItemsFromEditValue is IList)
            {
                return (IList) selectedItemsFromEditValue;
            }
            List<object> list1 = new List<object>();
            list1.Add(selectedItemsFromEditValue);
            return list1;
        }

        public object GetPreviousSelectedItemsSource(ListChangedEventArgs e) => 
            null;

        public object GetSelectedItemFromEditValue(object editValue)
        {
            IList list = editValue as IList;
            if ((list != null) && (list.Count > 0))
            {
                using (IEnumerator enumerator = list.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        object current = enumerator.Current;
                        int controllerIndex = -1;
                        controllerIndex = (this.Editor.SelectItemWithNullValue || !this.EditStrategy.IsNullValue(editValue)) ? this.ItemsProvider.IndexOfValue(current, this.EditStrategy.CurrentDataViewHandle) : -1;
                        if (controllerIndex > -1)
                        {
                            return this.ItemsProvider.GetItemByControllerIndex(controllerIndex, this.EditStrategy.CurrentDataViewHandle);
                        }
                    }
                }
            }
            return ((this.Editor.SelectItemWithNullValue || !this.EditStrategy.IsNullValue(editValue)) ? this.ItemsProvider.GetItem(editValue, this.EditStrategy.CurrentDataViewHandle) : null);
        }

        public object GetSelectedItemsFromEditValue(object editValue)
        {
            IList list = editValue as IList;
            if ((this.EditStrategy.IsSingleSelection && !this.EditStrategy.IsTokenMode) || (list == null))
            {
                return this.GetSelectedItemFromEditValue(editValue);
            }
            IList list2 = new List<object>();
            foreach (object obj2 in list)
            {
                object item = this.ItemsProvider.GetItem(obj2, this.EditStrategy.CurrentDataViewHandle);
                if (item != null)
                {
                    list2.Add(item);
                }
            }
            return list2;
        }

        public object GetSelectedItemsSourceFromEditValue(object baseValue) => 
            this.GetSelectedItemsFromEditValue(baseValue);

        private int GetValueIndex(ref object editValue)
        {
            object obj2;
            if (this.EditStrategy.ProvideEditValue(editValue, out obj2, UpdateEditorSource.ValueChanging))
            {
                editValue = obj2;
            }
            return this.EditStrategy.ItemsProvider.IndexOfValue(editValue, this.EditStrategy.CurrentDataViewHandle);
        }

        private bool IsOptimizedSyncWithValueOnDataChanged(ListChangedType changeType) => 
            (changeType == ListChangedType.ItemAdded) || ((changeType == ListChangedType.ItemDeleted) || (changeType == ListChangedType.ItemChanged));

        public bool OptimizedSyncWithValueOnDataChanged(ItemsProviderDataChangedEventArgs e, object editValue, Action syncWithValue)
        {
            if (!this.EditStrategy.IsInProcessNewValue)
            {
                if (!this.IsOptimizedSyncWithValueOnDataChanged(e.ListChangedType))
                {
                    return false;
                }
                if (this.ShouldSyncWithValueOnDataChanged(e, editValue))
                {
                    syncWithValue();
                }
                if ((e.ListChangedType == ListChangedType.ItemChanged) && ((e.Descriptor == null) || e.Descriptor.If<PropertyDescriptor>(x => (x.Name == this.Editor.DisplayMember)).ReturnSuccess<PropertyDescriptor>()))
                {
                    ((EditStrategyBase) this.EditStrategy).UpdateDisplayText();
                }
            }
            return true;
        }

        public void RaiseSelectedIndexChangedEvent(object oldValue, object newValue)
        {
            int indexFromEditValue = this.GetIndexFromEditValue(oldValue);
            if (!indexFromEditValue.Equals(this.GetIndexFromEditValue(newValue)))
            {
                this.Editor.RaiseEvent(new RoutedEventArgs(this.EditStrategy.SelectedIndexChangedEvent));
            }
        }

        public void SetOwner(ISelectorEditStrategy strategy)
        {
            this.EditStrategy = strategy;
        }

        private bool ShouldSyncWithValueOnDataChanged(ItemsProviderDataChangedEventArgs e, object editValue)
        {
            if (!this.EditStrategy.IsSingleSelection && !this.EditStrategy.IsTokenMode)
            {
                return true;
            }
            int valueIndex = this.GetValueIndex(ref editValue);
            return (((e.ListChangedType == ListChangedType.ItemAdded) || (e.ListChangedType == ListChangedType.ItemDeleted)) ? ((valueIndex == -1) || (e.NewIndex == valueIndex)) : ((e.ListChangedType != ListChangedType.ItemChanged) || ((e.NewIndex != valueIndex) || (editValue != this.EditStrategy.ItemsProvider.GetValueByIndex(valueIndex, this.EditStrategy.CurrentDataViewHandle)))));
        }

        public void SyncICollectionView(object editValue)
        {
            if (this.Editor.IsSynchronizedWithCurrentItem)
            {
                IItemsProviderCollectionViewSupport icollectionView = this.ItemsProvider;
                object currentItem = this.ItemsProvider.GetItem(editValue, this.EditStrategy.CurrentDataViewHandle);
                if ((currentItem != null) || (this.ItemsProvider.GetIndexByItem(null, this.EditStrategy.CurrentDataViewHandle) >= 0))
                {
                    this.SyncWithICollectionViewLocker.DoLockedActionIfNotLocked(() => icollectionView.SetCurrentItem(currentItem));
                }
            }
        }

        public void SyncWithICollectionView()
        {
            this.SyncWithICollectionViewLocker.DoIfNotLocked(() => this.ItemsProvider.SyncWithCurrentItem());
        }

        public void SyncWithICollectionView(object currentItem)
        {
            this.SyncWithICollectionViewLocker.DoIfNotLocked(() => this.EditStrategy.EditValue = this.GetEditValueFromSelectedItem(currentItem));
        }

        public void UpdateSelectedItems(object selectedItems)
        {
            this.Editor.SelectedItems.Clear();
            IList source = selectedItems as IList;
            if (source == null)
            {
                object item = this.FilterSelectedItem(selectedItems);
                if (item != null)
                {
                    this.Editor.SelectedItems.Add(item);
                }
            }
            else
            {
                foreach (object obj2 in this.FilterSelectedItems(source.Cast<object>()))
                {
                    this.Editor.SelectedItems.Add(obj2);
                }
            }
        }

        private Locker SyncWithICollectionViewLocker { get; set; }

        private bool IsInLookUpMode =>
            this.EditStrategy.IsInLookUpMode;

        private ISelectorEditStrategy EditStrategy { get; set; }

        private ISelectorEdit Editor =>
            this.EditStrategy.Editor;

        private IItemsProvider2 ItemsProvider =>
            this.EditStrategy.ItemsProvider;

        public bool ShouldSyncWithItems =>
            this.Editor.Items.Count > 0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectorPropertiesCoercionHelper.<>c <>9 = new SelectorPropertiesCoercionHelper.<>c();
            public static Func<object, bool> <>9__35_0;

            internal bool <GetEditValueFromItems>b__35_0(object element) => 
                (element is ListBoxEditItem) && ((ListBoxEditItem) element).IsSelected;
        }
    }
}

