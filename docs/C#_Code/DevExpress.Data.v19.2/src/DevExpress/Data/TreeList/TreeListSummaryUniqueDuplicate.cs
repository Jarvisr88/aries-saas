namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class TreeListSummaryUniqueDuplicate : TreeListSummaryValue
    {
        private List<object> values;
        private object result;

        public TreeListSummaryUniqueDuplicate(TreeListSummaryItem item, bool duplicates);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Finish(TreeListNodeBase node);

        public override object Value { get; }

        protected bool Duplicates { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeListSummaryUniqueDuplicate.<>c <>9;
            public static Func<object, object> <>9__10_0;
            public static Func<IGrouping<object, object>, bool> <>9__10_1;
            public static Func<IGrouping<object, object>, object> <>9__10_2;
            public static Func<object, object> <>9__10_3;
            public static Func<IGrouping<object, object>, bool> <>9__10_4;
            public static Func<IGrouping<object, object>, object> <>9__10_5;

            static <>c();
            internal object <Finish>b__10_0(object v);
            internal bool <Finish>b__10_1(IGrouping<object, object> v);
            internal object <Finish>b__10_2(IGrouping<object, object> v);
            internal object <Finish>b__10_3(object v);
            internal bool <Finish>b__10_4(IGrouping<object, object> v);
            internal object <Finish>b__10_5(IGrouping<object, object> v);
        }
    }
}

