namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarItemLinkCreator : ObjectCreator<BarItemLink>
    {
        private static BarItemLinkCreator defaultCreator;

        public void RegisterObject(Type itemType, Type linkType, CreateObjectMethod<BarItemLink> linkCreateMethod);
        protected override void RegisterObjects();

        public static BarItemLinkCreator Default { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkCreator.<>c <>9;
            public static CreateObjectMethod<BarItemLink> <>9__3_0;
            public static CreateObjectMethod<BarItemLink> <>9__3_1;
            public static CreateObjectMethod<BarItemLink> <>9__3_2;
            public static CreateObjectMethod<BarItemLink> <>9__3_3;
            public static CreateObjectMethod<BarItemLink> <>9__3_4;
            public static CreateObjectMethod<BarItemLink> <>9__3_5;
            public static CreateObjectMethod<BarItemLink> <>9__3_6;
            public static CreateObjectMethod<BarItemLink> <>9__3_7;
            public static CreateObjectMethod<BarItemLink> <>9__3_8;
            public static CreateObjectMethod<BarItemLink> <>9__3_9;
            public static CreateObjectMethod<BarItemLink> <>9__3_10;
            public static CreateObjectMethod<BarItemLink> <>9__3_11;
            public static CreateObjectMethod<BarItemLink> <>9__3_12;
            public static CreateObjectMethod<BarItemLink> <>9__3_13;
            public static CreateObjectMethod<BarItemLink> <>9__3_14;
            public static CreateObjectMethod<BarItemLink> <>9__3_15;
            public static CreateObjectMethod<BarItemLink> <>9__3_16;
            public static CreateObjectMethod<BarItemLink> <>9__3_17;

            static <>c();
            internal BarItemLink <RegisterObjects>b__3_0(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_1(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_10(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_11(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_12(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_13(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_14(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_15(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_16(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_17(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_2(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_3(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_4(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_5(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_6(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_7(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_8(object <arg>);
            internal BarItemLink <RegisterObjects>b__3_9(object <arg>);
        }
    }
}

