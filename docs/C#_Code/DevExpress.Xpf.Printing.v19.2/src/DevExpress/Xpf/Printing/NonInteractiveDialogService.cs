namespace DevExpress.Xpf.Printing
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class NonInteractiveDialogService : IDialogService
    {
        public Window GetParentWindow() => 
            null;

        public void ShowError(string text, string caption)
        {
        }

        public void ShowInformation(string text, string caption)
        {
        }

        public Stream ShowOpenFileDialog(string caption, string filter) => 
            null;

        public Stream ShowOpenFileDialog(string caption, string filter, out string fileName)
        {
            fileName = null;
            return null;
        }

        public Stream ShowSaveFileDialog(string caption, string filter, int filterIndex, string initialDirectory, string fileName) => 
            null;
    }
}

