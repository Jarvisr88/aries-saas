namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfType1FontPrivateData
    {
        public const double DefaultBlueScale = 0.039625;
        public const double DefaultBlueShift = 7.0;
        public const int DefaultLanguageGroup = 0;
        public const double DefaultExpansionFactor = 0.06;
        private PdfType1FontGlyphZone[] blueValues;
        private PdfType1FontGlyphZone[] otherBlues;
        private PdfType1FontGlyphZone[] familyBlues;
        private PdfType1FontGlyphZone[] familyOtherBlues;
        private double blueScale = 0.039625;
        private double blueShift = 7.0;
        private int blueFuzz;
        private double? stdHW;
        private double? stdVW;
        private double[] stemSnapH;
        private double[] stemSnapV;
        private bool forceBold;
        private double? forceBoldThreshold;
        private int languageGroup;
        private double expansionFactor = 0.06;

        protected PdfType1FontPrivateData()
        {
        }

        private static bool IsInvalidBlueValues(PdfType1FontGlyphZone[] blueValues) => 
            (blueValues != null) && (blueValues.Length > 7);

        private static bool IsInvalidOtherBlues(PdfType1FontGlyphZone[] otherBlues) => 
            (otherBlues != null) && (otherBlues.Length > 5);

        private static bool IsInvalidStemSnap(double[] stemSnap) => 
            (stemSnap != null) && (stemSnap.Length > 12);

        protected internal virtual bool Patch() => 
            false;

        protected void Validate()
        {
            if (IsInvalidBlueValues(this.blueValues) || (IsInvalidOtherBlues(this.otherBlues) || (IsInvalidBlueValues(this.familyBlues) || (IsInvalidOtherBlues(this.familyOtherBlues) || (IsInvalidStemSnap(this.stemSnapH) || IsInvalidStemSnap(this.stemSnapV))))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public PdfType1FontGlyphZone[] BlueValues
        {
            get => 
                this.blueValues;
            internal set => 
                this.blueValues = value;
        }

        public PdfType1FontGlyphZone[] OtherBlues
        {
            get => 
                this.otherBlues;
            internal set => 
                this.otherBlues = value;
        }

        public PdfType1FontGlyphZone[] FamilyBlues
        {
            get => 
                this.familyBlues;
            internal set => 
                this.familyBlues = value;
        }

        public PdfType1FontGlyphZone[] FamilyOtherBlues
        {
            get => 
                this.familyOtherBlues;
            internal set => 
                this.familyOtherBlues = value;
        }

        public double BlueScale
        {
            get => 
                this.blueScale;
            internal set => 
                this.blueScale = value;
        }

        public double BlueShift
        {
            get => 
                this.blueShift;
            internal set => 
                this.blueShift = value;
        }

        public int BlueFuzz
        {
            get => 
                this.blueFuzz;
            internal set => 
                this.blueFuzz = value;
        }

        public double? StdHW
        {
            get => 
                this.stdHW;
            internal set => 
                this.stdHW = value;
        }

        public double? StdVW
        {
            get => 
                this.stdVW;
            internal set => 
                this.stdVW = value;
        }

        public double[] StemSnapH
        {
            get => 
                this.stemSnapH;
            internal set => 
                this.stemSnapH = value;
        }

        public double[] StemSnapV
        {
            get => 
                this.stemSnapV;
            internal set => 
                this.stemSnapV = value;
        }

        public bool ForceBold
        {
            get => 
                this.forceBold;
            internal set => 
                this.forceBold = value;
        }

        public double? ForceBoldThreshold
        {
            get => 
                this.forceBoldThreshold;
            internal set => 
                this.forceBoldThreshold = value;
        }

        public int LanguageGroup
        {
            get => 
                this.languageGroup;
            internal set => 
                this.languageGroup = value;
        }

        public double ExpansionFactor
        {
            get => 
                this.expansionFactor;
            internal set => 
                this.expansionFactor = value;
        }
    }
}

