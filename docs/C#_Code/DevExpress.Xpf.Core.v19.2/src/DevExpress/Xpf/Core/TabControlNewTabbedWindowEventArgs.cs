namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabControlNewTabbedWindowEventArgs : EventArgs
    {
        private Window newWindow;
        private DXTabControl newTabControl;

        public TabControlNewTabbedWindowEventArgs(Window sourceWindow, DXTabControl sourceTabControl, object sourceData)
        {
            this.SourceWindow = sourceWindow;
            this.SourceTabControl = sourceTabControl;
            this.SourceData = sourceData;
            this.NewWindow = this.newWindow;
            this.NewTabControl = this.newTabControl;
        }

        public Point CalcNewWindowPosition()
        {
            Point point = new Point();
            Point mousePositionOnScreen = DragControllerHelper.GetMousePositionOnScreen();
            point.X = (mousePositionOnScreen.X / ScreenHelper.ScaleX) - (50.0 * ScreenHelper.ScaleX);
            point.Y = (mousePositionOnScreen.Y / ScreenHelper.ScaleX) - (20.0 * ScreenHelper.ScaleX);
            return point;
        }

        private DXTabControl CreateNewTabControl()
        {
            DXTabControl control = ((ICloneable) this.SourceTabControl).Clone() as DXTabControl;
            if ((this.SourceData != null) && !(this.SourceData is DXTabItem))
            {
                control.ItemsSource = new ObservableCollection<object>();
            }
            control.Style = this.SourceTabControl.View.StretchView.NewTabControlStyle;
            return control;
        }

        private Window CreateNewWindow()
        {
            Window window = (this.SourceWindow != null) ? DXTabbedWindow.CloneCore(this.SourceWindow) : new DXTabbedWindow();
            window.Content = null;
            Point point = this.CalcNewWindowPosition();
            window.Left = point.X;
            window.Top = point.Y;
            window.DataContext = this.SourceTabControl.DataContext;
            window.Style = this.SourceTabControl.View.StretchView.NewWindowStyle;
            window.Content = this.NewTabControl;
            return window;
        }

        public Window SourceWindow { get; private set; }

        public DXTabControl SourceTabControl { get; private set; }

        public object SourceData { get; private set; }

        public Window NewWindow
        {
            get
            {
                Window newWindow = this.newWindow;
                if (this.newWindow == null)
                {
                    Window local1 = this.newWindow;
                    newWindow = this.newWindow = this.CreateNewWindow();
                }
                return newWindow;
            }
            set
            {
                if (this.newWindow != null)
                {
                    this.newWindow.Content = null;
                }
                this.newWindow = value;
            }
        }

        public DXTabControl NewTabControl
        {
            get
            {
                DXTabControl newTabControl = this.newTabControl;
                if (this.newTabControl == null)
                {
                    DXTabControl local1 = this.newTabControl;
                    newTabControl = this.newTabControl = this.CreateNewTabControl();
                }
                return newTabControl;
            }
            set => 
                this.newTabControl = value;
        }
    }
}

