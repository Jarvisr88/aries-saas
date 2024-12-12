namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class ChildMinWidthPanel : Decorator
    {
        public static readonly DependencyProperty ChildMinWidthProperty;

        static ChildMinWidthPanel()
        {
            ChildMinWidthProperty = DependencyProperty.Register("ChildMinWidth", typeof(double), typeof(ChildMinWidthPanel), new PropertyMetadata(0.0, (d, e) => ((ChildMinWidthPanel) d).OnChildMinWidthChanged()));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if ((this.ChildMinWidth == 0.0) || ((this.ChildMinWidth < 0.0) || double.IsNaN(this.ChildMinWidth)))
            {
                return base.MeasureOverride(availableSize);
            }
            this.Child.Measure(availableSize);
            Size desiredSize = this.Child.DesiredSize;
            return new Size(Math.Min(Math.Max(this.ChildMinWidth, desiredSize.Width), availableSize.Width), desiredSize.Height);
        }

        private void OnChildMinWidthChanged()
        {
            base.InvalidateMeasure();
        }

        public double ChildMinWidth
        {
            get => 
                (double) base.GetValue(ChildMinWidthProperty);
            set => 
                base.SetValue(ChildMinWidthProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ChildMinWidthPanel.<>c <>9 = new ChildMinWidthPanel.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ChildMinWidthPanel) d).OnChildMinWidthChanged();
            }
        }
    }
}

