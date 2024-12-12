namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class FilterEditorAttribute : FilterAttribute
    {
        public FilterEditorAttribute(BooleanUIEditorType editorType);
        public FilterEditorAttribute(DateTimeRangeUIEditorType editorType);
        public FilterEditorAttribute(GroupUIEditorType editorType);
        public FilterEditorAttribute(LookupUIEditorType editorType);
        public FilterEditorAttribute(RangeUIEditorType editorType);
        public FilterEditorAttribute(LookupUIEditorType editorType, bool useFlags);

        public FilterRangeEditorSettings RangeEditorSettings { get; private set; }

        public FilterLookupEditorSettings LookupEditorSettings { get; private set; }

        public FilterBooleanEditorSettings BooleanEditorSettings { get; private set; }

        public FilterEnumEditorSettings EnumEditorSettings { get; private set; }

        public FilterGroupEditorSettings GroupEditorSettings { get; private set; }
    }
}

