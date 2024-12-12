namespace DevExpress.Xpf.Printing
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface IDialogService
    {
        Window GetParentWindow();
        void ShowError(string text, string caption);
        void ShowInformation(string text, string caption);
        Stream ShowOpenFileDialog(string caption, string filter);
        Stream ShowOpenFileDialog(string caption, string filter, out string fileName);
        Stream ShowSaveFileDialog(string caption, string filter, int filterIndex, string initialDirectory, string fileName);
    }
}

