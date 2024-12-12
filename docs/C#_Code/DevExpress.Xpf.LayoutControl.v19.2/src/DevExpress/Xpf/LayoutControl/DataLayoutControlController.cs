namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class DataLayoutControlController : LayoutControlController
    {
        public DataLayoutControlController(ILayoutControl control) : base(control)
        {
        }

        protected override bool ForwardDesignTimeInput
        {
            get
            {
                Func<DataLayoutControl, bool> evaluator = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<DataLayoutControl, bool> local1 = <>c.<>9__2_0;
                    evaluator = <>c.<>9__2_0 = x => !x.AutoGenerateItems;
                }
                return (base.Control as DataLayoutControl).Return<DataLayoutControl, bool>(evaluator, (<>c.<>9__2_1 ??= () => false));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataLayoutControlController.<>c <>9 = new DataLayoutControlController.<>c();
            public static Func<DataLayoutControl, bool> <>9__2_0;
            public static Func<bool> <>9__2_1;

            internal bool <get_ForwardDesignTimeInput>b__2_0(DataLayoutControl x) => 
                !x.AutoGenerateItems;

            internal bool <get_ForwardDesignTimeInput>b__2_1() => 
                false;
        }
    }
}

