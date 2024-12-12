namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class BandRowDefinition : DependencyObject
    {
        public static readonly DependencyProperty HeightProperty;

        static BandRowDefinition()
        {
            HeightProperty = DependencyProperty.Register("Height", typeof(GridLength), typeof(BandRowDefinition), new PropertyMetadata(new GridLength(1.0, GridUnitType.Star), (d, e) => ((BandRowDefinition) d).OnHeightChanged()));
        }

        private void OnHeightChanged()
        {
        }

        public GridLength Height
        {
            get => 
                (GridLength) base.GetValue(HeightProperty);
            set => 
                base.SetValue(HeightProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BandRowDefinition.<>c <>9 = new BandRowDefinition.<>c();

            internal void <.cctor>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BandRowDefinition) d).OnHeightChanged();
            }
        }
    }
}

