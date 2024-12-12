namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class ConditionalFormatSummaryInfo
    {
        public ConditionalFormatSummaryInfo(ConditionalFormatSummaryType summaryType, string fieldName)
        {
            this.SummaryType = summaryType;
            string text1 = fieldName;
            if (fieldName == null)
            {
                string local1 = fieldName;
                text1 = string.Empty;
            }
            this.FieldName = text1;
        }

        public override bool Equals(object obj)
        {
            ConditionalFormatSummaryInfo info = obj as ConditionalFormatSummaryInfo;
            return ((info != null) && ((info.FieldName == this.FieldName) && (info.SummaryType == this.SummaryType)));
        }

        public override int GetHashCode() => 
            this.SummaryType.GetHashCode() ^ this.FieldName.GetHashCode();

        private static SummaryItemType ToSummaryItemType(ConditionalFormatSummaryType summaryType, Type type)
        {
            switch (summaryType)
            {
                case ConditionalFormatSummaryType.Min:
                    return SummaryItemType.Min;

                case ConditionalFormatSummaryType.Max:
                    return SummaryItemType.Max;

                case ConditionalFormatSummaryType.Average:
                    return ((type != typeof(DateTime)) ? SummaryItemType.Average : SummaryItemType.Custom);

                case ConditionalFormatSummaryType.SortedList:
                    return SummaryItemType.Custom;
            }
            throw new InvalidOperationException();
        }

        public ConditionalFormatSummaryType SummaryType { get; private set; }

        public string FieldName { get; private set; }
    }
}

