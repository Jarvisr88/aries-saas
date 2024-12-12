namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class AddBarAction : InsertBarAction
    {
        [Description("Gets the index at which a bar is added to the BarManager.Bars collection. This property is overridden, so the bar is always appended at the end of the bar collection.")]
        public override int BarIndex { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AddBarAction.<>c <>9;
            public static Func<BarManager, int> <>9__1_0;
            public static Func<int> <>9__1_1;

            static <>c();
            internal int <get_BarIndex>b__1_0(BarManager x);
            internal int <get_BarIndex>b__1_1();
        }
    }
}

