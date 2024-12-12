namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ExcelDateColumnFilterList : ExcelColumnFilterValuesListBase
    {
        internal readonly ExcelDateColumnFilterScope FilterScope;
        protected bool HasVisibleItems;

        public ExcelDateColumnFilterList() : this(ExcelDateColumnFilterScope.Year)
        {
        }

        public ExcelDateColumnFilterList(ExcelDateColumnFilterScope filterScope)
        {
            this.FilterScope = filterScope;
        }

        public override void AddItem(ExcelColumnFilterValue item)
        {
            base.AddItem(item);
            ExcelDateColumnFilterValue value2 = item as ExcelDateColumnFilterValue;
            if (value2 != null)
            {
                value2.ItemsCheckedChanged += new EventHandler(this.DateValue_ItemsCheckedChanged);
            }
        }

        private void DateValue_ItemsCheckedChanged(object sender, EventArgs e)
        {
            base.UpdateCollection(sender as ExcelColumnFilterValue);
        }

        private List<ExcelColumnFilterValue> FindAllDayItems()
        {
            List<ExcelColumnFilterValue> list = new List<ExcelColumnFilterValue>();
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = i => i is ExcelDateDayColumnFilterValue;
            }
            foreach (ExcelDateDayColumnFilterValue value2 in this.Where<ExcelColumnFilterValue>(predicate))
            {
                list.Add(value2);
            }
            Func<ExcelColumnFilterValue, bool> func2 = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<ExcelColumnFilterValue, bool> local2 = <>c.<>9__7_1;
                func2 = <>c.<>9__7_1 = i => i is ExcelDateColumnFilterValue;
            }
            foreach (ExcelDateColumnFilterValue value3 in this.Where<ExcelColumnFilterValue>(func2))
            {
                list.AddRange(value3.Items.FindAllDayItems());
            }
            if (base.ServiceItem_SelectBlanks != null)
            {
                list.Add(base.ServiceItem_SelectBlanks);
            }
            return list;
        }

        internal ExcelDateDayColumnFilterValue FindDayItem(DateTime date)
        {
            ExcelDateDayColumnFilterValue value3;
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__6_0;
                predicate = <>c.<>9__6_0 = i => i is ExcelDateDayColumnFilterValue;
            }
            using (IEnumerator<ExcelColumnFilterValue> enumerator = this.Where<ExcelColumnFilterValue>(predicate).GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    ExcelDateDayColumnFilterValue current = (ExcelDateDayColumnFilterValue) enumerator.Current;
                    if (current.Date == date)
                    {
                        return current;
                    }
                }
            }
            Func<ExcelColumnFilterValue, bool> func2 = <>c.<>9__6_1;
            if (<>c.<>9__6_1 == null)
            {
                Func<ExcelColumnFilterValue, bool> local2 = <>c.<>9__6_1;
                func2 = <>c.<>9__6_1 = i => i is ExcelDateColumnFilterValue;
            }
            using (IEnumerator<ExcelColumnFilterValue> enumerator2 = this.Where<ExcelColumnFilterValue>(func2).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        ExcelDateColumnFilterValue current = (ExcelDateColumnFilterValue) enumerator2.Current;
                        ExcelDateDayColumnFilterValue value5 = current.Items.FindDayItem(date);
                        if (value5 == null)
                        {
                            continue;
                        }
                        value3 = value5;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return value3;
        }

        public override ExcelColumnFilterValue FindItem(object value, ColumnFilterMode columnFilterMode)
        {
            if (value is DateTime)
            {
                return this.FindDayItem((DateTime) value);
            }
            DateTime? nullable = value as DateTime?;
            return ((nullable == null) ? (!ColumnFilterInfoHelper.IsNullOrEmptyString(value) ? null : base.ServiceItem_SelectBlanks) : this.FindDayItem(nullable.Value));
        }

        private int GetCheckedSubItemsCount()
        {
            int num = 0;
            foreach (ExcelColumnFilterValue value2 in this)
            {
                if (!ReferenceEquals(value2, base.ServiceItem_SelectAll) && !ReferenceEquals(value2, base.ServiceItem_AddFilter))
                {
                    ExcelDateColumnFilterValue value3 = value2 as ExcelDateColumnFilterValue;
                    if ((value3 != null) && value3.Items.IsAllItemsChecked)
                    {
                        num++;
                        continue;
                    }
                    if ((value2.IsChecked != null) && value2.IsChecked.Value)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public void UpdateFilter(ExcelDateColumnFilterScope filterScope, string oldValue, string value)
        {
            this.HasVisibleItems = false;
            bool flag = (filterScope & this.FilterScope) == this.FilterScope;
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__8_0;
                predicate = <>c.<>9__8_0 = i => i is ExcelDateColumnFilterValue;
            }
            foreach (ExcelDateColumnFilterValue value2 in this.Where<ExcelColumnFilterValue>(predicate))
            {
                if (string.IsNullOrWhiteSpace(value) || (flag && ColumnFilterInfoHelper.GetStringContainsSubstring(value2.GetComputedValue().ToString(), value)))
                {
                    value2.SetIsVisible(true);
                    value2.Items.UpdateFilter(filterScope, oldValue, null);
                    this.HasVisibleItems = true;
                    continue;
                }
                value2.Items.UpdateFilter(filterScope, oldValue, value);
                if (value2.Items.HasVisibleItems)
                {
                    this.HasVisibleItems = true;
                }
                value2.SetIsVisible(value2.Items.HasVisibleItems);
            }
            Func<ExcelColumnFilterValue, bool> func2 = <>c.<>9__8_1;
            if (<>c.<>9__8_1 == null)
            {
                Func<ExcelColumnFilterValue, bool> local2 = <>c.<>9__8_1;
                func2 = <>c.<>9__8_1 = i => (i is ExcelDateDayColumnFilterValue) || ((i.EditValue is string) && (((string) i.EditValue) == string.Empty));
            }
            foreach (ExcelColumnFilterValue value3 in this.Where<ExcelColumnFilterValue>(func2))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value3.SetIsVisible(true);
                    this.HasVisibleItems = true;
                    continue;
                }
                if (ReferenceEquals(value3, base.ServiceItem_SelectBlanks) && (value3.DisplayValue != null))
                {
                    base.ServiceItem_SelectBlanks.SetIsVisible(ColumnFilterInfoHelper.GetStringContainsSubstring(base.ServiceItem_SelectBlanks.DisplayValue.ToString(), value));
                    if (!base.ServiceItem_SelectBlanks.IsVisible)
                    {
                        continue;
                    }
                    this.HasVisibleItems = true;
                    continue;
                }
                if (!flag)
                {
                    value3.SetIsVisible(false);
                    continue;
                }
                value3.SetIsVisible(ColumnFilterInfoHelper.GetStringContainsSubstring(value3.GetComputedValue().ToString(), value));
                if (value3.IsVisible)
                {
                    this.HasVisibleItems = true;
                }
            }
            this.UpdateFilter(oldValue, value);
        }

        public override int FilterValuesCount =>
            this.FindAllDayItems().Count;

        internal override bool AllItemsVisible
        {
            get
            {
                Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__13_0;
                    predicate = <>c.<>9__13_0 = item => !item.IsVisible;
                }
                return (this.FindAllDayItems().Where<ExcelColumnFilterValue>(predicate).ToList<ExcelColumnFilterValue>().Count == 0);
            }
        }

        internal override bool ForceCalcVisibility =>
            false;

        public override bool IsAllItemsChecked =>
            this.GetCheckedSubItemsCount() == ((base.Count - 1) - ((base.ServiceItem_AddFilter != null) ? 1 : 0));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelDateColumnFilterList.<>c <>9 = new ExcelDateColumnFilterList.<>c();
            public static Func<ExcelColumnFilterValue, bool> <>9__6_0;
            public static Func<ExcelColumnFilterValue, bool> <>9__6_1;
            public static Func<ExcelColumnFilterValue, bool> <>9__7_0;
            public static Func<ExcelColumnFilterValue, bool> <>9__7_1;
            public static Func<ExcelColumnFilterValue, bool> <>9__8_0;
            public static Func<ExcelColumnFilterValue, bool> <>9__8_1;
            public static Func<ExcelColumnFilterValue, bool> <>9__13_0;

            internal bool <FindAllDayItems>b__7_0(ExcelColumnFilterValue i) => 
                i is ExcelDateDayColumnFilterValue;

            internal bool <FindAllDayItems>b__7_1(ExcelColumnFilterValue i) => 
                i is ExcelDateColumnFilterValue;

            internal bool <FindDayItem>b__6_0(ExcelColumnFilterValue i) => 
                i is ExcelDateDayColumnFilterValue;

            internal bool <FindDayItem>b__6_1(ExcelColumnFilterValue i) => 
                i is ExcelDateColumnFilterValue;

            internal bool <get_AllItemsVisible>b__13_0(ExcelColumnFilterValue item) => 
                !item.IsVisible;

            internal bool <UpdateFilter>b__8_0(ExcelColumnFilterValue i) => 
                i is ExcelDateColumnFilterValue;

            internal bool <UpdateFilter>b__8_1(ExcelColumnFilterValue i) => 
                (i is ExcelDateDayColumnFilterValue) || ((i.EditValue is string) && (((string) i.EditValue) == string.Empty));
        }
    }
}

