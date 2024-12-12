namespace DevExpress.Office.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public interface IEditValueBarItem
    {
        event EditValueChangedEventHandler EditValueChanged;

        object EditValue { get; set; }

        ICommand Command { get; set; }
    }
}

