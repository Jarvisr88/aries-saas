namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class ColorAnimationFactory : TimelineFactoryBase<Color>
    {
        public ColorAnimationFactory(AnimationSettingsBase animationSettings, PropertyPath path, Color targetValue) : base(animationSettings, path, targetValue)
        {
        }

        protected override AnimationTimeline CreateAnimationOverride() => 
            new ColorAnimation(base.TargetValue, base.GetActualTimelineDuration()) { EasingFunction = base.CreateEasingFunction() };
    }
}

