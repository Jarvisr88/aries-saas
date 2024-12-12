namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;

    public class DataTreeBuilderFormatInfoProvider<T> : IFormatInfoProvider
    {
        private readonly T owner;
        private readonly Func<T, string, object> valueAccessor;
        private readonly Func<T, DataTreeBuilder> treeBuilderAccessor;

        public DataTreeBuilderFormatInfoProvider(T owner, Func<T, DataTreeBuilder> treeBuilderAccessor, Func<T, string, object> valueAccessor)
        {
            this.owner = owner;
            this.treeBuilderAccessor = treeBuilderAccessor;
            this.valueAccessor = valueAccessor;
        }

        object IFormatInfoProvider.GetCellValue(string fieldName) => 
            this.valueAccessor(this.owner, fieldName);

        object IFormatInfoProvider.GetCellValueByListIndex(int listIndex, string fieldName) => 
            this.TreeBuilder.View.DataControl.DataProviderBase.GetFormatInfoCellValueByListIndex(listIndex, fieldName);

        object IFormatInfoProvider.GetTotalSummaryValue(string fieldName, ConditionalFormatSummaryType summaryType) => 
            this.TreeBuilder.GetServiceTotalSummaryValue(fieldName, summaryType);

        private DataTreeBuilder TreeBuilder =>
            this.treeBuilderAccessor(this.owner);

        ValueComparer IFormatInfoProvider.ValueComparer =>
            this.TreeBuilder.View.DataControl.DataProviderBase.ValueComparer;
    }
}

