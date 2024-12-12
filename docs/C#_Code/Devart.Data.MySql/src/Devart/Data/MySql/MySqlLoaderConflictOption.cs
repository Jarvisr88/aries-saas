namespace Devart.Data.MySql
{
    using System;

    public enum MySqlLoaderConflictOption
    {
        public const MySqlLoaderConflictOption None = MySqlLoaderConflictOption.None;,
        public const MySqlLoaderConflictOption Replace = MySqlLoaderConflictOption.Replace;,
        public const MySqlLoaderConflictOption Ignore = MySqlLoaderConflictOption.Ignore;
    }
}

