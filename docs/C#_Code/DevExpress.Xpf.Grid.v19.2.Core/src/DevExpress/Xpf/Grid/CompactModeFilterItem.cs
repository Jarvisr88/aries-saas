namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class CompactModeFilterItem : ICustomItem, INotifyPropertyChanged
    {
        private object displayValue;
        private object editValue;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object DisplayValue
        {
            get => 
                this.displayValue;
            set
            {
                this.displayValue = value;
                this.OnPropertyChange("DisplayValue");
            }
        }

        public object EditValue
        {
            get => 
                this.editValue;
            set
            {
                this.editValue = value;
                this.OnPropertyChange("EditValue");
            }
        }
    }
}

