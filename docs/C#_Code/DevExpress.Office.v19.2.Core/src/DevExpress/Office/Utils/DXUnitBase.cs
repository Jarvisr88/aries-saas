namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DXUnitBase
    {
        private readonly float value;
        private readonly DXUnitType type;

        protected DXUnitBase(float value, DXUnitType type)
        {
            this.value = this.ObtainValue(value, type);
            this.type = type;
        }

        protected DXUnitBase(string value, float minValue, float maxValue)
        {
            DXUnitBase base2 = this.Parse(value, minValue, maxValue);
            this.value = base2.value;
            this.type = base2.type;
        }

        protected DXUnitBase(int value, DXUnitType type, float minValue, float maxValue)
        {
            this.ValidateValueRange((float) value, minValue, maxValue);
            this.value = value;
            this.type = type;
        }

        protected DXUnitBase(float value, DXUnitType type, float minValue, float maxValue)
        {
            this.ValidateValueRange(value, minValue, maxValue);
            this.value = this.ObtainValue(value, type);
            this.type = type;
        }

        private float ObtainValue(float value, DXUnitType type)
        {
            if ((type == DXUnitType.Pixel) || (type == DXUnitType.Emu))
            {
                value = (int) value;
            }
            return value;
        }

        protected abstract DXUnitBase Parse(string value, float minValue, float maxValue);
        private void ValidateValueRange(float value, float minValue, float maxValue)
        {
            if ((value < minValue) || (value > maxValue))
            {
                throw new ArgumentOutOfRangeException("value");
            }
        }

        public float Value =>
            this.value;

        public DXUnitType Type =>
            this.type;
    }
}

