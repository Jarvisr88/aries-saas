namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BarSparklineControlBase : SparklineControl
    {
        public static readonly DependencyProperty BarDistanceProperty;
        private const int defaultBarDistance = 2;
        private int barDistance = 2;

        static BarSparklineControlBase()
        {
            BarDistanceProperty = DependencyProperty.Register("BarDistance", typeof(int), typeof(BarSparklineControlBase), new FrameworkPropertyMetadata(2, (d, e) => ((BarSparklineControlBase) d).OnBarDistanceChanged((int) e.NewValue)));
        }

        protected BarSparklineControlBase()
        {
        }

        public override void Assign(SparklineControl view)
        {
            base.Assign(view);
            BarSparklineControlBase base2 = view as BarSparklineControlBase;
            if (base2 != null)
            {
                this.barDistance = base2.barDistance;
            }
        }

        private void OnBarDistanceChanged(int barDistance)
        {
            this.barDistance = barDistance;
            base.PropertyChanged();
        }

        internal int ActualBarDistance =>
            this.barDistance;

        public int BarDistance
        {
            get => 
                (int) base.GetValue(BarDistanceProperty);
            set => 
                base.SetValue(BarDistanceProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarSparklineControlBase.<>c <>9 = new BarSparklineControlBase.<>c();

            internal void <.cctor>b__11_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BarSparklineControlBase) d).OnBarDistanceChanged((int) e.NewValue);
            }
        }
    }
}

