namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public sealed class FilterEnumChoiceAttribute : BaseFilterLookupAttribute
    {
        internal bool? useFlags;

        public FilterEnumChoiceAttribute()
        {
        }

        public FilterEnumChoiceAttribute(DevExpress.Utils.Filtering.FlagComparisonRule flagComparisonRule)
        {
            this.FlagComparisonRule = flagComparisonRule;
        }

        public FilterEnumChoiceAttribute(bool useFlags)
        {
            this.useFlags = new bool?(useFlags);
        }

        public LookupUIEditorType EditorType { get; set; }

        public bool UseFlags
        {
            get => 
                this.useFlags.GetValueOrDefault(true);
            set => 
                this.useFlags = new bool?(value);
        }

        public DevExpress.Utils.Filtering.FlagComparisonRule FlagComparisonRule { get; set; }
    }
}

