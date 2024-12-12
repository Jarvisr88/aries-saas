namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class EditorInitializedEventArgs : EventArgs
    {
        public EditorInitializedEventArgs(ColumnBase column, IBaseEdit editor)
        {
            this.Column = column;
            this.Editor = editor;
        }

        public ColumnBase Column { get; private set; }

        public IBaseEdit Editor { get; private set; }
    }
}

