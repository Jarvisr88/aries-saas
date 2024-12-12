namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class PropertyExtendedEntryContext : ValueExtendedEntryContext
    {
        protected PropertyExtendedEntryContext(byte type);

        public short Attribute { get; protected set; }
    }
}

