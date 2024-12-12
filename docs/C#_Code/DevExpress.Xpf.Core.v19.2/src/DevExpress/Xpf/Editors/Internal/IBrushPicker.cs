namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public interface IBrushPicker
    {
        event EventHandler BrushChanged;

        bool NeedsKey(Key key, ModifierKeys modifiers);
        void PerformSync(object value);

        bool HasFocus { get; }

        object Brush { get; set; }

        DevExpress.Xpf.Editors.BrushType BrushType { get; set; }
    }
}

