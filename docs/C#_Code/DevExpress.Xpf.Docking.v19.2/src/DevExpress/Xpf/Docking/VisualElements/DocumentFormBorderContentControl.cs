namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Windows;

    public class DocumentFormBorderContentControl : FormBorderContentControl
    {
        protected override void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, base.IsActive ? "Active" : "Inactive", false);
        }
    }
}

