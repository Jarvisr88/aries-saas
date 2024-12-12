namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DataBarAnimationFactory : IndicatorAnimationFactory
    {
        private double CalcValuePosition(FormatValueProvider provider)
        {
            if (this.Condition == null)
            {
                return 0.0;
            }
            DataBarFormatInfo info = this.Condition.CoerceDataBarFormatInfo(null, provider);
            return ((info == null) ? 0.0 : info.ValuePosition);
        }

        private ITimelineFactory CreateValuePositionAnimationFactory(double newValuePosition, double change)
        {
            DoubleAnimationFactory factory = new DoubleAnimationFactory(base.AnimationSettings, AnimationPropertyPaths.CreateValuePositionPath(), newValuePosition);
            if ((base.DefaultAnimationSettings == null) || base.DefaultAnimationSettings.Value.UseConstantDataBarAnimationSpeed)
            {
                factory.DurationMultiplicator = change;
            }
            return factory;
        }

        protected override FormatConditionBaseInfo GetConditionInfo() => 
            this.Condition;

        protected internal override List<ITimelineFactory> GetTimelineFormatFactories()
        {
            List<ITimelineFactory> timelineFormatFactories = base.GetTimelineFormatFactories();
            double num = this.CalcValuePosition(this.OldProvider);
            double newValuePosition = this.CalcValuePosition(base.Provider);
            if (num != newValuePosition)
            {
                timelineFormatFactories.Add(this.CreateValuePositionAnimationFactory(newValuePosition, Math.Abs((double) (newValuePosition - num))));
            }
            return timelineFormatFactories;
        }

        public override void UpdateContext(DataUpdate update)
        {
            base.UpdateContext(update);
            if (update != null)
            {
                this.OldProvider = update.GetOldValue(base.GetActualFieldName());
            }
        }

        public FormatValueProvider OldProvider { get; set; }

        public DataBarFormatConditionInfo Condition { get; set; }

        private string ActualFieldName =>
            this.Condition?.ActualFieldName;
    }
}

