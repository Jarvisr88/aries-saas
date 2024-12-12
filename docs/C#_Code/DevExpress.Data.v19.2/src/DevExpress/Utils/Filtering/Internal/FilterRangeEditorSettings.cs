namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class FilterRangeEditorSettings
    {
        public FilterRangeEditorSettings(DateTimeRangeUIEditorType editorType);
        public FilterRangeEditorSettings(RangeUIEditorType editorType);

        public RangeUIEditorType? NumericEditorType { get; private set; }

        public DateTimeRangeUIEditorType? DateTimeEditorType { get; private set; }
    }
}

