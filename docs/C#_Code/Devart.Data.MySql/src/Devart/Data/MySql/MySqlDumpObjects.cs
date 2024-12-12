namespace Devart.Data.MySql
{
    using System;

    [Flags]
    public enum MySqlDumpObjects
    {
        public const MySqlDumpObjects None = MySqlDumpObjects.None;,
        public const MySqlDumpObjects Database = MySqlDumpObjects.Database;,
        public const MySqlDumpObjects Events = MySqlDumpObjects.Events;,
        public const MySqlDumpObjects Functions = MySqlDumpObjects.Functions;,
        public const MySqlDumpObjects Procedures = MySqlDumpObjects.Procedures;,
        public const MySqlDumpObjects Tables = MySqlDumpObjects.Tables;,
        public const MySqlDumpObjects Triggers = MySqlDumpObjects.Triggers;,
        public const MySqlDumpObjects Views = MySqlDumpObjects.Views;,
        public const MySqlDumpObjects Udfs = MySqlDumpObjects.Udfs;,
        public const MySqlDumpObjects Users = MySqlDumpObjects.Users;,
        public const MySqlDumpObjects All = MySqlDumpObjects.All;
    }
}

