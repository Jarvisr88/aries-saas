namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Controls;

    internal static class RatingDefaultValueHelper
    {
        public const RatingPrecision Precision = RatingPrecision.Full;
        public const int ItemsCount = 5;
        public const double Minimum = 0.0;
        public const double Maximum = double.NaN;
        public const string ToolTipStringFormat = "{0:f1}";

        public static System.Windows.Controls.Orientation Orientation =>
            System.Windows.Controls.Orientation.Horizontal;
    }
}

