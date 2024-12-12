namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Windows.Input;

    public interface IGestureClient
    {
        int OnHorizontalPan(ManipulationDeltaEventArgs e);
        void OnManipulationStarting(ManipulationStartingEventArgs e);
        int OnVerticalPan(ManipulationDeltaEventArgs e);
        void OnZoom(ManipulationDeltaEventArgs e, double deltaZoom);
    }
}

