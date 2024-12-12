namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;

    public class CustomNotifier
    {
        private readonly List<ToastInfo> toastsQueue = new List<ToastInfo>();
        private const int maxVisibleToasts = 3;
        internal static NotificationPositioner<ToastInfo> positioner;
        private IScreen screen;
        private Point currentScreenPosition;

        public CustomNotifier(IScreen screen = null)
        {
            IScreen screen1 = screen;
            if (screen == null)
            {
                IScreen local1 = screen;
                screen1 = new PrimaryScreen();
            }
            this.screen = screen1;
            this.screen.WorkingAreaChanged += new Action(this.screen_WorkingAreaChanged);
            this.UpdatePositioner(NotificationPosition.TopRight, 3);
        }

        internal void Activate(CustomNotification toast)
        {
            this.RemoveVisibleToast(toast, NotificationResult.Activated);
            this.ShowNext();
        }

        public void ChangeScreen(Point position)
        {
            if (!this.VisibleItems.Any<ToastInfo>() && (this.currentScreenPosition != position))
            {
                this.currentScreenPosition = position;
                this.UpdatePositioner(positioner.position, positioner.maxCount);
            }
        }

        internal void Dismiss(CustomNotification toast)
        {
            this.RemoveVisibleToast(toast, NotificationResult.UserCanceled);
            this.ShowNext();
        }

        private ToastInfo GetVisibleToastInfo(CustomNotification toast) => 
            positioner.Items.FirstOrDefault<ToastInfo>(t => (t != null) && ReferenceEquals(t.toast, toast));

        internal void Hide(CustomNotification toast)
        {
            ToastInfo item = this.toastsQueue.FirstOrDefault<ToastInfo>(i => ReferenceEquals(i.toast, toast));
            if (item != null)
            {
                this.toastsQueue.Remove(item);
                item.source.SetResult(NotificationResult.ApplicationHidden);
            }
            else
            {
                this.RemoveVisibleToast(toast, NotificationResult.ApplicationHidden);
                this.ShowNext();
            }
        }

        private void RemoveVisibleToast(CustomNotification toast, NotificationResult result)
        {
            ToastInfo visibleToastInfo = this.GetVisibleToastInfo(toast);
            if (visibleToastInfo != null)
            {
                visibleToastInfo.win.Close();
                visibleToastInfo.timer.Stop();
                positioner.Remove(visibleToastInfo);
                visibleToastInfo.source.SetResult(result);
            }
        }

        internal void ResetTimer(CustomNotification toast)
        {
            Action<ToastInfo> action = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Action<ToastInfo> local1 = <>c.<>9__25_0;
                action = <>c.<>9__25_0 = t => t.timer.Start();
            }
            this.GetVisibleToastInfo(toast).Do<ToastInfo>(action);
        }

        private void screen_WorkingAreaChanged()
        {
            positioner.Update(this.screen.GetWorkingArea(this.currentScreenPosition));
            foreach (ToastInfo info in this.VisibleItems)
            {
                info.timer.Stop();
                info.timer.Start();
                Point itemPosition = positioner.GetItemPosition(info);
                info.win.Left = itemPosition.X;
                info.win.Top = itemPosition.Y;
            }
        }

        public Task<NotificationResult> ShowAsync(CustomNotification toast, int msDuration = 0xbb8)
        {
            ToastInfo info1 = new ToastInfo();
            info1.toast = toast;
            Timer timer1 = new Timer();
            timer1.Interval = msDuration;
            info1.timer = timer1;
            info1.source = new TaskCompletionSource<NotificationResult>();
            ToastInfo item = info1;
            this.toastsQueue.Add(item);
            this.ShowNext();
            return item.source.Task;
        }

        private void ShowNext()
        {
            ToastContentControl content;
            ToastInfo info;
            if (positioner.HasEmptySlot() && this.toastsQueue.Any<ToastInfo>())
            {
                info = this.toastsQueue[0];
                this.toastsQueue.RemoveAt(0);
                ToastContentControl control1 = new ToastContentControl();
                control1.Toast = info.toast;
                content = control1;
                content.Content = info.toast.ViewModel;
                content.Style = this.Style;
                if (this.ContentTemplateSelector == null)
                {
                    DataTemplate contentTemplate = this.ContentTemplate;
                    DataTemplate defaultCustomToastTemplate = contentTemplate;
                    if (contentTemplate == null)
                    {
                        DataTemplate local1 = contentTemplate;
                        defaultCustomToastTemplate = NotificationServiceTemplatesHelper.DefaultCustomToastTemplate;
                    }
                    content.ContentTemplate = defaultCustomToastTemplate;
                }
                content.ContentTemplateSelector = this.ContentTemplateSelector;
                try
                {
                    info.win = new ToastWindow();
                }
                catch
                {
                    content.TimeOutCommand.Execute(null);
                    return;
                }
                info.win.Content = content;
                info.win.DataContext = info.toast.ViewModel;
                if (double.IsNaN(content.Width) || double.IsNaN(content.Height))
                {
                    throw new InvalidOperationException("The height or width of a custom notification can not be set to Auto");
                }
                Point point = positioner.Add(info, content.Width, content.Height);
                info.win.Left = point.X;
                info.win.Top = point.Y;
                try
                {
                    info.win.Show();
                }
                catch (Win32Exception)
                {
                    content.TimeOutCommand.Execute(null);
                    return;
                }
                info.timer.Tick += delegate (object s, EventArgs e) {
                    content.TimeOutCommand.Execute(null);
                    info.timer.Stop();
                };
                info.timer.Start();
            }
        }

        internal void StopTimer(CustomNotification toast)
        {
            Action<ToastInfo> action = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Action<ToastInfo> local1 = <>c.<>9__24_0;
                action = <>c.<>9__24_0 = t => t.timer.Stop();
            }
            this.GetVisibleToastInfo(toast).Do<ToastInfo>(action);
        }

        internal void TimeOut(CustomNotification toast)
        {
            this.RemoveVisibleToast(toast, NotificationResult.TimedOut);
            this.ShowNext();
        }

        public void UpdatePositioner(NotificationPosition position, int maxCount)
        {
            positioner ??= new NotificationPositioner<ToastInfo>();
            positioner.Update(this.screen.GetWorkingArea(this.currentScreenPosition), position, maxCount);
        }

        public System.Windows.Style Style { get; set; }

        private List<ToastInfo> VisibleItems
        {
            get
            {
                Func<ToastInfo, bool> predicate = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<ToastInfo, bool> local1 = <>c.<>9__12_0;
                    predicate = <>c.<>9__12_0 = i => i != null;
                }
                return positioner.Items.Where<ToastInfo>(predicate).ToList<ToastInfo>();
            }
        }

        public DataTemplate ContentTemplate { get; set; }

        public DataTemplateSelector ContentTemplateSelector { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomNotifier.<>c <>9 = new CustomNotifier.<>c();
            public static Func<CustomNotifier.ToastInfo, bool> <>9__12_0;
            public static Action<CustomNotifier.ToastInfo> <>9__24_0;
            public static Action<CustomNotifier.ToastInfo> <>9__25_0;

            internal bool <get_VisibleItems>b__12_0(CustomNotifier.ToastInfo i) => 
                i != null;

            internal void <ResetTimer>b__25_0(CustomNotifier.ToastInfo t)
            {
                t.timer.Start();
            }

            internal void <StopTimer>b__24_0(CustomNotifier.ToastInfo t)
            {
                t.timer.Stop();
            }
        }

        internal class ToastInfo
        {
            public Window win;
            public CustomNotification toast;
            public Timer timer;
            public TaskCompletionSource<NotificationResult> source;
        }
    }
}

