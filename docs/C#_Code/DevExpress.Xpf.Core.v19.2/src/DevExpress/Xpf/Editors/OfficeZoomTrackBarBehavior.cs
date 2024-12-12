namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class OfficeZoomTrackBarBehavior : Behavior<TrackBarEdit>
    {
        private const int intervals = 50;
        private const double midPoint = 100.0;
        private const double defaultMinimum = 10.0;
        private const double defaultMaximum = 400.0;
        private double smallStep = 1.8;
        private double largeStep = 6.0;
        public static readonly DependencyProperty NormalizedValueProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty MaximumProperty;

        static OfficeZoomTrackBarBehavior()
        {
            Type ownerType = typeof(OfficeZoomTrackBarBehavior);
            NormalizedValueProperty = DependencyProperty.Register("NormalizedValue", typeof(double), ownerType, new PropertyMetadata(50.0, (d, e) => ((OfficeZoomTrackBarBehavior) d).NormalizedValueChanged((double) e.NewValue)));
            ValueProperty = DependencyProperty.Register("Value", typeof(double), ownerType, new PropertyMetadata(100.0, (d, e) => ((OfficeZoomTrackBarBehavior) d).ValueChanged((double) e.NewValue)));
            MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), ownerType, new PropertyMetadata(10.0, (d, e) => ((OfficeZoomTrackBarBehavior) d).MinimumChanged((double) e.NewValue)));
            MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), ownerType, new PropertyMetadata(400.0, (d, e) => ((OfficeZoomTrackBarBehavior) d).MaximumChanged((double) e.NewValue)));
        }

        private void AssociatedObjectOnCustomStep(object sender, CustomStepEventArgs e)
        {
            TrackBarEdit edit = (TrackBarEdit) sender;
            double originalValue = e.OriginalValue;
            if (originalValue < edit.Minimum)
            {
                originalValue = edit.Minimum;
            }
            else if (originalValue > edit.Maximum)
            {
                originalValue = edit.Maximum;
            }
            double num3 = Round(this.From(originalValue)) + (e.IsIncrement ? ((double) 10) : ((double) (-10)));
            double num4 = this.To(num3);
            e.Value = num4;
            e.Handled = true;
        }

        private double From(double value)
        {
            double num = (value <= 50.0) ? this.smallStep : this.largeStep;
            return ((num != 0.0) ? (((value - 50.0) * num) + 100.0) : 100.0);
        }

        private DoubleCollection GenerateSteps()
        {
            List<double> collection = new List<double>();
            for (int i = 0; i < 0x31; i++)
            {
                collection.Add((double) i);
            }
            collection.Add(50.0);
            for (int j = 0x33; j < 100; j++)
            {
                collection.Add((double) j);
            }
            return new DoubleCollection(collection);
        }

        private DoubleCollection GenerateTicks()
        {
            double[] collection = new double[] { 50.0 };
            return new DoubleCollection(collection);
        }

        private void MaximumChanged(double value)
        {
            this.largeStep = (value - 100.0) / 50.0;
            if (this.Value > value)
            {
                this.Value = value;
            }
        }

        private void MinimumChanged(double value)
        {
            this.smallStep = (100.0 - value) / 50.0;
            if (this.Value < value)
            {
                this.Value = value;
            }
        }

        private void NormalizedValueChanged(double value)
        {
            this.Value = this.From(value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.Minimum = 0.0;
            base.AssociatedObject.Maximum = 100.0;
            base.AssociatedObject.Ticks = this.GenerateTicks();
            base.AssociatedObject.Steps = this.GenerateSteps();
            ((TrackBarEditPropertyProvider) base.AssociatedObject.PropertyProvider).CustomStep += new CustomStepEventHandler(this.AssociatedObjectOnCustomStep);
            Binding binding = new Binding("NormalizedValue");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(base.AssociatedObject, BaseEdit.EditValueProperty, binding);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((TrackBarEditPropertyProvider) base.AssociatedObject.PropertyProvider).CustomStep -= new CustomStepEventHandler(this.AssociatedObjectOnCustomStep);
            BindingOperations.ClearBinding(base.AssociatedObject, BaseEdit.EditValueProperty);
        }

        private static double Round(double value) => 
            ((value % 10.0) >= 5.0) ? ((value + 10.0) - (value % 10.0)) : (value - (value % 10.0));

        private double To(double value)
        {
            double num = (value <= 100.0) ? this.smallStep : this.largeStep;
            return ((num != 0.0) ? (((value - 100.0) / num) + 50.0) : 50.0);
        }

        private void ValueChanged(double value)
        {
            this.NormalizedValue = this.To(value);
        }

        public double NormalizedValue
        {
            get => 
                (double) base.GetValue(NormalizedValueProperty);
            set => 
                base.SetValue(NormalizedValueProperty, value);
        }

        public double Value
        {
            get => 
                (double) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        public double Minimum
        {
            get => 
                (double) base.GetValue(MinimumProperty);
            set
            {
                if (value > 100.0)
                {
                    value = 100.0;
                }
                base.SetValue(MinimumProperty, value);
            }
        }

        public double Maximum
        {
            get => 
                (double) base.GetValue(MaximumProperty);
            set
            {
                if (value < 100.0)
                {
                    value = 100.0;
                }
                base.SetValue(MaximumProperty, value);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OfficeZoomTrackBarBehavior.<>c <>9 = new OfficeZoomTrackBarBehavior.<>c();

            internal void <.cctor>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((OfficeZoomTrackBarBehavior) d).NormalizedValueChanged((double) e.NewValue);
            }

            internal void <.cctor>b__12_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((OfficeZoomTrackBarBehavior) d).ValueChanged((double) e.NewValue);
            }

            internal void <.cctor>b__12_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((OfficeZoomTrackBarBehavior) d).MinimumChanged((double) e.NewValue);
            }

            internal void <.cctor>b__12_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((OfficeZoomTrackBarBehavior) d).MaximumChanged((double) e.NewValue);
            }
        }
    }
}

