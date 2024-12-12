namespace Devart.Data.MySql
{
    using System;

    public enum MySqlLoaderPriority
    {
        public const MySqlLoaderPriority None = MySqlLoaderPriority.None;,
        public const MySqlLoaderPriority Low = MySqlLoaderPriority.Low;,
        public const MySqlLoaderPriority Concurrent = MySqlLoaderPriority.Concurrent;
    }
}

