namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class DataUpdateAnimationSettings : AnimationSettingsBase
    {
        public static readonly DependencyProperty ShowDurationProperty;
        public static readonly DependencyProperty HoldDurationProperty;
        public static readonly DependencyProperty HideDurationProperty;

        static DataUpdateAnimationSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DataUpdateAnimationSettings), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DataUpdateAnimationSettings> registrator1 = DependencyPropertyRegistrator<DataUpdateAnimationSettings>.New().Register<Duration>(System.Linq.Expressions.Expression.Lambda<Func<DataUpdateAnimationSettings, Duration>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DataUpdateAnimationSettings.get_ShowDuration)), parameters), out ShowDurationProperty, new Duration(TimeSpan.FromMilliseconds(200.0)), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DataUpdateAnimationSettings), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DataUpdateAnimationSettings> registrator2 = registrator1.Register<Duration>(System.Linq.Expressions.Expression.Lambda<Func<DataUpdateAnimationSettings, Duration>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DataUpdateAnimationSettings.get_HoldDuration)), expressionArray2), out HoldDurationProperty, new Duration(TimeSpan.FromMilliseconds(600.0)), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DataUpdateAnimationSettings), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<Duration>(System.Linq.Expressions.Expression.Lambda<Func<DataUpdateAnimationSettings, Duration>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DataUpdateAnimationSettings.get_HideDuration)), expressionArray3), out HideDurationProperty, new Duration(TimeSpan.FromMilliseconds(200.0)), frameworkOptions);
        }

        internal override Duration GetHideDuration() => 
            this.HideDuration;

        internal override Duration GetHoldDuration() => 
            this.HoldDuration;

        internal override Duration GetShowDuration() => 
            this.ShowDuration;

        [XtraSerializableProperty]
        public Duration HideDuration
        {
            get => 
                (Duration) base.GetValue(HideDurationProperty);
            set => 
                base.SetValue(HideDurationProperty, value);
        }

        [XtraSerializableProperty]
        public Duration HoldDuration
        {
            get => 
                (Duration) base.GetValue(HoldDurationProperty);
            set => 
                base.SetValue(HoldDurationProperty, value);
        }

        [XtraSerializableProperty]
        public Duration ShowDuration
        {
            get => 
                (Duration) base.GetValue(ShowDurationProperty);
            set => 
                base.SetValue(ShowDurationProperty, value);
        }
    }
}

