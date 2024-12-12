namespace DevExpress.Xpf.Docking
{
    using System;

    public class CloseCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            !item.IsClosed && item.AllowClose;

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            controller.CloseEx(item);
        }
    }
}

