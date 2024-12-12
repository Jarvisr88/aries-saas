namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FieldList : List<FieldItem>
    {
        internal FieldList(IEnumerable<FieldItem> collection) : base(collection)
        {
        }

        public FieldItem this[string fieldName]
        {
            get
            {
                Func<FieldItem, IEnumerable<FieldItem>> getItems = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<FieldItem, IEnumerable<FieldItem>> local1 = <>c.<>9__2_0;
                    getItems = <>c.<>9__2_0 = x => x.Children;
                }
                return this.Flatten<FieldItem>(getItems).FirstOrDefault<FieldItem>(x => (x.FieldName == fieldName));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FieldList.<>c <>9 = new FieldList.<>c();
            public static Func<FieldItem, IEnumerable<FieldItem>> <>9__2_0;

            internal IEnumerable<FieldItem> <get_Item>b__2_0(FieldItem x) => 
                x.Children;
        }
    }
}

