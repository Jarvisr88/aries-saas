namespace DevExpress.Xpf.Docking
{
    using System;

    public class SetStyleGroupCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.SetGroupBorderStyle(item as LayoutGroup, GroupBorderStyle.Group);
            }
        }
    }
}

