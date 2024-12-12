namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class NumericMaskAttribute : MaskAttributeBase
    {
        public string Culture { get; set; }

        internal System.Globalization.CultureInfo CultureInfo { get; set; }

        protected override string CultureNameCore =>
            this.Culture;

        protected override System.Globalization.CultureInfo CultureInfoCore =>
            this.CultureInfo;
    }
}

