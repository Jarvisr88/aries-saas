namespace DevExpress.Office.Utils
{
    using System;

    public class DXUnit : DXUnitBase
    {
        public DXUnit() : base(0f, DXUnitType.Pixel)
        {
        }

        public DXUnit(int value) : this(value, -32768f, 32767f)
        {
        }

        public DXUnit(float value) : base(value, DXUnitType.Pixel, -32768f, 32767f)
        {
        }

        public DXUnit(string value) : base(value, -32768f, 32767f)
        {
        }

        public DXUnit(float value, DXUnitType type) : this(value, type, -32768f, 32767f)
        {
        }

        public DXUnit(int value, float minValue, float maxValue) : base(value, DXUnitType.Pixel, minValue, maxValue)
        {
        }

        public DXUnit(string value, float minValue, float maxValue) : base(value, minValue, maxValue)
        {
        }

        public DXUnit(float value, DXUnitType type, float minValue, float maxValue) : base(value, type, minValue, maxValue)
        {
        }

        protected override DXUnitBase Parse(string value, float minValue, float maxValue) => 
            new StringUnitValueParser().GetUnit(value, minValue, maxValue);
    }
}

