namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public static class RotatableHelper
    {
        public static Orientation Convert(HeaderLocation value);
        public static Dock Convert(TabViewInfo value);
        public static Brush CorrectBrush(Brush value, TabViewInfo viewInfo);
        public static Brush CorrectBrush(Brush value, Dock location);
        public static CornerRadius CorrectCornerRadius(CornerRadius value, TabViewInfo viewInfo);
        public static CornerRadius CorrectCornerRadius(CornerRadius value, Dock location);
        public static Thickness CorrectThickness(Thickness value, TabViewInfo viewInfo);
        public static Thickness CorrectThickness(Thickness value, Dock location);
        public static Thickness CorrectThicknessBasedOnOrientation(Thickness value, TabViewInfo viewInfo);
        private static Transform GetRotateLeftTransform();
        private static Transform GetRotateRightTransform();
        private static Transform GetRotateTwiceTransform();
        private static CornerRadius RotateLeft(CornerRadius value);
        private static Thickness RotateLeft(Thickness value);
        private static CornerRadius RotateRight(CornerRadius value);
        private static Thickness RotateRight(Thickness value);
        private static CornerRadius RotateTwice(CornerRadius value);
        private static Thickness RotateTwice(Thickness value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RotatableHelper.<>c <>9;
            public static Func<TabViewInfo, HeaderLocation> <>9__1_0;
            public static Func<HeaderLocation> <>9__1_1;

            static <>c();
            internal HeaderLocation <Convert>b__1_0(TabViewInfo x);
            internal HeaderLocation <Convert>b__1_1();
        }
    }
}

