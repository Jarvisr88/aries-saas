namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FilterEditorOperatorItemList : List<FilterEditorOperatorItem>
    {
        private OperatorItemListCore<FilterEditorOperatorItem, FilterEditorOperatorType> core;

        internal FilterEditorOperatorItemList(IEnumerable<FilterEditorOperatorItem> collection) : base(collection)
        {
        }

        public bool Remove(FilterEditorOperatorType type) => 
            this.Core.Remove(type);

        private OperatorItemListCore<FilterEditorOperatorItem, FilterEditorOperatorType> Core
        {
            get
            {
                if (this.core == null)
                {
                    Func<FilterEditorOperatorItem, FilterEditorOperatorType?> getKey = <>c.<>9__3_0;
                    if (<>c.<>9__3_0 == null)
                    {
                        Func<FilterEditorOperatorItem, FilterEditorOperatorType?> local1 = <>c.<>9__3_0;
                        getKey = <>c.<>9__3_0 = x => x.OperatorType;
                    }
                    this.core = new OperatorItemListCore<FilterEditorOperatorItem, FilterEditorOperatorType>(this, getKey);
                }
                return this.core;
            }
        }

        public FilterEditorOperatorItem this[FilterEditorOperatorType type]
        {
            get => 
                this.Core.GetItem(type);
            set => 
                this.Core.SetItem(type, value);
        }

        public FilterEditorOperatorItem this[int index]
        {
            get => 
                base[index];
            set => 
                base[index] = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorOperatorItemList.<>c <>9 = new FilterEditorOperatorItemList.<>c();
            public static Func<FilterEditorOperatorItem, FilterEditorOperatorType?> <>9__3_0;

            internal FilterEditorOperatorType? <get_Core>b__3_0(FilterEditorOperatorItem x) => 
                x.OperatorType;
        }
    }
}

