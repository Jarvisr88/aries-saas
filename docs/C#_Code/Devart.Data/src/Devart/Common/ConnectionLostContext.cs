namespace Devart.Common
{
    using System;

    public enum ConnectionLostContext
    {
        None,
        HasPrepared,
        InTransaction,
        InFetch
    }
}

