namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Summary;
    using DevExpress.Xpf.Core;
    using System;

    public class GridTotalSummaryPanelHelper : GridTotalSummaryHelper
    {
        public GridTotalSummaryPanelHelper(DataViewBase view) : base(view, null)
        {
        }

        public GridTotalSummaryPanelHelper(DataViewBase view, Func<ColumnBase> getColumn) : base(view, getColumn)
        {
        }

        protected internal override bool CanUseSummaryItem(ISummaryItem item) => 
            true;

        protected override GridSummaryItemsEditorController CreateSummaryItemController() => 
            new GridSummaryPanelItemsEditorController(this);

        protected override string GetEditorTitle() => 
            base.view.GetLocalizedString(GridControlStringId.TotalSummaryPanelEditorFormCaption);

        protected override void InitializeSummaryItemCore(SummaryItemBase item)
        {
            item.Alignment = GridSummaryItemAlignment.Right;
        }

        internal override bool PassesSummaryLimitations(ColumnBase column) => 
            column.ActualAllowTotalSummary();

        public override FloatingContainer ShowSummaryEditor() => 
            this.ShowSummaryEditor(SummaryEditorType.TotalSummaryPanel);
    }
}

