namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CalculatorCustomErrorTextEventArgs : RoutedEventArgs
    {
        public CalculatorCustomErrorTextEventArgs(string errorText) : base(Calculator.CustomErrorTextEvent)
        {
            this.ErrorText = errorText;
        }

        public string ErrorText { get; set; }
    }
}

