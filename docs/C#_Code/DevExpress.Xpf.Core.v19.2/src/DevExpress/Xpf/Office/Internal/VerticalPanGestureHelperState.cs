namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows.Input;

    public class VerticalPanGestureHelperState : PanGestureHelperStateBase
    {
        public VerticalPanGestureHelperState(GestureHelper helper) : base(helper)
        {
        }

        public override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.Y != 0.0)
            {
                int overPanY = base.Client.OnVerticalPan(e);
                base.ReportBoundaryFeekback(0, overPanY, e);
            }
        }
    }
}

