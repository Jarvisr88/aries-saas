namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DataControllerItemsCache
    {
        private bool selectNullValue;
        private readonly object nullableValue = new object();
        private int valueIndex;
        private int itemIndex;

        public DataControllerItemsCache(IDataControllerAdapter dataControllerAdapter, bool selectNullValue)
        {
            this.selectNullValue = selectNullValue;
            this.DataControllerAdapter = dataControllerAdapter;
            this.ValueToIndex = new Dictionary<object, int>();
            this.ItemToIndex = new Dictionary<object, int>();
            this.IndexToItem = new Dictionary<int, object>();
            this.IndexToValue = new Dictionary<int, object>();
            this.NotAvailableValues = new HashSet<object>();
        }

        private void AddIndexToCache(int listSourceIndex, object item, object value)
        {
            this.IndexToItem[listSourceIndex] = item;
            this.IndexToValue[listSourceIndex] = value;
        }

        private object AddItemToCache(int listSourceIndex)
        {
            object row = this.DataControllerAdapter.GetRow(listSourceIndex);
            if (!this.ItemToIndex.ContainsKey(this.GetWrapperValue(row)))
            {
                this.ItemToIndex[this.GetWrapperValue(row)] = listSourceIndex;
            }
            return row;
        }

        private object AddValueToCache(int listSourceIndex)
        {
            object rowValue = this.DataControllerAdapter.GetRowValue(listSourceIndex);
            if (!this.ValueToIndex.ContainsKey(this.GetWrapperValue(rowValue)))
            {
                this.ValueToIndex[this.GetWrapperValue(rowValue)] = LookUpPropertyDescriptorBase.IsUnsetValue(rowValue) ? -1 : listSourceIndex;
                this.NotAvailableValues.Remove(rowValue);
            }
            return rowValue;
        }

        private int FindIndexByItem(object item) => 
            !this.DataControllerAdapter.IsOwnSearchProcessing ? this.FindIndexByItemLocal(item) : this.FindIndexByItemServerMode(item);

        private int FindIndexByItemLocal(object item)
        {
            int itemIndex = this.itemIndex;
            while (itemIndex < this.DataControllerAdapter.VisibleRowCount)
            {
                int listSourceIndex = itemIndex;
                object row = this.DataControllerAdapter.GetRow(listSourceIndex);
                object wrapperValue = this.GetWrapperValue(row);
                listSourceIndex = (this.selectNullValue || (wrapperValue != this.nullableValue)) ? listSourceIndex : -1;
                this.ItemToIndex[wrapperValue] = listSourceIndex;
                this.IndexToItem[listSourceIndex] = row;
                if (Equals(row, item))
                {
                    return listSourceIndex;
                }
                itemIndex++;
                this.itemIndex++;
            }
            return -1;
        }

        private int FindIndexByItemServerMode(object item)
        {
            object rowValue = this.DataControllerAdapter.GetRowValue(item);
            return this.FindIndexByValue(rowValue);
        }

        private int FindIndexByValue(object value) => 
            !this.NotAvailableValues.Contains(value) ? (this.DataControllerAdapter.IsOwnSearchProcessing ? this.GetListSourceIndexAsync(value) : this.GetListSourceIndex(value)) : -1;

        private int GetListSourceIndex(object value)
        {
            for (int i = this.valueIndex; i < this.DataControllerAdapter.VisibleRowCount; i++)
            {
                this.valueIndex = this.itemIndex = i + 1;
                int listSourceIndex = i;
                object item = this.AddItemToCache(listSourceIndex);
                object obj3 = this.AddValueToCache(listSourceIndex);
                this.AddIndexToCache(i, item, obj3);
                if (Equals(value, obj3))
                {
                    return listSourceIndex;
                }
            }
            return -1;
        }

        private int GetListSourceIndexAsync(object value)
        {
            int listSourceIndex = this.DataControllerAdapter.GetListSourceIndex(value);
            if (listSourceIndex == -2147483638)
            {
                this.NotAvailableValues.Add(value);
                return -1;
            }
            this.UpdateItem(listSourceIndex);
            return listSourceIndex;
        }

        private object GetWrapperValue(object value) => 
            value ?? this.nullableValue;

        public int IndexByItem(object item)
        {
            int num;
            object wrapperValue = this.GetWrapperValue(item);
            return (this.ItemToIndex.TryGetValue(wrapperValue, out num) ? num : this.FindIndexByItem(item));
        }

        public int IndexOfValue(object value)
        {
            int num;
            object wrapperValue = this.GetWrapperValue(value);
            return ((this.selectNullValue || (wrapperValue != this.nullableValue)) ? (!this.ValueToIndex.TryGetValue(wrapperValue, out num) ? this.FindIndexByValue(value) : num) : -1);
        }

        private void RemoveItem(int listSourceIndex)
        {
            if (this.IndexToItem.ContainsKey(listSourceIndex))
            {
                this.ItemToIndex.Remove(this.GetWrapperValue(this.IndexToItem[listSourceIndex]));
                this.IndexToItem.Remove(listSourceIndex);
            }
            if (this.IndexToValue.ContainsKey(listSourceIndex))
            {
                this.ValueToIndex.Remove(this.GetWrapperValue(this.IndexToValue[listSourceIndex]));
                this.IndexToValue.Remove(listSourceIndex);
            }
        }

        public void Reset()
        {
            this.ResetIndexes();
            this.ValueToIndex.Clear();
            this.ItemToIndex.Clear();
            this.IndexToItem.Clear();
            this.IndexToValue.Clear();
            this.NotAvailableValues.Clear();
        }

        public void ResetIndexes()
        {
            this.itemIndex = 0;
            this.valueIndex = 0;
        }

        public void UpdateItem(int listSourceIndex)
        {
            if (listSourceIndex >= 0)
            {
                this.RemoveItem(listSourceIndex);
                this.AddItemToCache(listSourceIndex);
                this.AddValueToCache(listSourceIndex);
                this.AddIndexToCache(listSourceIndex, this.DataControllerAdapter.GetRow(listSourceIndex), this.DataControllerAdapter.GetRowValue(listSourceIndex));
            }
        }

        public void UpdateItemOnAdding(int newIndex)
        {
            this.Reset();
        }

        public void UpdateItemOnDeleting(int newIndex)
        {
            this.Reset();
        }

        public void UpdateItemOnMoving(int oldIndex, int newIndex)
        {
            this.Reset();
        }

        public void UpdateItemValue(int listSourceIndex, object value)
        {
            this.ValueToIndex[this.GetWrapperValue(value)] = listSourceIndex;
            if ((listSourceIndex >= 0) && this.IndexToValue.ContainsKey(listSourceIndex))
            {
                this.IndexToValue[listSourceIndex] = value;
            }
        }

        public HashSet<object> NotAvailableValues { get; set; }

        public Dictionary<object, int> ValueToIndex { get; private set; }

        public Dictionary<object, int> ItemToIndex { get; private set; }

        public Dictionary<int, object> IndexToItem { get; private set; }

        public Dictionary<int, object> IndexToValue { get; private set; }

        public int ValueIndex =>
            this.valueIndex;

        public int ItemIndex =>
            this.itemIndex;

        private IDataControllerAdapter DataControllerAdapter { get; set; }
    }
}

