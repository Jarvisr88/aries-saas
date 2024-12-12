namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class DialogService : DevExpress.Xpf.Printing.IDialogService
    {
        private FrameworkElement view;

        public DialogService(FrameworkElement view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            this.view = view;
        }

        public Window GetParentWindow() => 
            LayoutHelper.FindParentObject<Window>(this.view);

        public void ShowError(string text, string caption)
        {
            DXMessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        public void ShowInformation(string text, string caption)
        {
            DXMessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        public Stream ShowOpenFileDialog(string caption, string filter)
        {
            string fileName = "";
            return this.ShowOpenFileDialog(caption, filter, out fileName);
        }

        public Stream ShowOpenFileDialog(string caption, string filter, out string fileName)
        {
            OpenFileDialogService service1 = new OpenFileDialogService();
            service1.Filter = filter;
            service1.Title = caption;
            IOpenFileDialogService service = service1;
            if (!service.ShowDialog())
            {
                fileName = null;
                return null;
            }
            fileName = service.GetFullFileName();
            return service.File.OpenRead();
        }

        public Stream ShowSaveFileDialog(string caption, string filter, int filterIndex, string initialDirectory, string fileName)
        {
            SaveFileDialogService service = new SaveFileDialogService {
                Filter = filter,
                FilterIndex = filterIndex,
                DefaultFileName = fileName,
                Title = caption,
                InitialDirectory = initialDirectory
            };
            return (service.ShowDialog(null, null) ? service.OpenFile() : null);
        }
    }
}

