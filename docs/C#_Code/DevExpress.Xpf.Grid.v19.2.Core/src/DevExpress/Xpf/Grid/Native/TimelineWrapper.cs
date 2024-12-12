namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Media.Animation;

    internal class TimelineWrapper : ITimelineWrapper
    {
        private readonly System.Windows.Media.Animation.Timeline timeline;

        private event EventHandler deactivated;

        event EventHandler ITimelineWrapper.Deactivated
        {
            add
            {
                this.deactivated += value;
            }
            remove
            {
                this.deactivated -= value;
            }
        }

        public TimelineWrapper(System.Windows.Media.Animation.Timeline timeline)
        {
            if (timeline == null)
            {
                throw new ArgumentNullException();
            }
            this.timeline = timeline;
            this.Timeline.CurrentTimeInvalidated += new EventHandler(this.OnCurrentTimeInvalidated);
        }

        private void OnCurrentTimeInvalidated(object sender, EventArgs e)
        {
            AnimationClock clock = sender as AnimationClock;
            if ((clock != null) && (clock.CurrentState != ClockState.Active))
            {
                this.Timeline.CurrentTimeInvalidated -= new EventHandler(this.OnCurrentTimeInvalidated);
                this.RaiseDeactivated();
            }
        }

        private void RaiseDeactivated()
        {
            EventHandler deactivated = this.deactivated;
            if (deactivated != null)
            {
                deactivated(this, new EventArgs());
            }
        }

        public System.Windows.Media.Animation.Timeline Timeline =>
            this.timeline;
    }
}

