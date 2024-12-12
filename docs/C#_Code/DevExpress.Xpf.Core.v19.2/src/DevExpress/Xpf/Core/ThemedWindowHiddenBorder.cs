namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ThemedWindowHiddenBorder : Border
    {
        private ThemedWindow window;

        private ThemedWindow GetWindow() => 
            TreeHelper.GetParent<ThemedWindow>(this, null, true, true);

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (this.Window.ActualWindowKind == WindowKind.Tabbed)
            {
                WindowUtility.SetTabControlMargin(this.Window);
            }
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
    }
}

