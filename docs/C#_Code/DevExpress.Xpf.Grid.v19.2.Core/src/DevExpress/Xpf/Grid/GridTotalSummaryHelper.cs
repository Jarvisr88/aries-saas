namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Summary;
    using System;

    public class GridTotalSummaryHelper : GridSummaryHelper
    {
        private readonly Func<ColumnBase> getColumn;

        public GridTotalSummaryHelper(DataViewBase view, Func<ColumnBase> getColumn) : base(view)
        {
            this.getColumn = getColumn;
        }

        protected internal override bool CanUseSummaryItem(ISummaryItem item)
        {
            DevExpress.Xpf.Grid.SummaryItemBase base2 = item as DevExpress.Xpf.Grid.SummaryItemBase;
            bool flag = !string.IsNullOrEmpty(base2.ShowInColumn);
            return (((item.FieldName == this.Column.FieldName) || (!flag || (base2.ShowInColumn != this.Column.FieldName))) ? (((item.FieldName != this.Column.FieldName) || (!flag || (base2.ShowInColumn == this.Column.FieldName))) ? (item.FieldName == this.Column.FieldName) : false) : true);
        }

        protected override DevExpress.Xpf.Grid.SummaryItemBase CreateItemCore(string fieldName, SummaryItemType summaryType)
        {
            DevExpress.Xpf.Grid.SummaryItemBase item = base.CreateItemCore(fieldName, summaryType);
            this.InitializeSummaryItemCore(item);
            return item;
        }

        protected override string GetEditorTitle() => 
            string.Format(base.view.GetLocalizedString(GridControlStringId.TotalSummaryEditorFormCaption), this.Column.HeaderCaption.ToString());

        protected virtual void InitializeSummaryItemCore(DevExpress.Xpf.Grid.SummaryItemBase item)
        {
            item.ShowInColumn = this.Column.FieldName;
        }

        public override bool IsSummaryItemExists(ISummaryItem item) => 
            base.view.DataProviderBase.IsSummaryItemExists(SummaryItemCollectionType.Total, (DevExpress.Xpf.Grid.SummaryItemBase) item);

        internal override bool PassesSummaryLimitations(ColumnBase column) => 
            base.view.ActualAllowCountTotalSummary || column.ActualAllowTotalSummary();

        protected ColumnBase Column =>
            this.getColumn();

        protected override ISummaryItemOwner SummaryItems =>
            base.view.DataControl.DataProviderBase.TotalSummaryCore;
    }
}

