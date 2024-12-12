namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;

    public class EditorScrollBehavior : NativeScrollBehavior
    {
        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source)
        {
            BaseEdit edit = source as BaseEdit;
            return ((edit != null) ? ((edit.EditMode != EditMode.InplaceInactive) ? ((edit is TextEdit) ? (((TextEdit) edit).AllowSpinOnMouseWheel ? edit.IsEditorKeyboardFocused : false) : false) : false) : false);
        }
    }
}

