namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DateTimePickerPart : INotifyPropertyChanged
    {
        private DateTimePickerData selectedItem;
        private IEnumerable<DateTimePickerData> items;
        private bool isLooped;
        private bool isAnimated;
        private bool isExpanded;
        private int visibleItemsCount;
        private bool useTransitions;
        private bool isEnabled = true;

        public event EventHandler<AnimatedChangedEventArgs> AnimatedChanged;

        public event EventHandler<ExpandedChangedEventArgs> ExpandedChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private void IsAnimatedChanged(bool oldValue, bool newValue)
        {
            this.RaiseAnimatedChanged(newValue);
        }

        private void IsExpandedChanged(bool oldValue, bool newValue)
        {
            this.RaiseExpandedChanged(newValue);
        }

        private void RaiseAnimatedChanged(bool value)
        {
            if (this.AnimatedChanged != null)
            {
                this.AnimatedChanged(this, new AnimatedChangedEventArgs(value));
            }
        }

        private void RaiseExpandedChanged(bool value)
        {
            if (this.ExpandedChanged != null)
            {
                this.ExpandedChanged(this, new ExpandedChangedEventArgs(value));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SetValue<T>(string propertyName, ref T field, T newValue)
        {
            this.SetValue<T>(propertyName, ref field, newValue, null);
        }

        private void SetValue<T>(string propertyName, ref T field, T newValue, RaisePropertyChangedDelegate<T> raiseChangedDelegate)
        {
            if (!Equals((T) field, newValue))
            {
                T oldValue = field;
                field = newValue;
                this.RaisePropertyChanged(propertyName);
                if (raiseChangedDelegate != null)
                {
                    raiseChangedDelegate(oldValue, newValue);
                }
            }
        }

        public DateTimePickerData SelectedItem
        {
            get => 
                this.selectedItem;
            set => 
                this.SetValue<DateTimePickerData>("SelectedItem", ref this.selectedItem, value);
        }

        public int VisibleItemsCount
        {
            get => 
                this.visibleItemsCount;
            set => 
                this.SetValue<int>("VisibleItemsCount", ref this.visibleItemsCount, value);
        }

        public IEnumerable<DateTimePickerData> Items
        {
            get => 
                this.items;
            set => 
                this.SetValue<IEnumerable<DateTimePickerData>>("Items", ref this.items, value);
        }

        public bool IsAnimated
        {
            get => 
                this.isAnimated;
            set => 
                this.SetValue<bool>("IsAnimated", ref this.isAnimated, value, new RaisePropertyChangedDelegate<bool>(this.IsAnimatedChanged));
        }

        public bool IsExpanded
        {
            get => 
                this.isExpanded;
            set => 
                this.SetValue<bool>("IsExpanded", ref this.isExpanded, value, new RaisePropertyChangedDelegate<bool>(this.IsExpandedChanged));
        }

        public bool IsLooped
        {
            get => 
                this.isLooped;
            set => 
                this.SetValue<bool>("IsLooped", ref this.isLooped, value);
        }

        public bool UseTransitions
        {
            get => 
                this.useTransitions;
            set => 
                this.SetValue<bool>("UseTransitions", ref this.useTransitions, value);
        }

        public bool IsEnabled
        {
            get => 
                this.isEnabled;
            set => 
                this.SetValue<bool>("IsEnabled", ref this.isEnabled, value);
        }
    }
}

