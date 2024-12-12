namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class Range : DXFrameworkContentElement
    {
        public static readonly DependencyProperty Limit1Property;
        public static readonly DependencyProperty Limit2Property;
        public static readonly DependencyProperty AutoProperty;
        private IRangeContainer container;
        private object limit1;
        private object limit2;

        static Range()
        {
            Type ownerType = typeof(Range);
            Limit1Property = DependencyProperty.Register("Limit1", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((Range) o).OnLimit1Changed(args.NewValue)));
            Limit2Property = DependencyProperty.Register("Limit2", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((Range) o).OnLimit2Changed(args.NewValue)));
            AutoProperty = DependencyProperty.Register("Auto", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (o, args) => ((Range) o).OnAutoChanged((bool) args.NewValue)));
        }

        public Range() : this(null)
        {
        }

        public Range(IRangeContainer container)
        {
            this.container = container;
            this.InternalRange = new DevExpress.Xpf.Editors.InternalRange(0.0, 0.0, this.Auto);
        }

        private void OnAutoChanged(bool auto)
        {
            this.SetInternalRange();
            this.PropertyChanged();
        }

        private void OnLimit1Changed(object limit1)
        {
            this.limit1 = limit1;
            this.SetInternalRange();
            this.PropertyChanged();
        }

        private void OnLimit2Changed(object limit2)
        {
            this.limit2 = limit2;
            this.SetInternalRange();
            this.PropertyChanged();
        }

        private void PropertyChanged()
        {
            if (this.container != null)
            {
                this.container.OnRangeChanged();
            }
        }

        internal void SetContainer(IRangeContainer container)
        {
            this.container = container;
        }

        private void SetInternalRange()
        {
            this.InternalRange.Auto = this.Auto;
            if ((this.limit1 == null) || (this.limit2 == null))
            {
                this.InternalRange.IsSet = false;
                this.InternalRange.Min = 0.0;
                this.InternalRange.Max = 0.0;
            }
            else
            {
                SparklineScaleType type;
                SparklineScaleType type2;
                double? nullable = SparklineMathUtils.ConvertToDouble(this.limit1, out type);
                double? nullable2 = SparklineMathUtils.ConvertToDouble(this.limit2, out type2);
                if ((nullable == null) || (nullable2 == null))
                {
                    this.InternalRange.IsSet = false;
                    this.InternalRange.Min = 0.0;
                    this.InternalRange.Max = 0.0;
                    this.InternalRange.ScaleTypeMin = type;
                    this.InternalRange.ScaleTypeMax = type2;
                }
                else
                {
                    this.InternalRange.IsSet = true;
                    if (nullable.Value < nullable2.Value)
                    {
                        this.InternalRange.Min = nullable.Value;
                        this.InternalRange.Max = nullable2.Value;
                        this.InternalRange.ScaleTypeMin = type;
                        this.InternalRange.ScaleTypeMax = type2;
                    }
                    else
                    {
                        this.InternalRange.Min = nullable2.Value;
                        this.InternalRange.Max = nullable.Value;
                        this.InternalRange.ScaleTypeMin = type2;
                        this.InternalRange.ScaleTypeMax = type;
                    }
                }
            }
        }

        internal DevExpress.Xpf.Editors.InternalRange InternalRange { get; private set; }

        public object Limit1
        {
            get => 
                base.GetValue(Limit1Property);
            set => 
                base.SetValue(Limit1Property, value);
        }

        public object Limit2
        {
            get => 
                base.GetValue(Limit2Property);
            set => 
                base.SetValue(Limit2Property, value);
        }

        public bool Auto
        {
            get => 
                (bool) base.GetValue(AutoProperty);
            set => 
                base.SetValue(AutoProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Range.<>c <>9 = new Range.<>c();

            internal void <.cctor>b__3_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((Range) o).OnLimit1Changed(args.NewValue);
            }

            internal void <.cctor>b__3_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((Range) o).OnLimit2Changed(args.NewValue);
            }

            internal void <.cctor>b__3_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((Range) o).OnAutoChanged((bool) args.NewValue);
            }
        }
    }
}

