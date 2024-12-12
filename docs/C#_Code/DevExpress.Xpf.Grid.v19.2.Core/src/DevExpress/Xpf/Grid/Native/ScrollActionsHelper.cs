namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.InteropServices;

    internal static class ScrollActionsHelper
    {
        private static int CalcFirstScrollRowTotalLevel(DataViewBase view)
        {
            DataViewBase targetView = null;
            int targetVisibleIndex = 0;
            view.DataControl.FindViewAndVisibleIndexByScrollIndex(view.CalcFirstScrollRowScrollIndex(), true, out targetView, out targetVisibleIndex);
            if (targetView == null)
            {
                return 0;
            }
            int num2 = targetView.DataControl.CalcTotalLevel(targetVisibleIndex);
            if (targetView.DataControl.IsExpandedFixedRow(targetVisibleIndex))
            {
                num2++;
            }
            return num2;
        }

        private static int CalcGroupSummaryRowCountAfterRow(DataViewBase view, int visibleIndex)
        {
            if (!view.ShowGroupSummaryFooter)
            {
                return 0;
            }
            int num2 = 0;
            int scrollIndex = view.DataProviderBase.ConvertVisibleIndexToScrollIndex(visibleIndex, false) + 1;
            while (true)
            {
                if (scrollIndex < (view.DataProviderBase.VisibleCount + view.CalcGroupSummaryVisibleRowCount()))
                {
                    GroupSummaryRowKey visibleIndexByScrollIndex = view.DataProviderBase.GetVisibleIndexByScrollIndex(scrollIndex) as GroupSummaryRowKey;
                    if (visibleIndexByScrollIndex != null)
                    {
                        num2++;
                        scrollIndex++;
                        continue;
                    }
                }
                return num2;
            }
        }

        private static int CalcGroupSummaryRowCountBeforeRow(DataViewBase view, int visibleIndex)
        {
            if (!view.ShowGroupSummaryFooter)
            {
                return 0;
            }
            int num2 = 0;
            int scrollIndex = view.DataProviderBase.ConvertVisibleIndexToScrollIndex(visibleIndex, false) - 1;
            while (true)
            {
                if (scrollIndex >= 0)
                {
                    GroupSummaryRowKey visibleIndexByScrollIndex = view.DataProviderBase.GetVisibleIndexByScrollIndex(scrollIndex) as GroupSummaryRowKey;
                    if (visibleIndexByScrollIndex != null)
                    {
                        num2++;
                        scrollIndex--;
                        continue;
                    }
                }
                return num2;
            }
        }

        internal static int CalcLastScrollRowFirstInnerChildScrollIndex(DataViewBase view)
        {
            if (view.RootDataPresenter.IsAnimationInProgress)
            {
                return ((((int) Math.Ceiling(view.RootDataPresenter.ActualScrollOffset)) + view.RootDataPresenter.FullyVisibleItemsCount) - 1);
            }
            DataViewBase base2 = null;
            int visibleIndex = 0;
            view.GetLastScrollRowViewAndVisibleIndex(out base2, out visibleIndex);
            return ((base2 != null) ? base2.DataControl.FindFirstInnerChildScrollIndex(visibleIndex) : 0);
        }

        private static int CalcLastScrollRowFirstInnerChildTotalLevel(DataViewBase view)
        {
            int scrollIndex = CalcLastScrollRowFirstInnerChildScrollIndex(view);
            DataViewBase targetView = null;
            int targetVisibleIndex = 0;
            return (!view.DataControl.FindViewAndVisibleIndexByScrollIndex(scrollIndex, true, out targetView, out targetVisibleIndex) ? 0 : targetView.DataControl.CalcTotalLevel(targetVisibleIndex));
        }

        internal static int CalcLastScrollRowTotalLevel(DataViewBase view)
        {
            DataViewBase base2 = null;
            int visibleIndex = 0;
            view.GetLastScrollRowViewAndVisibleIndex(out base2, out visibleIndex);
            return ((base2 != null) ? base2.DataControl.CalcTotalLevel(visibleIndex) : 0);
        }

        internal static int FindNearestScrollableRow(DataViewBase targetView, int scrollIndex)
        {
            int num = -1;
            int num2 = scrollIndex;
            while (true)
            {
                if (num2 < (targetView.DataProviderBase.VisibleCount + targetView.CalcGroupSummaryVisibleRowCount()))
                {
                    object visibleIndexByScrollIndex = targetView.DataProviderBase.GetVisibleIndexByScrollIndex(num2);
                    if (!(visibleIndexByScrollIndex is int))
                    {
                        num2++;
                        continue;
                    }
                    num = num2;
                }
                if (num < 0)
                {
                    for (int i = scrollIndex; i >= 0; i--)
                    {
                        object visibleIndexByScrollIndex = targetView.DataProviderBase.GetVisibleIndexByScrollIndex(i);
                        if (visibleIndexByScrollIndex is int)
                        {
                            num = i;
                            break;
                        }
                    }
                }
                return ((num < 0) ? scrollIndex : num);
            }
        }

        internal static int GetGroupSummaryRowCountAfterRow(DataViewBase targetView, int visibleIndex, bool needToAdjustScroll, bool checkLastRow = false)
        {
            int num = CalcGroupSummaryRowCountAfterRow(targetView, visibleIndex);
            return ((num != 0) ? ((!checkLastRow || (visibleIndex != (targetView.DataControl.VisibleRowCount - 1))) ? (num + (needToAdjustScroll ? 1 : 0)) : -1) : 0);
        }

        internal static int GetGroupSummaryRowCountAfterRowByRowHandle(DataViewBase targetView, int rowHandle, bool needToAdjustScroll, bool checkLastRow = false)
        {
            int rowVisibleIndexByHandleCore = targetView.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);
            return GetGroupSummaryRowCountAfterRow(targetView, rowVisibleIndexByHandleCore, needToAdjustScroll, checkLastRow);
        }

        internal static int GetGroupSummaryRowCountBeforeRow(DataViewBase targetView, int visibleIndex, bool needToAdjustScroll)
        {
            int num = CalcGroupSummaryRowCountBeforeRow(targetView, visibleIndex);
            return ((num != 0) ? ((visibleIndex != 0) ? (num + (needToAdjustScroll ? 1 : 0)) : -1) : 0);
        }

        internal static int GetGroupSummaryRowCountBeforeRowByRowHandle(DataViewBase targetView, int rowHandle, bool needToAdjustScroll)
        {
            int rowVisibleIndexByHandleCore = targetView.DataControl.GetRowVisibleIndexByHandleCore(rowHandle);
            return GetGroupSummaryRowCountBeforeRow(targetView, rowVisibleIndexByHandleCore, needToAdjustScroll);
        }

        internal static int GetOffsetDeltaForPageDown(DataViewBase view) => 
            (view.RootDataPresenter.FullyVisibleItemsCount - CalcLastScrollRowTotalLevel(view)) - 1;

        internal static int GetOffsetDeltaForPageUp(DataViewBase view) => 
            (-view.RootDataPresenter.FullyVisibleItemsCount + CalcFirstScrollRowTotalLevel(view)) + 1;

        internal static int GetOffsetDeltaForPerPixelPageDown(DataViewBase view)
        {
            int num = view.RootDataPresenter.FullyVisibleItemsCount - 1;
            if (!view.RootDataPresenter.IsAnimationInProgress)
            {
                num -= CalcLastScrollRowFirstInnerChildTotalLevel(view);
            }
            return num;
        }

        internal static bool IsRowElementVisible(DataViewBase view, int rowHandle) => 
            view.GetRowElementByRowHandle(rowHandle) != null;
    }
}

