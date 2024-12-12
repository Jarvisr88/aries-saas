namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TransferContentControl : ContentControl
    {
        public static readonly DependencyProperty IsPreviousContentProperty;
        private System.Windows.Controls.ContentPresenter contentPresenter;
        private DevExpress.Xpf.Core.TransferControl transferControl;

        static TransferContentControl()
        {
            EventManager.RegisterClassHandler(typeof(TransferContentControl), DevExpress.Xpf.Core.TransferControl.ContentChangedEvent, new RoutedEventHandler(TransferContentControl.OnContentChanged));
            DevExpress.Xpf.Core.TransferControl.SpeedRatioProperty.AddOwner(typeof(TransferContentControl), new FrameworkPropertyMetadata(1.0));
            IsPreviousContentProperty = DependencyProperty.Register("IsPreviousContent", typeof(bool), typeof(TransferContentControl), new FrameworkPropertyMetadata(false));
        }

        protected DevExpress.Xpf.Core.TransferControl FindTransferControl()
        {
            DependencyObject reference = this;
            while ((reference != null) && !(reference is DevExpress.Xpf.Core.TransferControl))
            {
                reference = VisualTreeHelper.GetParent(reference);
            }
            return (reference as DevExpress.Xpf.Core.TransferControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.contentPresenter = (System.Windows.Controls.ContentPresenter) base.GetTemplateChild("PART_ContentPresenter");
        }

        private void OnContentChanged()
        {
            if (this.TransferControl == null)
            {
                this.transferControl = this.FindTransferControl();
            }
            if (this.TransferControl != null)
            {
                if (this.IsPreviousContent)
                {
                    this.TransferControl.OnPrevContentChanged(this);
                }
                else
                {
                    this.TransferControl.OnCurrentContentChanged(this);
                }
            }
            if (this.contentPresenter != null)
            {
                this.contentPresenter.RaiseEvent(new RoutedEventArgs(DevExpress.Xpf.Core.TransferControl.ContentChangedEvent, this));
            }
        }

        private static void OnContentChanged(object sender, RoutedEventArgs e)
        {
            ((TransferContentControl) sender).OnContentChanged();
        }

        public bool IsPreviousContent
        {
            get => 
                (bool) base.GetValue(IsPreviousContentProperty);
            set => 
                base.SetValue(IsPreviousContentProperty, value);
        }

        public System.Windows.Controls.ContentPresenter ContentPresenter =>
            this.contentPresenter;

        public double SpeedRatio
        {
            get => 
                (double) base.GetValue(DevExpress.Xpf.Core.TransferControl.SpeedRatioProperty);
            set => 
                base.SetValue(DevExpress.Xpf.Core.TransferControl.SpeedRatioProperty, value);
        }

        protected DevExpress.Xpf.Core.TransferControl TransferControl
        {
            get => 
                this.transferControl;
            set => 
                this.transferControl = value;
        }
    }
}

