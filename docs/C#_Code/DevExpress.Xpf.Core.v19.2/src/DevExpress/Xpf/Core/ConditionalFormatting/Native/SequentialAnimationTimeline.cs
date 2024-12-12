namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Windows.Media.Animation;

    public class SequentialAnimationTimeline
    {
        private readonly AnimationTimeline timeline;
        private readonly int generation;

        public SequentialAnimationTimeline(AnimationTimeline timeline) : this(timeline, 0)
        {
        }

        public SequentialAnimationTimeline(AnimationTimeline timeline, int generation)
        {
            if (timeline == null)
            {
                throw new ArgumentNullException("timeline");
            }
            this.timeline = timeline;
            this.generation = generation;
        }

        public AnimationTimeline Timeline =>
            this.timeline;

        public int Generation =>
            this.generation;
    }
}

