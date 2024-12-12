namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class BuiltInExcelStyleOperatorMenuItemIdentity : IExcelStyleOperatorMenuItemIdentity
    {
        public BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType type)
        {
            this.<Type>k__BackingField = type;
        }

        public override bool Equals(object obj)
        {
            BuiltInExcelStyleOperatorMenuItemIdentity identity = obj as BuiltInExcelStyleOperatorMenuItemIdentity;
            return ((identity != null) && (this.Type == identity.Type));
        }

        public override int GetHashCode() => 
            0x7a239275 + this.Type.GetHashCode();

        public ExcelStyleFilterElementOperatorType Type { get; }
    }
}

