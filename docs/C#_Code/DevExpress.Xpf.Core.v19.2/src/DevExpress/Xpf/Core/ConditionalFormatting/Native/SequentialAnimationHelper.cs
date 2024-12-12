namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public static class SequentialAnimationHelper
    {
        public static readonly DependencyProperty GenerationProperty = DependencyProperty.RegisterAttached("Generation", typeof(int), typeof(SequentialAnimationHelper), new PropertyMetadata(0));

        private static IList<AnimationTimeline> ExtractAnimations(IGrouping<int, SequentialAnimationTimeline> group)
        {
            Func<SequentialAnimationTimeline, AnimationTimeline> selector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<SequentialAnimationTimeline, AnimationTimeline> local1 = <>c.<>9__4_0;
                selector = <>c.<>9__4_0 = x => x.Timeline;
            }
            return group.Select<SequentialAnimationTimeline, AnimationTimeline>(selector).ToArray<AnimationTimeline>();
        }

        public static int GetGeneration(DependencyObject obj) => 
            (int) obj.GetValue(GenerationProperty);

        public static IList<IList<AnimationTimeline>> GroupAnimationsByGeneration(IList<SequentialAnimationTimeline> animations)
        {
            if (animations == null)
            {
                return null;
            }
            Func<SequentialAnimationTimeline, int> keySelector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<SequentialAnimationTimeline, int> local1 = <>c.<>9__3_0;
                keySelector = <>c.<>9__3_0 = x => x.Generation;
            }
            Func<IGrouping<int, SequentialAnimationTimeline>, int> func2 = <>c.<>9__3_1;
            if (<>c.<>9__3_1 == null)
            {
                Func<IGrouping<int, SequentialAnimationTimeline>, int> local2 = <>c.<>9__3_1;
                func2 = <>c.<>9__3_1 = y => y.Key;
            }
            Func<IGrouping<int, SequentialAnimationTimeline>, IList<AnimationTimeline>> selector = <>c.<>9__3_2;
            if (<>c.<>9__3_2 == null)
            {
                Func<IGrouping<int, SequentialAnimationTimeline>, IList<AnimationTimeline>> local3 = <>c.<>9__3_2;
                selector = <>c.<>9__3_2 = z => ExtractAnimations(z);
            }
            return animations.GroupBy<SequentialAnimationTimeline, int>(keySelector).OrderBy<IGrouping<int, SequentialAnimationTimeline>, int>(func2).Select<IGrouping<int, SequentialAnimationTimeline>, IList<AnimationTimeline>>(selector).ToList<IList<AnimationTimeline>>();
        }

        public static void SetGeneration(DependencyObject obj, int value)
        {
            obj.SetValue(GenerationProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SequentialAnimationHelper.<>c <>9 = new SequentialAnimationHelper.<>c();
            public static Func<SequentialAnimationTimeline, int> <>9__3_0;
            public static Func<IGrouping<int, SequentialAnimationTimeline>, int> <>9__3_1;
            public static Func<IGrouping<int, SequentialAnimationTimeline>, IList<AnimationTimeline>> <>9__3_2;
            public static Func<SequentialAnimationTimeline, AnimationTimeline> <>9__4_0;

            internal AnimationTimeline <ExtractAnimations>b__4_0(SequentialAnimationTimeline x) => 
                x.Timeline;

            internal int <GroupAnimationsByGeneration>b__3_0(SequentialAnimationTimeline x) => 
                x.Generation;

            internal int <GroupAnimationsByGeneration>b__3_1(IGrouping<int, SequentialAnimationTimeline> y) => 
                y.Key;

            internal IList<AnimationTimeline> <GroupAnimationsByGeneration>b__3_2(IGrouping<int, SequentialAnimationTimeline> z) => 
                SequentialAnimationHelper.ExtractAnimations(z);
        }
    }
}

