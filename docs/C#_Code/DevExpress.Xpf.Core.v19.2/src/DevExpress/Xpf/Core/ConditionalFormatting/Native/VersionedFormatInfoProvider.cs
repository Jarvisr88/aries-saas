namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class VersionedFormatInfoProvider : IFormatInfoProvider
    {
        private readonly IFormatInfoProvider component;
        private FormatInfoSnapshot previousVersion;
        private Locker cachingLocker = new Locker();

        public VersionedFormatInfoProvider(IFormatInfoProvider component)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            this.component = component;
            this.Clear();
            this.IsCachingEnabled = true;
        }

        public void Clear()
        {
            this.previousVersion = new FormatInfoSnapshot();
        }

        private object GetAndCacheValue<T>(object value, T key, Dictionary<T, object> dictionary)
        {
            if (this.IsCachingEnabled && !this.CachingLocker.IsLocked)
            {
                dictionary[key] = value;
            }
            return value;
        }

        public object GetCellValue(string fieldName) => 
            this.GetAndCacheValue<string>(this.component.GetCellValue(fieldName), fieldName, this.previousVersion.CellValues);

        public object GetCellValueByListIndex(int listIndex, string fieldName) => 
            this.GetAndCacheValue<ListIndexKey>(this.component.GetCellValueByListIndex(listIndex, fieldName), new ListIndexKey(listIndex, fieldName), this.previousVersion.ListIndexCellValues);

        public IFormatInfoProvider GetCurrentVersion() => 
            this.component;

        public FormatInfoSnapshot GetPreviousVersion() => 
            this.previousVersion;

        public object GetTotalSummaryValue(string fieldName, ConditionalFormatSummaryType summaryType)
        {
            object totalSummaryValue = this.component.GetTotalSummaryValue(fieldName, summaryType);
            return ((totalSummaryValue != null) ? this.GetAndCacheValue<TotalSummaryKey>(totalSummaryValue, new TotalSummaryKey(summaryType, fieldName), this.previousVersion.TotalSummaryValues) : null);
        }

        public DevExpress.Data.ValueComparer ValueComparer
        {
            get
            {
                DevExpress.Data.ValueComparer valueComparer = this.component.ValueComparer;
                this.previousVersion.CurrentValueComparer = valueComparer;
                return valueComparer;
            }
        }

        public bool IsCachingEnabled { get; set; }

        public Locker CachingLocker =>
            this.cachingLocker;
    }
}

