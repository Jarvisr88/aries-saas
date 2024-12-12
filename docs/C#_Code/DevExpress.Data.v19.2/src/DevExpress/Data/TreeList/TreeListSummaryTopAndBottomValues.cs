namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TreeListSummaryTopAndBottomValues : TreeListSummaryValue
    {
        private List<object> values;
        private object result;

        public TreeListSummaryTopAndBottomValues(TreeListSummaryItem item, bool getBottom, bool isPercentArgument, int argument);
        public override void Calculate(TreeListNodeBase node, bool summariesIgnoreNullValues);
        public override void Finish(TreeListNodeBase node);

        public override object Value { get; }

        protected bool GetBottom { get; private set; }

        protected bool IsPercentArgument { get; private set; }

        protected int Argument { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeListSummaryTopAndBottomValues.<>c <>9;
            public static Func<object, object> <>9__18_0;
            public static Func<object, object> <>9__18_1;

            static <>c();
            internal object <Finish>b__18_0(object v);
            internal object <Finish>b__18_1(object v);
        }
    }
}

