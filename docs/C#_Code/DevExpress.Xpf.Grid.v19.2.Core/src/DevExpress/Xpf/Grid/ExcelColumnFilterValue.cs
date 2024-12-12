namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExcelColumnFilterValue : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private object _DisplayValue;
        private bool? _IsChecked;
        private bool _IsExpanded;

        public event EventHandler IsVisibleChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public ExcelColumnFilterValue(object editValue) : this(editValue, null)
        {
        }

        public ExcelColumnFilterValue(object editValue, object displayValue)
        {
            this._IsChecked = false;
            this.IsVisible = true;
            this.EditValue = editValue;
            this.DisplayValue = displayValue;
        }

        public virtual object GetComputedValue() => 
            this.EditValue;

        protected virtual void OnCheckedChanged(bool? oldValue, bool? newValue)
        {
        }

        protected void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void OnPropertyChanging(string propName)
        {
            if (this.PropertyChanging != null)
            {
                this.PropertyChanging(this, new PropertyChangingEventArgs(propName));
            }
        }

        public void SetIsVisible(bool value)
        {
            if (this.IsVisible != value)
            {
                this.IsVisible = value;
                if (this.IsVisibleChanged != null)
                {
                    this.IsVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        public object EditValue { get; private set; }

        public object DisplayValue
        {
            get => 
                this._DisplayValue;
            set
            {
                this._DisplayValue = value;
                this.OnPropertyChanged("DisplayValue");
            }
        }

        public bool? IsChecked
        {
            get => 
                this._IsChecked;
            set
            {
                bool? nullable2 = this._IsChecked;
                bool? nullable3 = value;
                if (!((nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault()) ? ((nullable2 != null) == (nullable3 != null)) : false))
                {
                    this.OnPropertyChanging("IsChecked");
                    bool? oldValue = this._IsChecked;
                    this._IsChecked = value;
                    this.OnCheckedChanged(oldValue, value);
                    this.OnPropertyChanged("IsChecked");
                }
            }
        }

        public bool IsVisible { get; private set; }

        public bool IsExpanded
        {
            get => 
                this._IsExpanded;
            set
            {
                if (this._IsExpanded != value)
                {
                    this._IsExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }
    }
}

