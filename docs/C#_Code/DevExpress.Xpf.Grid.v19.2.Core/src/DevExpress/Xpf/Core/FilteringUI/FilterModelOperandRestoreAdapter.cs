namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class FilterModelOperandRestoreAdapter
    {
        public FilterModelOperandRestoreAdapter(FilterModelBase model, Func<OperandValuesRecord> save, Action<OperandValuesRecord> restore)
        {
            Guard.ArgumentNotNull(save, "save");
            Guard.ArgumentNotNull(restore, "restore");
            this.<Model>k__BackingField = model;
            this.<Save>k__BackingField = save;
            this.<Restore>k__BackingField = restore;
        }

        public FilterModelBase Model { get; }

        public Func<OperandValuesRecord> Save { get; }

        public Action<OperandValuesRecord> Restore { get; }
    }
}

