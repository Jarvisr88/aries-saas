namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false)]
    public class BarManagerThemeDependentValuesProvider : FrameworkElement
    {
        public static readonly DependencyProperty ToolbarCaptionEditorWindowFloatSizeProperty;
        public static readonly DependencyProperty CustomizationFormMinWidthProperty;
        public static readonly DependencyProperty CustomizationFormMinHeightProperty;
        public static readonly DependencyProperty CustomizationFormFloatSizeProperty;
        public static readonly DependencyProperty ColorizeGlyphProperty;

        static BarManagerThemeDependentValuesProvider();
        public BarManagerThemeDependentValuesProvider(BarManager manager);

        public SizeEx ToolbarCaptionEditorWindowFloatSize { get; set; }

        public SizeEx CustomizationFormFloatSize { get; set; }

        public double CustomizationFormMinHeight { get; set; }

        public double CustomizationFormMinWidth { get; set; }

        public bool ColorizeGlyph { get; set; }

        protected BarManager Manager { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarManagerThemeDependentValuesProvider.<>c <>9;

            static <>c();
            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

