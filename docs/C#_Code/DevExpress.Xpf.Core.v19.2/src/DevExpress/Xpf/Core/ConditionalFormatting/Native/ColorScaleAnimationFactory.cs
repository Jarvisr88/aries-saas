namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ColorScaleAnimationFactory : IndicatorAnimationFactory
    {
        protected override FormatConditionBaseInfo GetConditionInfo() => 
            this.Condition;

        protected internal override List<ITimelineFactory> GetTimelineFormatFactories()
        {
            List<ITimelineFactory> timelineFormatFactories = base.GetTimelineFormatFactories();
            Color? nullable = this.Condition.CalcColor(base.Provider);
            if (nullable != null)
            {
                timelineFormatFactories.Add(new ColorAnimationFactory(base.AnimationSettings, AnimationPropertyPaths.CreateBackgroundPath(), nullable.Value));
            }
            return timelineFormatFactories;
        }

        public ColorScaleFormatConditionInfo Condition { get; set; }
    }
}

