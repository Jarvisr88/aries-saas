namespace DevExpress.Xpf.Docking
{
    using System;

    public class ActivateCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            item.AllowActivate;

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            controller.Activate(item);
        }
    }
}

