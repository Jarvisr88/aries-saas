namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class CustomOperatorMenuItemIdentity : IFilterEditorOperatorMenuItemIdentity, IExcelStyleOperatorMenuItemIdentity
    {
        public CustomOperatorMenuItemIdentity(string name)
        {
            this.<Name>k__BackingField = name;
        }

        public override bool Equals(object obj)
        {
            CustomOperatorMenuItemIdentity identity = obj as CustomOperatorMenuItemIdentity;
            return ((identity != null) && (this.Name == identity.Name));
        }

        public override int GetHashCode() => 
            0x202169f6 + EqualityComparer<string>.Default.GetHashCode(this.Name);

        public string Name { get; }
    }
}

