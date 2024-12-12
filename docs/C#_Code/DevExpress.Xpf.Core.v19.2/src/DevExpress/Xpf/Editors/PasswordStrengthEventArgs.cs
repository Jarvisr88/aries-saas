namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PasswordStrengthEventArgs : RoutedEventArgs
    {
        public PasswordStrengthEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public DevExpress.Xpf.Editors.PasswordStrength PasswordStrength { get; set; }

        public object Password { get; internal set; }
    }
}

