namespace DevExpress.Office.NumberConverters
{
    using System.Collections.Generic;

    public class DigitInfoCollection : List<DigitInfo>
    {
        public DigitInfo Last =>
            base[base.Count - 1];
    }
}

