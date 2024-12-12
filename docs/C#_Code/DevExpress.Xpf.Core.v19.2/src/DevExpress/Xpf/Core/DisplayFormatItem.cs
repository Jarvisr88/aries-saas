namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DisplayFormatItem : INotifyPropertyChanged
    {
        private string example;
        private string value;
        private DisplayFormatGroupType group;

        public event PropertyChangedEventHandler PropertyChanged;

        public DisplayFormatItem(string value) : this(value, DisplayFormatGroupType.Default, "")
        {
        }

        public DisplayFormatItem(string value, DisplayFormatGroupType group) : this(value, group, "")
        {
        }

        public DisplayFormatItem(string value, DisplayFormatGroupType group, string example)
        {
            this.Value = value;
            this.Group = group;
            this.Example = value;
        }

        protected bool ChangeProperty<T>(ref T property, T value, string propertyName)
        {
            if (Equals((T) property, value))
            {
                return false;
            }
            property = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Value
        {
            get => 
                this.value;
            set => 
                this.ChangeProperty<string>(ref this.value, value, "Value");
        }

        public DisplayFormatGroupType Group
        {
            get => 
                this.group;
            set => 
                this.ChangeProperty<DisplayFormatGroupType>(ref this.group, value, "Group");
        }

        public string Example
        {
            get => 
                this.example;
            set => 
                this.ChangeProperty<string>(ref this.example, value, "Example");
        }
    }
}

