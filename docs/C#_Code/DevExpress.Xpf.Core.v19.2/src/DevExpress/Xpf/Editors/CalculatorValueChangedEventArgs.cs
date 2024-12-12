namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CalculatorValueChangedEventArgs : RoutedEventArgs
    {
        public CalculatorValueChangedEventArgs(decimal oldValue, decimal newValue) : base(Calculator.ValueChangedEvent)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public decimal NewValue { get; private set; }

        public decimal OldValue { get; private set; }
    }
}

