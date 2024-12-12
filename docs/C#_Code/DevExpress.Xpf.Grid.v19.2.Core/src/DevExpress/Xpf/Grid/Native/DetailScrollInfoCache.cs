namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DetailScrollInfoCache
    {
        private bool isCacheValid;
        private int ScrollDetailRowsCount;
        private int VisibleDetailRowCount;
        private readonly List<ExpandedDetailInfo> expandedDetailInfoList;
        private readonly List<int> expandedDetailsVisibleIndices;
        private readonly MasterDetailProvider owner;
        private readonly CalcVisibleDetailRowsCountBeforeRowComparer calcScrollDetailRowsCountBeforeRowComparer;
        private readonly CalcVisibleDetailRowsCountBeforeRowComparer calcVisibleDetailRowsCountBeforeRowComparer;
        private readonly CalcMasterRowInfoComparer calcMasterRowScrollInfoComparer;
        private readonly CalcMasterRowInfoComparer calcMasterRowNavigationInfoComparer;

        public DetailScrollInfoCache(MasterDetailProvider owner)
        {
            // Unresolved stack state at '000000F8'
        }

        public MasterRowNavigationInfo CalcMasterRowNavigationInfo(int commonVisibleIndex)
        {
            this.ValidateCache();
            this.calcMasterRowNavigationInfoComparer.CommonIndex = commonVisibleIndex;
            ExpandedDetailInfo item = new ExpandedDetailInfo();
            int index = this.expandedDetailInfoList.BinarySearch(item, this.calcMasterRowNavigationInfoComparer);
            int startVisibleIndex = commonVisibleIndex;
            if (index >= 0)
            {
                ExpandedDetailInfo info = this.expandedDetailInfoList[index];
                return ((commonVisibleIndex >= info.CommonVisibleIndex) ? new MasterRowNavigationInfo(info.LocalVisibleIndex, commonVisibleIndex - info.CommonVisibleIndex, true) : new MasterRowNavigationInfo(startVisibleIndex - this.GetPrevInfoVisibleRowCountBeforeNextRow(index), 0, false));
            }
            startVisibleIndex -= this.GetPrevInfoVisibleRowCountBeforeNextRow(this.expandedDetailInfoList.Count);
            int visibleRowCount = this.View.DataControl.VisibleRowCount;
            if (this.View.IsNewItemRowVisible)
            {
                visibleRowCount++;
            }
            return (((0 > startVisibleIndex) || (startVisibleIndex >= visibleRowCount)) ? null : new MasterRowNavigationInfo(startVisibleIndex, 0, false));
        }

        public MasterRowScrollInfo CalcMasterRowScrollInfo(int commonScrollIndex)
        {
            this.ValidateCache();
            this.calcMasterRowScrollInfoComparer.CommonIndex = commonScrollIndex;
            ExpandedDetailInfo item = new ExpandedDetailInfo();
            int index = this.expandedDetailInfoList.BinarySearch(item, this.calcMasterRowScrollInfoComparer);
            int startScrollIndex = commonScrollIndex;
            if (index < 0)
            {
                startScrollIndex -= this.GetPrevInfoScrollRowCountBeforeNextRow(this.expandedDetailInfoList.Count);
                return ((startScrollIndex >= (this.View.DataControl.VisibleRowCount + this.View.CalcGroupSummaryVisibleRowCount())) ? null : new MasterRowScrollInfo(startScrollIndex, 0, true));
            }
            ExpandedDetailInfo info = this.expandedDetailInfoList[index];
            return ((commonScrollIndex >= info.CommonScrollIndex) ? new MasterRowScrollInfo(info.LocalScrollIndex, commonScrollIndex - info.CommonScrollIndex, info.LastDetailRowCommonScrollIndex == commonScrollIndex) : new MasterRowScrollInfo(startScrollIndex - this.GetPrevInfoScrollRowCountBeforeNextRow(index), 0, true));
        }

        public int CalcScrollDetailRowsCount()
        {
            this.ValidateCache();
            return this.ScrollDetailRowsCount;
        }

        public int CalcScrollDetailRowsCountBeforeRow(int scrollIndex)
        {
            this.ValidateCache();
            this.calcScrollDetailRowsCountBeforeRowComparer.Index = scrollIndex;
            ExpandedDetailInfo item = new ExpandedDetailInfo();
            int num = this.expandedDetailInfoList.BinarySearch(item, this.calcScrollDetailRowsCountBeforeRowComparer);
            if (num < 0)
            {
                return 0;
            }
            ExpandedDetailInfo info = this.expandedDetailInfoList[num];
            return ((scrollIndex <= info.LocalScrollIndex) ? info.ScrollRowCountBeforeRow : (info.ScrollRowCountBeforeRow + info.ScrollRowCount));
        }

        public int CalcVisibleDetailDataRowCount()
        {
            this.ValidateCache();
            return this.VisibleDetailRowCount;
        }

        public int CalcVisibleDetailRowsCountBeforeRow(int visibleIndex)
        {
            this.ValidateCache();
            this.calcVisibleDetailRowsCountBeforeRowComparer.Index = visibleIndex;
            ExpandedDetailInfo item = new ExpandedDetailInfo();
            int num = this.expandedDetailInfoList.BinarySearch(item, this.calcVisibleDetailRowsCountBeforeRowComparer);
            if (num < 0)
            {
                return 0;
            }
            ExpandedDetailInfo info = this.expandedDetailInfoList[num];
            return ((visibleIndex <= info.LocalVisibleIndex) ? info.VisibleDetailRowCountBeforeRow : (info.VisibleDetailRowCountBeforeRow + info.VisibleRowCount));
        }

        private int GetPrevInfoScrollRowCountBeforeNextRow(int index) => 
            (index <= 0) ? 0 : this.expandedDetailInfoList[index - 1].ScrollRowCountBeforeNextRow;

        private int GetPrevInfoVisibleRowCountBeforeNextRow(int index) => 
            (index <= 0) ? 0 : this.expandedDetailInfoList[index - 1].VisibleDetailRowCountBeforeNextRow;

        public void InvalidateCache()
        {
            this.isCacheValid = false;
        }

        private void ValidateCache()
        {
            if (!this.isCacheValid)
            {
                this.isCacheValid = true;
                this.expandedDetailsVisibleIndices.Clear();
                foreach (int num2 in this.View.DataProviderBase.GetRowListIndicesWithExpandedDetails())
                {
                    int rowHandleByListIndex = this.View.DataProviderBase.GetRowHandleByListIndex(num2);
                    if (rowHandleByListIndex != -2147483648)
                    {
                        int rowVisibleIndexByHandle = this.View.DataProviderBase.GetRowVisibleIndexByHandle(rowHandleByListIndex);
                        if (rowVisibleIndexByHandle >= 0)
                        {
                            this.expandedDetailsVisibleIndices.Add(rowVisibleIndexByHandle);
                        }
                    }
                }
                this.expandedDetailsVisibleIndices.Sort();
                int count = this.expandedDetailsVisibleIndices.Count;
                this.ScrollDetailRowsCount = 0;
                this.VisibleDetailRowCount = 0;
                this.expandedDetailInfoList.Clear();
                for (int i = 0; i < count; i++)
                {
                    int visibleIndex = this.expandedDetailsVisibleIndices[i];
                    int localScrollIndex = this.View.DataProviderBase.ConvertVisibleIndexToScrollIndex(visibleIndex, this.View.AllowFixedGroupsCore);
                    int rowHandleByVisibleIndexCore = this.View.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                    int detailRowsCount = this.owner.CalcVisibleDetailRowsCountForRow(rowHandleByVisibleIndexCore);
                    int visibleRowCount = this.owner.CalcVisibleDataRowCountForRow(rowHandleByVisibleIndexCore);
                    int commonVisibleIndex = (visibleIndex + this.VisibleDetailRowCount) + 1;
                    int visibleRowCountBeforeRow = (commonVisibleIndex - visibleIndex) - 1;
                    if (this.View.IsNewItemRowVisible)
                    {
                        commonVisibleIndex++;
                    }
                    this.expandedDetailInfoList.Add(new ExpandedDetailInfo(localScrollIndex + this.ScrollDetailRowsCount, localScrollIndex, detailRowsCount, i, commonVisibleIndex, visibleIndex, visibleRowCount, visibleRowCountBeforeRow));
                    this.ScrollDetailRowsCount += detailRowsCount;
                    this.VisibleDetailRowCount += visibleRowCount;
                }
            }
        }

        private DataViewBase View =>
            this.owner.View;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DetailScrollInfoCache.<>c <>9 = new DetailScrollInfoCache.<>c();
            public static Func<DetailScrollInfoCache.ExpandedDetailInfo, int> <>9__13_0;
            public static Func<DetailScrollInfoCache.ExpandedDetailInfo, int> <>9__13_1;
            public static Func<DetailScrollInfoCache.ExpandedDetailInfo, int> <>9__13_2;
            public static Func<DetailScrollInfoCache.ExpandedDetailInfo, int> <>9__13_3;
            public static Func<DetailScrollInfoCache.ExpandedDetailInfo, int> <>9__13_4;
            public static Func<DetailScrollInfoCache.ExpandedDetailInfo, int> <>9__13_5;

            internal int <.ctor>b__13_0(DetailScrollInfoCache.ExpandedDetailInfo info) => 
                info.LocalScrollIndex;

            internal int <.ctor>b__13_1(DetailScrollInfoCache.ExpandedDetailInfo info) => 
                info.LocalVisibleIndex;

            internal int <.ctor>b__13_2(DetailScrollInfoCache.ExpandedDetailInfo info) => 
                info.CommonScrollIndex;

            internal int <.ctor>b__13_3(DetailScrollInfoCache.ExpandedDetailInfo info) => 
                info.LastDetailRowCommonScrollIndex;

            internal int <.ctor>b__13_4(DetailScrollInfoCache.ExpandedDetailInfo info) => 
                info.CommonVisibleIndex;

            internal int <.ctor>b__13_5(DetailScrollInfoCache.ExpandedDetailInfo info) => 
                info.LastDetailRowCommonVisibleIndex;
        }

        private class CalcMasterRowInfoComparer : IComparer<DetailScrollInfoCache.ExpandedDetailInfo>
        {
            private readonly List<DetailScrollInfoCache.ExpandedDetailInfo> expandedDetailInfoList;
            internal int CommonIndex;
            private readonly Func<DetailScrollInfoCache.ExpandedDetailInfo, int> getStartIndex;
            private readonly Func<DetailScrollInfoCache.ExpandedDetailInfo, int> getEndIndex;

            public CalcMasterRowInfoComparer(List<DetailScrollInfoCache.ExpandedDetailInfo> expandedDetailInfoList, Func<DetailScrollInfoCache.ExpandedDetailInfo, int> getStartIndex, Func<DetailScrollInfoCache.ExpandedDetailInfo, int> getEndIndex)
            {
                this.expandedDetailInfoList = expandedDetailInfoList;
                this.getStartIndex = getStartIndex;
                this.getEndIndex = getEndIndex;
            }

            int IComparer<DetailScrollInfoCache.ExpandedDetailInfo>.Compare(DetailScrollInfoCache.ExpandedDetailInfo item, DetailScrollInfoCache.ExpandedDetailInfo ignore)
            {
                int num = this.getStartIndex(item);
                int num2 = this.getEndIndex(item);
                if ((num <= this.CommonIndex) && (this.CommonIndex <= num2))
                {
                    return 0;
                }
                if (this.CommonIndex < num)
                {
                    int index = item.Index;
                    if ((index == 0) || (this.CommonIndex > this.getEndIndex(this.expandedDetailInfoList[index - 1])))
                    {
                        return 0;
                    }
                }
                return ((this.CommonIndex <= num2) ? 1 : -1);
            }
        }

        private class CalcVisibleDetailRowsCountBeforeRowComparer : IComparer<DetailScrollInfoCache.ExpandedDetailInfo>
        {
            private readonly List<DetailScrollInfoCache.ExpandedDetailInfo> expandedDetailInfoList;
            internal int Index;
            private readonly Func<DetailScrollInfoCache.ExpandedDetailInfo, int> getIndex;

            public CalcVisibleDetailRowsCountBeforeRowComparer(List<DetailScrollInfoCache.ExpandedDetailInfo> expandedDetailInfoList, Func<DetailScrollInfoCache.ExpandedDetailInfo, int> getIndex)
            {
                this.expandedDetailInfoList = expandedDetailInfoList;
                this.getIndex = getIndex;
            }

            int IComparer<DetailScrollInfoCache.ExpandedDetailInfo>.Compare(DetailScrollInfoCache.ExpandedDetailInfo item, DetailScrollInfoCache.ExpandedDetailInfo ignore)
            {
                if (this.Index < this.getIndex(item))
                {
                    return 1;
                }
                int index = item.Index;
                return (((index >= (this.expandedDetailInfoList.Count - 1)) || (this.Index < this.getIndex(this.expandedDetailInfoList[index + 1]))) ? 0 : -1);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ExpandedDetailInfo
        {
            public readonly int CommonScrollIndex;
            public readonly int LocalScrollIndex;
            public readonly int ScrollRowCount;
            public readonly int Index;
            public readonly int LocalVisibleIndex;
            public readonly int CommonVisibleIndex;
            public readonly int VisibleRowCount;
            public readonly int VisibleDetailRowCountBeforeRow;
            public ExpandedDetailInfo(int commonScrollIndex, int localScrollIndex, int detailRowsCount, int index, int commonVisibleIndex, int localVisibleIndex, int visibleRowCount, int visibleRowCountBeforeRow)
            {
                this.CommonScrollIndex = commonScrollIndex;
                this.LocalScrollIndex = localScrollIndex;
                this.ScrollRowCount = detailRowsCount;
                this.Index = index;
                this.CommonVisibleIndex = commonVisibleIndex;
                this.LocalVisibleIndex = localVisibleIndex;
                this.VisibleRowCount = visibleRowCount;
                this.VisibleDetailRowCountBeforeRow = visibleRowCountBeforeRow;
            }

            public int ScrollRowCountBeforeRow =>
                this.CommonScrollIndex - this.LocalScrollIndex;
            public int ScrollRowCountBeforeNextRow =>
                this.ScrollRowCountBeforeRow + this.ScrollRowCount;
            public int LastDetailRowCommonScrollIndex =>
                this.CommonScrollIndex + this.ScrollRowCount;
            public int VisibleDetailRowCountBeforeNextRow =>
                this.VisibleDetailRowCountBeforeRow + this.VisibleRowCount;
            public int LastDetailRowCommonVisibleIndex =>
                (this.CommonVisibleIndex + this.VisibleRowCount) - 1;
        }
    }
}

