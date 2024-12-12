namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using DevExpress.Utils.Filtering.Internal;
    using System;

    public abstract class EditorsGeneratorFilteringBase : EditorsGeneratorBase
    {
        protected EditorsGeneratorFilteringBase()
        {
        }

        public abstract void FilterBooleanChoice(IEdmPropertyInfo property, FilterBooleanEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterBooleanChoiceProperty(IEdmPropertyInfo property, FilterBooleanEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterEnumChoice(IEdmPropertyInfo property, FilterEnumEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterEnumChoiceProperty(IEdmPropertyInfo property, FilterEnumEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterLookup(IEdmPropertyInfo property, FilterLookupEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterLookupProperty(IEdmPropertyInfo property, FilterLookupEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterRange(IEdmPropertyInfo property, FilterRangeEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
        public abstract void FilterRangeProperty(IEdmPropertyInfo property, FilterRangeEditorSettings settings, EditorsGeneratorBase.Initializer initializer);
    }
}

