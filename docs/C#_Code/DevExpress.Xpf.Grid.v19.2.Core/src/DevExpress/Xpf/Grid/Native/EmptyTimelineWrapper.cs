namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Windows.Media.Animation;

    internal class EmptyTimelineWrapper : ITimelineWrapper
    {
        event EventHandler ITimelineWrapper.Deactivated
        {
            add
            {
            }
            remove
            {
            }
        }

        public System.Windows.Media.Animation.Timeline Timeline =>
            null;
    }
}

