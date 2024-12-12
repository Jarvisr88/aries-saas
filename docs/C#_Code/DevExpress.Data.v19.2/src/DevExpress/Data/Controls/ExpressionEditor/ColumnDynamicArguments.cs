namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;
    using System.Runtime.CompilerServices;

    public class ColumnDynamicArguments
    {
        public ColumnDynamicArguments(string columnName, string expression, int caretPosition);
        internal ColumnDynamicArguments(string columnName, string expression, int caretPosition, bool insertingColumnName);

        public string ColumnName { get; }

        public string Expression { get; }

        public int CaretPosition { get; }

        internal bool InsertingColumnName { get; }
    }
}

