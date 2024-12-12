namespace DevExpress.Xpf.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DataDependentEntity
    {
        public DataDependentEntity() : this(new HashSet<ColumnBase>(), new HashSet<SummaryItem>(), new HashSet<SummaryItem>(), false, false, false, false, false, false)
        {
        }

        public DataDependentEntity(HashSet<ColumnBase> columns, HashSet<SummaryItem> totalSummary, HashSet<SummaryItem> groupSummary) : this(columns, totalSummary, groupSummary, false, false, false, false, false, false)
        {
        }

        public DataDependentEntity(HashSet<ColumnBase> columns, HashSet<SummaryItem> totalSummary, HashSet<SummaryItem> groupSummary, bool hasSorting, bool hasGrouping, bool hasFilter, bool hasRowConditions, bool hasCheckBox, bool hasExpandStateBinding)
        {
            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }
            if (totalSummary == null)
            {
                throw new ArgumentNullException("totalSummary");
            }
            if (groupSummary == null)
            {
                throw new ArgumentNullException("groupSummary");
            }
            this.<Columns>k__BackingField = columns;
            this.<TotalSummary>k__BackingField = totalSummary;
            this.<GroupSummary>k__BackingField = groupSummary;
            this.<HasSorting>k__BackingField = hasSorting;
            this.<HasGrouping>k__BackingField = hasGrouping;
            this.<HasFilter>k__BackingField = hasFilter;
            this.<HasRowConditions>k__BackingField = hasRowConditions;
            this.<HasCheckBox>k__BackingField = hasCheckBox;
            this.<HasExpandStateBinding>k__BackingField = hasExpandStateBinding;
        }

        internal static DataDependentEntity Combine(IEnumerable<DataDependentEntity> entities)
        {
            HashSet<ColumnBase> columns = new HashSet<ColumnBase>();
            HashSet<SummaryItem> totalSummary = new HashSet<SummaryItem>();
            HashSet<SummaryItem> groupSummary = new HashSet<SummaryItem>();
            bool hasSorting = false;
            bool hasGrouping = false;
            bool hasFilter = false;
            bool hasRowConditions = false;
            bool hasCheckBox = false;
            bool hasExpandStateBinding = false;
            foreach (DataDependentEntity entity in entities)
            {
                columns.UnionWith(entity.Columns);
                totalSummary.UnionWith(entity.TotalSummary);
                groupSummary.UnionWith(entity.GroupSummary);
                hasSorting |= entity.HasSorting;
                hasGrouping |= entity.HasGrouping;
                hasFilter |= entity.HasFilter;
                hasRowConditions |= entity.HasRowConditions;
                hasCheckBox |= entity.HasCheckBox;
                hasExpandStateBinding |= entity.HasExpandStateBinding;
            }
            return new DataDependentEntity(columns, totalSummary, groupSummary, hasSorting, hasGrouping, hasFilter, hasRowConditions, hasCheckBox, hasExpandStateBinding);
        }

        internal static DataDependentEntity CreateFromColumns(HashSet<ColumnBase> columns)
        {
            bool? hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            return new DataDependentEntity().Update(columns, null, null, hasSorting, hasSorting, hasSorting, hasSorting, hasSorting, hasSorting);
        }

        internal static DataDependentEntity CreateFromGroupSummary(HashSet<SummaryItem> groupSummary)
        {
            bool? hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            return new DataDependentEntity().Update(null, null, groupSummary, hasSorting, hasSorting, hasSorting, hasSorting, hasSorting, hasSorting);
        }

        internal static DataDependentEntity CreateFromTotalSummary(HashSet<SummaryItem> totalSummary)
        {
            bool? hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            hasSorting = null;
            return new DataDependentEntity().Update(null, totalSummary, null, hasSorting, hasSorting, hasSorting, hasSorting, hasSorting, hasSorting);
        }

        internal DataDependentEntity Update(HashSet<ColumnBase> columns = null, HashSet<SummaryItem> totalSummary = null, HashSet<SummaryItem> groupSummary = null, bool? hasSorting = new bool?(), bool? hasGrouping = new bool?(), bool? hasFilter = new bool?(), bool? hasRowConditions = new bool?(), bool? hasCheckBox = new bool?(), bool? hasExpandStateBinding = new bool?())
        {
            bool? nullable = hasSorting;
            nullable = hasGrouping;
            nullable = hasFilter;
            nullable = hasRowConditions;
            nullable = hasCheckBox;
            nullable = hasExpandStateBinding;
            return new DataDependentEntity(columns ?? this.Columns, totalSummary ?? this.TotalSummary, groupSummary ?? this.GroupSummary, (nullable != null) ? nullable.GetValueOrDefault() : this.HasSorting, (nullable != null) ? nullable.GetValueOrDefault() : this.HasGrouping, (nullable != null) ? nullable.GetValueOrDefault() : this.HasFilter, (nullable != null) ? nullable.GetValueOrDefault() : this.HasRowConditions, (nullable != null) ? nullable.GetValueOrDefault() : this.HasCheckBox, (nullable != null) ? nullable.GetValueOrDefault() : this.HasExpandStateBinding);
        }

        public HashSet<ColumnBase> Columns { get; }

        public HashSet<SummaryItem> TotalSummary { get; }

        public HashSet<SummaryItem> GroupSummary { get; }

        public bool HasSorting { get; }

        public bool HasFilter { get; }

        public bool HasRowConditions { get; }

        public bool HasGrouping { get; }

        public bool HasCheckBox { get; }

        public bool HasExpandStateBinding { get; }
    }
}

