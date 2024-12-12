namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media.Animation;

    [ContentProperty("Animation")]
    public class ConditionalTimeline : DependencyObject
    {
        public static readonly DependencyProperty AnimationProperty;
        public static readonly DependencyProperty TargetPropertyProperty;

        static ConditionalTimeline()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ConditionalTimeline), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<ConditionalTimeline> registrator1 = DependencyPropertyRegistrator<ConditionalTimeline>.New().Register<AnimationTimeline>(System.Linq.Expressions.Expression.Lambda<Func<ConditionalTimeline, AnimationTimeline>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ConditionalTimeline.get_Animation)), parameters), out AnimationProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ConditionalTimeline), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<ConditionalTargetProperty>(System.Linq.Expressions.Expression.Lambda<Func<ConditionalTimeline, ConditionalTargetProperty>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ConditionalTimeline.get_TargetProperty)), expressionArray2), out TargetPropertyProperty, ConditionalTargetProperty.None, frameworkOptions);
        }

        public AnimationTimeline Animation
        {
            get => 
                (AnimationTimeline) base.GetValue(AnimationProperty);
            set => 
                base.SetValue(AnimationProperty, value);
        }

        public ConditionalTargetProperty TargetProperty
        {
            get => 
                (ConditionalTargetProperty) base.GetValue(TargetPropertyProperty);
            set => 
                base.SetValue(TargetPropertyProperty, value);
        }
    }
}

