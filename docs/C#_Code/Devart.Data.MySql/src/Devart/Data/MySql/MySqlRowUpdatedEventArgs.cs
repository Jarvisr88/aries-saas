namespace Devart.Data.MySql
{
    using System;
    using System.Data;
    using System.Data.Common;

    public class MySqlRowUpdatedEventArgs : RowUpdatedEventArgs
    {
        public MySqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping);

        public MySqlCommand Command { get; }
    }
}

