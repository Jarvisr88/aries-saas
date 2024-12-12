namespace DevExpress.Office.API.Internal
{
    using DevExpress.Office;
    using System;

    public class PointsToModelUnitsConverter : UnitConverter
    {
        public PointsToModelUnitsConverter(DocumentModelUnitConverter unitConverter) : base(unitConverter)
        {
        }

        public override float FromUnits(float value) => 
            base.Converter.ModelUnitsToPointsF(value);

        public override float ToUnits(float value) => 
            base.Converter.PointsToModelUnitsF(value);
    }
}

