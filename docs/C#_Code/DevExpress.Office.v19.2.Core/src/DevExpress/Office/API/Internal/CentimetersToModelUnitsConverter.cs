namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using System;

    public class CentimetersToModelUnitsConverter : UnitConverter
    {
        public CentimetersToModelUnitsConverter(DocumentModelUnitConverter unitConverter) : base(unitConverter)
        {
        }

        public override float FromUnits(float value) => 
            base.Converter.ModelUnitsToCentimetersF(value);

        public override float ToUnits(float value) => 
            base.Converter.CentimetersToModelUnitsF(value);
    }
}

