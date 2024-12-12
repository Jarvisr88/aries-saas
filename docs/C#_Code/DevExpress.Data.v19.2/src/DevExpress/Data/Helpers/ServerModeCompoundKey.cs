namespace DevExpress.Data.Helpers
{
    using System;

    public sealed class ServerModeCompoundKey
    {
        public readonly object[] SubKeys;

        public ServerModeCompoundKey(object[] subKeys);
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

