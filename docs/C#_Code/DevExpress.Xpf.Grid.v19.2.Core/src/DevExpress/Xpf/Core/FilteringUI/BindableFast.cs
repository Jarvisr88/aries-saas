namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class BindableFast : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected BindableFast()
        {
        }

        protected void RaisePropertyChanged(string propertyName = null)
        {
            if (this.PropertyChanged == null)
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            }
            else
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetValue<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals((T) storage, value))
            {
                return false;
            }
            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }
    }
}

