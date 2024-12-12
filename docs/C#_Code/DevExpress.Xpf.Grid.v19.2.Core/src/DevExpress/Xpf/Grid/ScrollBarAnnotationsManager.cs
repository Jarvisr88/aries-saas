namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ScrollBarAnnotationsManager
    {
        private readonly SortedDictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> InfoAnnotations = new SortedDictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>>();
        private readonly ScrollBarAnnotationMode[] FullCrawlAnnotation = new ScrollBarAnnotationMode[] { ScrollBarAnnotationMode.InvalidCells, ScrollBarAnnotationMode.InvalidRows, ScrollBarAnnotationMode.Custom };
        private ScrollBarAnnotationsAppearance scrollBarAnnotationsAppearanceDefault;
        internal readonly Locker UpdateLock = new Locker();
        public readonly ITableView TableView;
        private readonly bool IsDesignTime;

        public ScrollBarAnnotationsManager(ITableView tableView)
        {
            this.TableView = tableView;
            this.GridLoaded = false;
            this.IsDesignTime = this.View.IsDesignTime;
        }

        private void AddedOrRemoveRow(List<ScrollBarAnnotationRowInfo> value, int visibleIndex, ScrollBarAnnotationRowInfo info, bool inc)
        {
            if (((value == null) || (value.Count == 0)) & inc)
            {
                if ((info != null) && (value != null))
                {
                    value.Add(info);
                }
            }
            else
            {
                int index = this.BinarySearch(value, visibleIndex);
                int? endIndex = null;
                this.MoveIndexes(inc, (index >= 0) ? index : ~index, value, endIndex);
                if (inc)
                {
                    if (info != null)
                    {
                        value.Add(info);
                    }
                }
                else if (index >= 0)
                {
                    value.RemoveAt(index);
                }
                value.Sort();
            }
        }

        private void AnnotationOneIterate(DataViewBase view, int visibleIndex, bool searchNeed, bool cellErrorNeed, bool rowsErrorNeed, bool customNeed, Func<int, bool> fitSearch, ScrollBarAnnotationInfo searchInfo, ScrollBarAnnotationInfo rowErrorhInfo, ScrollBarAnnotationInfo cellErrorInfo, List<ScrollBarAnnotationRowInfo> search, List<ScrollBarAnnotationRowInfo> errorsCells, List<ScrollBarAnnotationRowInfo> errorsRows, List<ScrollBarAnnotationRowInfo> customRows)
        {
            int correctVisibleIndexByVisibleIndex = this.GetCorrectVisibleIndexByVisibleIndex(visibleIndex, view);
            if (correctVisibleIndexByVisibleIndex >= 0)
            {
                int rowHandleByVisibleIndexCore = view.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                if (searchNeed && this.View.IsSearchResult(fitSearch, rowHandleByVisibleIndexCore, correctVisibleIndexByVisibleIndex))
                {
                    search.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, searchInfo));
                }
                if (cellErrorNeed | rowsErrorNeed)
                {
                    if (cellErrorNeed & rowsErrorNeed)
                    {
                        ErrorsWatchMode? nullable = view.IsValidRowByRowHandle(rowHandleByVisibleIndexCore, ErrorsWatchMode.All, false);
                        if (nullable != null)
                        {
                            if (((ErrorsWatchMode) nullable.Value) == ErrorsWatchMode.Rows)
                            {
                                errorsRows.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, rowErrorhInfo));
                            }
                            else
                            {
                                errorsCells.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, cellErrorInfo));
                            }
                        }
                    }
                    else if (cellErrorNeed)
                    {
                        if (view.IsValidRowByRowHandle(rowHandleByVisibleIndexCore, ErrorsWatchMode.Cells, false) != null)
                        {
                            errorsCells.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, cellErrorInfo));
                        }
                    }
                    else if (rowsErrorNeed && (view.IsValidRowByRowHandle(rowHandleByVisibleIndexCore, ErrorsWatchMode.Rows, true) != null))
                    {
                        errorsRows.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, rowErrorhInfo));
                    }
                }
                if (customNeed)
                {
                    ScrollBarCustomRowAnnotationEventArgs e = new ScrollBarCustomRowAnnotationEventArgs(rowHandleByVisibleIndexCore, view.DataControl.GetRow(rowHandleByVisibleIndexCore));
                    ((ITableView) view).RaiseScrollBarCustomRowAnnotation(e);
                    if (e.ScrollBarAnnotationInfo != null)
                    {
                        customRows.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, e.ScrollBarAnnotationInfo));
                    }
                }
            }
        }

        private void AnnotationOrderCorrect(bool? direct, KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> item, int index, Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> elementAnnotations)
        {
            if (direct == null)
            {
                if (elementAnnotations.ContainsKey(item.Key))
                {
                    this.ChangeRow(item.Key, index, elementAnnotations[item.Key].FirstOrDefault<ScrollBarAnnotationRowInfo>());
                }
            }
            else if (!direct.Value)
            {
                this.AddedOrRemoveRow(item.Value, index, null, direct.Value);
            }
            else if (elementAnnotations.ContainsKey(item.Key))
            {
                this.AddedOrRemoveRow(item.Value, index, elementAnnotations[item.Key].FirstOrDefault<ScrollBarAnnotationRowInfo>(), direct.Value);
            }
        }

        private int BinarySearch(List<ScrollBarAnnotationRowInfo> list, int item)
        {
            Func<ScrollBarAnnotationRowInfo, int> selector = <>c.<>9__56_0;
            if (<>c.<>9__56_0 == null)
            {
                Func<ScrollBarAnnotationRowInfo, int> local1 = <>c.<>9__56_0;
                selector = <>c.<>9__56_0 = x => x.RowIndex;
            }
            return list.Select<ScrollBarAnnotationRowInfo, int>(selector).ToList<int>().BinarySearch(item);
        }

        private void ChangeOrderAnnotations(int rowHandle, bool? direct)
        {
            int correctVisibleIndexByRowHandle;
            bool? nullable;
            bool flag;
            if (this.GetCorrectVisibleIndexByRowHandle(rowHandle) < 0)
            {
                nullable = direct;
                flag = false;
                if (!((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false) || (rowHandle != this.View.DataProviderBase.DataRowCount))
                {
                    return;
                }
                correctVisibleIndexByRowHandle = this.GetCorrectVisibleIndexByRowHandle(rowHandle - 1);
                if (correctVisibleIndexByRowHandle < 0)
                {
                    return;
                }
                correctVisibleIndexByRowHandle++;
            }
            Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> elementAnnotations = null;
            nullable = direct;
            flag = false;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable == null) : true)
            {
                elementAnnotations = this.GetFullCrawlIndexes(this.View, new int?(correctVisibleIndexByRowHandle));
                if (this.NeedSelected(this.View) && (this.GetSelectedAnnotation(this.View) != null))
                {
                    elementAnnotations.Add(ScrollBarAnnotationMode.Selected, this.GetSelectedAnnotation(this.View).ToList<ScrollBarAnnotationRowInfo>());
                }
            }
            foreach (KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> pair in this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations)
            {
                ScrollBarAnnotationMode key = pair.Key;
                if (key > ScrollBarAnnotationMode.SearchResult)
                {
                    if (key != ScrollBarAnnotationMode.Custom)
                    {
                        if ((key != ScrollBarAnnotationMode.FocusedRow) && (key == ScrollBarAnnotationMode.All))
                        {
                        }
                        continue;
                    }
                }
                else
                {
                    switch (key)
                    {
                        case ScrollBarAnnotationMode.InvalidRows:
                        case ScrollBarAnnotationMode.InvalidCells:
                        case ScrollBarAnnotationMode.Selected:
                            break;

                        case (ScrollBarAnnotationMode.InvalidCells | ScrollBarAnnotationMode.InvalidRows):
                        {
                            continue;
                        }
                        default:
                        {
                            if (key != ScrollBarAnnotationMode.SearchResult)
                            {
                                continue;
                            }
                            if (elementAnnotations == null)
                            {
                                continue;
                            }
                            ScrollBarAnnotationRowInfo info = elementAnnotations.ContainsKey(pair.Key) ? elementAnnotations[pair.Key].FirstOrDefault<ScrollBarAnnotationRowInfo>() : null;
                            if ((info == null) || (info.RowIndex == info.EndRowIndex))
                            {
                                this.AnnotationOrderCorrect(direct, pair, correctVisibleIndexByRowHandle, elementAnnotations);
                                continue;
                            }
                            if ((elementAnnotations != null) && (elementAnnotations.ContainsKey(ScrollBarAnnotationMode.SearchResult) && (elementAnnotations[ScrollBarAnnotationMode.SearchResult].FirstOrDefault<ScrollBarAnnotationRowInfo>() != null)))
                            {
                                continue;
                            }
                            pair.Value.Clear();
                            pair.Value.AddRange(new List<ScrollBarAnnotationRowInfo>(this.GetSearchAnnotation(this.View)));
                            continue;
                        }
                    }
                }
                this.AnnotationOrderCorrect(direct, pair, correctVisibleIndexByRowHandle, elementAnnotations);
            }
            if (elementAnnotations != null)
            {
                foreach (KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> pair2 in elementAnnotations)
                {
                    if ((pair2.Value.FirstOrDefault<ScrollBarAnnotationRowInfo>() != null) && !this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations.ContainsKey(pair2.Key))
                    {
                        this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations.Add(pair2.Key, pair2.Value);
                    }
                }
            }
            this.RootTableView.ScrollBarAnnotationInfoRangeChanged();
        }

        private void ChangeRow(ScrollBarAnnotationMode key, int visibleIndex, ScrollBarAnnotationRowInfo info)
        {
            List<ScrollBarAnnotationRowInfo> list = new List<ScrollBarAnnotationRowInfo>();
            if (this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations.ContainsKey(key))
            {
                list = this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations[key];
                if (list == null)
                {
                    throw new Exception();
                }
                if (list.Count == 0)
                {
                    if (info != null)
                    {
                        list.Add(info);
                    }
                }
                else
                {
                    int index = this.BinarySearch(list, visibleIndex);
                    if (index < 0)
                    {
                        if (info != null)
                        {
                            list.Add(info);
                            list.Sort();
                        }
                    }
                    else if (info != null)
                    {
                        list[index] = info;
                    }
                    else
                    {
                        list.RemoveAt(index);
                    }
                }
            }
        }

        private void CheckSource()
        {
            if (((this.View != null) && ((this.View.DataProviderBase != null) && this.View.DataProviderBase.IsVirtualSource)) && (this.TableView.ScrollBarAnnotationMode != null))
            {
                ScrollBarAnnotationMode? scrollBarAnnotationMode = this.TableView.ScrollBarAnnotationMode;
                ScrollBarAnnotationMode none = ScrollBarAnnotationMode.None;
                if ((((ScrollBarAnnotationMode) scrollBarAnnotationMode.GetValueOrDefault()) == none) ? (scrollBarAnnotationMode == null) : true)
                {
                    throw new NotSupportedException("The GridControl bound to the Virtual data source does not support scrollbar annotations.");
                }
            }
        }

        private bool CheckStartGeneration(ScrollBarAnnotationMode scrollAnnotation, DataViewBase view)
        {
            if ((scrollAnnotation != ScrollBarAnnotationMode.None) && ((view.DataControl != null) && (view.DataControl.VisibleRowCount != 0)))
            {
                return true;
            }
            bool flag = this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations.Count > 0;
            this.UpdateScrollAnnotationInfo(ScrollBarAnnotationMode.None, this.RootView, null, null);
            if (flag)
            {
                this.RootTableView.ScrollBarAnnotationInfoRangeChanged();
            }
            return false;
        }

        private Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> CreateRangeDictionary() => 
            new Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> { 
                { 
                    ScrollBarAnnotationMode.Custom,
                    new List<ScrollBarAnnotationRowInfo>()
                },
                { 
                    ScrollBarAnnotationMode.InvalidCells,
                    new List<ScrollBarAnnotationRowInfo>()
                },
                { 
                    ScrollBarAnnotationMode.InvalidRows,
                    new List<ScrollBarAnnotationRowInfo>()
                },
                { 
                    ScrollBarAnnotationMode.SearchResult,
                    new List<ScrollBarAnnotationRowInfo>()
                },
                { 
                    ScrollBarAnnotationMode.FocusedRow,
                    new List<ScrollBarAnnotationRowInfo>()
                },
                { 
                    ScrollBarAnnotationMode.Selected,
                    new List<ScrollBarAnnotationRowInfo>()
                }
            };

        private void FullCrawlIndexesSyncDictionary(Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> source, Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> added)
        {
            source[ScrollBarAnnotationMode.SearchResult].AddRange(added[ScrollBarAnnotationMode.SearchResult]);
            source[ScrollBarAnnotationMode.InvalidCells].AddRange(added[ScrollBarAnnotationMode.InvalidCells]);
            source[ScrollBarAnnotationMode.InvalidRows].AddRange(added[ScrollBarAnnotationMode.InvalidRows]);
            source[ScrollBarAnnotationMode.Custom].AddRange(added[ScrollBarAnnotationMode.Custom]);
        }

        private IEnumerable<ScrollBarAnnotationRowInfo> GenerationAnnotationsIfNeed(ScrollBarAnnotationMode instanceMode, ScrollBarAnnotationMode scrollAnnotation, DataViewBase view) => 
            (!((ITableView) view).ScrollBarAnnotationModeActual.HasFlag(instanceMode) || !scrollAnnotation.HasFlag(instanceMode)) ? null : this.UpdateScrollAnnotationInfo(instanceMode, view, null, null);

        internal ScrollBarAnnotationInfo GetActualAnnotationInfo(ScrollBarAnnotationMode mode)
        {
            ScrollBarAnnotationInfo info = null;
            switch (mode)
            {
                case ScrollBarAnnotationMode.InvalidRows:
                    info = ((this.RootTableView.ScrollBarAnnotationsAppearance == null) || (this.RootTableView.ScrollBarAnnotationsAppearance.InvalidRows == null)) ? this.ScrollBarAnnotationsAppearanceDefault.InvalidRows : this.RootTableView.ScrollBarAnnotationsAppearance.InvalidRows;
                    break;

                case ScrollBarAnnotationMode.InvalidCells:
                    info = ((this.RootTableView.ScrollBarAnnotationsAppearance == null) || (this.RootTableView.ScrollBarAnnotationsAppearance.InvalidCells == null)) ? this.ScrollBarAnnotationsAppearanceDefault.InvalidCells : this.RootTableView.ScrollBarAnnotationsAppearance.InvalidCells;
                    break;

                case (ScrollBarAnnotationMode.InvalidCells | ScrollBarAnnotationMode.InvalidRows):
                    break;

                case ScrollBarAnnotationMode.Selected:
                    info = ((this.RootTableView.ScrollBarAnnotationsAppearance == null) || (this.RootTableView.ScrollBarAnnotationsAppearance.Selected == null)) ? this.ScrollBarAnnotationsAppearanceDefault.Selected : this.RootTableView.ScrollBarAnnotationsAppearance.Selected;
                    break;

                default:
                    if (mode == ScrollBarAnnotationMode.SearchResult)
                    {
                        info = ((this.RootTableView.ScrollBarAnnotationsAppearance == null) || (this.RootTableView.ScrollBarAnnotationsAppearance.SearchResult == null)) ? this.ScrollBarAnnotationsAppearanceDefault.SearchResult : this.RootTableView.ScrollBarAnnotationsAppearance.SearchResult;
                    }
                    else if (mode == ScrollBarAnnotationMode.FocusedRow)
                    {
                        info = ((this.RootTableView.ScrollBarAnnotationsAppearance == null) || (this.RootTableView.ScrollBarAnnotationsAppearance.FocusedRow == null)) ? this.ScrollBarAnnotationsAppearanceDefault.FocusedRow : this.RootTableView.ScrollBarAnnotationsAppearance.FocusedRow;
                    }
                    break;
            }
            return info;
        }

        private int GetCorrectVisibleIndexByRowHandle(int rowHandle) => 
            this.GetCorrectVisibleIndexByRowHandle(rowHandle, this.View);

        private int GetCorrectVisibleIndexByRowHandle(int rowHandle, DataViewBase view) => 
            this.HasDetailRootViews ? view.DataControl.FindFirstInnerChildScrollIndex(view.DataControl.GetRowVisibleIndexByHandleCore(rowHandle)) : this.View.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);

        private int GetCorrectVisibleIndexByVisibleIndex(int visibleIndex, DataViewBase view) => 
            this.HasDetailRootViews ? view.DataControl.FindFirstInnerChildScrollIndex(visibleIndex) : visibleIndex;

        private Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> GetFullCrawlIndexes(DataViewBase view, int? index = new int?())
        {
            bool flag;
            Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> dictionary = new Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>>();
            List<ScrollBarAnnotationRowInfo> search = new List<ScrollBarAnnotationRowInfo>();
            List<ScrollBarAnnotationRowInfo> errorsCells = new List<ScrollBarAnnotationRowInfo>();
            List<ScrollBarAnnotationRowInfo> errorsRows = new List<ScrollBarAnnotationRowInfo>();
            List<ScrollBarAnnotationRowInfo> customRows = new List<ScrollBarAnnotationRowInfo>();
            Func<int, bool> fitSearch = null;
            if (this.NeedSearch(view))
            {
                fitSearch = view.CreateAnnotationFilterFitPredicate();
                if (fitSearch == null)
                {
                    flag = false;
                }
            }
            ITableView view2 = view as ITableView;
            bool cellErrorNeed = view2.ScrollBarAnnotationModeActual.HasFlag(ScrollBarAnnotationMode.InvalidCells) && !this.IsServerOrAsyncServer;
            bool rowsErrorNeed = view2.ScrollBarAnnotationModeActual.HasFlag(ScrollBarAnnotationMode.InvalidRows) && !this.IsServerOrAsyncServer;
            bool customNeed = (view2.ScrollBarAnnotationModeActual.HasFlag(ScrollBarAnnotationMode.Custom) && view2.NeedCustomScrollBarAnnotation) && !this.IsServerOrAsyncServer;
            ScrollBarAnnotationInfo actualAnnotationInfo = this.GetActualAnnotationInfo(ScrollBarAnnotationMode.SearchResult);
            ScrollBarAnnotationInfo cellErrorInfo = this.GetActualAnnotationInfo(ScrollBarAnnotationMode.InvalidCells);
            ScrollBarAnnotationInfo rowErrorhInfo = this.GetActualAnnotationInfo(ScrollBarAnnotationMode.InvalidRows);
            if (index == null)
            {
                if (((cellErrorNeed | rowsErrorNeed) | customNeed) | flag)
                {
                    for (int i = 0; i < view.DataControl.VisibleRowCount; i++)
                    {
                        this.AnnotationOneIterate(view, i, flag, cellErrorNeed, rowsErrorNeed, customNeed, fitSearch, actualAnnotationInfo, rowErrorhInfo, cellErrorInfo, search, errorsCells, errorsRows, customRows);
                    }
                }
            }
            else
            {
                bool flag5 = this.RootTableView.ScrollBarAnnotationModeActual.HasFlag(ScrollBarAnnotationMode.Custom) && !this.IsServerOrAsyncServer;
                if (((cellErrorNeed | rowsErrorNeed) | flag) | flag5)
                {
                    ScrollBarAnnotationsCreatingEventArgs args = ((ITableView) view).RaiseScrollBarAnnotationsCreating();
                    if (!args.Handled)
                    {
                        this.AnnotationOneIterate(view, index.Value, flag, cellErrorNeed, rowsErrorNeed, customNeed, fitSearch, actualAnnotationInfo, rowErrorhInfo, cellErrorInfo, search, errorsCells, errorsRows, customRows);
                        if (flag5 && (args.CustomScrollBarAnnotations != null))
                        {
                            IEnumerable<ScrollBarAnnotationRowInfo> enumerable = from x in args.CustomScrollBarAnnotations
                                where this.GetCorrectVisibleIndexByRowHandle(x.RowIndex) == index.Value
                                select x;
                            if (enumerable != null)
                            {
                                customRows.AddRange(from x in enumerable select new ScrollBarAnnotationRowInfo(this.GetCorrectVisibleIndexByRowHandle(x.RowIndex), x.ScrollAnnotationInfo));
                            }
                        }
                    }
                }
            }
            dictionary.Add(ScrollBarAnnotationMode.Custom, customRows);
            dictionary.Add(ScrollBarAnnotationMode.InvalidCells, errorsCells);
            dictionary.Add(ScrollBarAnnotationMode.InvalidRows, errorsRows);
            dictionary.Add(ScrollBarAnnotationMode.SearchResult, search);
            return dictionary;
        }

        private IEnumerable<ScrollBarAnnotationRowInfo> GetSearchAnnotation(DataViewBase view)
        {
            Func<int, bool> fitSearch = view.CreateAnnotationFilterFitPredicate();
            if (fitSearch == null)
            {
                return null;
            }
            List<ScrollBarAnnotationRowInfo> list = new List<ScrollBarAnnotationRowInfo>();
            ScrollBarAnnotationInfo actualAnnotationInfo = this.GetActualAnnotationInfo(ScrollBarAnnotationMode.SearchResult);
            GridSearchControlBase searchControl = view.SearchControl as GridSearchControlBase;
            if ((view.CanStartIncrementalSearch || ((searchControl == null) || (searchControl.SearchInfo == null))) || (searchControl.SearchInfo.Count <= 0))
            {
                for (int i = 0; i < view.DataControl.VisibleRowCount; i++)
                {
                    int rowHandleByVisibleIndexCore = view.DataControl.GetRowHandleByVisibleIndexCore(i);
                    if (this.View.IsSearchResult(fitSearch, rowHandleByVisibleIndexCore, i))
                    {
                        list.Add(new ScrollBarAnnotationRowInfo(this.GetCorrectVisibleIndexByRowHandle(rowHandleByVisibleIndexCore, view), actualAnnotationInfo));
                    }
                }
            }
            else
            {
                foreach (int num in searchControl.SearchInfo)
                {
                    if (view.DataProviderBase.IsRowVisible(num))
                    {
                        list.Add(new ScrollBarAnnotationRowInfo(this.GetCorrectVisibleIndexByRowHandle(num, view), actualAnnotationInfo));
                    }
                }
            }
            if (list.Count != view.DataControl.VisibleRowCount)
            {
                return ((list.Count > 0) ? list : null);
            }
            List<ScrollBarAnnotationRowInfo> list1 = new List<ScrollBarAnnotationRowInfo>();
            list1.Add(new ScrollBarAnnotationRowInfo(0, view.DataControl.VisibleRowCount - 1, actualAnnotationInfo));
            return list1;
        }

        private IEnumerable<ScrollBarAnnotationRowInfo> GetSelectedAnnotation(DataViewBase view)
        {
            if (view.DataControl.SelectionMode == MultiSelectMode.None)
            {
                return null;
            }
            ScrollBarAnnotationInfo actualAnnotationInfo = this.GetActualAnnotationInfo(ScrollBarAnnotationMode.Selected);
            if ((view.AllItemsSelected != null) && (view.AllItemsSelected.Value && !this.HasDetailRootViews))
            {
                int correctVisibleIndexByVisibleIndex = this.GetCorrectVisibleIndexByVisibleIndex(0, view);
                int end = this.GetCorrectVisibleIndexByVisibleIndex(view.DataControl.VisibleRowCount - 1, view);
                if ((correctVisibleIndexByVisibleIndex < 0) || (end < 0))
                {
                    return null;
                }
                List<ScrollBarAnnotationRowInfo> list1 = new List<ScrollBarAnnotationRowInfo>();
                list1.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByVisibleIndex, end, actualAnnotationInfo));
                return list1;
            }
            int[] selectedRowHandles = view.DataControl.GetSelectedRowHandles();
            List<ScrollBarAnnotationRowInfo> list = null;
            if (selectedRowHandles != null)
            {
                list = new List<ScrollBarAnnotationRowInfo>();
                int[] numArray2 = selectedRowHandles;
                int index = 0;
                while (true)
                {
                    if (index >= numArray2.Length)
                    {
                        if (list.Count == 0)
                        {
                            list = null;
                        }
                        break;
                    }
                    int rowHandle = numArray2[index];
                    int correctVisibleIndexByRowHandle = this.GetCorrectVisibleIndexByRowHandle(rowHandle, view);
                    if (correctVisibleIndexByRowHandle >= 0)
                    {
                        list.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByRowHandle, actualAnnotationInfo));
                    }
                    index++;
                }
            }
            return list;
        }

        private bool IsLock() => 
            this.View.ColumnsCore.IsLockUpdate || (this.UpdateLock.IsLocked || ((this.View.DataProviderBase != null) && this.View.DataProviderBase.IsUpdateLocked));

        private void MoveIndexes(bool inc, int index, List<ScrollBarAnnotationRowInfo> value, int? endIndex = new int?())
        {
            index = Math.Max(index, 0);
            int num = (endIndex != null) ? Math.Min(endIndex.Value, value.Count) : value.Count;
            for (int i = index; i < num; i++)
            {
                int newIndex = inc ? (value[i].RowIndex + 1) : (value[i].RowIndex - 1);
                value[i].ChangeIndex(newIndex);
            }
        }

        private void MoveItem(int startHandle, int endHandle)
        {
            int correctVisibleIndexByRowHandle = this.GetCorrectVisibleIndexByRowHandle(startHandle);
            int endIndex = this.GetCorrectVisibleIndexByRowHandle(endHandle);
            if ((correctVisibleIndexByRowHandle >= 0) && (endIndex >= 0))
            {
                foreach (KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> pair in this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations)
                {
                    ScrollBarAnnotationMode key = pair.Key;
                    if (key > ScrollBarAnnotationMode.SearchResult)
                    {
                        if (key != ScrollBarAnnotationMode.Custom)
                        {
                            if ((key != ScrollBarAnnotationMode.FocusedRow) && (key == ScrollBarAnnotationMode.All))
                            {
                            }
                            continue;
                        }
                    }
                    else
                    {
                        switch (key)
                        {
                            case ScrollBarAnnotationMode.InvalidRows:
                            case ScrollBarAnnotationMode.InvalidCells:
                                break;

                            case (ScrollBarAnnotationMode.InvalidCells | ScrollBarAnnotationMode.InvalidRows):
                            case ScrollBarAnnotationMode.Selected:
                            {
                                continue;
                            }
                            default:
                            {
                                if (key == ScrollBarAnnotationMode.SearchResult)
                                {
                                    break;
                                }
                                continue;
                            }
                        }
                    }
                    if (this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations.ContainsKey(pair.Key))
                    {
                        this.MoveItemAnnotation(correctVisibleIndexByRowHandle, endIndex, this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations[pair.Key]);
                    }
                }
            }
        }

        private void MoveItemAnnotation(int startIndex, int endIndex, List<ScrollBarAnnotationRowInfo> list)
        {
            int index = this.BinarySearch(list, startIndex);
            ScrollBarAnnotationRowInfo info = null;
            if ((index < 0) || (list.Count <= 0))
            {
                index = ~index;
            }
            else
            {
                info = list[index];
                list.RemoveAt(index);
            }
            int? nullable = null;
            this.MoveIndexes(false, index, list, nullable);
            int num2 = this.BinarySearch(list, endIndex);
            num2 = (num2 >= 0) ? num2 : ~num2;
            nullable = null;
            this.MoveIndexes(true, num2, list, nullable);
            if (info != null)
            {
                list.Add(new ScrollBarAnnotationRowInfo(endIndex, info.ScrollAnnotationInfo));
            }
            list.Sort();
        }

        internal bool NeedSearch(DataViewBase view) => 
            this.RootTableView.ScrollBarAnnotationModeActual.HasFlag(ScrollBarAnnotationMode.SearchResult) && (!this.IsServerOrAsyncServer && ((!view.IsRootView || ((view.SearchControl == null) || ((view.SearchControl.FilterCriteria == null) || view.SearchPanelAllowFilter))) ? view.CanStartIncrementalSearch : true));

        private bool NeedSelected(DataViewBase view) => 
            (view as ITableView).ScrollBarAnnotationModeActual.HasFlag(ScrollBarAnnotationMode.Selected);

        public void ScrollBarAnnotationChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
        {
            this.CheckSource();
            if (!this.IsDesignTime && !this.IsLock())
            {
                if (oldRowHandle == null)
                {
                    this.ScrollBarAnnotationGeneration();
                }
                else if (this.CheckStartGeneration(this.RootTableView.ScrollBarAnnotationModeActual, this.View))
                {
                    if (changedType == ListChangedType.ItemDeleted)
                    {
                        newHandle = oldRowHandle.Value;
                    }
                    switch (changedType)
                    {
                        case ListChangedType.ItemAdded:
                            this.ChangeOrderAnnotations(newHandle, true);
                            return;

                        case ListChangedType.ItemDeleted:
                            this.ChangeOrderAnnotations(newHandle, false);
                            return;

                        case ListChangedType.ItemMoved:
                            this.MoveItem(oldRowHandle.Value, newHandle);
                            return;

                        case ListChangedType.ItemChanged:
                        {
                            if (oldRowHandle.Value != newHandle)
                            {
                                this.MoveItem(oldRowHandle.Value, newHandle);
                            }
                            bool? direct = null;
                            this.ChangeOrderAnnotations(newHandle, direct);
                            return;
                        }
                    }
                    this.ScrollBarAnnotationGeneration();
                }
            }
        }

        public void ScrollBarAnnotationGeneration()
        {
            this.CheckSource();
            if (!this.IsDesignTime && !this.IsLock())
            {
                ScrollBarAnnotationMode? scrollBarAnnotationMode = this.RootTableView.ScrollBarAnnotationMode;
                ScrollBarAnnotationMode all = ScrollBarAnnotationMode.All;
                if ((((ScrollBarAnnotationMode) scrollBarAnnotationMode.GetValueOrDefault()) == all) ? (scrollBarAnnotationMode == null) : true)
                {
                    this.InfoAnnotations.Clear();
                }
                this.ScrollBarAnnotationGeneration(this.TableView.ScrollBarAnnotationModeActual, true);
            }
        }

        public void ScrollBarAnnotationGeneration(ScrollBarAnnotationMode scrollAnnotation, bool fullGeneration = false)
        {
            this.CheckSource();
            if (!this.IsDesignTime && !this.IsLock())
            {
                ScrollBarAnnotationMode scrollAnnotationCurrent = ScrollBarAnnotationMode.None;
                Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> currentRange = this.CreateRangeDictionary();
                if (!this.HasDetailRootViews)
                {
                    scrollAnnotationCurrent = scrollAnnotation;
                    if (!this.CheckStartGeneration(this.TableView.ScrollBarAnnotationModeActual, this.View))
                    {
                        this.RootTableView.ScrollBarAnnotationInfoRangeChanged();
                        return;
                    }
                    this.ScrollBarAnnotationGenerationCore(scrollAnnotationCurrent, this.View, currentRange, fullGeneration);
                }
                else
                {
                    bool start = false;
                    this.RootView.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                        ITableView viewCore = dataControl.viewCore as ITableView;
                        ScrollBarAnnotationMode scrollBarAnnotationModeActual = viewCore.ScrollBarAnnotationModeActual;
                        if (this.CheckStartGeneration(scrollBarAnnotationModeActual, dataControl.DataView) || start)
                        {
                            start = true;
                            if (((viewCore.ViewBase.DataControl != null) && (viewCore.ViewBase.DataControl.VisibleRowCount != 0)) && (viewCore != null))
                            {
                                this.ScrollBarAnnotationGenerationCore(scrollBarAnnotationModeActual, viewCore.ViewBase, currentRange, fullGeneration);
                                scrollAnnotationCurrent |= scrollBarAnnotationModeActual;
                            }
                        }
                    }, null);
                    if (!start)
                    {
                        this.RootTableView.ScrollBarAnnotationInfoRangeChanged();
                        return;
                    }
                }
                if (scrollAnnotationCurrent.HasFlag(ScrollBarAnnotationMode.SearchResult))
                {
                    this.SyncInfoAnnotations(ScrollBarAnnotationMode.SearchResult, currentRange[ScrollBarAnnotationMode.SearchResult]);
                }
                if (scrollAnnotationCurrent.HasFlag(ScrollBarAnnotationMode.InvalidCells))
                {
                    this.SyncInfoAnnotations(ScrollBarAnnotationMode.InvalidCells, currentRange[ScrollBarAnnotationMode.InvalidCells]);
                }
                if (scrollAnnotationCurrent.HasFlag(ScrollBarAnnotationMode.InvalidRows))
                {
                    this.SyncInfoAnnotations(ScrollBarAnnotationMode.InvalidRows, currentRange[ScrollBarAnnotationMode.InvalidRows]);
                }
                if (scrollAnnotationCurrent.HasFlag(ScrollBarAnnotationMode.Custom))
                {
                    this.SyncInfoAnnotations(ScrollBarAnnotationMode.Custom, currentRange[ScrollBarAnnotationMode.Custom]);
                }
                if (scrollAnnotationCurrent.HasFlag(ScrollBarAnnotationMode.Selected))
                {
                    this.SyncInfoAnnotations(ScrollBarAnnotationMode.Selected, currentRange[ScrollBarAnnotationMode.Selected]);
                }
                if (scrollAnnotationCurrent.HasFlag(ScrollBarAnnotationMode.FocusedRow))
                {
                    this.SyncInfoAnnotations(ScrollBarAnnotationMode.FocusedRow, currentRange[ScrollBarAnnotationMode.FocusedRow]);
                }
                this.RootTableView.ScrollBarAnnotationInfoRangeChanged();
            }
        }

        private void ScrollBarAnnotationGenerationCore(ScrollBarAnnotationMode scrollAnnotation, DataViewBase view, Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> currentRange, bool fullGeneration)
        {
            if ((view.DataControl == null) || (view.DataControl.VisibleRowCount == 0))
            {
                this.UpdateScrollAnnotationInfo(ScrollBarAnnotationMode.None, view, null, null);
            }
            else
            {
                ScrollBarAnnotationsCreatingEventArgs args = ((ITableView) view).RaiseScrollBarAnnotationsCreating();
                if (!args.Handled)
                {
                    IEnumerable<ScrollBarAnnotationRowInfo> collection = this.GenerationAnnotationsIfNeed(ScrollBarAnnotationMode.FocusedRow, scrollAnnotation, view);
                    if (collection != null)
                    {
                        currentRange[ScrollBarAnnotationMode.FocusedRow].AddRange(collection);
                    }
                    collection = this.GenerationAnnotationsIfNeed(ScrollBarAnnotationMode.Selected, scrollAnnotation, view);
                    if (collection != null)
                    {
                        currentRange[ScrollBarAnnotationMode.Selected].AddRange(collection);
                    }
                    if (((fullGeneration && scrollAnnotation.HasFlag(ScrollBarAnnotationMode.SearchResult)) || (scrollAnnotation == ScrollBarAnnotationMode.SearchResult)) && this.NeedSearch(view))
                    {
                        collection = this.GenerationAnnotationsIfNeed(ScrollBarAnnotationMode.SearchResult, scrollAnnotation, view);
                        if (collection != null)
                        {
                            currentRange[ScrollBarAnnotationMode.SearchResult].AddRange(collection);
                        }
                    }
                    foreach (ScrollBarAnnotationMode mode in this.FullCrawlAnnotation)
                    {
                        if (scrollAnnotation.HasFlag(mode))
                        {
                            this.UpdateScrollAnnotationInfo(mode, view, args.CustomScrollBarAnnotations, currentRange);
                            return;
                        }
                    }
                }
            }
        }

        private void SyncInfoAnnotations(ScrollBarAnnotationMode annotation, List<ScrollBarAnnotationRowInfo> infoAnnotations)
        {
            SortedDictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> dictionary = this.RootTableView.ScrollBarAnnotationsManager.InfoAnnotations;
            if ((infoAnnotations == null) || (infoAnnotations.Count == 0))
            {
                dictionary.Remove(annotation);
            }
            else
            {
                infoAnnotations.Sort();
                if (dictionary.ContainsKey(annotation))
                {
                    dictionary[annotation] = infoAnnotations;
                }
                else
                {
                    dictionary.Add(annotation, infoAnnotations);
                }
            }
        }

        private IEnumerable<ScrollBarAnnotationRowInfo> UpdateScrollAnnotationInfo(ScrollBarAnnotationMode annotation, DataViewBase view, ICollection<ScrollBarAnnotationRowInfo> customCollection = null, Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> currentRange = null)
        {
            IEnumerable<ScrollBarAnnotationRowInfo> selectedAnnotation = null;
            int? nullable;
            switch (annotation)
            {
                case ScrollBarAnnotationMode.None:
                    (view as ITableView).ScrollBarAnnotationsManager.InfoAnnotations.Clear();
                    break;

                case ScrollBarAnnotationMode.InvalidRows:
                case ScrollBarAnnotationMode.InvalidCells:
                    goto TR_0014;

                case (ScrollBarAnnotationMode.InvalidCells | ScrollBarAnnotationMode.InvalidRows):
                case (ScrollBarAnnotationMode.Selected | ScrollBarAnnotationMode.InvalidRows):
                case (ScrollBarAnnotationMode.Selected | ScrollBarAnnotationMode.InvalidCells):
                case (ScrollBarAnnotationMode.Selected | ScrollBarAnnotationMode.InvalidCells | ScrollBarAnnotationMode.InvalidRows):
                    goto TR_0000;

                case ScrollBarAnnotationMode.Selected:
                    selectedAnnotation = this.GetSelectedAnnotation(view);
                    break;

                case ScrollBarAnnotationMode.SearchResult:
                    selectedAnnotation = this.GetSearchAnnotation(view);
                    break;

                default:
                    if (annotation == ScrollBarAnnotationMode.Custom)
                    {
                        goto TR_0014;
                    }
                    else if (annotation == ScrollBarAnnotationMode.FocusedRow)
                    {
                        int correctVisibleIndexByRowHandle = this.GetCorrectVisibleIndexByRowHandle(view.FocusedRowHandle, view);
                        if (correctVisibleIndexByRowHandle < 0)
                        {
                            selectedAnnotation = null;
                        }
                        else
                        {
                            List<ScrollBarAnnotationRowInfo> list1 = new List<ScrollBarAnnotationRowInfo>();
                            list1.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByRowHandle, this.GetActualAnnotationInfo(ScrollBarAnnotationMode.FocusedRow)));
                            selectedAnnotation = list1;
                        }
                        break;
                    }
                    goto TR_0000;
            }
            return selectedAnnotation;
        TR_0000:
            throw new ArgumentOutOfRangeException("annotation");
        TR_0014:
            nullable = null;
            Dictionary<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> fullCrawlIndexes = this.GetFullCrawlIndexes(view, nullable);
            if ((customCollection != null) && (customCollection.Count > 0))
            {
                List<ScrollBarAnnotationRowInfo> collection = new List<ScrollBarAnnotationRowInfo>();
                foreach (ScrollBarAnnotationRowInfo info in customCollection)
                {
                    int correctVisibleIndexByRowHandle = this.GetCorrectVisibleIndexByRowHandle(info.RowIndex, view);
                    if (correctVisibleIndexByRowHandle >= 0)
                    {
                        collection.Add(new ScrollBarAnnotationRowInfo(correctVisibleIndexByRowHandle, info.ScrollAnnotationInfo));
                    }
                }
                if (collection.Count > 0)
                {
                    currentRange[ScrollBarAnnotationMode.Custom].AddRange(collection);
                }
            }
            this.FullCrawlIndexesSyncDictionary(currentRange, fullCrawlIndexes);
            return null;
        }

        private ScrollBarAnnotationsAppearance ScrollBarAnnotationsAppearanceDefault
        {
            get
            {
                this.scrollBarAnnotationsAppearanceDefault ??= this.RootTableView.CreateDefaultScrollBarAnnotationsAppearance();
                return this.scrollBarAnnotationsAppearanceDefault;
            }
        }

        private DataViewBase View =>
            this.TableView.ViewBase;

        private bool HasDetailRootViews =>
            (this.RootView.DataControl != null) && (this.RootView.DataControl.DetailDescriptorCore != null);

        private bool HasDetailCurrentViews =>
            (this.View.DataControl != null) && (this.View.DataControl.DetailDescriptorCore != null);

        internal bool GridLoaded { get; set; }

        public ITableView RootTableView =>
            this.RootView as ITableView;

        public DataViewBase RootView =>
            this.View.RootView;

        public IEnumerable<ScrollBarAnnotationRowInfo> ScrollBarAnnotationInfoRange
        {
            get
            {
                List<ScrollBarAnnotationRowInfo> list = new List<ScrollBarAnnotationRowInfo>();
                Func<KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>>, bool> predicate = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>>, bool> local1 = <>c.<>9__26_0;
                    predicate = <>c.<>9__26_0 = x => (x.Value != null) && (x.Value.Count > 0);
                }
                if (this.InfoAnnotations.Any<KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>>>(predicate) && ((this.View.ActualFixedTopRowsCount != 0) || (this.View.ActualFixedBottomRowsCount != 0)))
                {
                    list.Add(new ScrollBarAnnotationFixedRowInfo(this.View.ActualFixedTopRowsCount, this.View.ActualFixedBottomRowsCount, this.View.DataControl.VisibleRowCount));
                }
                foreach (KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> pair in this.InfoAnnotations)
                {
                    list.AddRange(pair.Value);
                }
                return list;
            }
        }

        private bool IsServerOrAsyncServer =>
            (this.RootView != null) && ((this.RootView.DataProviderBase != null) && (this.RootView.DataProviderBase.IsServerMode || this.RootView.DataProviderBase.IsAsyncServerMode));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollBarAnnotationsManager.<>c <>9 = new ScrollBarAnnotationsManager.<>c();
            public static Func<KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>>, bool> <>9__26_0;
            public static Func<ScrollBarAnnotationRowInfo, int> <>9__56_0;

            internal int <BinarySearch>b__56_0(ScrollBarAnnotationRowInfo x) => 
                x.RowIndex;

            internal bool <get_ScrollBarAnnotationInfoRange>b__26_0(KeyValuePair<ScrollBarAnnotationMode, List<ScrollBarAnnotationRowInfo>> x) => 
                (x.Value != null) && (x.Value.Count > 0);
        }
    }
}

