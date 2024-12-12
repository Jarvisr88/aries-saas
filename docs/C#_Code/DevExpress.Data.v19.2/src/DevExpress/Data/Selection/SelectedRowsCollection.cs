namespace DevExpress.Data.Selection
{
    using DevExpress.Data.Helpers;
    using System;

    public class SelectedRowsCollection : BaseSelectionCollection<int?>, IIndexRenumber
    {
        private int[] indexes;

        public SelectedRowsCollection(SelectionController selectionController);
        protected internal override int CalcCRC();
        public int[] CopyToArray();
        public int[] CopyToArray(int length);
        public void CopyToArray(int[] array, int startIndex);
        int IIndexRenumber.GetCount();
        int IIndexRenumber.GetValue(int pos);
        void IIndexRenumber.SetValue(int pos, int val);
        protected internal override int? GetRowObjectByControllerRow(int controllerRow);
        protected internal void OnItemAdded(int listSourceRow);
        protected internal virtual void OnItemDeleted(int listSourceRow);
        protected internal virtual void OnItemFilteredOut(int listSourceRow);
        protected internal void OnItemMoved(int oldListSourceRow, int newListSourceRow);
        private void PrepareReindex(bool increment);
        private void RenumberIndexes(int listSourceRow, bool increment);
        private void RenumberIndexes(int oldListSourceRow, int newListSourceRow);
        private void SetListSourceRowSelected(int listSourceRow, bool selected, object selectionObject);
    }
}

