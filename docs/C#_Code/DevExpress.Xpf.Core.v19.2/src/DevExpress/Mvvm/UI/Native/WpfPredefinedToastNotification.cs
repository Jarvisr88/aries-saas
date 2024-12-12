namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal class WpfPredefinedToastNotification : IPredefinedToastNotification
    {
        private WpfPredefinedToastNotificationContent content;
        private CustomNotifier notifier;
        private CustomNotification toast;

        public WpfPredefinedToastNotification(WpfPredefinedToastNotificationContent content, CustomNotifier notifier)
        {
            this.toast = new CustomNotification(content.ViewModel, notifier);
            this.content = content;
            this.notifier = notifier;
            this.notifier.ContentTemplate = NotificationServiceTemplatesHelper.PredefinedToastTemplate;
        }

        public void Hide()
        {
            this.notifier.Hide(this.toast);
        }

        public Task<ToastNotificationResultInternal> ShowAsync()
        {
            Func<Task<NotificationResult>, ToastNotificationResultInternal> continuationFunction = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<Task<NotificationResult>, ToastNotificationResultInternal> local1 = <>c.<>9__5_0;
                continuationFunction = <>c.<>9__5_0 = (Func<Task<NotificationResult>, ToastNotificationResultInternal>) (t => t.Result);
            }
            return this.notifier.ShowAsync(this.toast, this.content.Duration).ContinueWith<ToastNotificationResultInternal>(continuationFunction);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WpfPredefinedToastNotification.<>c <>9 = new WpfPredefinedToastNotification.<>c();
            public static Func<Task<NotificationResult>, ToastNotificationResultInternal> <>9__5_0;

            internal ToastNotificationResultInternal <ShowAsync>b__5_0(Task<NotificationResult> t) => 
                t.Result;
        }
    }
}

