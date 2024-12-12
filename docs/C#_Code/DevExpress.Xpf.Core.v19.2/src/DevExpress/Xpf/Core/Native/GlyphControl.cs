namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    public class GlyphControl : Control
    {
        public static readonly DependencyProperty GlyphControlModeProperty;
        public static readonly DependencyProperty SizeProperty;
        public static readonly DependencyProperty ModeProperty;
        public static readonly DependencyProperty GlyphLeftTemplateProperty;
        public static readonly DependencyProperty GlyphRightTemplateProperty;
        public static readonly DependencyProperty GlyphUpTemplateProperty;
        public static readonly DependencyProperty GlyphDownTemplateProperty;
        public static readonly DependencyProperty GlyphPlusTemplateProperty;
        public static readonly DependencyProperty GlyphCloseTemplateProperty;
        public static readonly DependencyProperty GlyphLeftPaddingProperty;
        public static readonly DependencyProperty GlyphRightPaddingProperty;
        public static readonly DependencyProperty GlyphUpPaddingProperty;
        public static readonly DependencyProperty GlyphDownPaddingProperty;
        public static readonly DependencyProperty GlyphPlusPaddingProperty;
        public static readonly DependencyProperty GlyphClosePaddingProperty;
        public static readonly DependencyProperty GlyphViewInfoProperty;
        private double originalGlyphWidth;
        private double originalGlyphHeight;

        static GlyphControl();
        private void ApplySize();
        public static GlyphControlMode? GetGlyphControlMode(DependencyObject obj);
        private void InitOriginalGlyphSize();
        public override void OnApplyTemplate();
        private static void OnGlyphControlModeChanged(object sender, DependencyPropertyChangedEventArgs e);
        private void OnGlyphViewInfoChanged();
        protected override void OnStyleChanged(Style oldStyle, Style newStyle);
        public static void SetGlyphControlMode(DependencyObject obj, GlyphControlMode? value);
        private void UpdatePadding();
        private void UpdateTemplate();

        public int Size { get; set; }

        public GlyphControlMode Mode { get; set; }

        public ControlTemplate GlyphLeftTemplate { get; set; }

        public ControlTemplate GlyphRightTemplate { get; set; }

        public ControlTemplate GlyphUpTemplate { get; set; }

        public ControlTemplate GlyphDownTemplate { get; set; }

        public ControlTemplate GlyphPlusTemplate { get; set; }

        public ControlTemplate GlyphCloseTemplate { get; set; }

        public Thickness GlyphLeftPadding { get; set; }

        public Thickness GlyphRightPadding { get; set; }

        public Thickness GlyphUpPadding { get; set; }

        public Thickness GlyphDownPadding { get; set; }

        public Thickness GlyphPlusPadding { get; set; }

        public Thickness GlyphClosePadding { get; set; }

        public GlyphControlViewInfo GlyphViewInfo { get; set; }

        public System.Windows.Shapes.Path Path { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GlyphControl.<>c <>9;
            public static Action<GlyphControl> <>9__3_0;

            static <>c();
            internal void <.cctor>b__64_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_10(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_11(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_12(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_13(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_14(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_5(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_7(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_8(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__64_9(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <OnGlyphControlModeChanged>b__3_0(GlyphControl x);
        }
    }
}

