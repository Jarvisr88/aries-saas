namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public static class SummaryItemCollectionUpdater
    {
        private static bool CanUseFixedTotalSummary(DataViewBase dataView, DevExpress.Xpf.Grid.SummaryItemBase item) => 
            (item.SummaryType != SummaryItemType.Count) ? (!string.IsNullOrWhiteSpace(item.FieldName) ? (dataView.DataProviderBase.Columns[item.FieldName] != null) : false) : true;

        public static void ClearColumnSummaries(SummaryItemCollectionType collectionType, IColumnCollection columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                GetActualSummaryItemCollection(columns[i], collectionType).Clear();
            }
        }

        private static IList<DevExpress.Xpf.Grid.SummaryItemBase> GetActualSummaryItemCollection(ColumnBase column, SummaryItemCollectionType collectionType) => 
            (collectionType == SummaryItemCollectionType.Group) ? column.GroupSummariesCore : ((collectionType == SummaryItemCollectionType.Total) ? column.TotalSummariesCore : null);

        private static void RefreshSummariesByColumn(DataViewBase dataView, DevExpress.Xpf.Grid.SummaryItemBase item, IList<DevExpress.Xpf.Grid.SummaryItemBase> columnSummaries, SummaryItemCollectionType collectionType)
        {
            if (!item.Visible || (item.SummaryType == SummaryItemType.None))
            {
                columnSummaries.Remove(item);
            }
            else if (collectionType == SummaryItemCollectionType.Group)
            {
                if (!columnSummaries.Contains(item))
                {
                    columnSummaries.Add(item);
                }
            }
            else
            {
                GridSummaryItemAlignment alignment = item.Alignment;
                if (alignment == GridSummaryItemAlignment.Left)
                {
                    dataView.FixedSummariesHelper.FixedSummariesLeftCore.Add(item);
                }
                else if (alignment == GridSummaryItemAlignment.Right)
                {
                    dataView.FixedSummariesHelper.FixedSummariesRightCore.Add(item);
                }
                else if (!columnSummaries.Contains(item))
                {
                    columnSummaries.Add(item);
                }
            }
        }

        public static void Update(DataViewBase dataView, SummaryItemCollectionType collectionType, ISummaryItemOwner summaries, IColumnCollection columns)
        {
            if (dataView != null)
            {
                if (collectionType == SummaryItemCollectionType.Total)
                {
                    dataView.FixedSummariesHelper.FixedSummariesLeftCore.Clear();
                    dataView.FixedSummariesHelper.FixedSummariesRightCore.Clear();
                }
                foreach (DevExpress.Xpf.Grid.SummaryItemBase base2 in summaries)
                {
                    string actualShowInColumn = base2.ActualShowInColumn;
                    if (columns[actualShowInColumn] != null)
                    {
                        RefreshSummariesByColumn(dataView, base2, GetActualSummaryItemCollection(columns[actualShowInColumn], collectionType), collectionType);
                        continue;
                    }
                    if ((collectionType == SummaryItemCollectionType.Total) && base2.Visible)
                    {
                        GridSummaryItemAlignment alignment = base2.Alignment;
                        if (alignment == GridSummaryItemAlignment.Left)
                        {
                            dataView.FixedSummariesHelper.FixedSummariesLeftCore.Add(base2);
                            continue;
                        }
                        if (alignment == GridSummaryItemAlignment.Right)
                        {
                            dataView.FixedSummariesHelper.FixedSummariesRightCore.Add(base2);
                        }
                    }
                }
                UpdateSummariesIsLastProperty(dataView.FixedSummariesHelper.FixedSummariesLeftCore);
                UpdateSummariesIsLastProperty(dataView.FixedSummariesHelper.FixedSummariesRightCore);
            }
        }

        private static void UpdateSummariesIsLastProperty(IList<DevExpress.Xpf.Grid.SummaryItemBase> totalSummariesCore)
        {
            if (totalSummariesCore.Count != 0)
            {
                for (int i = 0; i < (totalSummariesCore.Count - 1); i++)
                {
                    totalSummariesCore[i].IsLast = false;
                }
                totalSummariesCore[totalSummariesCore.Count - 1].IsLast = true;
            }
        }
    }
}

