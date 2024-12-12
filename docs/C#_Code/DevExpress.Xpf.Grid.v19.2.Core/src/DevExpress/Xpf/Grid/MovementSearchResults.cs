namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using DevExpress.Xpf.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal static class MovementSearchResults
    {
        public static bool CanMovementSearchResult(DataViewBase view) => 
            (view != null) && (view.IsRootView && ((view.SearchControlCore != null) && ((view.SearchControlCore.FilterCriteria != null) && ((view.DataControl != null) && (view.NavigationStyle != GridViewNavigationStyle.None)))));

        private static bool CheckFilter(DataViewBase view, string original, string cellValue, ColumnBase column)
        {
            bool flag;
            FindSearchParserResults results = new FindSearchParser().Parse(original, view.ColumnsCore.Cast<IDataColumnInfo>());
            if ((results == null) || (results.SearchTexts == null))
            {
                return false;
            }
            string[] searchTexts = results.SearchTexts;
            int index = 0;
            while (true)
            {
                if (index < searchTexts.Length)
                {
                    string str = searchTexts[index];
                    if ((cellValue.IndexOf(str, StringComparison.CurrentCultureIgnoreCase) >= 0) || (cellValue.IndexOfInvariantCultureIgnoreCase(str) >= 0))
                    {
                        return true;
                    }
                    index++;
                    continue;
                }
                using (List<FindSearchField>.Enumerator enumerator = results.Fields.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            FindSearchField current = enumerator.Current;
                            if (current.Column.Column.FieldName != column.FieldName)
                            {
                                continue;
                            }
                            string[] values = current.Values;
                            int num2 = 0;
                            while (true)
                            {
                                if (num2 >= values.Length)
                                {
                                    break;
                                }
                                string str2 = values[num2];
                                if ((cellValue.IndexOf(str2, StringComparison.CurrentCultureIgnoreCase) < 0) && (cellValue.IndexOfInvariantCultureIgnoreCase(str2) < 0))
                                {
                                    num2++;
                                    continue;
                                }
                                return true;
                            }
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                break;
            }
            return flag;
        }

        private static bool CheckSearchPanelColumnProvider(DataViewBase view) => 
            (view != null) && ((view.SearchPanelColumnProvider != null) && ((view.SearchPanelColumnProvider.Columns != null) && (view.SearchPanelColumnProvider.Columns.Count > 0)));

        private static int GetCorrectColumnIndex(DataViewBase view, ColumnBase column, int defaultVal)
        {
            for (int i = 0; i < view.SearchPanelColumnProvider.Columns.Count; i++)
            {
                if (view.SearchPanelColumnProvider.Columns[i] == column)
                {
                    return i;
                }
            }
            return defaultVal;
        }

        public static bool MoveSearchResult(DataViewBase view, bool? next, bool useCurrentPosition = false)
        {
            if (!CanMovementSearchResult(view))
            {
                return false;
            }
            int focusedRowHandle = view.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                focusedRowHandle = 0;
            }
            return ((!view.IsEditing || view.CommitEditing()) ? SearchResultMove(view, focusedRowHandle, next, useCurrentPosition) : true);
        }

        private static bool MoveWithSearchResult(DataViewBase view, bool next, bool useCurrentPosition = false)
        {
            GridSearchControlBase searchControl = view.SearchControl as GridSearchControlBase;
            if ((view.DataControl == null) || ((searchControl.SearchInfo == null) || (searchControl.SearchInfo.Count == 0)))
            {
                return false;
            }
            List<int> source = new List<int>(searchControl.SearchInfo);
            int num = ((searchControl.ResultIndex - 1) > 0) ? (searchControl.ResultIndex - 1) : 0;
            int firstDataFocusedRowHandle = view.GetFirstDataFocusedRowHandle();
            if (next)
            {
                bool flag = (firstDataFocusedRowHandle > source[num]) || !view.DataControl.IsGroupRowHandleCore(view.FocusedRowHandle);
                view.FocusedRowHandle = !((!useCurrentPosition && (source[num] <= firstDataFocusedRowHandle)) & flag) ? source[num] : ((source.Count > searchControl.ResultIndex) ? source[searchControl.ResultIndex] : source.First<int>());
                return true;
            }
            bool flag2 = (firstDataFocusedRowHandle < source[num]) || !view.DataControl.IsGroupRowHandleCore(view.FocusedRowHandle);
            view.FocusedRowHandle = !((!useCurrentPosition && (source[num] >= firstDataFocusedRowHandle)) & flag2) ? source[num] : (((searchControl.ResultIndex - 2) >= 0) ? source[searchControl.ResultIndex - 2] : source.Last<int>());
            return true;
        }

        private static bool NextMove(DataViewBase view, int? focusedRowIndex, Func<int, bool> fitFunc, bool next, bool useCurrentPosition = false)
        {
            int? nullable;
            if ((view.SearchControl is GridSearchControlBase) && view.ActualShowSearchPanelResultInfo)
            {
                return MoveWithSearchResult(view, true, useCurrentPosition);
            }
            bool flag = focusedRowIndex == null;
            if (focusedRowIndex == null)
            {
                focusedRowIndex = 0;
            }
            int dataRowCount = view.DataProviderBase.DataRowCount;
            int num2 = ((view.DataControl.CurrentColumn == null) || flag) ? 0 : GetCorrectColumnIndex(view, view.DataControl.CurrentColumn, 0);
            if ((num2 != 0) && (view.DataControl.CurrentColumn != null))
            {
                string fieldName = view.DataControl.CurrentColumn.FieldName;
                for (int j = 0; j < view.SearchPanelColumnProvider.Columns.Count; j++)
                {
                    ColumnBase base3 = (ColumnBase) view.SearchPanelColumnProvider.Columns[j];
                    if (base3.FieldName == fieldName)
                    {
                        num2 = j;
                        break;
                    }
                    num2 = 0;
                }
            }
            int startIndex = 0;
            if (!flag && !useCurrentPosition)
            {
                if ((!next || ((num2 + 1) < (view.SearchPanelColumnProvider.Columns.Count - 1))) && (view.NavigationStyle != GridViewNavigationStyle.Row))
                {
                    startIndex = next ? Math.Min((int) (num2 + 1), (int) (view.SearchPanelColumnProvider.Columns.Count - 1)) : num2;
                }
                else
                {
                    int? nullable1;
                    startIndex = 0;
                    nullable = focusedRowIndex;
                    if (nullable != null)
                    {
                        nullable1 = new int?(nullable.GetValueOrDefault() + 1);
                    }
                    else
                    {
                        nullable1 = null;
                    }
                    focusedRowIndex = nullable1;
                }
            }
            int endIndex = CheckSearchPanelColumnProvider(view) ? (view.SearchPanelColumnProvider.Columns.Count - 1) : 0;
            DataProviderBase dataProviderBase = view.DataProviderBase;
            for (int i = focusedRowIndex.Value; i < dataRowCount; i++)
            {
                if (!dataProviderBase.IsFilteredByRowHandle(i) && fitFunc(i))
                {
                    nullable = focusedRowIndex;
                    if ((i == nullable.GetValueOrDefault()) ? (nullable == null) : true)
                    {
                        startIndex = 0;
                    }
                    if (SearchIterateColumn(view, startIndex, endIndex, i))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool PrevMove(DataViewBase view, int? focusedRowIndex, Func<int, bool> fitFunc, bool useCurrentPosition = false)
        {
            int? nullable;
            if ((view.SearchControl is GridSearchControlBase) && view.ActualShowSearchPanelResultInfo)
            {
                return MoveWithSearchResult(view, false, useCurrentPosition);
            }
            bool flag = focusedRowIndex == null;
            if (focusedRowIndex == null)
            {
                focusedRowIndex = new int?(view.DataProviderBase.DataRowCount - 1);
            }
            int num = 0;
            int num2 = ((view.DataControl.CurrentColumn == null) || flag) ? (view.SearchPanelColumnProvider.Columns.Count - 1) : GetCorrectColumnIndex(view, view.DataControl.CurrentColumn, view.SearchPanelColumnProvider.Columns.Count - 1);
            if ((num2 != (view.SearchPanelColumnProvider.Columns.Count - 1)) && (view.DataControl.CurrentColumn != null))
            {
                string fieldName = view.DataControl.CurrentColumn.FieldName;
                for (int j = 0; j < view.SearchPanelColumnProvider.Columns.Count; j++)
                {
                    ColumnBase base3 = (ColumnBase) view.SearchPanelColumnProvider.Columns[j];
                    if (base3.FieldName == fieldName)
                    {
                        num2 = j;
                        break;
                    }
                    num2 = view.SearchPanelColumnProvider.Columns.Count - 1;
                }
            }
            if ((num2 - 1) < 0)
            {
                nullable = focusedRowIndex;
                if ((num == nullable.GetValueOrDefault()) ? (nullable != null) : false)
                {
                    return false;
                }
            }
            int startIndex = 0;
            if (flag || useCurrentPosition)
            {
                startIndex = CheckSearchPanelColumnProvider(view) ? (view.SearchPanelColumnProvider.Columns.Count - 1) : 0;
            }
            else if (((num2 - 1) >= 0) && (view.NavigationStyle != GridViewNavigationStyle.Row))
            {
                startIndex = Math.Max(num2 - 1, 0);
            }
            else
            {
                int? nullable1;
                startIndex = view.SearchPanelColumnProvider.Columns.Count - 1;
                nullable = focusedRowIndex;
                if (nullable != null)
                {
                    nullable1 = new int?(nullable.GetValueOrDefault() - 1);
                }
                else
                {
                    nullable1 = null;
                }
                focusedRowIndex = nullable1;
            }
            int endIndex = 0;
            DataProviderBase dataProviderBase = view.DataProviderBase;
            for (int i = focusedRowIndex.Value; i >= num; i--)
            {
                if (!dataProviderBase.IsFilteredByRowHandle(i) && fitFunc(i))
                {
                    nullable = focusedRowIndex;
                    if ((i == nullable.GetValueOrDefault()) ? (nullable == null) : true)
                    {
                        startIndex = view.SearchPanelColumnProvider.Columns.Count - 1;
                    }
                    if (SearchIterateColumn(view, startIndex, endIndex, i))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool SearchIterateColumn(DataViewBase view, int startIndex, int endIndex, int rowHandle)
        {
            if ((startIndex >= 0) && ((endIndex >= 0) && ((endIndex <= view.SearchPanelColumnProvider.Columns.Count) && (startIndex <= view.SearchPanelColumnProvider.Columns.Count))))
            {
                if (endIndex >= startIndex)
                {
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        if (SearchOneIterateColumn(view, i, rowHandle))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    for (int i = startIndex; i >= endIndex; i--)
                    {
                        if (SearchOneIterateColumn(view, i, rowHandle))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool SearchOneIterateColumn(DataViewBase view, int i, int rowHanlde)
        {
            if ((view.SearchPanelColumnProvider.Columns == null) || ((view.SearchPanelColumnProvider.Columns.Count == 0) || (view.SearchPanelColumnProvider.Columns.Count <= i)))
            {
                return false;
            }
            object cellValue = view.DataControl.GetCellValue(rowHanlde, ((ColumnBase) view.SearchPanelColumnProvider.Columns[i]).FieldName);
            if (!CheckFilter(view, view.SearchString, (cellValue != null) ? cellValue.ToString() : string.Empty, (ColumnBase) view.SearchPanelColumnProvider.Columns[i]))
            {
                return false;
            }
            view.DataControl.CurrentColumn = (ColumnBase) view.SearchPanelColumnProvider.Columns[i];
            view.FocusedRowHandle = rowHanlde;
            return true;
        }

        private static bool SearchResultMove(DataViewBase view, int indexFocusedRow, bool? next, bool useCurrentPosition = false)
        {
            Func<int, bool> fitFunc = view.CreateFilterFitPredicate();
            if (fitFunc != null)
            {
                bool? nullable = next;
                bool flag = false;
                if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
                {
                    if (PrevMove(view, new int?(indexFocusedRow), fitFunc, useCurrentPosition))
                    {
                        return true;
                    }
                }
                else if (NextMove(view, new int?(indexFocusedRow), fitFunc, next != null, useCurrentPosition))
                {
                    return true;
                }
                if (next != null)
                {
                    int? nullable2;
                    if (next.Value)
                    {
                        if (CheckSearchPanelColumnProvider(view) && ((view.FocusedRowHandle != 0) || !ReferenceEquals(view.DataControl.CurrentColumn, view.SearchPanelColumnProvider.Columns[0])))
                        {
                            nullable2 = null;
                            return NextMove(view, nullable2, fitFunc, next.Value, useCurrentPosition);
                        }
                    }
                    else if (CheckSearchPanelColumnProvider(view) && ((view.FocusedRowHandle != (view.DataProviderBase.DataRowCount - 1)) || !ReferenceEquals(view.DataControl.CurrentColumn, view.SearchPanelColumnProvider.Columns[view.SearchPanelColumnProvider.Columns.Count - 1])))
                    {
                        nullable2 = null;
                        return PrevMove(view, nullable2, fitFunc, useCurrentPosition);
                    }
                }
            }
            return false;
        }
    }
}

