namespace DevExpress.Xpf.Docking
{
    using System;

    public class UngroupCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            if (items.Length == 1)
            {
                controller.Ungroup(items[0] as LayoutGroup);
            }
        }
    }
}

