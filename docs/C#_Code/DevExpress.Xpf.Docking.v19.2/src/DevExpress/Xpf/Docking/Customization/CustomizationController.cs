namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class CustomizationController : DependencyObject, ICustomizationController, IControlHost, IDisposable
    {
        public static readonly DependencyProperty IsCustomizationProperty;
        public static readonly DependencyProperty CustomizationItemsProperty;
        public static readonly DependencyProperty CustomizationRootProperty;
        public static readonly DependencyProperty DragInfoProperty;
        public static readonly RoutedEvent DragInfoChangedEvent;
        private bool isDisposing;
        private BarManagerMenuController itemContextMenuController;
        private BarManagerMenuController itemsSelectorMenuController;
        private BarManagerMenuController layoutControlItemContextMenuController;
        private BarManagerMenuController layoutControlItemCustomizationMenuController;
        private BarManagerMenuController hiddenItemsMenuController;
        private readonly ContextMenuManager contextMenuManager;
        private DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility visibilityCore;
        private DevExpress.Xpf.Bars.BarManager barManagerCore;
        private bool isOwnBarManager;
        private int lockHideCustomization;
        private static int menuItemCounter;
        private static int closedItemCounter;

        public event DragInfoChangedEventHandler DragInfoChanged
        {
            add
            {
                this.Container.AddHandler(DragInfoChangedEvent, value);
            }
            remove
            {
                this.Container.RemoveHandler(DragInfoChangedEvent, value);
            }
        }

        static CustomizationController()
        {
            DependencyPropertyRegistrator<CustomizationController> registrator = new DependencyPropertyRegistrator<CustomizationController>();
            registrator.Register<bool>("IsCustomization", ref IsCustomizationProperty, false, new PropertyChangedCallback(CustomizationController.OnIsCustomizationChanged), new CoerceValueCallback(CustomizationController.CoerceIsCustomization));
            registrator.Register<ObservableCollection<BaseLayoutItem>>("CustomizationItems", ref CustomizationItemsProperty, null, null, null);
            registrator.Register<LayoutGroup>("CustomizationRoot", ref CustomizationRootProperty, null, new PropertyChangedCallback(CustomizationController.OnCustomizationRootChanged), new CoerceValueCallback(CustomizationController.CoerceCustomizationRoot));
            registrator.Register<DevExpress.Xpf.Docking.Customization.DragInfo>("DragInfo", ref DragInfoProperty, null, null, null);
            registrator.RegisterDirectEvent<DragInfoChangedEventHandler>("DragInfoChanged", ref DragInfoChangedEvent);
        }

        public CustomizationController(DockLayoutManager container)
        {
            this.Container = container;
            this.Container.Unloaded += new RoutedEventHandler(this.OnContainerUnloaded);
            this.Container.ClosedPanels.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnClosedPanelsCollectionChanged);
            this.CustomizationItems = new ObservableCollection<BaseLayoutItem>();
            this.contextMenuManager = new ContextMenuManager(this);
            base.Dispatcher.BeginInvoke(new Action(this.InitializeContextMenuManager), new object[0]);
        }

        public void BeginCustomization()
        {
            if (this.Container.AllowCustomization)
            {
                this.IsCustomization = true;
                this.CustomizationRoot = this.Container.LayoutRoot;
                this.ShowCustomizationForm();
            }
        }

        public void CloseMenu()
        {
            this.contextMenuManager.Reset();
            menuItemCounter = 0;
        }

        protected virtual object CoerceCustomizationRoot(LayoutGroup baseValue) => 
            baseValue;

        private static object CoerceCustomizationRoot(DependencyObject obj, object baseValue) => 
            ((CustomizationController) obj).CoerceCustomizationRoot((LayoutGroup) baseValue);

        private static object CoerceIsCustomization(DependencyObject dObj, object value) => 
            ((CustomizationController) dObj).Container.IsInDesignTime ? true : value;

        protected virtual DevExpress.Xpf.Bars.BarManager CreateBarManager() => 
            new DockBarManager(this.Container);

        protected virtual DevExpress.Xpf.Docking.ClosedItemsBar CreateClosedItemsBar()
        {
            DevExpress.Xpf.Docking.ClosedItemsBar child = new DevExpress.Xpf.Docking.ClosedItemsBar(this.Container.ClosedItemsPanel);
            ((ILogicalOwner) this.Container).AddChild(child);
            return child;
        }

        public T CreateCommand<T>() where T: CustomizationControllerCommand, new()
        {
            T local1 = Activator.CreateInstance<T>();
            local1.Controller = this;
            return local1;
        }

        protected virtual DevExpress.Xpf.Docking.VisualElements.CustomizationControl CreateCustomizationControl() => 
            new DevExpress.Xpf.Docking.VisualElements.CustomizationControl();

        protected virtual FloatingContainer CreateCustomizationForm() => 
            CustomizationFormFactory.CreateCustomizationForm(this.Container, this.CustomizationControl);

        protected virtual FloatingContainer CreateDocumentSelectorContainer() => 
            DocumentSelectorContainerFactory.CreateDocumentSelectorContainer(this.Container, this.DocumentSelectorControl);

        protected virtual DocumentSelector CreateDocumentSelectorControl() => 
            new DocumentSelector();

        protected virtual FloatingContainer CreateDragCursorContainer(Size size = new Size()) => 
            DragCursorFactory.CreateDragCursorContainer(this.Container, this.DragCursorControl, size);

        protected virtual DevExpress.Xpf.Docking.VisualElements.DragCursorControl CreateDragCursorControl() => 
            new DevExpress.Xpf.Docking.VisualElements.DragCursorControl();

        protected virtual DevExpress.Xpf.Docking.HiddenItemContextMenu CreateHiddenItemMenu() => 
            new DevExpress.Xpf.Docking.HiddenItemContextMenu(this.Container);

        protected virtual DevExpress.Xpf.Docking.ItemContextMenu CreateItemContextMenu() => 
            new DevExpress.Xpf.Docking.ItemContextMenu(this.Container);

        protected virtual DevExpress.Xpf.Docking.ItemsSelectorMenu CreateItemsSelectorMenu() => 
            new DevExpress.Xpf.Docking.ItemsSelectorMenu(this.Container);

        protected virtual DevExpress.Xpf.Docking.LayoutControlItemContextMenu CreateLayoutControlItemContextMenu() => 
            new DevExpress.Xpf.Docking.LayoutControlItemContextMenu(this.Container);

        protected virtual DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu CreateLayoutControlItemCustomizationMenu() => 
            new DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu(this.Container);

        protected virtual BarManagerMenuController CreateMenuController() => 
            new BarManagerMenuController();

        private void CustomizationFormClosed(object sender, RoutedEventArgs e)
        {
            CustomizationFormVisibleChangedEventArgs args1 = new CustomizationFormVisibleChangedEventArgs(false);
            args1.Source = this.Container;
            this.Container.RaiseEvent(args1);
            if (this.lockHideCustomization <= 0)
            {
                this.EndCustomization();
            }
        }

        public void DesignTimeRaiseEvent(object sender, RoutedEventArgs e)
        {
            if (!this.isDisposing)
            {
                IUIElement rootUIScope = LayoutItemsHelper.GetIUIParent(sender as DependencyObject).GetRootUIScope();
                if (rootUIScope != null)
                {
                    LayoutView view = this.Container.GetView(rootUIScope) as LayoutView;
                    if (view != null)
                    {
                        view.OnDesignTimeEvent(sender, e);
                    }
                }
            }
        }

        private void DisposeCustomizationForm()
        {
            if (this.CustomizationForm != null)
            {
                this.CustomizationForm.BeginUpdate();
                this.CustomizationForm.Hidden -= new RoutedEventHandler(this.CustomizationFormClosed);
                if (this.CustomizationForm.IsOpen)
                {
                    this.CustomizationForm.IsOpen = false;
                }
                this.CustomizationForm.Close();
                this.CustomizationForm.Content = null;
                this.CustomizationForm = null;
            }
            if (this.CustomizationControl != null)
            {
                DockLayoutManager.SetDockLayoutManager(this.CustomizationControl, null);
                this.CustomizationControl.Dispose();
                this.CustomizationControl = null;
            }
        }

        public void EndCustomization()
        {
            this.ClearSelection();
            this.HideCustomizationForm();
            this.DisposeCustomizationForm();
            this.IsCustomization = false;
        }

        protected bool EnsureClosedItemsBar()
        {
            if (this.Container.ClosedItemsPanel == null)
            {
                return false;
            }
            if (this.ClosedItemsBar == null)
            {
                this.ClosedItemsBar = this.CreateClosedItemsBar();
            }
            else if (this.ClosedItemsBar.Panel == null)
            {
                this.ClosedItemsBar.UpdatePanel(this.Container.ClosedItemsPanel);
            }
            return (this.ClosedItemsBar != null);
        }

        protected bool EnsureCustomizationControl()
        {
            this.CustomizationControl ??= this.CreateCustomizationControl();
            return (this.CustomizationControl != null);
        }

        protected bool EnsureCustomizationForm()
        {
            if ((this.CustomizationForm == null) && this.EnsureCustomizationControl())
            {
                this.CustomizationForm = this.CreateCustomizationForm();
                this.CustomizationForm.Hidden += new RoutedEventHandler(this.CustomizationFormClosed);
            }
            return (this.CustomizationForm != null);
        }

        private void EnsureDockLayoutManager()
        {
            if ((this.CustomizationControl != null) && (this.Container != null))
            {
                DockLayoutManager.SetDockLayoutManager(this.CustomizationControl, this.Container);
            }
        }

        protected bool EnsureDocumentSelectorContainer()
        {
            if ((this.DocumentSelectorContainer == null) && this.EnsureDocumentSelectorControl())
            {
                this.DocumentSelectorContainer = this.CreateDocumentSelectorContainer();
            }
            return (this.DocumentSelectorContainer != null);
        }

        protected bool EnsureDocumentSelectorControl()
        {
            if (this.DocumentSelectorControl == null)
            {
                this.DocumentSelectorControl = this.CreateDocumentSelectorControl();
                WindowHelper.BindFlowDirection(this.DocumentSelectorControl, this.Container);
            }
            return (this.DocumentSelectorControl != null);
        }

        protected bool EnsureDragCursorContainer(Size size = new Size())
        {
            if (this.DragCursorContainer != null)
            {
                Size size2 = new Size();
                this.DragCursorContainer.FloatSize = (size == size2) ? new Size(150.0, 150.0) : size;
                this.DragCursorContainer.Content = this.DragCursorControl;
            }
            else if (this.EnsureDragCursorControl())
            {
                this.DragCursorContainer = this.CreateDragCursorContainer(size);
            }
            if (this.Container.OwnsFloatWindows)
            {
                Func<FloatingWindowContainer, WindowContentHolder> evaluator = <>c.<>9__165_0;
                if (<>c.<>9__165_0 == null)
                {
                    Func<FloatingWindowContainer, WindowContentHolder> local1 = <>c.<>9__165_0;
                    evaluator = <>c.<>9__165_0 = x => x.Window;
                }
                (this.DragCursorContainer as FloatingWindowContainer).With<FloatingWindowContainer, WindowContentHolder>(evaluator).Do<WindowContentHolder>(delegate (WindowContentHolder x) {
                    x.Owner = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this.Container);
                });
            }
            return (this.DragCursorContainer != null);
        }

        protected bool EnsureDragCursorControl()
        {
            this.DragCursorControl ??= this.CreateDragCursorControl();
            return (this.DragCursorControl != null);
        }

        public FrameworkElement[] GetChildren()
        {
            List<FrameworkElement> list = new List<FrameworkElement>();
            if ((this.barManagerCore != null) && this.isOwnBarManager)
            {
                list.Add(this.barManagerCore);
            }
            if (this.IsCustomization)
            {
                if (this.IsCustomizationFormVisible)
                {
                    if (this.CustomizationForm != null)
                    {
                        list.Add(this.CustomizationForm);
                    }
                    if (this.CustomizationControl != null)
                    {
                        list.Add(this.CustomizationControl);
                        FrameworkElement[] children = this.CustomizationControl.GetChildren();
                        for (int i = 0; i < children.Length; i++)
                        {
                            if (children[i] != null)
                            {
                                list.Add(children[i]);
                            }
                        }
                    }
                }
                if (this.IsDragCursorVisible)
                {
                    if (this.DragCursorContainer != null)
                    {
                        list.Add(this.DragCursorContainer);
                    }
                    if (this.DragCursorControl != null)
                    {
                        list.Add(this.DragCursorControl);
                        FrameworkElement[] children = this.DragCursorControl.GetChildren();
                        for (int i = 0; i < children.Length; i++)
                        {
                            if (children[i] != null)
                            {
                                list.Add(children[i]);
                            }
                        }
                    }
                }
                if (this.IsDocumentSelectorContainerVisible)
                {
                    if (this.DocumentSelectorContainer != null)
                    {
                        list.Add(this.DocumentSelectorContainer);
                    }
                    if (this.DocumentSelectorControl != null)
                    {
                        list.Add(this.DocumentSelectorControl);
                        FrameworkElement[] children = this.DocumentSelectorControl.GetChildren();
                        for (int i = 0; i < children.Length; i++)
                        {
                            if (children[i] != null)
                            {
                                list.Add(children[i]);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        private unsafe Point GetDragCursorPos(Point screenPoint, BaseLayoutItem dragItem)
        {
            if ((dragItem == null) || (!dragItem.IsItemWithRestrictedFloating() || !dragItem.AllowFloat))
            {
                Thickness floatingBorderMargin = dragItem.GetFloatingBorderMargin();
                screenPoint -= new Vector(floatingBorderMargin.Left, floatingBorderMargin.Top);
            }
            else
            {
                Rect dragCursorBounds = dragItem.DragCursorBounds;
                screenPoint -= new Vector(dragCursorBounds.Location.X, dragCursorBounds.Location.Y);
                if (!this.Container.IsDesktopFloatingMode)
                {
                    Rect bounds = new Rect(screenPoint, dragCursorBounds.Size);
                    screenPoint = this.Container.CorrectBoundsInAdorner(bounds).Location;
                }
                if (this.Container.FlowDirection == FlowDirection.RightToLeft)
                {
                    Point* pointPtr1 = &screenPoint;
                    pointPtr1.X += dragCursorBounds.Size.Width;
                }
            }
            return screenPoint;
        }

        public static string GetUniqueClosedItemName() => 
            $"ClosedItem{++closedItemCounter}";

        public static string GetUniqueMenuItemName() => 
            $"MenuItem{++menuItemCounter}";

        public void HideClosedItemsBar()
        {
            if (this.IsClosedPanelsVisible)
            {
                this.Container.LockClosedPanelsVisibility();
                try
                {
                    this.ClosedItemsBar.Visible = false;
                }
                finally
                {
                    this.Container.UnlockClosedPanelsVisibility();
                }
                closedItemCounter = this.ClosedItemsBar.ItemLinks.Count;
            }
        }

        public void HideCustomizationForm()
        {
            if (this.CustomizationForm != null)
            {
                this.lockHideCustomization++;
                this.CustomizationForm.IsOpen = false;
                this.Container.RemoveFromLogicalTree(this.CustomizationForm, this.CustomizationControl);
                DockLayoutManager.SetDockLayoutManager(this.CustomizationControl, null);
                this.lockHideCustomization--;
            }
        }

        public void HideDocumentSelectorForm()
        {
            if (this.IsDocumentSelectorContainerVisible)
            {
                this.DocumentSelectorControl.Close();
            }
        }

        public void HideDragCursor()
        {
            if (this.IsDragCursorVisible)
            {
                this.DragCursorContainer.IsOpen = false;
                this.Container.RemoveFromLogicalTree(this.DragCursorContainer, this.DragCursorControl);
                this.DragCursorContainer.Content = null;
                this.DragCursorControl.DragItem = null;
            }
        }

        private void InitializeContextMenuManager()
        {
            if (!this.isDisposing)
            {
                this.contextMenuManager.Initialize();
            }
        }

        protected virtual void InitializeMenuController(BarManagerMenuController controller)
        {
            controller.DataContext = null;
            DockLayoutManager.AddLogicalChild(this.Container, controller);
        }

        protected virtual void OnClosedPanelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.EnsureClosedItemsBar())
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        this.ClosedItemsBar.AddItems(e.NewItems);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        this.ClosedItemsBar.RemoveItems(e.OldItems);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        this.ClosedItemsBar.ResetItems(this.Container.ClosedPanels);
                        break;

                    default:
                        break;
                }
                this.UpdateClosedItemsBarVisibility();
            }
        }

        private void OnContainerUnloaded(object sender, RoutedEventArgs e)
        {
            this.contextMenuManager.Reset();
        }

        protected virtual void OnCustomizationRootChanged(LayoutGroup root)
        {
            this.ClearSelection();
            this.CustomizationItems.Clear();
            this.CustomizationItems.Add(root);
        }

        private static void OnCustomizationRootChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CustomizationController) obj).OnCustomizationRootChanged((LayoutGroup) e.NewValue);
        }

        protected virtual void OnDisposing()
        {
            this.Container.Unloaded -= new RoutedEventHandler(this.OnContainerUnloaded);
            this.Container.ClosedPanels.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnClosedPanelsCollectionChanged);
            this.barManagerCore = null;
            this.itemContextMenuController = null;
            this.itemsSelectorMenuController = null;
            this.layoutControlItemContextMenuController = null;
            this.layoutControlItemCustomizationMenuController = null;
            this.ClosedItemsBar = null;
            this.DisposeCustomizationForm();
            FloatingContainer dragCursorContainer = this.DragCursorContainer;
            if (dragCursorContainer == null)
            {
                FloatingContainer local1 = dragCursorContainer;
            }
            else
            {
                dragCursorContainer.Close();
            }
            this.contextMenuManager.Dispose();
            this.Container = null;
        }

        private static void OnIsCustomizationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DockLayoutManager container = ((CustomizationController) obj).Container;
            container.CoerceValue(DockLayoutManager.IsCustomizationProperty);
            IsCustomizationChangedEventArgs args1 = new IsCustomizationChangedEventArgs((bool) e.NewValue);
            args1.Source = container;
            container.RaiseEvent(args1);
        }

        public void SetDragCursorPosition(Point point)
        {
            if (this.IsDragCursorVisible)
            {
                this.DragCursorContainer.FloatLocation = this.GetDragCursorPos(point, this.DragCursorControl.DragItem);
            }
        }

        public void ShowClosedItemsBar()
        {
            if ((!this.IsClosedPanelsVisible && this.CanShowClosedItems) && this.EnsureClosedItemsBar())
            {
                this.Container.LockClosedPanelsVisibility();
                try
                {
                    this.ClosedItemsBar.Visible = true;
                }
                finally
                {
                    this.Container.UnlockClosedPanelsVisibility();
                }
            }
        }

        public void ShowContextMenu(BaseLayoutItem item)
        {
            this.CloseMenu();
            this.ItemContextMenuController.Menu = this.ItemContextMenu;
            UIElement placementTarget = this.MenuSource ?? (item.GetUIElement<IUIElement>() as UIElement);
            this.ItemContextMenu.Show(item, placementTarget);
            this.MenuSource = null;
        }

        public void ShowControlItemContextMenu(BaseLayoutItem item)
        {
            this.CloseMenu();
            if (this.IsCustomization)
            {
                this.ShowControlItemCustomizationMenu(item);
            }
            else
            {
                this.ShowControlItemNormalMenu(item);
            }
        }

        private void ShowControlItemCustomizationMenu(BaseLayoutItem item)
        {
            this.LayoutControlItemCustomizationMenuController.Menu = this.LayoutControlItemCustomizationMenu;
            UIElement source = this.MenuSource ?? (item.GetUIElement<IUIElement>() as UIElement);
            this.LayoutControlItemCustomizationMenu.Show(source, this.Selection.ToArray());
            this.MenuSource = null;
        }

        private void ShowControlItemNormalMenu(BaseLayoutItem item)
        {
            this.LayoutControlItemContextMenuController.Menu = this.LayoutControlItemContextMenu;
            this.LayoutControlItemContextMenu.Show(item, null);
        }

        public void ShowCustomizationForm()
        {
            if ((this.IsCustomization && !this.IsCustomizationFormVisible) && this.EnsureCustomizationForm())
            {
                this.Container.AddToLogicalTree(this.CustomizationForm, this.CustomizationControl);
                this.EnsureDockLayoutManager();
                this.CustomizationForm.IsOpen = true;
                CustomizationFormVisibleChangedEventArgs e = new CustomizationFormVisibleChangedEventArgs(true);
                e.Source = this.Container;
                this.Container.RaiseEvent(e);
            }
        }

        public void ShowDocumentSelectorForm()
        {
            if (this.Container.AllowDocumentSelector && this.EnsureDocumentSelectorContainer())
            {
                this.DocumentSelectorControl.InitializeItems(this.Container);
                if (this.DocumentSelectorControl.HasItemsToShow)
                {
                    this.Container.AddToLogicalTree(this.DocumentSelectorContainer, this.DocumentSelectorControl);
                    this.DocumentSelectorControl.SetSelectedItem();
                    this.DocumentSelectorControl.PanelsCaption = DockingLocalizer.GetString(DockingStringId.DocumentSelectorPanels);
                    this.DocumentSelectorControl.DocumentsCaption = DockingLocalizer.GetString(DockingStringId.DocumentSelectorDocuments);
                    this.DocumentSelectorContainer.ContainerTemplate = DocumentSelectorContainerFactory.GetTemplate(this.Container);
                    Size size = DocumentSelectorContainerFactory.GetSize(this.Container, new Size(400.0, 220.0));
                    this.DocumentSelectorContainer.FloatSize = size;
                    this.DocumentSelectorContainer.FloatLocation = DocumentSelectorContainerFactory.GetLocation(this.Container, size);
                    this.DocumentSelectorContainer.IsOpen = true;
                }
            }
        }

        public void ShowDragCursor(Point point, BaseLayoutItem item)
        {
            if (!this.IsDragCursorVisible)
            {
                Size size = new Size();
                if (item.IsItemWithRestrictedFloating() && item.AllowFloat)
                {
                    size = item.DragCursorBounds.Size;
                }
                if (this.EnsureDragCursorContainer(size))
                {
                    this.DragCursorControl.DragItem = item;
                    this.DragCursorContainer.FloatLocation = this.GetDragCursorPos(point, item);
                    this.Container.AddToLogicalTree(this.DragCursorContainer, this.DragCursorControl);
                    this.DragCursorContainer.IsOpen = true;
                }
            }
        }

        public void ShowHiddenItemMenu(BaseLayoutItem item)
        {
            this.CloseMenu();
            this.HiddenItemsMenuController.Menu = this.HiddenItemContextMenu;
            UIElement placementTarget = this.MenuSource ?? (item.GetUIElement<IUIElement>() as UIElement);
            this.HiddenItemContextMenu.Show(item, placementTarget);
            this.MenuSource = null;
        }

        public void ShowItemSelectorMenu(UIElement source, BaseLayoutItem[] items)
        {
            this.CloseMenu();
            this.ItemsSelectorMenuController.Menu = this.ItemsSelectorMenu;
            this.ItemsSelectorMenu.Show(source, items);
        }

        void IDisposable.Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        public void UpdateClosedItemsBar()
        {
            if (this.EnsureClosedItemsBar())
            {
                this.ClosedItemsBar.UpdateItems(this.Container.ClosedPanels);
                this.UpdateClosedItemsBarVisibility();
            }
        }

        protected void UpdateClosedItemsBarVisibility()
        {
            if (this.IsHideClosedItems)
            {
                this.HideClosedItemsBar();
            }
            else if (this.IsAutoShowClosedItems)
            {
                if (this.Container.ClosedPanels.Count == 0)
                {
                    this.HideClosedItemsBar();
                }
                else
                {
                    this.ShowClosedItemsBar();
                }
            }
        }

        public void UpdateDragInfo(DevExpress.Xpf.Docking.Customization.DragInfo value)
        {
            if (this.DragInfo != value)
            {
                this.DragInfo = value;
                this.Container.RaiseEvent(new DragInfoChangedEventArgs(this.DragInfo));
            }
        }

        public bool IsCustomization
        {
            get => 
                (bool) base.GetValue(IsCustomizationProperty);
            set => 
                base.SetValue(IsCustomizationProperty, value);
        }

        public DevExpress.Xpf.Docking.Customization.DragInfo DragInfo
        {
            get => 
                (DevExpress.Xpf.Docking.Customization.DragInfo) base.GetValue(DragInfoProperty);
            set => 
                base.SetValue(DragInfoProperty, value);
        }

        public ObservableCollection<BaseLayoutItem> CustomizationItems
        {
            get => 
                (ObservableCollection<BaseLayoutItem>) base.GetValue(CustomizationItemsProperty);
            set => 
                base.SetValue(CustomizationItemsProperty, value);
        }

        public LayoutGroup CustomizationRoot
        {
            get => 
                (LayoutGroup) base.GetValue(CustomizationRootProperty);
            set => 
                base.SetValue(CustomizationRootProperty, value);
        }

        protected DevExpress.Xpf.Docking.Selection Selection =>
            this.Container.LayoutController.Selection;

        public BarManagerMenuController ItemContextMenuController
        {
            get
            {
                BarManagerMenuController itemContextMenuController = this.itemContextMenuController;
                if (this.itemContextMenuController == null)
                {
                    BarManagerMenuController controller;
                    BarManagerMenuController local1 = this.itemContextMenuController;
                    this.itemContextMenuController = controller = this.CreateMenuController();
                    itemContextMenuController = controller.Do<BarManagerMenuController>(new Action<BarManagerMenuController>(this.InitializeMenuController));
                }
                return itemContextMenuController;
            }
        }

        public BarManagerMenuController ItemsSelectorMenuController
        {
            get
            {
                BarManagerMenuController itemsSelectorMenuController = this.itemsSelectorMenuController;
                if (this.itemsSelectorMenuController == null)
                {
                    BarManagerMenuController controller;
                    BarManagerMenuController local1 = this.itemsSelectorMenuController;
                    this.itemsSelectorMenuController = controller = this.CreateMenuController();
                    itemsSelectorMenuController = controller.Do<BarManagerMenuController>(new Action<BarManagerMenuController>(this.InitializeMenuController));
                }
                return itemsSelectorMenuController;
            }
        }

        public BarManagerMenuController LayoutControlItemContextMenuController
        {
            get
            {
                BarManagerMenuController layoutControlItemContextMenuController = this.layoutControlItemContextMenuController;
                if (this.layoutControlItemContextMenuController == null)
                {
                    BarManagerMenuController controller;
                    BarManagerMenuController local1 = this.layoutControlItemContextMenuController;
                    this.layoutControlItemContextMenuController = controller = this.CreateMenuController();
                    layoutControlItemContextMenuController = controller.Do<BarManagerMenuController>(new Action<BarManagerMenuController>(this.InitializeMenuController));
                }
                return layoutControlItemContextMenuController;
            }
        }

        public BarManagerMenuController LayoutControlItemCustomizationMenuController
        {
            get
            {
                BarManagerMenuController layoutControlItemCustomizationMenuController = this.layoutControlItemCustomizationMenuController;
                if (this.layoutControlItemCustomizationMenuController == null)
                {
                    BarManagerMenuController controller;
                    BarManagerMenuController local1 = this.layoutControlItemCustomizationMenuController;
                    this.layoutControlItemCustomizationMenuController = controller = this.CreateMenuController();
                    layoutControlItemCustomizationMenuController = controller.Do<BarManagerMenuController>(new Action<BarManagerMenuController>(this.InitializeMenuController));
                }
                return layoutControlItemCustomizationMenuController;
            }
        }

        public BarManagerMenuController HiddenItemsMenuController
        {
            get
            {
                BarManagerMenuController hiddenItemsMenuController = this.hiddenItemsMenuController;
                if (this.hiddenItemsMenuController == null)
                {
                    BarManagerMenuController controller;
                    BarManagerMenuController local1 = this.hiddenItemsMenuController;
                    this.hiddenItemsMenuController = controller = this.CreateMenuController();
                    hiddenItemsMenuController = controller.Do<BarManagerMenuController>(new Action<BarManagerMenuController>(this.InitializeMenuController));
                }
                return hiddenItemsMenuController;
            }
        }

        public DockLayoutManager Container { get; private set; }

        public DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility ClosedPanelsBarVisibility
        {
            get => 
                this.visibilityCore;
            set
            {
                if (this.visibilityCore != value)
                {
                    this.visibilityCore = value;
                    this.UpdateClosedItemsBarVisibility();
                }
            }
        }

        protected DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility ActualClosedItemsBarVisibility =>
            (this.ClosedPanelsBarVisibility != DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Default) ? this.ClosedPanelsBarVisibility : DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Manual;

        private bool CanShowClosedItems =>
            (this.Container.ClosedPanels.Count > 0) && (this.ClosedPanelsBarVisibility != DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Never);

        private bool IsAutoShowClosedItems =>
            this.ActualClosedItemsBarVisibility == DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Auto;

        private bool IsHideClosedItems =>
            !this.Container.IsLoaded || (this.ActualClosedItemsBarVisibility == DevExpress.Xpf.Docking.Base.ClosedPanelsBarVisibility.Never);

        public DevExpress.Xpf.Bars.BarManager BarManager
        {
            get
            {
                if (this.barManagerCore == null)
                {
                    this.barManagerCore = LayoutHelper.FindParentObject<DevExpress.Xpf.Bars.BarManager>(this.Container);
                    if (this.barManagerCore == null)
                    {
                        this.barManagerCore = this.CreateBarManager();
                        this.isOwnBarManager = true;
                        DockLayoutManager.AddToVisualTree(this.Container, this.barManagerCore);
                        DockLayoutManager.AddLogicalChild(this.Container, this.barManagerCore);
                    }
                }
                return this.barManagerCore;
            }
        }

        protected DevExpress.Xpf.Docking.ItemContextMenu ItemContextMenu =>
            this.contextMenuManager.ItemsContextMenu;

        protected DevExpress.Xpf.Docking.ItemsSelectorMenu ItemsSelectorMenu =>
            this.contextMenuManager.ItemsSelectorMenu;

        protected DevExpress.Xpf.Docking.LayoutControlItemContextMenu LayoutControlItemContextMenu =>
            this.contextMenuManager.LayoutControlItemMenu;

        protected DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu LayoutControlItemCustomizationMenu =>
            this.contextMenuManager.LayoutControlItemCustomizationMenu;

        protected DevExpress.Xpf.Docking.HiddenItemContextMenu HiddenItemContextMenu =>
            this.contextMenuManager.HiddenItemMenu;

        public UIElement MenuSource { get; set; }

        public DevExpress.Xpf.Docking.ClosedItemsBar ClosedItemsBar { get; private set; }

        public FloatingContainer CustomizationForm { get; private set; }

        public DevExpress.Xpf.Docking.VisualElements.CustomizationControl CustomizationControl { get; private set; }

        public bool IsClosedPanelsVisible =>
            (this.ClosedItemsBar != null) && this.ClosedItemsBar.Visible;

        public bool ClosedPanelsVisibility
        {
            get => 
                this.IsClosedPanelsVisible;
            set
            {
                if (value)
                {
                    this.ShowClosedItemsBar();
                }
                else
                {
                    this.HideClosedItemsBar();
                }
            }
        }

        public bool IsCustomizationFormVisible =>
            (this.CustomizationForm != null) && this.CustomizationForm.IsOpen;

        protected bool HasBarManager =>
            this.barManagerCore != null;

        protected bool IsDocumentSelectorContainerVisible =>
            (this.DocumentSelectorContainer != null) && this.DocumentSelectorContainer.IsOpen;

        public bool IsDocumentSelectorVisible =>
            this.IsDocumentSelectorContainerVisible;

        protected FloatingContainer DocumentSelectorContainer { get; private set; }

        protected DocumentSelector DocumentSelectorControl { get; private set; }

        protected FloatingContainer DragCursorContainer { get; private set; }

        protected internal DevExpress.Xpf.Docking.VisualElements.DragCursorControl DragCursorControl { get; private set; }

        public bool IsDragCursorVisible =>
            (this.DragCursorContainer != null) && this.DragCursorContainer.IsOpen;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomizationController.<>c <>9 = new CustomizationController.<>c();
            public static Func<FloatingWindowContainer, WindowContentHolder> <>9__165_0;

            internal WindowContentHolder <EnsureDragCursorContainer>b__165_0(FloatingWindowContainer x) => 
                x.Window;
        }

        private class ContextMenuManager : IDisposable
        {
            private readonly Dictionary<Type, PopupMenu> menuCache = new Dictionary<Type, PopupMenu>();
            private readonly Dictionary<Type, CreateInstance> initializers = new Dictionary<Type, CreateInstance>();
            private CustomizationController Controller;
            private readonly List<BaseLayoutElementMenu> menus = new List<BaseLayoutElementMenu>();
            private readonly Locker initializeLocker = new Locker();
            private bool isDisposing;

            public ContextMenuManager(CustomizationController controller)
            {
                this.Controller = controller;
                this.InitializeFactory();
            }

            public void Dispose()
            {
                if (!this.isDisposing)
                {
                    this.isDisposing = true;
                    this.Controller = null;
                }
                GC.SuppressFinalize(this);
            }

            private TMenu GetMenuByType<TMenu>() where TMenu: PopupMenu
            {
                PopupMenu menu;
                CreateInstance instance;
                if (!this.menuCache.TryGetValue(typeof(TMenu), out menu) && this.initializers.TryGetValue(typeof(TMenu), out instance))
                {
                    menu = instance();
                    this.menuCache[typeof(TMenu)] = menu;
                }
                TMenu local = menu as TMenu;
                if (local == null)
                {
                    throw new NotSupportedException("type");
                }
                return local;
            }

            public void Initialize()
            {
                if (!this.initializeLocker)
                {
                    this.initializeLocker.LockOnce();
                    foreach (KeyValuePair<Type, CreateInstance> pair in this.initializers)
                    {
                        PopupMenu menu;
                        if (!this.menuCache.TryGetValue(pair.Key, out menu))
                        {
                            menu = pair.Value();
                            this.menuCache[pair.Key] = menu;
                        }
                    }
                }
            }

            private void InitializeFactory()
            {
                this.initializers[typeof(DevExpress.Xpf.Docking.ItemsSelectorMenu)] = (CreateInstance) (() => this.Controller.CreateItemsSelectorMenu().Do<DevExpress.Xpf.Docking.ItemsSelectorMenu>(x => this.menus.Add(x)));
                this.initializers[typeof(ItemContextMenu)] = (CreateInstance) (() => this.Controller.CreateItemContextMenu().Do<ItemContextMenu>(x => this.menus.Add(x)));
                this.initializers[typeof(LayoutControlItemContextMenu)] = (CreateInstance) (() => this.Controller.CreateLayoutControlItemContextMenu().Do<LayoutControlItemContextMenu>(x => this.menus.Add(x)));
                this.initializers[typeof(DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu)] = (CreateInstance) (() => this.Controller.CreateLayoutControlItemCustomizationMenu().Do<DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu>(x => this.menus.Add(x)));
                this.initializers[typeof(HiddenItemContextMenu)] = (CreateInstance) (() => this.Controller.CreateHiddenItemMenu().Do<HiddenItemContextMenu>(x => this.menus.Add(x)));
            }

            public void Reset()
            {
                foreach (BaseLayoutElementMenu menu in this.menus)
                {
                    menu.Close();
                    menu.ClearItems();
                }
            }

            public DevExpress.Xpf.Docking.ItemsSelectorMenu ItemsSelectorMenu =>
                this.GetMenuByType<DevExpress.Xpf.Docking.ItemsSelectorMenu>();

            public ItemContextMenu ItemsContextMenu =>
                this.GetMenuByType<ItemContextMenu>();

            public LayoutControlItemContextMenu LayoutControlItemMenu =>
                this.GetMenuByType<LayoutControlItemContextMenu>();

            public DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu LayoutControlItemCustomizationMenu =>
                this.GetMenuByType<DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenu>();

            public HiddenItemContextMenu HiddenItemMenu =>
                this.GetMenuByType<HiddenItemContextMenu>();

            private delegate PopupMenu CreateInstance();
        }

        private class DockBarManager : BarManager
        {
            public DockBarManager(DockLayoutManager container)
            {
                base.AllowCustomization = false;
                base.CreateStandardLayout = false;
                DXSerializer.SetEnabled(this, false);
            }
        }
    }
}

