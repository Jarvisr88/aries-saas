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

    [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), TargetType(typeof(Window)), TargetType(typeof(System.Windows.Controls.UserControl))]
    public class SaveFileDialogService : FileDialogServiceBase, ISaveFileDialogService
    {
        public static readonly DependencyProperty CreatePromptProperty = DependencyProperty.Register("CreatePrompt", typeof(bool), typeof(SaveFileDialogService), new PropertyMetadata(false));
        public static readonly DependencyProperty OverwritePromptProperty = DependencyProperty.Register("OverwritePrompt", typeof(bool), typeof(SaveFileDialogService), new PropertyMetadata(true));
        public static readonly DependencyProperty DefaultExtProperty = DependencyProperty.Register("DefaultExt", typeof(string), typeof(SaveFileDialogService), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(SaveFileDialogService), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty FilterIndexProperty = DependencyProperty.Register("FilterIndex", typeof(int), typeof(SaveFileDialogService), new PropertyMetadata(1));

        public SaveFileDialogService()
        {
            base.CheckFileExists = false;
        }

        protected override DevExpress.Utils.CommonDialogs.IFileDialog CreateFileDialogAdapter() => 
            new SaveFileDialogAdapter(new System.Windows.Forms.SaveFileDialog());

        bool ISaveFileDialogService.ShowDialog(Action<CancelEventArgs> fileOK, string directoryName, string fileName)
        {
            if (directoryName != null)
            {
                base.InitialDirectory = directoryName;
            }
            if (fileName != null)
            {
                base.DefaultFileName = fileName;
            }
            bool flag = base.Show(fileOK);
            this.FilterIndex = this.SaveFileDialog.FilterIndex;
            return flag;
        }

        protected override List<object> GetFileInfos()
        {
            List<object> list = new List<object>();
            foreach (string str in this.SaveFileDialog.FileNames)
            {
                list.Add(FileInfoWrapper.Create(str));
            }
            return list;
        }

        protected override void InitFileDialog()
        {
            this.SaveFileDialog.CreatePrompt = this.CreatePrompt;
            this.SaveFileDialog.OverwritePrompt = this.OverwritePrompt;
            this.SaveFileDialog.FileName = base.DefaultFileName;
            this.SaveFileDialog.DefaultExt = this.DefaultExt;
            this.SaveFileDialog.Filter = this.Filter;
            this.SaveFileDialog.FilterIndex = this.FilterIndex;
        }

        public bool CreatePrompt
        {
            get => 
                (bool) base.GetValue(CreatePromptProperty);
            set => 
                base.SetValue(CreatePromptProperty, value);
        }

        public bool OverwritePrompt
        {
            get => 
                (bool) base.GetValue(OverwritePromptProperty);
            set => 
                base.SetValue(OverwritePromptProperty, value);
        }

        public string DefaultExt
        {
            get => 
                (string) base.GetValue(DefaultExtProperty);
            set => 
                base.SetValue(DefaultExtProperty, value);
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

        private ISaveFileDialog SaveFileDialog =>
            (ISaveFileDialog) base.GetFileDialog();

        IFileInfo ISaveFileDialogService.File =>
            (IFileInfo) base.GetFiles().FirstOrDefault<object>();

        protected class SaveFileDialogAdapter : FileDialogServiceBase.FileDialogAdapter<SaveFileDialog>, ISaveFileDialog, DevExpress.Utils.CommonDialogs.IFileDialog, ICommonDialog, IDisposable
        {
            public SaveFileDialogAdapter(SaveFileDialog fileDialog) : base(fileDialog)
            {
            }

            bool ISaveFileDialog.CreatePrompt
            {
                get => 
                    base.fileDialog.CreatePrompt;
                set => 
                    base.fileDialog.CreatePrompt = value;
            }

            bool ISaveFileDialog.OverwritePrompt
            {
                get => 
                    base.fileDialog.OverwritePrompt;
                set => 
                    base.fileDialog.OverwritePrompt = value;
            }
        }
    }
}

