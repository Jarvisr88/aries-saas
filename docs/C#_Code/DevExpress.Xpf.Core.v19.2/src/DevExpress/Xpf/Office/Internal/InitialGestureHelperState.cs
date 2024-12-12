namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class InitialGestureHelperState : GestureHelperState
    {
        private bool disableManipulations;
        private ManipulationModes allowedManipulations;
        private DispatcherTimer timer;
        private int touchDownCount;
        private int timerElapsedCount;

        public InitialGestureHelperState(GestureHelper helper) : base(helper)
        {
        }

        public override void Finish()
        {
            this.StopTimer();
        }

        public override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (this.disableManipulations)
            {
                e.Cancel();
                e.Handled = true;
            }
            if (this.IsZoomingAllowed && (this.touchDownCount == 2))
            {
                base.Helper.SwitchState(new ZoomGestureHelperState(base.Helper));
                base.Helper.State.OnManipulationDelta(e);
            }
            else
            {
                Vector translation = e.DeltaManipulation.Translation;
                double x = translation.X;
                double y = translation.Y;
                if ((x != 0.0) && (Math.Abs(x) > Math.Abs(y)))
                {
                    if (this.IsHorizontalPanningAllowed)
                    {
                        base.Helper.SwitchState(new HorizontalPanGestureHelperState(base.Helper));
                        base.Helper.State.OnManipulationDelta(e);
                        return;
                    }
                    e.Cancel();
                    e.Handled = true;
                    this.disableManipulations = true;
                }
                if (this.IsVerticalPanningAllowed && (y != 0.0))
                {
                    base.Helper.SwitchState(new VerticalPanGestureHelperState(base.Helper));
                    base.Helper.State.OnManipulationDelta(e);
                }
            }
        }

        public override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            e.Cancel();
            e.Handled = true;
        }

        public override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            if (this.disableManipulations)
            {
                e.Cancel();
                e.Handled = true;
            }
            base.Client.OnManipulationStarting(e);
            this.allowedManipulations = e.Mode;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.disableManipulations = true;
            this.StopTimer();
            this.timerElapsedCount++;
        }

        public override void OnTouchDown(TouchEventArgs e)
        {
            this.StopTimer();
            this.touchDownCount++;
            this.disableManipulations = false;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(500.0);
            this.timer.Tick += new EventHandler(this.OnTimerTick);
            this.timer.Start();
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            this.disableManipulations = true;
            this.StopTimer();
        }

        public override void Start()
        {
        }

        private void StopTimer()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Tick -= new EventHandler(this.OnTimerTick);
                this.timer = null;
            }
        }

        private bool IsZoomingAllowed =>
            (this.allowedManipulations & ManipulationModes.Scale) != ManipulationModes.None;

        private bool IsHorizontalPanningAllowed =>
            (this.allowedManipulations & ManipulationModes.TranslateX) != ManipulationModes.None;

        private bool IsVerticalPanningAllowed =>
            (this.allowedManipulations & ManipulationModes.TranslateY) != ManipulationModes.None;
    }
}

