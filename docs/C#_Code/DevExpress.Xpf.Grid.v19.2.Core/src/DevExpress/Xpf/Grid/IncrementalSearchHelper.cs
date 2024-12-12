namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class IncrementalSearchHelper
    {
        private static int? CalcContinuedRowIndex(DataViewBase view, bool down)
        {
            if (view != null)
            {
                return (!down ? new int?(GetMasterRowHandle(view.DataControl)) : new int?(GetMasterRowHandle(view.DataControl) + 1));
            }
            return null;
        }

        private static int CalcStartColumnIndex(DataViewBase view, bool down) => 
            !down ? (view.VisibleColumnsCore.Count - 1) : 0;

        private static int CalcStartRowIndex(DataViewBase view, bool down) => 
            !down ? (view.DataControl.DataProviderBase.DataRowCount - 1) : 0;

        public static CriteriaOperator GetCriteriaOperator(string[] columns, FilterCondition filterCondition, string searchText, CriteriaOperatorType operatorType)
        {
            if (string.IsNullOrEmpty(searchText) || ((columns == null) || (columns.Length == 0)))
            {
                return null;
            }
            FindSearchParserResults parseResult = new FindSearchParser().Parse($""{searchText}"");
            char[] trimChars = new char[] { '"' };
            parseResult.SearchTexts[0] = parseResult.SearchTexts[0].Trim(trimChars);
            parseResult.AppendColumnFieldPrefixes();
            return DxFtsContainsHelper.Create(columns, parseResult, filterCondition);
        }

        public static int GetMasterRowHandle(DataControlBase dataControl) => 
            dataControl.GetMasterRowHandleInternal();

        public static TableTextSearchEngine.TableIndex SearchCallback(string prefix, TableTextSearchEngine.TableIndex startIndex, bool ignoreStartIndex, bool down, DataViewBase view)
        {
            if (!string.IsNullOrEmpty(prefix))
            {
                startIndex ??= new TableTextSearchEngine.TableIndex(CalcStartRowIndex(view.RootView, down), CalcStartColumnIndex(view.RootView, down), view.RootView, 0);
                TableTextSearchEngine.TableIndex result = new TableTextSearchEngine.TableIndex();
                DataViewBase dataView = view;
                int level = startIndex.Level;
                bool flag = false;
                Dictionary<int, DataViewBase> dictionary = new Dictionary<int, DataViewBase>();
                while ((level >= 0) && (startIndex.Level != 0))
                {
                    flag = true;
                    int masterRowHandle = GetMasterRowHandle(dataView.DataControl);
                    DataControlBase masterGridCore = dataView.DataControl.GetMasterGridCore();
                    dictionary.Add(level, dataView);
                    if (level != 0)
                    {
                        dataView = masterGridCore.DataView;
                    }
                    level--;
                }
                if (!flag)
                {
                    return (!SearchOnMasterView(startIndex.View, prefix, startIndex.RowIndex, startIndex.ColumnIndex, ignoreStartIndex, down, result) ? null : result);
                }
                if (SearchOnMasterView(view, prefix, startIndex.RowIndex, startIndex.ColumnIndex, ignoreStartIndex, down, result))
                {
                    return result;
                }
                DataViewBase base3 = null;
                for (int i = startIndex.Level; i >= 0; i--)
                {
                    bool flag4;
                    int? nullable = CalcContinuedRowIndex(base3, down);
                    base3 = dictionary[i];
                    if (i == startIndex.Level)
                    {
                        flag4 = SearchOnMasterView(base3, prefix, startIndex.RowIndex, startIndex.ColumnIndex, ignoreStartIndex, down, result);
                    }
                    else
                    {
                        int? nullable2 = nullable;
                        flag4 = SearchOnMasterView(base3, prefix, (nullable2 != null) ? nullable2.GetValueOrDefault() : CalcStartRowIndex(base3, down), CalcStartColumnIndex(base3, down), ignoreStartIndex, down, result);
                    }
                    if (flag4)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private static bool SearchOnMasterView(DataViewBase view, string prefix, int startIndex, int startColumnIndex, bool ignoreStartIndex, bool down, TableTextSearchEngine.TableIndex result)
        {
            string[] incrementalSearchColumns = view.IncrementalSearchColumns;
            bool flag = view.DataControl.DetailDescriptorCore != null;
            if (!down)
            {
                int num5 = ignoreStartIndex ? (startColumnIndex - 1) : startColumnIndex;
                int rowHandle = startIndex;
                while (rowHandle >= 0)
                {
                    if (flag && ((rowHandle != startIndex) && view.DataControl.MasterDetailProvider.IsMasterRowExpanded(rowHandle, null)))
                    {
                        DataViewBase base5 = ((MasterDetailProvider) view.DataControl.MasterDetailProvider).GetRowDetailInfo(rowHandle).FindFirstDetailView();
                        if (base5 == null)
                        {
                            return false;
                        }
                        if (SearchOnMasterView(base5, prefix, CalcStartRowIndex(base5, down), CalcStartColumnIndex(base5, down), false, down, result))
                        {
                            return true;
                        }
                        result.Level--;
                    }
                    int num7 = num5;
                    while (true)
                    {
                        if ((num7 < 0) || (incrementalSearchColumns.Length == 0))
                        {
                            num5 = CalcStartColumnIndex(view, false);
                            rowHandle--;
                            break;
                        }
                        if (incrementalSearchColumns.Contains<string>(view.VisibleColumnsCore[num7].FieldName))
                        {
                            string cellDisplayText = view.DataControl.GetCellDisplayText(rowHandle, view.VisibleColumnsCore[num7].FieldName);
                            if (!string.IsNullOrEmpty(cellDisplayText) && cellDisplayText.ToLower().StartsWith(prefix.ToLower()))
                            {
                                result.RowIndex = rowHandle;
                                result.ColumnIndex = num7;
                                result.View = view;
                                return true;
                            }
                        }
                        num7--;
                    }
                }
            }
            else
            {
                int num = ignoreStartIndex ? (startColumnIndex + 1) : startColumnIndex;
                int rowHandle = startIndex;
                while (rowHandle < view.DataControl.DataProviderBase.DataRowCount)
                {
                    int num3 = num;
                    while (true)
                    {
                        if ((num3 >= view.VisibleColumnsCore.Count) || (incrementalSearchColumns.Length == 0))
                        {
                            if (flag && view.DataControl.MasterDetailProvider.IsMasterRowExpanded(rowHandle, null))
                            {
                                DataViewBase base3 = ((MasterDetailProvider) view.DataControl.MasterDetailProvider).GetRowDetailInfo(rowHandle).FindFirstDetailView();
                                if (base3 == null)
                                {
                                    return false;
                                }
                                result.Level++;
                                if (SearchOnMasterView(base3, prefix, CalcStartRowIndex(base3, down), CalcStartColumnIndex(base3, down), false, down, result))
                                {
                                    return true;
                                }
                                result.Level--;
                            }
                            num = CalcStartColumnIndex(view, true);
                            rowHandle++;
                            break;
                        }
                        if (incrementalSearchColumns.Contains<string>(view.VisibleColumnsCore[num3].FieldName))
                        {
                            string cellDisplayText = view.DataControl.GetCellDisplayText(rowHandle, view.VisibleColumnsCore[num3].FieldName);
                            if (!string.IsNullOrEmpty(cellDisplayText) && cellDisplayText.ToLower().StartsWith(prefix.ToLower()))
                            {
                                result.RowIndex = rowHandle;
                                result.ColumnIndex = num3;
                                result.View = view;
                                return true;
                            }
                        }
                        num3++;
                    }
                }
            }
            return false;
        }
    }
}

