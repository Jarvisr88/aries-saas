namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AreaSparklineControl : LineSparklineControl
    {
        public static readonly DependencyProperty AreaOpacityProperty;
        private const double defaultAreaOpacity = 0.5;
        private double areaOpacity = 0.5;

        static AreaSparklineControl()
        {
            AreaOpacityProperty = DependencyProperty.Register("AreaOpacity", typeof(double), typeof(AreaSparklineControl), new FrameworkPropertyMetadata(0.5, (d, e) => ((AreaSparklineControl) d).OnAreaOpacityChanged((double) e.NewValue)));
        }

        public override void Assign(SparklineControl view)
        {
            base.Assign(view);
            AreaSparklineControl control = view as AreaSparklineControl;
            if (control != null)
            {
                this.areaOpacity = control.areaOpacity;
            }
        }

        protected internal override BaseSparklinePainter CreatePainter() => 
            new AreaSparklinePainter();

        protected override string GetViewName() => 
            EditorLocalizer.GetString(EditorStringId.SparklineViewArea);

        private void OnAreaOpacityChanged(double areaOpacity)
        {
            this.areaOpacity = areaOpacity;
            base.PropertyChanged();
        }

        internal double ActualAreaOpacity =>
            this.areaOpacity;

        public double AreaOpacity
        {
            get => 
                (double) base.GetValue(AreaOpacityProperty);
            set => 
                base.SetValue(AreaOpacityProperty, value);
        }

        public override SparklineViewType Type =>
            SparklineViewType.Area;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AreaSparklineControl.<>c <>9 = new AreaSparklineControl.<>c();

            internal void <.cctor>b__15_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((AreaSparklineControl) d).OnAreaOpacityChanged((double) e.NewValue);
            }
        }
    }
}

