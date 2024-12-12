namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ProcessActivatingKeyEventArgs : ActivatingKeyEventArgs
    {
        public ProcessActivatingKeyEventArgs(Key key, ModifierKeys modifiers, IBaseEdit baseEdit) : base(key, modifiers, baseEdit)
        {
            this.IsProcessed = false;
        }

        public bool IsProcessed { get; set; }

        public FrameworkElement EditCore =>
            base.BaseEdit.EditCore;
    }
}

