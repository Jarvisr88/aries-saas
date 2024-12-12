namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public abstract class ConditionalAnimationFactoryBase : IConditionalAnimationFactory
    {
        private AnimationSettingsBase animationSettings;
        private Freezable formatCore;

        protected ConditionalAnimationFactoryBase()
        {
        }

        public IList<SequentialAnimationTimeline> CreateAnimations() => 
            !this.UseExplicitAnimations() ? this.GenerateAnimationsFromFormat() : this.GetExplicitAnimations();

        private List<SequentialAnimationTimeline> GenerateAnimationsFromFormat()
        {
            List<SequentialAnimationTimeline> list = new List<SequentialAnimationTimeline>();
            foreach (ITimelineFactory factory in this.GetTimelineFormatFactories())
            {
                SequentialAnimationTimeline item = factory.CreateAnimation();
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        protected abstract FormatConditionBaseInfo GetConditionInfo();
        private List<SequentialAnimationTimeline> GetExplicitAnimations()
        {
            List<SequentialAnimationTimeline> list = new List<SequentialAnimationTimeline>();
            foreach (ConditionalTimeline timeline in this.AnimationSettings.AnimationTimelines)
            {
                AnimationTimeline animation = timeline.Animation;
                if (animation != null)
                {
                    PropertyPath conditionalTimelinePropertyPath = AnimationPropertyPaths.GetConditionalTimelinePropertyPath(timeline.TargetProperty);
                    if (conditionalTimelinePropertyPath != null)
                    {
                        Storyboard.SetTargetProperty(animation, conditionalTimelinePropertyPath);
                    }
                    list.Add(new SequentialAnimationTimeline(animation, SequentialAnimationHelper.GetGeneration(animation)));
                }
            }
            return list;
        }

        protected internal virtual List<ITimelineFactory> GetTimelineFormatFactories() => 
            new List<ITimelineFactory>();

        private void UpdateAnimationSettings()
        {
            FormatConditionBaseInfo conditionInfo = this.GetConditionInfo();
            this.animationSettings = (conditionInfo == null) ? null : (conditionInfo.AnimationSettings ?? conditionInfo.CreateDefaultAnimationSettings(this.DefaultAnimationSettings));
        }

        public virtual void UpdateContext(DataUpdate update)
        {
        }

        public void UpdateDefaultSettings(DevExpress.Xpf.Core.ConditionalFormatting.Native.DefaultAnimationSettings? settings)
        {
            this.DefaultAnimationSettings = settings;
        }

        private void UpdateFormat()
        {
            Func<FormatConditionBaseInfo, Freezable> evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<FormatConditionBaseInfo, Freezable> local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = x => x.FormatCore;
            }
            this.formatCore = this.GetConditionInfo().With<FormatConditionBaseInfo, Freezable>(evaluator);
        }

        private bool UseExplicitAnimations() => 
            DataUpdateConditionInfo.IsCustomAnimationSettings(this.AnimationSettings);

        protected DevExpress.Xpf.Core.ConditionalFormatting.Native.DefaultAnimationSettings? DefaultAnimationSettings { get; private set; }

        protected AnimationSettingsBase AnimationSettings
        {
            get
            {
                if (this.animationSettings == null)
                {
                    this.UpdateAnimationSettings();
                }
                return this.animationSettings;
            }
        }

        protected Freezable FormatCore
        {
            get
            {
                if (this.formatCore == null)
                {
                    this.UpdateFormat();
                }
                return this.formatCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConditionalAnimationFactoryBase.<>c <>9 = new ConditionalAnimationFactoryBase.<>c();
            public static Func<FormatConditionBaseInfo, Freezable> <>9__11_0;

            internal Freezable <UpdateFormat>b__11_0(FormatConditionBaseInfo x) => 
                x.FormatCore;
        }
    }
}

