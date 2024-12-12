namespace DevExpress.Office.Forms
{
    using System;

    public abstract class InsertSymbolViewModel : ViewModelBase
    {
        private char unicodeChar = ' ';
        private string fontName = string.Empty;
        private bool modelessBehavior;

        protected InsertSymbolViewModel()
        {
        }

        public abstract void ApplyChanges();

        public char UnicodeChar
        {
            get => 
                this.unicodeChar;
            set
            {
                if (this.UnicodeChar != value)
                {
                    this.unicodeChar = value;
                    base.OnPropertyChanged("UnicodeChar");
                }
            }
        }

        public string FontName
        {
            get => 
                this.fontName;
            set
            {
                if (this.FontName != value)
                {
                    this.fontName = value;
                    base.OnPropertyChanged("FontName");
                }
            }
        }

        public bool ModelessBehavior
        {
            get => 
                this.modelessBehavior;
            set
            {
                if (this.ModelessBehavior != value)
                {
                    this.modelessBehavior = value;
                    base.OnPropertyChanged("ModelessBehavior");
                }
            }
        }
    }
}

