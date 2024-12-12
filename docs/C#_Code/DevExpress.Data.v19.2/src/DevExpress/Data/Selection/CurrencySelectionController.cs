namespace DevExpress.Data.Selection
{
    using DevExpress.Data;
    using System;

    public class CurrencySelectionController : SelectionController
    {
        public CurrencySelectionController(DataController controller);
        private int[] CheckNormalizedSelectedRows(int[] rows);
        public override int[] GetNormalizedSelectedRows();
        public override int[] GetNormalizedSelectedRowsEx();

        protected internal CurrencyDataController Controller { get; }
    }
}

