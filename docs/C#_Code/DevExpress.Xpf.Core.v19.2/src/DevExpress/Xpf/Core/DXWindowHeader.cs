namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DXWindowHeader : Border
    {
        public static readonly DependencyProperty IsAeroModeEnabledProperty = DependencyProperty.Register("IsAeroModeEnabled", typeof(bool), typeof(DXWindowHeader), new FrameworkPropertyMetadata(true));
        private DXWindow windowCore;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if ((this.Window != null) && this.IsAeroModeEnabled)
            {
                this.Window.OnHeaderSizeChanged(this, sizeInfo.NewSize, sizeInfo.PreviousSize);
            }
        }

        public bool IsAeroModeEnabled
        {
            get => 
                (bool) base.GetValue(IsAeroModeEnabledProperty);
            set => 
                base.SetValue(IsAeroModeEnabledProperty, value);
        }

        private DXWindow Window
        {
            get
            {
                this.windowCore ??= LayoutHelper.FindParentObject<DXWindow>(this);
                return this.windowCore;
            }
        }
    }
}

