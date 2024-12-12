namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class GetActiveEditorNeedsKeyEventArgs : EditorEventArgsBase
    {
        protected internal GetActiveEditorNeedsKeyEventArgs(System.Windows.Input.Key key, ModifierKeys modifiers, DependencyObject templateChild, bool needsKey, DataViewBase view, int rowHandle, ColumnBase column) : base(DataViewBase.GetActiveEditorNeedsKeyEvent, view, rowHandle, column)
        {
            this.Key = key;
            this.Modifiers = modifiers;
            this.TemplateChild = templateChild;
            this.NeedsKey = needsKey;
        }

        public System.Windows.Input.Key Key { get; private set; }

        public ModifierKeys Modifiers { get; private set; }

        public DependencyObject TemplateChild { get; private set; }

        public bool NeedsKey { get; set; }
    }
}

