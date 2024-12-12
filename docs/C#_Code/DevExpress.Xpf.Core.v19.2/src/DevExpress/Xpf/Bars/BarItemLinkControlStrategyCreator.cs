namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BarItemLinkControlStrategyCreator : ObjectCreator<BarItemLinkControlStrategy>
    {
        private static BarItemLinkControlStrategyCreator defaultCreator;
        private readonly Dictionary<Type, Type> controlLinkTypes;

        public BarItemLinkControlStrategyCreator();
        public BarItemLinkControlStrategy CreateDefault(BarItemLinkControl linkControl);
        public void RegisterObject<TLink, TLinkControl, TStrategy>(Func<IBarItemLinkControl, TStrategy> linkCreateMethod) where TLinkControl: BarItemLinkControl where TStrategy: BarItemLinkControlStrategy;
        public void RegisterObject(Type itemType, Type linkType, CreateObjectMethod<BarItemLinkControlStrategy> linkCreateMethod);
        protected override void RegisterObjects();

        public static BarItemLinkControlStrategyCreator Default { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkControlStrategyCreator.<>c <>9;
            public static Func<IBarItemLinkControl, BarItemLinkControlStrategy> <>9__4_0;
            public static Func<IBarItemLinkControl, BarItemLinkMenuHeaderControlStrategy> <>9__4_1;
            public static Func<IBarItemLinkControl, BarStaticItemLinkControlStrategy> <>9__4_2;
            public static Func<IBarItemLinkControl, BarSubItemLinkControlStrategy> <>9__4_3;
            public static Func<IBarItemLinkControl, BarSplitButtonItemLinkControlStrategy> <>9__4_4;
            public static Func<IBarItemLinkControl, BarCheckItemLinkControlStrategy> <>9__4_5;
            public static Func<IBarItemLinkControl, BarButtonItemLinkControlStrategy> <>9__4_6;
            public static Func<IBarItemLinkControl, BarEditItemLinkControlStrategy> <>9__4_7;
            public static Func<IBarItemLinkControl, BarSplitCheckItemLinkControlStrategy> <>9__4_8;
            public static Func<IBarItemLinkControl, GalleryBarItemLinkControllStrategy> <>9__4_9;
            public static Func<IBarItemLinkControl, BarItemLinkSeparatorControlStrategy> <>9__4_10;
            public static Func<IBarItemLinkControl, BarHistoryListSummaryItemLinkControlStrategy> <>9__4_11;

            static <>c();
            internal BarItemLinkControlStrategy <RegisterObjects>b__4_0(IBarItemLinkControl x);
            internal BarItemLinkMenuHeaderControlStrategy <RegisterObjects>b__4_1(IBarItemLinkControl x);
            internal BarItemLinkSeparatorControlStrategy <RegisterObjects>b__4_10(IBarItemLinkControl x);
            internal BarHistoryListSummaryItemLinkControlStrategy <RegisterObjects>b__4_11(IBarItemLinkControl x);
            internal BarStaticItemLinkControlStrategy <RegisterObjects>b__4_2(IBarItemLinkControl x);
            internal BarSubItemLinkControlStrategy <RegisterObjects>b__4_3(IBarItemLinkControl x);
            internal BarSplitButtonItemLinkControlStrategy <RegisterObjects>b__4_4(IBarItemLinkControl x);
            internal BarCheckItemLinkControlStrategy <RegisterObjects>b__4_5(IBarItemLinkControl x);
            internal BarButtonItemLinkControlStrategy <RegisterObjects>b__4_6(IBarItemLinkControl x);
            internal BarEditItemLinkControlStrategy <RegisterObjects>b__4_7(IBarItemLinkControl x);
            internal BarSplitCheckItemLinkControlStrategy <RegisterObjects>b__4_8(IBarItemLinkControl x);
            internal GalleryBarItemLinkControllStrategy <RegisterObjects>b__4_9(IBarItemLinkControl x);
        }
    }
}

