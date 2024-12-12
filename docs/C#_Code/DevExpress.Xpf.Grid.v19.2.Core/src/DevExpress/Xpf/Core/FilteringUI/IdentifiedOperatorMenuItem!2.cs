namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class IdentifiedOperatorMenuItem<TID, TValue>
    {
        public IdentifiedOperatorMenuItem(TID id, TValue value)
        {
            Guard.ArgumentNotNull(id, "id");
            this.<ID>k__BackingField = id;
            this.<Value>k__BackingField = value;
        }

        public TID ID { get; }

        public TValue Value { get; }
    }
}

