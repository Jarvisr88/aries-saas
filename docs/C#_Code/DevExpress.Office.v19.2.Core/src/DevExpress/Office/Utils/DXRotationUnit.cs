namespace DevExpress.Office.Utils
{
    using System;

    public class DXRotationUnit : DXUnitBase
    {
        public DXRotationUnit() : base(0f, DXUnitType.Deg)
        {
        }

        public DXRotationUnit(string value) : base(value, -2.147484E+09f, 2.147484E+09f)
        {
        }

        public DXRotationUnit(float value, DXUnitType type, float minValue, float maxValue) : base(value, type, minValue, maxValue)
        {
        }

        protected override DXUnitBase Parse(string value, float minValue, float maxValue) => 
            new StringUnitValueParser().GetRotationUnitType(value, minValue, maxValue);
    }
}

