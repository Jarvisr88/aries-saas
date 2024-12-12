namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Interop;

    public static class PopupExtensions
    {
        public static void BringToFront(this Popup popup)
        {
            popup.IsOpen = false;
            popup.IsOpen = true;
        }

        public static Point GetOffset(this Popup popup) => 
            new Point(popup.HorizontalOffset, popup.VerticalOffset);

        private static Rect GetScreenWorkArea(UIElement placementTarget, UIElement popupChild) => 
            BrowserInteropHelper.IsBrowserHosted ? new Rect(popupChild.GetRootVisual().GetSize()) : ((placementTarget == null) ? SystemParameters.WorkArea : ScreenHelper.GetScreenWorkArea((FrameworkElement) placementTarget));

        public static void MakeVisible(this Popup popup)
        {
            Point? offset = null;
            Rect? clearZone = null;
            popup.MakeVisible(offset, clearZone);
        }

        public static void MakeVisible(this Popup popup, Point? offset, Rect? clearZone)
        {
            UIElement parent = popup.GetParent() as UIElement;
            if (popup.PlacementTarget != null)
            {
                parent = popup.PlacementTarget;
            }
            Point? nullable = offset;
            Point point = MakeVisible(parent, (nullable != null) ? nullable.GetValueOrDefault() : popup.GetOffset(), clearZone, popup.Child);
            popup.SetOffset(point);
        }

        public static unsafe Point MakeVisible(UIElement placementTarget, Point popupOffset, Rect? clearZone, UIElement popupChild)
        {
            if (popupChild.DesiredSize.IsZero())
            {
                popupChild.Measure(SizeHelper.Infinite);
            }
            Size desiredSize = popupChild.DesiredSize;
            Rect screenWorkArea = GetScreenWorkArea(placementTarget, popupChild);
            if (placementTarget != null)
            {
                screenWorkArea = placementTarget.MapRectFromScreen(screenWorkArea);
            }
            bool flag = ((popupChild is FrameworkElement) && (((FrameworkElement) popupChild).FlowDirection == FlowDirection.RightToLeft)) && ((placementTarget == null) || ((placementTarget is FrameworkElement) && (((FrameworkElement) placementTarget).FlowDirection == FlowDirection.LeftToRight)));
            if (flag)
            {
                Point* pointPtr1 = &popupOffset;
                pointPtr1.X -= desiredSize.Width;
            }
            if ((popupOffset.X + desiredSize.Width) > screenWorkArea.Right)
            {
                if ((clearZone == null) || ((popupOffset.Y >= clearZone.Value.Bottom) || ((popupOffset.Y + desiredSize.Height) <= clearZone.Value.Top)))
                {
                    popupOffset.X = screenWorkArea.Right - desiredSize.Width;
                }
                else
                {
                    Point* pointPtr2 = &popupOffset;
                    pointPtr2.X -= ((2.0 * (popupOffset.X - clearZone.Value.Right)) + clearZone.Value.Width) + desiredSize.Width;
                }
            }
            popupOffset.X = Math.Max(screenWorkArea.Left, popupOffset.X);
            if (flag)
            {
                Point* pointPtr3 = &popupOffset;
                pointPtr3.X += desiredSize.Width;
            }
            if ((popupOffset.Y + desiredSize.Height) > screenWorkArea.Bottom)
            {
                if ((clearZone == null) || ((popupOffset.X >= clearZone.Value.Right) || ((popupOffset.X + desiredSize.Width) <= clearZone.Value.Left)))
                {
                    popupOffset.Y = screenWorkArea.Bottom - desiredSize.Height;
                }
                else
                {
                    Point* pointPtr4 = &popupOffset;
                    pointPtr4.Y -= ((2.0 * (popupOffset.Y - clearZone.Value.Bottom)) + clearZone.Value.Height) + desiredSize.Height;
                }
            }
            popupOffset.Y = Math.Max(screenWorkArea.Top, popupOffset.Y);
            return popupOffset;
        }

        public static void SetOffset(this Popup popup, Point offset)
        {
            if (popup.HorizontalOffset != offset.X)
            {
                popup.HorizontalOffset = offset.X;
            }
            if (popup.VerticalOffset != offset.Y)
            {
                popup.VerticalOffset = offset.Y;
            }
        }
    }
}

