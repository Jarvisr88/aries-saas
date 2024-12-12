namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Summary;
    using System;

    public class GridGroupFooterSummaryHelper : GridTotalSummaryHelper
    {
        public GridGroupFooterSummaryHelper(DataViewBase view, Func<ColumnBase> getColumn) : base(view, getColumn)
        {
        }

        protected override bool AreItemsEqual(SummaryItemBase item1, SummaryItemBase item2) => 
            base.AreItemsEqual(item1, item2) && (((IGroupFooterSummaryItem) item1).ShowInGroupColumnFooter == ((IGroupFooterSummaryItem) item2).ShowInGroupColumnFooter);

        protected internal override bool CanUseSummaryItem(ISummaryItem item) => 
            ((IGroupFooterSummaryItem) item).ShowInGroupColumnFooter == base.Column.FieldName;

        protected override string GetEditorTitle() => 
            string.Format(base.view.GetLocalizedString(GridControlStringId.TotalSummaryEditorFormCaption), base.Column.HeaderCaption.ToString());

        protected override void InitializeSummaryItemCore(SummaryItemBase item)
        {
            base.InitializeSummaryItemCore(item);
            ((IGroupFooterSummaryItem) item).ShowInGroupColumnFooter = base.Column.FieldName;
        }

        public override bool IsSummaryItemExists(ISummaryItem item) => 
            base.view.DataProviderBase.IsSummaryItemExists(SummaryItemCollectionType.Group, (SummaryItemBase) item);

        protected override ISummaryItemOwner SummaryItems =>
            base.view.DataControl.DataProviderBase.GroupSummaryCore;
    }
}

