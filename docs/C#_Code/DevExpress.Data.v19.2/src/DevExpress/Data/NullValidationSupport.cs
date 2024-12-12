namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    internal class NullValidationSupport : IDataControllerValidationSupport
    {
        internal static NullValidationSupport Default;

        static NullValidationSupport();
        public void OnBeginCurrentRowEdit();
        public void OnControllerItemChanged(ListChangedEventArgs e);
        public void OnCurrentRowUpdated(ControllerRowEventArgs e);
        public void OnEndNewItemRow();
        public void OnPostCellException(ControllerRowCellExceptionEventArgs e);
        public void OnPostRowException(ControllerRowExceptionEventArgs e);
        public void OnStartNewItemRow();
        public void OnValidatingCurrentRow(ValidateControllerRowEventArgs e);

        public IBoundControl BoundControl { get; }
    }
}

