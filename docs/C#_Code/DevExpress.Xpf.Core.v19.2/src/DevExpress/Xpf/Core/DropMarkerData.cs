namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class DropMarkerData : INotifyPropertyChanged
    {
        private DropPosition positionCore;
        private System.Windows.Controls.Orientation orientationCore;
        private Thickness paddingCore;
        private PropertyChangedEventHandler propertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.propertyChanged += value;
            }
            remove
            {
                this.propertyChanged -= value;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.propertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DropPosition Position
        {
            get => 
                this.positionCore;
            set
            {
                if (this.positionCore != value)
                {
                    this.positionCore = value;
                    this.RaisePropertyChanged("Position");
                }
            }
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                this.orientationCore;
            set
            {
                if (this.orientationCore != value)
                {
                    this.orientationCore = value;
                    this.RaisePropertyChanged("Orientation");
                }
            }
        }

        public Thickness Padding
        {
            get => 
                this.paddingCore;
            set
            {
                if (this.paddingCore != value)
                {
                    this.paddingCore = value;
                    this.RaisePropertyChanged("Padding");
                }
            }
        }
    }
}

