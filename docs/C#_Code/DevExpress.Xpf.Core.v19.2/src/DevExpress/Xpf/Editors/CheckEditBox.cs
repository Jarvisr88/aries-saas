namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Automation;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    [ToolboxItem(false)]
    public class CheckEditBox : ToggleStateButton
    {
        public static readonly DependencyProperty DisplayModeProperty;
        public static readonly DependencyProperty GlyphProperty;
        public static readonly DependencyProperty GlyphTemplateProperty;

        public event DependencyPropertyChangedEventHandler IsCheckedChanged;

        public event EventHandler RequestUpdate;

        static CheckEditBox()
        {
            ToggleButton.IsCheckedProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnIsCheckedChanged(e)));
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnHorizontalContentAlignmentChanged(e)));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnVerticalContentAlignmentChanged(e)));
            Control.PaddingProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnPaddingChanged(e)));
            UIElement.SnapsToDevicePixelsProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnSnapsToDevicePixelsChanged(e)));
            ContentControl.ContentProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnContentChanged(e)));
            ContentControl.ContentTemplateProperty.OverrideMetadata(typeof(CheckEditBox), new FrameworkPropertyMetadata((d, e) => ((CheckEditBox) d).OnContentTemplateChanged(e)));
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(CheckEditDisplayMode), typeof(CheckEditBox), new PropertyMetadata(CheckEditDisplayMode.Check));
            GlyphProperty = DependencyProperty.Register("Glyph", typeof(ImageSource), typeof(CheckEditBox));
            GlyphTemplateProperty = DependencyProperty.Register("GlyphTemplate", typeof(DataTemplate), typeof(CheckEditBox));
        }

        protected virtual void OnContentChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseRequestUpdate();
        }

        protected virtual void OnContentTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseRequestUpdate();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new CheckEditBoxAutomationPeer(this);

        protected virtual void OnHorizontalContentAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseRequestUpdate();
        }

        protected virtual void OnIsCheckedChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.IsCheckedChanged != null)
            {
                this.IsCheckedChanged(this, e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
        }

        protected virtual void OnPaddingChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseRequestUpdate();
        }

        protected virtual void OnSnapsToDevicePixelsChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseRequestUpdate();
        }

        protected virtual void OnVerticalContentAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseRequestUpdate();
        }

        protected virtual void RaiseRequestUpdate()
        {
            if (this.RequestUpdate != null)
            {
                this.RequestUpdate(this, EventArgs.Empty);
            }
        }

        public ImageSource Glyph
        {
            get => 
                (ImageSource) base.GetValue(GlyphProperty);
            set => 
                base.SetValue(GlyphProperty, value);
        }

        public DataTemplate GlyphTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GlyphProperty);
            set => 
                base.SetValue(GlyphProperty, value);
        }

        public CheckEditDisplayMode DisplayMode
        {
            get => 
                (CheckEditDisplayMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckEditBox.<>c <>9 = new CheckEditBox.<>c();

            internal void <.cctor>b__3_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnIsCheckedChanged(e);
            }

            internal void <.cctor>b__3_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnHorizontalContentAlignmentChanged(e);
            }

            internal void <.cctor>b__3_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnVerticalContentAlignmentChanged(e);
            }

            internal void <.cctor>b__3_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnPaddingChanged(e);
            }

            internal void <.cctor>b__3_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnSnapsToDevicePixelsChanged(e);
            }

            internal void <.cctor>b__3_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnContentChanged(e);
            }

            internal void <.cctor>b__3_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CheckEditBox) d).OnContentTemplateChanged(e);
            }
        }
    }
}

