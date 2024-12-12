namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Xml;

    public class LayoutControlCustomizationController
    {
        private Style _CustomizationControlStyle;
        private FrameworkElement _CustomizationToolbarElement;
        private TransparentPopup _CustomizationToolbarPopup;
        private Style _ItemCustomizationToolbarStyle;
        private Style _ItemParentIndicatorStyle;
        private Style _ItemSelectionIndicatorStyle;
        private Window _RootVisual;

        public event EventHandler<LayoutControlSelectionChangedEventArgs> SelectionChanged;

        public LayoutControlCustomizationController(LayoutControlController controller)
        {
            this.Controller = controller;
        }

        protected virtual void AddNewItem(FrameworkElement container)
        {
            ILayoutControlCustomizableItem item = container as ILayoutControlCustomizableItem;
            if ((item != null) && item.CanAddNewItems)
            {
                FrameworkElement element = item.AddNewItem();
                this.ILayoutControl.InitNewElement(element);
                this.ProcessSelection(element, true);
                this.Control.UpdateLayout();
                this.ShowCustomizationToolbar();
            }
        }

        protected internal virtual void BeginCustomization()
        {
            this.SelectedElements = new FrameworkElements();
            this.SelectedElements.CollectionChanged += (sender, e) => this.OnSelectedElementsChanged(e);
            if (!this.Control.IsInDesignTool())
            {
                this.ShowCustomizationCover();
            }
            this.ShowCustomizationCanvas();
            if (!this.Control.IsInDesignTool())
            {
                this.SelectionIndicators = this.CreateSelectionIndicators(this.CustomizationCanvas);
                this.SelectionIndicators.ItemStyle = this.ItemSelectionIndicatorStyle;
                this.ShowCustomizationControl();
                if (this.Controller.IsLoaded)
                {
                    this.RootVisual = this.Control.FindElementByTypeInParents<Window>(null);
                }
            }
            this.ILayoutControl.AvailableItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnAvailableItemsChanged);
        }

        protected virtual bool CanSetCustomizationToolbarElement(FrameworkElement element) => 
            !this.Control.IsInDesignTool() ? ((element == null) || (((this.CustomizationControl == null) || !this.CustomizationControl.IsAvailableItemsListOpen) ? (!this.Controller.IsDragAndDrop ? ((this.RootVisual == null) || (this.RootVisual.IsActive && (BrowserInteropHelper.IsBrowserHosted || (this.RootVisual.WindowState != WindowState.Minimized)))) : false) : false)) : false;

        protected virtual void ChangeSelectedGroupPadding(bool isHorizontalChange, bool isIncrease)
        {
            Thickness padding = this.SelectedGroup.Padding;
            double num = isHorizontalChange ? padding.Left : padding.Top;
            num = !isIncrease ? (num - 1.0) : (num + 1.0);
            num = Math.Max(0.0, num);
            if (isHorizontalChange)
            {
                padding.Left = padding.Right = num;
            }
            else
            {
                padding.Top = padding.Bottom = num;
            }
            if (this.SelectedGroup.Padding != padding)
            {
                this.SelectedGroup.Padding = padding;
                this.Controller.OnModelChanged(new LayoutControlModelPropertyChangedEventArgs(this.SelectedGroup, "Padding", LayoutControlBase.PaddingProperty));
            }
        }

        public void CheckSelectedElementsAreInVisualTree()
        {
            if (this.SelectedElements != null)
            {
                Func<FrameworkElement, bool> predicate = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<FrameworkElement, bool> local1 = <>c.<>9__8_0;
                    predicate = <>c.<>9__8_0 = element => element.IsInVisualTree();
                }
                List<FrameworkElement> source = new List<FrameworkElement>(this.SelectedElements.Where<FrameworkElement>(predicate));
                this.SelectedElements.Assign(source);
            }
        }

        protected virtual Canvas CreateCustomizationCanvas() => 
            new Canvas();

        protected virtual LayoutControlCustomizationControl CreateCustomizationControl() => 
            new LayoutControlCustomizationControl();

        protected virtual LayoutItemCustomizationToolbar CreateCustomizationToolbar() => 
            new LayoutItemCustomizationToolbar();

        protected virtual FrameworkElement CreateNewItem(LayoutControlNewItemInfo itemInfo)
        {
            if (!(itemInfo.Data is LayoutGroupView))
            {
                return null;
            }
            LayoutGroup group = this.ILayoutControl.CreateGroup();
            group.Header = itemInfo.Label;
            group.View = (LayoutGroupView) itemInfo.Data;
            this.InitNewItem(group);
            return group;
        }

        protected virtual LayoutItemParentIndicator CreateParentIndicator() => 
            new LayoutItemParentIndicator();

        protected virtual LayoutItemSelectionIndicators CreateSelectionIndicators(Panel container) => 
            new LayoutItemSelectionIndicators(container, this.ILayoutControl);

        protected virtual void DeleteAvailableItem(FrameworkElement item)
        {
            this.FocusControl();
            this.ILayoutControl.DeleteAvailableItem(item);
        }

        protected internal virtual void EndCustomization()
        {
            this.ILayoutControl.AvailableItems.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnAvailableItemsChanged);
            this.RootVisual = null;
            this.SelectedElements.Clear();
            if (this.CustomizationToolbar != null)
            {
                this.HideCustomizationToolbar(true);
            }
            if (!this.Control.IsInDesignTool())
            {
                this.HideCustomizationControl();
            }
            this.SelectionIndicators = null;
            this.HideCustomizationCanvas();
            if (!this.Control.IsInDesignTool())
            {
                this.HideCustomizationCover();
            }
            this.SelectedElements = null;
        }

        protected virtual void FinalizeCustomizationControl()
        {
            this.CustomizationControl.IsAvailableItemsListOpenChanged -= new EventHandler(this.OnIsAvailableItemsListOpenChanged);
            this.CustomizationControl.AvailableItems = null;
            this.CustomizationControl.NewItemsInfo = null;
        }

        protected void FocusControl()
        {
            if ((this.CustomizationControl != null) && this.CustomizationControl.IsInVisualTree())
            {
                this.CustomizationControl.Focus();
            }
        }

        protected virtual Geometry GetCustomizationCoverHitTestClip()
        {
            PathGeometry geometry = new PathGeometry {
                Figures = { GraphicsHelper.CreateRectFigure(this.CustomizationCover.GetSize().ToRect()) }
            };
            List<Rect> areas = new List<Rect>();
            this.ILayoutControl.GetLiveCustomizationAreas(areas, this.CustomizationCover);
            foreach (Rect rect in areas)
            {
                geometry.Figures.Add(GraphicsHelper.CreateRectFigure(rect));
            }
            return geometry;
        }

        protected FrameworkElement GetCustomizationToolbarElement(Point p)
        {
            FrameworkElement element2;
            Rect clientBounds = this.ILayoutControl.ClientBounds;
            if (!clientBounds.Contains(p))
            {
                return null;
            }
            using (IEnumerator<FrameworkElement> enumerator = this.SelectedElements.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FrameworkElement current = enumerator.Current;
                        if ((ReferenceEquals(current, this.Control) || !current.IsInVisualTree()) || !this.GetCustomizationToolbarElementBounds(current).Contains(p))
                        {
                            continue;
                        }
                        element2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return element2;
        }

        protected virtual Rect GetCustomizationToolbarElementBounds(FrameworkElement element) => 
            element.GetBounds(this.Control);

        protected virtual Point GetCustomizationToolbarOffset(Rect elementBounds, Size toolbarSize)
        {
            Rect rect = RectHelper.New(toolbarSize);
            RectHelper.AlignHorizontally(ref rect, elementBounds, HorizontalAlignment.Center);
            rect.Y = elementBounds.Bottom;
            return rect.Location();
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__9))]
        public virtual IEnumerable<UIElement> GetInternalElements()
        {
            if (this.CustomizationCover != null)
            {
                yield return this.CustomizationCover;
            }
            if (this.CustomizationCanvas != null)
            {
                yield return this.CustomizationCanvas;
            }
            while (true)
            {
                if (this.CustomizationControl != null)
                {
                    yield return this.CustomizationControl;
                }
            }
        }

        protected virtual LayoutControlNewItemsInfo GetNewItemsInfo()
        {
            LayoutControlNewItemsInfo info1 = new LayoutControlNewItemsInfo();
            info1.Add(new LayoutControlNewItemInfo(LocalizationRes.LayoutControl_Customization_NewGroupBox, LayoutGroupView.GroupBox));
            info1.Add(new LayoutControlNewItemInfo(LocalizationRes.LayoutControl_Customization_NewTabbedGroup, LayoutGroupView.Tabs));
            return info1;
        }

        public virtual FrameworkElement GetSelectableItem(Point p)
        {
            FrameworkElement destination = this.Controller.GetItem(p, false, this.Control.IsInDesignTool());
            if (this.Control.IsInDesignTool() && (destination is LayoutItem))
            {
                destination = ((LayoutItem) destination).GetSelectableElement(this.Control.MapPoint(p, destination));
            }
            if (!this.Control.IsInDesignTool() && ReferenceEquals(destination, this.Control))
            {
                destination = null;
            }
            return destination;
        }

        protected T? GetSelectedElementsProperty<T>(Func<FrameworkElement, T> getElementProperty) where T: struct
        {
            T? nullable2;
            T? nullable = null;
            using (IEnumerator<FrameworkElement> enumerator = this.SelectedElements.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FrameworkElement current = enumerator.Current;
                        if ((current == null) || ReferenceEquals(current, this.Control))
                        {
                            continue;
                        }
                        if (nullable == null)
                        {
                            nullable = new T?(getElementProperty(current));
                            continue;
                        }
                        if (nullable.Equals(getElementProperty(current)))
                        {
                            continue;
                        }
                        nullable2 = null;
                        nullable2 = nullable2;
                    }
                    else
                    {
                        return nullable;
                    }
                    break;
                }
            }
            return nullable2;
        }

        private void HideCustomizationCanvas()
        {
            this.ILayoutControl.Children.Remove(this.CustomizationCanvas);
            this.CustomizationCanvas = null;
        }

        private void HideCustomizationControl()
        {
            this.ILayoutControl.Children.Remove(this.CustomizationControl);
            this.FinalizeCustomizationControl();
            this.CustomizationControl = null;
        }

        private void HideCustomizationCover()
        {
            this.ILayoutControl.Children.Remove(this.CustomizationCover);
            this.CustomizationCover = null;
        }

        private void HideCustomizationToolbar(bool remove)
        {
            this._CustomizationToolbarPopup.IsOpen = false;
            if (remove)
            {
                this._CustomizationToolbarPopup.Child = null;
                this._CustomizationToolbarPopup = null;
                this.CustomizationToolbar.OnHide();
                this.CustomizationToolbar = null;
            }
        }

        private void HideParentIndicator()
        {
            this.CustomizationCanvas.Children.Remove(this.ParentIndicator);
            this.ParentIndicator = null;
        }

        protected virtual void InitCustomizationControl()
        {
            this.CustomizationControl.AvailableItems = this.ILayoutControl.VisibleAvailableItems;
            Binding binding = new Binding("ActualAllowAvailableItemsDuringCustomization");
            binding.Source = this.Control;
            binding.Converter = new BoolToVisibilityConverter();
            this.CustomizationControl.SetBinding(LayoutControlCustomizationControl.AvailableItemsUIVisibilityProperty, binding);
            this.CustomizationControl.NewItemsInfo = this.GetNewItemsInfo();
            Binding binding2 = new Binding("AllowNewItemsDuringCustomization");
            binding2.Source = this.Control;
            binding2.Converter = new BoolToVisibilityConverter();
            this.CustomizationControl.SetBinding(LayoutControlCustomizationControl.NewItemsUIVisibilityProperty, binding2);
            this.CustomizationControl.Style = this.CustomizationControlStyle;
            this.CustomizationControl.DeleteAvailableItem += new Action<FrameworkElement>(this.DeleteAvailableItem);
            this.CustomizationControl.IsAvailableItemsListOpenChanged += new EventHandler(this.OnIsAvailableItemsListOpenChanged);
            this.CustomizationControl.StartAvailableItemDragAndDrop += (o, e) => this.StartAvailableItemDragAndDrop(e);
            this.CustomizationControl.StartNewItemDragAndDrop += (o, e) => this.StartNewItemDragAndDrop(e);
        }

        protected virtual void InitCustomizationToolbar()
        {
            Binding binding = new Binding("ActualAllowAvailableItemsDuringCustomization");
            binding.Source = this.Control;
            binding.Converter = new BoolToVisibilityConverter();
            this.CustomizationToolbar.SetBinding(LayoutItemCustomizationToolbar.AvailableItemsUIVisibilityProperty, binding);
            this.CustomizationToolbar.Style = this.ItemCustomizationToolbarStyle;
            this.UpdateCustomizationToolbarValues();
            this.CustomizationToolbar.AddNewItem += () => this.AddNewItem(this.CustomizationToolbarElement);
            this.CustomizationToolbar.ItemHeaderChanged += () => (((ILayoutControlCustomizableItem) this.CustomizationToolbarElement).Header = this.CustomizationToolbar.ItemHeader);
            this.CustomizationToolbar.ItemHorizontalAlignmentChanged += () => (this.SelectedElementsHorizontalAlignment = this.CustomizationToolbar.ItemHorizontalAlignment);
            this.CustomizationToolbar.ItemVerticalAlignmentChanged += () => (this.SelectedElementsVerticalAlignment = this.CustomizationToolbar.ItemVerticalAlignment);
            this.CustomizationToolbar.MoveItem += forward => this.MoveItem(this.CustomizationToolbarElement, forward);
            this.CustomizationToolbar.MoveItemToAvailableItems += () => this.MoveItemsToAvailableItems(this.SelectedElements);
            this.CustomizationToolbar.ReturnFocus += () => this.FocusControl();
            this.CustomizationToolbar.SelectItemParent += () => this.MoveSelectionToParent(this.CustomizationToolbarElement);
            this.CustomizationToolbar.ShowItemParentIndicator += () => this.ShowParentIndicator(this.CustomizationToolbarElement);
            this.CustomizationToolbar.HideItemParentIndicator += () => this.HideParentIndicator();
        }

        protected virtual void InitNewItem(LayoutGroup group)
        {
            if (group.View == LayoutGroupView.Tabs)
            {
                group.Children.Add(this.ILayoutControl.CreateGroup());
                group.Children.Add(this.ILayoutControl.CreateGroup());
                foreach (FrameworkElement element in group.GetLogicalChildren(false))
                {
                    this.ILayoutControl.InitNewElement(element);
                }
            }
        }

        protected virtual void MoveItem(FrameworkElement item, bool forward)
        {
            ILayoutGroup parent = item.Parent as ILayoutGroup;
            if (parent != null)
            {
                FrameworkElements logicalChildren = parent.GetLogicalChildren(true);
                int index = logicalChildren.IndexOf(item);
                index = !forward ? (index - 1) : (index + 1);
                if ((index >= 0) && (index <= (logicalChildren.Count - 1)))
                {
                    UIElement element = logicalChildren[index];
                    parent.Children.Remove(item);
                    index = parent.Children.IndexOf(element);
                    if (forward)
                    {
                        index++;
                    }
                    parent.Children.Insert(index, item);
                    this.ILayoutControl.MakeControlVisible(item);
                }
            }
        }

        protected virtual void MoveItemsToAvailableItems(FrameworkElements items)
        {
            if (items.Count == 1)
            {
                this.ILayoutControl.AvailableItems.Add(items[0]);
            }
            else
            {
                this.ILayoutControl.AvailableItems.BeginUpdate();
                try
                {
                    foreach (FrameworkElement element in items)
                    {
                        this.ILayoutControl.AvailableItems.Add(element);
                    }
                }
                finally
                {
                    this.ILayoutControl.AvailableItems.EndUpdate();
                }
            }
            this.ILayoutControl.OptimizeLayout(true);
        }

        protected virtual void MoveSelectionToParent(FrameworkElement child)
        {
            this.SelectedElements.BeginUpdate();
            try
            {
                if (child == null)
                {
                    if (this.SelectedElements.Count > 1)
                    {
                        FrameworkElement item = this.SelectedElements[this.SelectedElements.Count - 1];
                        this.SelectedElements.Clear();
                        this.SelectedElements.Add(item);
                    }
                    else if ((this.SelectedElements.Count == 1) && (this.SelectedElements[0] != this.Control))
                    {
                        this.SelectedElements[0] = (FrameworkElement) this.SelectedElements[0].Parent;
                    }
                }
                else if (!ReferenceEquals(child, this.Control))
                {
                    this.SelectedElements.Clear();
                    this.SelectedElements.Add((FrameworkElement) child.Parent);
                }
                else
                {
                    return;
                }
            }
            finally
            {
                this.SelectedElements.EndUpdate();
            }
            this.ShowCustomizationToolbar();
        }

        public virtual void OnArrange(Size finalSize)
        {
            if (this.CustomizationCover != null)
            {
                this.CustomizationCover.Arrange(this.ILayoutControl.ClientBounds);
            }
            if (this.CustomizationCanvas != null)
            {
                this.CustomizationCanvas.Arrange(this.ILayoutControl.ClientBounds);
                this.CustomizationCanvas.ClipToBounds();
            }
            if (this.CustomizationControl != null)
            {
                this.CustomizationControl.Measure(this.ILayoutControl.ClientBounds.Size());
                this.CustomizationControl.Arrange(this.ILayoutControl.ClientBounds);
            }
        }

        protected virtual void OnAvailableItemsChanged(NotifyCollectionChangedEventArgs args)
        {
            IEnumerable<FrameworkElement> collection = from element in this.SelectedElements
                where !this.ILayoutControl.AvailableItems.Contains(element)
                select element;
            this.SelectedElements.Assign(new List<FrameworkElement>(collection));
        }

        private void OnAvailableItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnAvailableItemsChanged(e);
        }

        protected virtual void OnAvailableItemsListClosed()
        {
            this.ShowCustomizationToolbar();
        }

        protected virtual void OnAvailableItemsListOpened()
        {
            this.CustomizationToolbarElement = null;
        }

        protected internal virtual void OnControlVisibilityChanged(FrameworkElement control)
        {
            if (control.Visibility != Visibility.Visible)
            {
                this.SelectedElements.Remove(control);
            }
            else if (this.SelectedElements.Count == 1)
            {
                this.Control.UpdateLayout();
                if (this.SelectedElements.Count == 1)
                {
                    this.ILayoutControl.MakeControlVisible(this.SelectedElements[0]);
                }
            }
        }

        protected internal virtual void OnDropElement(FrameworkElement element)
        {
            this.ProcessSelection(element, true);
        }

        protected internal virtual void OnEndDragAndDrop(bool accept)
        {
            this.Control.UpdateLayout();
            if (accept && (this.SelectedElements.Count == 1))
            {
                this.ILayoutControl.MakeControlVisible(this.SelectedElements[0]);
            }
            if (this.SelectionIndicators != null)
            {
                this.SelectionIndicators.IsVisible = true;
            }
            this.ShowCustomizationToolbar();
            this.FocusControl();
        }

        protected internal virtual void OnGroupCollapsed(ILayoutGroup group)
        {
            foreach (FrameworkElement element in this.SelectedElements)
            {
                if (element.FindIsInParents(group.Control))
                {
                    this.ProcessSelection(group.Control, true);
                    break;
                }
            }
        }

        private void OnIsAvailableItemsListOpenChanged(object sender, EventArgs e)
        {
            if (this.CustomizationControl.IsAvailableItemsListOpen)
            {
                this.OnAvailableItemsListOpened();
            }
            else
            {
                this.OnAvailableItemsListClosed();
            }
        }

        protected internal virtual void OnKeyDown(DXKeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.Escape)
            {
                this.MoveSelectionToParent(null);
                e.Handled = true;
            }
            else
            {
                switch (key)
                {
                    case Key.Left:
                    case Key.Up:
                    case Key.Right:
                    case Key.Down:
                        if ((this.SelectedGroup == null) || this.SelectedGroup.IsLocked)
                        {
                            break;
                        }
                        this.ChangeSelectedGroupPadding((e.Key == Key.Left) || (e.Key == Key.Right), (e.Key == Key.Right) || (e.Key == Key.Up));
                        e.Handled = true;
                        return;

                    case Key.Select:
                    case Key.Print:
                    case Key.Execute:
                    case Key.Snapshot:
                    case Key.Insert:
                    case Key.Delete:
                    case Key.Help:
                        break;

                    case Key.D0:
                    case Key.D1:
                    case Key.D2:
                    case Key.D3:
                    case Key.D4:
                    case Key.D5:
                    case Key.D6:
                    case Key.D7:
                    case Key.D8:
                    case Key.D9:
                        if ((this.SelectedGroup == null) || this.SelectedGroup.IsLocked)
                        {
                            break;
                        }
                        this.SetSelectedGroupItemSpace((double) (e.Key - Key.D0));
                        e.Handled = true;
                        return;

                    default:
                        if (key != Key.F2)
                        {
                            return;
                        }
                        if (!this.Control.IsInDesignTool())
                        {
                            this.ShowLayoutXML();
                            e.Handled = true;
                        }
                        break;
                }
            }
        }

        protected internal virtual void OnLayoutUpdated()
        {
            this.UpdateCustomizationCoverHitTestClip();
            if (this.SelectionIndicators != null)
            {
                this.SelectionIndicators.UpdateBounds();
            }
            if (this.ParentIndicator != null)
            {
                this.ParentIndicator.UpdateBounds();
            }
            this.UpdateCustomizationToolbarValues();
            this.UpdateCustomizationToolbarBounds();
        }

        protected internal virtual void OnLoaded()
        {
            this.RootVisual = this.Control.FindElementByTypeInParents<Window>(null);
        }

        public virtual void OnMeasure(Size availableSize)
        {
            if (this.CustomizationCover != null)
            {
                this.CustomizationCover.Measure(availableSize);
            }
            if (this.CustomizationCanvas != null)
            {
                this.CustomizationCanvas.Measure(availableSize);
            }
        }

        protected internal virtual void OnMouseLeftButtonDown(DXMouseButtonEventArgs e)
        {
            this.ProcessSelection(this.GetSelectableItem(e.GetPosition(this.Control)), Keyboard.Modifiers == ModifierKeys.None);
        }

        protected internal virtual void OnMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            this.ShowCustomizationToolbar(e.GetPosition(this.Control));
        }

        protected internal virtual void OnMouseMove(DXMouseEventArgs e)
        {
            if (DevExpress.Xpf.Core.Controller.MouseCaptureOwner == null)
            {
                this.ShowCustomizationToolbar(e.GetPosition(this.Control));
            }
        }

        protected internal virtual void OnMouseRightButtonDown(DXMouseButtonEventArgs e)
        {
            FrameworkElement selectableItem = this.GetSelectableItem(e.GetPosition(this.Control));
            if (!this.SelectedElements.Contains(selectableItem))
            {
                this.ProcessSelection(selectableItem, true);
            }
        }

        protected virtual void OnSelectedElementsChanged(NotifyCollectionChangedEventArgs e)
        {
            if ((this.CustomizationToolbarElement != null) && !this.SelectedElements.Contains(this.CustomizationToolbarElement))
            {
                this.CustomizationToolbarElement = null;
            }
            if (this.SelectedElements.Count == 1)
            {
                this.ILayoutControl.MakeControlVisible(this.SelectedElements[0]);
                this.Control.UpdateLayout();
            }
            if (this.SelectionIndicators != null)
            {
                this.SelectionIndicators.Update(this.SelectedElements);
            }
            if (this.Controller.IsCustomization)
            {
                this.FocusControl();
            }
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this.Control, new LayoutControlSelectionChangedEventArgs(this.SelectedElements));
            }
        }

        protected internal virtual void OnStartDragAndDrop()
        {
            if (this.SelectionIndicators != null)
            {
                this.SelectionIndicators.IsVisible = false;
            }
            this.CustomizationToolbarElement = null;
        }

        protected internal virtual void OnTabClicked(ILayoutGroup group, FrameworkElement selectedTabChild)
        {
            if (!group.IsLocked)
            {
                FrameworkElement element = selectedTabChild;
                if (selectedTabChild == null)
                {
                    FrameworkElement local1 = selectedTabChild;
                    element = group.Control;
                }
                this.ProcessSelection(element, true);
                this.ShowCustomizationToolbar();
            }
        }

        protected internal virtual void ProcessSelection(FrameworkElement element, bool clearExistingSelection)
        {
            if (element != null)
            {
                this.SelectedElements.BeginUpdate();
                try
                {
                    if (clearExistingSelection)
                    {
                        if (this.SelectedElements.Count == 1)
                        {
                            this.SelectedElements[0] = element;
                        }
                        else
                        {
                            this.SelectedElements.Clear();
                            this.SelectedElements.Add(element);
                        }
                    }
                    else if (!this.SelectedElements.Contains(element))
                    {
                        this.SelectedElements.Remove(this.Control);
                        this.SelectedElements.Add(element);
                    }
                    else
                    {
                        this.SelectedElements.Remove(element);
                        if (this.Control.IsInDesignTool() && (this.SelectedElements.Count == 0))
                        {
                            this.SelectedElements.Add(this.Control);
                        }
                    }
                }
                finally
                {
                    this.SelectedElements.EndUpdate();
                }
            }
        }

        private void RootVisualActivated(object sender, EventArgs e)
        {
            this.ShowCustomizationToolbar();
        }

        private void RootVisualDeactivated(object sender, EventArgs e)
        {
            this.FocusControl();
            this.CustomizationToolbarElement = null;
        }

        private void RootVisualLocationChanged(object sender, EventArgs e)
        {
            this.UpdateCustomizationToolbarBounds();
            if (this._CustomizationToolbarPopup != null)
            {
                this._CustomizationToolbarPopup.UpdatePosition();
            }
        }

        private void RootVisualStateChanged(object sender, EventArgs e)
        {
            if (this.RootVisual.WindowState == WindowState.Minimized)
            {
                this.CustomizationToolbarElement = null;
            }
            else
            {
                this.ShowCustomizationToolbar();
            }
        }

        protected void SetSelectedElementsProperty<T>(Action<FrameworkElement, T> setElementProperty, T value)
        {
            foreach (FrameworkElement element in this.SelectedElements)
            {
                if (!ReferenceEquals(element, this.Control))
                {
                    setElementProperty(element, value);
                }
            }
        }

        protected virtual void SetSelectedGroupItemSpace(double itemSpace)
        {
            if (this.SelectedGroup.ItemSpace != itemSpace)
            {
                this.SelectedGroup.ItemSpace = itemSpace;
                this.Controller.OnModelChanged(new LayoutControlModelPropertyChangedEventArgs(this.SelectedGroup, "ItemSpace", LayoutControlBase.ItemSpaceProperty));
            }
        }

        private void ShowCustomizationCanvas()
        {
            this.CustomizationCanvas = this.CreateCustomizationCanvas();
            this.CustomizationCanvas.SetZIndex(0x3e8);
            this.ILayoutControl.Children.Add(this.CustomizationCanvas);
        }

        private void ShowCustomizationControl()
        {
            this.CustomizationControl = this.CreateCustomizationControl();
            this.InitCustomizationControl();
            this.CustomizationControl.SetZIndex(0x3e8);
            this.ILayoutControl.Children.Add(this.CustomizationControl);
        }

        private void ShowCustomizationCover()
        {
            this.CustomizationCover = new LayoutControlCustomizationCover();
            this.CustomizationCover.SetZIndex(0x3e8);
            this.ILayoutControl.Children.Add(this.CustomizationCover);
        }

        protected void ShowCustomizationToolbar()
        {
            if (this.SelectedElements.Count != 0)
            {
                this.CustomizationToolbarElement = this.SelectedElements[0];
            }
        }

        private void ShowCustomizationToolbar(FrameworkElement element)
        {
            if (this.CustomizationToolbar != null)
            {
                this.UpdateCustomizationToolbarValues();
            }
            else
            {
                this.CustomizationToolbar = this.CreateCustomizationToolbar();
                this.InitCustomizationToolbar();
                this._CustomizationToolbarPopup = new TransparentPopup();
                this._CustomizationToolbarPopup.Child = this.CustomizationToolbar;
                this._CustomizationToolbarPopup.PlacementTarget = this.Control;
            }
            this.UpdateCustomizationToolbarBounds(element);
        }

        protected void ShowCustomizationToolbar(Point p)
        {
            FrameworkElement customizationToolbarElement = this.GetCustomizationToolbarElement(p);
            if (customizationToolbarElement != null)
            {
                this.CustomizationToolbarElement = customizationToolbarElement;
            }
        }

        protected virtual void ShowLayoutXML()
        {
            StringBuilder output = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(output, settings))
            {
                this.ILayoutControl.WriteToXML(writer);
            }
            TextBox box1 = new TextBox();
            box1.FontFamily = new FontFamily("Courier New");
            box1.FontSize = 12.0;
            box1.IsReadOnly = true;
            TextBox textBox = box1;
            textBox.KeyDown += delegate (object sender, KeyEventArgs e) {
                if (e.Key == Key.Escape)
                {
                    ((Popup) textBox.Parent).IsOpen = false;
                    e.Handled = true;
                }
            };
            textBox.LostFocus += (o, e) => (((Popup) textBox.Parent).IsOpen = false);
            textBox.Text = output.ToString();
            textBox.SelectAll();
            Popup popup = new Popup {
                Child = textBox
            };
            popup.Closed += (o, e) => this.FocusControl();
            popup.IsOpen = true;
            textBox.Focus();
        }

        private void ShowParentIndicator(FrameworkElement element)
        {
            this.ParentIndicator = this.CreateParentIndicator();
            this.ParentIndicator.Style = this.ItemParentIndicatorStyle;
            this.CustomizationCanvas.Children.Add(this.ParentIndicator);
            this.ParentIndicator.LayoutControl = this.ILayoutControl;
            this.ParentIndicator.Element = (FrameworkElement) element.Parent;
        }

        protected void StartAvailableItemDragAndDrop(LayoutControlStartDragAndDropEventArgs<FrameworkElement> arguments)
        {
            this.Controller.StartItemDragAndDrop(arguments.Data, arguments.MouseEventArgs, arguments.Source);
        }

        protected void StartNewItemDragAndDrop(LayoutControlStartDragAndDropEventArgs<LayoutControlNewItemInfo> arguments)
        {
            FrameworkElement element = this.CreateNewItem(arguments.Data);
            if (element != null)
            {
                this.ILayoutControl.InitNewElement(element);
                this.Controller.StartItemDragAndDrop(element, arguments.MouseEventArgs, arguments.Source);
            }
        }

        protected void UpdateCustomizationCoverHitTestClip()
        {
            if (this.CustomizationCover != null)
            {
                this.CustomizationCover.HitTestClip = this.GetCustomizationCoverHitTestClip();
            }
        }

        protected void UpdateCustomizationToolbarBounds()
        {
            if (this.CustomizationToolbarElement != null)
            {
                this.UpdateCustomizationToolbarBounds(this.CustomizationToolbarElement);
            }
        }

        private void UpdateCustomizationToolbarBounds(FrameworkElement element)
        {
            Rect bounds = element.GetBounds(this.Control);
            bounds.Intersect(this.ILayoutControl.ClientBounds);
            this._CustomizationToolbarPopup.IsOpen = this.Control.IsVisible() && !bounds.IsEmpty;
            if (this._CustomizationToolbarPopup.IsOpen)
            {
                this.CustomizationToolbar.Measure(SizeHelper.Infinite);
                Point customizationToolbarOffset = this.GetCustomizationToolbarOffset(bounds, this.CustomizationToolbar.DesiredSize);
                this._CustomizationToolbarPopup.MakeVisible(new Point?(customizationToolbarOffset), new Rect?(bounds));
            }
        }

        protected void UpdateCustomizationToolbarValues()
        {
            if (this.CustomizationToolbarElement != null)
            {
                this.CustomizationToolbar.IsInitializing = true;
                try
                {
                    this.UpdateCustomizationToolbarValuesCore();
                }
                finally
                {
                    this.CustomizationToolbar.IsInitializing = false;
                }
            }
        }

        protected virtual void UpdateCustomizationToolbarValuesCore()
        {
            this.CustomizationToolbar.ItemHorizontalAlignment = this.SelectedElementsHorizontalAlignment;
            this.CustomizationToolbar.ItemVerticalAlignment = this.SelectedElementsVerticalAlignment;
            if (!this.CustomizationToolbarElementParent.HasUniformLayout)
            {
                this.CustomizationToolbar.ItemMovingUIVisibility = Visibility.Collapsed;
            }
            else
            {
                Binding binding = new Binding("AllowItemMovingDuringCustomization");
                binding.Source = this.Control;
                binding.Converter = new BoolToVisibilityConverter();
                this.CustomizationToolbar.SetBinding(LayoutItemCustomizationToolbar.ItemMovingUIVisibilityProperty, binding);
            }
            FrameworkElements logicalChildren = this.CustomizationToolbarElementParent.GetLogicalChildren(true);
            this.CustomizationToolbar.CanMoveItemBackward = this.CustomizationToolbarElement != logicalChildren[0];
            this.CustomizationToolbar.CanMoveItemForward = this.CustomizationToolbarElement != logicalChildren[logicalChildren.Count - 1];
            this.CustomizationToolbar.ItemMovingDirection = this.CustomizationToolbarElementParent.VisibleOrientation;
            ILayoutControlCustomizableItem customizationToolbarElement = this.CustomizationToolbarElement as ILayoutControlCustomizableItem;
            if ((customizationToolbarElement == null) || (customizationToolbarElement.IsLocked || (!customizationToolbarElement.HasHeader || ((customizationToolbarElement.Header != null) && !(customizationToolbarElement.Header is string)))))
            {
                this.CustomizationToolbar.ItemRenamingUIVisibility = Visibility.Collapsed;
            }
            else
            {
                this.CustomizationToolbar.ItemHeader = customizationToolbarElement.Header;
                Binding binding = new Binding("AllowItemRenamingDuringCustomization");
                binding.Source = this.Control;
                binding.Converter = new BoolToVisibilityConverter();
                this.CustomizationToolbar.SetBinding(LayoutItemCustomizationToolbar.ItemRenamingUIVisibilityProperty, binding);
            }
            if ((customizationToolbarElement == null) || (customizationToolbarElement.IsLocked || !customizationToolbarElement.CanAddNewItems))
            {
                this.CustomizationToolbar.NewItemsUIVisibility = Visibility.Collapsed;
            }
            else
            {
                Binding binding = new Binding("AllowNewItemsDuringCustomization");
                binding.Source = this.Control;
                binding.Converter = new BoolToVisibilityConverter();
                this.CustomizationToolbar.SetBinding(LayoutItemCustomizationToolbar.NewItemsUIVisibilityProperty, binding);
            }
        }

        public FrameworkElement Control =>
            this.Controller.Control;

        public Canvas CustomizationCanvas { get; private set; }

        public Style CustomizationControlStyle
        {
            get => 
                this._CustomizationControlStyle;
            set
            {
                if (!ReferenceEquals(this.CustomizationControlStyle, value))
                {
                    this._CustomizationControlStyle = value;
                    if (this.CustomizationControl != null)
                    {
                        this.CustomizationControl.Style = this.CustomizationControlStyle;
                    }
                }
            }
        }

        public DevExpress.Xpf.LayoutControl.ILayoutControl ILayoutControl =>
            this.Controller.ILayoutControl;

        public Style ItemCustomizationToolbarStyle
        {
            get => 
                this._ItemCustomizationToolbarStyle;
            set
            {
                if (!ReferenceEquals(this.ItemCustomizationToolbarStyle, value))
                {
                    this._ItemCustomizationToolbarStyle = value;
                    if (this.CustomizationToolbar != null)
                    {
                        this.CustomizationToolbar.Style = this.ItemCustomizationToolbarStyle;
                    }
                }
            }
        }

        public Style ItemParentIndicatorStyle
        {
            get => 
                this._ItemParentIndicatorStyle;
            set
            {
                if (!ReferenceEquals(this.ItemParentIndicatorStyle, value))
                {
                    this._ItemParentIndicatorStyle = value;
                    if (this.ParentIndicator != null)
                    {
                        this.ParentIndicator.Style = this.ItemParentIndicatorStyle;
                    }
                }
            }
        }

        public Style ItemSelectionIndicatorStyle
        {
            get => 
                this._ItemSelectionIndicatorStyle;
            set
            {
                if (!ReferenceEquals(this.ItemSelectionIndicatorStyle, value))
                {
                    this._ItemSelectionIndicatorStyle = value;
                    if (this.SelectionIndicators != null)
                    {
                        this.SelectionIndicators.ItemStyle = this.ItemSelectionIndicatorStyle;
                    }
                }
            }
        }

        public FrameworkElements SelectedElements { get; private set; }

        protected LayoutControlController Controller { get; private set; }

        protected LayoutControlCustomizationControl CustomizationControl { get; private set; }

        protected LayoutControlCustomizationCover CustomizationCover { get; private set; }

        protected LayoutItemCustomizationToolbar CustomizationToolbar { get; private set; }

        protected FrameworkElement CustomizationToolbarElement
        {
            get => 
                this._CustomizationToolbarElement;
            set
            {
                if (ReferenceEquals(value, this.Control))
                {
                    value = null;
                }
                if (!ReferenceEquals(this.CustomizationToolbarElement, value) && this.CanSetCustomizationToolbarElement(value))
                {
                    FrameworkElement customizationToolbarElement = this.CustomizationToolbarElement;
                    this._CustomizationToolbarElement = value;
                    if (customizationToolbarElement != null)
                    {
                        this.HideCustomizationToolbar(false);
                    }
                    if (this.CustomizationToolbarElement != null)
                    {
                        this.ShowCustomizationToolbar(this.CustomizationToolbarElement);
                    }
                }
            }
        }

        protected ILayoutGroup CustomizationToolbarElementParent =>
            this.CustomizationToolbarElement.Parent as ILayoutGroup;

        protected LayoutItemParentIndicator ParentIndicator { get; private set; }

        protected Window RootVisual
        {
            get => 
                this._RootVisual;
            private set
            {
                if (!ReferenceEquals(this.RootVisual, value))
                {
                    if (this.RootVisual != null)
                    {
                        this.RootVisual.Activated -= new EventHandler(this.RootVisualActivated);
                        this.RootVisual.Deactivated -= new EventHandler(this.RootVisualDeactivated);
                        this.RootVisual.LocationChanged -= new EventHandler(this.RootVisualLocationChanged);
                        this.RootVisual.StateChanged -= new EventHandler(this.RootVisualStateChanged);
                    }
                    this._RootVisual = value;
                    if (this.RootVisual != null)
                    {
                        this.RootVisual.Activated += new EventHandler(this.RootVisualActivated);
                        this.RootVisual.Deactivated += new EventHandler(this.RootVisualDeactivated);
                        this.RootVisual.LocationChanged += new EventHandler(this.RootVisualLocationChanged);
                        this.RootVisual.StateChanged += new EventHandler(this.RootVisualStateChanged);
                    }
                }
            }
        }

        protected HorizontalAlignment? SelectedElementsHorizontalAlignment
        {
            get
            {
                Func<FrameworkElement, HorizontalAlignment> getElementProperty = <>c.<>9__112_0;
                if (<>c.<>9__112_0 == null)
                {
                    Func<FrameworkElement, HorizontalAlignment> local1 = <>c.<>9__112_0;
                    getElementProperty = <>c.<>9__112_0 = delegate (FrameworkElement element) {
                        Func<HorizontalAlignment> fallback = <>c.<>9__112_2;
                        if (<>c.<>9__112_2 == null)
                        {
                            Func<HorizontalAlignment> local1 = <>c.<>9__112_2;
                            fallback = <>c.<>9__112_2 = () => HorizontalAlignment.Stretch;
                        }
                        return (element.Parent as ILayoutGroup).Return<ILayoutGroup, HorizontalAlignment>(x => x.GetItemHorizontalAlignment(element), fallback);
                    };
                }
                return this.GetSelectedElementsProperty<HorizontalAlignment>(getElementProperty);
            }
            set
            {
                if (value != null)
                {
                    Action<FrameworkElement, HorizontalAlignment> setElementProperty = <>c.<>9__113_0;
                    if (<>c.<>9__113_0 == null)
                    {
                        Action<FrameworkElement, HorizontalAlignment> local1 = <>c.<>9__113_0;
                        setElementProperty = <>c.<>9__113_0 = (element, alignment) => (element.Parent as ILayoutGroup).Do<ILayoutGroup>(x => x.SetItemHorizontalAlignment(element, alignment, true));
                    }
                    this.SetSelectedElementsProperty<HorizontalAlignment>(setElementProperty, value.Value);
                }
            }
        }

        protected VerticalAlignment? SelectedElementsVerticalAlignment
        {
            get
            {
                Func<FrameworkElement, VerticalAlignment> getElementProperty = <>c.<>9__115_0;
                if (<>c.<>9__115_0 == null)
                {
                    Func<FrameworkElement, VerticalAlignment> local1 = <>c.<>9__115_0;
                    getElementProperty = <>c.<>9__115_0 = delegate (FrameworkElement element) {
                        Func<VerticalAlignment> fallback = <>c.<>9__115_2;
                        if (<>c.<>9__115_2 == null)
                        {
                            Func<VerticalAlignment> local1 = <>c.<>9__115_2;
                            fallback = <>c.<>9__115_2 = () => VerticalAlignment.Stretch;
                        }
                        return (element.Parent as ILayoutGroup).Return<ILayoutGroup, VerticalAlignment>(x => x.GetItemVerticalAlignment(element), fallback);
                    };
                }
                return this.GetSelectedElementsProperty<VerticalAlignment>(getElementProperty);
            }
            set
            {
                if (value != null)
                {
                    Action<FrameworkElement, VerticalAlignment> setElementProperty = <>c.<>9__116_0;
                    if (<>c.<>9__116_0 == null)
                    {
                        Action<FrameworkElement, VerticalAlignment> local1 = <>c.<>9__116_0;
                        setElementProperty = <>c.<>9__116_0 = (element, alignment) => (element.Parent as ILayoutGroup).Do<ILayoutGroup>(x => x.SetItemVerticalAlignment(element, alignment, true));
                    }
                    this.SetSelectedElementsProperty<VerticalAlignment>(setElementProperty, value.Value);
                }
            }
        }

        protected LayoutGroup SelectedGroup =>
            ((this.SelectedElements.Count != 1) || ((this.SelectedElements[0] != this.Control) && !this.SelectedElements[0].IsLayoutGroup())) ? null : ((LayoutGroup) this.SelectedElements[0]);

        protected LayoutItemSelectionIndicators SelectionIndicators { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlCustomizationController.<>c <>9 = new LayoutControlCustomizationController.<>c();
            public static Func<FrameworkElement, bool> <>9__8_0;
            public static Func<HorizontalAlignment> <>9__112_2;
            public static Func<FrameworkElement, HorizontalAlignment> <>9__112_0;
            public static Action<FrameworkElement, HorizontalAlignment> <>9__113_0;
            public static Func<VerticalAlignment> <>9__115_2;
            public static Func<FrameworkElement, VerticalAlignment> <>9__115_0;
            public static Action<FrameworkElement, VerticalAlignment> <>9__116_0;

            internal bool <CheckSelectedElementsAreInVisualTree>b__8_0(FrameworkElement element) => 
                element.IsInVisualTree();

            internal HorizontalAlignment <get_SelectedElementsHorizontalAlignment>b__112_0(FrameworkElement element)
            {
                Func<HorizontalAlignment> fallback = <>9__112_2;
                if (<>9__112_2 == null)
                {
                    Func<HorizontalAlignment> local1 = <>9__112_2;
                    fallback = <>9__112_2 = () => HorizontalAlignment.Stretch;
                }
                return (element.Parent as ILayoutGroup).Return<ILayoutGroup, HorizontalAlignment>(x => x.GetItemHorizontalAlignment(element), fallback);
            }

            internal HorizontalAlignment <get_SelectedElementsHorizontalAlignment>b__112_2() => 
                HorizontalAlignment.Stretch;

            internal VerticalAlignment <get_SelectedElementsVerticalAlignment>b__115_0(FrameworkElement element)
            {
                Func<VerticalAlignment> fallback = <>9__115_2;
                if (<>9__115_2 == null)
                {
                    Func<VerticalAlignment> local1 = <>9__115_2;
                    fallback = <>9__115_2 = () => VerticalAlignment.Stretch;
                }
                return (element.Parent as ILayoutGroup).Return<ILayoutGroup, VerticalAlignment>(x => x.GetItemVerticalAlignment(element), fallback);
            }

            internal VerticalAlignment <get_SelectedElementsVerticalAlignment>b__115_2() => 
                VerticalAlignment.Stretch;

            internal void <set_SelectedElementsHorizontalAlignment>b__113_0(FrameworkElement element, HorizontalAlignment alignment)
            {
                (element.Parent as ILayoutGroup).Do<ILayoutGroup>(x => x.SetItemHorizontalAlignment(element, alignment, true));
            }

            internal void <set_SelectedElementsVerticalAlignment>b__116_0(FrameworkElement element, VerticalAlignment alignment)
            {
                (element.Parent as ILayoutGroup).Do<ILayoutGroup>(x => x.SetItemVerticalAlignment(element, alignment, true));
            }
        }

    }
}

