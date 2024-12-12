namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Images;
    using DevExpress.Xpf.Docking.Platform;
    using System;
    using System.Windows;

    public class CustomizationFormFactory
    {
        private static Size DesiredSize = new Size(350.0, 400.0);

        private static unsafe Point CalcFormLocation(DockLayoutManager owner, FloatingContainer container)
        {
            Size floatSize = container.FloatSize;
            Point clientLocation = new Point(owner.ActualWidth - floatSize.Width, 0.0);
            if (WindowHelper.IsXBAP)
            {
                clientLocation.Y = owner.ActualHeight - floatSize.Height;
            }
            if (owner.FlowDirection == FlowDirection.RightToLeft)
            {
                if ((container is FloatingAdornerContainer) && !((FloatingAdornerContainer) container).InvertLeftAndRightOffsets)
                {
                    return clientLocation;
                }
                Point* pointPtr1 = &clientLocation;
                pointPtr1.X += floatSize.Width;
            }
            if (!DockLayoutManagerHelper.IsPopupRoot(LayoutHelper.FindRoot(owner, false)))
            {
                Window window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(owner);
                if (window != null)
                {
                    Point point3 = window.TransformToDescendant(owner).Transform(clientLocation);
                    if (point3.Y > 0.0)
                    {
                        clientLocation.Y = point3.Y;
                    }
                }
                clientLocation = ScreenHelper.GetClientLocation(ScreenHelper.UpdateContainerLocation(new Rect(ScreenHelper.GetScreenLocation(clientLocation, owner), floatSize)), owner);
            }
            return clientLocation;
        }

        public static FloatingContainer CreateCustomizationForm(DockLayoutManager manager, object content)
        {
            FloatingContainer container = FloatingContainerFactory.Create(DevExpress.Xpf.Core.FloatingMode.Window);
            container.BeginUpdate();
            container.Owner = manager;
            container.MinWidth = 300.0;
            container.MinHeight = 300.0;
            container.FloatSize = DesiredSize;
            container.FloatLocation = CalcFormLocation(manager, container);
            container.Icon = DevExpress.Xpf.Docking.Images.ImageHelper.GetImage("Customization");
            container.Caption = DockingLocalizer.GetString(DockingStringId.TitleCustomizationForm);
            container.Content = content;
            manager.AddToLogicalTree(container, content);
            container.EndUpdate();
            return container;
        }
    }
}

