namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class GridSortInfoCollectionExtensions
    {
        public static List<GridSortInfo> GetMergeSortInfo(this IEnumerable<GridSortInfo> collection, GridSortInfo sortInfo, int groupCount)
        {
            if ((collection == null) || ((sortInfo == null) || (groupCount <= 0)))
            {
                return null;
            }
            int num = 0;
            List<GridSortInfo> list = new List<GridSortInfo>();
            bool flag = false;
            bool flag2 = false;
            foreach (GridSortInfo info in collection)
            {
                if (num != groupCount)
                {
                    if (!flag)
                    {
                        if (ReferenceEquals(info, sortInfo))
                        {
                            if (!info.MergeWithPreviousGroup)
                            {
                                list.Clear();
                            }
                            flag2 = true;
                            list.Add(info);
                            flag = true;
                            continue;
                        }
                        if (num == 0)
                        {
                            list.Add(info);
                        }
                        else if (info.MergeWithPreviousGroup)
                        {
                            list.Add(info);
                        }
                        else
                        {
                            list.Clear();
                            list.Add(info);
                        }
                        num++;
                        continue;
                    }
                    if (info.MergeWithPreviousGroup)
                    {
                        list.Add(info);
                        continue;
                    }
                }
                break;
            }
            return (flag2 ? ((list.Count <= 1) ? null : list) : null);
        }
    }
}

