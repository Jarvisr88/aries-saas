namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Threading.Tasks;

    public interface ICustomUIFilterDialogDispatcherService
    {
        Task Queue(Action action);
    }
}

