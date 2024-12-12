namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Windows.Media.Animation;

    internal class EmptyAnimationWrapperFactory : IAnimationWrapperFactory
    {
        public IStoryboardWrapper CreateStoryboardWrapper(Storyboard storyboard) => 
            new EmptyStoryboardWrapper();

        public ITimelineWrapper CreateTimelineWrapper(Timeline timeline) => 
            new EmptyTimelineWrapper();
    }
}

