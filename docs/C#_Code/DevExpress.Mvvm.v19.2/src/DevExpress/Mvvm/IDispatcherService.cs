namespace DevExpress.Mvvm
{
    using System;
    using System.Threading.Tasks;

    public interface IDispatcherService
    {
        Task BeginInvoke(Action action);
        void Invoke(Action action);
    }
}

