namespace Devart.Data.MySql
{
    using System;
    using System.Data;
    using System.Data.Common;

    public class MySqlRowUpdatingEventArgs : RowUpdatingEventArgs
    {
        public MySqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping);

        public MySqlCommand Command { get; set; }
    }
}

