namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Windows.Media.Animation;

    internal class AnimationWrapperFactory : IAnimationWrapperFactory
    {
        public IStoryboardWrapper CreateStoryboardWrapper(Storyboard storyboard) => 
            new StoryboardWrapper(storyboard);

        public ITimelineWrapper CreateTimelineWrapper(Timeline timeline) => 
            new TimelineWrapper(timeline);
    }
}

