namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using System;

    public class MillimetersToModelUnitsConverter : UnitConverter
    {
        public MillimetersToModelUnitsConverter(DocumentModelUnitConverter unitConverter) : base(unitConverter)
        {
        }

        public override float FromUnits(float value) => 
            base.Converter.ModelUnitsToMillimetersF(value);

        public override float ToUnits(float value) => 
            base.Converter.MillimetersToModelUnitsF(value);
    }
}

