namespace DevExpress.Xpf.Editors.Internal
{
    using System;

    public interface ISelectionProvider
    {
        void SelectAll();
        void SetSelectAll(bool? value);
        void UnselectAll();
    }
}

