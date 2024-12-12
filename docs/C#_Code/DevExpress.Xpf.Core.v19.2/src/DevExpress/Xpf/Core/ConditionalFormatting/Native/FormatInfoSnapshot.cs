namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class FormatInfoSnapshot : IFormatInfoProvider, IValidationServiceClient
    {
        public event EventHandler ErrorDetected;

        public FormatInfoSnapshot()
        {
            this.CellValues = new Dictionary<string, object>();
            this.ListIndexCellValues = new Dictionary<ListIndexKey, object>();
            this.TotalSummaryValues = new Dictionary<TotalSummaryKey, object>();
        }

        object IFormatInfoProvider.GetCellValue(string fieldName) => 
            this.GetCachedValue<string>(fieldName, this.CellValues);

        object IFormatInfoProvider.GetCellValueByListIndex(int listIndex, string fieldName) => 
            this.GetCachedValue<ListIndexKey>(new ListIndexKey(listIndex, fieldName), this.ListIndexCellValues);

        object IFormatInfoProvider.GetTotalSummaryValue(string fieldName, ConditionalFormatSummaryType summaryType) => 
            this.GetCachedValue<TotalSummaryKey>(new TotalSummaryKey(summaryType, fieldName), this.TotalSummaryValues);

        private object GetCachedValue<T>(T key, Dictionary<T, object> dictionary)
        {
            if (dictionary != null)
            {
                object obj2 = null;
                if (dictionary.TryGetValue(key, out obj2))
                {
                    return obj2;
                }
            }
            this.RaiseErrorDetected();
            return InvalidFormatCache.Instance;
        }

        private void RaiseErrorDetected()
        {
            EventHandler errorDetected = this.ErrorDetected;
            if (errorDetected != null)
            {
                errorDetected(this, new EventArgs());
            }
        }

        public ValueComparer CurrentValueComparer { get; set; }

        public Dictionary<string, object> CellValues { get; set; }

        public Dictionary<ListIndexKey, object> ListIndexCellValues { get; set; }

        public Dictionary<TotalSummaryKey, object> TotalSummaryValues { get; set; }

        ValueComparer IFormatInfoProvider.ValueComparer
        {
            get
            {
                if (this.CurrentValueComparer == null)
                {
                    this.RaiseErrorDetected();
                }
                return this.CurrentValueComparer;
            }
        }
    }
}

