namespace DevExpress.Data.Linq.Helpers
{
    using System;

    [Flags]
    internal enum EntityQueryTypeCaps
    {
        public const EntityQueryTypeCaps None = EntityQueryTypeCaps.None;,
        public const EntityQueryTypeCaps EntityFunctions = EntityQueryTypeCaps.EntityFunctions;,
        public const EntityQueryTypeCaps ObjectToString = EntityQueryTypeCaps.ObjectToString;
    }
}

