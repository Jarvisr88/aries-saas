namespace DevExpress.Xpf.Docking
{
    using System;

    public class RestoreCommand : DockControllerCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            item.IsClosed && item.AllowRestore;

        protected override void ExecuteCore(IDockController controller, BaseLayoutItem item)
        {
            controller.Restore(item);
        }
    }
}

