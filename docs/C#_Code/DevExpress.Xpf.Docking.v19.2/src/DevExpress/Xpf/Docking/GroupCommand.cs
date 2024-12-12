namespace DevExpress.Xpf.Docking
{
    using System;

    public class GroupCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            controller.Group(items);
        }
    }
}

