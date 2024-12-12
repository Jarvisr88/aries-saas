namespace DevExpress.Data.Selection
{
    using DevExpress.Data;
    using System;

    public class CardCollapsedRowsCollection : SelectedRowsCollection
    {
        public CardCollapsedRowsCollection(CardSelectionController selectionController);
        protected override void OnSelectionChanged(SelectionChangedEventArgs e);
    }
}

