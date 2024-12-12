namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class KeyFrameAnimationFactory : TimelineFactoryBase<object>
    {
        public KeyFrameAnimationFactory(AnimationSettingsBase animationSettings, PropertyPath path, object targetValue) : base(animationSettings, path, targetValue)
        {
        }

        protected override AnimationTimeline CreateAnimationOverride()
        {
            ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();
            DiscreteObjectKeyFrame keyFrame = new DiscreteObjectKeyFrame(base.TargetValue, KeyTime.FromPercent(0.0));
            frames.KeyFrames.Add(keyFrame);
            frames.Duration = base.GetActualTimelineDuration();
            return frames;
        }
    }
}

