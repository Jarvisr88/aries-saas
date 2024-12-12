namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class StartScreenControl : Control
    {
        private static readonly DependencyPropertyKey ActualRecentFilesPropertyKey;
        public static readonly DependencyProperty ActualRecentFilesProperty;
        public static readonly DependencyProperty RecentFilesProperty;
        public static readonly DependencyProperty NumberOfRecentFilesProperty;
        public static readonly DependencyProperty ShowOpenFileButtonProperty;

        static StartScreenControl()
        {
            Type ownerType = typeof(StartScreenControl);
            RecentFilesProperty = DependencyPropertyManager.Register("RecentFiles", typeof(ObservableCollection<RecentFileViewModel>), ownerType, new PropertyMetadata(null, (obj, args) => ((StartScreenControl) obj).OnRecentFilesChanged((ObservableCollection<RecentFileViewModel>) args.OldValue, (ObservableCollection<RecentFileViewModel>) args.NewValue)));
            NumberOfRecentFilesProperty = DependencyPropertyManager.Register("NumberOfRecentFiles", typeof(int), ownerType, new PropertyMetadata(5, (obj, args) => ((StartScreenControl) obj).OnNumberOfRecentFilesChanged((int) args.NewValue)));
            ShowOpenFileButtonProperty = DependencyPropertyManager.Register("ShowOpenFileButton", typeof(bool), ownerType, new PropertyMetadata(true));
            ActualRecentFilesPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualRecentFiles", typeof(ObservableCollection<RecentFileViewModel>), ownerType, new PropertyMetadata(null));
            ActualRecentFilesProperty = ActualRecentFilesPropertyKey.DependencyProperty;
        }

        public StartScreenControl()
        {
            this.SetDefaultStyleKey(typeof(StartScreenControl));
        }

        protected virtual void OnNumberOfRecentFilesChanged(int newValue)
        {
            this.UpdateActualRecentFiles();
        }

        protected virtual void OnRecentFilesChanged(ObservableCollection<RecentFileViewModel> oldValue, ObservableCollection<RecentFileViewModel> newValue)
        {
            oldValue.Do<ObservableCollection<RecentFileViewModel>>(delegate (ObservableCollection<RecentFileViewModel> x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnRecentFilesCollectionChanged);
            });
            this.UpdateActualRecentFiles();
            newValue.Do<ObservableCollection<RecentFileViewModel>>(delegate (ObservableCollection<RecentFileViewModel> x) {
                x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnRecentFilesCollectionChanged);
            });
        }

        protected virtual void OnRecentFilesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateActualRecentFiles();
        }

        private void UpdateActualRecentFiles()
        {
            if (this.RecentFiles != null)
            {
                ObservableCollection<RecentFileViewModel> observables = new ObservableCollection<RecentFileViewModel>();
                if (this.RecentFiles.Count > this.NumberOfRecentFiles)
                {
                    for (int i = this.RecentFiles.Count - 1; i >= (this.RecentFiles.Count - this.NumberOfRecentFiles); i--)
                    {
                        observables.Add(this.RecentFiles[i]);
                    }
                }
                else
                {
                    foreach (RecentFileViewModel model in this.RecentFiles)
                    {
                        observables.Insert(0, model);
                    }
                }
                this.ActualRecentFiles = observables;
            }
        }

        public ObservableCollection<RecentFileViewModel> RecentFiles
        {
            get => 
                (ObservableCollection<RecentFileViewModel>) base.GetValue(RecentFilesProperty);
            set => 
                base.SetValue(RecentFilesProperty, value);
        }

        public ObservableCollection<RecentFileViewModel> ActualRecentFiles
        {
            get => 
                (ObservableCollection<RecentFileViewModel>) base.GetValue(ActualRecentFilesProperty);
            private set => 
                base.SetValue(ActualRecentFilesPropertyKey, value);
        }

        public int NumberOfRecentFiles
        {
            get => 
                (int) base.GetValue(NumberOfRecentFilesProperty);
            set => 
                base.SetValue(NumberOfRecentFilesProperty, value);
        }

        public bool ShowOpenFileButton
        {
            get => 
                (bool) base.GetValue(ShowOpenFileButtonProperty);
            set => 
                base.SetValue(ShowOpenFileButtonProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StartScreenControl.<>c <>9 = new StartScreenControl.<>c();

            internal void <.cctor>b__5_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((StartScreenControl) obj).OnRecentFilesChanged((ObservableCollection<RecentFileViewModel>) args.OldValue, (ObservableCollection<RecentFileViewModel>) args.NewValue);
            }

            internal void <.cctor>b__5_1(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((StartScreenControl) obj).OnNumberOfRecentFilesChanged((int) args.NewValue);
            }
        }
    }
}

