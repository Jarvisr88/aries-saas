namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarItemLinkControlCreator : ObjectCreator<BarItemLinkControlBase>
    {
        private static BarItemLinkControlCreator defaultCreator;

        public void RegisterObject(Type linkType, Type linkControlType, CreateObjectMethod<BarItemLinkControlBase> linkControlCreateMethod);
        protected override void RegisterObjects();

        public static BarItemLinkControlCreator Default { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkControlCreator.<>c <>9;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_0;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_1;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_2;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_3;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_4;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_5;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_6;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_7;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_8;
            public static CreateObjectMethod<BarItemLinkControlBase> <>9__3_9;

            static <>c();
            internal BarItemLinkControlBase <RegisterObjects>b__3_0(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_1(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_2(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_3(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_4(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_5(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_6(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_7(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_8(object arg);
            internal BarItemLinkControlBase <RegisterObjects>b__3_9(object arg);
        }
    }
}

