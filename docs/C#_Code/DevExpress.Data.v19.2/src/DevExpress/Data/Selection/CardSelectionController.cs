namespace DevExpress.Data.Selection
{
    using DevExpress.Data;
    using System;

    public class CardSelectionController : CurrencySelectionController
    {
        private CardCollapsedRowsCollection collapsedRows;

        public CardSelectionController(DataController controller);
        public void CollapseAll();
        public void ExpandAll();
        public bool GetCollapsed(int controllerRow);
        public void SetCollapsed(int controllerRow, bool collapsed);

        public CardCollapsedRowsCollection CollapsedRows { get; }

        public int CollapsedCount { get; }
    }
}

