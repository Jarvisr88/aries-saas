namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ConditionalAnimationFactory : ConditionalAnimationFactoryBase
    {
        private ITimelineFactory CreateColorAnimation(object toValue, PropertyPath targetPropertyPath)
        {
            Color? nullable = ExtractBrushColor(toValue);
            return ((nullable != null) ? new ColorAnimationFactory(base.AnimationSettings, targetPropertyPath, nullable.Value) : null);
        }

        private DoubleAnimationFactory CreateDoubleAnimation(object toValue, PropertyPath targetPropertyPath) => 
            new DoubleAnimationFactory(base.AnimationSettings, targetPropertyPath, (double) toValue);

        private ITimelineFactory CreateObjectAnimation(object toValue, PropertyPath targetPropertyPath) => 
            new KeyFrameAnimationFactory(base.AnimationSettings, targetPropertyPath, toValue);

        private ITimelineFactory CreateTimeline(DependencyProperty sourceProperty, Func<object, ITimelineFactory> createTimeLineCore)
        {
            if (!this.Format.IsPropertyAssigned(sourceProperty))
            {
                return null;
            }
            object arg = this.Format.GetValue(sourceProperty);
            return createTimeLineCore(arg);
        }

        private static Color? ExtractBrushColor(object brush)
        {
            SolidColorBrush brush2 = brush as SolidColorBrush;
            if (brush2 != null)
            {
                return new Color?(brush2.Color);
            }
            return null;
        }

        protected override FormatConditionBaseInfo GetConditionInfo() => 
            this.Condition;

        [IteratorStateMachine(typeof(<GetTimelineCreators>d__12))]
        private IEnumerable<Func<ITimelineFactory>> GetTimelineCreators()
        {
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.BackgroundProperty, x => this.CreateColorAnimation(x, AnimationPropertyPaths.CreateBackgroundPath()));
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.ForegroundProperty, x => this.CreateColorAnimation(x, AnimationPropertyPaths.CreateForegroundPath()));
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.FontSizeProperty, x => this.CreateDoubleAnimation(x, AnimationPropertyPaths.CreateFontSizePath()));
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.FontStyleProperty, x => this.CreateObjectAnimation(x, AnimationPropertyPaths.CreateFontStylePath()));
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.FontFamilyProperty, x => this.CreateObjectAnimation(x, AnimationPropertyPaths.CreateFontFamilyPath()));
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.FontStretchProperty, x => this.CreateObjectAnimation(x, AnimationPropertyPaths.CreateFontStretchPath()));
            yield return () => this.CreateTimeline(DevExpress.Xpf.Core.ConditionalFormatting.Format.FontWeightProperty, x => this.CreateObjectAnimation(x, AnimationPropertyPaths.CreateFontWeightPath()));
        }

        protected internal override List<ITimelineFactory> GetTimelineFormatFactories()
        {
            List<ITimelineFactory> timelineFormatFactories = base.GetTimelineFormatFactories();
            if ((base.AnimationSettings != null) && (this.Format != null))
            {
                this.PopulateSimpleAnimationFactories(timelineFormatFactories);
                this.PopulateIconAnimationFactories(timelineFormatFactories);
            }
            return timelineFormatFactories;
        }

        private void PopulateIconAnimationFactories(List<ITimelineFactory> result)
        {
            if (this.Format.IsPropertyAssigned(DevExpress.Xpf.Core.ConditionalFormatting.Format.IconProperty))
            {
                if (this.UseTriggerIcons)
                {
                    this.PopulateIcontTriggerAnimationFactories(result);
                }
                else
                {
                    this.PopulateIconTransitionAnimationFactories(result);
                }
            }
        }

        private void PopulateIconTransitionAnimationFactories(List<ITimelineFactory> result)
        {
            IconSetAnimationFactory factory1 = new IconSetAnimationFactory();
            factory1.Condition = this.Condition;
            List<ITimelineFactory> timelineFormatFactories = factory1.GetTimelineFormatFactories();
            if (timelineFormatFactories != null)
            {
                timelineFormatFactories.ForEach(x => result.Add(x));
            }
        }

        private void PopulateIcontTriggerAnimationFactories(List<ITimelineFactory> result)
        {
            result.Add(this.CreateObjectAnimation(this.Format.Icon, AnimationPropertyPaths.CreateIconPath()));
            DoubleAnimationFactory item = this.CreateDoubleAnimation(1.0, AnimationPropertyPaths.CreateIconOpacityPath());
            item.FromValue = 0.0;
            result.Add(item);
        }

        private void PopulateSimpleAnimationFactories(List<ITimelineFactory> result)
        {
            foreach (Func<ITimelineFactory> func in this.GetTimelineCreators())
            {
                ITimelineFactory item = func();
                if (item != null)
                {
                    result.Add(item);
                }
            }
        }

        private DevExpress.Xpf.Core.ConditionalFormatting.Format Format =>
            base.FormatCore as DevExpress.Xpf.Core.ConditionalFormatting.Format;

        public FormatConditionBaseInfo Condition { get; set; }

        public bool UseTriggerIcons { get; set; }

    }
}

