namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ExcelStyleFilterElementOperatorItemList : List<ExcelStyleFilterElementOperatorItem>
    {
        private OperatorItemListCore<ExcelStyleFilterElementOperatorItem, ExcelStyleFilterElementOperatorType> core;

        internal ExcelStyleFilterElementOperatorItemList(IEnumerable<ExcelStyleFilterElementOperatorItem> collection) : base(collection)
        {
        }

        public bool Remove(ExcelStyleFilterElementOperatorType type) => 
            this.Core.Remove(type);

        private OperatorItemListCore<ExcelStyleFilterElementOperatorItem, ExcelStyleFilterElementOperatorType> Core
        {
            get
            {
                if (this.core == null)
                {
                    Func<ExcelStyleFilterElementOperatorItem, ExcelStyleFilterElementOperatorType?> getKey = <>c.<>9__3_0;
                    if (<>c.<>9__3_0 == null)
                    {
                        Func<ExcelStyleFilterElementOperatorItem, ExcelStyleFilterElementOperatorType?> local1 = <>c.<>9__3_0;
                        getKey = <>c.<>9__3_0 = x => x.OperatorType;
                    }
                    this.core = new OperatorItemListCore<ExcelStyleFilterElementOperatorItem, ExcelStyleFilterElementOperatorType>(this, getKey);
                }
                return this.core;
            }
        }

        public ExcelStyleFilterElementOperatorItem this[ExcelStyleFilterElementOperatorType type]
        {
            get => 
                this.Core.GetItem(type);
            set => 
                this.Core.SetItem(type, value);
        }

        public ExcelStyleFilterElementOperatorItem this[int index]
        {
            get => 
                base[index];
            set => 
                base[index] = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelStyleFilterElementOperatorItemList.<>c <>9 = new ExcelStyleFilterElementOperatorItemList.<>c();
            public static Func<ExcelStyleFilterElementOperatorItem, ExcelStyleFilterElementOperatorType?> <>9__3_0;

            internal ExcelStyleFilterElementOperatorType? <get_Core>b__3_0(ExcelStyleFilterElementOperatorItem x) => 
                x.OperatorType;
        }
    }
}

