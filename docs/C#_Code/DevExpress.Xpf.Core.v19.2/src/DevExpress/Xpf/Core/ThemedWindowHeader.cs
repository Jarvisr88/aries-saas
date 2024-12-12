namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ThemedWindowHeader : ContentControl
    {
        public static readonly DependencyProperty EnableTransparencyProperty;
        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register("TitleAlignment", typeof(WindowTitleAlignment), typeof(ThemedWindowHeader), new PropertyMetadata(WindowTitleAlignment.Left));
        private ThemedWindow window;

        static ThemedWindowHeader()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowHeader), new FrameworkPropertyMetadata(typeof(ThemedWindowHeader)));
            EnableTransparencyProperty = DependencyProperty.Register("EnableTransparency", typeof(bool), typeof(ThemedWindowHeader), new PropertyMetadata(false));
        }

        private ThemedWindow GetWindow() => 
            TreeHelper.GetParent<ThemedWindow>(this, null, true, true);

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ThemedWindowHeaderAutomationPeer(this);

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (ThemedWindowOptions.GetDeclareHeaderAsContent(this) && (e.LeftButton == MouseButtonState.Pressed))
            {
                this.Window.DragMove();
            }
        }

        public WindowTitleAlignment TitleAlignment
        {
            get => 
                (WindowTitleAlignment) base.GetValue(TitleAlignmentProperty);
            set => 
                base.SetValue(TitleAlignmentProperty, value);
        }

        public ThemedWindow Window
        {
            get
            {
                ThemedWindow window = this.window;
                if (this.window == null)
                {
                    ThemedWindow local1 = this.window;
                    window = this.window = this.GetWindow();
                }
                return window;
            }
        }

        public bool EnableTransparency
        {
            get => 
                (bool) base.GetValue(EnableTransparencyProperty);
            set => 
                base.SetValue(EnableTransparencyProperty, value);
        }
    }
}

