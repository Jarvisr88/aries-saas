namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.ThemeKeys;
    using System;
    using System.Windows;

    public class DocumentSelectorContainerFactory
    {
        public static FloatingContainer CreateDocumentSelectorContainer(DockLayoutManager manager, object content)
        {
            FloatingContainer container = FloatingContainerFactory.Create(DevExpress.Xpf.Core.FloatingMode.Window);
            container.BeginUpdate();
            container.CloseOnEscape = true;
            container.Owner = manager;
            container.ContainerStartupLocation = WindowStartupLocation.Manual;
            container.AllowMoving = false;
            container.AllowSizing = false;
            container.ContainerTemplate = null;
            container.ShowModal = true;
            container.Content = content;
            FloatingContainer.SetFloatingContainer((DependencyObject) content, container);
            manager.AddToLogicalTree(container, content);
            container.EndUpdate();
            return container;
        }

        public static unsafe Point GetLocation(DockLayoutManager manager, Size size)
        {
            Point point = new Point((manager.ActualWidth - size.Width) * 0.5, (manager.ActualHeight - size.Height) * 0.5);
            if (manager.FlowDirection == FlowDirection.RightToLeft)
            {
                Point* pointPtr1 = &point;
                pointPtr1.X += size.Width;
            }
            return point;
        }

        public static Size GetSize(DockLayoutManager manager, Size minSize) => 
            new Size(Math.Max(minSize.Width, manager.ActualWidth * 0.6), Math.Max(minSize.Height, manager.ActualHeight * 0.6));

        public static DataTemplate GetTemplate(DockLayoutManager manager)
        {
            DocumentSelectorElementsThemeKeyExtension key = new DocumentSelectorElementsThemeKeyExtension();
            key.ResourceKey = DocumentSelectorElements.Template;
            key.ThemeName = DockLayoutManagerExtension.GetThemeName(manager);
            return (manager.FindResource(key) as DataTemplate);
        }
    }
}

