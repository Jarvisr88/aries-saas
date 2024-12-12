namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class DateTimeMaskAttribute : MaskAttributeBase
    {
        public DateTimeMaskAttribute()
        {
            base.Mask = "d";
        }

        internal override bool IsDefaultMask(string mask) => 
            mask == "d";

        public string Culture { get; set; }

        internal System.Globalization.CultureInfo CultureInfo { get; set; }

        public bool AutomaticallyAdvanceCaret { get; set; }

        protected override string CultureNameCore =>
            this.Culture;

        protected override System.Globalization.CultureInfo CultureInfoCore =>
            this.CultureInfo;

        internal override bool AutomaticallyAdvanceCaretInternal =>
            this.AutomaticallyAdvanceCaret;
    }
}

