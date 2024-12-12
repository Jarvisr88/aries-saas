namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FormatValueProvider
    {
        private readonly IFormatInfoProvider provider;
        private readonly object value;
        private readonly string fieldName;
        public FormatValueProvider(IFormatInfoProvider provider, object value, string fieldName)
        {
            this.provider = provider;
            this.value = value;
            this.fieldName = fieldName;
        }

        public object Value =>
            this.value;
        public DevExpress.Data.ValueComparer ValueComparer =>
            this.provider.ValueComparer;
        public object GetTotalSummaryValue(ConditionalFormatSummaryType summaryType) => 
            this.provider.GetTotalSummaryValue(this.fieldName, summaryType);

        public object GetValueByListIndex(int listIndex) => 
            this.provider.GetCellValueByListIndex(listIndex, this.fieldName);

        public bool GetSelectiveValue(string name) => 
            FormatConditionBaseInfo.IsFit(this.provider.GetCellValue(name));

        public object GetCellValue(string name) => 
            this.provider.GetCellValue(name);
    }
}

