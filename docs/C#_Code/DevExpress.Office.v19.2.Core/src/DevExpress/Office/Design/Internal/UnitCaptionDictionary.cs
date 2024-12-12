namespace DevExpress.Office.Design.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Localization;
    using System;
    using System.Collections.Generic;

    public class UnitCaptionDictionary : Dictionary<DocumentUnit, OfficeStringId>
    {
        public UnitCaptionDictionary()
        {
            this.PopulateByDefaultValues();
        }

        private void PopulateByDefaultValues()
        {
            base.Add(DocumentUnit.Inch, OfficeStringId.Caption_UnitInches);
            base.Add(DocumentUnit.Centimeter, OfficeStringId.Caption_UnitCentimeters);
            base.Add(DocumentUnit.Millimeter, OfficeStringId.Caption_UnitMillimeters);
            base.Add(DocumentUnit.Point, OfficeStringId.Caption_UnitPoints);
        }
    }
}

