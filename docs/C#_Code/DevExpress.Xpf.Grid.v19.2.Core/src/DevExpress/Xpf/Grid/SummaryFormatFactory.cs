namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    internal class SummaryFormatFactory
    {
        private const string NoneLocalizedFormat = "{0}";

        public string CreateFormat(SummaryItemType summaryType)
        {
            GridControlStringId? localizationId = this.GetLocalizationId(summaryType);
            return ((localizationId != null) ? GridControlLocalizer.GetString(localizationId.Value) : "{0}");
        }

        private GridControlStringId? GetLocalizationId(SummaryItemType summaryType)
        {
            switch (summaryType)
            {
                case SummaryItemType.Sum:
                    return this.Sum;

                case SummaryItemType.Min:
                    return this.Min;

                case SummaryItemType.Max:
                    return this.Max;

                case SummaryItemType.Count:
                    return this.Count;

                case SummaryItemType.Average:
                    return this.Average;
            }
            return null;
        }

        public GridControlStringId? Sum { get; set; }

        public GridControlStringId? Min { get; set; }

        public GridControlStringId? Max { get; set; }

        public GridControlStringId? Count { get; set; }

        public GridControlStringId? Average { get; set; }
    }
}

