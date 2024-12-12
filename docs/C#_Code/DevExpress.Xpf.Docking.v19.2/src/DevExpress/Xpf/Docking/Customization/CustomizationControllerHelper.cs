namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class CustomizationControllerHelper
    {
        public static void AssignCommand(BarItem item, ICommand command, UIElement commandTarget)
        {
            if (item != null)
            {
                item.Command = command;
                item.CommandTarget = commandTarget;
            }
        }

        public static CustomizationControllerCommand CreateCommand(DockLayoutManager container) => 
            GetCustomizationController(container).IsClosedPanelsVisible ? ((CustomizationControllerCommand) CreateCommand<HideClosedItemsCommand>(container)) : ((CustomizationControllerCommand) CreateCommand<ShowClosedItemsCommand>(container));

        public static T CreateCommand<T>(DockLayoutManager container) where T: CustomizationControllerCommand, new()
        {
            ICustomizationController customizationController = GetCustomizationController(container);
            if (customizationController != null)
            {
                return customizationController.CreateCommand<T>();
            }
            return default(T);
        }

        private static ICustomizationController GetCustomizationController(DockLayoutManager container) => 
            container?.CustomizationController;
    }
}

