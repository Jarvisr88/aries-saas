namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DXWindowSmartCenteringPanel : Border
    {
        private System.Windows.Window windowCore;
        private DXWindowHeader popupCore;

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            TextBlock child = base.Child as TextBlock;
            double actualWidth = 0.0;
            if ((this.Window != null) || (this.Popup != null))
            {
                if (this.Window == null)
                {
                    if (double.IsNaN(this.Popup.ActualWidth))
                    {
                        return arrangeBounds;
                    }
                    actualWidth = this.Popup.ActualWidth;
                }
                else
                {
                    DXWindow window = this.Window as DXWindow;
                    if ((window != null) && !window.ShowTitle)
                    {
                        base.Visibility = Visibility.Collapsed;
                        return new Size(0.0, 0.0);
                    }
                    if (double.IsNaN(this.Window.ActualWidth))
                    {
                        return arrangeBounds;
                    }
                    actualWidth = this.GetRealWindowWidth(this.Window);
                }
                double x = ((actualWidth - child.DesiredSize.Width) / 2.0) - this.IconRight;
                double num5 = x + child.DesiredSize.Width;
                double y = Math.Round((double) ((arrangeBounds.Height - child.DesiredSize.Height) / 2.0)) - 1.0;
                Point location = new Point(x, y);
                double width = arrangeBounds.Width;
                if (arrangeBounds.Width <= (num5 + base.Padding.Right))
                {
                    width = arrangeBounds.Width - base.Padding.Right;
                }
                bool flag = num5 > width;
                SetTexVisibility(child);
                if (!flag)
                {
                    child.Arrange(new Rect(location, child.DesiredSize));
                }
                else
                {
                    location = new Point(x - (num5 - width), y);
                    if (location.X < base.Padding.Left)
                    {
                        location.X = base.Padding.Left;
                    }
                    child.Arrange(new Rect(location, child.DesiredSize));
                }
            }
            return arrangeBounds;
        }

        private double GetIconRight()
        {
            if (this.Popup != null)
            {
                return this.GetIconRightFromPopup(this.Popup);
            }
            if (!(this.Window is DXWindow) && !(this.Window is WindowContentHolder))
            {
                return 0.0;
            }
            WindowContentHolder windowContentHolder = this.Window as WindowContentHolder;
            if (windowContentHolder != null)
            {
                return this.GetIconRightFromWindowContentHolder(windowContentHolder);
            }
            DXWindow dxWindow = this.Window as DXWindow;
            return ((dxWindow == null) ? 0.0 : this.GetIconRightFromDXWindow(dxWindow));
        }

        private double GetIconRightFromDXWindow(DXWindow dxWindow)
        {
            if (!dxWindow.ShowIcon)
            {
                return 0.0;
            }
            Image iconImage = dxWindow.IconImage;
            return ((iconImage != null) ? this.GetRight(iconImage) : 0.0);
        }

        private double GetIconRightFromPopup(DXWindowHeader popup)
        {
            if (!DXWindow.GetShowIcon(popup))
            {
                return 0.0;
            }
            Image icon = (Image) LayoutHelper.FindElementByName(popup, "PART_Icon");
            return (((icon == null) || ((icon.Visibility == Visibility.Collapsed) || (icon.Visibility == Visibility.Hidden))) ? 0.0 : this.GetRight(icon));
        }

        private double GetIconRightFromWindowContentHolder(WindowContentHolder windowContentHolder)
        {
            if (!DXWindow.GetShowIcon(windowContentHolder))
            {
                return 0.0;
            }
            Image icon = (Image) LayoutHelper.FindElementByName(windowContentHolder, "PART_Icon");
            return (((icon == null) || ((icon.Visibility == Visibility.Collapsed) || (icon.Visibility == Visibility.Hidden))) ? 0.0 : this.GetRight(icon));
        }

        private DXWindowHeader GetPopup()
        {
            this.popupCore = LayoutHelper.FindParentObject<DXWindowHeader>(this);
            return this.popupCore;
        }

        private double GetRealWindowWidth(System.Windows.Window window) => 
            (this.Window.SizeToContent != SizeToContent.WidthAndHeight) ? (!double.IsNaN(this.Window.Width) ? this.Window.Width : this.Window.ActualWidth) : this.Window.DesiredSize.Width;

        private double GetRight(Image icon)
        {
            double right = icon.Margin.Right;
            double left = icon.Margin.Left;
            double actualWidth = icon.ActualWidth;
            if (double.IsNaN(actualWidth))
            {
                actualWidth = 0.0;
            }
            return ((left + actualWidth) + right);
        }

        private System.Windows.Window GetWindow()
        {
            this.windowCore = LayoutHelper.FindParentObject<DXWindow>(this);
            this.windowCore ??= LayoutHelper.FindParentObject<WindowContentHolder>(this);
            return this.windowCore;
        }

        private static void SetTexVisibility(TextBlock textBlock)
        {
            int num = 3;
            if (textBlock.DesiredSize.Width < num)
            {
                textBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                textBlock.Visibility = Visibility.Visible;
            }
        }

        private System.Windows.Window Window
        {
            get
            {
                if (this.windowCore == null)
                {
                    this.GetWindow();
                }
                return this.windowCore;
            }
        }

        private DXWindowHeader Popup
        {
            get
            {
                if (this.popupCore == null)
                {
                    this.GetPopup();
                }
                return this.popupCore;
            }
        }

        private double IconRight =>
            ((this.Window == null) || (!(this.Window is WindowContentHolder) && !(this.Window is DXWindow))) ? ((this.Popup == null) ? 0.0 : this.GetIconRight()) : this.GetIconRight();
    }
}

