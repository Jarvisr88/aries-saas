namespace DevExpress.Mvvm.UI
{
    using System;

    public interface IEventArgsConverter
    {
        object Convert(object sender, object args);
    }
}

