namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public class EditingFieldEventArgs : EventArgs
    {
        public EditingFieldEventArgs(DevExpress.XtraPrinting.EditingField editingField)
        {
            this.EditingField = editingField;
        }

        public DevExpress.XtraPrinting.EditingField EditingField { get; private set; }
    }
}

