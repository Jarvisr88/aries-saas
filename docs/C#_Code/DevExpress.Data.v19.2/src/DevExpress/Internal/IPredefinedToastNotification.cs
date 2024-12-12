namespace DevExpress.Internal
{
    using System;
    using System.Threading.Tasks;

    public interface IPredefinedToastNotification
    {
        void Hide();
        Task<ToastNotificationResultInternal> ShowAsync();
    }
}

