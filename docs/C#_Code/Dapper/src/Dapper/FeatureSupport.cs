namespace Dapper
{
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    internal class FeatureSupport
    {
        private static readonly FeatureSupport Default = new FeatureSupport(false);
        private static readonly FeatureSupport Postgres = new FeatureSupport(true);

        private FeatureSupport(bool arrays)
        {
            this.<Arrays>k__BackingField = arrays;
        }

        public static FeatureSupport Get(IDbConnection connection) => 
            !string.Equals(connection?.GetType().Name, "npgsqlconnection", StringComparison.OrdinalIgnoreCase) ? Default : Postgres;

        public bool Arrays { get; }
    }
}

