namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    [TemplatePart(Name="PART_Content", Type=typeof(FrameworkElement))]
    public class RatingItem : Control
    {
        private const string PART_Content = "PART_Content";
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty GeometryProperty;
        public static readonly DependencyProperty HoverBackgroundProperty;
        public static readonly DependencyProperty SelectedBackgroundProperty;
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty HoverValueProperty;
        private static readonly DependencyPropertyKey VisualValuePropertyKey;
        public static readonly DependencyProperty VisualValueProperty;
        private static readonly DependencyPropertyKey HoverVisualValuePropertyKey;
        public static readonly DependencyProperty HoverVisualValueProperty;
        public static readonly DependencyProperty IndexProperty;
        private static readonly DependencyPropertyKey IndexPropertyKey;

        static RatingItem()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator1 = DependencyPropertyRegistrator<RatingItem>.New().OverrideDefaultStyleKey().OverrideMetadata(FrameworkElement.FlowDirectionProperty, x => x.Update()).Register<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_Orientation)), parameters), out OrientationProperty, System.Windows.Controls.Orientation.Horizontal, x => x.Update(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator2 = registrator1.Register<System.Windows.Media.Geometry>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, System.Windows.Media.Geometry>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_Geometry)), expressionArray2), out GeometryProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator3 = registrator2.Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_HoverBackground)), expressionArray3), out HoverBackgroundProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator4 = registrator3.Register<Brush>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_SelectedBackground)), expressionArray4), out SelectedBackgroundProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator5 = registrator4.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_Value)), expressionArray5), out ValueProperty, 0.0, (x, oldValue, newValue) => x.OnValueChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator6 = registrator5.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_HoverValue)), expressionArray6), out HoverValueProperty, 0.0, (x, oldValue, newValue) => x.OnHoverValueChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator7 = registrator6.RegisterReadOnly<Brush>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_VisualValue)), expressionArray7), out VisualValuePropertyKey, out VisualValueProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingItem> registrator8 = registrator7.RegisterReadOnly<Brush>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, Brush>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_HoverVisualValue)), expressionArray8), out HoverVisualValuePropertyKey, out HoverVisualValueProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingItem), "x");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator8.RegisterReadOnly<int>(System.Linq.Expressions.Expression.Lambda<Func<RatingItem, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingItem.get_Index)), expressionArray9), out IndexPropertyKey, out IndexProperty, -1, frameworkOptions);
        }

        public RatingItem() : this(-1)
        {
        }

        public RatingItem(int index)
        {
            this.Index = index;
            this.Update();
        }

        public double GetValue(MouseEventArgs e)
        {
            FrameworkElement templateChild = base.GetTemplateChild("PART_Content") as FrameworkElement;
            if (templateChild == null)
            {
                return 0.0;
            }
            double definePoint = this.OrientationHelper.GetDefinePoint(e.GetPosition(templateChild));
            Size size = this.OrientationHelper.CreateSize(templateChild.ActualWidth, templateChild.ActualHeight);
            if (definePoint < 0.0)
            {
                definePoint = 0.0;
            }
            if (definePoint > this.OrientationHelper.GetDefineSize(size))
            {
                definePoint = this.OrientationHelper.GetDefineSize(size);
            }
            double num2 = definePoint / this.OrientationHelper.GetDefineSize(size);
            if ((base.FlowDirection == FlowDirection.LeftToRight) && (this.Orientation == System.Windows.Controls.Orientation.Vertical))
            {
                num2 = 1.0 - num2;
            }
            return num2;
        }

        private Brush GetVisualValue(double value)
        {
            LinearGradientBrush brush;
            if (value <= 0.0)
            {
                return Brushes.Transparent;
            }
            if (value >= 1.0)
            {
                return Brushes.Black;
            }
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                LinearGradientBrush brush1 = new LinearGradientBrush();
                brush1.StartPoint = new Point(0.0, 0.5);
                brush1.EndPoint = new Point(1.0, 0.5);
                brush = brush1;
            }
            else if (base.FlowDirection == FlowDirection.LeftToRight)
            {
                LinearGradientBrush brush2 = new LinearGradientBrush();
                brush2.StartPoint = new Point(0.5, 1.0);
                brush2.EndPoint = new Point(0.5, 0.0);
                brush = brush2;
            }
            else
            {
                LinearGradientBrush brush3 = new LinearGradientBrush();
                brush3.StartPoint = new Point(0.5, 0.0);
                brush3.EndPoint = new Point(0.5, 1.0);
                brush = brush3;
            }
            brush.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            brush.GradientStops.Add(new GradientStop(Colors.Black, value));
            brush.GradientStops.Add(new GradientStop(Colors.Transparent, value));
            brush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
            return brush;
        }

        private void OnHoverValueChanged(double oldValue, double newValue)
        {
            this.HoverVisualValue = this.GetVisualValue(newValue);
        }

        private void OnValueChanged(double oldValue, double newValue)
        {
            this.VisualValue = this.GetVisualValue(newValue);
        }

        private void Update()
        {
            this.OnValueChanged(this.Value, this.Value);
            this.OnHoverValueChanged(this.HoverValue, this.HoverValue);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public System.Windows.Media.Geometry Geometry
        {
            get => 
                (System.Windows.Media.Geometry) base.GetValue(GeometryProperty);
            set => 
                base.SetValue(GeometryProperty, value);
        }

        public Brush HoverBackground
        {
            get => 
                (Brush) base.GetValue(HoverBackgroundProperty);
            set => 
                base.SetValue(HoverBackgroundProperty, value);
        }

        public Brush SelectedBackground
        {
            get => 
                (Brush) base.GetValue(SelectedBackgroundProperty);
            set => 
                base.SetValue(SelectedBackgroundProperty, value);
        }

        public double Value
        {
            get => 
                (double) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        public double HoverValue
        {
            get => 
                (double) base.GetValue(HoverValueProperty);
            set => 
                base.SetValue(HoverValueProperty, value);
        }

        public Brush VisualValue
        {
            get => 
                (Brush) base.GetValue(VisualValueProperty);
            private set => 
                base.SetValue(VisualValuePropertyKey, value);
        }

        public Brush HoverVisualValue
        {
            get => 
                (Brush) base.GetValue(HoverVisualValueProperty);
            private set => 
                base.SetValue(HoverVisualValuePropertyKey, value);
        }

        public int Index
        {
            get => 
                (int) base.GetValue(IndexProperty);
            private set => 
                base.SetValue(IndexPropertyKey, value);
        }

        private SizeHelperBase OrientationHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.Orientation);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RatingItem.<>c <>9 = new RatingItem.<>c();

            internal void <.cctor>b__13_0(RatingItem x)
            {
                x.Update();
            }

            internal void <.cctor>b__13_1(RatingItem x)
            {
                x.Update();
            }

            internal void <.cctor>b__13_2(RatingItem x, double oldValue, double newValue)
            {
                x.OnValueChanged(oldValue, newValue);
            }

            internal void <.cctor>b__13_3(RatingItem x, double oldValue, double newValue)
            {
                x.OnHoverValueChanged(oldValue, newValue);
            }
        }
    }
}

