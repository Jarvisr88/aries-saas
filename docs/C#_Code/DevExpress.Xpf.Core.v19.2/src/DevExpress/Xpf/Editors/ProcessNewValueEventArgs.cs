namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ProcessNewValueEventArgs : RoutedEventArgs
    {
        public ProcessNewValueEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public string DisplayText { get; internal set; }

        public bool PostponedValidation { get; set; }
    }
}

