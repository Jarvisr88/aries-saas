namespace DevExpress.Xpf.Core
{
    using System;

    public interface ICustomDialogService
    {
        bool ShowDialog(string title, object viewModel);
    }
}

