namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class GetIsActivatingKeyEventArgs : ActivatingKeyEventArgs
    {
        public GetIsActivatingKeyEventArgs(Key key, ModifierKeys modifiers, IBaseEdit baseEdit, bool isActivatingKey) : base(key, modifiers, baseEdit)
        {
            this.IsActivatingKey = isActivatingKey;
        }

        public bool IsActivatingKey { get; set; }
    }
}

