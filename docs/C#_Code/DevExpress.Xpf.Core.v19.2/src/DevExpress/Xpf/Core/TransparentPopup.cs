namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Interop;
    using System.Windows.Media;

    public class TransparentPopup
    {
        private double _HorizontalOffset;
        private bool _IsOpen;
        private double _VerticalOffset;

        private bool HideInHost()
        {
            if (this.Host == null)
            {
                return false;
            }
            this.Host.Child = null;
            ((AdornerLayer) this.Host.Parent).Remove(this.Host);
            this.Host = null;
            return true;
        }

        private void HidePopup()
        {
            this.Popup.Child = null;
            this.Popup.IsOpen = false;
            this.Popup = null;
        }

        public void MakeVisible()
        {
            Point? offset = null;
            Rect? clearZone = null;
            this.MakeVisible(offset, clearZone);
        }

        public void MakeVisible(Point? offset)
        {
            Rect? clearZone = null;
            this.MakeVisible(offset, clearZone);
        }

        public void MakeVisible(Point? offset, Rect? clearZone)
        {
            if (offset == null)
            {
                offset = new Point(this.HorizontalOffset, this.VerticalOffset);
            }
            offset = new Point?(PopupExtensions.MakeVisible(this.PlacementTarget, offset.Value, clearZone, this.Child));
            this.HorizontalOffset = offset.Value.X;
            this.VerticalOffset = offset.Value.Y;
        }

        public void SetOffset(Point offset)
        {
            this.HorizontalOffset = offset.X;
            this.VerticalOffset = offset.Y;
        }

        private bool ShowInHost()
        {
            Page content = Application.Current.MainWindow.Content as Page;
            if (content != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(content);
                if (adornerLayer != null)
                {
                    this.Host = new PopupAdorner(content);
                    adornerLayer.Add(this.Host);
                    this.Host.Child = this.Child;
                    this.UpdatePositionInHost();
                    return true;
                }
            }
            return false;
        }

        private void ShowPopup()
        {
            this.Popup = new System.Windows.Controls.Primitives.Popup();
            this.Popup.AllowsTransparency = true;
            this.Popup.Placement = PlacementMode.Relative;
            this.Popup.PlacementTarget = this.PlacementTarget;
            this.Popup.UseLayoutRounding = true;
            this.Popup.Unloaded += (o, e) => (this.IsOpen = false);
            this.Popup.Child = this.Child;
            this.Popup.IsOpen = true;
            this.UpdatePopupPosition();
        }

        private void UpdatePopupPosition()
        {
            if (this.Popup != null)
            {
                double horizontalOffset = this.HorizontalOffset;
                if (SystemParameters.MenuDropAlignment && (this.Popup.Child != null))
                {
                    horizontalOffset += this.Popup.Child.DesiredSize.Width;
                }
                this.Popup.HorizontalOffset = horizontalOffset;
                this.Popup.VerticalOffset = this.VerticalOffset;
            }
        }

        public void UpdatePosition()
        {
            if (this.IsOpen && (this.PlacementTarget != null))
            {
                this.HorizontalOffset++;
                this.HorizontalOffset--;
            }
        }

        private void UpdatePositionInHost()
        {
            if (this.Host != null)
            {
                Point p = new Point(this.HorizontalOffset, this.VerticalOffset);
                p = (this.PlacementTarget == null) ? this.Host.MapPointFromScreen(p) : this.PlacementTarget.TranslatePoint(p, this.Host);
                this.Host.ChildPosition = p;
            }
        }

        public UIElement Child { get; set; }

        public double HorizontalOffset
        {
            get => 
                this._HorizontalOffset;
            set
            {
                if (this.HorizontalOffset != value)
                {
                    this._HorizontalOffset = value;
                    this.UpdatePopupPosition();
                    this.UpdatePositionInHost();
                }
            }
        }

        public bool IsOpen
        {
            get => 
                this._IsOpen;
            set
            {
                if (this.IsOpen != value)
                {
                    this._IsOpen = value;
                    if (!this.IsOpen)
                    {
                        if (!this.HideInHost())
                        {
                            this.HidePopup();
                        }
                    }
                    else if (!BrowserInteropHelper.IsBrowserHosted || !this.ShowInHost())
                    {
                        this.ShowPopup();
                    }
                }
            }
        }

        public UIElement PlacementTarget { get; set; }

        public System.Windows.Controls.Primitives.Popup Popup { get; private set; }

        public double VerticalOffset
        {
            get => 
                this._VerticalOffset;
            set
            {
                if (this.VerticalOffset != value)
                {
                    this._VerticalOffset = value;
                    this.UpdatePopupPosition();
                    this.UpdatePositionInHost();
                }
            }
        }

        private PopupAdorner Host { get; set; }

        private class PopupAdorner : Adorner
        {
            private static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(typeof(TransparentPopup.PopupAdorner));
            private static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(typeof(TransparentPopup.PopupAdorner));
            private UIElement _Child;
            private Point _ChildPosition;

            public PopupAdorner(UIElement adornedElement) : base(adornedElement)
            {
                object content = Application.Current.MainWindow.Content;
                Binding binding = new Binding("FontFamily");
                binding.Source = content;
                base.SetBinding(FontFamilyProperty, binding);
                Binding binding2 = new Binding("FontSize");
                binding2.Source = content;
                base.SetBinding(FontSizeProperty, binding2);
                this.Container = new Canvas();
                base.AddVisualChild(this.Container);
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                this.Container.Arrange(new Rect(finalSize));
                return base.ArrangeOverride(finalSize);
            }

            protected override Visual GetVisualChild(int index) => 
                this.Container;

            protected override Size MeasureOverride(Size constraint)
            {
                this.Container.Measure(constraint);
                return base.MeasureOverride(constraint);
            }

            protected void UpdateChildPosition()
            {
                if (this.Child != null)
                {
                    Canvas.SetLeft(this.Child, this.ChildPosition.X);
                    Canvas.SetTop(this.Child, this.ChildPosition.Y);
                }
            }

            public UIElement Child
            {
                get => 
                    this._Child;
                set
                {
                    if (!ReferenceEquals(this.Child, value))
                    {
                        if (this.Child != null)
                        {
                            this.Container.Children.Remove(this.Child);
                        }
                        this._Child = value;
                        if (this.Child != null)
                        {
                            this.Container.Children.Add(this.Child);
                        }
                        this.UpdateChildPosition();
                    }
                }
            }

            public Point ChildPosition
            {
                get => 
                    this._ChildPosition;
                set
                {
                    if (this.ChildPosition != value)
                    {
                        this._ChildPosition = value;
                        this.UpdateChildPosition();
                    }
                }
            }

            protected Canvas Container { get; private set; }

            protected override int VisualChildrenCount =>
                (this.Container != null) ? 1 : 0;
        }
    }
}

