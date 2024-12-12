namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class BuiltInFilterEditorOperatorMenuItemIdentity : IFilterEditorOperatorMenuItemIdentity
    {
        public BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType type)
        {
            this.<Type>k__BackingField = type;
        }

        public override bool Equals(object obj)
        {
            BuiltInFilterEditorOperatorMenuItemIdentity identity = obj as BuiltInFilterEditorOperatorMenuItemIdentity;
            return ((identity != null) && (this.Type == identity.Type));
        }

        public override int GetHashCode() => 
            0x7a239275 + this.Type.GetHashCode();

        public FilterEditorOperatorType Type { get; }
    }
}

