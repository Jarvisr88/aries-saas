namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarSparklineControl : BarSparklineControlBase, ISupportNegativePointsControl
    {
        public static readonly DependencyProperty HighlightNegativePointsProperty;
        private const bool defaultHighlightNegativePoints = false;
        private bool highlightNegativePoints;

        static BarSparklineControl()
        {
            HighlightNegativePointsProperty = DependencyProperty.Register("HighlightNegativePoints", typeof(bool), typeof(BarSparklineControl), new FrameworkPropertyMetadata(false, (d, e) => ((BarSparklineControl) d).OnHighlightNegativePointsChanged((bool) e.NewValue)));
        }

        public override void Assign(SparklineControl view)
        {
            base.Assign(view);
            ISupportNegativePointsControl control = view as ISupportNegativePointsControl;
            if (control != null)
            {
                this.highlightNegativePoints = control.HighlightNegativePoints;
            }
        }

        protected internal override BaseSparklinePainter CreatePainter() => 
            new BarSparklinePainter();

        protected override string GetViewName() => 
            EditorLocalizer.GetString(EditorStringId.SparklineViewBar);

        private void OnHighlightNegativePointsChanged(bool highlightNegativePoints)
        {
            this.highlightNegativePoints = highlightNegativePoints;
            base.PropertyChanged();
        }

        protected internal override bool ActualShowNegativePoint =>
            this.ActualHighlightNegativePoints;

        internal bool ActualHighlightNegativePoints =>
            this.highlightNegativePoints;

        public bool HighlightNegativePoints
        {
            get => 
                (bool) base.GetValue(HighlightNegativePointsProperty);
            set => 
                base.SetValue(HighlightNegativePointsProperty, value);
        }

        public override SparklineViewType Type =>
            SparklineViewType.Bar;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarSparklineControl.<>c <>9 = new BarSparklineControl.<>c();

            internal void <.cctor>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BarSparklineControl) d).OnHighlightNegativePointsChanged((bool) e.NewValue);
            }
        }
    }
}

