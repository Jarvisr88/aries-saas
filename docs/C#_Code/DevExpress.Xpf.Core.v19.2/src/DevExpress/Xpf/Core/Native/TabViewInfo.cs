namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TabViewInfo : INotifyPropertyChanged
    {
        private DevExpress.Xpf.Core.HeaderOrientation headerOrientationCore;
        private System.Windows.Controls.Orientation? headerOrientation;
        private System.Windows.Controls.Orientation? orientation;

        public event PropertyChangedEventHandler PropertyChanged;

        public TabViewInfo(DXTabControl tabControl);
        public TabViewInfo(DXTabItem tabItem);
        public bool Equals(TabViewInfo value);

        public DevExpress.Xpf.Core.HeaderLocation HeaderLocation { get; private set; }

        public System.Windows.Controls.Orientation HeaderOrientation { get; }

        public System.Windows.Controls.Orientation Orientation { get; }

        public SizeHelperBase OrientationHelper { get; }

        public bool IsHeaderOrientationDifferent { get; }

        public bool IsHoldMode { get; private set; }

        public bool IsMultiLineMode { get; private set; }

        public Color Background { get; private set; }

        public Color Border { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabViewInfo.<>c <>9;
            public static Func<DXTabControl, TabControlScrollView> <>9__34_0;
            public static Func<TabControlScrollView, HeaderOrientation> <>9__34_1;
            public static Func<HeaderOrientation> <>9__34_2;
            public static Func<DXTabControl, TabControlViewBase> <>9__34_3;
            public static Func<TabControlViewBase, HeaderLocation> <>9__34_4;
            public static Func<HeaderLocation> <>9__34_5;
            public static Func<DXTabControl, TabControlViewBase> <>9__34_6;
            public static Func<TabControlViewBase, TabControlMultiLineView> <>9__34_7;
            public static Func<DXTabControl, TabControlViewBase> <>9__34_8;
            public static Func<TabControlViewBase, TabControlMultiLineView> <>9__34_9;
            public static Func<TabControlMultiLineView, bool> <>9__34_10;
            public static Func<bool> <>9__34_11;
            public static Func<DXTabControl, DXTabItem> <>9__34_12;
            public static Func<DXTabItem, Color> <>9__34_13;
            public static Func<Color> <>9__34_14;
            public static Func<DXTabControl, DXTabItem> <>9__34_15;
            public static Func<DXTabItem, Color> <>9__34_16;
            public static Func<Color> <>9__34_17;
            public static Func<DXTabItem, Color> <>9__35_0;
            public static Func<Color> <>9__35_1;
            public static Func<DXTabItem, Color> <>9__35_2;
            public static Func<Color> <>9__35_3;

            static <>c();
            internal TabControlScrollView <.ctor>b__34_0(DXTabControl x);
            internal HeaderOrientation <.ctor>b__34_1(TabControlScrollView x);
            internal bool <.ctor>b__34_10(TabControlMultiLineView x);
            internal bool <.ctor>b__34_11();
            internal DXTabItem <.ctor>b__34_12(DXTabControl x);
            internal Color <.ctor>b__34_13(DXTabItem x);
            internal Color <.ctor>b__34_14();
            internal DXTabItem <.ctor>b__34_15(DXTabControl x);
            internal Color <.ctor>b__34_16(DXTabItem x);
            internal Color <.ctor>b__34_17();
            internal HeaderOrientation <.ctor>b__34_2();
            internal TabControlViewBase <.ctor>b__34_3(DXTabControl x);
            internal HeaderLocation <.ctor>b__34_4(TabControlViewBase x);
            internal HeaderLocation <.ctor>b__34_5();
            internal TabControlViewBase <.ctor>b__34_6(DXTabControl x);
            internal TabControlMultiLineView <.ctor>b__34_7(TabControlViewBase x);
            internal TabControlViewBase <.ctor>b__34_8(DXTabControl x);
            internal TabControlMultiLineView <.ctor>b__34_9(TabControlViewBase x);
            internal Color <.ctor>b__35_0(DXTabItem x);
            internal Color <.ctor>b__35_1();
            internal Color <.ctor>b__35_2(DXTabItem x);
            internal Color <.ctor>b__35_3();
        }
    }
}

