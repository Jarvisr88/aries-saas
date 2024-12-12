namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows.Input;

    public class ZoomGestureHelperState : GestureHelperState
    {
        public ZoomGestureHelperState(GestureHelper helper) : base(helper)
        {
        }

        public override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e.IsInertial)
            {
                e.Complete();
                e.Handled = true;
                base.Helper.SwitchToDefaultState();
            }
            double x = e.DeltaManipulation.Scale.X;
            if ((x != 1.0) && !e.IsInertial)
            {
                x = Math.Min(1.15, Math.Max(0.85, x));
                base.Client.OnZoom(e, x);
            }
        }

        public override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            e.Handled = true;
            e.Cancel();
            base.Helper.SwitchToDefaultState();
        }
    }
}

