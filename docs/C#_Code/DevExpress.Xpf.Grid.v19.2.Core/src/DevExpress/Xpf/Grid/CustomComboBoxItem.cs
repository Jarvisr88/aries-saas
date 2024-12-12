namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;

    public class CustomComboBoxItem : ICustomItem, INotifyPropertyChanged
    {
        private object displayValue;
        private object editValue;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public object DisplayValue
        {
            get => 
                this.displayValue;
            set => 
                this.displayValue = value;
        }

        public object EditValue
        {
            get => 
                this.editValue;
            set => 
                this.editValue = value;
        }
    }
}

