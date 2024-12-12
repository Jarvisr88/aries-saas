namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.ComponentModel;

    public abstract class PdfMarkupAnnotationData
    {
        private Lazy<PdfRGBColor> color;

        protected PdfMarkupAnnotationData()
        {
            this.color = this.CreateColorContainer();
        }

        private void ColorChanged(object sender, PropertyChangedEventArgs e)
        {
            this.SetColor(sender as PdfRGBColor);
        }

        private Lazy<PdfRGBColor> CreateColorContainer() => 
            new Lazy<PdfRGBColor>(delegate {
                PdfRGBColor color = (this.Annotation.Color == null) ? null : new PdfRGBColor(this.Annotation.Color);
                if (color != null)
                {
                    color.PropertyChanged += new PropertyChangedEventHandler(this.ColorChanged);
                }
                return color;
            });

        protected void RebuildAppearance()
        {
            this.AnnotationState.RebuildAppearance();
        }

        private void SetColor(PdfRGBColor value)
        {
            object obj1;
            if (value == null)
            {
                obj1 = null;
            }
            else
            {
                double[] components = new double[] { value.R, value.G, value.B };
                obj1 = new PdfColor(components);
            }
            PdfColor actualValue = (PdfColor) obj1;
            this.SetPropertyValue<PdfColor>(this.Annotation.Color, actualValue, () => this.Annotation.Color = actualValue);
            this.RebuildAppearance();
        }

        protected void SetPropertyValue<T>(T currentValue, T newValue, Action setNewValue)
        {
            if (!Equals(currentValue, newValue))
            {
                this.AnnotationState.NotifyAnnotationChanged();
                setNewValue();
            }
        }

        public PdfRectangle Bounds =>
            this.Annotation.Rect;

        public string Name
        {
            get => 
                this.Annotation.Name;
            set => 
                this.SetPropertyValue<string>(this.Annotation.Name, value, () => this.Annotation.Name = value);
        }

        public PdfRGBColor Color
        {
            get => 
                this.color.Value;
            set
            {
                PdfRGBColor color = this.Color;
                if ((color == null) || ((value == null) || ((color.R != value.R) || ((color.G != value.G) || (color.B != value.B)))))
                {
                    if (this.color.IsValueCreated && (this.color.Value != null))
                    {
                        this.color.Value.PropertyChanged -= new PropertyChangedEventHandler(this.ColorChanged);
                    }
                    this.SetColor(value);
                    this.color = this.CreateColorContainer();
                }
            }
        }

        public double Opacity
        {
            get => 
                this.Annotation.Opacity;
            set
            {
                if ((value < 0.0) || (value > 1.0))
                {
                    throw new ArgumentOutOfRangeException("value", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectOpacity));
                }
                this.SetPropertyValue<double>(this.Annotation.Opacity, value, () => this.Annotation.Opacity = value);
                this.RebuildAppearance();
            }
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.Annotation.CreationDate;
            set => 
                this.SetPropertyValue<DateTimeOffset?>(this.Annotation.CreationDate, value, () => this.Annotation.CreationDate = value);
        }

        public DateTimeOffset? ModificationDate
        {
            get => 
                this.Annotation.Modified;
            set => 
                this.SetPropertyValue<DateTimeOffset?>(this.Annotation.Modified, value, () => this.Annotation.Modified = value);
        }

        public string Author
        {
            get => 
                this.Annotation.Title;
            set => 
                this.SetPropertyValue<string>(this.Annotation.Title, value, () => this.Annotation.Title = value);
        }

        public string Subject
        {
            get => 
                this.Annotation.Subject;
            set => 
                this.SetPropertyValue<string>(this.Annotation.Subject, value, () => this.Annotation.Subject = value);
        }

        public string Contents
        {
            get => 
                this.Annotation.Contents;
            set => 
                this.SetPropertyValue<string>(this.Annotation.Contents, value, () => this.Annotation.Contents = value);
        }

        internal Guid CollectionID =>
            this.Annotation.Page.DocumentCatalog.Objects.Id;

        protected abstract PdfMarkupAnnotation Annotation { get; }

        protected internal abstract PdfMarkupAnnotationState AnnotationState { get; }
    }
}

