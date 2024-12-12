namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public abstract class PanGestureHelperStateBase : GestureHelperState
    {
        private static readonly TimeSpan boundaryFeedBackDuration = TimeSpan.FromMilliseconds(250.0);
        private DateTime boundaryFeedBackStartTime;

        protected PanGestureHelperStateBase(GestureHelper helper) : base(helper)
        {
        }

        public override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 0.001;
            e.Handled = true;
        }

        public override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            this.boundaryFeedBackStartTime = DateTime.MinValue;
        }

        protected void ReportBoundaryFeekback(int overPanX, int overPanY, ManipulationDeltaEventArgs e)
        {
            if ((overPanX != 0.0) || (overPanY != 0.0))
            {
                if (this.boundaryFeedBackStartTime == DateTime.MinValue)
                {
                    this.boundaryFeedBackStartTime = DateTime.Now;
                }
                Vector scale = new Vector();
                scale = new Vector();
                e.ReportBoundaryFeedback(new ManipulationDelta(new Vector((double) overPanX, (double) overPanY), 0.0, scale, scale));
                if ((DateTime.Now - this.boundaryFeedBackStartTime) > boundaryFeedBackDuration)
                {
                    e.Complete();
                }
            }
        }
    }
}

