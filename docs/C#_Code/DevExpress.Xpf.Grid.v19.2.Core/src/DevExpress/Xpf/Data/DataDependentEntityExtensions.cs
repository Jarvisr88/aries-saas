namespace DevExpress.Xpf.Data
{
    using DevExpress.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    internal static class DataDependentEntityExtensions
    {
        public static DataDependentEntity AddCheckBox(this DataDependentEntity dataDependentEntity)
        {
            bool? hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            return dataDependentEntity.Update(null, null, null, hasSorting, hasSorting, hasSorting, hasSorting, true, hasSorting);
        }

        public static DataDependentEntity AddColumn(this DataDependentEntity dataDependentEntity, ColumnBase column)
        {
            dataDependentEntity.Columns.Add(column);
            return dataDependentEntity;
        }

        public static DataDependentEntity AddFilter(this DataDependentEntity dataDependentEntity)
        {
            bool? hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            return dataDependentEntity.Update(null, null, null, hasSorting, hasSorting, true, hasSorting, hasSorting, hasSorting);
        }

        public static DataDependentEntity AddGrouping(this DataDependentEntity dataDependentEntity)
        {
            bool? hasGrouping = true;
            bool? hasFilter = null;
            hasFilter = null;
            hasFilter = null;
            hasFilter = null;
            return dataDependentEntity.Update(null, null, null, true, hasGrouping, hasFilter, hasFilter, hasFilter, hasFilter);
        }

        public static DataDependentEntity AddGroupSummary(this DataDependentEntity dataDependentEntity, SummaryItem groupSummary)
        {
            dataDependentEntity.GroupSummary.Add(groupSummary);
            return dataDependentEntity;
        }

        public static DataDependentEntity AddRowConditions(this DataDependentEntity dataDependentEntity)
        {
            bool? hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            return dataDependentEntity.Update(null, null, null, hasSorting, hasSorting, hasSorting, true, hasSorting, hasSorting);
        }

        public static DataDependentEntity AddSorting(this DataDependentEntity dataDependentEntity)
        {
            bool? hasGrouping = null;
            hasGrouping = null;
            hasGrouping = null;
            hasGrouping = null;
            hasGrouping = null;
            return dataDependentEntity.Update(null, null, null, true, hasGrouping, hasGrouping, hasGrouping, hasGrouping, hasGrouping);
        }

        public static DataDependentEntity AddTotalSummary(this DataDependentEntity dataDependentEntity, SummaryItem totalSummary)
        {
            dataDependentEntity.TotalSummary.Add(totalSummary);
            return dataDependentEntity;
        }
    }
}

