namespace DevExpress.Office.Design.Internal
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;

    public class UnitPrecisionDictionary : Dictionary<DocumentUnit, int>
    {
        private static readonly UnitPrecisionDictionary defaultPrecisions = new UnitPrecisionDictionary();

        static UnitPrecisionDictionary()
        {
            defaultPrecisions.Add(DocumentUnit.Centimeter, 1);
            defaultPrecisions.Add(DocumentUnit.Inch, 1);
            defaultPrecisions.Add(DocumentUnit.Millimeter, 0);
            defaultPrecisions.Add(DocumentUnit.Point, 0);
        }

        protected UnitPrecisionDictionary()
        {
        }

        public static UnitPrecisionDictionary DefaultPrecisions =>
            defaultPrecisions;
    }
}

