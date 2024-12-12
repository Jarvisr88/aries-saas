namespace DevExpress.Office.Design.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.API.Internal;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;

    public static class PaperKindDescriptionHelper
    {
        public static string CalculateDescription(PaperKind paperKind, string displayName, DocumentUnit unit, Dictionary<DocumentUnit, UnitConverter> unitConverters, DocumentModelUnitConverter documentModelUnitConverter)
        {
            unit = (unit == DocumentUnit.Document) ? DocumentUnit.Inch : unit;
            UnitConverter converter = unitConverters[unit];
            Size size = PaperSizeCalculator.CalculatePaperSize(paperKind);
            UIUnit unit2 = new UIUnit(converter.FromUnits((float) documentModelUnitConverter.TwipsToModelUnits(size.Width)), unit, UnitPrecisionDictionary.DefaultPrecisions);
            UIUnit unit3 = new UIUnit(converter.FromUnits((float) documentModelUnitConverter.TwipsToModelUnits(size.Height)), unit, UnitPrecisionDictionary.DefaultPrecisions);
            return $"{displayName}
{unit2.ToString()} x {unit3.ToString()}";
        }
    }
}

