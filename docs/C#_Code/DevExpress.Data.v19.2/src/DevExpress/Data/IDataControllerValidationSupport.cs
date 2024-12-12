namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public interface IDataControllerValidationSupport
    {
        void OnBeginCurrentRowEdit();
        void OnControllerItemChanged(ListChangedEventArgs e);
        void OnCurrentRowUpdated(ControllerRowEventArgs e);
        void OnEndNewItemRow();
        void OnPostCellException(ControllerRowCellExceptionEventArgs e);
        void OnPostRowException(ControllerRowExceptionEventArgs e);
        void OnStartNewItemRow();
        void OnValidatingCurrentRow(ValidateControllerRowEventArgs e);

        IBoundControl BoundControl { get; }
    }
}

