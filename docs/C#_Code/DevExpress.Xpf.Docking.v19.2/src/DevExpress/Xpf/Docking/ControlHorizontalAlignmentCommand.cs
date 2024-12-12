namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ControlHorizontalAlignmentCommand : LayoutControllerCommand
    {
        protected override bool CanExecuteCore(ILayoutController controller, BaseLayoutItem[] items) => 
            controller.IsCustomization;

        protected override void ExecuteCore(ILayoutController controller, BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                if (item is LayoutControlItem)
                {
                    ((LayoutControlItem) item).ControlHorizontalAlignment = this.HorizontalAlignment;
                }
            }
        }

        public System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }
    }
}

