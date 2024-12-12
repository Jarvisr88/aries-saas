namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows.Input;

    public class HorizontalPanGestureHelperState : PanGestureHelperStateBase
    {
        public HorizontalPanGestureHelperState(GestureHelper helper) : base(helper)
        {
        }

        public override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0.0)
            {
                int overPanX = base.Client.OnHorizontalPan(e);
                base.ReportBoundaryFeekback(overPanX, 0, e);
            }
        }
    }
}

