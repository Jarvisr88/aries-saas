namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ThemedWindowControlBoxBorder : ContentControl
    {
        private ThemedWindow window;

        static ThemedWindowControlBoxBorder()
        {
            WindowServiceHelper.IWindowServiceProperty.OverrideMetadata(typeof(ThemedWindowControlBoxBorder), new FrameworkPropertyMetadata((x, e) => ((ThemedWindowControlBoxBorder) x).OnWindowChanged(e.OldValue, e.NewValue)));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowControlBoxBorder), new FrameworkPropertyMetadata(typeof(ThemedWindowControlBoxBorder)));
        }

        private ThemedWindow GetWindow() => 
            TreeHelper.GetParent<ThemedWindow>(this, null, true, true);

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            WindowUtility.SetTabControlMargin(this.Window);
        }

        protected virtual void OnWindowChanged(object oldValue, object newValue)
        {
            ThemedWindow window = this.Window;
            if (window == null)
            {
                ThemedWindow local1 = window;
            }
            else
            {
                window.UpdateControlBoxContainer();
            }
            this.window = null;
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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindowControlBoxBorder.<>c <>9 = new ThemedWindowControlBoxBorder.<>c();

            internal void <.cctor>b__0_0(DependencyObject x, DependencyPropertyChangedEventArgs e)
            {
                ((ThemedWindowControlBoxBorder) x).OnWindowChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

