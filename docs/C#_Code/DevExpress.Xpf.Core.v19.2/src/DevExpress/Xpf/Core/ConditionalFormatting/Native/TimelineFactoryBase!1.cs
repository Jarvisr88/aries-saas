﻿namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public abstract class TimelineFactoryBase<T> : ITimelineFactory
    {
        private double showRatio;
        private double hideRatio;
        private readonly AnimationSettingsBase animationSettings;
        private readonly PropertyPath path;
        private readonly T targetValue;
        private double durationMultiplicatorCore;

        public TimelineFactoryBase(AnimationSettingsBase animationSettings, PropertyPath path, T targetValue)
        {
            this.durationMultiplicatorCore = 1.0;
            if (animationSettings == null)
            {
                throw new ArgumentNullException("animationSettings");
            }
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            this.animationSettings = animationSettings;
            this.path = path;
            this.targetValue = targetValue;
        }

        public SequentialAnimationTimeline CreateAnimation()
        {
            this.UpdateEasingRatios();
            AnimationTimeline animation = this.CreateAnimationOverride();
            this.SetFillBehavior(animation);
            TimelineFactoryBase<T>.SetTargetProperty(animation, this.Path);
            this.SetAutogeneratedInfo(animation);
            return new SequentialAnimationTimeline(animation, this.Generation);
        }

        protected abstract AnimationTimeline CreateAnimationOverride();
        protected IEasingFunction CreateEasingFunction()
        {
            TrapezoidEasingFunction function = new TrapezoidEasingFunction();
            Duration actualTimelineDuration = this.GetActualTimelineDuration();
            function.ShowRatio = this.showRatio;
            function.HideRatio = this.hideRatio;
            function.Mode = this.AnimationSettings.EasingMode;
            return function;
        }

        protected Duration GetActualTimelineDuration() => 
            AnimationDurationCalculator.Multiply(this.AnimationSettings.GetTotalDuration(), this.DurationMultiplicator);

        private void SetAutogeneratedInfo(Timeline animation)
        {
            AutogeneratedTimelineHelper.SetHideRatio(animation, this.hideRatio);
        }

        private void SetFillBehavior(Timeline animation)
        {
            animation.FillBehavior = FillBehavior.Stop;
        }

        private static void SetTargetProperty(DependencyObject element, PropertyPath path)
        {
            if ((element != null) && (path != null))
            {
                Storyboard.SetTargetProperty(element, path);
            }
        }

        private void UpdateEasingRatios()
        {
            Duration totalDuration = this.AnimationSettings.GetTotalDuration();
            this.showRatio = AnimationDurationCalculator.Div(this.AnimationSettings.GetShowDuration(), totalDuration);
            this.hideRatio = AnimationDurationCalculator.Div(this.AnimationSettings.GetHideDuration(), totalDuration);
        }

        protected AnimationSettingsBase AnimationSettings =>
            this.animationSettings;

        protected PropertyPath Path =>
            this.path;

        protected T TargetValue =>
            this.targetValue;

        public int Generation { get; set; }

        public double DurationMultiplicator
        {
            get => 
                this.durationMultiplicatorCore;
            set => 
                this.durationMultiplicatorCore = value;
        }
    }
}

