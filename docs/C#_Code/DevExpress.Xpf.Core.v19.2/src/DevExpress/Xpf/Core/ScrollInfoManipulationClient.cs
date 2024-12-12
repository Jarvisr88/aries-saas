namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class ScrollInfoManipulationClient : IManipulationClient
    {
        public ScrollInfoManipulationClient(IScrollInfo scrollInfo)
        {
            this.ScrollInfo = scrollInfo;
        }

        IInputElement IManipulationClient.GetManipulationContainer() => 
            this.ScrollInfo.ScrollOwner;

        Vector IManipulationClient.GetMaxScrollValue() => 
            new Vector(this.ScrollInfo.ExtentWidth - this.ScrollInfo.ViewportWidth, this.ScrollInfo.ExtentHeight - this.ScrollInfo.ViewportHeight);

        Vector IManipulationClient.GetMinScrollValue() => 
            new Vector(0.0, 0.0);

        Vector IManipulationClient.GetScrollValue() => 
            new Vector(this.ScrollInfo.HorizontalOffset, this.ScrollInfo.VerticalOffset);

        void IManipulationClient.Scroll(Vector scrollValue)
        {
            this.ScrollInfo.SetHorizontalOffset(scrollValue.X);
            this.ScrollInfo.SetVerticalOffset(scrollValue.Y);
        }

        private IScrollInfo ScrollInfo { get; set; }
    }
}

