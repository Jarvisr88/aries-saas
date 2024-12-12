namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Customization;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class ClosedItemsBarInfo
    {
        public ClosedItemsBarInfo(ClosedItemsBar bar, DockLayoutManager container)
        {
            this.Bar = bar;
            this.Container = container;
        }

        public void CreateBarButtonItem(BaseLayoutItem item)
        {
            this.CreateBarButtonItem(item, DockControllerCommand.Restore);
        }

        protected virtual BarButtonItem CreateBarButtonItem(object content, ICommand command)
        {
            BarButtonItem item = this.Bar.CreateBarButtonItem(content);
            CustomizationControllerHelper.AssignCommand(item, command, this.Container);
            item.CommandParameter = content;
            return item;
        }

        public ClosedItemsBar Bar { get; private set; }

        public DockLayoutManager Container { get; private set; }
    }
}

