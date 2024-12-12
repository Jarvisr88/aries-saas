namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Core;
    using System;

    public class CheckedUncheckedBoolToStringConverter : BoolToStringConverter
    {
        public CheckedUncheckedBoolToStringConverter() : base(EditorLocalizer.GetString(EditorStringId.FilterEditorChecked), EditorLocalizer.GetString(EditorStringId.FilterEditorUnchecked))
        {
        }
    }
}

