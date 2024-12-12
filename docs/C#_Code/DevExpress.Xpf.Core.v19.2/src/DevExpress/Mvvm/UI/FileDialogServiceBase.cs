namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.CommonDialogs;
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public abstract class FileDialogServiceBase : WindowAwareServiceBase, IFileDialogServiceBase
    {
        public static readonly DependencyProperty CheckFileExistsProperty = DependencyProperty.Register("CheckFileExists", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(false));
        public static readonly DependencyProperty AddExtensionProperty = DependencyProperty.Register("AddExtension", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(true));
        public static readonly DependencyProperty AutoUpgradeEnabledProperty = DependencyProperty.Register("AutoUpgradeEnabled", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(true));
        public static readonly DependencyProperty CheckPathExistsProperty = DependencyProperty.Register("CheckPathExists", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(true));
        public static readonly DependencyProperty DereferenceLinksProperty = DependencyProperty.Register("DereferenceLinks", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(true));
        public static readonly DependencyProperty InitialDirectoryProperty = DependencyProperty.Register("InitialDirectory", typeof(string), typeof(FileDialogServiceBase), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DefaultFileNameProperty = DependencyProperty.Register("DefaultFileName", typeof(string), typeof(FileDialogServiceBase), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RestoreDirectoryProperty = DependencyProperty.Register("RestoreDirectory", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowHelpProperty = DependencyProperty.Register("ShowHelp", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(false));
        public static readonly DependencyProperty SupportMultiDottedExtensionsProperty = DependencyProperty.Register("SupportMultiDottedExtensions", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(false));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(FileDialogServiceBase), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ValidateNamesProperty = DependencyProperty.Register("ValidateNames", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(true));
        public static readonly DependencyProperty RestorePreviouslySelectedDirectoryProperty = DependencyProperty.Register("RestorePreviouslySelectedDirectory", typeof(bool), typeof(FileDialogServiceBase), new PropertyMetadata(true));
        public static readonly DependencyProperty HelpRequestCommandProperty = DependencyProperty.Register("HelpRequestCommand", typeof(ICommand), typeof(FileDialogServiceBase), new PropertyMetadata(null));
        public static readonly DependencyProperty FileOkCommandProperty = DependencyProperty.Register("FileOkCommand", typeof(ICommand), typeof(FileDialogServiceBase), new PropertyMetadata(null));
        private DevExpress.Utils.CommonDialogs.IFileDialog FileDialog;
        private IEnumerable<object> FilesCore;
        private Action<CancelEventArgs> fileOK;

        public event CancelEventHandler FileOk;

        public event EventHandler HelpRequest;

        public FileDialogServiceBase()
        {
            this.FileDialog = this.CreateFileDialogAdapter();
            this.FilesCore = new List<object>();
            this.FileDialog.FileOk += new CancelEventHandler(this.OnDialogFileOk);
            this.FileDialog.HelpRequest += new EventHandler(this.OnDialogHelpRequest);
        }

        private bool ConvertDialogResultToBoolean(System.Windows.Forms.DialogResult result)
        {
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }
            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                throw new InvalidOperationException("The Dialog has returned a not supported value");
            }
            return false;
        }

        protected abstract DevExpress.Utils.CommonDialogs.IFileDialog CreateFileDialogAdapter();
        void IFileDialogServiceBase.Reset()
        {
            this.FileDialog.Reset();
        }

        protected object GetFileDialog() => 
            this.FileDialog;

        protected abstract List<object> GetFileInfos();
        protected IEnumerable<object> GetFiles() => 
            this.FilesCore;

        private string GetPreviouslySelectedDirectory() => 
            !(this.FilesCore.First<object>() is IFileInfo) ? (!(this.FilesCore.First<object>() is IFolderInfo) ? null : ((IFolderInfo) this.FilesCore.First<object>()).DirectoryName) : ((IFileInfo) this.FilesCore.First<object>()).DirectoryName;

        protected abstract void InitFileDialog();
        private void InitFileDialogCore()
        {
            this.FileDialog.CheckFileExists = this.CheckFileExists;
            this.FileDialog.AddExtension = this.AddExtension;
            this.FileDialog.CheckPathExists = this.CheckPathExists;
            this.FileDialog.DereferenceLinks = this.DereferenceLinks;
            this.FileDialog.InitialDirectory = this.InitialDirectory;
            this.FileDialog.FileName = this.DefaultFileName;
            this.FileDialog.RestoreDirectory = this.RestoreDirectory;
            this.FileDialog.ShowHelp = this.ShowHelp;
            this.FileDialog.SupportMultiDottedExtensions = this.SupportMultiDottedExtensions;
            this.FileDialog.Title = this.Title;
            this.FileDialog.ValidateNames = this.ValidateNames;
            if (this.RestorePreviouslySelectedDirectory && (this.FilesCore.Count<object>() > 0))
            {
                this.FileDialog.InitialDirectory = this.GetPreviouslySelectedDirectory();
            }
            else
            {
                this.FileDialog.InitialDirectory = this.InitialDirectory;
            }
        }

        protected override void OnActualWindowChanged(Window oldWindow)
        {
        }

        private void OnDialogFileOk(object sender, CancelEventArgs e)
        {
            this.UpdateFiles();
            this.FileOk.Do<CancelEventHandler>(x => x(sender, e));
            this.FileOkCommand.If<ICommand>(x => x.CanExecute(e)).Do<ICommand>(x => x.Execute(e));
            this.fileOK.Do<Action<CancelEventArgs>>(x => x(e));
        }

        private void OnDialogHelpRequest(object sender, EventArgs e)
        {
            this.HelpRequest.Do<EventHandler>(x => x(sender, e));
            this.HelpRequestCommand.If<ICommand>(x => x.CanExecute(e)).Do<ICommand>(x => x.Execute(e));
        }

        protected bool Show(Action<CancelEventArgs> fileOK)
        {
            this.fileOK = fileOK;
            this.InitFileDialogCore();
            this.InitFileDialog();
            ((IList) this.FilesCore).Clear();
            return this.ShowCore();
        }

        private bool ShowCore()
        {
            DevExpress.Utils.CommonDialogs.Internal.DialogResult result = (base.ActualWindow == null) ? this.FileDialog.ShowDialog() : this.FileDialog.ShowDialog(new Win32WindowWrapper(base.ActualWindow));
            if (result == DevExpress.Utils.CommonDialogs.Internal.DialogResult.OK)
            {
                return true;
            }
            if (result != DevExpress.Utils.CommonDialogs.Internal.DialogResult.Cancel)
            {
                throw new InvalidOperationException("The Dialog has returned a not supported value");
            }
            return false;
        }

        private void UpdateFiles()
        {
            List<object> fileInfos = this.GetFileInfos();
            IList filesCore = (IList) this.FilesCore;
            filesCore.Clear();
            foreach (object obj2 in fileInfos)
            {
                filesCore.Add(obj2);
            }
        }

        public bool CheckFileExists
        {
            get => 
                (bool) base.GetValue(CheckFileExistsProperty);
            set => 
                base.SetValue(CheckFileExistsProperty, value);
        }

        public bool AddExtension
        {
            get => 
                (bool) base.GetValue(AddExtensionProperty);
            set => 
                base.SetValue(AddExtensionProperty, value);
        }

        public bool AutoUpgradeEnabled
        {
            get => 
                (bool) base.GetValue(AutoUpgradeEnabledProperty);
            set => 
                base.SetValue(AutoUpgradeEnabledProperty, value);
        }

        public bool CheckPathExists
        {
            get => 
                (bool) base.GetValue(CheckPathExistsProperty);
            set => 
                base.SetValue(CheckPathExistsProperty, value);
        }

        public bool DereferenceLinks
        {
            get => 
                (bool) base.GetValue(DereferenceLinksProperty);
            set => 
                base.SetValue(DereferenceLinksProperty, value);
        }

        public string InitialDirectory
        {
            get => 
                (string) base.GetValue(InitialDirectoryProperty);
            set => 
                base.SetValue(InitialDirectoryProperty, value);
        }

        public string DefaultFileName
        {
            get => 
                (string) base.GetValue(DefaultFileNameProperty);
            set => 
                base.SetValue(DefaultFileNameProperty, value);
        }

        public bool RestoreDirectory
        {
            get => 
                (bool) base.GetValue(RestoreDirectoryProperty);
            set => 
                base.SetValue(RestoreDirectoryProperty, value);
        }

        public bool ShowHelp
        {
            get => 
                (bool) base.GetValue(ShowHelpProperty);
            set => 
                base.SetValue(ShowHelpProperty, value);
        }

        public bool SupportMultiDottedExtensions
        {
            get => 
                (bool) base.GetValue(SupportMultiDottedExtensionsProperty);
            set => 
                base.SetValue(SupportMultiDottedExtensionsProperty, value);
        }

        public string Title
        {
            get => 
                (string) base.GetValue(TitleProperty);
            set => 
                base.SetValue(TitleProperty, value);
        }

        public bool ValidateNames
        {
            get => 
                (bool) base.GetValue(ValidateNamesProperty);
            set => 
                base.SetValue(ValidateNamesProperty, value);
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

        public ICommand FileOkCommand
        {
            get => 
                (ICommand) base.GetValue(FileOkCommandProperty);
            set => 
                base.SetValue(FileOkCommandProperty, value);
        }

        protected abstract class FileDialogAdapter<TFileDialog> : DevExpress.Utils.CommonDialogs.IFileDialog, ICommonDialog, IDisposable where TFileDialog: FileDialog
        {
            protected readonly TFileDialog fileDialog;
            [CompilerGenerated]
            private CancelEventHandler FileOk;
            [CompilerGenerated]
            private EventHandler HelpRequest;
            private readonly FileDialogCustomPlacesCollectionWrapper customPlacesCore;

            public event CancelEventHandler FileOk
            {
                [CompilerGenerated] add
                {
                    CancelEventHandler fileOk = this.FileOk;
                    while (true)
                    {
                        CancelEventHandler comparand = fileOk;
                        CancelEventHandler handler3 = comparand + value;
                        fileOk = Interlocked.CompareExchange<CancelEventHandler>(ref this.FileOk, handler3, comparand);
                        if (ReferenceEquals(fileOk, comparand))
                        {
                            return;
                        }
                    }
                }
                [CompilerGenerated] remove
                {
                    CancelEventHandler fileOk = this.FileOk;
                    while (true)
                    {
                        CancelEventHandler comparand = fileOk;
                        CancelEventHandler handler3 = comparand - value;
                        fileOk = Interlocked.CompareExchange<CancelEventHandler>(ref this.FileOk, handler3, comparand);
                        if (ReferenceEquals(fileOk, comparand))
                        {
                            return;
                        }
                    }
                }
            }

            public event EventHandler HelpRequest
            {
                [CompilerGenerated] add
                {
                    EventHandler helpRequest = this.HelpRequest;
                    while (true)
                    {
                        EventHandler comparand = helpRequest;
                        EventHandler handler3 = comparand + value;
                        helpRequest = Interlocked.CompareExchange<EventHandler>(ref this.HelpRequest, handler3, comparand);
                        if (ReferenceEquals(helpRequest, comparand))
                        {
                            return;
                        }
                    }
                }
                [CompilerGenerated] remove
                {
                    EventHandler helpRequest = this.HelpRequest;
                    while (true)
                    {
                        EventHandler comparand = helpRequest;
                        EventHandler handler3 = comparand - value;
                        helpRequest = Interlocked.CompareExchange<EventHandler>(ref this.HelpRequest, handler3, comparand);
                        if (ReferenceEquals(helpRequest, comparand))
                        {
                            return;
                        }
                    }
                }
            }

            public FileDialogAdapter(TFileDialog fileDialog)
            {
                this.fileDialog = fileDialog;
                this.fileDialog.FileOk += new CancelEventHandler(this.OnDialogFileOk);
                this.fileDialog.HelpRequest += new EventHandler(this.OnDialogHelpRequest);
                this.customPlacesCore = new FileDialogCustomPlacesCollectionWrapper(this.fileDialog.CustomPlaces);
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
                FileDialogServiceBase.FileDialogAdapter<TFileDialog>.Convert(this.ShowDialog());

            DevExpress.Utils.CommonDialogs.Internal.DialogResult ICommonDialog.ShowDialog(object ownerWindow)
            {
                Window window = ownerWindow as Window;
                return FileDialogServiceBase.FileDialogAdapter<TFileDialog>.Convert((window == null) ? this.ShowDialog() : this.ShowDialog(window));
            }

            private void OnDialogFileOk(object sender, CancelEventArgs e)
            {
                if (this.FileOk != null)
                {
                    this.FileOk(sender, e);
                }
            }

            private void OnDialogHelpRequest(object sender, EventArgs e)
            {
                if (this.HelpRequest != null)
                {
                    this.HelpRequest(sender, e);
                }
            }

            public System.Windows.Forms.DialogResult ShowDialog() => 
                this.fileDialog.ShowDialog();

            public System.Windows.Forms.DialogResult ShowDialog(Window ownerWindow) => 
                this.fileDialog.ShowDialog(new Win32WindowWrapper(ownerWindow));

            void IDisposable.Dispose()
            {
                this.fileDialog.Dispose();
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.CheckPathExists
            {
                get => 
                    this.fileDialog.CheckPathExists;
                set => 
                    this.fileDialog.CheckPathExists = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.CheckFileExists
            {
                get => 
                    this.fileDialog.CheckFileExists;
                set => 
                    this.fileDialog.CheckFileExists = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.AddExtension
            {
                get => 
                    this.fileDialog.AddExtension;
                set => 
                    this.fileDialog.AddExtension = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.DereferenceLinks
            {
                get => 
                    this.fileDialog.DereferenceLinks;
                set => 
                    this.fileDialog.DereferenceLinks = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.RestoreDirectory
            {
                get => 
                    this.fileDialog.RestoreDirectory;
                set => 
                    this.fileDialog.RestoreDirectory = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.ShowHelp
            {
                get => 
                    this.fileDialog.ShowHelp;
                set => 
                    this.fileDialog.ShowHelp = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.SupportMultiDottedExtensions
            {
                get => 
                    this.fileDialog.SupportMultiDottedExtensions;
                set => 
                    this.fileDialog.SupportMultiDottedExtensions = value;
            }

            bool DevExpress.Utils.CommonDialogs.IFileDialog.ValidateNames
            {
                get => 
                    this.fileDialog.ValidateNames;
                set => 
                    this.fileDialog.ValidateNames = value;
            }

            string DevExpress.Utils.CommonDialogs.IFileDialog.InitialDirectory
            {
                get => 
                    this.fileDialog.InitialDirectory;
                set => 
                    this.fileDialog.InitialDirectory = value;
            }

            string DevExpress.Utils.CommonDialogs.IFileDialog.Title
            {
                get => 
                    this.fileDialog.Title;
                set => 
                    this.fileDialog.Title = value;
            }

            string[] DevExpress.Utils.CommonDialogs.IFileDialog.FileNames =>
                this.fileDialog.FileNames;

            string DevExpress.Utils.CommonDialogs.IFileDialog.Filter
            {
                get => 
                    this.fileDialog.Filter;
                set => 
                    this.fileDialog.Filter = value;
            }

            int DevExpress.Utils.CommonDialogs.IFileDialog.FilterIndex
            {
                get => 
                    this.fileDialog.FilterIndex;
                set => 
                    this.fileDialog.FilterIndex = value;
            }

            string DevExpress.Utils.CommonDialogs.IFileDialog.FileName
            {
                get => 
                    this.fileDialog.FileName;
                set => 
                    this.fileDialog.FileName = value;
            }

            string DevExpress.Utils.CommonDialogs.IFileDialog.DefaultExt
            {
                get => 
                    this.fileDialog.DefaultExt;
                set => 
                    this.fileDialog.DefaultExt = value;
            }

            DevExpress.Utils.CommonDialogs.Internal.FileDialogCustomPlacesCollection DevExpress.Utils.CommonDialogs.IFileDialog.CustomPlaces =>
                this.customPlacesCore;
        }
    }
}

