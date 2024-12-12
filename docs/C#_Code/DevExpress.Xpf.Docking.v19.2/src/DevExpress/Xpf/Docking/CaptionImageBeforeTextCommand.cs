namespace DevExpress.Xpf.Docking
{
    using System;

    public class CaptionImageBeforeTextCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                item.CaptionImageLocation = ImageLocation.BeforeText;
            }
        }
    }
}

