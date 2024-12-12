namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows.Controls;

    public class GroupOrientationHorizontalCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                controller.ChangeGroupOrientation(item as LayoutGroup, Orientation.Horizontal);
            }
        }
    }
}

