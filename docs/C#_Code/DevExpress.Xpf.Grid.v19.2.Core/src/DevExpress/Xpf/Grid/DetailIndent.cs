namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DetailIndent : INotifyPropertyChanged
    {
        private double width;
        private double widthAtRight;
        private int level;
        private bool isDetailMargin;
        private bool isLast;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void Update(double width, double widthAtRight, int level, bool isDetailMargin, bool isLast)
        {
            this.Width = width;
            this.WidthAtRight = widthAtRight;
            this.Level = level;
            this.IsDetailMargin = isDetailMargin;
            this.IsLast = isLast;
        }

        public double Width
        {
            get => 
                this.width;
            private set
            {
                if (this.width != value)
                {
                    this.width = value;
                    this.RaisePropertyChanged("Width");
                }
            }
        }

        public double WidthAtRight
        {
            get => 
                this.widthAtRight;
            private set
            {
                if (this.widthAtRight != value)
                {
                    this.widthAtRight = value;
                    this.RaisePropertyChanged("WidthAtRight");
                }
            }
        }

        public int Level
        {
            get => 
                this.level;
            private set
            {
                if (this.level != value)
                {
                    this.level = value;
                    this.RaisePropertyChanged("Level");
                }
            }
        }

        public bool IsDetailMargin
        {
            get => 
                this.isDetailMargin;
            private set
            {
                if (this.isDetailMargin != value)
                {
                    this.isDetailMargin = value;
                    this.RaisePropertyChanged("IsDetailMargin");
                }
            }
        }

        public bool IsLast
        {
            get => 
                this.isLast;
            private set
            {
                if (this.isLast != value)
                {
                    this.isLast = value;
                    this.RaisePropertyChanged("IsLast");
                }
            }
        }
    }
}

