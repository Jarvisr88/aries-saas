namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class SortInfoCollectionBase : ObservableCollectionCore<GridSortInfo>
    {
        protected int fGroupCount;
        private string defaultSorting;
        private ListSortDirection defaultSortOrder;

        protected SortInfoCollectionBase()
        {
        }

        public void AddRange(params GridSortInfo[] items)
        {
            this.BeginUpdate();
            try
            {
                GridSortInfo[] infoArray = items;
                int index = 0;
                while (true)
                {
                    if (index >= infoArray.Length)
                    {
                        this.VerifyDefaultSorting();
                        break;
                    }
                    GridSortInfo item = infoArray[index];
                    if (this.IsValid(item))
                    {
                        base.Add(item);
                    }
                    index++;
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        public void ClearAndAddRange(params GridSortInfo[] items)
        {
            this.ClearAndAddRangeCore(0, items);
        }

        public void ClearAndAddRange(params string[] fieldNames)
        {
            this.ClearAndAddRange(0, fieldNames);
        }

        public void ClearAndAddRange(int groupCount, params string[] fieldNames)
        {
            GridSortInfo[] items = new GridSortInfo[fieldNames.Length];
            for (int i = 0; i < fieldNames.Length; i++)
            {
                GridSortInfo info1 = new GridSortInfo();
                info1.FieldName = fieldNames[i];
                items[i] = info1;
            }
            this.ClearAndAddRangeCore(groupCount, items);
        }

        internal void ClearAndAddRangeCore(int groupCount, params GridSortInfo[] items)
        {
            this.BeginUpdate();
            try
            {
                base.Clear();
                this.AddRange(items);
                this.GroupCountInternal = groupCount;
            }
            finally
            {
                this.EndUpdate();
            }
        }

        public void ClearSorting()
        {
            if (this.GroupCountInternal < base.Count)
            {
                this.BeginUpdate();
                this.LockVerifying = true;
                try
                {
                    while (this.GroupCountInternal < base.Count)
                    {
                        base.RemoveAt(base.Count - 1);
                    }
                }
                finally
                {
                    this.LockVerifying = false;
                    this.VerifyDefaultSorting();
                    this.EndUpdate();
                }
            }
        }

        protected override void InsertItem(int index, GridSortInfo item)
        {
            item.Owner = this;
            base.InsertItem(index, item);
        }

        internal bool IsSingleGroup() => 
            (base.Count == this.GroupCountInternal) && (base.Count == 1);

        internal bool IsSingleSort() => 
            (this.GroupCountInternal == 0) && (base.Count == 1);

        protected virtual bool IsValid(GridSortInfo item) => 
            !string.IsNullOrEmpty(item.FieldName);

        internal void OnChanged()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected internal void OnColumnHeaderClick(string name, bool isShift, bool isCtrl, bool singleGroupSortMode = false, AllowedSortOrders allowedDirections = 3, ListSortDirection defaultDirection = 0, ColumnSortClearMode clearSortMode = 0)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ListSortDirection actualDirection = GridSortInfo.GetActualDirection(allowedDirections, defaultDirection);
                if (isShift)
                {
                    this.OnColumnHeaderClickAddSort(name, allowedDirections, actualDirection, singleGroupSortMode);
                }
                else if (this.ShouldRemoveSortOnClick(name, isCtrl, allowedDirections, actualDirection, clearSortMode))
                {
                    this.OnColumnHeaderClickRemoveSort(name);
                }
                else
                {
                    GridSortInfo item = this[name];
                    if (!singleGroupSortMode || ((item != null) || !this.IsSingleGroup()))
                    {
                        this.BeginUpdate();
                        if (item == null)
                        {
                            GridSortInfo info1 = new GridSortInfo();
                            info1.FieldName = name;
                            info1.SortOrder = actualDirection;
                            item = info1;
                        }
                        else if (GridSortInfo.GetActualDirection(allowedDirections, GridSortInfo.InvertSortDirection(item.SortOrder)) != item.SortOrder)
                        {
                            item.ChangeSortOrder(name);
                        }
                        try
                        {
                            this.LockVerifying = true;
                            int index = base.Count - 1;
                            while (true)
                            {
                                if (index < this.GroupCountInternal)
                                {
                                    if (this[item.FieldName] == null)
                                    {
                                        base.Add(item);
                                    }
                                    this.LockVerifying = false;
                                    this.VerifyDefaultSorting();
                                    break;
                                }
                                base.RemoveAt(index);
                                index--;
                            }
                        }
                        finally
                        {
                            this.EndUpdate();
                        }
                    }
                }
            }
        }

        internal void OnColumnHeaderClickAddSort(string name, AllowedSortOrders allowedDirections, ListSortDirection direction, bool singleGroupSortMode)
        {
            GridSortInfo item = this[name];
            if (singleGroupSortMode && ((base.Count > 0) && (item == null)))
            {
                if (this.IsSingleGroup())
                {
                    return;
                }
                if (this.IsSingleSort())
                {
                    this.ClearSorting();
                }
            }
            this.BeginUpdate();
            try
            {
                if (item == null)
                {
                    GridSortInfo info1 = new GridSortInfo();
                    info1.FieldName = name;
                    info1.SortOrder = direction;
                    item = info1;
                    base.Add(item);
                }
                else if (GridSortInfo.GetActualDirection(allowedDirections, GridSortInfo.InvertSortDirection(item.SortOrder)) != item.SortOrder)
                {
                    if (base.IndexOf(item) < this.GroupCountInternal)
                    {
                        item.ChangeSortOrder(name);
                    }
                    else
                    {
                        base.Remove(item);
                        item.ChangeSortOrder(name);
                        base.Add(item);
                    }
                }
                this.VerifyDefaultSorting();
            }
            finally
            {
                this.EndUpdate();
            }
        }

        internal void OnColumnHeaderClickRemoveSort(string name)
        {
            GridSortInfo item = this[name];
            if ((item != null) && (base.IndexOf(item) >= this.GroupCountInternal))
            {
                base.Remove(item);
            }
            this.VerifyDefaultSorting();
        }

        protected override void RemoveItem(int index)
        {
            base[index].Owner = null;
            base.RemoveItem(index);
            this.VerifyDefaultSorting();
        }

        private bool ShouldRemoveSortOnClick(string name, bool isCtrl, AllowedSortOrders allowedDirections, ListSortDirection actualDirection, ColumnSortClearMode clearSortMode)
        {
            if (clearSortMode == ColumnSortClearMode.ClickWithCtrlPressed)
            {
                return isCtrl;
            }
            GridSortInfo item = this[name];
            return ((item != null) && ((base.IndexOf(item) >= this.GroupCountInternal) && (GridSortInfo.GetActualDirection(allowedDirections, GridSortInfo.InvertSortDirection(item.SortOrder)) == actualDirection)));
        }

        internal void SortByColumn(string fieldName, ColumnSortOrder direction, int sortIndex)
        {
            if (!string.IsNullOrEmpty(fieldName))
            {
                GridSortInfo item = this[fieldName];
                this.BeginUpdate();
                this.LockVerifying = true;
                try
                {
                    if (item == null)
                    {
                        GridSortInfo info1 = new GridSortInfo();
                        info1.FieldName = fieldName;
                        item = info1;
                    }
                    else
                    {
                        if (base.IndexOf(item) < this.GroupCountInternal)
                        {
                            int groupCountInternal = this.GroupCountInternal;
                            this.GroupCountInternal = groupCountInternal - 1;
                        }
                        base.Remove(item);
                    }
                    if ((sortIndex > -1) && (direction != ColumnSortOrder.None))
                    {
                        item.SortOrder = GridSortInfo.GetSortDirectionBySortOrder(direction);
                        int index = sortIndex + this.GroupCountInternal;
                        if (index > (base.Count - 1))
                        {
                            index = base.Count;
                        }
                        base.Insert(index, item);
                    }
                }
                finally
                {
                    this.LockVerifying = false;
                    this.VerifyDefaultSorting();
                    this.EndUpdate();
                }
            }
        }

        internal void VerifyDefaultSorting()
        {
            if (!this.LockVerifying && !string.IsNullOrEmpty(this.DefaultSorting))
            {
                this.LockVerifying = true;
                try
                {
                    GridSortInfo item = this[this.DefaultSorting];
                    if (item != null)
                    {
                        int index = base.IndexOf(item);
                        if (index < this.GroupCountInternal)
                        {
                            return;
                        }
                        else if (index >= (base.Count - 1))
                        {
                            return;
                        }
                        else
                        {
                            base.Remove(item);
                        }
                    }
                    base.Add(new GridSortInfo(this.DefaultSorting, this.DefaultSortOrder));
                }
                finally
                {
                    this.LockVerifying = false;
                }
            }
        }

        protected bool LockVerifying { get; set; }

        internal string DefaultSorting
        {
            get => 
                this.defaultSorting;
            set
            {
                if (value != this.DefaultSorting)
                {
                    this.defaultSorting = value;
                    this.VerifyDefaultSorting();
                }
            }
        }

        internal ListSortDirection DefaultSortOrder
        {
            get => 
                this.defaultSortOrder;
            set
            {
                if (this.defaultSortOrder != value)
                {
                    this.defaultSortOrder = value;
                    this.VerifyDefaultSorting();
                }
            }
        }

        internal int GroupCountCore =>
            this.fGroupCount;

        internal int GroupCountInternal
        {
            get => 
                (this.fGroupCount < base.Count) ? this.fGroupCount : base.Count;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (this.fGroupCount != value)
                {
                    this.fGroupCount = value;
                    this.OnChanged();
                }
            }
        }

        public GridSortInfo this[string name] =>
            GridSortInfo.GetSortInfoByFieldName(this, name);
    }
}

