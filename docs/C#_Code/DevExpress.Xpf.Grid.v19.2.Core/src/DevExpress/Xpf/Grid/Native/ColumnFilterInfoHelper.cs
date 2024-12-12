namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraGrid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ColumnFilterInfoHelper
    {
        internal static Type[] NotSupportEditSettings = new Type[] { typeof(HyperlinkEditSettings), typeof(AutoSuggestEditSettings) };

        public static IEnumerable<object> AddNullOrEmptyOperator(IEnumerable<object> selectedItems, CriteriaOperator op) => 
            !ContainsIsNullOrEmptyOperator(op) ? selectedItems : string.Empty.Yield<string>().Concat<object>(selectedItems);

        public static bool CanUseEditSettingsInExcelColumnFilter(ColumnBase column) => 
            (column.ColumnFilterMode != DevExpress.Xpf.Grid.ColumnFilterMode.DisplayText) && ((column.EditSettings != null) && (IsSupportedEditSettings(column.EditSettings) && !column.IsAsyncLookup));

        public static bool CanUseEditSettingsInFilterEditor(BaseEditSettings editSettings)
        {
            if (editSettings == null)
            {
                return false;
            }
            foreach (Type type in NotSupportEditSettings)
            {
                if (type.IsAssignableFrom(editSettings.GetType()))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool ContainsIsNullOrEmptyOperator(CriteriaOperator op)
        {
            GroupOperator @operator = op as GroupOperator;
            if (@operator == null)
            {
                return IsNullOrEmptyOperator(op);
            }
            if (@operator.OperatorType != GroupOperatorType.Or)
            {
                return false;
            }
            Func<CriteriaOperator, bool> predicate = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<CriteriaOperator, bool> local1 = <>c.<>9__11_0;
                predicate = <>c.<>9__11_0 = x => IsNullOrEmptyOperator(x);
            }
            return @operator.Operands.Any<CriteriaOperator>(predicate);
        }

        public static CriteriaOperator CreateCriteriaOperator(IEnumerable items, string fieldName, bool implyNullLikeEmptyStringWhenFiltering, Func<object, object> getValue)
        {
            List<CriteriaOperator> operands = new List<CriteriaOperator>();
            List<CriteriaOperator> collection = new List<CriteriaOperator>();
            foreach (object obj2 in items)
            {
                object obj3 = getValue(obj2);
                if (!implyNullLikeEmptyStringWhenFiltering || !IsNullOrEmptyString(obj3))
                {
                    if (obj3 is CriteriaOperator)
                    {
                        operands.Add(obj3 as CriteriaOperator);
                        continue;
                    }
                    collection.Add(new OperandValue(obj3));
                }
            }
            CriteriaOperator item = null;
            if (collection.Count > 0)
            {
                InOperator operator2 = new InOperator(new OperandProperty(fieldName), new CriteriaOperator[0]);
                operator2.Operands.AddRange(collection);
                item = operator2;
            }
            if (operands.Count > 0)
            {
                operands.Insert(0, item);
                item = CriteriaOperator.Or(operands);
            }
            return item;
        }

        public static CriteriaOperator CreateCriteriaOperatorLegacy(IEnumerable items, string fieldName, bool implyNullLikeEmptyStringWhenFiltering, Func<object, object> getValue)
        {
            List<CriteriaOperator> collection = new List<CriteriaOperator>();
            foreach (object obj2 in items)
            {
                object obj3 = getValue(obj2);
                if (!implyNullLikeEmptyStringWhenFiltering || !IsNullOrEmptyString(obj3))
                {
                    collection.Add(new OperandValue(obj3));
                }
            }
            CriteriaOperator @operator = null;
            if (collection.Count > 0)
            {
                InOperator operator2 = new InOperator(new OperandProperty(fieldName), new CriteriaOperator[0]);
                operator2.Operands.AddRange(collection);
                @operator = operator2;
            }
            return @operator;
        }

        public static IList CreateFilterList(DataViewBase view, ColumnBase column) => 
            !column.IsAsyncLookup ? new List<object>() : new InstantFeedbackColumnFilterList(column.ItemsProvider, view, column);

        public static CriteriaOperator CreateIsNullOrEmptyCriteria(string fieldName)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName) };
            return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
        }

        public static BaseEditSettings GetDisplayEditSettings(BaseEditSettings editSettings) => 
            ((editSettings == null) || !IsSupportedEditSettings(editSettings)) ? null : editSettings;

        public static bool GetIncludeFilteredOut(ColumnBase column) => 
            column.GetShowAllTableValuesInFilterPopup() || column.IsFiltered;

        public static CriteriaOperator GetIsNullOrEmptyCriteria(IEnumerable<object> selectedItems, string fieldName, bool implyNullLikeEmptyStringWhenFiltering, Func<object, object> getValue) => 
            (!implyNullLikeEmptyStringWhenFiltering || !selectedItems.Select<object, object>(getValue).Any<object>(new Func<object, bool>(ColumnFilterInfoHelper.IsNullOrEmptyString))) ? null : CreateIsNullOrEmptyCriteria(fieldName);

        public static IEnumerable<object> GetSelectedItems(CriteriaOperator op)
        {
            List<object> list = new List<object>();
            InOperator @operator = op as InOperator;
            GroupOperator groupOperator = op as GroupOperator;
            if (IsValidGroupOperator(groupOperator))
            {
                Func<CriteriaOperator, bool> predicate = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<CriteriaOperator, bool> local1 = <>c.<>9__6_0;
                    predicate = <>c.<>9__6_0 = operand => operand is InOperator;
                }
                @operator = (InOperator) groupOperator.Operands.FirstOrDefault<CriteriaOperator>(predicate);
            }
            if (@operator != null)
            {
                Func<CriteriaOperator, object> selector = <>c.<>9__6_1;
                if (<>c.<>9__6_1 == null)
                {
                    Func<CriteriaOperator, object> local2 = <>c.<>9__6_1;
                    selector = <>c.<>9__6_1 = opValue => ((OperandValue) opValue).Value;
                }
                list.AddRange(@operator.Operands.Select<CriteriaOperator, object>(selector));
            }
            return list;
        }

        public static List<object> GetSelectedItems(CriteriaOperator op, out List<CriteriaOperator> customOperators)
        {
            customOperators = new List<CriteriaOperator>();
            InOperator @operator = op as InOperator;
            if (@operator != null)
            {
                Func<CriteriaOperator, object> selector = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<CriteriaOperator, object> local1 = <>c.<>9__7_0;
                    selector = <>c.<>9__7_0 = opValue => ((OperandValue) opValue).Value;
                }
                return @operator.Operands.Select<CriteriaOperator, object>(selector).ToList<object>();
            }
            List<object> list = new List<object>();
            GroupOperator operator2 = op as GroupOperator;
            if ((operator2 == null) || (operator2.OperatorType != GroupOperatorType.Or))
            {
                if (op != null)
                {
                    List<CriteriaOperator> list1 = new List<CriteriaOperator>();
                    list1.Add(op);
                    customOperators = list1;
                }
                return list;
            }
            foreach (CriteriaOperator operator3 in operator2.Operands)
            {
                InOperator operator4 = operator3 as InOperator;
                if (operator4 == null)
                {
                    customOperators.Add(operator3);
                    continue;
                }
                Func<CriteriaOperator, object> selector = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<CriteriaOperator, object> local2 = <>c.<>9__7_1;
                    selector = <>c.<>9__7_1 = opValue => ((OperandValue) opValue).Value;
                }
                list.AddRange(operator4.Operands.Select<CriteriaOperator, object>(selector));
            }
            return list;
        }

        public static bool GetStringContainsSubstring(string original, string subString) => 
            original.IndexOf(subString, 0, StringComparison.OrdinalIgnoreCase) != -1;

        public static CriteriaOperator IncludeMonth(string fieldName, DateTime date)
        {
            DateTime time = date.AddMonths(1);
            return (new BinaryOperator(fieldName, new DateTime(date.Year, date.Month, 1), BinaryOperatorType.GreaterOrEqual) & new BinaryOperator(fieldName, new DateTime(time.Year, time.Month, 1), BinaryOperatorType.Less));
        }

        public static CriteriaOperator IncludeMonths(string fieldName, Tuple<int, int, int> monthsRange)
        {
            DateTime time = new DateTime(monthsRange.Item1, monthsRange.Item3, 1).AddMonths(1);
            return (new BinaryOperator(fieldName, new DateTime(monthsRange.Item1, monthsRange.Item2, 1), BinaryOperatorType.GreaterOrEqual) & new BinaryOperator(fieldName, new DateTime(time.Year, time.Month, 1), BinaryOperatorType.Less));
        }

        public static CriteriaOperator IncludeYear(string fieldName, DateTime date)
        {
            DateTime time = date.AddYears(1);
            return (new BinaryOperator(fieldName, new DateTime(date.Year, 1, 1), BinaryOperatorType.GreaterOrEqual) & new BinaryOperator(fieldName, new DateTime(time.Year, 1, 1), BinaryOperatorType.Less));
        }

        public static CriteriaOperator IncludeYears(string fieldName, Tuple<int, int> yearsRange)
        {
            DateTime time = new DateTime(yearsRange.Item2, 1, 1).AddYears(1);
            return (new BinaryOperator(fieldName, new DateTime(yearsRange.Item1, 1, 1), BinaryOperatorType.GreaterOrEqual) & new BinaryOperator(fieldName, new DateTime(time.Year, 1, 1), BinaryOperatorType.Less));
        }

        private static bool IsNullOrEmptyOperator(CriteriaOperator op) => 
            (op is FunctionOperator) && (((FunctionOperator) op).OperatorType == FunctionOperatorType.IsNullOrEmpty);

        public static bool IsNullOrEmptyString(object value) => 
            (value is string) && string.IsNullOrEmpty((string) value);

        public static bool IsSupportedEditSettings(BaseEditSettings editSettings) => 
            !ExcelColumnFilterInfo.NotSupportEditSettings.Any<Type>(t => t.IsAssignableFrom(editSettings.GetType()));

        private static bool IsValidGroupOperator(GroupOperator groupOperator)
        {
            if (groupOperator == null)
            {
                return false;
            }
            if ((groupOperator.OperatorType != GroupOperatorType.Or) || (groupOperator.Operands.Count != 2))
            {
                return false;
            }
            Func<CriteriaOperator, bool> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<CriteriaOperator, bool> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = operand => IsNullOrEmptyOperator(operand);
            }
            if (groupOperator.Operands.FirstOrDefault<CriteriaOperator>(predicate) == null)
            {
                return false;
            }
            Func<CriteriaOperator, bool> func2 = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<CriteriaOperator, bool> local2 = <>c.<>9__9_1;
                func2 = <>c.<>9__9_1 = operand => operand is InOperator;
            }
            return (groupOperator.Operands.FirstOrDefault<CriteriaOperator>(func2) != null);
        }

        public static void SortComboBoxItems(object[] values, ColumnBase Column, ComboBoxEditSettings comboBoxEditSettings, Func<object, object> getItem)
        {
            if ((comboBoxEditSettings != null) && ((Column.ColumnFilterMode != DevExpress.Xpf.Grid.ColumnFilterMode.DisplayText) && !Column.IsAsyncLookup))
            {
                Array.Sort(values, new ComboBoxItemComparer(comboBoxEditSettings));
            }
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = getItem(values[i]);
            }
            if (!Column.IsAsyncLookup && ((Column.ColumnFilterMode == DevExpress.Xpf.Grid.ColumnFilterMode.DisplayText) || ((Column.GetSortMode() == ColumnSortMode.DisplayText) && (comboBoxEditSettings == null))))
            {
                Array.Sort(values, new CustomComboBoxItemComparer());
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnFilterInfoHelper.<>c <>9 = new ColumnFilterInfoHelper.<>c();
            public static Func<CriteriaOperator, bool> <>9__6_0;
            public static Func<CriteriaOperator, object> <>9__6_1;
            public static Func<CriteriaOperator, object> <>9__7_0;
            public static Func<CriteriaOperator, object> <>9__7_1;
            public static Func<CriteriaOperator, bool> <>9__9_0;
            public static Func<CriteriaOperator, bool> <>9__9_1;
            public static Func<CriteriaOperator, bool> <>9__11_0;

            internal bool <ContainsIsNullOrEmptyOperator>b__11_0(CriteriaOperator x) => 
                ColumnFilterInfoHelper.IsNullOrEmptyOperator(x);

            internal bool <GetSelectedItems>b__6_0(CriteriaOperator operand) => 
                operand is InOperator;

            internal object <GetSelectedItems>b__6_1(CriteriaOperator opValue) => 
                ((OperandValue) opValue).Value;

            internal object <GetSelectedItems>b__7_0(CriteriaOperator opValue) => 
                ((OperandValue) opValue).Value;

            internal object <GetSelectedItems>b__7_1(CriteriaOperator opValue) => 
                ((OperandValue) opValue).Value;

            internal bool <IsValidGroupOperator>b__9_0(CriteriaOperator operand) => 
                ColumnFilterInfoHelper.IsNullOrEmptyOperator(operand);

            internal bool <IsValidGroupOperator>b__9_1(CriteriaOperator operand) => 
                operand is InOperator;
        }
    }
}

