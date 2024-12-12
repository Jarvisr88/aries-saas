namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class PanelControllerBase : Controller
    {
        private Style _HorzScrollBarStyle;
        private Style _VertScrollBarStyle;
        private Style _CornerBoxStyle;

        public PanelControllerBase(DevExpress.Xpf.Core.IPanel control) : base(control)
        {
        }

        protected override void CheckScrollBars()
        {
            if (base.HasScrollBars())
            {
                this.CreateScrollBars();
            }
            else
            {
                this.DestroyScrollBars();
            }
        }

        private void CreateScrollBars()
        {
            if (base.HorzScrollBar == null)
            {
                ScrollBar bar1 = new ScrollBar();
                bar1.Orientation = Orientation.Horizontal;
                base.HorzScrollBar = bar1;
                this.InitScrollBar(base.HorzScrollBar);
                base.HorzScrollBar.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.HorzScrollBarStyle);
                ScrollBar bar2 = new ScrollBar();
                bar2.Orientation = Orientation.Vertical;
                base.VertScrollBar = bar2;
                this.InitScrollBar(base.VertScrollBar);
                base.VertScrollBar.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.VertScrollBarStyle);
                base.CornerBox = new CornerBox();
                base.CornerBox.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.CornerBoxStyle);
                base.CornerBox.SetZIndex(0x3e8);
                if (base.Control.IsInitialized)
                {
                    this.Children.Add(base.HorzScrollBar);
                    this.Children.Add(base.VertScrollBar);
                    this.Children.Add(base.CornerBox);
                }
            }
        }

        private void DestroyScrollBars()
        {
            if (base.HorzScrollBar != null)
            {
                if (this.Children.Contains(base.HorzScrollBar))
                {
                    this.Children.Remove(base.HorzScrollBar);
                }
                if (this.Children.Contains(base.VertScrollBar))
                {
                    this.Children.Remove(base.VertScrollBar);
                }
                if (this.Children.Contains(base.CornerBox))
                {
                    this.Children.Remove(base.CornerBox);
                }
                base.HorzScrollBar.Scroll -= new ScrollEventHandler(this.ScrollBarScroll);
                base.VertScrollBar.Scroll -= new ScrollEventHandler(this.ScrollBarScroll);
                base.HorzScrollBar = null;
                base.VertScrollBar = null;
                base.CornerBox = null;
            }
        }

        public virtual void GetClientPadding(ref Thickness padding)
        {
            if (base.HasScrollBars() && !this.FloatingScrollBars)
            {
                this.GetScrollBarPadding(ref padding);
            }
        }

        protected virtual Rect GetCornerBoxBounds() => 
            new Rect(this.ScrollBarAreaBounds.BottomRight, new Size(base.VertScrollBar.DesiredSize.Width, base.HorzScrollBar.DesiredSize.Height));

        protected virtual Rect GetHorzScrollBarBounds()
        {
            Rect scrollBarAreaBounds = this.ScrollBarAreaBounds;
            scrollBarAreaBounds.Y = scrollBarAreaBounds.Bottom;
            scrollBarAreaBounds.Height = base.HorzScrollBar.DesiredSize.Height;
            return scrollBarAreaBounds;
        }

        private void GetScrollBarPadding(ref Thickness padding)
        {
            if (base.IsHorzScrollBarVisible)
            {
                padding.Bottom += base.HorzScrollBar.DesiredSize.Height;
            }
            if (base.IsVertScrollBarVisible)
            {
                padding.Right += base.VertScrollBar.DesiredSize.Width;
            }
        }

        protected virtual Rect GetVertScrollBarBounds()
        {
            Rect scrollBarAreaBounds = this.ScrollBarAreaBounds;
            scrollBarAreaBounds.X = scrollBarAreaBounds.Right;
            scrollBarAreaBounds.Width = base.VertScrollBar.DesiredSize.Width;
            return scrollBarAreaBounds;
        }

        private void InitScrollBar(ScrollBar scrollBar)
        {
            Binding binding = new Binding();
            binding.Source = base.Control;
            binding.Path = new PropertyPath(ScrollBarExtensions.ScrollViewerMouseMovedProperty);
            scrollBar.SetBinding(ScrollBarExtensions.ScrollViewerMouseMovedProperty, binding);
            Binding binding2 = new Binding();
            binding2.Source = base.Control;
            binding2.Path = new PropertyPath(ScrollBarExtensions.ScrollViewerSizeChangedProperty);
            scrollBar.SetBinding(ScrollBarExtensions.ScrollViewerSizeChangedProperty, binding2);
            scrollBar.SetZIndex(0x3e8);
            scrollBar.Visibility = Visibility.Collapsed;
            scrollBar.Scroll += new ScrollEventHandler(this.ScrollBarScroll);
        }

        public virtual void MeasureScrollBars()
        {
            if (base.HasScrollBars())
            {
                Size infinite = SizeHelper.Infinite;
                base.HorzScrollBar.Measure(infinite);
                base.VertScrollBar.Measure(infinite);
                base.CornerBox.Measure(infinite);
            }
        }

        public virtual void OnArrange(Size finalSize)
        {
            if (base.DragAndDropController != null)
            {
                base.DragAndDropController.OnArrange(finalSize);
            }
        }

        public virtual void OnInitialized()
        {
            if (base.HorzScrollBar != null)
            {
                this.Children.Add(base.HorzScrollBar);
                this.Children.Add(base.VertScrollBar);
                this.Children.Add(base.CornerBox);
            }
        }

        public virtual void OnMeasure(Size availableSize)
        {
            if (base.DragAndDropController != null)
            {
                base.DragAndDropController.OnMeasure(availableSize);
            }
        }

        protected override void UpdateScrollBars()
        {
            base.HorzScrollBar.SetVisible(base.IsHorzScrollBarVisible);
            base.VertScrollBar.SetVisible(base.IsVertScrollBarVisible);
            base.HorzScrollBar.Arrange(base.IsHorzScrollBarVisible ? this.GetHorzScrollBarBounds() : UIElementExtensions.InvisibleBounds);
            base.VertScrollBar.Arrange(base.IsVertScrollBarVisible ? this.GetVertScrollBarBounds() : UIElementExtensions.InvisibleBounds);
            bool flag = (base.IsHorzScrollBarVisible && base.IsVertScrollBarVisible) && !this.FloatingScrollBars;
            base.CornerBox.Arrange(flag ? this.GetCornerBoxBounds() : UIElementExtensions.InvisibleBounds);
        }

        public Rect ClientBounds =>
            this.IPanel.ClientBounds;

        public Rect ContentBounds =>
            this.IPanel.ContentBounds;

        public DevExpress.Xpf.Core.IPanel IPanel =>
            base.IControl as DevExpress.Xpf.Core.IPanel;

        protected UIElementCollection Children =>
            this.IPanel.Children;

        public Style HorzScrollBarStyle
        {
            get => 
                this._HorzScrollBarStyle;
            set
            {
                if (!ReferenceEquals(this.HorzScrollBarStyle, value))
                {
                    this._HorzScrollBarStyle = value;
                    if (base.HorzScrollBar != null)
                    {
                        base.HorzScrollBar.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.HorzScrollBarStyle);
                    }
                }
            }
        }

        public Style VertScrollBarStyle
        {
            get => 
                this._VertScrollBarStyle;
            set
            {
                if (!ReferenceEquals(this.VertScrollBarStyle, value))
                {
                    this._VertScrollBarStyle = value;
                    if (base.VertScrollBar != null)
                    {
                        base.VertScrollBar.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.VertScrollBarStyle);
                    }
                }
            }
        }

        public Style CornerBoxStyle
        {
            get => 
                this._CornerBoxStyle;
            set
            {
                if (!ReferenceEquals(this.CornerBoxStyle, value))
                {
                    this._CornerBoxStyle = value;
                    if (base.CornerBox != null)
                    {
                        base.CornerBox.SetValueIfNotDefault(FrameworkElement.StyleProperty, this.CornerBoxStyle);
                    }
                }
            }
        }

        protected virtual bool FloatingScrollBars =>
            ScrollBarExtensions.GetScrollBarMode(base.Control) == ScrollBarMode.TouchOverlap;

        protected override Rect ScrollableAreaBounds =>
            this.ClientBounds;

        protected virtual Rect ScrollBarAreaBounds
        {
            get
            {
                Rect clientBounds = this.ClientBounds;
                if (this.FloatingScrollBars)
                {
                    Thickness padding = new Thickness(0.0);
                    this.GetScrollBarPadding(ref padding);
                    RectHelper.Deflate(ref clientBounds, padding);
                }
                return clientBounds;
            }
        }
    }
}

