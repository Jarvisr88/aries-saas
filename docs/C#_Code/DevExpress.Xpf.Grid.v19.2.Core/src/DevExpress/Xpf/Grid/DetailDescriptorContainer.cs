namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DetailDescriptorContainer : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public DetailDescriptorContainer(DetailDescriptorBase content)
        {
            this.Content = content;
        }

        public DetailDescriptorBase Content { get; private set; }
    }
}

