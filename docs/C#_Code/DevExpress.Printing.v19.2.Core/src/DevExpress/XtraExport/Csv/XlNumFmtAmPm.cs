namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtAmPm : XlNumFmtDateBase
    {
        private bool amIsLower;
        private bool pmIsLower;

        public XlNumFmtAmPm() : base(2)
        {
        }

        public XlNumFmtAmPm(bool amIsLower, bool pmIsLower) : base(1)
        {
            this.amIsLower = amIsLower;
            this.pmIsLower = pmIsLower;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            double numericValue = value.NumericValue;
            numericValue -= Math.Floor(numericValue);
            result.Text = result.Text + this.GetDesignator(numericValue < 0.5);
        }

        private string GetDesignator(bool isAM) => 
            !isAM ? (!base.IsDefaultNetDateTimeFormat ? (this.pmIsLower ? "pm" : "PM") : (this.pmIsLower ? "p" : "P")) : (!base.IsDefaultNetDateTimeFormat ? (this.amIsLower ? "am" : "AM") : (this.amIsLower ? "a" : "A"));

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.AmPm;
    }
}

