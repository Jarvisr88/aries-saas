namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class FilterEnumEditorSettings : FilterLookupEditorSettings
    {
        public FilterEnumEditorSettings(LookupUIEditorType editorType, bool useFlags);

        public bool UseFlags { get; private set; }
    }
}

