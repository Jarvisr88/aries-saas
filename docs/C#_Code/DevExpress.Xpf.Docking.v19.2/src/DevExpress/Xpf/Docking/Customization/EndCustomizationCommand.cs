namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking;
    using System;

    public class EndCustomizationCommand : CustomizationControllerCommand
    {
        protected override bool CanExecuteCore(ICustomizationController controller) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ICustomizationController controller)
        {
            controller.EndCustomization();
        }
    }
}

