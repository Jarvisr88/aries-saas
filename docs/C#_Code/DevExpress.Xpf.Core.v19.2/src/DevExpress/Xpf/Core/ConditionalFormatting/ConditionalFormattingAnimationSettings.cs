namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class ConditionalFormattingAnimationSettings : AnimationSettingsBase
    {
        public static readonly DependencyProperty DurationProperty;

        static ConditionalFormattingAnimationSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ConditionalFormattingAnimationSettings), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<ConditionalFormattingAnimationSettings>.New().Register<System.Windows.Duration>(System.Linq.Expressions.Expression.Lambda<Func<ConditionalFormattingAnimationSettings, System.Windows.Duration>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ConditionalFormattingAnimationSettings.get_Duration)), parameters), out DurationProperty, new System.Windows.Duration(TimeSpan.FromSeconds(1.0)), frameworkOptions);
        }

        internal override System.Windows.Duration GetHideDuration() => 
            new System.Windows.Duration();

        internal override System.Windows.Duration GetHoldDuration() => 
            new System.Windows.Duration();

        internal override System.Windows.Duration GetShowDuration() => 
            this.Duration;

        public System.Windows.Duration Duration
        {
            get => 
                (System.Windows.Duration) base.GetValue(DurationProperty);
            set => 
                base.SetValue(DurationProperty, value);
        }
    }
}

