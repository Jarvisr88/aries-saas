namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class CardsSeparator : DependencyObject
    {
        private static readonly DependencyPropertyKey MarginPropertyKey;
        public static readonly DependencyProperty MarginProperty;
        private static readonly DependencyPropertyKey LengthPropertyKey;
        public static readonly DependencyProperty LengthProperty;

        static CardsSeparator();
        public CardsSeparator(int rowIndex);

        public int RowIndex { get; private set; }

        public Thickness Margin { get; internal set; }

        public double Length { get; internal set; }

        public System.Windows.Controls.Orientation Orientation { get; set; }
    }
}

