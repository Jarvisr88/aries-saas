namespace DevExpress.Office.Design.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Localization;
    using System;
    using System.Collections.Generic;

    public class UnitAbbreviationDictionary : Dictionary<DocumentUnit, OfficeStringId>
    {
        public UnitAbbreviationDictionary()
        {
            this.PopulateByDefaultValues();
        }

        private void PopulateByDefaultValues()
        {
            base.Add(DocumentUnit.Centimeter, OfficeStringId.UnitAbbreviation_Centimeter);
            base.Add(DocumentUnit.Inch, OfficeStringId.UnitAbbreviation_Inch);
            base.Add(DocumentUnit.Millimeter, OfficeStringId.UnitAbbreviation_Millimeter);
            base.Add(DocumentUnit.Point, OfficeStringId.UnitAbbreviation_Point);
        }
    }
}

