namespace DevExpress.Mvvm
{
    using System;
    using System.Threading.Tasks;

    public interface INotification
    {
        void Hide();
        Task<NotificationResult> ShowAsync();
    }
}

