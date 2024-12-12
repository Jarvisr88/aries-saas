namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class CustomDisplayTextEventArgs : RoutedEventArgs
    {
        private object editValue;
        private string displayText;

        public CustomDisplayTextEventArgs()
        {
        }

        public CustomDisplayTextEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public CustomDisplayTextEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public object EditValue
        {
            get => 
                this.editValue;
            set => 
                this.editValue = value;
        }

        public string DisplayText
        {
            get => 
                this.displayText;
            set => 
                this.displayText = value;
        }
    }
}

