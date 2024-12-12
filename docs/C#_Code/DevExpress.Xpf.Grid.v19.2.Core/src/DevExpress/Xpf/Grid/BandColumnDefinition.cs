namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class BandColumnDefinition : DependencyObject
    {
        public static readonly DependencyProperty WidthProperty;

        static BandColumnDefinition()
        {
            WidthProperty = DependencyProperty.Register("Width", typeof(GridLength), typeof(BandColumnDefinition), new PropertyMetadata(new GridLength(1.0, GridUnitType.Star), (d, e) => ((BandColumnDefinition) d).OnWidthChanged()));
        }

        private void OnWidthChanged()
        {
        }

        public GridLength Width
        {
            get => 
                (GridLength) base.GetValue(WidthProperty);
            set => 
                base.SetValue(WidthProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandColumnDefinition.<>c <>9 = new BandColumnDefinition.<>c();

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandColumnDefinition) d).OnWidthChanged();
            }
        }
    }
}

