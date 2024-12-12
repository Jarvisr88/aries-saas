namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class VirtualServerModeCanPerformColumnServerActionEventArgs : EventArgs
    {
        private readonly string _ColumnName;
        private readonly ColumnServerActionType _ActionType;

        public VirtualServerModeCanPerformColumnServerActionEventArgs(string columnName, ColumnServerActionType actionType);

        public bool? AllowAction { get; set; }

        public string ColumnName { get; }

        public ColumnServerActionType ActionType { get; }
    }
}

