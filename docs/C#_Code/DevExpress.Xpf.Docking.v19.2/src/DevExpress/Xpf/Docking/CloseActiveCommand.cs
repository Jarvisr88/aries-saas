namespace DevExpress.Xpf.Docking
{
    using System;

    internal class CloseActiveCommand : CloseCommand
    {
        protected override bool CanExecuteCore(BaseLayoutItem item) => 
            base.CanExecuteCore(item) && item.IsActive;
    }
}

