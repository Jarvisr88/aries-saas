namespace DevExpress.Xpf.Office.Internal
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;

    public abstract class ToolTipController<T> : IDisposable where T: IToolTipControlClient
    {
        private static double DefaultTimerInterval;
        private static int DefaultPopupVerticalOffset;
        private static int DefaultPopupHorizontalOffset;
        private static PlacementMode DefaultPopupPlacement;
        private static int DefaultMaxTextLength;
        private object activeObject;
        private ToolTip popup;
        private DispatcherTimer timer;
        private bool isDisposed;
        private double popupAutoCloseDelay;
        private int popupHorizontalOffset;
        private int popupVerticalOffset;
        private PlacementMode popupPlacement;
        private int maxToolTipLength;
        private readonly UIElement owner;

        static ToolTipController()
        {
            ToolTipController<T>.DefaultTimerInterval = 10000.0;
            ToolTipController<T>.DefaultPopupVerticalOffset = -80;
            ToolTipController<T>.DefaultPopupHorizontalOffset = 0;
            ToolTipController<T>.DefaultPopupPlacement = PlacementMode.Mouse;
            ToolTipController<T>.DefaultMaxTextLength = 400;
        }

        protected ToolTipController(UIElement owner)
        {
            Guard.ArgumentNotNull(owner, "owner");
            this.owner = owner;
            this.timer = new DispatcherTimer();
            this.popup = this.CreatePopupControl();
            this.PopupAutoCloseDelay = ToolTipController<T>.DefaultTimerInterval;
            this.PopupHorizontalOffset = ToolTipController<T>.DefaultPopupHorizontalOffset;
            this.PopupVerticalOffset = ToolTipController<T>.DefaultPopupVerticalOffset;
            this.PopupPlacement = ToolTipController<T>.DefaultPopupPlacement;
            this.MaxToolTipLength = ToolTipController<T>.DefaultMaxTextLength;
            this.SubscribeToEvents();
        }

        private void ClosePopup()
        {
            if (this.popup.IsOpen)
            {
                this.popup.IsOpen = false;
                this.activeObject = null;
                this.timer.Stop();
            }
        }

        private ToolTip CreatePopupControl()
        {
            ToolTip element = new ToolTip {
                PlacementTarget = this.owner
            };
            ToolTipService.SetInitialShowDelay(element, ToolTipService.GetInitialShowDelay(this.owner));
            return element;
        }

        protected internal abstract SuperTipControl CreateSuperTipControl(ToolTipControlInfo info);
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.popup.IsOpen = false;
                this.UsubscribeToEvents();
            }
            this.isDisposed = true;
        }

        ~ToolTipController()
        {
            this.Dispose(false);
        }

        protected internal string GetActualText(string origin) => 
            (origin.Length <= this.MaxToolTipLength) ? origin : $"{origin.Substring(0, this.MaxToolTipLength)}...";

        private void OnOwnerMouseClick(object sender, MouseButtonEventArgs e)
        {
            this.ClosePopup();
        }

        private void OnOwnerMouseLeave(object sender, MouseEventArgs e)
        {
            this.ClosePopup();
        }

        private void OnOwnerMouseMove(object sender, MouseEventArgs e)
        {
            if (this.ToolTipClient.HasToolTip)
            {
                Point position = e.GetPosition(this.owner);
                ToolTipControlInfo objectInfo = this.ToolTipClient.GetObjectInfo(position);
                if (objectInfo == null)
                {
                    this.ClosePopup();
                }
                else if (objectInfo.Object != this.activeObject)
                {
                    this.ClosePopup();
                    this.OpenPopup(objectInfo);
                }
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.popup.Dispatcher.BeginInvoke(new Action(this.ClosePopup), new object[0]);
        }

        private void OpenPopup(ToolTipControlInfo info)
        {
            SuperTipControl control = this.CreateSuperTipControl(info);
            if (control != null)
            {
                this.popup.Content = control;
                this.popup.IsOpen = true;
                this.timer.Start();
                this.activeObject = info.Object;
            }
        }

        private void SubscribeToEvents()
        {
            this.owner.MouseMove += new MouseEventHandler(this.OnOwnerMouseMove);
            this.owner.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnOwnerMouseClick);
            this.owner.MouseRightButtonUp += new MouseButtonEventHandler(this.OnOwnerMouseClick);
            this.owner.MouseLeave += new MouseEventHandler(this.OnOwnerMouseLeave);
            this.timer.Tick += new EventHandler(this.OnTimerTick);
        }

        private void UsubscribeToEvents()
        {
            this.owner.MouseMove -= new MouseEventHandler(this.OnOwnerMouseMove);
            this.owner.MouseLeftButtonUp -= new MouseButtonEventHandler(this.OnOwnerMouseClick);
            this.owner.MouseRightButtonUp -= new MouseButtonEventHandler(this.OnOwnerMouseClick);
            this.owner.MouseLeave -= new MouseEventHandler(this.OnOwnerMouseLeave);
            this.timer.Tick -= new EventHandler(this.OnTimerTick);
        }

        protected UIElement Owner =>
            this.owner;

        public IToolTipControlClient ToolTipClient =>
            (IToolTipControlClient) this.owner;

        public object ActiveObject =>
            this.activeObject;

        public bool IsDisposed =>
            this.isDisposed;

        public int MaxToolTipLength
        {
            get => 
                this.maxToolTipLength;
            set => 
                this.maxToolTipLength = (value < 1) ? 1 : value;
        }

        public int PopupHorizontalOffset
        {
            get => 
                this.popupHorizontalOffset;
            set
            {
                if (this.popupHorizontalOffset != value)
                {
                    this.popupHorizontalOffset = value;
                    this.popup.HorizontalOffset = this.popupHorizontalOffset;
                }
            }
        }

        public int PopupVerticalOffset
        {
            get => 
                this.popupVerticalOffset;
            set
            {
                if (this.popupVerticalOffset != value)
                {
                    this.popupVerticalOffset = value;
                    this.popup.VerticalOffset = this.popupVerticalOffset;
                }
            }
        }

        public PlacementMode PopupPlacement
        {
            get => 
                this.popupPlacement;
            set
            {
                if (this.popupPlacement != value)
                {
                    this.popupPlacement = value;
                    this.popup.Placement = this.popupPlacement;
                }
            }
        }

        public double PopupAutoCloseDelay
        {
            get => 
                this.popupAutoCloseDelay;
            set
            {
                if (value != this.popupAutoCloseDelay)
                {
                    this.popupAutoCloseDelay = (value < 1.0) ? 1.0 : value;
                    this.timer.Interval = TimeSpan.FromMilliseconds(this.popupAutoCloseDelay);
                }
            }
        }
    }
}

