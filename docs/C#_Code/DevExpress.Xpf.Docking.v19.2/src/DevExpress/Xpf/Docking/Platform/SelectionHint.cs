namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class SelectionHint : DockHintElement
    {
        private static readonly DependencyPropertyKey ShowLeftMarkerPropertyKey;
        public static readonly DependencyProperty ShowLeftMarkerProperty;
        private static readonly DependencyPropertyKey ShowRightMarkerPropertyKey;
        public static readonly DependencyProperty ShowRightMarkerProperty;
        private static readonly DependencyPropertyKey ShowTopMarkerPropertyKey;
        public static readonly DependencyProperty ShowTopMarkerProperty;
        private static readonly DependencyPropertyKey ShowBottomMarkerPropertyKey;
        public static readonly DependencyProperty ShowBottomMarkerProperty;
        private BaseLayoutItem itemCore;

        static SelectionHint()
        {
            DependencyPropertyRegistrator<SelectionHint> registrator = new DependencyPropertyRegistrator<SelectionHint>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.RegisterReadonly<bool>("ShowLeftMarker", ref ShowLeftMarkerPropertyKey, ref ShowLeftMarkerProperty, false, null, null);
            registrator.RegisterReadonly<bool>("ShowRightMarker", ref ShowRightMarkerPropertyKey, ref ShowRightMarkerProperty, false, null, null);
            registrator.RegisterReadonly<bool>("ShowTopMarker", ref ShowTopMarkerPropertyKey, ref ShowTopMarkerProperty, false, null, null);
            registrator.RegisterReadonly<bool>("ShowBottomMarker", ref ShowBottomMarkerPropertyKey, ref ShowBottomMarkerProperty, false, null, null);
        }

        public SelectionHint() : base(DockVisualizerElement.Selection)
        {
            base.IsTabStop = false;
            base.IsHitTestVisible = false;
        }

        protected override Rect CalcBounds(DockingHintAdornerBase adorner)
        {
            Rect itemBounds = this.GetItemBounds();
            if (!itemBounds.IsEmpty)
            {
                RectHelper.Inflate(ref itemBounds, (double) 1.0, 1.0);
            }
            return itemBounds;
        }

        protected override bool CalcVisibleState(DockingHintAdornerBase adorner) => 
            adorner.ShowSelectionHints && ((this.Item != null) && (this.Item.Visibility != Visibility.Collapsed));

        public void EnqueueFocus()
        {
            Action method = () => base.Focus();
            if (base.IsVisible)
            {
                method();
            }
            else
            {
                base.Dispatcher.BeginInvoke(method, DispatcherPriority.Render, new object[0]);
            }
        }

        private void ForceUpdateElementBounds(ILayoutElement element)
        {
            element.Invalidate();
            element.EnsureBounds();
        }

        protected internal Rect GetItemBounds()
        {
            if ((this.Item != null) && ((this.Item.Manager != null) && (this.ItemElement != null)))
            {
                ILayoutElement viewElement = this.Item.Manager.GetViewElement((IUIElement) this.ItemElement);
                if (viewElement != null)
                {
                    this.ForceUpdateElementBounds(viewElement);
                    return ElementHelper.GetRect(viewElement);
                }
            }
            return Rect.Empty;
        }

        protected virtual void OnItemChanged()
        {
            if ((this.Item == null) || (this.Item.Manager == null))
            {
                this.ItemElement = null;
            }
            else
            {
                IUIElement element;
                BaseLayoutItem uIElement = this.Item.GetUIElement<BaseLayoutItem>();
                if (element == null)
                {
                    BaseLayoutItem local1 = this.Item.GetUIElement<BaseLayoutItem>();
                    uIElement = this.Item.GetUIElement<IUIElement>();
                }
                this.ItemElement = (UIElement) uIElement;
            }
            this.UpdateMarkers();
        }

        internal void OnItemElementMouseEnter(object sender, MouseEventArgs e)
        {
            this.UpdateMarkers();
        }

        internal void OnItemElementMouseLeave(object sender, MouseEventArgs e)
        {
            this.ShowLeftMarker = false;
            this.ShowRightMarker = false;
            this.ShowTopMarker = false;
            this.ShowBottomMarker = false;
        }

        protected void UpdateMarkers()
        {
            if (this.Item != null)
            {
                LayoutGroup parent = this.Item.Parent;
                if (parent != null)
                {
                    int index = parent.Items.IndexOf(this.Item);
                    bool isFirst = index == 0;
                    this.UpdateMarkers(parent.Orientation == Orientation.Vertical, isFirst, index == (parent.Items.Count - 1));
                }
            }
        }

        protected void UpdateMarkers(bool isVerticalOrientation, bool isFirst, bool isLast)
        {
            if (isVerticalOrientation)
            {
                this.ShowTopMarker = !isFirst;
                this.ShowBottomMarker = !isLast;
                this.ShowLeftMarker = false;
                this.ShowRightMarker = false;
            }
            else
            {
                this.ShowLeftMarker = !isFirst;
                this.ShowRightMarker = !isLast;
                this.ShowTopMarker = false;
                this.ShowBottomMarker = false;
            }
        }

        public bool ShowLeftMarker
        {
            get => 
                (bool) base.GetValue(ShowLeftMarkerProperty);
            internal set => 
                base.SetValue(ShowLeftMarkerPropertyKey, value);
        }

        public bool ShowRightMarker
        {
            get => 
                (bool) base.GetValue(ShowRightMarkerProperty);
            internal set => 
                base.SetValue(ShowRightMarkerPropertyKey, value);
        }

        public bool ShowTopMarker
        {
            get => 
                (bool) base.GetValue(ShowTopMarkerProperty);
            internal set => 
                base.SetValue(ShowTopMarkerPropertyKey, value);
        }

        public bool ShowBottomMarker
        {
            get => 
                (bool) base.GetValue(ShowBottomMarkerProperty);
            internal set => 
                base.SetValue(ShowBottomMarkerPropertyKey, value);
        }

        public BaseLayoutItem Item
        {
            get => 
                this.itemCore;
            set
            {
                if (!ReferenceEquals(this.Item, value))
                {
                    this.itemCore = value;
                    this.OnItemChanged();
                }
            }
        }

        protected UIElement ItemElement { get; set; }
    }
}

