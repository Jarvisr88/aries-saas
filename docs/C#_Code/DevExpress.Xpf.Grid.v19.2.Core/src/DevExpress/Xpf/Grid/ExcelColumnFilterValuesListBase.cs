namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class ExcelColumnFilterValuesListBase : List<ExcelColumnFilterValue>
    {
        private readonly Locker IsCheckedChangedLocker = new Locker();
        private int VisibleNullItemsCount;
        protected int VisibleItemsCount;
        protected internal ExcelColumnFilterServiceValueSelectAll ServiceItem_SelectAll;
        protected internal ExcelColumnFilterServiceValueAddFilter ServiceItem_AddFilter;
        protected internal ExcelColumnFilterValue ServiceItem_SelectBlanks;
        protected internal bool LockCheckedChanged;

        public event EventHandler BeginSelection;

        public event EventHandler EndSelection;

        public event EventHandler IsCheckedChanged;

        protected ExcelColumnFilterValuesListBase()
        {
        }

        public virtual void AddItem(ExcelColumnFilterValue item)
        {
            base.Add(item);
            item.PropertyChanging += new PropertyChangingEventHandler(this.Item_PropertyChanging);
            item.PropertyChanged += new PropertyChangedEventHandler(this.Item_PropertyChanged);
            if (item is ExcelColumnFilterServiceValueBase)
            {
                if (item is ExcelColumnFilterServiceValueAddFilter)
                {
                    this.ServiceItem_AddFilter = item as ExcelColumnFilterServiceValueAddFilter;
                }
                else if (item is ExcelColumnFilterServiceValueSelectAll)
                {
                    this.ServiceItem_SelectAll = item as ExcelColumnFilterServiceValueSelectAll;
                }
            }
            else
            {
                int visibleCheckedItemsCount;
                item.IsVisibleChanged += new EventHandler(this.Item_IsVisibleChanged);
                if (item.IsVisible)
                {
                    this.VisibleItemsCount++;
                    if (item.IsChecked == null)
                    {
                        this.VisibleNullItemsCount++;
                    }
                    else if (item.IsChecked.Value)
                    {
                        visibleCheckedItemsCount = this.VisibleCheckedItemsCount;
                        this.VisibleCheckedItemsCount = visibleCheckedItemsCount + 1;
                    }
                }
                if ((item.IsChecked != null) && item.IsChecked.Value)
                {
                    visibleCheckedItemsCount = this.CheckedItemsCount;
                    this.CheckedItemsCount = visibleCheckedItemsCount + 1;
                }
                if (ColumnFilterInfoHelper.IsNullOrEmptyString(item.EditValue))
                {
                    this.ServiceItem_SelectBlanks = item;
                }
            }
        }

        public void AddSearchAddFilterItem(string caption)
        {
            ExcelColumnFilterServiceValueAddFilter item = new ExcelColumnFilterServiceValueAddFilter(caption);
            this.AddItem(item);
        }

        public void AddSelectAllItem(string caption, bool isChecked, bool isVisible = true)
        {
            ExcelColumnFilterServiceValueSelectAll item = new ExcelColumnFilterServiceValueSelectAll(caption, isChecked);
            item.SetIsVisible(isVisible);
            this.AddItem(item);
        }

        public void AddSelectBlanksItem(string caption)
        {
            ExcelColumnFilterValue item = new ExcelColumnFilterValue(string.Empty, caption);
            this.AddItem(item);
        }

        public void ChangeSelectAll(bool isTrue)
        {
            this.ChangeSelectAll(isTrue, false);
        }

        internal void ChangeSelectAll(bool isTrue, bool includeInvisible)
        {
            this.IsCheckedChangedLocker.DoLockedActionIfNotLocked(delegate {
                this.RaiseBeginSelection();
                foreach (ExcelColumnFilterValue value2 in this)
                {
                    if ((value2.IsVisible | includeInvisible) && (!ReferenceEquals(value2, this.ServiceItem_AddFilter) && !ReferenceEquals(value2, this.ServiceItem_SelectAll)))
                    {
                        value2.IsChecked = new bool?(isTrue);
                    }
                }
                this.UpdateSelectAllItem();
                this.RaiseEndSelection();
            });
            this.RaiseOnCheckedChanged();
        }

        private void DataItemCheckedChanged(ExcelColumnFilterValue item)
        {
            if (item.IsChecked == null)
            {
                this.VisibleNullItemsCount++;
            }
            else if (item.IsChecked.Value)
            {
                int checkedItemsCount = this.CheckedItemsCount;
                this.CheckedItemsCount = checkedItemsCount + 1;
                if (item.IsVisible)
                {
                    checkedItemsCount = this.VisibleCheckedItemsCount;
                    this.VisibleCheckedItemsCount = checkedItemsCount + 1;
                }
            }
            this.IsCheckedChangedLocker.DoLockedActionIfNotLocked(delegate {
                this.UpdateSelectAllItem();
                this.RaiseOnCheckedChanged();
            });
        }

        private void DataItemCheckedChanging(ExcelColumnFilterValue item)
        {
            if (item.IsChecked == null)
            {
                this.VisibleNullItemsCount--;
            }
            else if (item.IsChecked.Value)
            {
                int checkedItemsCount = this.CheckedItemsCount;
                this.CheckedItemsCount = checkedItemsCount - 1;
                if (item.IsVisible)
                {
                    checkedItemsCount = this.VisibleCheckedItemsCount;
                    this.VisibleCheckedItemsCount = checkedItemsCount - 1;
                }
            }
        }

        public abstract ExcelColumnFilterValue FindItem(object value, ColumnFilterMode columnFilterMode);
        public IEnumerable<ExcelColumnFilterValue> GetFilterableValues() => 
            from item in this
                where !ReferenceEquals(item, this.ServiceItem_SelectAll) && !ReferenceEquals(item, this.ServiceItem_AddFilter)
                select item;

        private bool GetIsSelectAll()
        {
            bool? selectAllValue = this.GetSelectAllValue();
            return ((selectAllValue == null) ? false : selectAllValue.Value);
        }

        private bool GetIsSelectNone()
        {
            bool? selectAllValue = this.GetSelectAllValue();
            return ((selectAllValue == null) ? false : !selectAllValue.Value);
        }

        internal List<ExcelColumnFilterValue> GetItemsWithCriteriaOperator()
        {
            List<ExcelColumnFilterValue> list = new List<ExcelColumnFilterValue>();
            foreach (ExcelColumnFilterValue value2 in this)
            {
                if (value2.EditValue is CriteriaOperator)
                {
                    list.Add(value2);
                }
            }
            return list;
        }

        private bool? GetSelectAllValue()
        {
            if ((this.VisibleCheckedItemsCount == this.VisibleItemsCount) && (this.VisibleItemsCount > 0))
            {
                return true;
            }
            if ((this.VisibleCheckedItemsCount + this.VisibleNullItemsCount) == 0)
            {
                return false;
            }
            return null;
        }

        public IEnumerable<ExcelColumnFilterValue> GetVisibleItems()
        {
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__39_0;
                predicate = <>c.<>9__39_0 = item => item.IsVisible;
            }
            return this.Where<ExcelColumnFilterValue>(predicate);
        }

        private void Item_IsVisibleChanged(object sender, EventArgs e)
        {
            ExcelColumnFilterValue item = sender as ExcelColumnFilterValue;
            if ((item != null) && !(item is ExcelColumnFilterServiceValueBase))
            {
                this.UpdateVisibleItemsCount(item);
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                ExcelColumnFilterValue item = sender as ExcelColumnFilterValue;
                if (item != null)
                {
                    if (!(item is ExcelColumnFilterServiceValueBase))
                    {
                        this.DataItemCheckedChanged(item);
                    }
                    else if (ReferenceEquals(item, this.ServiceItem_AddFilter))
                    {
                        this.UpdateCollection(item);
                    }
                    else if (ReferenceEquals(item, this.ServiceItem_SelectAll) && (item.IsChecked != null))
                    {
                        this.ChangeSelectAll(item.IsChecked.Value);
                    }
                }
            }
        }

        private void Item_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                ExcelColumnFilterValue item = sender as ExcelColumnFilterValue;
                if ((item != null) && !(item is ExcelColumnFilterServiceValueBase))
                {
                    this.DataItemCheckedChanging(item);
                }
            }
        }

        protected void RaiseBeginSelection()
        {
            if (this.BeginSelection != null)
            {
                this.BeginSelection(this, EventArgs.Empty);
            }
        }

        protected void RaiseEndSelection()
        {
            if (this.EndSelection != null)
            {
                this.EndSelection(this, EventArgs.Empty);
            }
        }

        protected void RaiseOnCheckedChanged()
        {
            if (!this.LockCheckedChanged && (this.IsCheckedChanged != null))
            {
                this.IsCheckedChanged(this, EventArgs.Empty);
            }
        }

        protected void UpdateCollection(ExcelColumnFilterValue item)
        {
            this.RaiseOnCheckedChanged();
        }

        public virtual void UpdateFilter(string oldValue, string newValue)
        {
            if (this.ServiceItem_AddFilter != null)
            {
                if (string.IsNullOrEmpty(newValue))
                {
                    this.ServiceItem_AddFilter.IsChecked = false;
                    this.ServiceItem_AddFilter.SetIsVisible(false);
                }
                else
                {
                    this.ServiceItem_AddFilter.SetIsVisible(true);
                }
            }
        }

        private void UpdateSelectAllItem()
        {
            if (this.ServiceItem_SelectAll != null)
            {
                this.ServiceItem_SelectAll.IsChecked = this.GetSelectAllValue();
            }
        }

        private void UpdateVisibleItemsCount(ExcelColumnFilterValue item)
        {
            this.VisibleItemsCount = !item.IsVisible ? (this.VisibleItemsCount - 1) : (this.VisibleItemsCount + 1);
            if ((item.IsChecked != null) && item.IsChecked.Value)
            {
                int visibleCheckedItemsCount;
                if (item.IsVisible)
                {
                    visibleCheckedItemsCount = this.VisibleCheckedItemsCount;
                    this.VisibleCheckedItemsCount = visibleCheckedItemsCount + 1;
                }
                else
                {
                    visibleCheckedItemsCount = this.VisibleCheckedItemsCount;
                    this.VisibleCheckedItemsCount = visibleCheckedItemsCount - 1;
                }
            }
            this.UpdateSelectAllItem();
        }

        protected int VisibleCheckedItemsCount { get; private set; }

        protected int CheckedItemsCount { get; private set; }

        public bool IsSelectAll =>
            this.GetIsSelectAll();

        public bool IsSelectNone =>
            this.GetIsSelectNone();

        public abstract bool IsAllItemsChecked { get; }

        internal abstract bool AllItemsVisible { get; }

        public bool SelectedItemsAddedToFilter =>
            (this.ServiceItem_AddFilter != null) ? ((this.ServiceItem_AddFilter.IsChecked == null) ? false : this.ServiceItem_AddFilter.IsChecked.Value) : false;

        public abstract int FilterValuesCount { get; }

        internal abstract bool ForceCalcVisibility { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelColumnFilterValuesListBase.<>c <>9 = new ExcelColumnFilterValuesListBase.<>c();
            public static Func<ExcelColumnFilterValue, bool> <>9__39_0;

            internal bool <GetVisibleItems>b__39_0(ExcelColumnFilterValue item) => 
                item.IsVisible;
        }
    }
}

