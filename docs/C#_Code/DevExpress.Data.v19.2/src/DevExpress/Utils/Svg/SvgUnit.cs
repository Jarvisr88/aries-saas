namespace DevExpress.Utils.Svg
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class SvgUnit : ICloneable
    {
        public static string None = "none";

        public SvgUnit(double unitValue) : this(SvgUnitType.Px, unitValue)
        {
        }

        public SvgUnit(SvgUnitType unitType, double unitValue)
        {
            this.UnitType = unitType;
            this.UnitValue = unitValue;
            this.ConvertToPixels();
        }

        public object Clone() => 
            new SvgUnit(this.UnitType, this.UnitValue);

        private void ConvertToPixels()
        {
            float num3;
            int num = 0x60;
            float num2 = 2.54f;
            switch (this.UnitType)
            {
                case SvgUnitType.Em:
                    num3 = (float) (this.UnitValue * 9.0);
                    this.Value = (num3 / 72f) * num;
                    return;

                case SvgUnitType.Ex:
                    num3 = (float) (this.UnitValue * 9.0);
                    this.Value = (num3 / 144f) * num;
                    return;

                case SvgUnitType.Px:
                    this.Value = this.UnitValue;
                    return;

                case SvgUnitType.In:
                    this.Value = this.UnitValue * num;
                    return;

                case SvgUnitType.Cm:
                    this.Value = (float) ((this.UnitValue / ((double) num2)) * num);
                    return;

                case SvgUnitType.Mm:
                    this.Value = ((float) (this.UnitValue / ((double) (10f * num2)))) * num;
                    return;

                case SvgUnitType.Pt:
                    this.Value = (this.UnitValue / 72.0) * num;
                    return;

                case SvgUnitType.Pc:
                    this.Value = ((this.UnitValue * 12.0) / 72.0) * num;
                    return;

                case SvgUnitType.Percentage:
                    this.Value = this.UnitValue / 100.0;
                    return;
            }
        }

        public override string ToString() => 
            (this.UnitType != SvgUnitType.Percentage) ? (this.UnitValue.ToString(CultureInfo.InvariantCulture) + this.UnitType.ToString().ToLower()) : (this.UnitValue.ToString(CultureInfo.InvariantCulture) + "%");

        public double Value { get; private set; }

        public double UnitValue { get; private set; }

        public SvgUnitType UnitType { get; private set; }
    }
}

