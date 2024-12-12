namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTextSearchParameters
    {
        private PdfTextSearchDirection direction = PdfTextSearchDirection.Forward;
        private PdfTextSearchOptions options = PdfTextSearchOptions.None;

        internal PdfTextSearchParameters CloneParameters()
        {
            PdfTextSearchParameters parameters1 = new PdfTextSearchParameters();
            parameters1.direction = this.direction;
            parameters1.options = this.options;
            return parameters1;
        }

        internal bool EqualsTo(PdfTextSearchParameters newParams) => 
            (newParams != null) && (this.options == newParams.options);

        private bool GetOption(PdfTextSearchOptions option) => 
            (this.options & option) != PdfTextSearchOptions.None;

        private void SetOption(PdfTextSearchOptions option, bool value)
        {
            if (value)
            {
                this.options |= option;
            }
            else
            {
                this.options &= ~option;
            }
        }

        public PdfTextSearchDirection Direction
        {
            get => 
                this.direction;
            set => 
                this.direction = value;
        }

        public bool CaseSensitive
        {
            get => 
                this.GetOption(PdfTextSearchOptions.CaseSensitive);
            set => 
                this.SetOption(PdfTextSearchOptions.CaseSensitive, value);
        }

        public bool WholeWords
        {
            get => 
                this.GetOption(PdfTextSearchOptions.WholeWords);
            set => 
                this.SetOption(PdfTextSearchOptions.WholeWords, value);
        }
    }
}

