namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.CommonDialogs;
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;

    [Browsable(true), TargetType(typeof(Window)), EditorBrowsable(EditorBrowsableState.Always), TargetType(typeof(System.Windows.Controls.UserControl))]
    public class FolderBrowserDialogService : ServiceBase, IFolderBrowserDialogService
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(FolderBrowserDialogService), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RootFolderProperty = DependencyProperty.Register("RootFolder", typeof(Environment.SpecialFolder), typeof(FolderBrowserDialogService), new PropertyMetadata(Environment.SpecialFolder.Desktop));
        public static readonly DependencyProperty ShowNewFolderButtonProperty = DependencyProperty.Register("ShowNewFolderButton", typeof(bool), typeof(FolderBrowserDialogService), new PropertyMetadata(true));
        public static readonly DependencyProperty StartPathProperty = DependencyProperty.Register("StartPath", typeof(string), typeof(FolderBrowserDialogService), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RestorePreviouslySelectedDirectoryProperty = DependencyProperty.Register("RestorePreviouslySelectedDirectory", typeof(bool), typeof(FolderBrowserDialogService), new PropertyMetadata(true));
        public static readonly DependencyProperty HelpRequestCommandProperty = DependencyProperty.Register("HelpRequestCommand", typeof(ICommand), typeof(FolderBrowserDialogService), new PropertyMetadata(null));
        private IFolderBrowserDialog Dialog;
        private string resultPath = string.Empty;

        public event EventHandler HelpRequest
        {
            add
            {
                this.Dialog.HelpRequest += value;
            }
            remove
            {
                this.Dialog.HelpRequest -= value;
            }
        }

        public FolderBrowserDialogService()
        {
            this.Dialog = this.CreateFolderBrowserDialog();
            this.HelpRequest += delegate (object d, EventArgs e) {
                if ((this.HelpRequestCommand != null) && this.HelpRequestCommand.CanExecute(e))
                {
                    this.HelpRequestCommand.Execute(e);
                }
            };
        }

        protected virtual IFolderBrowserDialog CreateFolderBrowserDialog() => 
            new FolderBrowserDialogAdapter();

        bool IFolderBrowserDialogService.ShowDialog()
        {
            this.Dialog.Description = this.Description;
            this.Dialog.RootFolder = this.RootFolder;
            this.Dialog.ShowNewFolderButton = this.ShowNewFolderButton;
            this.Dialog.SelectedPath = (!this.RestorePreviouslySelectedDirectory || string.IsNullOrEmpty(this.resultPath)) ? this.StartPath : this.resultPath;
            DevExpress.Utils.CommonDialogs.Internal.DialogResult result = this.Dialog.ShowDialog();
            this.resultPath = this.Dialog.SelectedPath;
            if (result == DevExpress.Utils.CommonDialogs.Internal.DialogResult.OK)
            {
                return true;
            }
            if (result != DevExpress.Utils.CommonDialogs.Internal.DialogResult.Cancel)
            {
                throw new InvalidOperationException();
            }
            return false;
        }

        protected object GetFileDialog() => 
            this.Dialog;

        public string Description
        {
            get => 
                (string) base.GetValue(DescriptionProperty);
            set => 
                base.SetValue(DescriptionProperty, value);
        }

        public Environment.SpecialFolder RootFolder
        {
            get => 
                (Environment.SpecialFolder) base.GetValue(RootFolderProperty);
            set => 
                base.SetValue(RootFolderProperty, value);
        }

        public bool ShowNewFolderButton
        {
            get => 
                (bool) base.GetValue(ShowNewFolderButtonProperty);
            set => 
                base.SetValue(ShowNewFolderButtonProperty, value);
        }

        public string StartPath
        {
            get => 
                (string) base.GetValue(StartPathProperty);
            set => 
                base.SetValue(StartPathProperty, value);
        }

        public bool RestorePreviouslySelectedDirectory
        {
            get => 
                (bool) base.GetValue(RestorePreviouslySelectedDirectoryProperty);
            set => 
                base.SetValue(RestorePreviouslySelectedDirectoryProperty, value);
        }

        public ICommand HelpRequestCommand
        {
            get => 
                (ICommand) base.GetValue(HelpRequestCommandProperty);
            set => 
                base.SetValue(HelpRequestCommandProperty, value);
        }

        string IFolderBrowserDialogService.ResultPath =>
            this.resultPath;

        protected class FolderBrowserDialogAdapter : IFolderBrowserDialog, ICommonDialog, IDisposable
        {
            private readonly FolderBrowserDialog fileDialog = new FolderBrowserDialog();

            event EventHandler ICommonDialog.HelpRequest
            {
                add
                {
                    this.fileDialog.HelpRequest += value;
                }
                remove
                {
                    this.fileDialog.HelpRequest -= value;
                }
            }

            private static DevExpress.Utils.CommonDialogs.Internal.DialogResult Convert(System.Windows.Forms.DialogResult result)
            {
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.OK:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.OK;

                    case System.Windows.Forms.DialogResult.Cancel:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.Cancel;

                    case System.Windows.Forms.DialogResult.Abort:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.Abort;

                    case System.Windows.Forms.DialogResult.Retry:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.Retry;

                    case System.Windows.Forms.DialogResult.Ignore:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.Ignore;

                    case System.Windows.Forms.DialogResult.Yes:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.Yes;

                    case System.Windows.Forms.DialogResult.No:
                        return DevExpress.Utils.CommonDialogs.Internal.DialogResult.No;
                }
                return DevExpress.Utils.CommonDialogs.Internal.DialogResult.None;
            }

            void ICommonDialog.Reset()
            {
                this.fileDialog.Reset();
            }

            DevExpress.Utils.CommonDialogs.Internal.DialogResult ICommonDialog.ShowDialog() => 
                Convert(this.fileDialog.ShowDialog());

            DevExpress.Utils.CommonDialogs.Internal.DialogResult ICommonDialog.ShowDialog(object ownerWindow)
            {
                Window window = ownerWindow as Window;
                return Convert((window == null) ? this.fileDialog.ShowDialog() : this.fileDialog.ShowDialog(new Win32WindowWrapper(window)));
            }

            void IDisposable.Dispose()
            {
                this.fileDialog.Dispose();
            }

            Environment.SpecialFolder IFolderBrowserDialog.RootFolder
            {
                get => 
                    this.fileDialog.RootFolder;
                set => 
                    this.fileDialog.RootFolder = value;
            }

            bool IFolderBrowserDialog.ShowNewFolderButton
            {
                get => 
                    this.fileDialog.ShowNewFolderButton;
                set => 
                    this.fileDialog.ShowNewFolderButton = value;
            }

            string IFolderBrowserDialog.SelectedPath
            {
                get => 
                    this.fileDialog.SelectedPath;
                set => 
                    this.fileDialog.SelectedPath = value;
            }

            string IFolderBrowserDialog.Description
            {
                get => 
                    this.fileDialog.Description;
                set => 
                    this.fileDialog.Description = value;
            }
        }
    }
}

