namespace Devart.Data.MySql
{
    using System;

    public enum MySqlDependencyCheckType
    {
        public const MySqlDependencyCheckType Default = MySqlDependencyCheckType.Default;,
        public const MySqlDependencyCheckType Timestamp = MySqlDependencyCheckType.Timestamp;,
        public const MySqlDependencyCheckType Checksum = MySqlDependencyCheckType.Checksum;
    }
}

