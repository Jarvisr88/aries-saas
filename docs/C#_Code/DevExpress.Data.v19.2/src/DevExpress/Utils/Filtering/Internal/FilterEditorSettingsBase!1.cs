namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class FilterEditorSettingsBase<TEditorType> where TEditorType: struct
    {
        public FilterEditorSettingsBase(TEditorType editorType);

        public TEditorType EditorType { get; private set; }
    }
}

