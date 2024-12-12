namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.GridData;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GridSortInfo : DependencyObject, IColumnInfo, IMergeWithPreviousGroup
    {
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty SortOrderProperty;
        public static readonly DependencyProperty IsGroupedProperty;
        private static readonly DependencyPropertyKey IsGroupedPropertyKey;
        public static readonly DependencyProperty GroupIndexProperty;
        public static readonly DependencyProperty SortIndexProperty;
        public static readonly DependencyProperty MergeWithPreviousGroupProperty;
        internal SortInfoCollectionBase Owner;

        static GridSortInfo()
        {
            Type ownerType = typeof(GridSortInfo);
            FieldNameProperty = DependencyPropertyManager.Register("FieldName", typeof(string), ownerType, new PropertyMetadata(string.Empty, (d, e) => ((GridSortInfo) d).OnChanged(), new CoerceValueCallback(GridSortInfo.OnCoerceName)));
            SortOrderProperty = DependencyPropertyManager.Register("SortOrder", typeof(ListSortDirection), ownerType, new PropertyMetadata(ListSortDirection.Ascending, (d, e) => ((GridSortInfo) d).OnChanged()));
            IsGroupedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsGrouped", typeof(bool), ownerType, new PropertyMetadata(false));
            IsGroupedProperty = IsGroupedPropertyKey.DependencyProperty;
            GroupIndexProperty = DependencyPropertyManager.Register("GroupIndex", typeof(int), ownerType, new PropertyMetadata(-1));
            SortIndexProperty = DependencyPropertyManager.Register("SortIndex", typeof(int), ownerType, new PropertyMetadata(-1));
            MergeWithPreviousGroupProperty = DependencyPropertyManager.Register("MergeWithPreviousGroup", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((GridSortInfo) d).OnChanged()));
        }

        public GridSortInfo() : this(string.Empty, ListSortDirection.Ascending)
        {
        }

        public GridSortInfo(string fieldName) : this(fieldName, ListSortDirection.Ascending)
        {
        }

        public GridSortInfo(string fieldName, ListSortDirection sortOrder) : this(fieldName, sortOrder, false)
        {
        }

        public GridSortInfo(string fieldName, ListSortDirection sortOrder, bool mergeWithPreviousGroup)
        {
            this.FieldName = fieldName;
            this.SortOrder = sortOrder;
            this.MergeWithPreviousGroup = mergeWithPreviousGroup;
        }

        public void ChangeSortOrder()
        {
            this.ChangeSortOrder(this.FieldName);
        }

        public virtual void ChangeSortOrder(string fieldName)
        {
            this.SortOrder = InvertSortDirection(this.SortOrder);
        }

        public static ListSortDirection GetActualDirection(AllowedSortOrders allowedDirections, ListSortDirection defaultDirection)
        {
            switch (allowedDirections)
            {
                case AllowedSortOrders.Ascending:
                    return ListSortDirection.Ascending;

                case AllowedSortOrders.Descending:
                    return ListSortDirection.Descending;
            }
            return defaultDirection;
        }

        internal static ColumnSortOrder GetColumnSortOrder(ListSortDirection sortDirection) => 
            (sortDirection == ListSortDirection.Ascending) ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;

        internal static ListSortDirection GetSortDirectionBySortOrder(ColumnSortOrder sortOrder) => 
            (sortOrder == ColumnSortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;

        internal static GridSortInfo GetSortInfoByFieldName(IList sortInfoList, string fieldName)
        {
            GridSortInfo info2;
            if (string.IsNullOrEmpty(fieldName))
            {
                return null;
            }
            using (IEnumerator enumerator = sortInfoList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        GridSortInfo current = (GridSortInfo) enumerator.Current;
                        if (current.FieldName != fieldName)
                        {
                            continue;
                        }
                        info2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info2;
        }

        internal ColumnSortOrder GetSortOrder() => 
            GetColumnSortOrder(this.SortOrder);

        internal static ListSortDirection InvertSortDirection(ListSortDirection original) => 
            (original == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;

        private void OnChanged()
        {
            if (this.Owner != null)
            {
                this.Owner.OnChanged();
            }
        }

        private static string OnCoerceName(DependencyObject d, object baseValue) => 
            (baseValue == null) ? string.Empty : (baseValue as string);

        internal void SetGroupSortIndexes(int sortIndex, int groupIndex)
        {
            this.GroupIndex = groupIndex;
            this.SortIndex = sortIndex;
            this.IsGrouped = groupIndex >= 0;
        }

        [Description("Gets whether the column which is referred to by the current GridSortInfo object is a grouping column. This is a dependency property.")]
        public bool IsGrouped
        {
            get => 
                (bool) base.GetValue(IsGroupedProperty);
            internal set => 
                base.SetValue(IsGroupedPropertyKey, value);
        }

        [Description("Gets or sets the position of the column referred to by the current GridSortInfo object, among grouping columns. This is a dependency property.")]
        public int GroupIndex
        {
            get => 
                (int) base.GetValue(GroupIndexProperty);
            set => 
                base.SetValue(GroupIndexProperty, value);
        }

        [Description("Gets or sets the position of the column referred to by the current GridSortInfo object, among sorted columns. This is a dependency property.")]
        public int SortIndex
        {
            get => 
                (int) base.GetValue(SortIndexProperty);
            set => 
                base.SetValue(SortIndexProperty, value);
        }

        [Description("Gets or sets the field name of the column to sort. This is a dependency property."), XtraSerializableProperty]
        public string FieldName
        {
            get => 
                (string) base.GetValue(FieldNameProperty);
            set => 
                base.SetValue(FieldNameProperty, value);
        }

        [Description("Gets or sets the column's sort order. This is a dependency property."), XtraSerializableProperty]
        public ListSortDirection SortOrder
        {
            get => 
                (ListSortDirection) base.GetValue(SortOrderProperty);
            set => 
                base.SetValue(SortOrderProperty, value);
        }

        [Description("Gets or sets whether the current group is merged with the previous group. This is a dependency property."), XtraSerializableProperty]
        public bool MergeWithPreviousGroup
        {
            get => 
                (bool) base.GetValue(MergeWithPreviousGroupProperty);
            set => 
                base.SetValue(MergeWithPreviousGroupProperty, value);
        }

        string IColumnInfo.FieldName =>
            this.FieldName;

        ColumnSortOrder IColumnInfo.SortOrder =>
            this.GetSortOrder();

        UnboundColumnType IColumnInfo.UnboundType =>
            UnboundColumnType.Bound;

        string IColumnInfo.UnboundExpression =>
            string.Empty;

        bool IColumnInfo.ReadOnly =>
            false;

        bool IMergeWithPreviousGroup.MergeWithPreviousGroup =>
            this.MergeWithPreviousGroup;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridSortInfo.<>c <>9 = new GridSortInfo.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridSortInfo) d).OnChanged();
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridSortInfo) d).OnChanged();
            }

            internal void <.cctor>b__7_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridSortInfo) d).OnChanged();
            }
        }
    }
}

