namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public interface INotificationService
    {
        INotification CreateCustomNotification(object viewModel);
        INotification CreatePredefinedNotification(string text1, string text2, string text3, ImageSource image = null);
    }
}

