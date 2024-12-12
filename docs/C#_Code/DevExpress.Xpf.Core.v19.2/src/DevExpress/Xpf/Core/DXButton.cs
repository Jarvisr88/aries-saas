namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class DXButton : ContentControlBase
    {
        public event RoutedEventHandler Click
        {
            add
            {
                this.Controller.Click += value;
            }
            remove
            {
                this.Controller.Click -= value;
            }
        }

        protected override ControlControllerBase CreateController() => 
            new DXButtonController(this);

        public DXButtonController Controller =>
            (DXButtonController) base.Controller;
    }
}

