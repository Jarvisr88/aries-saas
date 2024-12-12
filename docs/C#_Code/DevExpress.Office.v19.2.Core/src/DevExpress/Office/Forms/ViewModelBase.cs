namespace DevExpress.Office.Forms
{
    using System;
    using System.ComponentModel;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler onPropertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                this.onPropertyChanged += value;
            }
            remove
            {
                this.onPropertyChanged -= value;
            }
        }

        protected ViewModelBase()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.onPropertyChanged != null)
            {
                this.onPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

