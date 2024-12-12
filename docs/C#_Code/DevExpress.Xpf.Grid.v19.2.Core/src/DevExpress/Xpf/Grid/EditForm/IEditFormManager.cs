namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public interface IEditFormManager
    {
        EditFormRowData GetInplaceData(int rowHandle);
        bool IsInlineFormChild(DependencyObject source);
        void OnAfterScroll();
        void OnBeforeScroll(int targetRowHandle);
        void OnDoubleClick(MouseButtonEventArgs e);
        void OnInlineFormClosed(bool success);
        void OnIsModifiedChanged(bool isModified);
        void OnPreviewKeyDown(KeyEventArgs e);
        bool RequestUIUpdate();

        bool IsEditFormVisible { get; }

        bool IsEditFormModified { get; }

        bool AllowEditForm { get; }

        Locker EditFormOpenLocker { get; }
    }
}

