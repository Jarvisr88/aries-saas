namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;

    public class ShowClosedItemsCommand : CustomizationControllerCommand
    {
        protected override bool CanExecuteCore(ICustomizationController controller) => 
            (controller.ClosedPanelsBarVisibility != ClosedPanelsBarVisibility.Never) && ((controller.Container.ClosedPanels.Count > 0) && !controller.IsClosedPanelsVisible);

        protected override void ExecuteCore(ICustomizationController controller)
        {
            controller.ShowClosedItemsBar();
            if (controller.Container.ClosedPanelsBarVisibility == ClosedPanelsBarVisibility.Auto)
            {
                controller.Container.ClosedPanelsBarVisibility = ClosedPanelsBarVisibility.Manual;
            }
        }
    }
}

