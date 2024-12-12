namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class ReuseCellDataHelper
    {
        private static TColumnData GetItem<TColumnData>(Dictionary<ColumnBase, TColumnData> matchedItems, List<TColumnData> unmatchedItems, ColumnBase column) where TColumnData: GridColumnData
        {
            if (matchedItems.ContainsKey(column))
            {
                return matchedItems[column];
            }
            int index = 0;
            int num2 = 0;
            while (true)
            {
                if (num2 < unmatchedItems.Count)
                {
                    if (unmatchedItems[num2].Column != null)
                    {
                        num2++;
                        continue;
                    }
                    index = num2;
                }
                TColumnData local = unmatchedItems[index];
                unmatchedItems.RemoveAt(index);
                return local;
            }
        }

        internal static void ReuseCellData<TColumnData>(ColumnsRowDataBase rowData, Func<ColumnsRowDataBase, IList<TColumnData>> getter, Action<ColumnsRowDataBase, IList> setter, UpdateCellDataStrategyBase<TColumnData> updateStrategy, IList<ColumnBase> sourceColumns, int bufferLength, int maxDataCount) where TColumnData: GridColumnData
        {
            if (sourceColumns != null)
            {
                IList<TColumnData> list = getter(rowData);
                if (list == null)
                {
                    list = new VersionedObservableCollection<TColumnData>(Guid.Empty);
                    setter(rowData, (IList) list);
                }
                int num = Math.Min(bufferLength + sourceColumns.Count, maxDataCount) - list.Count;
                for (int i = 0; i < num; i++)
                {
                    list.Add(updateStrategy.CreateNewData());
                }
                Dictionary<ColumnBase, TColumnData> matchedItems = new Dictionary<ColumnBase, TColumnData>();
                List<TColumnData> unmatchedItems = new List<TColumnData>();
                foreach (TColumnData local in list)
                {
                    if (sourceColumns.Contains(local.Column))
                    {
                        matchedItems.Add(local.Column, local);
                        continue;
                    }
                    unmatchedItems.Add(local);
                }
                for (int j = 0; j < sourceColumns.Count; j++)
                {
                    ColumnBase key = sourceColumns[j];
                    if (!matchedItems.ContainsKey(key))
                    {
                        for (int m = 0; m < unmatchedItems.Count; m++)
                        {
                            TColumnData local2 = unmatchedItems[m];
                            if ((local2.Column != null) && ((key.ActualEditSettings != null) && EditSettingsComparer.IsCompatibleEditSettings(local2.Column.ActualEditSettings, key.ActualEditSettings)))
                            {
                                matchedItems.Add(key, local2);
                                unmatchedItems.Remove(local2);
                                break;
                            }
                        }
                    }
                }
                bool flag = false;
                for (int k = 0; k < sourceColumns.Count; k++)
                {
                    ColumnBase column = sourceColumns[k];
                    TColumnData data = GetItem<TColumnData>(matchedItems, unmatchedItems, column);
                    if ((data.Column != null) && (updateStrategy.DataCache.ContainsKey(data.Column) && (updateStrategy.DataCache[data.Column] == data)))
                    {
                        updateStrategy.DataCache.Remove(data.Column);
                    }
                    if (!rowData.ShouldUpdateCellData(data, column))
                    {
                        updateStrategy.DataCache[data.Column] = data;
                    }
                    else
                    {
                        updateStrategy.DataCache[column] = data;
                        updateStrategy.UpdateData(column, data);
                        flag = true;
                    }
                    if (data.VisibleIndex != k)
                    {
                        flag = true;
                        data.VisibleIndex = k;
                    }
                    if (data.ActualVisibleIndex != column.ActualVisibleIndex)
                    {
                        data.ActualVisibleIndex = column.ActualVisibleIndex;
                        flag = true;
                    }
                }
                foreach (TColumnData local4 in unmatchedItems)
                {
                    if (!OrderPanelBase.IsInvisibleIndex(local4.VisibleIndex))
                    {
                        flag = true;
                        local4.VisibleIndex = -1;
                        local4.ActualVisibleIndex = -1;
                    }
                }
                if (flag)
                {
                    ((VersionedObservableCollection<TColumnData>) list).RaiseCollectionChanged();
                }
            }
        }
    }
}

