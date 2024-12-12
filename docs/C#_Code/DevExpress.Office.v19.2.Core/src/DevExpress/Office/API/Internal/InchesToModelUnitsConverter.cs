namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using System;

    public class InchesToModelUnitsConverter : UnitConverter
    {
        public InchesToModelUnitsConverter(DocumentModelUnitConverter unitConverter) : base(unitConverter)
        {
        }

        public override float FromUnits(float value) => 
            base.Converter.ModelUnitsToInchesF(value);

        public override float ToUnits(float value) => 
            base.Converter.InchesToModelUnitsF(value);
    }
}

