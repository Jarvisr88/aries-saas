namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;

    public class HideClosedItemsCommand : CustomizationControllerCommand
    {
        protected override bool CanExecuteCore(ICustomizationController controller) => 
            controller.IsClosedPanelsVisible;

        protected override void ExecuteCore(ICustomizationController controller)
        {
            controller.HideClosedItemsBar();
            if (controller.Container.ClosedPanelsBarVisibility == ClosedPanelsBarVisibility.Auto)
            {
                controller.Container.ClosedPanelsBarVisibility = ClosedPanelsBarVisibility.Manual;
            }
        }
    }
}

