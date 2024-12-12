namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RuntimeRemoveLinkCustomization : RuntimeCollectionCustomization, IRemoveRuntimeCustomization
    {
        public RuntimeRemoveLinkCustomization();
        public RuntimeRemoveLinkCustomization(BarItemLinkBase forcedTarget);
        protected override bool ApplyOverride(bool silent);
        protected BarItemLink FindLink();
        protected override bool UndoOverride();

        [XtraSerializableProperty]
        public string HolderName { get; set; }

        [XtraSerializableProperty]
        public int Index { get; set; }

        [XtraSerializableProperty]
        public string BasedOn { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RuntimeRemoveLinkCustomization.<>c <>9;
            public static Func<BarItemLinkBase, BarItemLinkCollection> <>9__13_0;
            public static Func<BarItemLinkCollection, ILinksHolder> <>9__13_1;
            public static Func<IFrameworkInputElement, string> <>9__15_0;

            static <>c();
            internal BarItemLinkCollection <.ctor>b__13_0(BarItemLinkBase x);
            internal ILinksHolder <.ctor>b__13_1(BarItemLinkCollection x);
            internal string <ApplyOverride>b__15_0(IFrameworkInputElement x);
        }
    }
}

