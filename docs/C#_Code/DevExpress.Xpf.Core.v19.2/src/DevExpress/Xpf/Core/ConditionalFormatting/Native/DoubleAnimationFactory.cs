namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class DoubleAnimationFactory : TimelineFactoryBase<double>
    {
        public DoubleAnimationFactory(AnimationSettingsBase animationSettings, PropertyPath path, double targetValue) : base(animationSettings, path, targetValue)
        {
        }

        protected override AnimationTimeline CreateAnimationOverride()
        {
            DoubleAnimation animation = new DoubleAnimation(base.TargetValue, base.GetActualTimelineDuration());
            if (this.FromValue != null)
            {
                animation.From = new double?(this.FromValue.Value);
            }
            animation.EasingFunction = base.CreateEasingFunction();
            return animation;
        }

        public double? FromValue { get; set; }
    }
}

