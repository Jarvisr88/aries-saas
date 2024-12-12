namespace DevExpress.Xpf.Docking
{
    using System;

    public class CaptionLocationBottomCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                item.CaptionLocation = CaptionLocation.Bottom;
            }
            controller.Container.Update();
        }
    }
}

