namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public abstract class AnimationSettingsBase : DependencyObject
    {
        public static readonly DependencyProperty EasingModeProperty;
        public static readonly DependencyProperty AnimationTimelinesProperty;

        static AnimationSettingsBase()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationSettingsBase), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<AnimationSettingsBase> registrator1 = DependencyPropertyRegistrator<AnimationSettingsBase>.New().Register<AnimationEasingMode>(System.Linq.Expressions.Expression.Lambda<Func<AnimationSettingsBase, AnimationEasingMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationSettingsBase.get_EasingMode)), parameters), out EasingModeProperty, AnimationEasingMode.Linear, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AnimationSettingsBase), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<List<ConditionalTimeline>>(System.Linq.Expressions.Expression.Lambda<Func<AnimationSettingsBase, List<ConditionalTimeline>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AnimationSettingsBase.get_AnimationTimelines)), expressionArray2), out AnimationTimelinesProperty, null, frameworkOptions);
        }

        public AnimationSettingsBase()
        {
            base.SetValue(AnimationTimelinesProperty, new List<ConditionalTimeline>());
        }

        internal abstract Duration GetHideDuration();
        internal abstract Duration GetHoldDuration();
        internal abstract Duration GetShowDuration();
        internal Duration GetTotalDuration()
        {
            Duration[] durations = new Duration[] { this.GetShowDuration(), this.GetHoldDuration(), this.GetHideDuration() };
            return AnimationDurationCalculator.Sum(durations);
        }

        [XtraSerializableProperty]
        public AnimationEasingMode EasingMode
        {
            get => 
                (AnimationEasingMode) base.GetValue(EasingModeProperty);
            set => 
                base.SetValue(EasingModeProperty, value);
        }

        public List<ConditionalTimeline> AnimationTimelines
        {
            get => 
                (List<ConditionalTimeline>) base.GetValue(AnimationTimelinesProperty);
            set => 
                base.SetValue(AnimationTimelinesProperty, value);
        }
    }
}

