namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    internal class PopupVisualTreeRoot : IVisualTreeRoot
    {
        private readonly Popup popup;

        public PopupVisualTreeRoot()
        {
            Popup popup1 = new Popup();
            popup1.Width = 0.0;
            popup1.Height = 0.0;
            this.popup = popup1;
        }

        public bool Active
        {
            get => 
                this.popup.IsOpen;
            set => 
                this.popup.IsOpen = value;
        }

        public UIElement Content
        {
            get => 
                this.popup.Child;
            set => 
                this.popup.Child = value;
        }
    }
}

