namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class VmlUnitConverter
    {
        private readonly DocumentModelUnitConverter unitConverter;

        public VmlUnitConverter(DocumentModelUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public int ToModelUnits(DXVmlUnit unit) => 
            (int) Math.Round((double) this.ToModelUnitsF(unit));

        public float ToModelUnitsF(DXVmlUnit unit)
        {
            switch (unit.Type)
            {
                case DXUnitType.Pixel:
                    return this.unitConverter.PointsToModelUnitsF(unit.Value);

                case DXUnitType.Point:
                    return this.unitConverter.PointsToModelUnitsF(unit.Value);

                case DXUnitType.Pica:
                    return this.unitConverter.PicasToModelUnitsF(unit.Value);

                case DXUnitType.Inch:
                    return this.unitConverter.InchesToModelUnitsF(unit.Value);

                case DXUnitType.Mm:
                    return this.unitConverter.MillimetersToModelUnitsF(unit.Value);

                case DXUnitType.Cm:
                    return this.unitConverter.CentimetersToModelUnitsF(unit.Value);
            }
            return this.unitConverter.EmuToModelUnitsF((int) Math.Round((double) unit.Value));
        }
    }
}

