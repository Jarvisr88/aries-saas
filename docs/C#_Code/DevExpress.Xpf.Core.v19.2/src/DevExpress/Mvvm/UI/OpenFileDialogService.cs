namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils.CommonDialogs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;

    [TargetType(typeof(System.Windows.Controls.UserControl)), EditorBrowsable(EditorBrowsableState.Always), TargetType(typeof(Window)), Browsable(true)]
    public class OpenFileDialogService : FileDialogServiceBase, IOpenFileDialogService
    {
        public static readonly DependencyProperty MultiselectProperty = DependencyProperty.Register("Multiselect", typeof(bool), typeof(OpenFileDialogService), new PropertyMetadata(false));
        public static readonly DependencyProperty ReadOnlyCheckedProperty = DependencyProperty.Register("ReadOnlyChecked", typeof(bool), typeof(OpenFileDialogService), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowReadOnlyProperty = DependencyProperty.Register("ShowReadOnly", typeof(bool), typeof(OpenFileDialogService), new PropertyMetadata(false));
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(OpenFileDialogService), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty FilterIndexProperty = DependencyProperty.Register("FilterIndex", typeof(int), typeof(OpenFileDialogService), new PropertyMetadata(1));

        public OpenFileDialogService()
        {
            base.CheckFileExists = true;
        }

        protected override DevExpress.Utils.CommonDialogs.IFileDialog CreateFileDialogAdapter() => 
            new OpenFileDialogAdapter(new System.Windows.Forms.OpenFileDialog());

        bool IOpenFileDialogService.ShowDialog(Action<CancelEventArgs> fileOK, string directoryName)
        {
            if (directoryName != null)
            {
                base.InitialDirectory = directoryName;
            }
            bool flag = base.Show(fileOK);
            this.FilterIndex = this.OpenFileDialog.FilterIndex;
            return flag;
        }

        protected override List<object> GetFileInfos()
        {
            List<object> list = new List<object>();
            foreach (string str in this.OpenFileDialog.FileNames)
            {
                list.Add(FileInfoWrapper.Create(str));
            }
            return list;
        }

        protected override void InitFileDialog()
        {
            this.OpenFileDialog.Multiselect = this.Multiselect;
            this.OpenFileDialog.ReadOnlyChecked = this.ReadOnlyChecked;
            this.OpenFileDialog.ShowReadOnly = this.ShowReadOnly;
            this.OpenFileDialog.Filter = this.Filter;
            this.OpenFileDialog.FilterIndex = this.FilterIndex;
        }

        public bool Multiselect
        {
            get => 
                (bool) base.GetValue(MultiselectProperty);
            set => 
                base.SetValue(MultiselectProperty, value);
        }

        public bool ReadOnlyChecked
        {
            get => 
                (bool) base.GetValue(ReadOnlyCheckedProperty);
            set => 
                base.SetValue(ReadOnlyCheckedProperty, value);
        }

        public bool ShowReadOnly
        {
            get => 
                (bool) base.GetValue(ShowReadOnlyProperty);
            set => 
                base.SetValue(ShowReadOnlyProperty, value);
        }

        public string Filter
        {
            get => 
                (string) base.GetValue(FilterProperty);
            set => 
                base.SetValue(FilterProperty, value);
        }

        public int FilterIndex
        {
            get => 
                (int) base.GetValue(FilterIndexProperty);
            set => 
                base.SetValue(FilterIndexProperty, value);
        }

        private IOpenFileDialog OpenFileDialog =>
            (IOpenFileDialog) base.GetFileDialog();

        IFileInfo IOpenFileDialogService.File =>
            (IFileInfo) base.GetFiles().FirstOrDefault<object>();

        IEnumerable<IFileInfo> IOpenFileDialogService.Files =>
            base.GetFiles().Cast<IFileInfo>();

        protected class OpenFileDialogAdapter : FileDialogServiceBase.FileDialogAdapter<OpenFileDialog>, IOpenFileDialog, DevExpress.Utils.CommonDialogs.IFileDialog, ICommonDialog, IDisposable
        {
            public OpenFileDialogAdapter(OpenFileDialog fileDialog) : base(fileDialog)
            {
            }

            bool IOpenFileDialog.Multiselect
            {
                get => 
                    base.fileDialog.Multiselect;
                set => 
                    base.fileDialog.Multiselect = value;
            }

            bool IOpenFileDialog.ReadOnlyChecked
            {
                get => 
                    base.fileDialog.ReadOnlyChecked;
                set => 
                    base.fileDialog.ReadOnlyChecked = value;
            }

            bool IOpenFileDialog.ShowReadOnly
            {
                get => 
                    base.fileDialog.ShowReadOnly;
                set => 
                    base.fileDialog.ShowReadOnly = value;
            }

            string IOpenFileDialog.SafeFileName =>
                base.fileDialog.SafeFileName;

            string[] IOpenFileDialog.SafeFileNames =>
                base.fileDialog.SafeFileNames;
        }
    }
}

