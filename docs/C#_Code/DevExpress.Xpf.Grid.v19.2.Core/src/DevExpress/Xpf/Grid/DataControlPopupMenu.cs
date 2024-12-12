namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public abstract class DataControlPopupMenu : GridPopupMenuBase
    {
        private DataViewBase view;
        public static readonly DependencyProperty GridMenuTypeProperty = DependencyPropertyManager.RegisterAttached("GridMenuType", typeof(GridMenuType?), typeof(DataControlPopupMenu), new FrameworkPropertyMetadata(null));
        protected internal static readonly RoutedEvent ManagerChangedEvent = EventManager.RegisterRoutedEvent("ManagerChanged", RoutingStrategy.Direct, typeof(RoutedPropertyChangedEventHandler<BarManager>), typeof(DataControlPopupMenu));

        public DataControlPopupMenu(DataViewBase view) : base(view.RootView)
        {
            this.view = view;
        }

        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget)
        {
            if (placementTarget == null)
            {
                throw new ArgumentException();
            }
            GridMenuType? gridMenuType = GetGridMenuType(placementTarget);
            return ((gridMenuType != null) ? this.CreateMenuInfoCore(gridMenuType) : null);
        }

        protected abstract MenuInfoBase CreateMenuInfoCore(GridMenuType? type);
        protected internal bool ExecuteOriginationViewMenuController(Func<DataViewBase, BarManagerMenuController> getMenuController)
        {
            DataViewBase originationView = this.View.OriginationView;
            if (originationView == null)
            {
                return false;
            }
            BarManagerMenuController controller = getMenuController(originationView);
            if (controller == null)
            {
                return false;
            }
            controller.Menu = this;
            try
            {
                controller.Execute(null);
            }
            finally
            {
                controller.Menu = originationView.DataControlMenu;
            }
            return true;
        }

        public static GridMenuType? GetGridMenuType(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (GridMenuType?) element.GetValue(GridMenuTypeProperty);
        }

        protected override bool RaiseShowMenu()
        {
            if (!this.RequestUIUpdate())
            {
                return false;
            }
            GridMenuEventArgs args1 = new GridMenuEventArgs(this);
            args1.RoutedEvent = DataViewBase.ShowGridMenuEvent;
            GridMenuEventArgs e = args1;
            this.View.RaiseShowGridMenu(e);
            return !e.Handled;
        }

        private bool RequestUIUpdate()
        {
            bool flag;
            this.View.LockEditorClose = true;
            try
            {
                flag = this.View.RequestUIUpdate();
            }
            finally
            {
                this.View.LockEditorClose = false;
            }
            return flag;
        }

        public static void SetGridMenuType(DependencyObject element, GridMenuType? value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(GridMenuTypeProperty, value);
        }

        protected override bool ShouldClearItemsOnClose =>
            true;

        public GridMenuInfo MenuInfo =>
            (GridMenuInfo) base.MenuInfo;

        public DataViewBase View =>
            this.view;

        public GridMenuType? MenuType
        {
            get
            {
                if (this.MenuInfo != null)
                {
                    return new GridMenuType?(this.MenuInfo.MenuType);
                }
                return null;
            }
        }
    }
}

