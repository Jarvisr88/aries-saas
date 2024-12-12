namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class RatingControl : Control
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty PrecisionProperty;
        public static readonly DependencyProperty ItemsCountProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty ActualValueProperty;
        private static readonly DependencyPropertyKey ActualValuePropertyKey;
        public static readonly DependencyProperty ActualHoverValueProperty;
        private static readonly DependencyPropertyKey ActualHoverValuePropertyKey;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty MaximumProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty AllowHoverProperty;
        public static readonly DependencyProperty ToolTipStringFormatProperty;
        private LockableCollection<RatingItem> items = new LockableCollection<RatingItem>();

        public event ValueChangedEventHandler<double> ValueChanged;

        static RatingControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator1 = DependencyPropertyRegistrator<RatingControl>.New().OverrideDefaultStyleKey().OverrideMetadata(UIElement.FocusableProperty, false, null, FrameworkPropertyMetadataOptions.None).OverrideMetadata(Control.IsTabStopProperty, false, null, FrameworkPropertyMetadataOptions.None).OverrideMetadata(FrameworkElement.FlowDirectionProperty, x => x.UpdateItemValues()).Register<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_Orientation)), parameters), out OrientationProperty, RatingDefaultValueHelper.Orientation, delegate (RatingControl x) {
                x.UpdateItemProperties();
                x.UpdateItemValues();
            }, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator2 = registrator1.Register<RatingPrecision>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, RatingPrecision>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_Precision)), expressionArray2), out PrecisionProperty, RatingPrecision.Full, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator3 = registrator2.Register<int>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_ItemsCount)), expressionArray3), out ItemsCountProperty, 5, x => x.OnItemsCountChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator4 = registrator3.Register<Style>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_ItemStyle)), expressionArray4), out ItemStyleProperty, null, x => x.UpdateItemProperties(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator5 = registrator4.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_Value)), expressionArray5), out ValueProperty, 0.0, (x, oldValue, newValue) => x.OnValueChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator6 = registrator5.RegisterReadOnly<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_ActualValue)), expressionArray6), out ActualValuePropertyKey, out ActualValueProperty, 0.0, (x, oldValue, newValue) => x.OnActualValueChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator7 = registrator6.RegisterReadOnly<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_ActualHoverValue)), expressionArray7), out ActualHoverValuePropertyKey, out ActualHoverValueProperty, 0.0, (x, oldValue, newValue) => x.OnActualHoverValueChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator8 = registrator7.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_Minimum)), expressionArray8), out MinimumProperty, 0.0, (x, oldValue, newValue) => x.UpdateActualValue(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator9 = registrator8.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_Maximum)), expressionArray9), out MaximumProperty, double.NaN, (x, oldValue, newValue) => x.UpdateActualValue(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator10 = registrator9.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_AllowHover)), expressionArray10), out AllowHoverProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingControl> registrator11 = registrator10.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_IsReadOnly)), expressionArray11), out IsReadOnlyProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingControl), "x");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator11.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<RatingControl, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingControl.get_ToolTipStringFormat)), expressionArray12), out ToolTipStringFormatProperty, "{0:f1}", frameworkOptions);
        }

        public RatingControl()
        {
            this.OnItemsCountChanged();
        }

        private bool ActualIsMouseOver(MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            return ((position.X >= 0.0) && ((position.X <= base.ActualWidth) && ((position.Y >= 0.0) && (position.Y <= base.ActualHeight))));
        }

        private void ClearItemHoverValuesAndToolTips()
        {
            Action<RatingItem> action = <>c.<>9__69_0;
            if (<>c.<>9__69_0 == null)
            {
                Action<RatingItem> local1 = <>c.<>9__69_0;
                action = <>c.<>9__69_0 = delegate (RatingItem x) {
                    x.HoverValue = 0.0;
                    x.ToolTip = null;
                };
            }
            this.items.ForEach(action);
        }

        private double ConvertFromActualValue(double value)
        {
            Tuple<double, double> minMax = this.GetMinMax();
            return ((minMax.Item2 > minMax.Item1) ? ((value > 0.0) ? ((value < 1.0) ? ((value * (minMax.Item2 - minMax.Item1)) + minMax.Item1) : minMax.Item2) : minMax.Item1) : 0.0);
        }

        private double ConvertToActualValue(double value)
        {
            Tuple<double, double> minMax = this.GetMinMax();
            return ((minMax.Item2 > minMax.Item1) ? ((value < minMax.Item2) ? ((value > minMax.Item1) ? ((value - minMax.Item1) / (minMax.Item2 - minMax.Item1)) : 0.0) : 1.0) : 0.0);
        }

        private double CorrectPrecision(double value)
        {
            if (this.Precision == RatingPrecision.Exact)
            {
                return value;
            }
            if (this.Precision == RatingPrecision.Full)
            {
                return (((value - Math.Floor(value)) == 0.0) ? value : (Math.Floor(value) + 1.0));
            }
            double num = value - Math.Floor(value);
            num = (num < 0.5) ? ((num <= 0.12) ? 0.0 : 0.5) : 1.0;
            return (Math.Floor(value) + num);
        }

        private double GetActualValue(RatingItem item, MouseEventArgs e)
        {
            double num = item.GetValue(e);
            num = this.CorrectPrecision(num);
            num = ((base.FlowDirection != FlowDirection.LeftToRight) || (this.Orientation != System.Windows.Controls.Orientation.Vertical)) ? (num + item.Index) : (num + ((this.items.Count - item.Index) - 1));
            return (num / ((double) this.items.Count));
        }

        private double GetItemValue(RatingItem item, double actualValue)
        {
            int index = item.Index;
            int count = this.items.Count;
            if ((this.Orientation == System.Windows.Controls.Orientation.Vertical) && (base.FlowDirection == FlowDirection.LeftToRight))
            {
                index = (count - index) - 1;
            }
            return Math.Max((double) 0.0, (double) ((actualValue * count) - index));
        }

        private Tuple<double, double> GetMinMax() => 
            GetMinMax(this.Minimum, this.Maximum, (double) this.ItemsCount);

        internal static Tuple<double, double> GetMinMax(double minimum, double maximum, double itemsCount)
        {
            double d = minimum;
            double num2 = maximum;
            if (double.IsInfinity(d) || double.IsNaN(d))
            {
                d = 0.0;
            }
            if (double.IsInfinity(num2) || double.IsNaN(num2))
            {
                num2 = d + itemsCount;
            }
            return new Tuple<double, double>(d, num2);
        }

        private RatingItem GetRatingItem(MouseEventArgs e) => 
            (e.OriginalSource is DependencyObject) ? LayoutTreeHelper.GetVisualParents((DependencyObject) e.OriginalSource, this).OfType<RatingItem>().FirstOrDefault<RatingItem>() : null;

        internal object GetToolTip(double value, bool isActualValue = false)
        {
            if (isActualValue)
            {
                value = this.ConvertFromActualValue(value);
            }
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit(this);
            return ((ownerEdit == null) ? string.Format(this.ToolTipStringFormat, value) : ownerEdit.GetDisplayText(value, true));
        }

        private void OnActualHoverValueChanged(double oldValue, double newValue)
        {
            this.items.ForEach(x => x.HoverValue = this.GetItemValue(x, newValue));
        }

        private void OnActualValueChanged(double oldValue, double newValue)
        {
            this.UpdateItemValues();
        }

        private void OnItemsCountChanged()
        {
            this.items.BeginUpdate();
            this.items.Clear();
            for (int i = 0; i < this.ItemsCount; i++)
            {
                this.items.Add(new RatingItem(i));
            }
            this.UpdateItemProperties();
            this.UpdateActualValue();
            this.UpdateItemValues();
            this.items.EndUpdate();
            if (double.IsNaN(this.Maximum) && (this.Value > this.ItemsCount))
            {
                this.Value = this.ItemsCount;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.ActualHoverValue = 0.0;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (!this.IsReadOnly)
            {
                Mouse.Capture(this, CaptureMode.SubTree);
                e.Handled = true;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (base.IsMouseCaptured)
            {
                if (this.ActualIsMouseOver(e))
                {
                    RatingItem ratingItem = this.GetRatingItem(e);
                    if (ratingItem != null)
                    {
                        this.Value = this.ConvertFromActualValue(this.GetActualValue(ratingItem, e));
                    }
                }
                this.ClearItemHoverValuesAndToolTips();
                base.ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.AllowHover)
            {
                double actualValue = 0.0;
                RatingItem ratingItem = this.GetRatingItem(e);
                if (this.ActualIsMouseOver(e) && (ratingItem != null))
                {
                    actualValue = this.GetActualValue(ratingItem, e);
                }
                this.ActualHoverValue = actualValue;
                if (ratingItem != null)
                {
                    this.UpdateItemToolTip(ratingItem, this.ActualHoverValue);
                }
            }
        }

        private void OnValueChanged(double oldValue, double newValue)
        {
            this.UpdateActualValue();
            this.ValueChanged.Do<ValueChangedEventHandler<double>>(x => x(this, new ValueChangedEventArgs<double>(oldValue, newValue)));
        }

        private void UpdateActualValue()
        {
            this.ActualValue = this.ConvertToActualValue(this.Value);
        }

        private void UpdateItemProperties()
        {
            foreach (RatingItem item in this.items)
            {
                item.Orientation = this.Orientation;
                if (this.ItemStyle != null)
                {
                    item.Style = this.ItemStyle;
                }
            }
        }

        private void UpdateItemToolTip(RatingItem item, double actualValue)
        {
            item.Do<RatingItem>(x => x.ToolTip = this.GetToolTip(actualValue, true));
        }

        private void UpdateItemValues()
        {
            this.items.ForEach(x => x.Value = this.GetItemValue(x, this.ActualValue));
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public RatingPrecision Precision
        {
            get => 
                (RatingPrecision) base.GetValue(PrecisionProperty);
            set => 
                base.SetValue(PrecisionProperty, value);
        }

        public int ItemsCount
        {
            get => 
                (int) base.GetValue(ItemsCountProperty);
            set => 
                base.SetValue(ItemsCountProperty, value);
        }

        public Style ItemStyle
        {
            get => 
                (Style) base.GetValue(ItemStyleProperty);
            set => 
                base.SetValue(ItemStyleProperty, value);
        }

        public double Value
        {
            get => 
                (double) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        public double ActualValue
        {
            get => 
                (double) base.GetValue(ActualValueProperty);
            private set => 
                base.SetValue(ActualValuePropertyKey, value);
        }

        public double ActualHoverValue
        {
            get => 
                (double) base.GetValue(ActualHoverValueProperty);
            private set => 
                base.SetValue(ActualHoverValuePropertyKey, value);
        }

        public double Minimum
        {
            get => 
                (double) base.GetValue(MinimumProperty);
            set => 
                base.SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => 
                (double) base.GetValue(MaximumProperty);
            set => 
                base.SetValue(MaximumProperty, value);
        }

        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        public bool AllowHover
        {
            get => 
                (bool) base.GetValue(AllowHoverProperty);
            set => 
                base.SetValue(AllowHoverProperty, value);
        }

        public string ToolTipStringFormat
        {
            get => 
                (string) base.GetValue(ToolTipStringFormatProperty);
            set => 
                base.SetValue(ToolTipStringFormatProperty, value);
        }

        public IEnumerable<RatingItem> Items =>
            this.items;

        private SizeHelperBase OrientationHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.Orientation);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RatingControl.<>c <>9 = new RatingControl.<>c();
            public static Action<RatingItem> <>9__69_0;

            internal void <.cctor>b__58_0(RatingControl x)
            {
                x.UpdateItemValues();
            }

            internal void <.cctor>b__58_1(RatingControl x)
            {
                x.UpdateItemProperties();
                x.UpdateItemValues();
            }

            internal void <.cctor>b__58_2(RatingControl x)
            {
                x.OnItemsCountChanged();
            }

            internal void <.cctor>b__58_3(RatingControl x)
            {
                x.UpdateItemProperties();
            }

            internal void <.cctor>b__58_4(RatingControl x, double oldValue, double newValue)
            {
                x.OnValueChanged(oldValue, newValue);
            }

            internal void <.cctor>b__58_5(RatingControl x, double oldValue, double newValue)
            {
                x.OnActualValueChanged(oldValue, newValue);
            }

            internal void <.cctor>b__58_6(RatingControl x, double oldValue, double newValue)
            {
                x.OnActualHoverValueChanged(oldValue, newValue);
            }

            internal void <.cctor>b__58_7(RatingControl x, double oldValue, double newValue)
            {
                x.UpdateActualValue();
            }

            internal void <.cctor>b__58_8(RatingControl x, double oldValue, double newValue)
            {
                x.UpdateActualValue();
            }

            internal void <ClearItemHoverValuesAndToolTips>b__69_0(RatingItem x)
            {
                x.HoverValue = 0.0;
                x.ToolTip = null;
            }
        }
    }
}

