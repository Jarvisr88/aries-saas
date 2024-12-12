namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabControlMultiLineView : TabControlViewBase
    {
        public static readonly DependencyProperty FixedHeadersProperty;
        public static readonly DependencyProperty HeaderAutoFillProperty;

        static TabControlMultiLineView()
        {
            FixedHeadersProperty = DependencyProperty.Register("FixedHeaders", typeof(bool), typeof(TabControlMultiLineView), new PropertyMetadata(false, (d, e) => ((TabControlMultiLineView) d).UpdateViewProperties()));
            HeaderAutoFillProperty = DependencyProperty.Register("HeaderAutoFill", typeof(bool), typeof(TabControlMultiLineView), new PropertyMetadata(false, (d, e) => ((TabControlMultiLineView) d).UpdateViewProperties()));
        }

        [Description("Gets or sets whether the line with the selected header holds its initial position. This is a dependency property.")]
        public bool FixedHeaders
        {
            get => 
                (bool) base.GetValue(FixedHeadersProperty);
            set => 
                base.SetValue(FixedHeadersProperty, value);
        }

        public bool HeaderAutoFill
        {
            get => 
                (bool) base.GetValue(HeaderAutoFillProperty);
            set => 
                base.SetValue(HeaderAutoFillProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabControlMultiLineView.<>c <>9 = new TabControlMultiLineView.<>c();

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlMultiLineView) d).UpdateViewProperties();
            }

            internal void <.cctor>b__9_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TabControlMultiLineView) d).UpdateViewProperties();
            }
        }
    }
}

