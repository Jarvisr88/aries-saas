namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfRGBColor : INotifyPropertyChanged
    {
        private const string redComponentName = "R";
        private const string greenComponentName = "G";
        private const string blueComponentName = "B";
        private double red;
        private double green;
        private double blue;

        public event PropertyChangedEventHandler PropertyChanged;

        public PdfRGBColor()
        {
        }

        internal PdfRGBColor(PdfColor color)
        {
            PdfRGBColorData data = new PdfRGBColorData(color);
            this.R = data.R;
            this.G = data.G;
            this.B = data.B;
        }

        public PdfRGBColor(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ValidateColorComponent(double component, string componentName)
        {
            if ((component < 0.0) || (component > 1.0))
            {
                throw new ArgumentOutOfRangeException(componentName, PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectColorComponentValue));
            }
        }

        public double R
        {
            get => 
                this.red;
            set
            {
                this.ValidateColorComponent(value, "R");
                if (this.red != value)
                {
                    this.red = value;
                    this.RaisePropertyChanged("R");
                }
            }
        }

        public double G
        {
            get => 
                this.green;
            set
            {
                this.ValidateColorComponent(value, "G");
                if (this.green != value)
                {
                    this.green = value;
                    this.RaisePropertyChanged("G");
                }
            }
        }

        public double B
        {
            get => 
                this.blue;
            set
            {
                this.ValidateColorComponent(value, "B");
                if (this.blue != value)
                {
                    this.blue = value;
                    this.RaisePropertyChanged("B");
                }
            }
        }
    }
}

