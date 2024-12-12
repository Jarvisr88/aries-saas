namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public interface ICustomUIFilterDialogService
    {
        Task<bool> Show(object uiProvider, ICustomUIFilterDialogViewModel viewModel, out IDisposable cancelationToken);
    }
}

