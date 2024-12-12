namespace DevExpress.Xpf.Data
{
    using System;

    public interface ISelectionController
    {
        void BeginSelection();
        void Clear();
        void EndSelection();
        bool GetSelected(int controllerRow);
        object GetSelectedObject(int controllerRow);
        int[] GetSelectedRows();
        void SelectAll();
        void SetActuallyChanged();
        void SetSelected(int rowHandle, bool selected);
        void SetSelected(int rowHandle, bool selected, object selectionObject);

        bool IsSelectionLocked { get; }

        int Count { get; }
    }
}

