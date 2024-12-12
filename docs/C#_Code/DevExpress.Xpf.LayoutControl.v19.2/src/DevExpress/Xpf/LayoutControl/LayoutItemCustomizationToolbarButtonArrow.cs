namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class LayoutItemCustomizationToolbarButtonArrow : Control
    {
        public static readonly DependencyProperty DefaultDirectionProperty;
        public static readonly DependencyProperty DirectionProperty;
        public static readonly DependencyProperty RotateTransformProperty;

        static LayoutItemCustomizationToolbarButtonArrow()
        {
            DefaultDirectionProperty = DependencyProperty.Register("DefaultDirection", typeof(Side), typeof(LayoutItemCustomizationToolbarButtonArrow), new PropertyMetadata((o, e) => ((LayoutItemCustomizationToolbarButtonArrow) o).OnDefaultDirectionChanged()));
            DirectionProperty = DependencyProperty.Register("Direction", typeof(Side), typeof(LayoutItemCustomizationToolbarButtonArrow), new PropertyMetadata((o, e) => ((LayoutItemCustomizationToolbarButtonArrow) o).OnDirectionChanged()));
            RotateTransformProperty = DependencyProperty.Register("RotateTransform", typeof(System.Windows.Media.RotateTransform), typeof(LayoutItemCustomizationToolbarButtonArrow), null);
        }

        public LayoutItemCustomizationToolbarButtonArrow()
        {
            base.DefaultStyleKey = typeof(LayoutItemCustomizationToolbarButtonArrow);
        }

        private double GetAngle(Side direction)
        {
            switch (direction)
            {
                case Side.Left:
                    return 0.0;

                case Side.Top:
                    return 90.0;

                case Side.LeftRight:
                    return 180.0;

                case Side.Bottom:
                    return 270.0;
            }
            return 0.0;
        }

        protected virtual void OnAngleChanged()
        {
            this.UpdateRotateTransform();
        }

        protected virtual void OnDefaultDirectionChanged()
        {
            this.OnAngleChanged();
        }

        protected virtual void OnDirectionChanged()
        {
            this.OnAngleChanged();
        }

        protected void UpdateRotateTransform()
        {
            System.Windows.Media.RotateTransform transform1 = new System.Windows.Media.RotateTransform();
            transform1.Angle = this.Angle;
            this.RotateTransform = transform1;
        }

        public Side DefaultDirection
        {
            get => 
                (Side) base.GetValue(DefaultDirectionProperty);
            set => 
                base.SetValue(DefaultDirectionProperty, value);
        }

        public Side Direction
        {
            get => 
                (Side) base.GetValue(DirectionProperty);
            set => 
                base.SetValue(DirectionProperty, value);
        }

        public System.Windows.Media.RotateTransform RotateTransform
        {
            get => 
                (System.Windows.Media.RotateTransform) base.GetValue(RotateTransformProperty);
            set => 
                base.SetValue(RotateTransformProperty, value);
        }

        protected double Angle =>
            this.GetAngle(this.Direction) - this.GetAngle(this.DefaultDirection);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemCustomizationToolbarButtonArrow.<>c <>9 = new LayoutItemCustomizationToolbarButtonArrow.<>c();

            internal void <.cctor>b__20_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemCustomizationToolbarButtonArrow) o).OnDefaultDirectionChanged();
            }

            internal void <.cctor>b__20_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemCustomizationToolbarButtonArrow) o).OnDirectionChanged();
            }
        }
    }
}

