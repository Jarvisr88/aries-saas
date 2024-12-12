namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class ActivatingKeyEventArgs : EventArgs
    {
        public ActivatingKeyEventArgs(System.Windows.Input.Key key, ModifierKeys modifiers, IBaseEdit baseEdit)
        {
            this.Key = key;
            this.Modifiers = modifiers;
            this.BaseEdit = baseEdit;
        }

        public System.Windows.Input.Key Key { get; private set; }

        public ModifierKeys Modifiers { get; private set; }

        public IBaseEdit BaseEdit { get; private set; }
    }
}

