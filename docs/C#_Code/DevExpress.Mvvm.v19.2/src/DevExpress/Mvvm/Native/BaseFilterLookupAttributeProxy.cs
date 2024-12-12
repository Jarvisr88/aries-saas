namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseFilterLookupAttributeProxy : FilterAttributeProxy
    {
        protected BaseFilterLookupAttributeProxy()
        {
        }

        public FilterLookupUIEditorType EditorType { get; set; }

        public bool? UseFlags { get; set; }

        public bool? UseSelectAll { get; set; }

        public string SelectAllName { get; set; }
    }
}

