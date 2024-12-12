namespace DevExpress.Xpf.Docking
{
    using System;

    public class RenameCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] item) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] item)
        {
            controller.Rename(item[0]);
        }
    }
}

