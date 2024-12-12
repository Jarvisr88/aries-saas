namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class PopupFlyoutContainer : FlyoutContainer
    {
        protected override void ClosedSubscribe(EventHandler value)
        {
            this.Popup.Closed += value;
        }

        protected override void ClosedUnSubscribe(EventHandler value)
        {
            this.Popup.Closed -= value;
        }

        protected override void OpenedSubscribe(EventHandler value)
        {
            this.Popup.Opened += value;
        }

        protected override void OpenedUnSubscribe(EventHandler value)
        {
            this.Popup.Opened -= value;
        }

        public System.Windows.Controls.Primitives.Popup Popup { get; set; }

        public override FrameworkElement Element =>
            this.Popup;

        public override UIElement Child
        {
            get => 
                this.Popup.Child;
            set => 
                this.Popup.Child = value;
        }

        public override double HorizontalOffset
        {
            get => 
                this.Popup.HorizontalOffset;
            set => 
                this.Popup.HorizontalOffset = value;
        }

        public override double VerticalOffset
        {
            get => 
                this.Popup.VerticalOffset;
            set => 
                this.Popup.VerticalOffset = value;
        }

        public override bool IsOpen
        {
            get => 
                this.Popup.IsOpen;
            set => 
                this.Popup.IsOpen = value;
        }
    }
}

