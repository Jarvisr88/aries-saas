namespace DevExpress.Xpf.Printing.Parameters
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ParameterLineLayout : ContentControl
    {
        static ParameterLineLayout()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ParameterLineLayout), new FrameworkPropertyMetadata(typeof(ParameterLineLayout)));
        }
    }
}

