namespace DevExpress.Xpf.Grid.Native
{
    using System.Windows.Media.Animation;

    internal interface IAnimationWrapperFactory
    {
        IStoryboardWrapper CreateStoryboardWrapper(Storyboard storyboard);
        ITimelineWrapper CreateTimelineWrapper(Timeline timeline);
    }
}

