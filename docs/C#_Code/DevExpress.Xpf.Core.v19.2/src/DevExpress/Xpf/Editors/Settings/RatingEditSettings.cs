namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class RatingEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty PrecisionProperty;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty MaximumProperty;
        public static readonly DependencyProperty ItemsCountProperty;
        public static readonly DependencyProperty ItemStyleProperty;

        static RatingEditSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEditSettings), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEditSettings> registrator1 = DependencyPropertyRegistrator<RatingEditSettings>.New().OverrideMetadata(BaseEditSettings.DisplayFormatProperty, "{0:f1}", null, FrameworkPropertyMetadataOptions.None).Register<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<RatingEditSettings, System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEditSettings.get_Orientation)), parameters), out OrientationProperty, RatingDefaultValueHelper.Orientation, new Action<RatingEditSettings, DependencyPropertyChangedEventArgs>(BaseEditSettings.OnSettingsPropertyChanged), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEditSettings), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEditSettings> registrator2 = registrator1.Register<RatingPrecision>(System.Linq.Expressions.Expression.Lambda<Func<RatingEditSettings, RatingPrecision>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEditSettings.get_Precision)), expressionArray2), out PrecisionProperty, RatingPrecision.Full, new Action<RatingEditSettings, DependencyPropertyChangedEventArgs>(BaseEditSettings.OnSettingsPropertyChanged), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEditSettings), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEditSettings> registrator3 = registrator2.Register<int>(System.Linq.Expressions.Expression.Lambda<Func<RatingEditSettings, int>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEditSettings.get_ItemsCount)), expressionArray3), out ItemsCountProperty, 5, new Action<RatingEditSettings, DependencyPropertyChangedEventArgs>(BaseEditSettings.OnSettingsPropertyChanged), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEditSettings), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEditSettings> registrator4 = registrator3.Register<Style>(System.Linq.Expressions.Expression.Lambda<Func<RatingEditSettings, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEditSettings.get_ItemStyle)), expressionArray4), out ItemStyleProperty, null, new Action<RatingEditSettings, DependencyPropertyChangedEventArgs>(BaseEditSettings.OnSettingsPropertyChanged), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEditSettings), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<RatingEditSettings> registrator5 = registrator4.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingEditSettings, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEditSettings.get_Minimum)), expressionArray5), out MinimumProperty, 0.0, new Action<RatingEditSettings, DependencyPropertyChangedEventArgs>(BaseEditSettings.OnSettingsPropertyChanged), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(RatingEditSettings), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator5.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<RatingEditSettings, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(RatingEditSettings.get_Maximum)), expressionArray6), out MaximumProperty, double.NaN, new Action<RatingEditSettings, DependencyPropertyChangedEventArgs>(BaseEditSettings.OnSettingsPropertyChanged), frameworkOptions);
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            RatingEdit editor = edit as RatingEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(OrientationProperty, () => editor.Orientation = this.Orientation);
                base.SetValueFromSettings(PrecisionProperty, () => editor.Precision = this.Precision);
                base.SetValueFromSettings(ItemsCountProperty, () => editor.ItemsCount = this.ItemsCount);
                base.SetValueFromSettings(ItemStyleProperty, () => editor.ItemStyle = this.ItemStyle);
                base.SetValueFromSettings(MinimumProperty, () => editor.Minimum = this.Minimum);
                base.SetValueFromSettings(MaximumProperty, () => editor.Maximum = this.Maximum);
            }
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
    }
}

