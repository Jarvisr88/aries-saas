namespace DevExpress.Data.Controls
{
    using System;
    using System.Runtime.CompilerServices;

    public class ControlRowSourceChangedEventArgs : EventArgs
    {
        public ControlRowSourceChangedEventArgs(ControlRows changedRows);

        public ControlRows ChangedRows { get; private set; }
    }
}

