namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Docking;
    using System;

    public class HideCustomizationFormCommand : CustomizationControllerCommand
    {
        protected override bool CanExecuteCore(ICustomizationController controller) => 
            controller.IsCustomizationFormVisible && controller.IsCustomization;

        protected override void ExecuteCore(ICustomizationController controller)
        {
            controller.HideCustomizationForm();
        }
    }
}

