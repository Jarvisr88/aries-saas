namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public static class DocumentPageExtensions
    {
        public static bool IsRestored(this Page page)
        {
            Func<PartiallyDeserializedPage, bool> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<PartiallyDeserializedPage, bool> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Restored;
            }
            return (page as PartiallyDeserializedPage).Return<PartiallyDeserializedPage, bool>(evaluator, (<>c.<>9__0_1 ??= () => true));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPageExtensions.<>c <>9 = new DocumentPageExtensions.<>c();
            public static Func<PartiallyDeserializedPage, bool> <>9__0_0;
            public static Func<bool> <>9__0_1;

            internal bool <IsRestored>b__0_0(PartiallyDeserializedPage x) => 
                x.Restored;

            internal bool <IsRestored>b__0_1() => 
                true;
        }
    }
}

